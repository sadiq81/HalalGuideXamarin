using MonoTouch.UIKit;
using HalalGuide.iOS.Util;
using MonoTouch.Foundation;

namespace HalalGuide.iOS.Textfield
{
	[Register ("BaseTextField")]
	public class BaseTextField : UITextField
	{
		public BaseTextField ()
		{
		}


		public BaseTextField (NSCoder coder) : base (coder)
		{
		}


		public BaseTextField (NSObjectFlag t) : base (t)
		{
		}


		public BaseTextField (System.IntPtr handle) : base (handle)
		{
		}


		public BaseTextField (System.Drawing.RectangleF frame) : base (frame)
		{
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			this.TranslateLabelsAndPlaceholders ();
		}
	}
}

