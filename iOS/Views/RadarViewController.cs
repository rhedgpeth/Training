using System;
using Foundation;
using UIKit;

namespace EpocratesTraining.iOS
{
	public partial class RadarViewController : UIViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			var url = new NSUrl("http://mobile.weather.gov/index.php?lat=37.77492773500046&lon=-122.41941932299972#radar");
			var request = new NSUrlRequest(url);

			webView.LoadRequest(request);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


