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
        showConfigJson = true;
        showAdvanced = false;

        static $inject = ["$scope", "$http", "app.common.ConfigService"];
        constructor(
            private $scope: IRenderManagerControllerScope,
            private $http: ng.IHttpService,
            private configService: app.common.IConfigService) {
            configService.getCurrent().then(config => {
                var rmConfig: any = null;
                var i = 0;
                this.configRoot = config;

                // if the current render manager config is a referenced file,
                // replace the renderManagerConfig with the body of the referenced file
                // (we'll only save it if the user makes changes and hits save)
                if (angular.isString(this.configRoot.body.renderManagerConfig) &&
                    angular.isDefined(this.configRoot.includes) &&
                    this.configRoot.includes !== null) {

                    for (i = 0; i < this.configRoot.includes.length; i++) {
                        var inc = this.configRoot.includes[i];
                        if (inc.relativePath === this.configRoot.body.renderManagerConfig) {
                            this.configRoot.body.renderManagerConfig = inc.body;
                            this.configRoot.includes.splice(i, 1);
                            this.config = inc.body.renderManagerConfig;
                            break;
                        }
                    }
                } else if (!angular.isDefined(this.configRoot.body.renderManagerConfig) ||
                    this.configRoot.body.renderManagerConfig === null) {
                    this.configRoot.body.renderManagerConfig = {
                        renderManagerConfig: {}
                    };

                    this.config = this.configRoot.body.renderManagerConfig.renderManagerConfig;
                } else {
                    this.config = this.configRoot.body.renderManagerConfig.renderManagerConfig;
                }
                this.applyTemporaryOverrides();
            });
        }

        /**
         * Apply temporary overrides for settings that are not yet shown in the UI.
         */
        applyTemporaryOverrides() {
            // ATW is not officially released and causes a crash in Unreal when enabled.
            // disable it until this can be fixed.
            this.config.timeWarp.asynchronous = false;
        }

        enableDirectMode() {
            this.$http.post("/api/enableDirectMode", {}).then(
                response => {
                    console.log("/api/enableDirectMode call succeeded.");
                }, reason => {
                    console.log("/api/enableDirectMode call failed.");
                });
        }

        disableDirectMode() {
            this.$http.post("/api/disableDirectMode", {}).then(
                response => {
                    console.log("/api/disableDirectMode call succeeded.");
                }, reason => {
                    console.log("/api/disableDirectMode call failed.");
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

        inputColumnClassObject() {
            return {
                'col-md-6': this.showConfigJson,
                'col-md-4': !this.showConfigJson
            };
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
