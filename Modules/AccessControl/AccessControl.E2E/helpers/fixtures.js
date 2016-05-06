exports.makeSandboxApplication = function(ctx, api, owner, organizations, users) {
	var chain = api
		.createApplication(owner)
		.then(function() {
			return api.login(owner.email, owner.password);
		});

	if (organizations) {
		_.each(organizations, function(organization) {
			chain = chain.then(function(result) {
				return api.organizations.register("random-organization-name");
			}).then(function(result) {
				ctx[organization.ref] = {key: result.body};
			});
		})
	}

	if (users) {
		_.each(users, function(user) {
			chain = chain.then(function(result) {
				return api.users.register(ctx[user.organization].key, "john smith", "john@nowhere.com", "random-password");
			}).then(function(result) {
				ctx[user.organization][user.ref] = {key: result.body};
			});
		});
	}

	return chain;
}
