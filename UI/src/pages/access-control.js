(function() {

	angular
		.module("trackwane")
		.component('accessControlPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row; display: flex; flex: 1">
						<div style="overflow-y: scroll">
							<access-control-organizations />
							<access-control-users />
						</div>
						<access-control-organization-detail class="z-depth-5" />
						<access-control-user-detail class="z-depth-5" />
					</div>
				</div>
			)
		});
})();
