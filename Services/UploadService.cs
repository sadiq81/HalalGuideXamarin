using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace HalalGuide.Services
{
	public class UploadService
	{

		private MobileServiceClient client { get { return ServiceContainer.Resolve<MobileServiceClient> (); } }

		private async Task<ImageSASResponse> GetImageUri (string filename)
		{

			Dictionary<string, string> apiParameters = new Dictionary<string, string> ();

			apiParameters.Add ("resourceName", filename);

			JToken imageSas = await client.InvokeApiAsync ("images", HttpMethod.Get, apiParameters);
			return imageSas.ToObject<ImageSASResponse> ();
		}

		public async Task<string> UploadFile (Stream data, string fileName)
		{

			using (HttpClient httpClient = new HttpClient ()) {

				var imageUri = await GetImageUri (fileName);
				var content = new StreamContent (data);
				content.Headers.Add ("Content-Type", "image/jpeg");
				content.Headers.Add ("x-ms-blob-type", "BlockBlob"); 

				await httpClient.PutAsync (new Uri (imageUri.ImageUri + '?' + imageUri.SASQueryString), content);

				return imageUri.ImageUri;
			}
		}
	}

	public class ImageSASResponse
	{
		[JsonProperty (PropertyName = "sasQueryString")]
		public string SASQueryString { get; set; }

		[JsonProperty (PropertyName = "imageUri")]
		public string ImageUri { get; set; }
	}
}

