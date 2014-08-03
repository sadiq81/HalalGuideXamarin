using System;
using HalalGuide.Util;
using Xamarin.Auth;
using SimpleDBPersistence.Service;
using System.Collections.Generic;
using System.Linq;

namespace HalalGuide.Services
{
	public class KeyChainService
	{

		private readonly AccountStore Store = ServiceContainer.Resolve<AccountStore> ();

		public KeyChainService ()
		{

		}

		/*
		public static Account GetFaceBookAccount ()
		{
			List<Account> accounts = Store.FindAccountsForService (Constants.Facebook).ToList ();
			if (accounts.Count > 0) {
				return accounts [0];
			} else {
				return null;
			}
		}
		*/
	}
}

