using System;
using MonoTouch.UIKit;
using HalalGuide.iOS.Util;
using MonoTouch.Foundation;

namespace HalalGuide.iOS.Tables.Cells
{
	[Register ("BaseTableViewCell")]
	public class BaseTableViewCell : UITableViewCell
	{

		public BaseTableViewCell (UITableViewCellStyle style, string reuseIdentifier) : base (style, reuseIdentifier)
		{
		}


		public BaseTableViewCell ()
		{
		}


		public BaseTableViewCell (MonoTouch.Foundation.NSCoder coder) : base (coder)
		{
		}


		public BaseTableViewCell (MonoTouch.Foundation.NSObjectFlag t) : base (t)
		{
		}


		public BaseTableViewCell (IntPtr handle) : base (handle)
		{
		}


		public BaseTableViewCell (System.Drawing.RectangleF frame) : base (frame)
		{
		}


		public BaseTableViewCell (UITableViewCellStyle style, MonoTouch.Foundation.NSString reuseIdentifier) : base (style, reuseIdentifier)
		{
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			this.TranslateLabelsAndPlaceholders ();
		}
	}
}

