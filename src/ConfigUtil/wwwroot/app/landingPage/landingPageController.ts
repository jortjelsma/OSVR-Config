module app.landingPage {
    class LandingPageController {
        currentConfig: any = {};
        static $inject = ["$http", "$log"];
        constructor(private $http: ng.IHttpService, private $log: ng.ILogService) {
            $http.get("/api/currentconfig").then(
                success => {
                    this.currentConfig = success.data;
                },
                failure => {
                    $log.error("Could not get the current config.");
                });
        }
    }

    angular.module("app.landingPage", [])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("landingPage", {
                url: "/",
                templateUrl: "app/landingPage/landingPage.html",
                controller: "app.landingPage.LandingPageController as vm"
            });
        }])
        .controller("app.landingPage.LandingPageController", LandingPageController);
}
