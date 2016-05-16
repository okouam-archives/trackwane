(function() {

	class NotificationsBoundaries {

		constructor() {
		}

	}

	NotificationsBoundaries.$inject = [];


	angular
		.module("trackwane")
		.component('notificationsBoundaries', {
			controller: NotificationsBoundaries,
			template: (
				<div>
					SHOW BOUNDARIES FOR ORGANIZATION
				</div>
			)
		});
})();
