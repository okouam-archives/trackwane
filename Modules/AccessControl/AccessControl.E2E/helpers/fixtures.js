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
				ctx[user.organization][user.ref] = {
					email: user.email || "john@nowhere.com",
					password: user.password || "random-password",
					displayName: user.displayName || "John Smith"
				}
				var current = ctx[user.organization][user.ref];
				return api.users.register(ctx[user.organization].key, current.displayName, current.email, current.password);
			}).then(function(result) {
				ctx[user.organization][user.ref].key = result.body;
			});
		});
	}

	return chain;
}
