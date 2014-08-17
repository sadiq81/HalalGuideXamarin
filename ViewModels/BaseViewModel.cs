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
	public abstract class BaseViewModel
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

		public event EventHandler<AuthenticatorCompletedEventArgs> LoginCompletedEvent = delegate { };

		protected virtual void OnLoginCompletedEvent (AuthenticatorCompletedEventArgs e)
		{
			EventHandler<AuthenticatorCompletedEventArgs> handler = LoginCompletedEvent;
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

		public OAuth2Authenticator Authenticate ()
		{
			XUbertesters.LogInfo (string.Format ("BaseViewModel: Authenticate")); 

			var auth = new OAuth2Authenticator (
				           clientId: Credentials.FacebookAppId,
				           scope: "",
				           authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				           redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));
			auth.AllowCancel = true;
			auth.Completed += (sender, eventArgs) => {

				if (eventArgs.IsAuthenticated) {

					XUbertesters.LogInfo (string.Format ("BaseViewModel: Authenticated facebook with facebook account:"));

					Account ac = eventArgs.Account;
					var getFacebookInfo = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, eventArgs.Account);

					getFacebookInfo.GetResponseAsync ().ContinueWith (t => {
						if (t.IsFaulted) {
							XUbertesters.LogInfo (String.Format ("BaseViewModel: Could not get facebook information: {0}", t.Result.GetResponseText ()));
							LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (null));
						} else {
							string stringFullOfJson = t.Result.GetResponseText ();
							JToken token = JObject.Parse (stringFullOfJson);
							string id = (string)token.SelectToken ("id");
							eventArgs.Account.Username = id;
							KeyChain.StoreAccount (eventArgs.Account);
							XUbertesters.LogInfo (string.Format ("BaseViewModel: Got facebook information from user: {0}", id));
							LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (ac));
						}
					}, UiScheduler);

				} else {
					XUbertesters.LogWarn (string.Format ("BaseViewModel: Could not authenticate with facebook: {0}", eventArgs.ToString ()));
					LoginCompletedEvent (sender, eventArgs);
				}
			};

			return auth;
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
			query.Equal ("LocationId", location.Id);
			List<LocationPicture> list = await LocationPictureDAO.Select (query);
			if (list != null && list.Count > 0) {
				try {
					GetObjectResult result = await S3.GetObject (Constants.S3Bucket, list [0].Id);
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

