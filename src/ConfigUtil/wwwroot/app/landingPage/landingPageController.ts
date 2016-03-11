module app.landingPage {
    class LandingPageController {
        test = "this is a test in LandingPageController";
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
