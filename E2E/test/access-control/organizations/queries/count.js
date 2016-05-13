require(process.cwd() + '/background')();

describe("Organizations :: Queries :: Count", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 and counts all organizations in an application", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
				[{ref: "Nike"}, {ref: "Reebok"}, {ref: "Converse"}]
			)
			.then(function(result) {
				return api.organizations.count(true);
			})
			.then(function(result) {
				expect(result.body).to.equal(3);
				expect(result).to.have.status(200);
			});
	});

});
