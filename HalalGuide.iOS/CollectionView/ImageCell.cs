using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;

namespace HalalGuide.iOS.CollectionView
{
	public class ImageCell: UICollectionViewCell
	{
		UIImageView imageView;

		[Export ("initWithFrame:")]
		public ImageCell (System.Drawing.RectangleF frame) : base (frame)
		{
			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);

			imageView = new UIImageView (UIImage.FromBundle ("locationDining.png"));
			imageView.Center = ContentView.Center;
			imageView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);

			ContentView.AddSubview (imageView);
		}

		public UIImage Image {
			set {
				imageView.Image = value;
			}
		}
	}
}

