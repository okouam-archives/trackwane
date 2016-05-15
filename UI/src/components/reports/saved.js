(function() {
	angular
		.module("trackwane")
		.component('savedReports', {
			controller: function() {
				this.saved_reports = [
					{name: "Monthly Speed Report for AgeRoute"},
					{name: "Monthly Speed Report for CIBICI"},
					{name: "Distance Report for Residence Eburnea"},
					{name: "Monthly Speed Report for AgeRoute fleet"},
					{name: "Monthly Speed Report for AgeRoute fleet"}
				];
			},
			template: (
				<div style="width: 100%">
					<ul>
						<li class="header">Saved Reports</li>
						<li ng-repeat="report in $ctrl.saved_reports" style="padding: 10px">
							<i class="material-icons" style="position: relative; top: 5px">directions_bus</i>
							{{report.name}}
						</li>
					</ul>
				</div>
			)
		});
})();
