using S3Storage.S3;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using HalalGuide.Util;
using S3Storage.Response;
using HalalGuide.Domain;
using System.IO;

namespace HalalGuide.DAO
{
	public class S3ProfilePictureDAO:S3BaseDAO
	{
		private   S3ClientCore _S3 = ServiceContainer.Resolve<S3ClientCore> ();

		public async Task StoreProfilePicture (FacebookUser facebookUser, byte[] data)
		{
			await _S3.PutObject (Constants.S3Bucket, Constants.Facebook + "/" + facebookUser.Id + ".jpg", data);
		}

		public async Task<Stream> RetrieveProfilePicture (string userId)
		{
			GetObjectResult result = await _S3.GetObject (Constants.S3Bucket, Constants.Facebook + "/" + userId + ".jpg");
			return result.Stream;
		}

		public async Task DeleteProfilePicture (FacebookUser facebookUser)
		{
			await _S3.DeleteObject (Constants.S3Bucket, Constants.Facebook + "/" + facebookUser.Id + ".jpg");
		}

	}
}

