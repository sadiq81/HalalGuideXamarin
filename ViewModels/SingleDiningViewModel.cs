using HalalGuide.DAO;
using HalalGuide.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using Xamarin.Geolocation;
using HalalGuide.Util;
using HalalGuide.Domain.Enum;
using System.Linq;
using System.Globalization;
using System;
using XUbertestersSDK;
using System.IO;
using S3Storage.Response;
using S3Storage.AWSException;

namespace HalalGuide.ViewModels
{
	public class SingleDiningViewModel : BaseViewModel, ITableViewModel<LocationPicture>
	{
		public event EventHandler LoadedReviewListEvent = delegate { };

		protected virtual void OnLoadedReviewListEvent (EventArgs e)
		{
			EventHandler handler = LoadedReviewListEvent;
			if (handler != null) {
				handler (this, e);
			}
		}

		private List<LocationPicture> Pictures { get; set; }

		private List<Review> Reviews { get; set; }

		public SingleDiningViewModel () : base ()
		{
			Pictures = new List<LocationPicture> ();
			Reviews = new List<Review> ();
			//LocationChangedEvent += (sender, e) => ;
		}

		public int Rows ()
		{
			return Pictures.Count;
		}

		public LocationPicture GetLocationAtRow (int row)
		{
			return Pictures [row];
		}

		public async Task<Stream> GetLocationPictureAtRow (int row)
		{
			try {
				GetObjectResult result = await S3.GetObject (Constants.S3Bucket, Pictures [row].LocationId + "/" + Pictures [row].Id);
				return result.Stream;
			} catch (AWSErrorException ex) {
				XUbertesters.LogError (string.Format ("BaseViewModel: Error downloading image: {0} due to: {1}", Pictures [row].Id, ex));
				return null;
			}
		}


		public async Task Update ()
		{
			XUbertesters.LogInfo (string.Format ("SingleDiningViewModel: Fetching pictures for Location: {0}", SelectedLocation.Id));
			SelectQuery<LocationPicture> query = new SelectQuery<LocationPicture> ();
			query.Equal (LocationPicture.LocationIdIdentifier, SelectedLocation.Id);
			Pictures = await LocationPictureDAO.Select (query);
			XUbertesters.LogInfo (string.Format ("SingleDiningViewModel: Fetched pictures for Location: {0}, found: {1}", SelectedLocation.Id, Pictures.Count));

			OnLoadedListEvent (EventArgs.Empty);

			XUbertesters.LogInfo (string.Format ("SingleDiningViewModel: Fetching Review for Location: {0}", SelectedLocation.Id));
			SelectQuery<Review> query2 = new SelectQuery<Review> ();
			query2.Equal (Review.LocationIdIdentifier, SelectedLocation.Id);
			Reviews = await ReviewDAO.Select (query2);
			XUbertesters.LogInfo (string.Format ("SingleDiningViewModel: Fetched Review for Location: {0}, found: {1}", SelectedLocation.Id, Reviews.Count));

			LoadedReviewListEvent (this, EventArgs.Empty);
		}

		public int NumberOfReviews ()
		{
			return Reviews.Count;
		}

		public Review GetReviewAtRow (int item)
		{
			return Reviews [item];
		}

		public double AverageReviewScore ()
		{
			if (Reviews != null) {
				return Reviews.Average (r => r.Rating);
			} else {
				return 0;
			}
		}

		public async Task<string> GetReviewTextAtRow (int item)
		{
			try {
				String line = "";
				GetObjectResult result = await S3.GetObject (Constants.S3Bucket, Reviews [item].LocationId + "/" + Reviews [item].Id + ".txt");
				using (StreamReader sr = new StreamReader (result.Stream)) {
					line = sr.ReadToEnd ();
				}
				return line;
			} catch (AWSErrorException ex) {
				XUbertesters.LogError (string.Format ("BaseViewModel: Error downloading review: {0} due to: {1}", Reviews [0].Id, ex));
				return "ERROR";
			} catch (Exception e) {
				XUbertesters.LogError (string.Format ("BaseViewModel: Error parsing review: {0} due to: {1}", Reviews [0].Id, e));
				return "ERROR";
			}
		}
	}
}

