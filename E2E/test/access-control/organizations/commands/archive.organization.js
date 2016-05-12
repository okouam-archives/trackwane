require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Archive Organization", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});


	it("returns a 400 when the organization does not exist", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function(result) {
				return api.organizations.archive("RANDOM-KEY", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 200 when successful", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.organizations.register("Nike", true);
			}).then(function(result) {
				return api.organizations.archive(result.body, true);
			}).then(function(result) {
				expect(result).to.have.status(200);
			});
	});

	it("returns a 400 when the user is not a system manager", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.login(ctx["Nike"]["John"].email, ctx["Nike"]["John"].password)
			}).then(function(result) {
				return api.organizations.archive(ctx["Nike"].key, true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});
});
