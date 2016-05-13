var sleep = require('./sleep');
var Organizations = require('./operations/organizations');
var Users = require('./operations/users');

module.exports = function(host, chakram) {

	this.APPLICATION_KEY = hashids.encode(new Date().getTime());

	//console.log("Creating new Trackwane Application with key: ", this.APPLICATION_KEY);

	var REQUEST_OPTIONS = {
		headers: {
			"X-Trackwane-Application": this.APPLICATION_KEY
		}
	};

	function verify(result, skipVerification, expectedCode) {
		if (!skipVerification) chakram.expect(result).to.have.status(expectedCode || 200);
		return result;
	}

	this.createApplication = function(owner, skipVerification) {
		return chakram.
			post(host + "/application", owner, REQUEST_OPTIONS)
			.then(function(result) {
				sleep(2);
				return result;
			});
	};

	this.users = new Users(host, chakram, REQUEST_OPTIONS, verify);

	this.organizations = new Organizations(host, chakram, REQUEST_OPTIONS, verify);

	this.authenticate = function(email, password, skipVerification) {
		var opts = _.merge({}, REQUEST_OPTIONS, {qs: {email: email, password: password}});
		return chakram
			.get(host + "/token", opts)
			.then(function(result) {
				return verify(result, skipVerification)
			});
	}

	this.login = function(email, password, skipVerification) {
		sleep(2);
		var opts = _.merge({}, REQUEST_OPTIONS, {qs: {email: email, password: password}});
		REQUEST_OPTIONS.headers.Authorization = null;
		return chakram
			.get(host + "/token", opts)
			.then(function(result) {
				REQUEST_OPTIONS.headers.Authorization = result.body;
				return verify(result, skipVerification)
			});
	};
}
