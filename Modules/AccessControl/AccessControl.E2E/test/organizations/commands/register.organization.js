require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Register Organization", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 204 when successful", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.organizations.register("Nike", true);
			}).then(function(result) {
				expect(result).to.have.status(201);
			});
	});

	it("returns a 401 when user is not a system administrator", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
				[{ref: "Nike"}],
				[{organization: "Nike", ref: "John", email: "toby@nowhere.com", password: "changeme"}])
			.then(function(result) {
				return api.login(ctx["Nike"]["John"].email, ctx["Nike"]["John"].password)
			}).then(function(result) {
				return api.organizations.register("Reebok", true);
			}).then(function(result) {
				expect(result).to.have.status(401);
			});
	});

	it("returns a 400 when the name provided is already in use", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER)
			.then(function() {
				return api.organizations.register("Nike", true);
			}).then(function() {
				return api.organizations.register("Nike", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});
});
