import LoginPage from './pages/login/module'
import LogoutPage from './pages/logout/module'
import DashboardPage from './pages/dashboard/module'

var app = angular.module("trackwane", [
	"ng.reflux",
	"ui.router",
	LoginPage,
	LogoutPage,
	DashboardPage
]);

app.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/dashboard");

	var modules = "src/modules/pages/";
	var application = "src/application/pages/";

    $stateProvider
        .state('login', {url: "/", templateUrl: application + "login/main.html"})
        .state('logout', {url: "/logout", templateUrl: application + "logout/main.html"})
        .state('account', {
            url: "/account",
            templateUrl: application + "account/main.html"
        })
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: application + "dashboard/main.html"
        })
        .state('management.boundaries', {
            url: "/management/boundaries",
            templateUrl: modules + "partials/state2.html"
        })
        .state('management.drivers', {
            url: "/management/drivers",
            templateUrl: modules + "partials/state2.html"
        })
        .state('management.trackers', {
            url: "/management/trackers",
            templateUrl: modules + "partials/state2.html"
        })
        .state('access-control.users', {
            url: "/access-control/users",
            templateUrl: modules + "partials/state2.html"
        })
        .state('access-control.organizations', {
            url: "/access-control/organizations",
            templateUrl: modules + "partials/state2.html"
        })
        .state('data.realtime', {
            url: "/data/realtime",
            templateUrl: modules + "partials/state2.html"
        })
        .state('data.historical', {
            url: "/data/historical",
            templateUrl: "partials/state2.list.html",
        });
});

export default app
