(function() {
	angular
		.module("trackwane")
		.component('reportBuilder', {
			controller: function() {
			},
			template: (
				<div style="width: 100%;  border-top: 1px solid #666">
					<ul>
						<li class="header" style="padding-top: 0">Report Builder</li>
						<li style="padding: 10px">
						<md-datepicker ng-model="myDate" md-placeholder="From"></md-datepicker>
						</li>
						<li style="padding: 10px">
						<md-datepicker ng-model="myDate" md-placeholder="To"></md-datepicker>
						</li>
						<li style="padding: 10px">Report Type</li>
					</ul>
				</div>
			)
		});
})();
