require(process.cwd() + '/background')();

describe("Users :: Commands :: Update User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and changes a user's display name", function() {
	});

	xit("returns a 200 and changes a user's password", function() {
	});

	xit("returns a 200 and changes a user's email", function() {
	});

});
