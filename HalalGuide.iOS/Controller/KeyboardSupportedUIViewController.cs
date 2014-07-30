using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace HalalGuide.iOS
{
	public partial class KeyboardSupportedUIViewController : UIViewController, IUITextFieldDelegate
	{
		private float animatedDistance;
		const float KEYBOARD_ANIMATION_DURATION = 0.3f;
		const float MINIMUM_SCROLL_FRACTION = 0.2f;
		const float MAXIMUM_SCROLL_FRACTION = 0.8f;
		const float PORTRAIT_KEYBOARD_HEIGHT = 216f;
		const float LANDSCAPE_KEYBOARD_HEIGHT = 162f;

		public KeyboardSupportedUIViewController () : base ("KeyboardSupportedUIViewController", null)
		{
		}

		public KeyboardSupportedUIViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			foreach (UIView view in this.View.Subviews) {
				if (view is UITextField) {
					((UITextField)view).WeakDelegate = this;
				}

			}
		}

		[MonoTouch.Foundation.Export ("textFieldDidBeginEditing:")]
		public  void EditingStarted (UITextField textField)
		{


			RectangleF textFieldRect = this.View.Window.ConvertRectFromView (textField.Bounds, textField);
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

		[MonoTouch.Foundation.Export ("textFieldDidEndEditing:")]
		public  void EditingEnded (UITextField textField)
		{
			RectangleF viewFrame = this.View.Frame;
			viewFrame.Y += animatedDistance;
			UIView.BeginAnimations ("slideScreenForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (KEYBOARD_ANIMATION_DURATION);
			this.View.Frame = viewFrame;
			UIView.CommitAnimations (); 
		}

		[MonoTouch.Foundation.Export ("textFieldShouldReturn:")]
		public  bool ShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();
			return true;
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			foreach (UIView view in this.View.Subviews) {
				if (view is UITextField) {
					view.ResignFirstResponder ();
				}
			}
		}
	}
}

