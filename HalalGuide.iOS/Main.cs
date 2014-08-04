using MonoTouch.UIKit;
using S3Storage.S3;
using SimpleDBPersistence.SimpleDB;
using SimpleDBPersistence.Service;
using Xamarin.Geolocation;
using HalalGuide.ViewModels;
using HalalGuide.Services;
using Xamarin.Media;
using Xamarin.Auth;
using HalalGuide.DAO;
using System.Net;
using HalalGuide.Util;

namespace HalalGuide.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{


			ServiceContainer.Register<Geolocator> (() => new Geolocator { DesiredAccuracy = 25 });
			ServiceContainer.Register<MediaPicker> (() => new MediaPicker ());
			ServiceContainer.Register<AccountStore> (() => AccountStore.Create ());

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			ServiceContainer.Register<S3ClientCore> (() => new S3ClientCore (Credentials.AWSKey, Credentials.AWSSecret, S3Storage.Region.EUWest_1));

			ServiceContainer.Register <SimpleDBClientCore> (() => new SimpleDBClientCore (Credentials.AWSKey, Credentials.AWSSecret, SimpleDBPersistence.SimpleDB.Region.EUWest_1));

			ServiceContainer.Register <LocationDAO> (() => new LocationDAO ());

			ServiceContainer.Register <SimpleDBPersistence.Service.ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register <S3Storage.Service.ISHA256Service> (() => new SHA256Service ());
			ServiceContainer.Register<AddressService> (() => new AddressService ());
			ServiceContainer.Register<KeyChainService> (() => new KeyChainService ());

			ServiceContainer.Register<LatestViewModel> (() => new LatestViewModel ());
			ServiceContainer.Register<DiningViewModel> (() => new DiningViewModel ());
			ServiceContainer.Register<AddDiningViewModel> (() => new AddDiningViewModel ());

			UIApplication.Main (args, null, "AppDelegate");
			UIApplication.SharedApplication.IdleTimerDisabled = true;
		}
	}
}
