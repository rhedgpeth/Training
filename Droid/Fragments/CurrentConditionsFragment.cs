
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
using EpocratesTraining.Services;
using FFImageLoading;
using FFImageLoading.Views;

namespace EpocratesTraining.Droid
{
	public class CurrentConditionsFragment : Fragment
	{
		View view;
		CurrentObservation currentConditions; 

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			view = inflater.Inflate(Resource.Layout.CurrentConditionsFragment, container, false);

			((MainActivity)this.Activity).StartProgressIndicator();

			Task.Run(async () =>
			{
				if (currentConditions == null)
					currentConditions = await WeatherService.Instance.GetCurrentConditions();

				if (currentConditions != null)
				{
					this.Activity.RunOnUiThread(() =>
					{
						var iconImage = view.FindViewById<ImageViewAsync>(Resource.Id.currentConditionIconImage);
						ImageService.Instance.LoadUrl(currentConditions.IconUrl).IntoAsync(iconImage);

						view.FindViewById<TextView>(Resource.Id.currentConditionsText).Text = currentConditions.Weather;
						view.FindViewById<TextView>(Resource.Id.temperatureText).Text = currentConditions.Temperature;
						view.FindViewById<TextView>(Resource.Id.feelsLikeText).Text = currentConditions.FeelsLike;
						view.FindViewById<TextView>(Resource.Id.windText).Text = currentConditions.WindDescription;
						view.FindViewById<TextView>(Resource.Id.windChillText).Text = currentConditions.Windchill;
						view.FindViewById<TextView>(Resource.Id.humidityText).Text = currentConditions.RelativeHumidity;
						view.FindViewById<TextView>(Resource.Id.dewpointText).Text = currentConditions.Dewpoint;
						view.FindViewById<TextView>(Resource.Id.heatIndexText).Text = currentConditions.HeatIndex;
					});
				}

				((MainActivity)this.Activity).StopProgressIndicator();
			});

			return view;
		}
	}
}

