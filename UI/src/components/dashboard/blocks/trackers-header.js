(function() {

	class DashboardTrackersHeader {

		registerNew() {
			console.log("Registering new tracker");
		}
	}

	angular
		.module("trackwane")
		.component('dashboardTrackersHeader', {
			controller: DashboardTrackersHeader,
			template: (
				<li class="header">
					Trackers
					<a href="#" ng-click="$ctrl.registerNew()">
						<i style="float: right" class="material-icons">add_box</i>
					</a>
				</li>
			)
		});
})();
