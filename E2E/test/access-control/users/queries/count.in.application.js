require(process.cwd() + '/background')();

describe("Users :: Queries :: Count In Application", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	function setupBackground() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[
				{organization: "Nike", ref: "John", manage: ["Nike"]},
				{organization: "Reebok", ref: "Alan", manage: ["Nike"]},
				{organization: "Reebok", ref: "Tony", view: ["Nike"]},
				{organization: "Nike", ref: "Jim", administrate: ["Reebok"]},
				{organization: "Nike", ref: "Arthur", administrate: ["Reebok"]}
			]);
	}

	it("returns a 200 and counts all users in an application", function() {
		return setupBackground()
			.then(function(result) {
				return api.users.countInApplication();
			})
			.then(function(result) {
				expect(result.body).to.equal(6);
			})
	});

});
