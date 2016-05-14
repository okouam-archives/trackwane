require(process.cwd() + '/background')();

describe("Management :: Sensors :: Commands :: Register Sensor", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("does not allow registration of duplicate sensors within a single organization with the same hardware ID and returns a 400", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.sensors.register("AN-ORGANIZATION-KEY", {
							hardwareId: "MY-FA345FAN-32423023-23",
							model: "PT560",
							identifier: "My personal fitness tracker"
					});
			}).then(function(result) {
				return api.sensors.register("AN-ORGANIZATION-KEY", {
							hardwareId: "MY-FA345FAN-32423023-23",
							model: "AH560",
							identifier: "My other personal fitness tracker"
					}, true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});


	it("returns a 201 when a new sensor is successfully registered", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.sensors.register("AN-ORGANIZATION-KEY", {
							hardwareId: "MY-FA345FAN-32423023-23",
							model: "PT560",
							identifier: "My personal fitness tracker"
					}, true);
			}).then(function(result) {
				expect(result).to.have.status(201);
			});
		});
});
