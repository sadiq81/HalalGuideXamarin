using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HalalGuide.ViewModels;
using HalalGuide.Domain;
using HalalGuide.iOS.Util;
using HalalGuide.iOS.Tables.Cells;
using HalalGuide.Services;
using HalalGuide.iOS.ViewController.Base;


namespace HalalGuide.iOS.ViewController
{
	public partial class LandingViewController : BaseViewController
	{
		public LandingViewController (IntPtr handle) : base (handle)
		{
		}

		public LandingViewModel ViewModel = ServiceContainer.Resolve<LandingViewModel> ();
		public SingleDiningViewModel SingleDiningViewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();
 
		private UITableViewController TableViewController = new UITableViewController ();
		private UIRefreshControl RefreshControl = new UIRefreshControl ();

		public async override void  ViewDidLoad ()
		{
			NavigationController.NavigationBar.Translucent = false;

			base.ViewDidLoad ();

			SetupTableView ();

			SetupEventListeners ();

			ViewModel.RefreshCache ();

			//Refresh app data
			await ViewModel.RefreshLocations ();
			await ViewModel.RefreshLocationData ();
			await ViewModel.RefreshReviews ();

		}

		#region Setup

		private void SetupTableView ()
		{
			TableViewController.TableView = LatestUpdatedTableView;

			TableViewController.RefreshControl = RefreshControl;
			RefreshControl.ValueChanged += async (sender, e) => {
				RefreshControl.BeginRefreshing ();
				await ViewModel.RefreshLocations ();
				RefreshControl.EndRefreshing ();
			};
		}


		private void SetupEventListeners ()
		{

			ViewModel.refreshedLocations += (sender, e) => InvokeOnMainThread (() => {
				TableViewController.TableView.ReloadData ();
			});

			ViewModel.refreshedLocationPictures += (sender, e) => InvokeOnMainThread (() => {
				TableViewController.TableView.ReloadData ();
			});

			ViewModel.locationChangedEvent += (sender, e) => InvokeOnMainThread (() => {
				ViewModel.CalculateDistances ();
				TableViewController.TableView.ReloadSections (new NSIndexSet (0), UITableViewRowAnimation.None);
			});
			
		}

		#endregion

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (Segue.SingleDiningViewControllerSegue.Equals (segue.Identifier)) {
				NSIndexPath indexPath = LatestUpdatedTableView.IndexPathForCell ((UITableViewCell)sender);
				SingleDiningViewModel.selectedLocation = ViewModel.GetLocationAtRow (indexPath.Item);
			}
		}

		#region TableView

		[Export ("tableView:numberOfRowsInSection:")]
		public  int RowsInSection (UITableView tableview, int section)
		{
			return ViewModel.Rows ();
		}

		[Export ("tableView:cellForRowAtIndexPath:")]
		public  UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			Location l = ViewModel.GetLocationAtRow (indexPath.Item);

			UITableViewCell cell = tableView.DequeueReusableCell (l.locationType.ToString ());

			((ILocationCell)cell).ConfigureLocation (l);

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public  void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true); // normal iOS behaviour is to remove the blue highlight
		}

		#endregion
	}
}
