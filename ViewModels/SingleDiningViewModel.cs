using HalalGuide.Domain;
using System.Collections.Generic;
using System.Linq;
using System;
using MonoTouch.MessageUI;
using HalalGuide.Util;

namespace HalalGuide.ViewModels
{
	public sealed class SingleDiningViewModel : BaseViewModel
	{
		public event EventHandler RefreshedReviewCompletedEvent = delegate { };

		public event EventHandler RefreshedPicturesCompletedEvent = delegate { };

		public List<LocationPicture> pictures { get; set; }

		public List<Review> reviews { get; set; }

		public SingleDiningViewModel () : base ()
		{
		}

		public override void RefreshCache ()
		{
			pictures = imageService.RetrieveAllPicturesForLocation (selectedLocation);
		}

		public double AverageReviewScore ()
		{
			if (reviews != null && reviews.Count > 0) {
				return reviews.Average (r => r.rating);
			} else {
				return 0;
			}
		}

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
	}
}

