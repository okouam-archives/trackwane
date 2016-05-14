require(process.cwd() + '/background')();

describe("Data :: Commands :: Save Reading", function() {

	var api, ctx;

	beforeEach(function () {
		api = new SensorAPI(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200", function() {
		var applicationKey = hashids.encode(new Date().getTime());
		return api.save(applicationKey, "AN-ORGANIZATION-KEY", {
					speed: 45,
					distance: 39334,
					batteryLevel: 29,
					petrol: 293,
					timestamp: "2018-09-12T02:22",
					hardwareId: "MY-FAKE-BOFAN-TRACKER",
					longitude: 5.34,
					latitude: 2.34
			}, true)
			.then(function(result) {
				expect(result).to.have.status(204);
			});
		});
});
