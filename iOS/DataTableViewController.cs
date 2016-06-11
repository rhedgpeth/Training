using System;
using System.Linq;

using Foundation;
using UIKit;

using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.iOS
{
    public partial class DataTableViewController : UIViewController
    {
		DataTableSource dataSource;

        public DataTableViewController (IntPtr handle) : base (handle)
        {
        }

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var weatherService = WeatherService.Instance;

			var forcastDays = await weatherService.Get10DayForecast();

			dataSource = new DataTableSource(forcastDays);
			dataSource.ImageLoaded = ReloadRow;

			DataTable.Source = dataSource;
			DataTable.ReloadData();
		}

		void ReloadRow(int row)
		{
			NSIndexPath[] rowsToReload = new NSIndexPath[] 
			{
				NSIndexPath.FromRowSection(row, 0) // points to second row in the first section of the model
			};

			DataTable.ReloadRows(rowsToReload, UITableViewRowAnimation.None);
		}
    }
}