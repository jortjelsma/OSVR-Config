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
    export interface IOSVRInclude {
        RelativePath: string;
        Body: any;
    }

    export interface IOSVRConfig {
        Body: any[];
        Includes: IOSVRInclude[];
    }

    export interface IConfigService {
        getCurrent(): ng.IPromise<IOSVRConfig>;
    }

    class ConfigService implements IConfigService {
        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) { }

        getCurrent(): ng.IPromise<IOSVRConfig> {
            return this.$http.get("/api/currentconfig").then(result => { return result.data });
        }
    }

    angular.module("app.common.ConfigService", []).service("app.common.ConfigService", ConfigService);
}
