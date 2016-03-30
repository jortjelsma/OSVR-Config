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
module app.samples {
    interface ISampleConfig {
        fileName: string;
        body: common.IOSVRConfig;
        showDetail?: boolean;
    }

    class SamplesController {
        sampleConfigs: ISampleConfig[] = [];
        static $inject = ["$http", "$log", "app.common.ConfigService"];
        constructor(private $http: ng.IHttpService, private $log: ng.ILogService, private configService: app.common.IConfigService) {
            $http.get("/api/sampleConfigs").then(result => {
                this.sampleConfigs = <ISampleConfig[]>result.data;
            });
        }

        clickSampleConfig(sampleConfig: ISampleConfig) {
            this.configService.setCurrent(sampleConfig.body);
        }

        viewSampleConfig(sampleConfig: ISampleConfig) {
            var i = 0;
            for (i = 0; i < this.sampleConfigs.length; i++) {
                this.sampleConfigs[i].showDetail = false;
            }
            sampleConfig.showDetail = true;
        }
    }

    angular.module("app.samples", ["ui.router"])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("samples", {
                url: "/samples",
                templateUrl: "app/samples/samples.html",
                controller: "app.samples.SamplesController as vm"
            });
        }])
        .controller("app.samples.SamplesController", SamplesController);
}
