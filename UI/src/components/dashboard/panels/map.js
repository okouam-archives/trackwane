(function() {
	angular
		.module("trackwane")
		.component("dashboardMap", {
			controller: [
				"services.applicationState",
				"services.dashboard.mapService",
				function($applicationState, $mapService) {
					this.map = {
						center: {
							latitude: 41.87,
							longitude: -87.62
						},
					 	zoom: 8
					};
					this.trackers = $applicationState.trackers;
					this.showTracker = function(marker, event, tracker) {
						console.log(tracker);
					};
				}
			],
			template:
				<ui-gmap-google-map center='$ctrl.map.center' zoom='$ctrl.map.zoom'>
					<ui-gmap-markers models="$ctrl.trackers" coords="'coords'" icon="'icon'" idkey="'identifier'" click="$ctrl.showTracker"></ui-gmap-markers>
				</ui-gmap-google-map>
		});
})();
