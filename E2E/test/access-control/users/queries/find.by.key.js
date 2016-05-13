require(process.cwd() + '/background')();

describe("Users :: Queries : Find By Key", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 and finds an existing user by key", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}], [{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.findByKey(ctx["Nike"]["John"].key);
			}).then(function(result) {
				expect(result).to.have.status(200);
				expect(result.body.DisplayName).to.equal("John");
			});
	});

	it("returns a 404 if the user does not exist in the application", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}], [{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.findByKey("random-key", true);
			}).then(function(result) {
				expect(result).to.have.status(404);
			});
	});
});
