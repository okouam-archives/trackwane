require 'rest-client'
require 'nokogiri'

class SensorSimulator

	def initialize(uri, key)
		@uri = uri
		@key = key
	end

	def show_available_routes
		response = RestClient.get @uri + "/getroutes?key=" + @key
		doc = Nokogiri::XML(response)
		routes = []
		doc.xpath('//rt').each do |rt|
			routes << rt.text
		end
		puts routes
	end

	def get_readings
		response = RestClient.get @uri + "/getvehicles?rt=71,72,73&key=" + @key
		Nokogiri::XML(response).xpath('//vehicle').map do |vehicle|
			{
				latitude: vehicle.css('lat').text,
				heading: vehicle.css('hdg').text,
				speed: vehicle.css('spd').text,
				longitude: vehicle.css('lon').text
			}
		end
	end

end
