using MonoTouch.UIKit;
using System;
using Xamarin.Geolocation;
using Xamarin.Media;
using HalalGuide.DAO;
using HalalGuide.Services;
using HalalGuide.ViewModels;
using HalalGuide.Util;
using System.IO;
using Xamarin.Auth;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin;

namespace HalalGuide.iOS
{
	public class Application
	{
		static void Main (string[] args)
		{

			Insights.Initialize ("0d28d9180fc6ec91afaa709ee9fe8b68fe46cb86");

			ServiceContainer.Register<MobileServiceClient> (() => new MobileServiceClient ("https://halalguide.azure-mobile.net/", "DzyawLMKsdmtXTcJTyIJjCQipurkgR22"));
			CurrentPlatform.Init ();

			ServiceContainer.Register<Geolocator> (() => new Geolocator { DesiredAccuracy = 25 });
			ServiceContainer.Register<MediaPicker> (() => new MediaPicker ());
			ServiceContainer.Register<AccountStore> (() => AccountStore.Create ());

			ServiceContainer.Register<DatabaseWrapper> (() => new DatabaseWrapper (Path.Combine (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library"), "halalguide.db3")));
			ServiceContainer.Register <LocationDAO> (() => new LocationDAO ());
			ServiceContainer.Register <LocationPictureDAO> (() => new LocationPictureDAO ());
			ServiceContainer.Register <ReviewDAO> (() => new ReviewDAO ());
			ServiceContainer.Register <FacebookUserDAO> (() => new FacebookUserDAO ());

			ServiceContainer.Register<AddressService> (() => new AddressService ());
			ServiceContainer.Register<KeyChainService> (() => new KeyChainService ());
			ServiceContainer.Register<FileService> (() => new FileService ());
			ServiceContainer.Register<LocationService> (() => new LocationService ());
			ServiceContainer.Register<PreferencesService> (() => new PreferencesService ());
			ServiceContainer.Register<ReviewService> (() => new ReviewService ());
			ServiceContainer.Register<FacebookService> (() => new FacebookService ());
			ServiceContainer.Register<ImageService> (() => new ImageService ());
			ServiceContainer.Register<UploadService> (() => new UploadService ());

			ServiceContainer.Register<LoginViewModel> (() => new LoginViewModel ());
			ServiceContainer.Register<LandingViewModel> (() => new LandingViewModel ());
			ServiceContainer.Register<MultipleDiningViewModel> (() => new MultipleDiningViewModel ());
			ServiceContainer.Register<AddDiningViewModel> (() => new AddDiningViewModel ());
			ServiceContainer.Register<AddReviewViewModel> (() => new AddReviewViewModel ());
			ServiceContainer.Register<SingleDiningViewModel> (() => new SingleDiningViewModel ()); 

			DatabaseWrapper database = ServiceContainer.Resolve<DatabaseWrapper> ();
			PreferencesService preferences = ServiceContainer.Resolve<PreferencesService> ();

			if (preferences.GetString (Constants.HasBeenLaunched) != null) {

				//TODO Start downloading new items from database;
			} else {

				database.CreateTable<HalalGuide.Domain.Location> ();
				database.CreateTable<HalalGuide.Domain.LocationPicture> ();
				database.CreateTable<HalalGuide.Domain.Review> ();

				preferences.StoreString (Constants.HasBeenLaunched, "true");
				preferences.StoreString (Constants.LocationLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));
				preferences.StoreString (Constants.LocationPictureLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));
				preferences.StoreString (Constants.LocationReviewLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));


			}
				
			UIApplication.Main (args, null, "AppDelegate");

		}
	}
}
