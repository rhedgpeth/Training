using System;
using System.Linq;

using Foundation;
using UIKit;

using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.iOS
{
    public partial class TenDayViewController : UIViewController
    {
		TenDayTableViewSource dataSource;

        public TenDayViewController (IntPtr handle) : base (handle)
        { }

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var loadingOverlay = new LoadingOverlay(UIScreen.MainScreen.Bounds);
			Add(loadingOverlay);

			var weatherService = WeatherService.Instance;

			var forcastDays = await weatherService.Get10DayForecast();

			dataSource = new TenDayTableViewSource(forcastDays);

			TenDayTableView.Source = dataSource;
			TenDayTableView.ReloadData();

			loadingOverlay.Hide();
		}

		void ReloadRow(int row)
		{
			NSIndexPath[] rowsToReload = new NSIndexPath[] 
			{
				NSIndexPath.FromRowSection(row, 0) // points to second row in the first section of the model
			};

			TenDayTableView.ReloadRows(rowsToReload, UITableViewRowAnimation.None);
		}
    }
}