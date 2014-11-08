using HalalGuide.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using MonoTouch.MessageUI;
using HalalGuide.Util;
using System.Threading.Tasks;

namespace HalalGuide.ViewModels
{
	public sealed class SingleDiningViewModel : BaseViewModel
	{
		public event EventHandler refreshedLocationData = delegate { };

		public List<LocationPicture> pictures { get; set; }

		public List<Review> reviews { get; set; }

		public SingleDiningViewModel () : base ()
		{
		}

		public override void RefreshCache ()
		{
			pictures = imageService.RetrieveAllPicturesForLocation (selectedLocation);
			reviews = reviewService.RetrieveAllReviewsForLocation (selectedLocation);
		}

		public double AverageReviewScore ()
		{
			if (reviews != null && reviews.Count > 0) {
				return reviews.Average (r => r.rating);
			} else {
				return 0;
			}
		}

		public async override Task RefreshLocationData ()
		{
			OnNetwork (true);
			await imageService.RetrieveLatestLocationPicturesForLocation (selectedLocation);
			await reviewService.RetrieveLatestReviewsForLocation (selectedLocation);
			OnNetwork (false);
			RefreshCache ();
			refreshedLocationData (this, EventArgs.Empty);
		}

		#if __IOS__
		public MFMailComposeViewController reportIncorrectInformation ()
		{
			MFMailComposeViewController mailController = new MFMailComposeViewController ();
			mailController.SetToRecipients (new string[]{ "tommy@eazyit.dk" });
			mailController.SetSubject (Localization.GetLocalizedValue (Feedback.Error) + " - " + ": " + selectedLocation.id);
			mailController.SetMessageBody (Localization.GetLocalizedValue (Feedback.ErrorTemplate), false);
			mailController.Finished += (  s, args) => {
				args.Controller.DismissViewController (true, null);
			};
			return mailController;
		}
		#elif __ANDROID__
		


#else
		#endif


	}
}

