using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

using EpocratesTraining.Models;
using FFImageLoading;

namespace EpocratesTraining.iOS
{
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

