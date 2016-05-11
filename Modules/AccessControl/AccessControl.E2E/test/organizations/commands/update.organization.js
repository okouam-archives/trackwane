require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Update Organization", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});


	xit("returns a 200 when the organization is successfully updated", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function(result) {
				return api.organizations.update(ctx["Nike"].key, {name: "A-NEW-NAME"}, true);
			}).then(function(result) {
				expect(result).to.have.status(300);
			});
	});

	xit("returns a 400 when the user is not a system manager", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John", view: ["Nike"]}])
			.then(function(result) {
				return api.login(ctx["Nike"]["John"].email, ctx["Nike"]["John"].password)
			}).then(function(result) {
				return api.organizations.update(ctx["Nike"].key, {name: "A-NEW-NAME"}, true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});
});
