using System;
using System.Threading.Tasks;
using HalalGuide.Domain;
using HalalGuide.Domain.Enum;

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
				Id = _KeyChain.GetFaceBookAccount ().Username + "-" + DateTime.Now.Ticks + ".txt",
				LocationId = location.Id,
				Rating = rating,
				CreationStatus = CreationStatus.Approved
			};

			CreateEntityResult result = await _ReviewService.SaveReview (review, reviewText);

			return result;
		}
	}
}

