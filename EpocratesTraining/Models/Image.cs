using System;

namespace EpocratesTraining.Models
{
	public class ImageData
	{
		public ImageData(string fileName, byte[] bytes)
		{
			FileName = fileName;
			Bytes = bytes;
		}

		public string FileName { get; private set; }

		public byte[] Bytes { get; private set; }
	}
}

