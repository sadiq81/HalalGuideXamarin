using System;
using HalalGuide.DAO;
using System.Threading.Tasks;
using HalalGuide.Domain;
using HalalGuide.Domain.Enums;
using HalalGuide.Util;
using HalalGuide.Services;

namespace HalalGuide.Services
{
	public class FacebookService
	{
		private FacebookUserDAO userDAO { get { return ServiceContainer.Resolve<FacebookUserDAO> (); } }

		private DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		public async Task<FacebookUser> GetFacebookUserName (String id)
		{
			return await userDAO.Get (id);
		}

		public async Task SaveFacebookUser (string id, string name, byte[] image)
		{
			FacebookUser fu = new FacebookUser (){ id = id, name = name };
			await userDAO.SaveOrReplace (fu);
			//TODO upload profile picture
		}


	}
}

