using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EpocratesTraining.Models;

namespace EpocratesTraining.Services
{
	public class WeatherService : BaseService
	{
		private const string apiKey = "a660601619ff5337";

		static readonly WeatherService instance = new WeatherService();

		public static WeatherService Instance
		{
			get { return instance; }
		}

		WeatherService()
		{
			client.BaseAddress = new Uri($"http://api.wunderground.com/api/{apiKey}/");
		}

		public Task<List<ForecastDay>> Get10DayForecast()
		{
			return GetAsync<List<ForecastDay>>("forecast10day/q/CA/San_Francisco.json", x => x["forecast"]["txt_forecast"]["forecastday"]);
		}
	}
}

