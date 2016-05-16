(function() {

	class NotificationsRules {

		constructor() {
		}

	}

	NotificationsRules.$inject = [];


	angular
		.module("trackwane")
		.component('notificationsRules', {
			controller: NotificationsRules,
			template: (
				<div>
					SHOW RULES FOR ORGANIZATION
				</div>
			)
		});
})();
