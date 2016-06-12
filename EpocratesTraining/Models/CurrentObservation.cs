using System;
using Newtonsoft.Json;

namespace EpocratesTraining
{
	public class CurrentObservation
	{
		public string Weather { get; set; }

		[JsonProperty("temperature_string")]
		public string Temperature { get; set; }
		//public double temp_f { get; set; }
		//public double temp_c { get; set; }

		[JsonProperty("relative_humidity")]
		public string RelativeHumidity { get; set; }

		[JsonProperty("wind_string")]
		public string WindDescription { get; set; }

		//public string wind_dir { get; set; }
		//public double wind_mph { get; set; }
		//public string wind_gust_mph { get; set; }

		//public double wind_kph { get; set; }
		//public string wind_gust_kph { get; set; }

		//public string pressure_mb { get; set; }
		//public string pressure_in { get; set; }
		//public string pressure_trend { get; set; }

		[JsonProperty("dewpoint_string")]
		public string Dewpoint { get; set; }
		//public int dewpoint_f { get; set; }
		//public int dewpoint_c { get; set; }

		[JsonProperty("heat_index_string")]
		public string HeatIndex { get; set; }
		//public string heat_index_f { get; set; }
		//public string heat_index_c { get; set; }

		[JsonProperty("wind_chill_string")]
		public string Windchill { get; set; }
		//public string windchill_f { get; set; }
		//public string windchill_c { get; set; }

		[JsonProperty("feelslike_string")]
		public string FeelsLike { get; set; }
		//public string feelslike_f { get; set; }
		//public string feelslike_c { get; set; }

		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }

		/*
		public string visibility_mi { get; set; }
		public string visibility_km { get; set; }
		public string solarradiation { get; set; }
		public string UV { get; set; }
		public string precip_1hr_string { get; set; }
		public string precip_1hr_in { get; set; }
		public string precip_1hr_metric { get; set; }
		public string precip_today_string { get; set; }
		public string precip_today_in { get; set; }
		public string precip_today_metric { get; set; }
		public string icon { get; set; }
		public string icon_url { get; set; }
		public string forecast_url { get; set; }
		public string history_url { get; set; }
		public string ob_url { get; set; }
		public string nowcast { get; set; }*/
	}
}

