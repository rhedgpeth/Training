using System;
using System.Collections.Generic;
using CoreGraphics;
using EpocratesTraining.Models;
using EpocratesTraining.Services;
using FFImageLoading;
using Foundation;
using UIKit;

namespace EpocratesTraining.iOS
{
	public partial class TenDayForecastViewController : UIViewController
	{
		public TenDayForecastViewController() : base("TenDayForecastViewController", null)
		{
		}

		public async override void ViewDidLoad()
		{
			Title = "10 Day Forecast";

			base.ViewDidLoad();

			var loadingOverlay = new LoadingOverlay(this.View.Bounds);
			Add(loadingOverlay);

			//var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;

			//tableView = new UITableView(new CGRect(0, statusBarHeight, View.Frame.Width, View.Frame.Height));
			//tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "MyCell");
			//tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			var forecastDays = await WeatherService.Instance.Get10DayForecast();

			if (forecastDays != null)
			{
				tableView.Source = new TenDayTableViewSource(forecastDays);
				tableView.ReloadData();
			}

			loadingOverlay.Hide();
		}

		/*
		void ReloadRow(int row)
		{
			NSIndexPath[] rowsToReload = new NSIndexPath[]
			{
				NSIndexPath.FromRowSection(row, 0) // points to second row in the first section of the model
			};

			TenDayTableView.ReloadRows(rowsToReload, UITableViewRowAnimation.None);
		}*/
	}

	public class TenDayTableViewSource : UITableViewSource
	{
		List<ForecastDay> items;
		string CellIdentifier = "MyCell";

		public TenDayTableViewSource(List<ForecastDay> items)
		{
			this.items = items;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return items.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

			// If there are no cells to reuse, create a new one
			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellIdentifier);

			var item = items[indexPath.Row];

			if (item != null)
			{
				cell.ImageView.Tag = indexPath.Row;

				ImageService.Instance.LoadUrl(item.IconUrl)
							.Into(cell.ImageView as UIImageView);

				cell.TextLabel.Text = item.Title;
				cell.DetailTextLabel.Text = item.Description;
			}

			return cell;
		}
	}
}


