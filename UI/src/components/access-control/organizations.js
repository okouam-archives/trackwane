(function() {

	class AccessControlOrganizations {

		constructor($applicationState) {
			this.organizations = $applicationState.organizations;
		}
	}

	AccessControlOrganizations.$inject = ["services.applicationState"];

	angular
		.module("trackwane")
		.component('accessControlOrganizations', {
			controller: AccessControlOrganizations,
			template: (
				<div style="background-color: rgb(66,66,66)">
				<md-content>
			      <md-list style="padding: 0; display: flex">
			        <md-subheader class="md-no-sticky" style="min-width: 140px">Organizations</md-subheader>
					<div>
			        <md-list-item class="md-3-line" style="flex: 1; padding: 0; display: inline-block; width: 400px; border-bottom: 1px solid #666; border-right: 1px solid #666" ng-repeat="organization in $ctrl.organizations">
						<img style="height: 120px; width: 120px; float: left; margin-right: 10px" src="{{organization.photo}}" />
						<div class="md-list-item-text" layout="column">
							<h3 style="margin-top: 5px">{{organization.name}}</h3>
				            <h4>{{organization.userCount}} users</h4>
						</div>
			        </md-list-item>
					</div>
					</md-list>
				</md-content>
				</div>
			)
		});
})();
