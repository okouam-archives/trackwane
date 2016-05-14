require(process.cwd() + '/background')();

describe("Management :: Locations :: Commands :: Register Location", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("does not allow registration of locations with duplicate names", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.locations.register(ctx["Nike"].key, {
							name: "Tony's Pizzeria",
							coordinates:  '{ "type": "Point", "coordinates": [4.0, 30.0] }'
					});
			}).then(function(result) {
				return api.locations.register(ctx["Nike"].key, {
							name: "Tony's Pizzeria",
							coordinates: '{ "type": "Point", "coordinates": [100.0, 0.0] }'
					}, true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 201 when a new location is successfully registered", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.locations.register(ctx["Nike"].key, {
							name: "Tony's Pizzeria",
							coordinates:  '{ "type": "Point", "coordinates": [4.0, 30.0] }'
					}, true);
			}).then(function(result) {
				expect(result).to.have.status(201);
			});
		});
});
