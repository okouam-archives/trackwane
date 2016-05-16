(function() {

	angular
		.module("trackwane")
		.service('services.applicationState', [
			function() {

				function generateFakes() {
					var items = [];
					var max = chance.integer({min: 0, max: 20});
					for (var j = 0; j < max; j++) {
						items.push({
							key: "asdasd",
							name: faker.company.companyName()
						});
					}
					return items;
				}

				this.boundaries = [];

				this.rules = [];

				this.users =  _.times(chance.integer({min: 2, max: 30}), function(index) {
					return {
						displayName: faker.name.findName(),
						email: faker.internet.email(),
						administrate: generateFakes(),
						manage: generateFakes(),
						view: generateFakes(),
						photo: faker.image.avatar()
					};
				});

				this.organizations = _.times(chance.integer({min: 2, max: 30}), function(index) {
					return {
						name: faker.company.companyName(),
						photo: faker.image.abstract(),
						userCount: chance.integer({min: 2, max: 20})
					};
				});

				this.locations = _.times(chance.integer({min: 2, max: 80}), function(index) {
					return {
						name: faker.company.companyName(),
						location: faker.address.streetAddress() + ", " + faker.address.city()
					};
				});

				this.trackers = _.times(chance.integer({min: 10, max: 60}), function(index) {
					return {
						icon: "http://www.nmr.ch.tum.de/images/stories/car.gif",
						identifier: "CTA Bus " + chance.integer({min: 0, max: 80}) + " on Route " + chance.integer({min: 0, max: 80}),
						speed: chance.integer({min: 0, max: 80}),
						location: faker.address.streetAddress() + ", " + faker.address.city(),
						coords: {
							longitude: chance.floating({min: -88, max: -87.5}),
							latitude: chance.floating({min: 41, max: 42})
						}
					};
				});
			}
		]);
})();
