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

		public override void RefreshCache ()
		{
			//throw new NotImplementedException ();
		}

		public async Task<CreateEntityResult> CreateNewReview (Location location, int rating, string reviewText)
		{

			Review review = new Review () {
				Id = _KeyChain.GetFaceBookAccount ().Username + "-" + DateTime.Now.Ticks,
				LocationId = location.Id,
				Rating = rating,
			};

			CreateEntityResult result = await _ReviewService.SaveReview (review, Encoding.UTF8.GetBytes (reviewText));

			return result;
		}
	}
}

