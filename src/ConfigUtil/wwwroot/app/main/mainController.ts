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
module app.main {
    interface INavbarItem {
        labelTranslateKey: string;
        state: string;
    }

    class MainController {
        navbarItems: INavbarItem[] = [
            {
                labelTranslateKey: "index.navBarItems.devices",
                state: "devices"
            },
            {
                labelTranslateKey: "index.navBarItems.renderManager",
                state: "renderManager"
            },
            {
                labelTranslateKey: "index.navBarItems.plugins",
                state: "plugins"
            },
            {
                labelTranslateKey: "index.navBarItems.samples",
                state: "samples"
            },
            {
                labelTranslateKey: "index.navBarItems.tools",
                state: "tools"
            }
        ];

        serverRoot: string;

        isActive(navBarItem: INavbarItem): boolean {
            return this.$state.current.name == navBarItem.state;
        }

        serverRootDefined(): boolean {
            return angular.isDefined(this.serverRoot) &&
                angular.isString(this.serverRoot) &&
                this.serverRoot.length > 0;
        }

        static $inject = ["$timeout", "$state", "app.common.ConfigService"];
        constructor(private $timeout: ng.ITimeoutService, private $state: ng.ui.IStateService, private configService: app.common.IConfigService) {
            configService.getCurrentServerRoot().then(serverRoot => {
                this.serverRoot = serverRoot;
                if (!this.serverRootDefined()) {
                    this.$state.go("serverRootNotDefined");
                }
            });

            var timeoutFunc = () => {
                configService.keepAlive();
                $timeout(timeoutFunc, 3000);
            };

            $timeout(timeoutFunc, 1000);
        }
    }
    angular.module("app.main", ["app.common.ConfigService", "ui.router"])
        .controller("app.main.MainController", MainController);
}
