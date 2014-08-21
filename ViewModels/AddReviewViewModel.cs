using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Media;
using SimpleDBPersistence.Service;
using HalalGuide.Domain;
using HalalGuide.Domain.Enum;
using HalalGuide.DAO;
using S3Storage.AWSException;
using System.Security.Cryptography;
using HalalGuide.Util;
using HalalGuide.Domain.Dawa;
using XUbertestersSDK;
using System.IO;
using System.Text;

namespace HalalGuide.ViewModels
{
	public class AddReviewViewModel : BaseViewModel
	{
		public AddReviewViewModel () : base ()
		{
		}

		public async Task<CreateReviewResult> CreateNewReview (Location location, int rating, string review)
		{

			Review r = new Review () {
				Id = KeyChain.GetFaceBookAccount ().Username + "-" + DateTime.Now.Ticks,
				LocationId = location.Id,
				Rating = rating,
			};

			string objectName = r.Id + ".txt";

			try {
				await S3.PutObject (Constants.S3Bucket, location.Id + "/" + objectName, Encoding.UTF8.GetBytes (review));
				await ReviewDAO.SaveOrReplace (r);

			} catch (AWSErrorException e) {
				XUbertesters.LogError ("ReviewViewModel: CouldNotUploadReviewToS3: " + e);
				ReviewDAO.Delete (r).RunSynchronously ();
				return CreateReviewResult.CouldNotUploadReviewToS3;

			} catch (SimpleDBPersistence.SimpleDB.Model.AWSException.AWSErrorException e) {

				XUbertesters.LogError ("ReviewViewModel: CouldNotCreateEntityInSimpleDB: " + e);
				S3.DeleteObject (Constants.S3Bucket, objectName).RunSynchronously ();
				ReviewDAO.Delete (r).RunSynchronously ();
				return CreateReviewResult.CouldNotCreateEntityInSimpleDB;
			}

			return CreateReviewResult.OK;
		}
	}
}

