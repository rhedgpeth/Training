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

		protected Task<T> GetAsync<T>(string requestUri, Func<JToken, JToken> filter = null)
		{
			return SendAsync<T>(RequestType.Get, requestUri, filter);
		}

		async Task<T> SendAsync<T>(RequestType requestType, string requestUri, Func<JToken, JToken> filter, string jsonRequest = null)
		{
			//HttpContent content = !string.IsNullOrEmpty(jsonRequest) ? new StringContent(jsonRequest, Encoding.UTF8, "application/json") : null;
	
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
					return Deserialize<T>(json, filter);
			}

			return default(T);
		} 

		protected T Deserialize<T>(string json, Func<JToken, JToken> filter)
		{
			var token = JToken.Parse(json);

			token = filter?.Invoke(token) ?? token;

			var obj = token.ToObject<T>();

			return obj;
		}
	}
}

