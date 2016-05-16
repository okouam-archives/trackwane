(function() {

	class AccessControlUsers {

		constructor($applicationState) {
			this.users = $applicationState.users;
		}
	}

	AccessControlUsers.$inject = ["services.applicationState"];

	angular
		.module("trackwane")
		.component('accessControlUsers', {
			controller: AccessControlUsers,
			template: (
				<div style="background-color: rgb(66,66,66); border-top: 1px solid #666; border-bottom: 1px solid #666">
				<md-content>
			      <md-list style="padding: 0; display: flex">
			        <md-subheader class="md-no-sticky" style="min-width: 140px">Users</md-subheader>
					<div>
			        <md-list-item class="md-3-line" style="flex: 1; padding: 0; display: inline-block; width: 400px; border-bottom: 1px solid #666; border-right: 1px solid #666" ng-repeat="user in $ctrl.users">
						<img style="height: 120px; float: left; margin-right: 10px" src="{{user.photo}}" />
						<div class="md-list-item-text" layout="column">
							<h3 style="margin-top: 5px">{{user.displayName}}</h3>
				            <h4>{{user.email}}</h4>
							<p style="font-weight: normal; font-size: 12px">Managing 0 organizations<br/>
							Administrator for 0 organizations<br/>
							Viewing 2 organizations</p>
						</div>
			        </md-list-item>
					</div>
					</md-list>
				</md-content>
				</div>
			)
		});
})();
