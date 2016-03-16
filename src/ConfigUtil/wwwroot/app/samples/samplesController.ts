module app.samples {
    interface ISampleConfig {
        FileName: string;
        Description: string;
        showDetail?: boolean;
        detailJson?: any;
    }

    class SamplesController {
        sampleConfigs: ISampleConfig[] = [];
        static $inject = ["$http", "$log"];
        constructor(private $http: ng.IHttpService, private $log: ng.ILogService) {
            $http.get("/api/sampleConfigs").then(result => {
                this.sampleConfigs = <ISampleConfig[]>result.data;
            });
        }

        clickSampleConfig(sampleConfig: ISampleConfig) {
            this.$http.post("/api/usesampleconfig", {}, {
                params: {
                    sampleFileName: sampleConfig.FileName
                }
            }).then(
                success => {
                    this.$log.info("Successfully used sample config.");
                },
                failureReason => {
                    this.$log.error("Could not use sample config.");
                });
        }

        viewSampleConfig(sampleConfig: ISampleConfig) {
            var i = 0;
            for (i = 0; i < this.sampleConfigs.length; i++) {
                this.sampleConfigs[i].showDetail = false;
            }
            this.$http.get("/api/sampleConfigs/" + sampleConfig.FileName).then(
                result => {
                    sampleConfig.showDetail = true;
                    sampleConfig.detailJson = result.data;
                },
                errorReason => {
                    this.$log.error("Could not get the sample config file " + sampleConfig.FileName);
                });
        }
    }

    angular.module("app.samples", [])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("samples", {
                url: "/samples",
                templateUrl: "app/samples/samples.html",
                controller: "app.samples.SamplesController as vm"
            });
        }])
        .controller("app.samples.SamplesController", SamplesController);
}
