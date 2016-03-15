module app.tools {
    class ToolsController {
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

        static $inject = ["$http"];
        constructor(private $http: ng.IHttpService) { }
    }

    angular.module("app.tools", [])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("tools", {
                url: "/tools",
                templateUrl: "app/tools/tools.html",
                controller: "app.tools.ToolsController as vm"
            });
        }])
        .controller("app.tools.ToolsController", ToolsController);
}