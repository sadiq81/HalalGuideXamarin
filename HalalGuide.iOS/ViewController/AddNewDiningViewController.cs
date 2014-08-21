using System;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Collections.Generic;
using XUbertestersSDK;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using HalalGuide.Util;
using Xamarin.Media;
using System.Drawing;
using HalalGuide.iOS.ViewController;
using HalalGuide.ViewModels;
using HalalGuide.Domain.Enum;

namespace HalalGuide.iOS.ViewController
{
	public partial class AddNewDiningViewController : KeyboardSupportedUIViewController
	{
		private readonly AddDiningViewModel ViewModel = ServiceContainer.Resolve<AddDiningViewModel> ();

		private bool isExpanded { get; set; }

		private static string cellIdentifier = "CategoryTableCell";

		List<DiningCategory> CategoriesShown = new List<DiningCategory> ();
		List<DiningCategory> CategoriesHidden = new List<DiningCategory> ();
		List<DiningCategory> CategoriesChoosen = new List<DiningCategory> ();

		public AddNewDiningViewController (IntPtr handle) : base (handle)
		{

		}

		public async override void ViewDidLoad ()
		{
			XUbertesters.LogInfo ("AddNewDiningController: ViewDidLoad-Start");

			SetupTableView ();

			SetupEventListeners ();

			await SetupUIElements ();

			XUbertesters.LogInfo ("AddNewDiningController: ViewDidLoad-End");

		}


		#region Setup

		private void SetupTableView ()
		{
			CategoriesTableView.TableFooterView = new UIView ();
		}

		private void SetupEventListeners ()
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
		}

		private async Task SetupUIElements ()
		{
			base.ViewDidLoad ();
			await ViewModel.LoadAddressNearPosition ();
			Road.AutoCompleteValues = ViewModel.StreetNames ();
			CategoriesLabel.Text = CategoriesChoosen.Count.ToString ();
		}

		#endregion

		#region Actions

		partial void Regreet (UIBarButtonItem sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: Regreet-Start");
			DismissViewController (true, null);
			XUbertesters.LogInfo ("AddNewDiningController: Regreet-End");

		}

