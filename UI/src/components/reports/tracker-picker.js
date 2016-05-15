(function() {

	class ReportsTrackerPicker {

		constructor($applicationState) {
			this.trackers = $applicationState.trackers;
		}

	}

	ReportsTrackerPicker.$inject = ["services.applicationState", "services.dashboard.mapService"];


	angular
		.module("trackwane")
		.component('reportTrackerPicker', {
			controller: ReportsTrackerPicker,
			template: (
				<div style="width: 100%; overflow-y: scroll; border-left: 1px solid #666; border-top: 1px solid #666">
					<ul>
						<li class="header" style="padding-top: 0">Trackers</li>
						<li ng-repeat="tracker in $ctrl.trackers" style="padding: 10px">
							<i class="material-icons" style="position: relative; top: 5px">directions_bus</i>
							{{tracker.identifier}}
							<md-checkbox style="float: right; position: relative; top: 9px" />
						</li>
					</ul>
				</div>
			)
		});
})();
