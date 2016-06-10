using System;
using System.Data;
using Newtonsoft.Json;

namespace EpocratesTraining.Models
{
	public class ForecastDay
	{
		public int Period { get; set; }
		public string Icon { get; set; }
		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }
		public string Title { get; set; }
		[JsonProperty("fcttext")]
		public string Description { get; set; }
		[JsonProperty("fcttext_metric")]
		public string DescriptionMetric { get; set; }
	}
}

