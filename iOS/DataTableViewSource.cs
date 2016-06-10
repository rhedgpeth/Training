using System;
using System.Linq;
using System.Collections.Generic;
using Foundation;
using UIKit;

using EpocratesTraining.Models;

namespace EpocratesTraining.iOS
{
	public class DataTableSource : UITableViewSource
	{
		ForecastDay[] tableItems;
		string CellIdentifier = "TableCell";

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
			var item = tableItems[indexPath.Row];

			//---- if there are no cells to reuse, create a new one
			if (cell == null)
			{ cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier); }

			cell.TextLabel.Text = item.Title;

			return cell;
		}
	}
}

