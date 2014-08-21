using MonoTouch.UIKit;

namespace HalalGuide.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			SimpleDBPersistence.Service.ServiceContainer.Register<Xamarin.Geolocation.Geolocator> (() => new Xamarin.Geolocation.Geolocator { DesiredAccuracy = 25 });
			SimpleDBPersistence.Service.ServiceContainer.Register<Xamarin.Media.MediaPicker> (() => new Xamarin.Media.MediaPicker ());
			SimpleDBPersistence.Service.ServiceContainer.Register<Xamarin.Auth.AccountStore> (() => Xamarin.Auth.AccountStore.Create ());

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			SimpleDBPersistence.Service.ServiceContainer.Register<S3Storage.S3.S3ClientCore> (() => new S3Storage.S3.S3ClientCore (HalalGuide.Util.Credentials.AWSKey, HalalGuide.Util.Credentials.AWSSecret, S3Storage.Region.EUWest_1));

			SimpleDBPersistence.Service.ServiceContainer.Register <SimpleDBPersistence.SimpleDB.SimpleDBClientCore> (() => new SimpleDBPersistence.SimpleDB.SimpleDBClientCore (HalalGuide.Util.Credentials.AWSKey, HalalGuide.Util.Credentials.AWSSecret, SimpleDBPersistence.SimpleDB.Region.EUWest_1));

			SimpleDBPersistence.Service.ServiceContainer.Register <HalalGuide.DAO.LocationDAO> (() => new HalalGuide.DAO.LocationDAO ());
			SimpleDBPersistence.Service.ServiceContainer.Register <HalalGuide.DAO.LocationPictureDAO> (() => new HalalGuide.DAO.LocationPictureDAO ());
			SimpleDBPersistence.Service.ServiceContainer.Register <HalalGuide.DAO.ReviewDAO> (() => new HalalGuide.DAO.ReviewDAO ());

			SimpleDBPersistence.Service.ServiceContainer.Register <SimpleDBPersistence.Service.ISHA256Service> (() => new HalalGuide.Services.SHA256Service ());
			S3Storage.Service.ServiceContainer.Register <S3Storage.Service.ISHA256Service> (() => new HalalGuide.Services.SHA256Service ());

			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.Services.AddressService> (() => new HalalGuide.Services.AddressService ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.Services.KeyChainService> (() => new HalalGuide.Services.KeyChainService ());

			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.BaseViewModel> (() => new HalalGuide.ViewModels.BaseViewModel ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.LandingViewModel> (() => new HalalGuide.ViewModels.LandingViewModel ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.MultipleDiningViewModel> (() => new HalalGuide.ViewModels.MultipleDiningViewModel ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.AddDiningViewModel> (() => new HalalGuide.ViewModels.AddDiningViewModel ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.AddReviewViewModel> (() => new HalalGuide.ViewModels.AddReviewViewModel ());
			SimpleDBPersistence.Service.ServiceContainer.Register<HalalGuide.ViewModels.SingleDiningViewModel> (() => new HalalGuide.ViewModels.SingleDiningViewModel ());

			UIApplication.Main (args, null, "AppDelegate");

		}
	}
}
