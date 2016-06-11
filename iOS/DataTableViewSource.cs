using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Foundation;
using UIKit;

using EpocratesTraining.Models;

namespace EpocratesTraining.iOS
{
	public class DataCell : UITableViewCell
	{
	}

	public class DataTableSource : UITableViewSource
	{
		ForecastDay[] tableItems;
		string CellIdentifier = "TheItem";

		public DataTableSource(IEnumerable<ForecastDay> items)
		{
			tableItems = items.ToArray();
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return tableItems.Length;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
			}

			var item = tableItems[indexPath.Row];

			var iconUri = new Uri(item.IconUrl);
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var localPath = Path.Combine(documentsPath, iconUri.Segments.Last());
			var image = UIImage.FromFile(localPath);

			cell.ImageView.Image = image;

			cell.TextLabel.Text = String.Format("{0}", item.Title);

			return cell;
		}
	}
}

