require(process.cwd() + '/background')();

describe("Organizations :: Commands :: Grant Permission", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	function checkStatusCode(permissionType) {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.organizations.grantPermission(ctx["Nike"].key, ctx["Nike"]["John"].key, permissionType, false)
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
	}

	function checkOrganizationUsers(permissionType, collection) {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.organizations.grantPermission(ctx["Nike"].key, ctx["Nike"]["John"].key, permissionType, false)
			}).then(function(result) {
				return api.organizations.findByKey(ctx["Nike"].key);
			}).then(function(result) {
				expect(result.body[collection][0].Id).to.equal(ctx["Nike"]["John"].key);
			});
	}

	function checkUserPermission(permissionType, collection) {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER,
			[{ref: "Nike"}],
			[{organization: "Nike", ref: "John"}])
			.then(function(result) {
				return api.organizations.grantPermission(ctx["Nike"].key, ctx["Nike"]["John"].key, permissionType, false)
			}).then(function(result) {
				return api.users.findByKey(ctx["Nike"]["John"].key);
			}).then(function(result) {
				expect(result.body[collection][0].Key).to.equal(ctx["Nike"].key);
				expect(result.body[collection][0].Value).to.equal("Nike");
			});
	}

	it("adds the user to the list of organization administrators once 'administrate' permission is granted", function() {
		return checkOrganizationUsers('administrate', "Administrators");
	});

	it("adds the user to the list of organization viewers once 'view' permission is granted", function() {
		return checkOrganizationUsers('view', "Viewers");
	});

	it("adds the user to the list of organization managers once 'manage' permission is granted", function() {
		return checkOrganizationUsers('manage', "Managers");
	});

	it("returns a 204 when granting 'administrate' permission by system administrator and successful", function() {
		return checkStatusCode('administrate');
	});

	it("returns a 204 when granting 'manage' permission by system administrator and successful", function() {
		return checkStatusCode('manage');
	});

	it("returns a 204 when granting 'view' permission by system administrator and successful", function() {
		return checkStatusCode('view');
	});

	it("grants administrate permission to the user", function() {
		return checkUserPermission('administrate', "Administrate");
	});

	it("grants view permission to the user", function() {
		return checkUserPermission('view', "View");
	});

	it("grants manage permission to the user", function() {
		return checkUserPermission('manage', "Manage");
	});
});
