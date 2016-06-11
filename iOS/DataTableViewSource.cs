﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foundation;
using UIKit;

using EpocratesTraining.Models;
using FFImageLoading;
//using EpocratesTraining.Services;

namespace EpocratesTraining.iOS
{
	public class DataCell : UITableViewCell
	{ }

	public class DataTableSource : UITableViewSource
	{
		public Action<int> ImageLoaded { get; set; }

		List<ForecastDay> items;
		string CellIdentifier = "MyCell";

		public DataTableSource(List<ForecastDay> items)
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
				            //.Success(() => { ImageLoaded(indexPath.Row); })
				            .Into(cell.ImageView as UIImageView);
				
				cell.TextLabel.Text = item.Title;
				cell.DetailTextLabel.Text = item.Description;
			}
		
			return cell;
		}
	}
}

