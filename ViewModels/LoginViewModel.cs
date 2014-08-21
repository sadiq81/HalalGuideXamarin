using System;
using Xamarin.Auth;
using XUbertestersSDK;
using HalalGuide.Util;
using Newtonsoft.Json.Linq;

namespace HalalGuide.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel ()
		{
		}

		public event EventHandler<AuthenticatorCompletedEventArgs> LoginCompletedEvent = delegate { };

		protected virtual void OnLoginCompletedEvent (AuthenticatorCompletedEventArgs e)
		{
			EventHandler<AuthenticatorCompletedEventArgs> handler = LoginCompletedEvent;
			if (handler != null) {
				handler (this, e);
			}
		}

		public OAuth2Authenticator Authenticate ()
		{
		
			XUbertesters.LogInfo (string.Format ("LoginViewModel: Authenticate")); 

			var auth = new OAuth2Authenticator (
				           clientId: Credentials.FacebookAppId,
				           scope: "",
				           authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				           redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));
			auth.AllowCancel = true;
			auth.Completed += (sender, eventArgs) => {

				if (eventArgs.IsAuthenticated) {

					XUbertesters.LogInfo (string.Format ("LoginViewModel: Authenticated facebook with facebook account:"));

					Account ac = eventArgs.Account;
					var getFacebookInfo = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, eventArgs.Account);

					getFacebookInfo.GetResponseAsync ().ContinueWith (t => {
						if (t.IsFaulted) {
							XUbertesters.LogInfo (String.Format ("LoginViewModel: Could not get facebook information: {0}", t.Result.GetResponseText ()));
							LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (null));
						} else {
							string stringFullOfJson = t.Result.GetResponseText ();
							JToken token = JObject.Parse (stringFullOfJson);
							string id = (string)token.SelectToken ("id");
							eventArgs.Account.Username = id;
							KeyChain.StoreAccount (eventArgs.Account);
							XUbertesters.LogInfo (string.Format ("LoginViewModel: Got facebook information from user: {0}", id));
							LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (ac));
						}
					}, UiScheduler);

				} else {
					XUbertesters.LogWarn (string.Format ("LoginViewModel: Could not authenticate with facebook: {0}", eventArgs.ToString ()));
					LoginCompletedEvent (sender, eventArgs);
				}
			};
			return auth;
		}
	}
}

