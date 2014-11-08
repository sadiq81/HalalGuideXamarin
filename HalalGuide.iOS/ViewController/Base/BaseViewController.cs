using MonoTouch.UIKit;
using MonoTouch.Foundation;
using HalalGuide.ViewModels;
using HalalGuide.iOS.Util;
using HalalGuide.Util;
using System.Drawing;
using HalalGuide.Services;
using HalalGuide.Domain.Enums;
using MonoTouch.AudioToolbox;
using BigTed;
using System;

namespace HalalGuide.iOS.ViewController.Base
{
	public class BaseViewController : UIViewController
	{
		protected LoginViewModel LoginViewModel = ServiceContainer.Resolve<LoginViewModel> ();

		public BaseViewController (System.IntPtr handle) : base (handle)
		{
		}

		public BaseViewController (string nibName, NSBundle bundle) : base (nibName, bundle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.TranslateLabelsAndPlaceholders ();
			TranslateNavigationItem ();

			SetupEventHandlers ();

		}

		public void TranslateNavigationItem ()
		{

			if (NavigationItem != null) {
				UIBarButtonItem left = NavigationItem.LeftBarButtonItem;
				if (left != null && left.Title != null) {
					string text = left.Title;
					string translation = Localization.GetLocalizedValue (text) ?? text;
					left.Title = translation;
				}
				UIBarButtonItem right = NavigationItem.RightBarButtonItem;
				if (right != null && right.Title != null) {
					string text = right.Title;
					string translation = Localization.GetLocalizedValue (text) ?? text;
					right.Title = translation;
				}
				if (NavigationItem.Title != null) {
					string text = NavigationItem.Title;
					string translation = Localization.GetLocalizedValue (text) ?? text;
					NavigationItem.Title = translation;
				}
			}
		}

		void SetupEventHandlers ()
		{
			LoginViewModel.busy += (object sender, ProgressResult e) => BeginInvokeOnMainThread (delegate {
				switch (e.type) {
				case ProgressType.Show:
					{
						BTProgressHUD.Show (null, -1, ProgressHUD.MaskType.Gradient);
						break;
					}
				case ProgressType.ShowWithText:
					{
						BTProgressHUD.Show (e.message, -1, ProgressHUD.MaskType.Gradient);
						break;
					}
				case ProgressType.ShowSuccessWithStatus:
					{
						BTProgressHUD.ShowSuccessWithStatus (e.message);
						break;
					}
				case ProgressType.ShowErrorWithStatus:
					{
						BTProgressHUD.ShowErrorWithStatus (e.message);
						break;
					}
				case ProgressType.ShowToast:
					{
						BTProgressHUD.ShowToast (e.message, ProgressHUD.ToastPosition.Center);
						break;
					}
				case ProgressType.Dismiss:
					{
						BTProgressHUD.Dismiss ();
						break;
					}
				}
			});
			LoginViewModel.network += (object sender, bool e) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = e;
			};
		}

		public override bool ShouldPerformSegue (string segueIdentifier, NSObject sender)
		{
			if (segueIdentifier != null && (segueIdentifier.Equals (Segue.AddNewDiningViewControllerSegue) || segueIdentifier.Equals (Segue.AddReviewViewControllerSegue))) {

				if (LoginViewModel.IsAuthenticated ()) {
					return true;
				} else {
					LoginViewModel.LoginCompletedEvent += (model, e) => {
						if (e.Value) {
							PerformSegue (segueIdentifier, sender);
						} else {
							new UIAlertView ("Fejl", "Du blev ikke logget ind, prøv igen senere", null, "Ok", null).Show ();
						}
					};
					LoginViewModel.Authenticate (this);
					return false;
				}
			} else {
				return true;
			}
		}
	}
}

