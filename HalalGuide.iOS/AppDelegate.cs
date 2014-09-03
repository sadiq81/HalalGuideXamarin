using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XUbertestersSDK;
using System;
using SimpleDBPersistence.Service;
using HalalGuide.Util;
using HalalGuide.Services;
using HalalGuide.Domain;

namespace HalalGuide.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		DatabaseWrapper _SQLiteConnection = ServiceContainer.Resolve<DatabaseWrapper> ();
		PreferencesService _PreferencesService = ServiceContainer.Resolve<PreferencesService> ();

		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			XUbertesters.Initialize ();

			AppDomain.CurrentDomain.UnhandledException += (object sender, UnhandledExceptionEventArgs e) => {
				XUbertesters.SendCrash (e);
			};

			if (_PreferencesService.GetString (Constants.HasBeenLaunched) != null) {
				//TODO Start downloading all items from database;
			} else {

				_PreferencesService.StoreString (Constants.HasBeenLaunched, "true");
				_PreferencesService.StoreString (Constants.LocationLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));
				_PreferencesService.StoreString (Constants.ReviewLastUpdated, DateTime.MinValue.ToString (Constants.DateFormat));

				_SQLiteConnection.CreateTable<Location> ();
				_SQLiteConnection.CreateTable<LocationPicture> ();
				_SQLiteConnection.CreateTable<Review> ();
				_SQLiteConnection.CreateTable<FacebookUser> ();
			}

			return true;
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

