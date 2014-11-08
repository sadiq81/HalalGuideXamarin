using System;
using System.Threading.Tasks;
using HalalGuide.Domain.Enums;
using HalalGuide.Domain;
using HalalGuide.DAO;
using HalalGuide.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace HalalGuide.Services
{
	public class ReviewService
	{
		private  DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		private  PreferencesService preferences { get { return ServiceContainer.Resolve<PreferencesService> (); } }

		private ReviewDAO reviewDAO { get { return ServiceContainer.Resolve<ReviewDAO> (); } }

		private KeyChainService keychain { get { return ServiceContainer.Resolve<KeyChainService> (); } }

		public ReviewService ()
		{
		}

		public async Task SaveReview (Review review)
		{
			await reviewDAO.SaveOrReplace (review);
		}

		public async Task RetrieveLatestReviews ()
		{
			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat, CultureInfo.InvariantCulture);

			string lastUpdatedString = preferences.GetString (Constants.LocationReviewLastUpdated);

			DateTime updatedLast = DateTime.ParseExact (lastUpdatedString, Constants.DateFormat, CultureInfo.InvariantCulture);

			await reviewDAO.Where (rev => rev.updatedAt > updatedLast);

			preferences.StoreString (Constants.LocationReviewLastUpdated, updatedTime);
		}

		public async Task<List<Review>> RetrieveLatestReviewsForLocation (Location selectedLocation)
		{
			string lastUpdatedString = preferences.GetString (Constants.LocationReviewLastUpdated);

			DateTime updatedLast = DateTime.ParseExact (lastUpdatedString, Constants.DateFormat, CultureInfo.InvariantCulture);

			return await reviewDAO.Where (rev => rev.locationId == selectedLocation.id && rev.updatedAt > updatedLast);
		}

		public List<Review> RetrieveAllReviewsForLocation (Location selectedLocation)
		{
			List<Review> reviews = database.Table<Review> ().Where (rev => rev.deleted == false && rev.locationId == selectedLocation.id).ToList ();
			return reviews;
		}
	}
}

