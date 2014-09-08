using HalalGuide.Util;
using Xamarin.Auth;
using SimpleDBPersistence.Service;
using System.Collections.Generic;
using System.Linq;

namespace HalalGuide.Services
{
	public class KeyChainService
	{
		private static readonly AccountStore Store = ServiceContainer.Resolve<AccountStore> ();
		private  Account Facebook;

		public KeyChainService ()
		{
			List<Account> accounts = Store.FindAccountsForService (Constants.Facebook).ToList ();
			if (accounts.Count > 0) {
				Facebook = accounts [0];				
			}
		}

		public Account GetFaceBookAccount ()
		{
			return Facebook;
		}

		public void StoreAccount (Account account)
		{
			Store.Save (Facebook = account, Constants.Facebook);
		}

		public  bool IsFaceBookAccountAuthenticated ()
		{
			return Facebook != null;
		}
	}
}

