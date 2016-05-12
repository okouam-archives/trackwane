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
			.delete(host + "/organizations/" + key, REQUEST_OPTIONS)
			.then(function(result) {
				return verify(result, skipVerification, 200);
			});
	};

	this.update = function() {

	};

	this.revokePermission = function(organizationKey, userKey, permissionType, skipVerification) {

	};
};
