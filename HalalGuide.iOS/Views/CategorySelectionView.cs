using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;
using XUbertestersSDK;
using System.Drawing;
using HalalGuide.Domain.Enum;

namespace HalalGuide.iOS
{
	[Register ("CategorySelectionView")]
	public sealed class CategorySelectionView : UIView
	{
		private UILabel CountLabel { get; set; }

		private UITableView TableView { get; set; }

		private UIButton Choose { get; set; }

		private UIButton Reset { get; set; }

		public List<DiningCategory> CategoriesChoosen { get; set; }

		public CategorySelectionView (IntPtr handle) : base (handle)
		{
		}

		public CategorySelectionView (RectangleF frame) : this (frame, new List<DiningCategory> ())
		{
		}

		public CategorySelectionView (RectangleF frame, List<DiningCategory> categoriesChoosen) : base (frame)
		{

		}


		private void OpenClose ()
		{

			XUbertesters.LogInfo ("CategorySelectionView: ChooseCategory");
			Choose.SetTitle (isExpanded ? "Vælg" : "Luk", UIControlState.Normal);

			isExpanded = !isExpanded;

			UIView.Animate (
				duration : 0.2,
				animation: () => {
					Frame = new RectangleF (Frame.X, Frame.Y, 320, isExpanded ? UIScreen.MainScreen.Bounds.Height - Frame.Y : 21);
				}
			);
		}

		private void ClearSelection ()
		{
			CountLabel.Text = "0";
			CategoriesChoosen.Clear ();
			foreach (UITableViewCell cell in TableView.VisibleCells) {
				cell.Accessory = UITableViewCellAccessory.None;
			}
		}

		private bool isExpanded { get; set; }

		private const string cellIdentifier = "CategoryTableCell";

		[Export ("tableView:numberOfRowsInSection:")]
		public  int RowsInSection (UITableView tableview, int section)
		{
			return DiningCategory.Categories.Count; 
		}

		[Export ("tableView:cellForRowAtIndexPath:")]
		public   UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{

			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}


			cell.TextLabel.Text = "\t" + DiningCategory.Categories [indexPath.Row].Title;

			bool selected = CategoriesChoosen.Contains (DiningCategory.Categories [indexPath.Row]);
			cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		public  void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{


			var cell = tableView.CellAt (indexPath);
			DiningCategory cat = DiningCategory.Categories [indexPath.Row];

			if (CategoriesChoosen.Contains (cat)) {
				CategoriesChoosen.Remove (DiningCategory.Categories [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.None;
			} else {
				CategoriesChoosen.Add (DiningCategory.Categories [indexPath.Row]);
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			}

			CountLabel.Text = CategoriesChoosen.Count.ToString ();
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

		}
	}
}

