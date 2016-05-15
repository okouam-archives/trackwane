(function() {

	class DashboardTrackersRows {

		constructor($applicationState, $mapService) {
			this.trackers = $applicationState.trackers;
			this.$mapService = $mapService;
		}

		moveTo(tracker) {
			this.$mapService.moveTo(tracker.coords);
		}
	}

	DashboardTrackersRows.$inject = ["services.applicationState", "services.dashboard.mapService"];

	angular
		.module("trackwane")
		.component('dashboardTrackersRows', {
			controller: DashboardTrackersRows,
			template: (
			    <li style="cursor: pointer" ng-click="$ctrl.moveTo(tracker)" class="entry" ng-repeat="tracker in $ctrl.trackers">
			        <i class="material-icons">directions_bus</i>
			        <span class="identifier">{{tracker.identifier}}</span>
					<p class="status">
						<span ng-if="tracker.speed == 0">Stopped</span>
						<span ng-if="tracker.speed > 0">Moving @ {{tracker.speed}}mph</span>
						<br/><span>{{tracker.location}}</span>
					</p>
			    </li>
			)
		});
})();
