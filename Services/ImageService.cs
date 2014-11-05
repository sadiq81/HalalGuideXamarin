using System;
using System.Threading.Tasks;
using HalalGuide.Domain;
using HalalGuide.Domain.Enums;
using Xamarin.Media;
using HalalGuide.DAO;
using MonoTouch.UIKit;
using System.Collections.Generic;
using HalalGuide.Util;
using System.Linq;
using System.Globalization;

namespace HalalGuide.Services
{
	public class ImageService
	{
		private UploadService upload { get { return ServiceContainer.Resolve<UploadService> (); } }

		private KeyChainService keychain { get { return ServiceContainer.Resolve<KeyChainService> (); } }

		private  DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		private LocationPictureDAO locationPictureDAO { get { return ServiceContainer.Resolve<LocationPictureDAO> (); } }

		private  PreferencesService preferences { get { return ServiceContainer.Resolve<PreferencesService> (); } }

		public ImageService ()
		{
		}

		public async Task UploadImageForLocation (Location location, MediaFile file)
		{
			LocationPicture picture = new LocationPicture () {
				locationId = location.id,
				imageUri = location.locationType.DefaultImageName (),
				submitterId = "",//keychain.user.UserId,
				creationStatus = CreationStatus.AwaitingApproval
			};

			await locationPictureDAO.SaveOrReplace (picture);

			picture.imageUri = await upload.UploadFile (file.GetStream (), picture.id + ".jpeg");

			await locationPictureDAO.SaveOrReplace (picture);
		}

		public async Task RetrieveLatestLocationPictures ()
		{
			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat, CultureInfo.InvariantCulture);

			string lastUpdatedString = preferences.GetString (Constants.LocationPictureLastUpdated);

			DateTime updatedLast = DateTime.ParseExact (lastUpdatedString, Constants.DateFormat, CultureInfo.InvariantCulture);

			await locationPictureDAO.Where (pic => pic.updatedAt > updatedLast);

			preferences.StoreString (Constants.LocationPictureLastUpdated, updatedTime);
		}

		public List<LocationPicture> RetrieveAllPicturesForLocation (Location location)
		{
			List<LocationPicture> pictures = database.Table<LocationPicture> ().Where (pic => pic.deleted == false && pic.locationId == location.id).ToList ();
			return pictures;
		}

	}
}

