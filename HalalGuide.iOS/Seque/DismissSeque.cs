using System;
using MonoTouch.UIKit;

namespace HalalGuide.iOS.Seque
{
	public partial class DismissSeque : UIStoryboardSegue
	{
		public DismissSeque (IntPtr handle) : base (handle)
		{
		}

		public override void Perform ()
		{
			SourceViewController.PresentingViewController.DismissViewController (true, null);
		}


	}
}
