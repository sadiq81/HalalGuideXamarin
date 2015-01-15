using System;
using MonoTouch.UIKit;
using HalalGuide.Domain.Enums;
using HalalGuide.iOS.Tables.Cells;
using System.Drawing;
using HalalGuide.Util;

namespace HalalGuideiOS.Views
{
	public class DiningCategoryView : UIView
	{
		private DiningCategory category { get; set; }

		private UILabel categoryLabel { get; set; }

		private UIImageView choosen { get; set; }

		public DiningCategoryView (UIView superView, PointF point, DiningCategory category) : base (new RectangleF (point, new SizeF (superView.Frame.Width, 42)))
		{
			this.category = category;
			this.categoryLabel = new UILabel (new RectangleF (8, 10.5f, superView.Frame.Width - 37, 21)) {
				Text = Localization.GetLocalizedValue (category.ToString ())
			};
			this.choosen = new UIImageView (new RectangleF (superView.Frame.Width - 29, 10.5f, 21, 21));
			AddSubviews (categoryLabel, choosen);
		}

	}
}

