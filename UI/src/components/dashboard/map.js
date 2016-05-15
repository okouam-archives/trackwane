(function() {
	angular
		.module("trackwane")
		.component("dashboardMap", {
			controller: [
				"services.applicationState",
				function($applicationState) {
					this.map = { center: { latitude: 45, longitude: -73 }, zoom: 8 };
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
