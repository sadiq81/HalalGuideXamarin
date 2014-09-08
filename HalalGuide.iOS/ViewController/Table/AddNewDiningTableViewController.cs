// This file has been autogenerated from a class added in the UI designer.

using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using HalalGuide.ViewModels;
using SimpleDBPersistence.Service;
using XUbertestersSDK;
using HalalGuide.Domain.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Media;
using HalalGuide.Util;
using HalalGuide.iOS.Util;
using HalalGuide.iOS.Tables.Cells;

namespace HalalGuide.iOS.ViewController.Table
{
	public partial class AddNewDiningTableViewController : KeyboardSupportedTableViewController
	{
		public AddNewDiningTableViewController (IntPtr handle) : base (handle)
		{
		}

		private readonly AddDiningViewModel ViewModel = ServiceContainer.Resolve<AddDiningViewModel> ();
		private readonly AddReviewViewModel AddReviewViewModel = ServiceContainer.Resolve<AddReviewViewModel> ();

		public async  override void ViewDidLoad ()
		{
			SetupEventListeners ();
			await SetupUIElements ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			XUbertesters.LogInfo ("AddNewDiningTableViewController: ViewDidAppear");
		}

		#region Setup

		private void  SetupEventListeners ()
		{

			Road.EditingDidEnd += async (sender, e) => {
				RoadNumber.AutoCompleteValues = ViewModel.StreetNumbers (Road.Text);
				PostalCode.Text = ViewModel.PostalCode (Road.Text) ?? PostalCode.Text;
				City.Text = await ViewModel.GetCityNameFromPostalCode (PostalCode.Text) ?? City.Text;
			};
			PostalCode.EditingDidEnd += async (sender, e) => {
				string cityName = await ViewModel.GetCityNameFromPostalCode (PostalCode.Text);
				City.Text = cityName ?? City.Text;
			};

			PorkSwitch.ValueChanged += (sender, e) => PorkValueChanged ((UISwitch)sender);
			AlcoholSwitch.ValueChanged += (sender, e) => AlcoholValueChanged ((UISwitch)sender);
			HalalSwitch.ValueChanged += (sender, e) => HalalValueChanged ((UISwitch)sender);

			Choose.TouchUpInside += (sender, e) => ChooseCategories ((UIButton)sender);
			Reset.TouchUpInside += (sender, e) => ResetCategories ((UIButton)sender);

			PickImage.TouchUpInside += (sender, e) => SelectImage ((UIButton)sender);

		}

		private async Task SetupUIElements ()
		{
			base.ViewDidLoad ();

			VisibleCategories = new List<DiningCategory> ();
			CategoriesChoosen = new List<DiningCategory> ();

			await ViewModel.LoadAddressNearPosition ();

			Road.AutoCompleteValues = ViewModel.StreetNames ();

		}


		[Export ("textFieldShouldReturn:")]
		public  override bool ShouldReturn (UITextField textField)
		{
			if (textField == Name) {
				Road.BecomeFirstResponder ();
				return false;
			} else if (textField == Road) {
				RoadNumber.BecomeFirstResponder ();
				return false;
			} else if (textField == RoadNumber) {
				PostalCode.BecomeFirstResponder ();
				return false;
			} else if (textField == PostalCode) {
				City.BecomeFirstResponder ();
				return false;
			} else if (textField == Telephone) {
				HomePage.BecomeFirstResponder ();
				return false;
			} else {
				return true;
			}
		}

		#endregion

		#region Actions

		private void Regreet (UIBarButtonItem sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: Regreet");
			DismissViewController (true, null);
		}

		public async Task Save (UIBarButtonItem sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: Save");

			ResignKeyboard ();

			if (String.IsNullOrEmpty (Name.Text)) {
				new UIAlertView ("Fejl", "Navn skal udfyldes", null, "Ok").Show ();
				return;
			}

			if (String.IsNullOrEmpty (Road.Text)) {
				new UIAlertView ("Fejl", "Vej skal udfyldes", null, "Ok").Show ();
				return;
			}

			if (String.IsNullOrEmpty (RoadNumber.Text)) {
				new UIAlertView ("Fejl", "Vejnummer skal udfyldes", null, "Ok").Show ();
				return;
			}

			if (String.IsNullOrEmpty (PostalCode.Text)) {
				new UIAlertView ("Fejl", "Postnummer skal udfyldes", null, "Ok").Show ();
				return;
			}

			if (String.IsNullOrEmpty (City.Text)) {
				new UIAlertView ("Fejl", "By skal udfyldes", null, "Ok").Show ();
				return;
			}

			InvokeOnMainThread (ActivityIndicator.StartAnimating);

			CreateEntityResult result = await ViewModel.CreateNewLocation (
				                            Name.Text, 
				                            Road.Text, 
				                            RoadNumber.Text, 
				                            PostalCode.Text, 
				                            City.Text, 
				                            Telephone.Text, 
				                            HomePage.Text, 
				                            PorkSwitch.On, 
				                            AlcoholSwitch.On, 
				                            HalalSwitch.On,
				                            CategoriesChoosen, PickImage.Title (UIControlState.Normal) == null ? Image.Image.AsJPEG ().ToArray () : null);

			ActivityIndicator.StopAnimating ();

			if (result == CreateEntityResult.OK) {
				new UIAlertView ("Succes", "Dit forslag er sent til godkendelse", null, "Ok", new string[]{ "Tilføj anmeldelse" }){ WeakDelegate = this }.Show ();
			} else {
				new UIAlertView ("Fejl", result.ToString (), null, "Ok", null).Show ();
			}

		}

