(function() {

	class NotificationsTrackers {

		constructor() {
		}

	}

	NotificationsTrackers.$inject = [];


	angular
		.module("trackwane")
		.component('notificationsTrackers', {
			controller: NotificationsTrackers,
			template: (
				<div>
					SHOW TRACKERS FOR ORGANIZATION
				</div>
			)
		});
})();
