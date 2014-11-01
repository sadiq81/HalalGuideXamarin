using System;
using Xamarin.Auth;
using HalalGuide.Util;
using Newtonsoft.Json.Linq;
using HalalGuide.Domain.Enums;
using System.Net;
using Microsoft.WindowsAzure.MobileServices;
using HalalGuide.Services;
using System.Threading.Tasks;
using MonoTouch.UIKit;

namespace HalalGuide.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public event EventHandler<EventArgs<bool>> LoginCompletedEvent = delegate { };

		private MobileServiceClient client { get { return ServiceContainer.Resolve<MobileServiceClient> (); } }


		public async Task Authenticate (UIViewController view)
		{
			try {
				MobileServiceUser user = await client.LoginAsync (view, MobileServiceAuthenticationProvider.Facebook);
				if (user != null) {
					keychainService.StoreAccount (user);
					LoginCompletedEvent (this, new EventArgs<bool> (true));
				} else {
					LoginCompletedEvent (this, new EventArgs<bool> (false));
				}
			} catch (Exception ex) {
				LoginCompletedEvent (this, new EventArgs<bool> (false));
			}
		}
	}
}

