var app = angular.module("trackwane", [	"ui.router", 'uiGmapgoogle-maps', 'ngMaterial']);

app.config(function ($stateProvider, $urlRouterProvider, $mdThemingProvider) {
	$mdThemingProvider
		.theme('default')
		.primaryPalette('blue')
	    .accentPalette('blue')
		.dark();

    $urlRouterProvider.otherwise("/dashboard");
    $stateProvider
        //.state('login', {url: "/", templateUrl: "pages/login.html"})
        //.state('logout', {url: "/logout", templateUrl: "pages/logout.html"})
		.state('reports', {
			url: "/reports",
			template: '<reports-page class="page" />',
			onEnter: [
				'services.reports',
				'services.trackers',
				function($reports, $trackers) {
					// fetch reports and populate applicationState
					// fetch trackers and populate applicationState
				}
			]
		})
		.state('timelines', {url: "/timelines", template: '<timelines-page class="page" />'})
		.state('notifications', {url: "/notifications", template: '<notifications-page class="page" />'})
		.state('access-control', {url: "/access-control", template: '<access-control-page class="page" />'})
        .state('dashboard', {
			onEnter: [
				'services.locations',
				'services.boundaries',
				'services.trackers',
				function($locations, $boundaries, $trackers) {
					// fetch trackers and populate applicationState
					// fetch locations and populate applicationState
					// fetch boundaries and populate applicationState
				}
			],
			url: "/dashboard",
			template: '<dashboard-page class="page" />'
		});
});
