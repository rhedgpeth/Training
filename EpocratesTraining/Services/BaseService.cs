using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;

namespace EpocratesTraining.Services
{
	public abstract partial class BaseService
	{
		enum RequestType { Delete, Get, Post, Put }

		protected HttpClient client;

		protected BaseService()
		{
			client = CreateNativeHttpClient();
		}

		protected Task<T> GetAsync<T>(string requestUri)
		{
			return SendAsync<T>(RequestType.Get, requestUri);
		}

		async Task<T> SendAsync<T>(RequestType requestType, string requestUri, string jsonRequest = null)
		{
			HttpContent content = null;

			if (jsonRequest != null)
				content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

			Task<HttpResponseMessage> httpTask;

			switch (requestType)
			{
				case RequestType.Get:
					httpTask = client.GetAsync(requestUri);
					break;
				/*
				case RequestType.Delete:
					httpTask = client.DeleteAsync(requestUri);
					break;
				case RequestType.Put:
					httpTask = client.PutAsync(requestUri, content);
					break;
				case RequestType.Post:
					httpTask = client.PostAsync(requestUri, content);
					break;
				*/
				default:
					throw new Exception("Not a valid request type");
			}

			if (CrossConnectivity.Current.IsConnected)
			{
				var response = await httpTask.ConfigureAwait(false);

				response.EnsureSuccessStatusCode();

				string json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (!string.IsNullOrEmpty(json))
					return JToken.Parse(json).ToObject<T>(); ;
			}

			return default(T);
		} 
	}
}

