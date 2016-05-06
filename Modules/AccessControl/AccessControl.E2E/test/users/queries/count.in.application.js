require(process.cwd() + '/background')();

describe("Users :: Queries :: Count In Application", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and counts all users in an application", function() {
	});

});
