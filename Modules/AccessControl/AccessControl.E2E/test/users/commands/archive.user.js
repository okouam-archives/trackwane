require(process.cwd() + '/background')();

describe("Users :: Commands :: Archive User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 204 when the user is active", function() {
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

	xit("returns a 204 when the user is already archived", function() {
		return api
			.createApplication(ctx, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.authenticate(ctx, defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password)
			}).then(function() {
				return api.users.register(ctx, "sdfsdf", "sdfsdf", "sdfsdfdsf")
			}).then(function(result) {
				ctx.userKey = result.body
				return api.users.archive(ctx.userKey);
			}).then(function() {
				return api.users.archive(ctx.userKey);
			});
	});
});
