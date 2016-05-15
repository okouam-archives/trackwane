(function() {
	angular
		.module("trackwane")
		.component('dashboardTracker', {
			controller: function() {
			},
			template: (
				<div>
					<p style="padding: 25px; font-weight: bold; font-size: 18px; padding-bottom: 0; padding-top: 0">CTA Bus 2034 on Route 6</p>
					<image src="/images/sample-closeup.png" style="width: 300px" />
					<div style="padding: 25px; padding-top: 5px">
						<ul>
							<li style="color: #6699cc">Driver</li>
							<li>John Smith</li>
							<li style="margin-top: 10px; color: #6699cc">Speed</li>
							<li>23mph</li>
							<li style="margin-top: 10px; color: #6699cc">Location</li>
							<li>123 Cheltenham Road, Battersea, London</li>
							<li style="margin-top: 10px; color: #6699cc">Places Nearby</li>
							<li>Luigi Pizzeria (0.2m)</li>
							<li>IGA Supermarket (0.4m)</li>
							<li>Hotel Hilton (1.3m)</li>
						</ul>
					</div>
				</div>
			)
		});
})();
