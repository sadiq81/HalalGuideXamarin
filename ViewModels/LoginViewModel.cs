using System;
using Xamarin.Auth;
using XUbertestersSDK;
using HalalGuide.Util;
using Newtonsoft.Json.Linq;
using HalalGuide.Domain.Enum;
using System.Runtime.Remoting.Messaging;
using System.Net;

namespace HalalGuide.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public event EventHandler<AuthenticatorCompletedEventArgs> LoginCompletedEvent = delegate { };

		public bool IsAuthenticated ()
		{
			return _KeyChain.IsFaceBookAccountAuthenticated ();
		}

		public override void RefreshCache ()
		{
			//DO NOTHING
		}

		public OAuth2Authenticator Authenticate ()
		{
		
			XUbertesters.LogInfo (string.Format ("LoginViewModel: Authenticate")); 

			OAuth2Authenticator auth = new OAuth2Authenticator (
				                           clientId: Credentials.FacebookAppId,
				                           scope: "",
				                           authorizeUrl: new Uri ("https://m.facebook.com/dialog/oauth/"),
				                           redirectUrl: new Uri ("http://www.facebook.com/connect/login_success.html"));

			auth.AllowCancel = true;
			auth.Completed += async (sender, eventArgs) => {

				if (eventArgs.IsAuthenticated) {

					XUbertesters.LogInfo (string.Format ("LoginViewModel: Authenticated facebook with facebook account:"));

					Account ac = eventArgs.Account;
					OAuth2Request getFacebookInfo = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, eventArgs.Account);

					Response facebookInfo = await getFacebookInfo.GetResponseAsync ();

					if (facebookInfo == null || facebookInfo.StatusCode.CompareTo (HttpStatusCode.OK) != 0) {
						XUbertesters.LogInfo (String.Format ("LoginViewModel: Could not get facebook information: {0}", facebookInfo.GetResponseText () ?? "null"));
						LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (null));
						return;
					}

					string stringFullOfJson = facebookInfo.GetResponseText ();
					JToken token = JObject.Parse (stringFullOfJson);

					string id = (string)token.SelectToken ("id");
					string name = (string)token.SelectToken ("name");

					eventArgs.Account.Username = id;

					XUbertesters.LogInfo (string.Format ("LoginViewModel: Got facebook information from user: {0}", id));

					OAuth2Request getProfilePicture = new OAuth2Request ("GET", new Uri (string.Format ("https://graph.facebook.com/{0}/picture", id)), null, eventArgs.Account);

					Response profilePicture = await getProfilePicture.GetResponseAsync ();

					if (profilePicture == null || facebookInfo.StatusCode.CompareTo (HttpStatusCode.OK) != 0) {
						XUbertesters.LogInfo (String.Format ("LoginViewModel: Could not get facebook picture: {0}", profilePicture.GetResponseText () ?? "null"));
						LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (null));
						return;
					}

					CreateEntityResult result = await _FacebookService.SaveFacebookUser (id, name, StreamUtil.ReadToEnd (profilePicture.GetResponseStream ()));

					if (result == CreateEntityResult.OK) {
						_KeyChain.StoreAccount (eventArgs.Account);
						LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (ac));
						return;
					} else {
						XUbertesters.LogInfo (String.Format ("LoginViewModel: Could not store facebook account: {0}", result));
						LoginCompletedEvent (auth, new AuthenticatorCompletedEventArgs (null));
						return;
					}
				} else {
					XUbertesters.LogWarn (string.Format ("LoginViewModel: Could not authenticate with facebook: {0}", eventArgs));
					LoginCompletedEvent (sender, eventArgs);
					return;
				}
			};

			return auth;
		}
	}
}

