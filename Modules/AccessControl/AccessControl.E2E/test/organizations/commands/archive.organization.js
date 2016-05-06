require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Archive Organization", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 204 when successful", function() {
	});

});
