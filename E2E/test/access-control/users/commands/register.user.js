require(process.cwd() + '/background')();

describe("Users :: Commands :: Register User", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 201 when a new user is created", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function(result) {
				return api.users.register(ctx["Nike"].key, "john smith", "john@nowhere.com", "random-password", true)
			}).then(function(result) {
				expect(result).to.have.status(201);
			});
	});

	it("returns a 400 if a blank password is provided", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, "john smith", "john@nowhere.com", " ", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if a blank email is provided", function() {
		return fixtures
			.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, "john smith", " ", "random-password", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if a blank display name is provided", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, " ", "john@nowhere.com", "random-password", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if no password is provided", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, "john smith", "john@nowhere.com", null, true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if no email is provided", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, "john smith", null, "random-password", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if no display name is provided", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function() {
				return api.users.register(ctx["Nike"].key, null, "john@nowhere.com", "random-password", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("returns a 400 if the organization key provided does not correspond to a known organization", function() {
		return fixtures.makeSandboxApplication(ctx, api, defaults.APPLICATION_OWNER, [{ref: "Nike"}])
			.then(function(result) {
				return api.users.register("incorrect-organization-key", "john smith", "john@nowhere.com", "random-password", true);
			}).then(function(result) {
				expect(result).to.have.status(400);
			});
	});
});
