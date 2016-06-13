using System;
using EpocratesTraining.Services;
using Plugin.Connectivity;
using UIKit;

namespace EpocratesTraining.iOS
{
	public partial class MainViewController : UITabBarController
	{
		UIViewController currentConditionsViewController, radarViewController, tenDayForecastViewController;

		public MainViewController() : base("MainViewController", null)
		{
			Title = "Awesome Weather App";
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

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			CrossConnectivity.Current.ConnectivityChanged += CrossConnectivity_Current_ConnectivityChanged;
		}

		void CrossConnectivity_Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{
			MessageService.ShowSimpleMessage(this, "Connection Event Detected", $"Connected: {e.IsConnected}");
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


