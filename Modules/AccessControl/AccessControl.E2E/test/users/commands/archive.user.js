var pp = require('jsome'),
﻿	 chakram = require('chakram'),
	sleep = require('../../../helpers/sleep'),
	inspect = require('../../../helpers/inspect'),
	client = require('../../../helpers/api'),
	defaults = require('../../../helpers/defaults'),
	expect = chakram.expect;
 	_ = require('lodash'),
	Hashids = require("hashids"),
	hashids = new Hashids("this is my salt");

describe("Archive User Command", function() {

	it("returns a 204 when the user is active", function() {
		var organizationKey, api = new client(defaults.HOST, chakram);
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function() {
				return api.login(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password);
			}).then(function(result) {
				return api.organizations.register("random-organization-name");
			}).then(function(result) {
				organizationKey = result.body;
				return api.users.register(organizationKey, "john smith", "john@nowhere.com", "random-password");
			}).then(function(result) {
				var userKey = result.body;
				return api.users.archive(organizationKey, userKey, false)
			}).then(function(result) {
				expect(result).to.have.status(204);
			});
	});

	xit("returns a 204 when the user is already archived", function() {
		var api = new client(defaults.HOST, chakram);
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function() {
				api.authenticate(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password)
			}).then(function() {
				var userKey = api.users.register("sdfsdf", "sdfsdf", "sdfsdfdsf")
			}).then(function() {
				return api.users.archive(userKey);
			}).then(function() {
				return api.users.archive(userKey);
			});
	});
});
