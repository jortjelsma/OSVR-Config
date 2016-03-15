module app.main {
    interface INavbarItem {
        label: string;
        state: string;
    }

    class MainController {
        navbarItems: INavbarItem[] = [
            {
                label: "Devices",
                state: "devices"
            },
            {
                label: "Tools",
                state: "tools"
            }
        ];

        isActive(navBarItem: INavbarItem): boolean {
            return this.$state.current.name == navBarItem.state;
        }

        static $inject = ["$state"];
        constructor(private $state: ng.ui.IStateService) { }
    }
    angular.module("app.main", [])
        .controller("app.main.MainController", MainController);
}
