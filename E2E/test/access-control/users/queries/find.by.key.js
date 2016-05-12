require(process.cwd() + '/background')();

describe("Users :: Queries : Find By Key", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 and finds an existing user by key", function() {
	});

	xit("returns a 400 if the user does not exist in the application", function() {
	});
});
