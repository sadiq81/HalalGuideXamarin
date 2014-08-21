using System;
using Xamarin.Geolocation;
using SimpleDBPersistence.Service;
using HalalGuide.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Auth;
using HalalGuide.Util;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using S3Storage.S3;
using XUbertestersSDK;
using System.IO;
using System.Collections.Generic;
using HalalGuide.Domain;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using HalalGuide.DAO;
using Xamarin.Media;
using S3Storage.Response;
using S3Storage.AWSException;
using System.Globalization;

namespace HalalGuide.ViewModels
{
	public  class BaseViewModel
	{
		public event EventHandler LoadedListEvent = delegate { };

		protected virtual void OnLoadedListEvent (EventArgs e)
		{
			EventHandler handler = LoadedListEvent;
			if (handler != null) {
				handler (this, e);
			}
		}

		public event EventHandler LocationChangedEvent = delegate { };

		protected virtual void OnLocationChangedEvent (EventArgs e)
		{
			EventHandler handler = LocationChangedEvent;
			if (handler != null) {
				handler (this, e);
			}
		}



		protected static Geolocator Locator = ServiceContainer.Resolve<Geolocator> ();

		protected static KeyChainService KeyChain = ServiceContainer.Resolve<KeyChainService> ();

		protected static  AddressService AddressService = ServiceContainer.Resolve<AddressService> ();

		protected LocationDAO LocationDAO = ServiceContainer.Resolve<LocationDAO> ();

		protected LocationPictureDAO LocationPictureDAO = ServiceContainer.Resolve<LocationPictureDAO> ();

		protected ReviewDAO ReviewDAO = ServiceContainer.Resolve<ReviewDAO> ();

		protected static  S3ClientCore S3 = ServiceContainer.Resolve<S3ClientCore> ();

		protected readonly MediaPicker MediaPicker = ServiceContainer.Resolve<MediaPicker> ();

		protected static Position Position { get; set; }

		public static Location SelectedLocation { get; set; }

		protected readonly TaskScheduler UiScheduler = TaskScheduler.FromCurrentSynchronizationContext ();

		public BaseViewModel ()
		{
			if (Locator.IsGeolocationAvailable && !Locator.IsListening) {
				XUbertesters.LogInfo (string.Format ("BaseViewModel: started listening on location")); 
				Locator.StartListening (10 * 60, 300);
			}

			Locator.PositionChanged += (object sender, PositionEventArgs e) => {
				XUbertesters.LogInfo (string.Format ("BaseViewModel: Location changed")); 

				Position = e.Position;
				if (SelectedLocation != null) {
					SelectedLocation.Distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = double.Parse (SelectedLocation.Latitude, CultureInfo.InvariantCulture),
						Longitude = double.Parse (SelectedLocation.Longtitude, CultureInfo.InvariantCulture)
					});
				}
				LocationChanged (this, e);
			};
		}

		protected virtual void LocationChanged (object sender, PositionEventArgs e)
		{
			LocationChangedEvent (sender, e);
		}

		public bool IsAuthenticated ()
		{
			return KeyChain.IsFaceBookAccountAuthenticated ();
		}

		public void SaveCredentials ()
		{

		}

		protected void CalculateDistances (ref List<Location> locations)
		{

			foreach (Location loc in locations) {

				if (Position != null) {
					double distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = double.Parse (loc.Latitude, CultureInfo.InvariantCulture),
						Longitude = double.Parse (loc.Longtitude, CultureInfo.InvariantCulture)
					});
					loc.Distance = distance;
				}
			}
		}

		public async Task<Stream> GetFirstImageForLocation (Location location)
		{
			SelectQuery<LocationPicture> query = new SelectQuery<LocationPicture> ();
			query.Equal (LocationPicture.LocationIdIdentifier, location.Id);
			List<LocationPicture> list = await LocationPictureDAO.Select (query);
			if (list != null && list.Count > 0) {
				try {
					GetObjectResult result = await S3.GetObject (Constants.S3Bucket, location.Id + "/" + list [0].Id);
					return result.Stream;
				} catch (AWSErrorException ex) {
					XUbertesters.LogError (string.Format ("BaseViewModel: Error downloading image: {0} due to: {1}", list [0].Id, ex));
					return null;
				}

			} else {
				return null;
			}
		}
	}
}

