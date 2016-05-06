require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Grant Administrate Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and provides view permission to a user", function() {
	});

});
