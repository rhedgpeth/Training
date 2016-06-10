using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EpocratesTraining.Models;

namespace EpocratesTraining.Services
{
	public class SongService : BaseService
	{
		static readonly SongService instance = new SongService();

		public static SongService Instance
		{
			get { return instance; }
		}

		SongService() 
		{
			client.BaseAddress = new Uri("https://dl.dropboxusercontent.com/s/cv75h76pv9su7l4/"); 
		}

		public Task<List<Song>> GetSmallSongsList()
		{
			return GetAsync<List<Song>>("songs-small.json");
		}

		public Task<List<Song>> GetLargeSongsList()
		{
			return GetAsync<List<Song>>("songs-large.json");
		}
	}
}

