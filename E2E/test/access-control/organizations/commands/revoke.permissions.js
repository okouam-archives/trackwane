require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Revoke Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	function setupBackground() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[
				{organization: "Nike", ref: "John", manage: ["Nike"]},
				{organization: "Nike", ref: "Alan", manage: ["Nike"]},
				{organization: "Nike", ref: "Tony", view: ["Nike"]},
				{organization: "Nike", ref: "Jim", administrate: ["Nike"]},
				{organization: "Nike", ref: "Arthur", administrate: ["Nike"]}
			]);
	}

	function revoke(requester, organization, user, permissionType) {
		return setupBackground()
			.then(function(result) {
				if (!requester.email) requester = ctx[organization][user];
				return api.login(requester.email, requester.password);
			}).then(function(result) {
				return api.organizations.revokePermission(ctx[organization].key, ctx[organization][user].key, permissionType, true);
			});
	}

	/* Administrators */

	it("returns a 204 when the user has an 'administrate' permission successfully revoked by a system manager", function() {
		return revoke(defaults.APPLICATION_OWNER, "Nike", "Arthur", "administrate")
			.then(function(result) {expect(result).to.have.status(204);});
	});

	it("returns a 204 when the user has an 'view' permission successfully revoked by a system manager", function() {
		return revoke(defaults.APPLICATION_OWNER, "Nike", "Tony", "view")
			.then(function(result) {expect(result).to.have.status(204);});
	});

	it("returns a 204 when the user has an 'manage' permission successfully revoked by a system manager", function() {
		return revoke(defaults.APPLICATION_OWNER, "Nike", "Alan", "manage")
			.then(function(result) {expect(result).to.have.status(204);});
	});

	/* Organization Administrators */

	xit("returns a 204 when the user has a 'manage' permission successfully revoked by an organization administrator", function() {
		return revoke("Arthur", "Nike", "John", "manage")
			.then(function(result) {expect(result).to.have.status(200); chakram.stopDebug()});
	});

	xit("returns a 204 when the user has a 'view' permission successfully revoked by an organization administrator", function() {
		return revoke("Arthur", "Nike", "Tony", "view")
			.then(function(result) {expect(result).to.have.status(200);});
	});

	xit("returns a 404 when an organization administrator attempts to revoke an 'administrate' permission", function() {
		return revoke("Arthur", "Nike", "Jim", "administrate")
			.then(function(result) {expect(result).to.have.status(404);});
	});

	/* Others */

	xit("returns a 404 when an organization manager attempts to revoke a permission", function() {
		return revoke(ctx["Nike"]["Alan"], ctx["Nike"].key, ctx["Nike"]["John"].key, "manage")
			.then(function(result) {expect(result).to.have.status(404);});
	});

	xit("returns a 404 when an organization viewer attempts to revoke a permission", function() {
		return revoke(ctx["Nike"]["Tony"], ctx["Nike"].key, ctx["Nike"]["John"].key, "manage")
			.then(function(result) {expect(result).to.have.status(404);});
	});

});
