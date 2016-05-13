exports.makeSandboxApplication = function(ctx, api, owner, organizations, users) {
	var chain = api
		.createApplication(owner)
		.then(function() {
			return api.login(owner.email, owner.password);
		});

	if (organizations) {
		_.each(organizations, function(organization) {
			chain = chain.then(function(result) {
				return api.organizations.register(organization.ref);
			}).then(function(result) {
				ctx[organization.ref] = {key: result.body};
			});
		})
	}

	if (users) {
		_.each(users, function(user) {
			chain = chain.then(function(result) {
				ctx[user.organization][user.ref] = {
					email: user.email || user.ref +  "@nowhere.com",
					password: user.password || "random-password",
					displayName: user.displayName || user.ref
				}
				var current = ctx[user.organization][user.ref];
				return api.users.register(ctx[user.organization].key, current.displayName, current.email, current.password);
			}).then(function(result) {
				ctx[user.organization][user.ref].key = result.body;
			});

			if (user.manage) {
				_.each(user.manage, function(organization) {
					chain = chain.then(function() {
						var current = ctx[user.organization][user.ref];
						return api.organizations.grantPermission(ctx[organization].key, current.key, 'manage', false);
					})
				});
			}

			if (user.administrate) {
				_.each(user.administrate, function(organization) {
					chain = chain.then(function() {
						var current = ctx[user.organization][user.ref];
						return api.organizations.grantPermission(ctx[organization].key, current.key, 'administrate', false);
					})
				});
			}

			if (user.view) {
				_.each(user.view, function(organization) {
					chain = chain.then(function() {
						var current = ctx[user.organization][user.ref];
						return api.organizations.grantPermission(ctx[organization].key, current.key, 'view', false);
					})
				});
			}
		});
	}

	return chain;
}
