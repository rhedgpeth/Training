using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

using EpocratesTraining.Models;
using EpocratesTraining.Services;

namespace EpocratesTraining.Droid
{
	public class WeatherAdapter : BaseAdapter<ForecastDay>
	{
		Context context;
		LayoutInflater inflator;
		List<ForecastDay> items;

		public override ForecastDay this[int position] => items[position];
		public override int Count => items.Count;
		public override long GetItemId(int position) => position;

		public WeatherAdapter(Context context, List<ForecastDay> items)
		{
			this.context = context;
			inflator = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
			this.items = items;
		}

		// TODO: This is just the first pass - second lesson involves using ViewHolder
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			convertView = convertView ?? inflator.Inflate(Resource.Layout.WeatherRowItem, parent, false);

			var item = items[position];

			if (item != null)
			{
				var iconImage = convertView.FindViewById<ImageView>(Resource.Id.iconImage);
				var titleText = convertView.FindViewById<TextView>(Resource.Id.titleText);
				var descriptionText = convertView.FindViewById<TextView>(Resource.Id.descriptionText);

				titleText.Text = item.Title;
				descriptionText.Text = item.Description;

				Task.Run(async () =>
				{
					var result = await ImageService.Instance.GetImageData(item.IconUrl).ConfigureAwait(false);

					if (result != null && result.Bytes != null)
					{
						var bitmap = await BitmapFactory.DecodeByteArrayAsync(result.Bytes, 0, result.Bytes.Length);
						var drawable = new BitmapDrawable(context.Resources, bitmap);
						iconImage.SetImageDrawable(drawable);
					}
				});
			}

			return convertView;
		}
	}
}

