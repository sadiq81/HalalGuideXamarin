using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HalalGuide.iOS.ViewController.Table
{
	public partial class KeyboardSupportedTableViewController : BaseTableViewController, IUITextFieldDelegate
	{
		private float animatedDistance;
		const float KEYBOARD_ANIMATION_DURATION = 0.3f;
		const float MINIMUM_SCROLL_FRACTION = 0.2f;
		const float MAXIMUM_SCROLL_FRACTION = 0.8f;
		const float PORTRAIT_KEYBOARD_HEIGHT = 216f;
		const float LANDSCAPE_KEYBOARD_HEIGHT = 162f;

		public KeyboardSupportedTableViewController () : base ("KeyboardSupportedTableViewController", null)
		{
		}

		public KeyboardSupportedTableViewController (IntPtr handle) : base (handle)
		{
		}

		private UITextField CurrentUITextField { get; set; }

		private UITextView CurrentUITextView { get; set; }

		private UITapGestureRecognizer Tap { get; set; }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			SetupTouchRecognizer ();
		}

		public void SetupTouchRecognizer ()
		{
			Tap = new UITapGestureRecognizer (() => {
				if (Tap.State == UIGestureRecognizerState.Ended) {
					ResignKeyboard ();
				}
			});
			Tap.CancelsTouchesInView = false;
			Tap.WeakDelegate = this;
			TableView.AddGestureRecognizer (Tap);
		}

		[Export ("gestureRecognizer:shouldReceiveTouch:")]
		public bool ShouldReceiveTouch (UIGestureRecognizer recognizer, UITouch touch)
		{

			if (touch.View is UIButton || touch.View is UIBarButtonItem || touch.View is UISwitch) {
				return false;
			} else {
				return true;
			}
		}

		protected void ResignKeyboard ()
		{
			if (CurrentUITextField != null) {
				CurrentUITextField.ResignFirstResponder ();
			}

			if (CurrentUITextView != null) {
				CurrentUITextView.ResignFirstResponder ();
			}
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			SetTextFieldDelegate (View);
		}

		private void SetTextFieldDelegate (UIView searchView)
		{
			foreach (UIView view in searchView.Subviews) {

				if (!(view is UITextField) && !(view is UITextField)) {
					SetTextFieldDelegate (view);
				} else {
					if (view is UITextField) {
						((UITextField)view).WeakDelegate = this;
					}
					if (view is UITextView) {
						((UITextView)view).WeakDelegate = this;
					}
				}
			}
		}

		[Export ("textViewDidBeginEditing:")]
		public void EditingStarted (UITextView textView)
		{
			CurrentUITextView = textView;

			MoveViewForTextFieldInput (textView);
		}

		[Export ("textViewDidEndEditing:")]
		public void EditingEnded (UITextView textView)
		{
			MoveViewBackAfterTextFieldEditing (textView);
		}

		[Export ("textViewShouldEndEditing:")]
		public bool ShouldEndEditing (MonoTouch.UIKit.UITextView textView)
		{
			textView.ResignFirstResponder ();
			return true;
		}

		[MonoTouch.Foundation.Export ("textFieldDidBeginEditing:")]
		public  void EditingStarted (UITextField textField)
		{
			CurrentUITextField = textField;

			MoveViewForTextFieldInput (textField);

		}

		[MonoTouch.Foundation.Export ("textFieldDidEndEditing:")]
		public  void EditingEnded (UITextField textField)
		{
			MoveViewBackAfterTextFieldEditing (textField);
		}

		[MonoTouch.Foundation.Export ("textFieldShouldReturn:")]
		public virtual bool ShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();
			return true;
		}

		private void MoveViewForTextFieldInput (UIView view)
		{
			RectangleF textFieldRect = this.View.Window.ConvertRectFromView (view.Bounds, view);
			RectangleF viewRect = this.View.Window.ConvertRectFromView (this.View.Bounds, this.View);
			float midline = (float)(textFieldRect.Y + 0.5 * textFieldRect.Size.Height);
			float numerator = midline - viewRect.Y - MINIMUM_SCROLL_FRACTION * viewRect.Size.Height;
			float denominator = (MAXIMUM_SCROLL_FRACTION - MINIMUM_SCROLL_FRACTION) * viewRect.Size.Height;

			float heightFraction = numerator / denominator;
			if (heightFraction < 0.0) {
				heightFraction = 0.0f;
			} else if (heightFraction > 1.0) {
				heightFraction = 1.0f;
			}

			UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;

			if (orientation == UIInterfaceOrientation.Portrait || orientation == UIInterfaceOrientation.PortraitUpsideDown) {
				animatedDistance = (float)Math.Floor (PORTRAIT_KEYBOARD_HEIGHT * heightFraction);
			} else {
				animatedDistance = (float)Math.Floor (LANDSCAPE_KEYBOARD_HEIGHT * heightFraction);
			}
			RectangleF viewFrame = this.View.Frame;
			viewFrame.Y -= animatedDistance;

			UIView.BeginAnimations ("slideScreenForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (KEYBOARD_ANIMATION_DURATION);
			this.View.Frame = viewFrame;
			UIView.CommitAnimations (); 
		}

		private void MoveViewBackAfterTextFieldEditing (UIView view)
		{
			RectangleF viewFrame = this.View.Frame;
			viewFrame.Y += animatedDistance;
			UIView.BeginAnimations ("slideScreenForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (KEYBOARD_ANIMATION_DURATION);
			this.View.Frame = viewFrame;
			UIView.CommitAnimations (); 
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			ResignKeyboard ();
		}
	}

}

