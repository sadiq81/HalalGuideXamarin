using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HalalGuide.ViewModels;
using SimpleDBPersistence.Service;
using HalalGuide.Domain.Enum;
using System.Collections.Generic;
using XUbertestersSDK;
using System.Drawing;

namespace HalalGuide.iOS.ViewController
{
	public partial class FilterMultipleDiningViewController : UIViewController
	{
		private bool isExpanded { get; set; }

		private MultipleDiningViewModel MultipleDiningViewModel = ServiceContainer.Resolve<MultipleDiningViewModel> ();

		private static string cellIdentifier = "CategoryTableCell";

		List<DiningCategory> CategoriesShown = new List<DiningCategory> ();
		List<DiningCategory> CategoriesHidden = new List<DiningCategory> ();

		public FilterMultipleDiningViewController (IntPtr handle) : base (handle)
		{

		}

		public  override void ViewDidLoad ()
		{
			XUbertesters.LogInfo ("FilterDiningPageController: ViewDidLoad-Start");
			base.ViewDidLoad ();

			SetupTableView ();

			SetupUIValues ();

			SetupEventListeners ();

			XUbertesters.LogInfo ("FilterDiningPageController: ViewDidLoad-End");
		}

		#region Setup

		private void SetupTableView ()
		{
			CategoryTableView.TableFooterView = new UIView ();
		}

		private void SetupUIValues ()
		{

			DistanceSlider.Value = (float)MultipleDiningViewModel.DistanceFilter;
			DistanceLabel.Text = MultipleDiningViewModel.DistanceFilter.ToString ();

			PorkSwitch.On = MultipleDiningViewModel.PorkFilter;
			AlcoholSwitch.On = MultipleDiningViewModel.AlcoholFilter;
			HalalSwitch.On = MultipleDiningViewModel.HalalFilter;

			CategoriesLabel.Text = MultipleDiningViewModel.CategoryFilter.Count.ToString ();
		}

		void SetupEventListeners ()
		{
			PorkSwitch.ValueChanged += (sender, e) => MultipleDiningViewModel.PorkFilter = PorkSwitch.On;
			AlcoholSwitch.ValueChanged += (sender, e) => MultipleDiningViewModel.AlcoholFilter = AlcoholSwitch.On;
			HalalSwitch.ValueChanged += (sender, e) => MultipleDiningViewModel.HalalFilter = HalalSwitch.On;
		}

		#endregion

		#region Actions

		partial void DistanceSliderValueChanged (UISlider sender)
		{
			XUbertesters.LogInfo ("FilterDiningPageController: SliderValueChanged-Start");
			sender.Value = (float)Math.Round (sender.Value, MidpointRounding.AwayFromZero);
			MultipleDiningViewModel.DistanceFilter = (int)sender.Value;
			DistanceLabel.Text = sender.Value + " km";
			XUbertesters.LogInfo ("FilterDiningPageController: SliderValueChanged-End");
		}

		partial void ResetCategories (UIButton sender)
		{
			XUbertesters.LogInfo ("FilterDiningPageController: ResetCategory-Start");
			MultipleDiningViewModel.CategoryFilter.Clear ();
			CategoriesLabel.Text = "0";
			foreach (UIView view in CategoryTableView.Subviews) {
				if (view is UITableViewCell) {
					((UITableViewCell)view).Accessory = UITableViewCellAccessory.None;
				}
			}
			XUbertesters.LogInfo ("FilterDiningPageController: ResetCategory-End");
		}

		partial void ChooseCategories (UIButton sender)
		{
			XUbertesters.LogInfo ("FilterDiningPageController: ChooseCategory-Start");
			sender.SetTitle (isExpanded ? "Vælg" : "Luk", UIControlState.Normal);

			if (isExpanded) {

				int count = CategoriesShown.Count;

				for (int i = 0; i < count; i++) {
					CategoryTableView.BeginUpdates ();
					// insert the 'ADD NEW' row at the end of table display
					CategoryTableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (CategoryTableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);
					// create a new item and add it to our underlying data (it is not intended to be permanent)

					DiningCategory cat = CategoriesShown [CategoryTableView.NumberOfRowsInSection (0) - 1];
					CategoriesHidden.Add (cat);
					CategoriesShown.Remove (cat);
					CategoryTableView.EndUpdates (); // applies the changes
				}

			} else {

				foreach (DiningCategory category in DiningCategory.Categories) {
					CategoryTableView.BeginUpdates ();
					// insert the 'ADD NEW' row at the end of table display
					CategoryTableView.InsertRows (new NSIndexPath[] { NSIndexPath.FromRowSection (CategoryTableView.NumberOfRowsInSection (0), 0) }, UITableViewRowAnimation.Fade);
					// create a new item and add it to our underlying data (it is not intended to be permanent)
					CategoriesHidden.Remove (category);
					CategoriesShown.Add (category);
					CategoryTableView.EndUpdates (); // applies the changes
				}
			}

			UIView.Animate (
				duration : 0.2,
				animation: () => {
					CategoryTableView.Frame = new RectangleF (CategoryTableView.Frame.X, CategoryTableView.Frame.Y, 320, isExpanded ? 0 : this.View.Frame.Height - CategoryTableView.Frame.Y);
				}
			);
			isExpanded = !isExpanded;
			XUbertesters.LogInfo ("FilterDiningPageController: ChooseCategory-End");
		}

		partial void Done (NSObject sender)
		{
			DismissViewController (true, null);
		}

		#endregion

		[Export ("positionForBar:")]
		public  UIBarPosition GetPositionForBar (IUIBarPositioning barPositioning)
		{
			return UIBarPosition.TopAttached;
		}

	

		#region TableView

		[Export ("tableView:numberOfRowsInSection:")]
		public  int RowsInSection (UITableView tableview, int section)
		{
			return CategoriesShown.Count;
		}

		[Export ("tableView:cellForRowAtIndexPath:")]
		public  UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{

			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}

			cell.TextLabel.Text = "\t" + CategoriesShown [indexPath.Row].Title;

			bool selected = MultipleDiningViewModel.CategoryFilter.Contains (CategoriesShown [indexPath.Row]);
			cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public  void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt (indexPath);
			DiningCategory cat = CategoriesShown [indexPath.Row];

			if (MultipleDiningViewModel.CategoryFilter.Contains (cat)) {
				MultipleDiningViewModel.CategoryFilter.Remove (CategoriesShown [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.None;
			} else {
				MultipleDiningViewModel.CategoryFilter.Add (CategoriesShown [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}

			CategoriesLabel.Text = MultipleDiningViewModel.CategoryFilter.Count.ToString ();
		}

		#endregion
	}
}
