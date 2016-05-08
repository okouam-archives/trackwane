var sleep = require('./sleep');

module.exports = function(host, chakram) {

	this.APPLICATION_KEY = hashids.encode(new Date().getTime());

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
	}

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
	}

	this.organizations = {
		register: function(name, skipVerification) {
			return chakram
				.post(host + "/organizations", {
					name: name
				}, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification, 201);
				});
		}
	};

	this.users = {
		archive: function(organizationKey, userKey, skipVerification) {
			return chakram
				.delete(host + "/organizations/" + organizationKey + "/users/" + userKey, {}, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification, 204)
				});
		},
		findByKey: function(key, skipVerification) {
			return chakram
				.get(host + "/users/" + key, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification)
				});
		},
		register: function(organizationKey, displayName, email, password, skipVerification) {
			return chakram
				.post(host + "/organizations/" + organizationKey + "/users", {
					password: password,
					email: email,
					displayName: displayName
				}, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification, 201);
				});
		}
	}
}
