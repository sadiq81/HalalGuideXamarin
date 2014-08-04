using System;
using Xamarin.Geolocation;
using SimpleDBPersistence.Service;
using HalalGuide.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Auth;
using HalalGuide.Util;
using Newtonsoft.Json.Linq;

namespace HalalGuide.ViewModels
{
	public abstract class BaseViewModel
	{
		public event EventHandler IsBusyChanged = delegate { };

		public event EventHandler LocationChangedEvent = delegate { };

		public event EventHandler<AuthenticatorCompletedEventArgs> LoginCompleted = delegate { };

		protected static Geolocator Locator = ServiceContainer.Resolve<Geolocator> ();

		protected KeyChainService KeyChain = ServiceContainer.Resolve<KeyChainService> ();

		protected AddressService AddressService = ServiceContainer.Resolve<AddressService> ();

		protected static Position Position { get; set; }

		private readonly TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext ();

		public BaseViewModel ()
		{
			if (Locator.IsGeolocationAvailable && !Locator.IsListening) {
				Locator.StartListening (10 * 60, 300);
			}

			Locator.PositionChanged += (object sender, PositionEventArgs e) => {
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
			var auth = new OAuth2Authenticator (
				           clientId: Credentials.FacebookAppId,
				           scope: "",
				           authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				           redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));
			auth.AllowCancel = true;
			auth.Completed += (sender, eventArgs) => {

				if (eventArgs.IsAuthenticated) {
					LoginCompleted (sender, eventArgs);

					var getFacebookInfo = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, eventArgs.Account);

					getFacebookInfo.GetResponseAsync ().ContinueWith (t => {
						if (t.IsFaulted) {
							LoginCompleted (sender, new AuthenticatorCompletedEventArgs (null));
						} else {
							string stringFullOfJson = t.Result.GetResponseText ();
							JToken token = JObject.Parse (stringFullOfJson);
							string id = (string)token.SelectToken ("id");
							eventArgs.Account.Username = id;
							KeyChain.StoreAccount (eventArgs.Account);
						}
					}, uiScheduler);

				} else {
					LoginCompleted (sender, eventArgs);
				}
			};

			return auth;
		}

		private bool isBusy = false;

		public bool IsBusy {
			get { return isBusy; }
			set {
				isBusy = value;
				IsBusyChanged (this, EventArgs.Empty);
			}
		}
	}
}

