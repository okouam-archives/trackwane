var sleep = require('./sleep');
var Organizations = require('./operations/organizations');
var Users = require('./operations/users');
var Boundaries = require('./operations/boundaries');
var Locations = require('./operations/locations');
var Sensors = require('./operations/sensors');

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

	var common = {

		register: function(resource, organizationKey, payload, skipVerification) {
			return chakram
				.post(host + "/organizations/" + organizationKey + "/" + resource, payload, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification, 201)
				});
		},

		archive: function(resource, organizationKey, resourceKey, skipVerification) {
			return chakram
				.delete(host + "/organizations/" + organizationKey + "/" + resource + "/" + resourceKey, {}, REQUEST_OPTIONS)
				.then(function(result) {
					return verify(result, skipVerification, 204)
				});
		}
	};

	this.createApplication = function(owner, skipVerification) {
		return chakram.
			post(host + "/application", owner, REQUEST_OPTIONS)
			.then(function(result) {
				sleep(2);
				return result;
			});
	};

	this.boundaries = new Boundaries(host, chakram, REQUEST_OPTIONS, verify, common);

	this.locations = new Locations(host, chakram, REQUEST_OPTIONS, verify, common);

	this.users = new Users(host, chakram, REQUEST_OPTIONS, verify, common);

	this.sensors = new Sensors(host, chakram, REQUEST_OPTIONS, verify, common);

	this.organizations = new Organizations(host, chakram, REQUEST_OPTIONS, verify, common);

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
