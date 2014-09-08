using S3Storage.S3;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using HalalGuide.Util;
using S3Storage.Response;
using System.IO;
using HalalGuide.Domain;

namespace HalalGuide.DAO
{
	public class S3LocationPictureDAO :S3BaseDAO
	{
		private   S3ClientCore _S3 = ServiceContainer.Resolve<S3ClientCore> ();

		public async Task StoreLocationPicture (LocationPicture locationPicture, byte[] data)
		{
			await _S3.PutObject (Constants.S3Bucket, locationPicture.LocationId + "/" + locationPicture.Id, data);
		}

		public async Task<Stream> RetrieveLocationPicture (LocationPicture locationPicture)
		{
			GetObjectResult result = await _S3.GetObject (Constants.S3Bucket, locationPicture.LocationId + "/" + locationPicture.Id);
			return result.Stream;
		}

		public async Task DeleteLocationPicture (LocationPicture locationPicture)
		{
			await _S3.DeleteObject (Constants.S3Bucket, locationPicture.LocationId + "/" + locationPicture.Id);
		}
	}
}

