require(process.cwd() + '/background')();

describe("Users :: Queries :: Get Access Token", function() {

	it("returns a 200 when the credentials provided are valid", function() {
		var api = new API(defaults.HOST, chakram);
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function(result) {
				return api.authenticate(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password, true);
			});
	});

	it("returns a 400 when the password provided is incorrect", function() {
		var api = new API(defaults.HOST, chakram);
		return api.createApplication(defaults.APPLICATION_OWNER)
		.then(function() {
			var result = api.authenticate(defaults.APPLICATION_OWNER.email, "RANDOM-INCORRECT-PASSWORD", true);
			expect(result).to.have.status(400);
			return result;
		});
	});

	it("returns a 400 when the email provided is incorrect", function() {
		var api = new API(defaults.HOST, chakram);
		return api.createApplication(defaults.APPLICATION_OWNER)
		.then(function() {
			var result = api.authenticate("RANDOM-INCORRECT-EMAIL", defaults.APPLICATION_OWNER.password, true);
			expect(result).to.have.status(400);
			return result;
		});
	});
});
