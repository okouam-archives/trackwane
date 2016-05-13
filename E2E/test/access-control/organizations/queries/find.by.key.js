require(process.cwd() + '/background')();

describe("Organizations :: Queries :: Find By Key", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 and returns an organization when found", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
				[{ref: "Nike"}, {ref: "Reebok"}]
			)
			.then(function(result) {
				return api.organizations.findByKey(ctx["Nike"].key, true);
			})
			.then(function(result) {
				var organization = result.body;
				expect(organization.Key).to.equal(ctx["Nike"].key);
				expect(organization.Name).to.equal("Nike");
				expect(organization.IsArchived).to.equal(false);
				expect(result).to.have.status(200);
			});
	});

});
