using System;
using System.Threading.Tasks;
using HalalGuide.Domain.Enums;
using HalalGuide.Domain;
using HalalGuide.DAO;
using HalalGuide.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HalalGuide.Services
{
	public class ReviewService
	{
		private  DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		private  PreferencesService preferences { get { return ServiceContainer.Resolve<PreferencesService> (); } }

		private ReviewDAO reviewDAO { get { return ServiceContainer.Resolve<ReviewDAO> (); } }

		private  FileService fileService { get { return ServiceContainer.Resolve<FileService> (); } }

		public ReviewService ()
		{
		}

		public async Task SaveReview (Review review)
		{
			await reviewDAO.SaveOrReplace (review);
		}

		public async Task<List<Review>> GetReviewsForLocation (Location selectedLocation)
		{
			return await reviewDAO.Where (review => review.locationId == selectedLocation.id);
		}
	}
}

