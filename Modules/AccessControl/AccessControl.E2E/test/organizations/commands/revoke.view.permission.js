require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Revoke View Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	function revokePermission(promise, requesterEmail, requesterPassword, organizationKey, userKey, type) {
		return promise.then(function(result) {
			return api.login(requesterEmail, requesterPassword)
		}).then(function(result) {
			return api.organizations.revokePermission(organizationKey, userKey, type, true);
		});
	}

	xit("returns a 200 when the permission is successfully revoked", function() {
		var promise = fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[{organization: "Nike", ref: "John", view: ["Nike"]}, {organization: "Reebok", ref: "Nick", manage: ["Nike"]}]);
		return revokePermission(promise, ctx["Reebok"]["Nick"].email, ctx["Reebok"]["Nick"].password, ctx["Nike"].key, ctx["Nike"]["John"].key, 'view')
		.then(function(result) {
			expect(result).to.have.status(200);
		});
	});

	xit("returns a 401 when requested by a user with only view permission on the organization", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[{organization: "Nike", ref: "John", view: ["Nike"]}, {organization: "Reebok", ref: "Nick", view: ["Nike"]}])
			.then(function(result) {
				return api.login(ctx["Reebok"]["Nick"].email, ctx["Reebok"]["Nick"].password)
			}).then(function(result) {
				return api.organizations.revokePermission(ctx["Nike"].key, ctx["Nike"]["John"].key, "view", true);
			}).then(function(result) {
				expect(result).to.have.status(401);
			});
	});

	xit("returns a 200 when requested by a user with administrate permission on the organization", function() {

	});

	xit("returns a 200 when requested by a user with manage permission on the organization", function() {

	});
});
