using System;
using Android.App;
using Android.Widget;
using Android.OS;

using Plugin.Connectivity;

using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.Droid
{
	[Activity(Label = "Epocrates Training", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		Button smallSongListButton;
		Button largeSongListButton;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			smallSongListButton = FindViewById<Button>(Resource.Id.smallSongListButton);
			largeSongListButton = FindViewById<Button>(Resource.Id.largeSongLightButton);
		}

		protected override void OnResume()
		{
			base.OnResume();

			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;

			smallSongListButton.Click += smallSongListButton_Click;
			largeSongListButton.Click += largeSongListButton_Click;
		}

		protected override void OnPause()
		{
			base.OnPause();

			CrossConnectivity.Current.ConnectivityChanged -= CrossConnectivity_Current_ConnectivityChanged;

			smallSongListButton.Click -= smallSongListButton_Click;
			largeSongListButton.Click -= largeSongListButton_Click;
		}

		async void smallSongListButton_Click(object sender, EventArgs e)
		{
			var songs = await SongService.Instance.GetSmallSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		async void largeSongListButton_Click(object sender, EventArgs e)
		{
			var songs = await SongService.Instance.GetLargeSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		void ShowSongCount(int count)
		{
			Message.ShowSimpleMessage(this, "Songs Received", $"{count} received!");
		}

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			Message.ShowSimpleMessage(this, "Connection Event Detected", $"Connected: {e.IsConnected}");
		}
	}
}


