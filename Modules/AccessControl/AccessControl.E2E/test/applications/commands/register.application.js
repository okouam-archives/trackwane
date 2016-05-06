require(process.cwd() + '/background')();

describe("Applications :: Commands :: Register Application", function() {

	var api, ctx;

	beforeEach(function () {
		api = new API(defaults.HOST, chakram);
		ctx = {};
	});

	it("returns a 200 and creates the application owner", function() {
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function(result) {
				ctx.userKey = result.body;
			})
			.then(function() {
				return api.login(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password)
			})
			.then(function(result) {
				return api.users.findByKey(ctx.userKey);
			})
			.then(function(result) {
				expect(result).to.have.status(200);
				expect(result.body.DisplayName).to.equal(defaults.APPLICATION_OWNER.displayName);
			});
	});

	it("returns a 400 because it does not allow duplicate applications to be registered", function() {
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function(result) {
				return api
					.createApplication(defaults.APPLICATION_OWNER)
					.then(function(result) {
		   				expect(result).to.have.status(400);
						expect(result.response.body.Message).to.equal("The application with key " + api.APPLICATION_KEY + " already exists")
		   			});
			});
	})

    it("should return the key for the application owner", function () {
        return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function(result) {
				expect(result.response.statusCode).to.equal(200);
				expect(result.body).to.not.be.empty;
			});
    });

	it("should check an email is provided", function() {
        return api
			.createApplication(_.merge({}, defaults.APPLICATION_OWNER, {email: ''} ))
			.then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("should check a display name is provided", function() {
        return api
			.createApplication(_.merge({}, defaults.APPLICATION_OWNER, {displayName: ''} ))
			.then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("should check a password is provided", function() {
		return api
			.createApplication(_.merge({}, defaults.APPLICATION_OWNER, {password: ''} ))
			.then(function(result) {
				expect(result).to.have.status(400);
			});
	});

	it("should check a secret key is provided", function() {
		return api
			.createApplication(_.merge({}, defaults.APPLICATION_OWNER, {secretKey: ''} ))
			.then(function(result) {
				expect(result).to.have.status(400);
			});
	});

});
