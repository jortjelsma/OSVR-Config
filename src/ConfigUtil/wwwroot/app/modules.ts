module modules {
    angular.module("app",
        [
            "ui.router",
            "app.main",
            "app.landingPage"
        ])
        .config(["$stateProvider", "$urlRouterProvider",
            ($stateProvider: ng.ui.IStateProvider, $urlRouterProvider: ng.ui.IUrlRouterProvider) => {
                $urlRouterProvider.otherwise("/");
            }]);

}
