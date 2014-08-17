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
	public class ReviewViewModel : BaseViewModel
	{


		public ReviewViewModel () : base ()
		{
		}

		public async Task<CreateReviewResult> CreateNewReview (Location location, int rating, string review)
		{

			Review r = new Review () {
				LocationId = location.Id,
				Rating = rating,
				Submitter = KeyChain.GetFaceBookAccount ().Username
			};

			string objectName = location.Name + "/" + r.Submitter + "-" + DateTime.Now.Ticks + ".txt";
			r.Id = objectName;

			try {
				await S3.PutObject (Constants.S3Bucket, objectName, Encoding.UTF8.GetBytes (review));
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

