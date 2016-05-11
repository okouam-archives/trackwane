require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Revoke Manage Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	xit("returns a 200 when the user has manage permission and it is successfully revoked", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John", manage: ["Nike"]}])
			.then(function(result) {
				return api.organizations.revokePermission(ctx["Nike"].key, ctx["Nike"]["John"].key, "manage", true);
			}).then(function(result) {
				expect(result).to.have.status(200);
			});
	});

	xit("returns a 401 when requested by a user with view permission on the organization", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[{organization: "Nike", ref: "John", manage: ["Nike"]}, {organization: "Reebok", ref: "Nick", view: ["Nike"]}])
			.then(function(result) {
				return api.login(ctx["Reebok"]["Nick"].email, ctx["Reebok"]["Nick"].password)
			}).then(function(result) {
				return api.organizations.revokePermission(ctx["Nike"].key, ctx["Nike"]["John"].key, "manage", true);
			}).then(function(result) {
				expect(result).to.have.status(402);
			});
	});

	xit("returns a 200 when requested by a user with administrate permission on the organization", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[{organization: "Nike", ref: "John", manage: ["Nike"]}, {organization: "Reebok", ref: "Nick", administrate: ["Nike"]}])
			.then(function(result) {
				return api.login(ctx["Reebok"]["Nick"].email, ctx["Reebok"]["Nick"].password)
			}).then(function(result) {
				return api.organizations.revokePermission(ctx["Nike"].key, ctx["Nike"]["John"].key, "manage", true);
			}).then(function(result) {
				expect(result).to.have.status(200);
			});
	});

	xit("returns a 200 when requested by a user with manage permission on the organization", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}, {ref: "Reebok"}],
			[{organization: "Nike", ref: "John", manage: ["Nike"]}, {organization: "Reebok", ref: "Nick", manage: ["Nike"]}])
			.then(function(result) {
				return api.login(ctx["Reebok"]["Nick"].email, ctx["Reebok"]["Nick"].password)
			}).then(function(result) {
				return api.organizations.revokePermission(ctx["Nike"].key, ctx["Nike"]["John"].key, "manage", true);
			}).then(function(result) {
				expect(result).to.have.status(200);
			});
	});
});
