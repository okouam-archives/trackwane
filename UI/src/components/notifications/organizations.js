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
				<div>
					<md-subheader class="md-no-sticky">3 line item</md-subheader>
			        <md-list-item class="md-3-line" ng-repeat="organization in $ctrl.organizations">
			          <i class="material-icons">directions_bus</i>
			          <div class="md-list-item-text" layout="column">
			            <h3>{{ organization.name }}</h3>
			            <h4>{{ organization.userCount }}</h4>
			          </div>
			        </md-list-item>
			        <md-divider ></md-divider>
				</div>
			)
		});
})();
