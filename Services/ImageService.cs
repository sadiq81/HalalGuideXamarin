using System;
using System.Threading.Tasks;
using SimpleDBPersistence.Service;
using System.Collections.Generic;
using HalalGuide.Domain;
using System.IO;
using HalalGuide.Util;
using S3Storage.AWSException;
using XUbertestersSDK;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using HalalGuide.DAO;
using System.Linq;
using HalalGuide.Domain.Enum;
using SimpleDBPersistence.Domain;

namespace HalalGuide.Services
{
	public  class ImageService
	{
		private LocationPictureDAO _LocationPictureDAO = ServiceContainer.Resolve<LocationPictureDAO> ();
		private S3LocationPictureDAO _S3LocationPictureDAO = ServiceContainer.Resolve<S3LocationPictureDAO> ();
		private S3ProfilePictureDAO _S3ProfilePictureDAO = ServiceContainer.Resolve<S3ProfilePictureDAO> ();

		private  DatabaseWrapper _SQLiteConnection = ServiceContainer.Resolve<DatabaseWrapper> ();

		private  FileService _FileService = ServiceContainer.Resolve<FileService> ();

		private  PreferencesService _PreferencesService = ServiceContainer.Resolve<PreferencesService> ();


		#region ProfilePicture

		public async Task<CreateEntityResult> UploadLocationPicture (Location location, byte[] data)
		{
			if (data == null) {
				return CreateEntityResult.OK;
			}

			data = ImageResize.MaxResizeImage (data, 320, 320);

			string objectName = location.Submitter + "-" + DateTime.UtcNow.Ticks + ".jpg";

			LocationPicture picture = new LocationPicture () {
				Id = objectName,
				LocationId = location.Id,
				CreationStatus = CreationStatus.Approved
			};

			try {
				//TODO Error handling if upload dont succed
				await _S3LocationPictureDAO.StoreLocationPicture (picture, data);

				string path = _FileService.GetPathForLocationPicture (picture);

				_FileService.StoreFile (data, path);

				await _LocationPictureDAO.SaveOrReplace (picture);
				_SQLiteConnection.InsertOrReplace (picture);

			} catch (AWSErrorException e) {
				XUbertesters.LogError ("ImageService: CouldNotUploadPictureToS3: " + e + " Entity: " + picture);
				return CreateEntityResult.CouldNotUploadImageToS3;

			} catch (SimpleDBPersistence.SimpleDB.Model.AWSException.AWSErrorException e) {

				XUbertesters.LogError ("ImageService: CouldNotCreateEntityInSimpleDB: " + e + " Entity: " + picture);
				_S3LocationPictureDAO.DeleteLocationPicture (picture).RunSynchronously ();
				return CreateEntityResult.CouldNotCreateEntityInSimpleDB;
			}
			return CreateEntityResult.OK;
		}

		public async Task<CreateEntityResult> UploadProfilePicture (FacebookUser user, byte[] data)
		{
			try {
				await _S3ProfilePictureDAO.StoreProfilePicture (user, data);
			} catch (AWSErrorException e) {
				XUbertesters.LogInfo (String.Format ("ImageService: Could not  upload picture: {0}", e));
				return CreateEntityResult.CouldNotUploadImageToS3;
			} 
			return CreateEntityResult.OK;
		}

		public async Task<string> GetPathForFacebookPicture (string userId)
		{
			string filepath = _FileService.GetPathForUserPicture (userId);
			//Is picture stored locally?
			if (File.Exists (filepath)) {
				return filepath;
			} else {
				try {
					Stream image = await _S3ProfilePictureDAO.RetrieveProfilePicture (userId);
					_FileService.StoreFile (image, filepath);
					return filepath;
				} catch (AWSErrorException ex) {
					XUbertesters.LogError (string.Format ("ImageService: Error downloading image: {0} due to: {1}", userId, ex));
					return null;
				} catch (Exception ex) {
					XUbertesters.LogError (string.Format ("ImageService: Error downloading image: {0} due to: {1}", userId, ex));
					return null;
				}
			}
		}


		#endregion

		#region LocationPicture

		public List<LocationPicture> GetImagesForLocation (Location selectedLocation)
		{
			return _SQLiteConnection.Table<LocationPicture> ().Where (pic => pic.LocationId == selectedLocation.Id).ToList ();
		}

		public async Task<string> GetFirstImagePathForLocation (Location location)
		{
			LocationPicture picture = _SQLiteConnection.Query<LocationPicture> (string.Format ("SELECT * FROM {0} WHERE {1} = {2}", LocationPicture.TableIdentifier, LocationPicture.LocationIdIdentifier, location.Id)).FirstOrDefault ();

			if (picture == null) {
				//Test online for pictures
				SelectQuery<LocationPicture> query = new SelectQuery<LocationPicture> ();
				query.Equal (LocationPicture.LocationIdIdentifier, location.Id);
				List<LocationPicture> list = await _LocationPictureDAO.Select (query);

				if (list != null && list.Count > 0) {
				
					picture = list.First ();
					list.ForEach (_SQLiteConnection.InsertOrReplace);
				} else {
					return null;
				}
			} 

			var path = await GetPicturePath (picture);
			return path;

		}

		public async Task<string> GetPicturePath (LocationPicture lp)
		{
			string filepath = _FileService.GetPathForLocationPicture (lp); 
			//Is picture stored locally?
			if (File.Exists (filepath)) {
				return filepath;
			} else {
				try {
					Stream image = await _S3LocationPictureDAO.RetrieveLocationPicture (lp);
					_FileService.StoreFile (image, filepath);
					return filepath;
				} catch (AWSErrorException ex) {
					XUbertesters.LogError (string.Format ("ImageService: Error downloading image: {0} due to: {1}", lp.Id, ex));
					return null;
				} catch (Exception ex) {
					XUbertesters.LogError (string.Format ("ImageService: Error downloading image: {0} due to: {1}", lp.Id, ex));
					return null;
				}
			}
		}

		public async Task<List<LocationPicture>> GetLatestImagesForLocation (Location location)
		{
			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat);
			string lastUpdated = _PreferencesService.GetString (Constants.LocationPictureLastUpdated + "-" + location.Id) ?? DateTime.MinValue.ToString (Constants.DateFormat);

			SelectQuery<LocationPicture> query = new SelectQuery<LocationPicture> ()
			.Equal (LocationPicture.LocationIdIdentifier, location.Id)
				.GreatherThanOrEqual (Entity.UpdatedIdentifier, lastUpdated)
				.NotNull (Entity.UpdatedIdentifier)
				.SetSortOrder (Entity.UpdatedIdentifier);

			List<LocationPicture> list = await _LocationPictureDAO.Select (query);

			if (list != null && list.Count > 0) {
				list.ForEach (_SQLiteConnection.InsertOrReplace);
			}
			_PreferencesService.StoreString (Constants.LocationPictureLastUpdated + "-" + location.Id, updatedTime);

			return list;
		}

		#endregion
	}
}