		[Export ("alertView:clickedButtonAtIndex:")]
		public virtual void Clicked (UIAlertView alertview, int buttonIndex)
		{
			switch (buttonIndex) {
			case 0:
				{
					DismissViewController (true, null);
					break;
				}
			case 1:
				{
					PerformSegue (Segue.AddReviewViewControllerSegue, this);
					break;
				}
			}
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (Segue.AddReviewViewControllerSegue.Equals (segue.Identifier)) {

				AddReviewViewModel.SelectedLocation = ViewModel.SelectedLocation;
			}
		}



		private void AlcoholValueChanged (UISwitch sender)
		{
			ResignKeyboard ();
			AlcoholImage.Image = UIImage.FromBundle (Images.Alcohol + sender.On);
		}

		private void HalalValueChanged (UISwitch sender)
		{
			ResignKeyboard ();
			HalalImage.Image = UIImage.FromBundle (Images.NonHalal + sender.On);
		}

		private void PorkValueChanged (UISwitch sender)
		{
			ResignKeyboard ();
			PorkImage.Image = UIImage.FromBundle (Images.Pig + sender.On);
		}

		private void SelectImage (UIButton sender)
		{
			ResignKeyboard ();

			XUbertesters.LogInfo ("AddNewDiningController: PickImage-Start");
			if (ViewModel.IsCameraAvailable () || UIDevice.CurrentDevice.Model.Contains ("Simulator")) {

				UIActionSheet actionSheet = new UIActionSheet ("Tilføj billede", null, "Fortryd", null, "Tag med kamera", "Væg fra kamerarulle");
				actionSheet.Clicked += async delegate(object a, UIButtonEventArgs b) {
					switch (b.ButtonIndex) {
					case 0:
						{
							MediaFile file = await ViewModel.TakePicture ("../tmp", "temp.jpg");
							if (file != null)
								InvokeOnMainThread (() => {
									Image.Image = UIImage.LoadFromData (NSData.FromFile (file.Path));
									PickImage.SetTitle (null, UIControlState.Normal);
								});
							break;
						}
					case 1:
						{
							MediaFile file = await ViewModel.GetPictureFromDevice ();
							if (file != null)
								InvokeOnMainThread (() => {
									Image.Image = UIImage.LoadFromData (NSData.FromFile (file.Path));
									PickImage.SetTitle (null, UIControlState.Normal);
								});
							break;
						}
					case 2:
						{
							break;
						}

					}
				};
				actionSheet.ShowInView (View);

			} else {
				XUbertesters.LogWarn ("AddNewDiningController: noCameraFound");
				UIAlertView noCameraFound = new UIAlertView ("Fejl", "Intet kamera tilgægeligt", null, "Luk");
				noCameraFound.Show ();
			}
		}

		#endregion

		#region TableView

		private bool isExpanded { get; set; }

		public List<DiningCategory> VisibleCategories { get; set; }

		public List<DiningCategory> CategoriesChoosen { get; set; }

		private void ChooseCategories (UIButton sender)
		{
			ResignKeyboard ();

			sender.SetTitle (isExpanded ? "Vælg" : "Luk", UIControlState.Normal);

			if (isExpanded) {

				TableView.BeginUpdates ();
				for (int i = 0; i < DiningCategory.Categories.Count; i++) {
					VisibleCategories.RemoveAt (0);
					TableView.DeleteRows (new []{ NSIndexPath.FromRowSection (i, 2) }, UITableViewRowAnimation.Fade);
				}
				TableView.EndUpdates ();

			} else {
				TableView.BeginUpdates ();
				for (int i = 0; i < DiningCategory.Categories.Count; i++) {
					VisibleCategories.Add (DiningCategory.Categories [i]);
					TableView.InsertRows (new []{ NSIndexPath.FromRowSection (i, 2) }, UITableViewRowAnimation.Fade);
				}
				TableView.EndUpdates ();

			}

			isExpanded = !isExpanded;

		}

		private void ResetCategories (UIButton sender)
		{
			ResignKeyboard ();

			Count.Text = "0";
			CategoriesChoosen.Clear ();
			TableView.ReloadSections (new NSIndexSet (2), UITableViewRowAnimation.Fade);
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			if (section == 2) {
				return VisibleCategories.Count;
			} else {
				return base.RowsInSection (tableView, section);
			} 
		}

		public  override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 2) {

				var cell = tableView.DequeueReusableCell (CategoryCell.Identifier);

				if (cell == null) {
					cell = new CategoryCell (UITableViewCellStyle.Default, CategoryCell.Identifier);
				}

				cell.TextLabel.Text = "\t" + DiningCategory.Categories [indexPath.Row].Title;

				bool selected = CategoriesChoosen.Contains (DiningCategory.Categories [indexPath.Row]);
				cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

				return cell;
			} else {
				return base.GetCell (tableView, indexPath);
			}
		}


		public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath rowIndexPath)
		{
			if (rowIndexPath.Section == 2) {
				return true;
			} else {
				return false;
			}
		}


		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt (indexPath);
			tableView.DeselectRow (indexPath, false);

			if (indexPath.Section != 2) {
				return;
			}

			DiningCategory cat = DiningCategory.Categories [indexPath.Row];

			if (CategoriesChoosen.Contains (cat)) {
				CategoriesChoosen.Remove (DiningCategory.Categories [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.None;
			} else {
				CategoriesChoosen.Add (DiningCategory.Categories [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}

			Count.Text = CategoriesChoosen.Count.ToString ();
		}


		public override float GetHeightForRow (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Section == 2) {
				return 44;
			} else {
				return base.GetHeightForRow (tableView, indexPath);
			}
		}

		public override int IndentationLevel (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Section == 2) {
				return 0;
			} else {
				return base.IndentationLevel (tableView, indexPath);
			}
		}

		#endregion
	}
}
