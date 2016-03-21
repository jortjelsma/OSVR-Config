module app.main {
    interface INavbarItem {
        labelTranslateKey: string;
        state: string;
    }

    class MainController {
        navbarItems: INavbarItem[] = [
            {
                labelTranslateKey: "index.navBarItems.samples",
                state: "samples"
            },
            {
                labelTranslateKey: "index.navBarItems.tools",
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
