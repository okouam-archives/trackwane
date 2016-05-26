(function() {

	class NotificationsTrackers {

		constructor($applicationState) {
			this.trackers = $applicationState.trackers;
		}

	}

	NotificationsTrackers.$inject = ["services.applicationState"];


	angular
		.module("trackwane")
		.component('notificationsTrackers', {
			controller: NotificationsTrackers,
			template: (
				<md-content flex layout="column">
					<md-subheader class="md-sticky">Trackers</md-subheader>
					<md-list-item class="md-2-line" style="display: inline-block; width: 400px; float: left" ng-repeat="tracker in $ctrl.trackers">
					  <div class="md-list-item-text" layout="column">
						<h3>{{tracker.identifier}}</h3>
					  </div>
					  <md-divider></md-divider>
					</md-list-item>
				</md-content>
			)
		});
})();
