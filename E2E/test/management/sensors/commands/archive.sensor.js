require(process.cwd() + '/background')();

describe("Management :: Sensors :: Commands :: Archive Sensor", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 201 when a sensor is successfully archived", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.sensors.register("AN-ORGANIZATION-KEY", {
							hardwareId: "MY-FA345FAN-32423023-23",
							model: "PT560",
							identifier: "My personal fitness tracker"
					});
			}).then(function(result) {
				return api.sensors.archive("AN-ORGANIZATION-KEY", result.body, true);
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
		});
});
