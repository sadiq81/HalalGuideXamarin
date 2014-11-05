using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace HalalGuide.iOS.Util
{
	public static class UITableViewCellExtensions
	{

		public static UITableViewCell AddSeperatorToCell (this UITableViewCell cell)
		{
			UIView lineView = new UIView (new RectangleF (0, cell.ContentView.Frame.Size.Height - 0.5f, cell.ContentView.Frame.Size.Width, 0.5f));
			lineView.BackgroundColor = UIColor.DarkGray;
			cell.ContentView.AddSubview (lineView);
			cell.ClipsToBounds = true;
			return cell;
		}
	}
}

