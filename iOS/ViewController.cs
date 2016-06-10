using System;
using UIKit;

using Plugin.Connectivity;

using EpocratesTraining.Services;

namespace EpocratesTraining.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{ }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;

			smallSongsListButton.TouchUpInside += SmallSongsListButton_TouchUpInside;
			largeSongsListButton.TouchUpInside += LargeSongsListButton_TouchUpInside;
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			CrossConnectivity.Current.ConnectivityChanged -= CrossConnectivity_Current_ConnectivityChanged;

			smallSongsListButton.TouchUpInside -= SmallSongsListButton_TouchUpInside;
			largeSongsListButton.TouchUpInside -= LargeSongsListButton_TouchUpInside;
		}

		async void SmallSongsListButton_TouchUpInside(object sender, EventArgs e)
		{
			isBusyIndicator.StartAnimating();
			var songs = await SongService.Instance.GetSmallSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		async void LargeSongsListButton_TouchUpInside(object sender, EventArgs e)
		{
			isBusyIndicator.StartAnimating();
			var songs = await SongService.Instance.GetLargeSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		void ShowSongCount(int count)
		{
			Message.ShowSimpleMessage(this, "Songs Received", $"{count} received!");
			isBusyIndicator.StopAnimating();
		}

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			Message.ShowSimpleMessage(this, "Connection Event Detected", $"Connected: {e.IsConnected}");
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
