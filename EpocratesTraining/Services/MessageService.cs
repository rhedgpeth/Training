#if __ANDROID__
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
#else
using UIKit;
#endif

namespace EpocratesTraining.Services
{
	public static class MessageService
	{
		#if __ANDROID__

				public static void ShowSimpleMessage(this Activity activity, string title, string message)
				{
					var alert = new AlertDialog.Builder(activity);
					alert.SetTitle("Songs Received");
					alert.SetPositiveButton("OK", (senderAlert, args) => { });
					alert.SetMessage(message);

					activity.RunOnUiThread(() =>
					{
						alert.Show();
					});
				}

		#else

				public static void ShowSimpleMessage(this UIViewController viewController, string title, string message)
				{
					viewController.InvokeOnMainThread(() =>
					{
						var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
						alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
						viewController.PresentViewController(alert, true, null);
					});
				}

		#endif
	}
}
