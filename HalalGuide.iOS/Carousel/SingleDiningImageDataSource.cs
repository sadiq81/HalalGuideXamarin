using System;
using Alliance.Carousel;
using HalalGuide.ViewModels;
using MonoTouch.UIKit;
using System.Drawing;
using HalalGuide.Services;
using MonoTouch.CoreText;

namespace HalalGuide.iOS.Carousel
{
	public class SingleDiningImageDataSource : CarouselViewDataSource
	{
		private readonly SingleDiningViewModel ViewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();

		public SingleDiningImageDataSource ()
		{
		}

		public override uint NumberOfItems (CarouselView carousel)
		{
			return (uint)ViewModel.Pictures.Count;
		}

		public override UIView ViewForItem (CarouselView carousel, uint index, UIView reusingView)
		{
			if (reusingView == null) {
				var imgView = new UIImageView (new RectangleF (10, 10, 90, 90)) {
					Image = UIImage.FromBundle ("Camera.png"),
					ContentMode = UIViewContentMode.ScaleAspectFit
				};
				reusingView = imgView;
			} else {
				//TODO Use SDWEB image to set image
			}
			return reusingView;
		}

	}
}

