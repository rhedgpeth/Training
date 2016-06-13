using System;
using System.Threading.Tasks;
using UIKit;
using EpocratesTraining.Services;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;
using FFImageLoading;

namespace EpocratesTraining.iOS
{
	public abstract class BaseViewController : UIViewController
	{

	}

	public partial class CurrentConditionsViewController : UIViewController
	{
		UITableView tableView;

		public CurrentConditionsViewController() : base("CurrentConditionsViewController", null)
		{ }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height;

			tableView = new UITableView(new CGRect(0, statusBarHeight + 100, View.Frame.Width, View.Frame.Height));
			tableView.RegisterClassForCellReuse(typeof(UITableViewCell), "MyCell");
			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			View.AddSubview(tableView);

			var loadingOverlay = new LoadingOverlay(this.View.Bounds);
			Add(loadingOverlay);

			Task.Run(async () =>
			{
				var currentConditions = await WeatherService.Instance.GetCurrentConditions();

				if (currentConditions != null)
				{
					InvokeOnMainThread(() =>
					{
						var imageView = new UIImageView(new CGRect((View.Frame.Width / 2) - 24, 100, 48, 48));

						//ImageService.Instance.LoadUrl(currentConditions.IconUrl).Into(imageView);

						imageView.Image = FromUrl(currentConditions.IconUrl);

						View.AddSubview(imageView);

						var items = new List<Tuple<string, string>>();
						items.Add(new Tuple<string, string>("Current conditions: ", currentConditions.Weather));
						items.Add(new Tuple<string, string>("Temperature: ", currentConditions.Temperature));
						items.Add(new Tuple<string, string>("Feels like: ", currentConditions.FeelsLike));
						items.Add(new Tuple<string, string>("Wind: ", currentConditions.WindDescription));
						items.Add(new Tuple<string, string>("Wind Chill: ", currentConditions.Windchill));
						items.Add(new Tuple<string, string>("Humidity: ", currentConditions.RelativeHumidity));
						items.Add(new Tuple<string, string>("Dewpoint: ", currentConditions.Dewpoint));
						items.Add(new Tuple<string, string>("Heat Index: ", currentConditions.HeatIndex));

						tableView.Source = new CurrentConditionsTableSource(items);
						tableView.ReloadData();

						loadingOverlay.Hide();
					});
				}
			});
		}

		UIImage FromUrl(string uri)
		{
			using (var url = new NSUrl(uri))
			using (var data = NSData.FromUrl(url))
				return UIImage.LoadFromData(data);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}

	public class CurrentConditionsTableSource : UITableViewSource
	{
		List<Tuple<string, string>> items;

		public CurrentConditionsTableSource(List<Tuple<string, string>> items)
		{
			this.items = items;
		}

		//UITableViewDataSource basic stuff
		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return items.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(MultiColumnCell.ID) as MultiColumnCell;

			if (cell == null)
				cell = new MultiColumnCell();
			
			cell.LeftValue.Text = items[indexPath.Row].Item1;
			cell.RightValue.Text = items[indexPath.Row].Item2;

			return cell;
		}
	}

	public class MultiColumnCell : UITableViewCell
	{
		public static readonly string ID = "MultiColumnCell";

		public UILabel LeftValue { get; set; }
		public UILabel RightValue { get; set; }

		public MultiColumnCell()
		{
			var columnSize = UIScreen.MainScreen.Bounds.Width / 2;

			LeftValue = new UILabel(new CGRect(0, 0, columnSize - 8, 50));
			LeftValue.TextAlignment = UITextAlignment.Right;
			LeftValue.Font = UIFont.BoldSystemFontOfSize(12f);

			RightValue = new UILabel(new CGRect(columnSize, 0, columnSize, 50));

			AddSubview(LeftValue);
			AddSubview(RightValue);
		}
	}
}


