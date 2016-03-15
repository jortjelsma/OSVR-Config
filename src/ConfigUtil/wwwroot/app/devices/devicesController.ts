module app.devices {
    class DevicesController {
        test = "this is a test in DevicesController";
    }

    angular.module("app.devices", [])
        .config(["$stateProvider", ($stateProvider: angular.ui.IStateProvider) => {
            $stateProvider.state("devices", {
                url: "/devices",
                templateUrl: "app/devices/devices.html",
                controller: "app.devices.DevicesController as vm"
            });
        }])
        .controller("app.devices.DevicesController", DevicesController);
}
