require(process.cwd() + '/background')();

describe("Users :: Organizations :: Count", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and counts all organizations in an application", function() {
	});

});
