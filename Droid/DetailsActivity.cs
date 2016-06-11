
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EpocratesTraining.Droid
{
	[Activity(Label = "DetailsActivity")]
	public class DetailsActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Details);

			var title = Intent.Extras.GetString("forecast_day_title") ?? string.Empty;
			var description = Intent.Extras.GetString("forecast_day_description") ?? string.Empty;

			FindViewById<TextView>(Resource.Id.detailsTitleText).Text = title;
			FindViewById<TextView>(Resource.Id.detailsDescriptionText).Text = description;
		}
	}
}

