require(process.cwd() + '/background')();

describe("Users :: Commands :: Archive User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 204 when the user is active", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.archive(ctx["Nike"].key, ctx["Nike"]["John"].key, false)
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
	});

	it("returns a 204 when the user is already archived", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function() {
				return api.users.archive(ctx["Nike"].key, ctx["Nike"]["John"].key)
			}).then(function() {
				return api.users.archive(ctx["Nike"].key, ctx["Nike"]["John"].key, true);
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
	});
});
