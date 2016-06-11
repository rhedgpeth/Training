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
        public DataTableViewController (IntPtr handle) : base (handle)
        {
        }

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var weatherService = WeatherService.Instance;

			var forcastDays = await weatherService.Get10DayForecast();

			DataTable.Source = new DataTableSource(forcastDays);
			DataTable.ReloadData();
		}
    }
}