require_relative './sensor-simulator.rb'
require_relative './trackwane-client.rb'

API_URI = "http://www.ctabustracker.com/bustime/api/v1"
API_KEY = "tPhNNj5pvWbnW6yYvWpbx9Ssx"
TRACKWANE_URI = "http://api.trackwane.local/data"
APPLICATION_KEY = "TEST";
ORGANIZATION_KEY = "TEST"

simulator = SensorSimulator.new API_URI, API_KEY
readings = simulator.get_readings
client = TrackwaneClient.new TRACKWANE_URI
readings.each do |reading|
	client.save_reading APPLICATION_KEY, ORGANIZATION_KEY, reading
end
