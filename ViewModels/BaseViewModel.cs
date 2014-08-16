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

		protected static  S3ClientCore S3 = ServiceContainer.Resolve<S3ClientCore> ();

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
	}
}

