using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.App;
using Android.Widget;
using Android.OS;

using Plugin.Connectivity;

using EpocratesTraining.Models;
using EpocratesTraining.Services;
using Android.Content;

namespace EpocratesTraining.Droid
{
	[Activity(Label = "Epocrates Training", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		ProgressDialog progressDialog;
		ListView listView;
		List<ForecastDay> forecastDays;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			StartProgressIndicator();

			listView = FindViewById<ListView>(Resource.Id.weatherList);
			listView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				var forecastDay = forecastDays[e.Position];

				if (forecastDay != null)
				{
					var intent = new Intent(this, typeof(DetailsActivity));
					intent.PutExtra("forecast_day_title", forecastDay.Title);
					intent.PutExtra("forecast_day_description", forecastDay.Description);
					StartActivity(intent);
				}
			};

			Task.Run(async () =>
			{
				forecastDays = await WeatherService.Instance.Get10DayForecast();

				if (forecastDays != null)
				{
					RunOnUiThread(() =>
					{
						listView.Adapter = new WeatherAdapter(this, forecastDays);
						StopProgressIndicator();
					});
				}
			});
		}

		protected override void OnResume()
		{
			base.OnResume();
			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;
		}

		protected override void OnPause()
		{
			base.OnPause();
			CrossConnectivity.Current.ConnectivityChanged -= CrossConnectivity_Current_ConnectivityChanged;
		}

		void StartProgressIndicator()
		{
			progressDialog = new ProgressDialog(this);
			progressDialog.SetMessage("Retrieving data...");
			progressDialog.Show();
			progressDialog.SetCanceledOnTouchOutside(false);
			progressDialog.SetCancelable(false);
		}

		void StopProgressIndicator()
		{
			if (progressDialog.IsShowing)
				progressDialog.Dismiss();
		}

		void ShowSongCount(int count)
		{
			StopProgressIndicator();
			Message.ShowSimpleMessage(this, "Songs Received", $"{count} received!");
		}

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			Message.ShowSimpleMessage(this, "Connection Event Detected", $"Connected: {e.IsConnected}");
		}
	}
}


