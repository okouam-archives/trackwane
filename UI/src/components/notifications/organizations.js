(function() {

	class NotificationsOrganizations {

		constructor($applicationState) {
			this.organizations = $applicationState.organizations;
		}

	}

	NotificationsOrganizations.$inject =  ["services.applicationState"];

	angular
		.module("trackwane")
		.component('notificationsOrganizations', {
			controller: NotificationsOrganizations,
			template: (
				<md-content flex layout="column">
					<md-subheader class="md-sticky">Organizations</md-subheader>
			        <md-list-item class="md-2-line" ng-repeat="organization in $ctrl.organizations">
			          <div class="md-list-item-text" layout="column">
			            <h3>{{organization.name}}</h3>
			          </div>
					  <md-divider></md-divider>
			        </md-list-item>
				</md-content>
			)
		});
})();
