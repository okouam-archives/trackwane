module.exports = function(host, chakram, REQUEST_OPTIONS, verify, common) {

	this.archive = function(organizationKey, locationKey, skipVerification) {
		return common.archive('locations', organizationKey, locationKey, skipVerification);
	};

	this.register = function(organizationKey, payload, skipVerification) {
		return common.register('locations', organizationKey, payload, skipVerification);
	};

};
