using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using HalalGuide.Domain;
using HalalGuide.ViewModels;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;

namespace HalalGuide.iOS.CollectionView
{
	public class LocationPictureImageCell: UICollectionViewCell
	{
		public static NSString Indentifier = new NSString ("PictureCell");

		public SingleDiningViewModel ViewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();

		private LocationPicture LocationPicture { get; set; }

		UIImageView imageView;

		[Export ("initWithFrame:")]
		public LocationPictureImageCell (System.Drawing.RectangleF frame) : base (frame)
		{
			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);

			imageView = new UIImageView (UIImage.FromBundle ("Dining.png"));
			imageView.Center = ContentView.Center;
			imageView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);

			ContentView.AddSubview (imageView);
		}

		public void Configure (LocationPicture lp)
		{
			LocationPicture = lp;

			Task.Factory.StartNew (() => 
				ViewModel.GetLocationPicturePath (lp).
				ContinueWith (t => {
				if (t.Result != null && lp == LocationPicture) {
					InvokeOnMainThread (delegate {
						Image = UIImage.FromFile (t.Result);
					});
				}
			}));
		}

		public UIImage Image {
			set {
				imageView.Image = value;
			}
		}

	}
}

