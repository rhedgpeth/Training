using System.Net.Http;

namespace EpocratesTraining.Services
{
	public abstract partial class BaseService
	{
		HttpClient CreateNativeHttpClient()
		{
			return new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
		}
	}
}

