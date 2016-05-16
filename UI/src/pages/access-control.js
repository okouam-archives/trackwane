(function() {

	angular
		.module("trackwane")
		.component('accessControlPage', {
			template: (
				<div class="canvas">
					<top-navigation />
					<div style="flex-direction: row" class="flexable">
						<div style="overflow-y: scroll; flex-direction: column; border-top: 1px solid #666" class="flexable">
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
