(function() {

	angular
		.module("trackwane")
		.service('services.applicationState', [
			function() {

				this.boundaries = [];

				this.locations = [];

				for(var i = 0; i < 37; i++) {
					this.locations.push({
						name: faker.company.companyName(),
						location: faker.address.streetAddress() + ", " + faker.address.city()
					});
				}

				this.trackers = [
					{identifier: "A SENSOR IDENTIFIER"},
					{identifier: "A SENSOR A"},
					{identifier: "A SENSOR B"},
					{identifier: "A SENSOR C"},
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
					tracker.identifier = "CTA Bus " + chance.integer({min: 0, max: 80}) + " on Route " + chance.integer({min: 0, max: 80});
					tracker.speed = chance.integer({min: 0, max: 80});
					tracker.location = faker.address.streetAddress() + ", " + faker.address.city();
					tracker.coords = {
						longitude: chance.floating({min: -88, max: -87.5}),
						latitude: chance.floating({min: 41, max: 42})
					}
				});
			}
		]);
})();
