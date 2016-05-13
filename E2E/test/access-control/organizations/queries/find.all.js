require(process.cwd() + '/background')();

describe("Organizations :: Queries :: Find All", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 and returns all organizations in an application", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
				[{ref: "Nike"}, {ref: "Reebok"}, {ref: "Converse"}]
			)
			.then(function(result) {
				return api.organizations.findAll(true);
			})
			.then(function(result) {
				expect(result).to.have.status(200);
			});
	});

});
