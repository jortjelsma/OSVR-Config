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
module app.renderManager {

    interface IRenderManagerForm extends ng.IFormController {

    }

    interface IRenderManagerControllerScope extends ng.IScope {
        vm: RenderManagerController;
        renderManagerForm: IRenderManagerForm;
    }

    class RenderManagerController {
        configRoot: app.common.IOSVRConfig;
        config: any;
        showConfigJson = false;

        static $inject = ["$scope", "app.common.ConfigService"];
        constructor(private $scope: IRenderManagerControllerScope, private configService: app.common.IConfigService) {
            configService.getCurrent().then(config => {
                var rmConfig: any = null;
                var i = 0;
                this.configRoot = config;

                // if the current render manager config is a referenced file,
                // replace the renderManagerConfig with the body of the referenced file
                // (we'll only save it if the user makes changes and hits save)
                if (angular.isString(this.configRoot.Body.renderManagerConfig) &&
                    angular.isDefined(this.configRoot.Includes) &&
                    this.configRoot.Includes !== null) {

                    for (i = 0; i < this.configRoot.Includes.length; i++) {
                        var inc = this.configRoot.Includes[i];
                        if (inc.RelativePath === this.configRoot.Body.renderManagerConfig) {
                            this.configRoot.Body.renderManagerConfig = inc.Body;
                            this.configRoot.Includes.splice(i, 1);
                            this.config = inc.Body.renderManagerConfig;
                            break;
                        }
                    }
                } else if (!angular.isDefined(this.configRoot.Body.renderManagerConfig) ||
                    this.configRoot.Body.renderManagerConfig === null) {
                    this.configRoot.Body.renderManagerConfig = {
                        renderManagerConfig: {}
                    };

                    this.config = this.configRoot.Body.renderManagerConfig.renderManagerConfig;
                } else {
                    this.config = this.configRoot.Body.renderManagerConfig.renderManagerConfig;
                }
            });
        }

        showHideConfigJsonButtonTextKey() {
            return this.showConfigJson ? "renderManager.hideConfigJson" : "renderManager.showConfigJson";
        }

        toggleShowConfigJson() {
            this.showConfigJson = !this.showConfigJson;
        }

        save() {
            this.configService.setCurrent(this.configRoot).then(
                _ => {
                    this.$scope.renderManagerForm.$setPristine();
                });
        }
    }

    angular.module("app.renderManager", ["app.common.ConfigService", "ui.router"])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("renderManager", {
                url: "/renderManager",
                templateUrl: "app/renderManager/renderManager.html",
                controller: "app.renderManager.RenderManagerController as vm"
            });
        }])
        .controller("app.renderManager.RenderManagerController", RenderManagerController);
}
