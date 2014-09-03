using System;
using MonoTouch.UIKit;
using HalalGuide.ViewModels;
using XUbertestersSDK;
using SimpleDBPersistence.Service;
using MonoTouch.Foundation;
using HalalGuide.Util;

namespace HalalGuide.iOS.ViewController
{
	public class BaseTableViewController : UITableViewController
	{
		public BaseTableViewController ()
		{
		}

		protected LoginViewModel LoginViewModel = ServiceContainer.Resolve<LoginViewModel> ();

		private UIViewController LoginViewController { get; set; }

		public BaseTableViewController (System.IntPtr handle) : base (handle)
		{
		}

		public BaseTableViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
		}

		public override bool ShouldPerformSegue (string segueIdentifier, NSObject sender)
		{
			XUbertesters.LogInfo (string.Format ("BaseViewController: ShouldPerformSegue-Start {0}", segueIdentifier));
			if (segueIdentifier != null && segueIdentifier.Equals (Segue.AddNewDiningViewControllerSegue)) {

				if (LoginViewModel.IsAuthenticated ()) {
					XUbertesters.LogInfo ("BaseViewController: ShouldPerformSegue-End");
					return true;
				} else {
					LoginViewModel.LoginCompletedEvent += (model, e) => LoginViewController.DismissViewController (true, delegate {
						if (e.IsAuthenticated) {
							PerformSegue (segueIdentifier, sender);
						} else {
							new UIAlertView ("Fejl", "Du blev ikke logget ind, prøv igen senere", null, "Ok", null).Show ();
						}
					});

					var auth = LoginViewModel.Authenticate ();
					PresentViewController (LoginViewController = auth.GetUI (), true, null);

					XUbertesters.LogInfo ("BaseViewController: ShouldPerformSegue-End");
					return false;
				}
			} else {
				XUbertesters.LogInfo ("BaseViewController: ShouldPerformSegue-End");
				return true;
			}
		}
	}
}

