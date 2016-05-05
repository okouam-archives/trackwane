var root = process.cwd(),
    pp = require('jsome'),
﻿	 chakram = require('chakram'),
	sleep = require(root + '/helpers/sleep'),
	inspect = require(root + '/helpers/inspect'),
	client = require(root + '/helpers/api'),
	defaults = require(root + '/helpers/defaults'),
	expect = chakram.expect;
 	_ = require('lodash'),
	Hashids = require("hashids"),
	hashids = new Hashids("this is my salt");

describe("Get Access Token Query", function() {

	it("returns a 200 when the credentials provided are valid", function() {
		var api = new client(defaults.HOST, chakram);
		return api
			.createApplication(defaults.APPLICATION_OWNER)
			.then(function(result) {
				return api.authenticate(defaults.APPLICATION_OWNER.email, defaults.APPLICATION_OWNER.password, true);
			});
	});

	it("returns a 400 when the password provided is incorrect", function() {
		var api = new client(defaults.HOST, chakram);
		return api.createApplication(defaults.APPLICATION_OWNER)
		.then(function() {
			var result = api.authenticate(defaults.APPLICATION_OWNER.email, "RANDOM-INCORRECT-PASSWORD", true);
			expect(result).to.have.status(400);
			return result;
		});
	});

	it("returns a 400 when the email provided is incorrect", function() {
		var api = new client(defaults.HOST, chakram);
		return api.createApplication(defaults.APPLICATION_OWNER)
		.then(function() {
			var result = api.authenticate("RANDOM-INCORRECT-EMAIL", defaults.APPLICATION_OWNER.password, true);
			expect(result).to.have.status(400);
			return result;
		});
	});
});
