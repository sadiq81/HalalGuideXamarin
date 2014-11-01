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

namespace HalalGuide.iOS
{
	public class Application
	{


		// This is the main entry point of the application.
		static void Main (string[] args)
		{

			ServiceContainer.Register<MobileServiceClient> (() => new MobileServiceClient ("https://halalguide.azure-mobile.net/", "DzyawLMKsdmtXTcJTyIJjCQipurkgR22"));

			ServiceContainer.Register<Geolocator> (() => new Geolocator { DesiredAccuracy = 25 });
			ServiceContainer.Register<MediaPicker> (() => new MediaPicker ());
			ServiceContainer.Register<AccountStore> (() => AccountStore.Create ());

			ServiceContainer.Register<DatabaseWrapper> (() => new DatabaseWrapper (Path.Combine (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "..", "Library"), "halalguide.db3")));
			ServiceContainer.Register <MobileServiceClient> (() => new MobileServiceClient ("https://halalguide.azure-mobile.net/", "exBpcaIGogtMvJsitZgFKERYWMpkGQ75"));
			
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


			ServiceContainer.Register<LoginViewModel> (() => new LoginViewModel ());
			ServiceContainer.Register<LandingViewModel> (() => new LandingViewModel ());
			ServiceContainer.Register<MultipleDiningViewModel> (() => new MultipleDiningViewModel ());
			ServiceContainer.Register<AddDiningViewModel> (() => new AddDiningViewModel ());
			ServiceContainer.Register<AddReviewViewModel> (() => new AddReviewViewModel ());
			ServiceContainer.Register<SingleDiningViewModel> (() => new SingleDiningViewModel ()); 

			DatabaseWrapper database = ServiceContainer.Resolve<DatabaseWrapper> ();
			PreferencesService preferences = ServiceContainer.Resolve<PreferencesService> ();

			database.CreateTable<HalalGuide.Domain.Location> ();

			if (preferences.GetString (Constants.HasBeenLaunched) != null) {

				//TODO Start downloading all items from database;
			} else {

				preferences.StoreString (Constants.HasBeenLaunched, "true");
				preferences.StoreString (Constants.LocationLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));
				preferences.StoreString (Constants.ReviewLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));

				/*
				_SQLiteConnection.CreateTable<HalalGuide.Domain.LocationPicture> ();
				_SQLiteConnection.CreateTable<HalalGuide.Domain.Review> ();
				_SQLiteConnection.CreateTable<HalalGuide.Domain.FacebookUser> ();
				*/
			}

			CurrentPlatform.Init ();

			UIApplication.Main (args, null, "AppDelegate");

		}
	}
}
