(function() {
	angular
		.module("trackwane")
		.component('dashboardTrackers', {
			controller: [
				"services.applicationState",
				function($applicationState) {
					this.trackers = $applicationState.trackers;
				}
			],
			template: (
				<div style="overflow: hidden">
					<ul style="border-top: 1px solid #777; margin-top: 0">
						<li style="padding: 10px; font-weight: bold; border-bottom: 1px solid #777">Trackers</li>
					    <li ng-repeat="tracker in $ctrl.trackers" style="padding: 10px; padding-bottom: 15px; border-bottom: 1px solid #777">
					        <i class="material-icons" style="position: relative; top: 5px">directions_bus</i>
					        <span style="margin-left: 5px">{{tracker.identifier}}</span>
							<p style="margin-bottom: 0; margin-left: 33px; margin-top: 3px; font-size: 12px">
								<span ng-if="tracker.speed == 0">Stopped</span>
								<span ng-if="tracker.speed > 0">Moving, {{tracker.speed}}mph</span>
								<br/><span>{{tracker.location}}</span>
							</p>
					    </li>
					</ul>
				</div>

			)
		});
})();
