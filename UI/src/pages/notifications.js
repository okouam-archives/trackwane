(function() {

	angular
		.module("trackwane")
		.component('notificationsPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row" class="flexable">
						<notifications-organizations class="flexable"/>
						<div class="flexable" style="flex-direction: column">
							<notifications-trackers class="flexable"/>
							<notifications-locations class="flexable"/>
							<notifications-boundaries class="flexable"/>
							<notifications-rules  class="flexable"/>
							<notifications-actions class="flexable" />
						</div>
					</div>
				</div>
			)
		});
})();
