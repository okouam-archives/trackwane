require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Revoke View Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});


	xit("returns a 200 when the user has view permission and it is successfully revoked", function() {
	});

	xit("returns a 401 when the requester is not in the same organization as the user", function() {
	});

	xit("returns a 401 when requested by a user with view permission on the organization", function() {

	});

	xit("returns a 200 when requested by a user with administrate permission on the organization", function() {

	});

	xit("returns a 200 when requested by a user with manage permission on the organization", function() {

	});
});
