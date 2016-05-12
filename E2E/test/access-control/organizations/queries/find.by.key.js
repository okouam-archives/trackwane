require(process.cwd() + '/background')();

describe("Users :: Queries :: Find By Key", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and returns an organization when found", function() {
	});

});
