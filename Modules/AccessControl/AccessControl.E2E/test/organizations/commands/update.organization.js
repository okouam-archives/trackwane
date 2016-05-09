require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Update Organization", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});


	xit("returns a 200 when the organization is successfully updated", function() {
	});

	xit("returns a 401 when the requester is not a system administrator or has administrate permission on the organization", function() {
	});

	xit("returns a 200 when requested by a user with administrate permission on the organization", function() {

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
