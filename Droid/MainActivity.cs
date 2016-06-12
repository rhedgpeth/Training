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

		CurrentConditionsFragment currentConditionsFragment;
		RadarFragment radarFragment;
		TenDayFragment tenDayFragment;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			currentConditionsFragment = new CurrentConditionsFragment();
			FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
			transaction.Add(Resource.Id.mainContent, currentConditionsFragment, "CurrentFragment");
			transaction.CommitAllowingStateLoss();

			var currentConditionsButton = FindViewById<Button>(Resource.Id.currentConditionsButton);
			currentConditionsButton.Click += (sender, e) =>
			{
				if (currentConditionsFragment == null)
					currentConditionsFragment = new CurrentConditionsFragment();

				PushRootFragment(currentConditionsFragment);
			};

			var radarButton = FindViewById<Button>(Resource.Id.radarButton);
			radarButton.Click += (sender, e) =>
			{
				if (radarFragment == null)
					radarFragment = new RadarFragment();

				PushRootFragment(radarFragment);
			};

			var tenDayForecastButton = FindViewById<Button>(Resource.Id.tenDayButton);
			tenDayForecastButton.Click += (sender, e) =>
			{
				if (tenDayFragment == null)
					tenDayFragment = new TenDayFragment();

				PushRootFragment(tenDayFragment);
			};
		}

		void PushRootFragment(Fragment frag)
		{
			FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
			transaction.Replace(Resource.Id.mainContent, frag);
			transaction.CommitAllowingStateLoss();
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

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			Message.ShowSimpleMessage(this, "Connection Event Detected", $"Connected: {e.IsConnected}");
		}
	}
}


