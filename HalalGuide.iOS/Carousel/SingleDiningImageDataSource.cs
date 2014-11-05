using System;
using Alliance.Carousel;
using HalalGuide.ViewModels;
using MonoTouch.UIKit;
using System.Drawing;
using HalalGuide.Services;
using MonoTouch.CoreText;
using SDWebImage;
using MonoTouch.Foundation;

namespace HalalGuide.iOS.Carousel
{
	public class SingleDiningImageDataSource : CarouselViewDataSource
	{
		private readonly SingleDiningViewModel viewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();

		public SingleDiningImageDataSource ()
		{
		}

		public override uint NumberOfItems (CarouselView carousel)
		{
			return (uint)viewModel.pictures.Count;
		}

		public override UIView ViewForItem (CarouselView carousel, uint index, UIView reusingView)
		{
			UIImageView imageView = null;

			if (reusingView == null) {
				reusingView = imageView = new UIImageView (new RectangleF (10, 10, 100, 100)) {
					ContentMode = UIViewContentMode.ScaleAspectFill,
					ClipsToBounds = true
				};
			} else {
				imageView = (UIImageView)reusingView;
			}

			imageView.SetImage (
				url: new NSUrl (viewModel.pictures [(int)index].imageUri), 
				placeholder: UIImage.FromBundle ("Camera.png")
			);

			return reusingView;
		}

	}
}

