module.exports = function(host, chakram, REQUEST_OPTIONS, verify) {

	this.register = function(name, skipVerification) {
		return chakram
			.post(host + "/organizations", {
				name: name
			}, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 201);
			});
	};

	this.archive = function(key, skipVerification) {
		return chakram
			.delete(host + "/organizations/" + key, null, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 200);
			});
	};

	this.update = function(key, changes, skipVerification) {
		return chakram
			.post(host + "/organizations/" + key, changes, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 204);
			});
	};

	this.count = function(skipVerification) {
		return chakram
			.get(host + "/organizations/count", REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 200);
			});
	};

	this.findAll = function(skipVerification) {
		return chakram
			.get(host + "/organizations", REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 200);
			});
	};

	this.findByKey = function(key, skipVerification) {
		return chakram
			.get(host + "/organizations/" + key, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 200);
			});
	};

	this.grantPermission = function(organizationKey, userKey, permissionType, skipVerification) {
		return chakram
			.post(host + "/organizations/" + organizationKey + "/users/" + userKey + "/" + permissionType, null, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 204);
			});
	};

	this.revokePermission = function(organizationKey, userKey, permissionType, skipVerification) {
		return chakram
			.delete(host + "/organizations/" + organizationKey + "/users/" + userKey + "/" + permissionType, null, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 204);
			});
	};
};
