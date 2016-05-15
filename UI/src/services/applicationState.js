(function() {

	angular
		.module("trackwane")
		.service('services.applicationState', [
			function() {
				this.trackers = [
					{identifier: "A SENSOR IDENTIFIER", coords: {longitude: -73, latitude: 45}},
					{identifier: "A SENSOR A", coords: {longitude: -72, latitude: 45}},
					{identifier: "A SENSOR B", coords: {longitude: -73, latitude: 43}},
					{identifier: "A SENSOR C", coords: {longitude: -69.50, latitude: 44.22}},
					{identifier: "A SENSOR IDENTIFIER"},
					{identifier: "ANOTHER SENSOR ID", location: "Pulham Road, Battersea"},
					{identifier: "CTA Bus 2034 on Route 6", location: "King's Road, Chelsea"},
					{identifier: "ANOTHER SENSOR ID", location: "Sydenham High Street, Sydenham"},
					{identifier: "CTA Bus 12 on Route 6"},
					{identifier: "CTA Bus 93 on Route 12"},
					{identifier: "CTA Bus 86 on Route 1"},
					{identifier: "CTA Bus 123 on Route 5"},
					{identifier: "CTA Bus 93 on Route 12"},
					{identifier: "CTA Bus 86 on Route 1"},
					{identifier: "CTA Bus 123 on Route 5"},
					{identifier: "CTA Bus 93 on Route 12"},
					{identifier: "CTA Bus 86 on Route 1"},
					{identifier: "CTA Bus 123 on Route 5"}
				];

				_.each(this.trackers, function(tracker) {
					tracker.icon = "http://www.nmr.ch.tum.de/images/stories/car.gif";
					tracker.speed = chance.integer({min: 0, max: 80});
					tracker.coords = {
						longitude: chance.floating({min: -73, max: -69});,
						latitude: chance.floating({min: 43, max: 45});
					}
				});

				console.log(this.trackers);
			}
		]);
})();
