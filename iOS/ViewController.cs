using System;
using System.Threading.Tasks;

using UIKit;

using EpocratesTraining.Models;
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

			smallSongsListButton.TouchUpInside += SmallSongsListButton_TouchUpInside;
			largeSongsListButton.TouchUpInside += LargeSongsListButton_TouchUpInside;
		}

		public override void ViewDidUnload()
		{
			base.ViewDidUnload();

			smallSongsListButton.TouchUpInside -= SmallSongsListButton_TouchUpInside;
			largeSongsListButton.TouchUpInside -= LargeSongsListButton_TouchUpInside;
		}

		async void SmallSongsListButton_TouchUpInside(object sender, EventArgs e)
		{
			var songs = await SongService.Instance.GetSmallSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		async void LargeSongsListButton_TouchUpInside(object sender, EventArgs e)
		{
			var songs = await SongService.Instance.GetLargeSongsList();
			ShowSongCount(songs?.Count ?? 0);
		}

		void ShowSongCount(int count)
		{
			InvokeOnMainThread(() =>
			{
				var alert = UIAlertController.Create("Songs Received", $"{count} received!", UIAlertControllerStyle.Alert);
				alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
				PresentViewController(alert, true, null);
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
