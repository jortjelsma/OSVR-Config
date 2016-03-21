module modules {
    angular.module("app",
        [
            "ui.router",
            "app.main",
            "app.landingPage",
            "app.tools",
            "app.samples",
            "pascalprecht.translate"
        ])
        .config(["$stateProvider", "$urlRouterProvider", "$translateProvider",
            (
                $stateProvider: ng.ui.IStateProvider,
                $urlRouterProvider: ng.ui.IUrlRouterProvider,
                $translateProvider: ng.translate.ITranslateProvider) => {

                $urlRouterProvider.otherwise("/");
                $translateProvider.useStaticFilesLoader({
                    prefix: "localization/locale-",
                    suffix: ".json"
                });
                $translateProvider.useSanitizeValueStrategy('escape');
                $translateProvider.preferredLanguage("en");
            }]);

}
