using System;
using System.Threading.Tasks;
using HalalGuide.Domain;
using HalalGuide.Domain.Enums;

namespace HalalGuide.ViewModels
{
	public class AddReviewViewModel : BaseViewModel
	{
		public AddReviewViewModel () : base ()
		{
		}

		public async Task CreateNewReview (Location location, int rating, string reviewText)
		{

			Review review = new Review () {
				locationId = location.id,
				rating = rating,
				review = reviewText,
				creationStatus = CreationStatus.Approved
			};
			await reviewService.SaveReview (review);
		}
	}
}

