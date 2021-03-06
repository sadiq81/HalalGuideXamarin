﻿using HalalGuide.Util;
using Xamarin.Auth;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin;

namespace HalalGuide.Services
{
	public class KeyChainService
	{
		private AccountStore store { get { return ServiceContainer.Resolve<AccountStore> (); } }

		private const string KtokenIdentifier = "MobileServiceAuthenticationToken";
		private const string KserviceId = "Facebook";

		public MobileServiceUser user { get; set; }

		public KeyChainService ()
		{
			List<Account> accounts = store.FindAccountsForService (KserviceId).ToList ();
			if (accounts.Count > 0) {
				Account facebook = accounts [0];				
				user = new MobileServiceUser (facebook.Username);
				string token;
				facebook.Properties.TryGetValue (KtokenIdentifier, out token);
				user.MobileServiceAuthenticationToken = token;
			}
		}

		public void StoreAccount (MobileServiceUser user)
		{
			Account account = new Account (user.UserId, new Dictionary<string, string> () { {
					KtokenIdentifier,
					user.MobileServiceAuthenticationToken
				}
			});
//			Insights.Identify(user.UserId);
//			var traits = new Dictionary<string, string>() {
//				{Insights.Traits.Email, "gordon.strachen@celtic.com"},
//				{Insights.Traits.Name, "Gordon Strachen"}
//			};
//			Insights.Identify("YourUsersUniqueId", traits);
			store.Save (account, KserviceId);
		}

		public  bool IsAuthenticated ()
		{
			return user != null;
		}
	}
}

