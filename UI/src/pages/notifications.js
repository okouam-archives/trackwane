(function() {

	angular
		.module("trackwane")
		.component('notificationsPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row" class="flexable">
						<notifications-organizations style="min-width: 350px" />
						<div flex style="flex-direction: column; display: flex">
							<notifications-trackers flex />
							<notifications-locations flex />
							<notifications-boundaries flex />
							<notifications-rules flex />
							<notifications-actions flex />
						</div>
					</div>
				</div>
			)
		});
})();
