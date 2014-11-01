using HalalGuide.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace HalalGuide.ViewModels
{
	public sealed class SingleDiningViewModel : BaseViewModel
	{
		public event EventHandler RefreshedReviewCompletedEvent = delegate { };

		public event EventHandler RefreshedPicturesCompletedEvent = delegate { };

		public List<LocationPicture> Pictures { get; set; }

		public List<Review> Reviews { get; set; }

		public SingleDiningViewModel () : base ()
		{
			Pictures = new List<LocationPicture> ();
			Reviews = new List<Review> ();
		}

		public double AverageReviewScore ()
		{
			if (Reviews != null && Reviews.Count > 0) {
				return Reviews.Average (r => r.rating);
			} else {
				return 0;
			}
		}
	}
}

