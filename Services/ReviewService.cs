using System;
using S3Storage.S3;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using HalalGuide.Domain.Enum;
using HalalGuide.Domain;
using System.Security.Cryptography;
using XUbertestersSDK;
using HalalGuide.DAO;
using HalalGuide.Util;
using System.Text;
using S3Storage.AWSException;
using System.Collections.Generic;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using SimpleDBPersistence.Domain;
using System.IO;
using S3Storage.Response;
using System.Linq;

namespace HalalGuide.Services
{
	public class ReviewService
	{
		private   S3ClientCore _S3 = ServiceContainer.Resolve<S3ClientCore> ();

		private  DatabaseWrapper _SQLiteConnection = ServiceContainer.Resolve<DatabaseWrapper> ();

		private  PreferencesService _PreferencesService = ServiceContainer.Resolve<PreferencesService> ();

		private ReviewDAO _ReviewDAO = ServiceContainer.Resolve <ReviewDAO> ();

		private  FileService _FileService = ServiceContainer.Resolve<FileService> ();

		public ReviewService ()
		{
		}

		public async Task<CreateEntityResult> SaveReview (Review review, byte[] data)
		{
			string objectName = review.Id + ".txt";

			try {
				await _S3.PutObject (Constants.S3Bucket, review.LocationId + "/" + objectName, data);
				await _ReviewDAO.SaveOrReplace (review);

			} catch (AWSErrorException e) {
				XUbertesters.LogError ("ReviewViewModel: CouldNotUploadReviewToS3: " + e);
				_ReviewDAO.Delete (review).RunSynchronously ();
				return CreateEntityResult.CouldNotUploadReviewToS3;

			} catch (SimpleDBPersistence.SimpleDB.Model.AWSException.AWSErrorException e) {

				XUbertesters.LogError ("ReviewViewModel: CouldNotCreateEntityInSimpleDB: " + e);
				_S3.DeleteObject (Constants.S3Bucket, objectName).RunSynchronously ();
				_ReviewDAO.Delete (review).RunSynchronously ();
				return CreateEntityResult.CouldNotCreateEntityInSimpleDB;
			}

			return CreateEntityResult.OK;
		}

		public async Task<List<Review>> GetLatestReviews (Location location)
		{
			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat);
			string lastUpdated = _PreferencesService.GetString (Constants.ReviewLastUpdated + " - " + location.Id) ?? DateTime.MinValue.ToString (Constants.DateFormat);

			SelectQuery<Review> reviewQuery = new SelectQuery<Review> ()
				.Equal (Review.CreationStatusIdentifier, CreationStatus.Approved.ToString ())
				.Equal (Review.LocationIdIdentifier, location.Id)
			                                  	.GreatherThanOrEqual (Entity.UpdatedIdentifier, lastUpdated)
				.NotNull (Entity.UpdatedIdentifier)
				.SetSortOrder (Entity.UpdatedIdentifier);

			List<Review> reviews = await _ReviewDAO.Select (reviewQuery);

			if (reviews != null && reviews.Count > 0) {
				reviews.ForEach (_SQLiteConnection.InsertOrReplace);
			}

			_PreferencesService.StoreString (Constants.ReviewLastUpdated + " - " + location.Id, updatedTime);

			return reviews;
		}

		public async Task<string> GetReviewText (Review review)
		{
			string filepath = _FileService.GetPathForReview (review);
			//Is review stored locally?
			if (File.Exists (filepath)) {
				return File.ReadAllText (filepath);
			} else {
				try {
					Stream text = await GetReviewFromServer (review);
					_FileService.StoreFile (text, filepath);
					return File.ReadAllText (filepath);
				} catch (AWSErrorException ex) {
					XUbertesters.LogError (string.Format ("ReviewService: Error downloading review: {0} due to: {1}", review.Id, ex));
					return null;
				} catch (Exception ex) {
					XUbertesters.LogError (string.Format ("ReviewService: Error downloading review: {0} due to: {1}", review.Id, ex));
					return null;
				}
			}
		}

		public List<Review> GetReviewsForLocation (Location selectedLocation)
		{
			return _SQLiteConnection.Table<Review> ().Where (rev => rev.LocationId == selectedLocation.Id).ToList ();
		}

		//TODO Create S3 DAOs
		private async Task<Stream> GetReviewFromServer (Review review)
		{
			GetObjectResult result = await _S3.GetObject (Constants.S3Bucket, review.LocationId + "/" + review.Id + ".txt");
			return result.Stream;
		}
	}
}

