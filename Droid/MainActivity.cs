using System;
using Android.App;
using Android.Widget;
using Android.OS;

using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.Droid
{
	[Activity(Label = "Epocrates Training", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		AlertDialog.Builder alert;

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

			smallSongListButton.Click += smallSongListButton_Click;
			largeSongListButton.Click += largeSongListButton_Click;
		}

		protected override void OnPause()
		{
			base.OnPause();

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
			if (alert == null)
			{
				alert = new AlertDialog.Builder(this);
				alert.SetTitle("Songs Received");
				alert.SetPositiveButton("OK", (senderAlert, args) => { });
			}

			alert.SetMessage($"{count} received!");

			RunOnUiThread(() =>
			{
				alert.Show();
			});
		}
	}
}


