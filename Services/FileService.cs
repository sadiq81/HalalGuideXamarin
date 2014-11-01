using System;
using HalalGuide.Domain;
using System.IO;
using HalalGuide.Util;

namespace HalalGuide.Services
{
	public  class FileService
	{
		public  void StoreFile (Stream data, string path)
		{
			StoreFile (StreamUtil.ReadToEnd (data), path);
		}

		public  void StoreFile (byte[] data, string path)
		{
			string directoryPath = Path.GetDirectoryName (path);

			if (!Directory.Exists (directoryPath)) {
				Directory.CreateDirectory (directoryPath);
			}

			File.WriteAllBytes (path, data);
		}

		public  byte[] RetriveFile (string path)
		{
			if (File.Exists (path)) {
				return StreamUtil.ReadToEnd (File.OpenRead (path));
			} else {
				return null;
			}
		}

		public string GetPathForUserPicture (string user)
		{
			string path;
			#if __IOS__
			path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library/Caches/Facebook/" + user + ".jpg");
			#elif __ANDROID__
			path= "";
			#else
			throw new ArgumentException ("Platform not recognized");
			#endif
			return path;
		}

		public  string GetPathForLocationPicture (LocationPicture picture)
		{
			string path;
			#if __IOS__
			path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library/Caches/" + picture.locationId + "/" + picture.id);
			#elif __ANDROID__
			path= "";
			#else
			throw new ArgumentException ("Platform not recognized");
			#endif
			return path;
		}

		public  string GetPathForReview (Review review)
		{
			string path;
			#if __IOS__
			path = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library/Caches/" + review.locationId + "/" + review.id);
			#elif __ANDROID__
			path= "";
			#else
			throw new ArgumentException ("Platform not recognized");
			#endif
			return path;
		}
	}
}

