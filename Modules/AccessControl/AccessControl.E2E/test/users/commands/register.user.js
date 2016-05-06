require(process.cwd() + '/background')();

describe("Users :: Commands :: Register User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 when a new user is created", function() {
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function() {
				return api.login(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password);
			}).then(function(result) {
				return api.organizations.register("random-organization-name");
			}).then(function(result) {
				ctx.organizationKey = result.body;
				return api.users.register(ctx.organizationKey, "john smith", "john@nowhere.com", "random-password");
			}).then(function(result) {
				return api.users.archive(ctx.organizationKey, result.body, false)
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
	});

	xit("returns a 400 if a blank password is provided", function() {
	});

	xit("returns a 400 if a blank email is provided", function() {
	});

	xit("returns a 400 if a blank display name is provided", function() {
	});

	xit("returns a 400 if no passowrd is provided", function() {
	});

	xit("returns a 400 if no email is provided", function() {
	});

	xit("returns a 400 if no display name is provided", function() {
	});

	xit("returns a 400 if the organization key provided does not correspond to a known organization", function() {
	});
});
