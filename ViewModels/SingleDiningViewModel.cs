using HalalGuide.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using HalalGuide.Domain.Enum;
using System.Linq;
using System;
using HalalGuide.Util;
using System.Text;
using System.Security.Cryptography;

namespace HalalGuide.ViewModels
{
	public sealed class SingleDiningViewModel : BaseViewModel
	{
		public event EventHandler RefreshedReviewCompletedEvent = delegate { };

		public event EventHandler RefreshedPicturesCompletedEvent = delegate { };

		private List<LocationPicture> Pictures { get; set; }

		private List<Review> Reviews { get; set; }

		public SingleDiningViewModel () : base ()
		{
			Pictures = new List<LocationPicture> ();
			Reviews = new List<Review> ();

			RefreshCache ();
		}

		public override void RefreshCache ()
		{
			if (SelectedLocation != null) {
				Pictures = _ImageService.GetImagesForLocation (SelectedLocation);
				Reviews = _ReviewService.GetReviewsForLocation (SelectedLocation);
			}
		}

		public string Categories ()
		{
			if (SelectedLocation != null && SelectedLocation.Categories != null) {
				StringBuilder translated = new StringBuilder ();
				string[] categories = SelectedLocation.Categories.Split (new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string cat in categories) {
					translated.Append (string.Format ("{0}, ", Localization.GetLocalizedValue (cat.Trim ())));
				}
				return translated.ToString ().RemoveCharFromEnd (2);
			} else {
				return "";
			}
		}

		public int PicturesItems ()
		{
			return Pictures.Count;
		}

		public int ReviewsInSection ()
		{
			return Reviews.Count;
		}

		public LocationPicture  GetLocationPictureAtRow (int row)
		{
			return Pictures [row];
		}

		public async Task<string> GetLocationPicturePath (LocationPicture locationPicture)
		{
			return await _ImageService.GetPicturePath (locationPicture);
		}

		public  Review GetReviewAtRow (int row)
		{
			return Reviews [row];
		}

		public async Task<string> GetReviewText (Review review)
		{
			return await _ReviewService.GetReviewText (review);
		}

		public async Task RefreshDataForLocation ()
		{

			List<LocationPicture> pictures = await _ImageService.GetLatestImagesForLocation (SelectedLocation);

			List<Review> reviews = await _ReviewService.GetLatestReviews (SelectedLocation);

			if (pictures != null && pictures.Count > 0) {
				RefreshedPicturesCompletedEvent (this, EventArgs.Empty);
				RefreshCache ();
			}

			if (reviews != null && reviews.Count > 0) {
				RefreshedReviewCompletedEvent (this, EventArgs.Empty);
				RefreshCache ();
			}

		}

		public int NumberOfReviews ()
		{
			return Reviews.Count;
		}

		public double AverageReviewScore ()
		{
			if (Reviews != null && Reviews.Count > 0) {
				return Reviews.Average (r => r.Rating);
			} else {
				return 0;
			}
		}


		public async Task<string> NameOfSubmitter (string id)
		{

			return await _FacebookService.GetFacebookUserName (id);
		}

		public async Task<string> GetProfilePicture (string id)
		{
			return await _ImageService.GetPathForFacebookPicture (id);
		}

		public async Task<CreateEntityResult> AddLocationPicture (byte[] data)
		{
			return await _ImageService.UploadLocationPicture (SelectedLocation, data);
		}
	}
}

