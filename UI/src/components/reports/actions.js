(function() {
	angular
		.module("trackwane")
		.component('reportActions', {
			controller: function() {
			},
			template: (
				<div>
					<ul>
						<li class="header" style="padding-top: 0">Save Report</li>
						<li style="padding: 10px">
							REPORT NAME
						</li>
						<li style="padding: 10px">
							<button>SAVE</button>
						</li>
					</ul>
					<ul style="border-top: 1px solid #888">
						<li class="header" style="padding-top: 10px">Email Report</li>
						<li style="padding: 10px">
							RECIPIENTS
						</li>
						<li style="padding: 10px">
							MESSAGE
						</li>
						<li style="padding: 10px">
							<button>SEND</button>
						</li>
					</ul>
					<ul style="border-top: 1px solid #888">
						<li class="header" style="padding-top: 10px">Save Report</li>
						<li style="padding: 10px">
							SCHEDULE REPORT
						</li>
						<li style="padding: 10px">
							REPORT NAME
						</li>
						<li style="padding: 10px">
							RECIPIENTS
						</li>
						<li style="padding: 10px">
							START DATE
						</li>
						<li style="padding: 10px">
							<button>SEND</button>
						</li>
					</ul>
				</div>
			)
		});
})();
