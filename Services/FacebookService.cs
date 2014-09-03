using System;
using HalalGuide.DAO;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using HalalGuide.Domain;
using System.Runtime.CompilerServices;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using SimpleDBPersistence.Domain;
using HalalGuide.Domain.Enum;
using SimpleDBPersistence.SimpleDB.Model.AWSException;
using XUbertestersSDK;
using HalalGuide.Util;
using System.Linq.Expressions;
using System.Deployment.Internal;
using System.Threading;

namespace HalalGuide.Services
{
	public class FacebookService
	{
		private   ImageService _ImageService = ServiceContainer.Resolve<ImageService> ();

		private FacebookUserDAO UserDAO = ServiceContainer.Resolve<FacebookUserDAO> ();

		private  DatabaseWrapper DB = ServiceContainer.Resolve<DatabaseWrapper> ();

		public async Task<String> GetFacebookUserName (String id)
		{
			FacebookUser user = DB.Table<FacebookUser> ().Where (fbu => fbu.Id == id).FirstOrDefault ();
			if (user == null) {
				user = await UserDAO.Get (id, true);
				DB.InsertOrReplace (user);
			}
			return user.Name;
		}

		public async Task<CreateEntityResult> SaveFacebookUser (string id, string name, byte[] image)
		{
			FacebookUser fu = new FacebookUser (){ Id = id, Name = name };

			try {

				Task<bool> saveUserTask = UserDAO.SaveOrReplace (fu);

				if (await saveUserTask) {

					CreateEntityResult imageResult = await _ImageService.UploadProfilePicture (fu, image);

					if (imageResult != CreateEntityResult.OK) {

						await UserDAO.Delete (fu);
					}
				}

			} catch (AWSErrorException ex) {
				XUbertesters.LogError ("FacebookService: CouldNotCreateEntityInSimpleDB: " + ex + " Entity: " + id);
				return CreateEntityResult.CouldNotUploadImageToS3;
			} catch (Exception e) {  //TODO remove try/catch
				XUbertesters.LogError ("FacebookService: CouldNotCreateEntityInSimpleDB: " + e + " Entity: " + id);
				return CreateEntityResult.CouldNotUploadImageToS3;
			}
			return CreateEntityResult.OK;
		}

	}
}

