module app.main {
    class MainController {
        test = "This is a test value from MainController";
    }
    angular.module("app.main", [])
        .controller("app.main.MainController", MainController);
}
