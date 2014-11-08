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
using Xamarin;
using System.Collections.Generic;

namespace HalalGuide.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public event EventHandler<EventArgs<bool>> LoginCompletedEvent = delegate { };

		private MobileServiceClient client { get { return ServiceContainer.Resolve<MobileServiceClient> (); } }

		public LoginViewModel () : base ()
		{
		}


		public async void Authenticate (UIViewController view)
		{
			try {
				OnNetwork (true);
				MobileServiceUser user = await client.LoginAsync (view, MobileServiceAuthenticationProvider.Facebook);
				OnNetwork (false);
				if (user != null) {
					keychainService.StoreAccount (user);
					LoginCompletedEvent (this, new EventArgs<bool> (true));
				} else {
					LoginCompletedEvent (this, new EventArgs<bool> (false));
				}
			} catch (Exception ex) {
				Insights.Report (ex); 
				LoginCompletedEvent (this, new EventArgs<bool> (false));
			}
		}
	}
}

