using System;
using System.IO;
using System.Net.Http;
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
			var imageService = ImageService.Instance;

			var forcastDays = await weatherService.Get10DayForecast();

			var imageClient = new HttpClient();

			foreach (var forcastDay in forcastDays)
			{
				var imageData = await imageService.GetImageData(forcastDay.IconUrl);

				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var localPath = Path.Combine(documentsPath, imageData.FileName);
				File.WriteAllBytes(localPath, imageData.Bytes);
			}

			DataTable.Source = new DataTableSource(forcastDays);
			DataTable.ReloadData();
		}
    }
}