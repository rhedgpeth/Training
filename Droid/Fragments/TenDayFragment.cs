
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.Droid
{
	public class TenDayFragment : Fragment
	{
		List<ForecastDay> forecastDays;
		ListView listView;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.TenDayFragment, container, false);

			((MainActivity)this.Activity).StartProgressIndicator();

			listView = view.FindViewById<ListView>(Resource.Id.weatherList);
			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				var forecastDay = forecastDays[e.Position];

				if (forecastDay != null)
				{
					// Push new fragment
				}
			};

			Task.Run(async () =>
			{
				forecastDays = await WeatherService.Instance.Get10DayForecastCached();

				if (forecastDays != null)
				{
					this.Activity.RunOnUiThread(() =>
					{
						listView.Adapter = new WeatherAdapter(this.Activity, forecastDays);
						//StopProgressIndicator();
					});
				}

				((MainActivity)this.Activity).StopProgressIndicator();
			});

			return view;
		}
	}
}

