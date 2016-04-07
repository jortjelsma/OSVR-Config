/// OSVR-Config
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>
///
module app.devices {

    class DevicesController {
        configRoot: app.common.IOSVRConfig;
        config: any;
        displays: common.IOSVRDisplay[];

        static $inject = ["app.common.ConfigService"];
        constructor(private configService: app.common.IConfigService) {
            configService.getAvailableDisplays().then(displays => this.displays = displays);
            configService.getCurrent().then(config => { this.configRoot = config; });
        }

        clickDisplay(display: common.IOSVRDisplay) {
            this.configRoot.body.display = display.relativePath;
            this.configService.setCurrent(this.configRoot);
        }

        viewDisplay(display: common.IOSVRDisplay) {
            var temp = display.showDetail;
            var i = 0;
            for (i = 0; i < this.displays.length; i++) {
                this.displays[i].showDetail = false;
            }

            display.showDetail = !temp;
        }

        showHideDisplayLabelKey(display: common.IOSVRDisplay) {
            return display.showDetail ? "devices.hideDisplay" : "devices.showDisplay";
        }
    }

    angular.module("app.devices", ["app.common.ConfigService", "ui.router"])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("devices", {
                url: "/devices",
                templateUrl: "app/devices/devices.html",
                controller: "app.devices.DevicesController as vm"
            });
        }])
        .controller("app.devices.DevicesController", DevicesController);
}
