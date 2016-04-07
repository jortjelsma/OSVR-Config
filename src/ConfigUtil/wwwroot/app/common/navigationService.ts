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

module app.common {

    export interface INavbarItem {
        labelTranslateKey: string;
        descriptionTranslateKey: string;
        state: string;
    }
    export interface INavigationService {
        getNavbarItems(): INavbarItem[];
    }

    class NavigationService implements INavigationService {
        constructor() { }

        getNavbarItems(): INavbarItem[] {
            return [
                {
                    labelTranslateKey: "index.navBarItems.devices",
                    descriptionTranslateKey: "index.navBarItems.devicesDescription",
                    state: "devices"
                },
                {
                    labelTranslateKey: "index.navBarItems.renderManager",
                    descriptionTranslateKey: "index.navBarItems.renderManagerDescription",
                    state: "renderManager"
                },
                {
                    labelTranslateKey: "index.navBarItems.plugins",
                    descriptionTranslateKey: "index.navBarItems.pluginsDescription",
                    state: "plugins"
                },
                {
                    labelTranslateKey: "index.navBarItems.samples",
                    descriptionTranslateKey: "index.navBarItems.samplesDescription",
                    state: "samples"
                },
                {
                    labelTranslateKey: "index.navBarItems.tools",
                    descriptionTranslateKey: "index.navBarItems.toolsDescription",
                    state: "tools"
                }
            ];
        }
    }

    angular.module("app.common.NavigationService", []).service("app.common.NavigationService", NavigationService);
}
