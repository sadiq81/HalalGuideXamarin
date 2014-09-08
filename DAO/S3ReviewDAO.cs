using System.Threading.Tasks;
using HalalGuide.Domain;
using S3Storage.S3;
using SimpleDBPersistence.Service;
using S3Storage.Response;
using HalalGuide.Util;
using System.IO;
using System.Text;

namespace HalalGuide.DAO
{
	public class S3ReviewDAO:  S3BaseDAO
	{
		private   S3ClientCore _S3 = ServiceContainer.Resolve<S3ClientCore> ();

		public async Task StoreReview (Review review, string reviewText)
		{
			await _S3.PutObject (Constants.S3Bucket, review.LocationId + "/" + review.Id, Encoding.UTF8.GetBytes (reviewText));
		}

		public async Task<Stream> RetrieveReview (Review review)
		{
			GetObjectResult result = await _S3.GetObject (Constants.S3Bucket, review.LocationId + "/" + review.Id);
			return result.Stream;
		}

		public async Task DeleteReview (Review review)
		{
			await _S3.DeleteObject (Constants.S3Bucket, review.LocationId + "/" + review.Id);
		}

	}
}

