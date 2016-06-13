using System;

using UIKit;

namespace EpocratesTraining.iOS
{
	public partial class MainViewController : UITabBarController
	{
		UIViewController currentConditionsViewController, radarViewController, tenDayForecastViewController;

		public MainViewController() : base("MainViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			currentConditionsViewController = new CurrentConditionsViewController { Title = "Current Conditions" };
			radarViewController = new RadarViewController { Title = "Radar" };
			tenDayForecastViewController = new TenDayForecastViewController { Title = "10 Day Forecast" };

			var tabs = new UIViewController[]
			{
				currentConditionsViewController, radarViewController, tenDayForecastViewController
			};

			ViewControllers = tabs;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


