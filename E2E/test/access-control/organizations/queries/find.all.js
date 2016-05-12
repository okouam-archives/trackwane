require(process.cwd() + '/background')();

describe("Organizations :: Queries :: Find All", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and returns all organizations in an application", function() {
	});

});
