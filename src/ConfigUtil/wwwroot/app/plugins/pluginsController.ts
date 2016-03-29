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
module app.plugins {

    interface IPluginsForm extends ng.IFormController {

    }

    interface IPluginsControllerScope extends ng.IScope {
        vm: PluginsController;
        pluginsForm: IPluginsForm;
    }

    class PluginsController {
        currentConfig: common.IOSVRConfig;
        config: any = {};
        nextPlugin = "";
        plugins: common.IOSVRPlugin[] = [];
        static $inject = ["$http", "$log", "$scope", "app.common.ConfigService"];
        constructor(
            private $http: ng.IHttpService,
            private $log: ng.ILogService,
            private $scope: IPluginsControllerScope,
            private configService: app.common.IConfigService) {
            configService.getCurrent().then(currentConfig => {
                this.currentConfig = currentConfig;
                this.config = currentConfig.Body;
                if (!angular.isDefined(this.config.plugins)) {
                    this.config.plugins = [];
                }
                return configService.getAvailableManualLoadPlugins().then(plugins => {
                    for (var plugin of this.config.plugins) {
                        this.plugins.push({
                            Name: plugin,
                            enabled: true
                        });
                    }
                    for (var availablePlugin of plugins) {
                        // @todo replace with one-line underscore call
                        var alreadyEnabled = false;
                        for (var enabledPlugin of this.plugins) {
                            if (enabledPlugin.Name === availablePlugin.Name) {
                                alreadyEnabled = true;
                                break;
                            }
                        }
                        if (!alreadyEnabled) {
                            this.plugins.push({
                                Name: availablePlugin.Name,
                                enabled: false
                            });
                        }
                    }
                });
            });
        }

        addPluginEnabled() {
            return angular.isString(this.nextPlugin)
        }

        addPlugin(plugin: string) {
            this.plugins.push({
                Name: plugin,
                enabled: true
            });
        }
        
        save() {
            this.config.plugins = [];
            for (var plugin of this.plugins) {
                if (plugin.enabled) {
                    this.config.plugins.push(plugin.Name);
                }
            }
            this.configService.setCurrent(this.currentConfig).then(
                success => {
                    this.$scope.pluginsForm.$setPristine();
                });
        }
    }

    angular.module("app.plugins", ["app.common.ConfigService", "ui.router"])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("plugins", {
                url: "/plugins",
                templateUrl: "app/plugins/plugins.html",
                controller: "app.plugins.PluginsController as vm"
            });
        }])
        .controller("app.plugins.PluginsController", PluginsController);
}
