
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace EpocratesTraining.Droid
{
	public class RadarFragment : Fragment
	{
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = inflater.Inflate(Resource.Layout.RadarFragment, container, false);

			var webView = view.FindViewById<WebView>(Resource.Id.webView);
			webView.Settings.JavaScriptEnabled = true;
			webView.SetWebViewClient(new CustomWebViewClient());
			webView.LoadUrl("http://mobile.weather.gov/index.php?lat=37.77492773500046&lon=-122.41941932299972#radar");

			return view;
		}
	}

	public class CustomWebViewClient : WebViewClient
	{
		public override bool ShouldOverrideUrlLoading(WebView view, string url)
		{
			if (!string.IsNullOrEmpty(url))
			{
				view.LoadUrl(url);
				return true;
			}

			return false;
		}
	}
}