		async partial void Save (UIBarButtonItem sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: Save-Start");

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

			CreateDiningResult result = await ViewModel.CreateNewLocation (
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
				                            CategoriesChoosen);

			ActivityIndicator.StopAnimating ();

			if (result == CreateDiningResult.OK) {
				new UIAlertView ("Succes", "Dit forslag er sent til godkendelse", null, "Ok", new string[]{ "Tilføj anmeldelse" }){ WeakDelegate = this }.Show ();
			} else {
				new UIAlertView ("Fejl", result.ToString (), null, "Ok", null).Show ();
			}

			XUbertesters.LogInfo ("AddNewDiningController: Save-End");

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

		#endregion

		[Export ("positionForBar:")]
		public  UIBarPosition GetPositionForBar (IUIBarPositioning barPositioning)
		{
			return UIBarPosition.TopAttached;
		}

		partial void AlcoholValueChanged (UISwitch sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: AlcoholValueChanged-Start");
			AlcoholImage.Image = UIImage.FromBundle (Images.Alcohol + sender.On);
			XUbertesters.LogInfo ("AddNewDiningController: AlcoholValueChanged-End");
		}

		partial void HalalValueChanged (UISwitch sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: HalalValueChanged-Start");
			HalalImage.Image = UIImage.FromBundle (Images.NonHalal + sender.On);
			XUbertesters.LogInfo ("AddNewDiningController: HalalValueChanged-End");
		}

		partial void PorkValueChanged (UISwitch sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: PorkValueChanged-Start");
			PorkImage.Image = UIImage.FromBundle (Images.Pig + sender.On);
			XUbertesters.LogInfo ("AddNewDiningController: PorkValueChanged-End");
		}

		partial void PickImage (UIButton sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: PickImage-Start");
			if (ViewModel.IsCameraAvailable ()) {

				UIActionSheet actionSheet = new UIActionSheet ("Tilføj billede", null, "Fortryd", null, "Tag med kamera", "Vælg fra kamerarulle");
				actionSheet.Clicked += async delegate(object a, UIButtonEventArgs b) {
					switch (b.ButtonIndex) {
					case 0:
						{
							MediaFile file = await ViewModel.TakePicture ("../Library/Caches", "test.jpg");
							if (file != null)
								InvokeOnMainThread (() => {
									DiningImage.Image = UIImage.LoadFromData (NSData.FromFile (file.Path));
									PickImageButton.SetTitle (null, UIControlState.Normal);
								});
							break;
						}
					case 1:
						{
							MediaFile file = await ViewModel.GetPictureFromDevice ();
							if (file != null)
								InvokeOnMainThread (() => {
									DiningImage.Image = UIImage.LoadFromData (NSData.FromFile (file.Path));
									PickImageButton.SetTitle (null, UIControlState.Normal);
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
				UIAlertView noCameraFound = new UIAlertView ("Fejl", "Intet kamera tilgængeligt", null, "Luk");
				noCameraFound.Show ();
			}
			XUbertesters.LogInfo ("AddNewDiningController: PickImage-End");
		}

		partial void ResetCategories (UIButton sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: ResetCategory-Start");
			View.EndEditing (true);

			CategoriesChoosen.Clear ();
			CategoriesLabel.Text = "0";
			foreach (UIView view in CategoriesTableView.Subviews) {
				if (view is UITableViewCell) {
					((UITableViewCell)view).Accessory = UITableViewCellAccessory.None;
				}
			}
			XUbertesters.LogInfo ("AddNewDiningController: ResetCategory-End");
		}

		partial void ChooseCategories (UIButton sender)
		{
			XUbertesters.LogInfo ("AddNewDiningController: ChooseCategory-Start");
			View.EndEditing (true);

			sender.SetTitle (isExpanded ? "Vælg" : "Luk", UIControlState.Normal);

			if (isExpanded) {

				int count = CategoriesShown.Count;

				for (int i = 0; i < count; i++) {
					CategoriesTableView.BeginUpdates ();
					// insert the 'ADD NEW' row at the end of table display
					CategoriesTableView.DeleteRows (new NSIndexPath[] { NSIndexPath.FromRowSection (CategoriesTableView.NumberOfRowsInSection (0) - 1, 0) }, UITableViewRowAnimation.Fade);
					// create a new item and add it to our underlying data (it is not intended to be permanent)

					DiningCategory cat = CategoriesShown [CategoriesTableView.NumberOfRowsInSection (0) - 1];
					CategoriesHidden.Add (cat);
					CategoriesShown.Remove (cat);
					CategoriesTableView.EndUpdates (); // applies the changes
				}

			} else {

				foreach (DiningCategory category in DiningCategory.Categories) {
					CategoriesTableView.BeginUpdates ();
					// insert the 'ADD NEW' row at the end of table display
					CategoriesTableView.InsertRows (new NSIndexPath[] { NSIndexPath.FromRowSection (CategoriesTableView.NumberOfRowsInSection (0), 0) }, UITableViewRowAnimation.Fade);
					// create a new item and add it to our underlying data (it is not intended to be permanent)
					CategoriesHidden.Remove (category);
					CategoriesShown.Add (category);
					CategoriesTableView.EndUpdates (); // applies the changes
				}
			}

			UIView.Animate (
				duration : 0.2,
				animation: () => {
					CategoriesTableView.Frame = new RectangleF (CategoriesTableView.Frame.X, CategoriesTableView.Frame.Y, 320, isExpanded ? 0 : this.View.Frame.Height - CategoriesTableView.Frame.Y);
				}
			);
			isExpanded = !isExpanded;

			XUbertesters.LogInfo ("AddNewDiningController: ChooseCategory-End");
		}

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

			bool selected = CategoriesChoosen.Contains (CategoriesShown [indexPath.Row]);
			cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public  void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.CellAt (indexPath);
			DiningCategory cat = CategoriesShown [indexPath.Row];

			if (CategoriesChoosen.Contains (cat)) {
				CategoriesChoosen.Remove (CategoriesShown [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.None;
			} else {
				CategoriesChoosen.Add (CategoriesShown [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}

			CategoriesLabel.Text = CategoriesChoosen.Count.ToString ();
		}
	}
}
