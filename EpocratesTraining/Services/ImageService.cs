using System;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using EpocratesTraining.Models;

namespace EpocratesTraining.Services
{
	public class ImageService : BaseService
	{
		//readonly HttpClient _client = new HttpClient();

		static readonly ImageService instance = new ImageService();

		public static ImageService Instance
		{
			get { return instance; }
		}

		public ImageService()
		{ }

		public async Task<ImageData> GetImageData(string url)
		{
			var uri = new Uri(url);
			var result = await client.GetAsync(uri).ConfigureAwait(false);
			var bytes = await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

			return new ImageData(uri.Segments.Last(), bytes);
		}
	}
}

