require(process.cwd() + '/background')();

describe("Users :: Commands :: Update User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 204 and changes a user's display name", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}], [{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.update(ctx["Nike"].key, ctx["Nike"]["John"].key, {displayName: "Mike"}, true)
			}).then(function(result) {
				expect(result).to.have.status(204);
			}).then(function(result) {
				return api.users.findByKey(ctx["Nike"]["John"].key);
			}).then(function(result) {
				expect(result.body.DisplayName).to.equal("Mike");
			});
	});

	it("returns a 200 and changes a user's password", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}], [{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.update(ctx["Nike"].key, ctx["Nike"]["John"].key, {password: "a-new-password"}, true)
			}).then(function(result) {
				expect(result).to.have.status(204);
			}).then(function(result) {
				return api.authenticate(ctx["Nike"]["John"].email, "a-new-password");
			}).then(function(result) {
				expect(result).to.have.status(200);
			});
	});

	it("returns a 200 and changes a user's email", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}], [{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.users.update(ctx["Nike"].key, ctx["Nike"]["John"].key, {email: "mike@nowhere.com"}, true)
			}).then(function(result) {
				expect(result).to.have.status(204);
			}).then(function(result) {
				return api.users.findByKey(ctx["Nike"]["John"].key);
			}).then(function(result) {
				expect(result.body.Email).to.equal("mike@nowhere.com");
			});
	});

});
