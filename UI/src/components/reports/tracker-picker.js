(function() {
	angular
		.module("trackwane")
		.component('reportTrackerPicker', {
			controller: function() {
				this.trackers = [
					{identifier: "A SENSOR IDENTIFIER"},
					{identifier: "A SENSOR A"},
					{identifier: "A SENSOR B"},
					{identifier: "A SENSOR C"}
				];
			},
			template: (
				<div>
					<ul>
						<li>Trackers</li>
						<li ng-repeat="tracker in $ctrl.trackers">
							<i class="material-icons" style="position: relative; top: 5px">directions_bus</i>
							{{tracker.identifier}}
						</li>
					</ul>
				</div>
			)
		});
})();
