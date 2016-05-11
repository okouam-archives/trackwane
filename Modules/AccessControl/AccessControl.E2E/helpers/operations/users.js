module.exports = function(host, chakram, REQUEST_OPTIONS, verify) {

	this.archive = function(organizationKey, userKey, skipVerification) {
		return chakram
			.delete(host + "/organizations/" + organizationKey + "/users/" + userKey, {}, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 204)
			});
	};

	this.findByKey = function(key, skipVerification) {
		return chakram
			.get(host + "/users/" + key, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification)
			});
	};

	this.register = function(organizationKey, displayName, email, password, skipVerification) {
		return chakram
			.post(host + "/organizations/" + organizationKey + "/users", {
				password: password,
				email: email,
				displayName: displayName
			}, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 201);
			});
	};
};
