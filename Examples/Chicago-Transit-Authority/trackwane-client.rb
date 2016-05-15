class TrackwaneClient

	def initialize(uri)
		@uri = uri
	end

	def save_reading(application_key, organization_key, reading)
		response = RestClient.post @uri + "/" + application_key + "/" + organization_key, reading
	end

end
