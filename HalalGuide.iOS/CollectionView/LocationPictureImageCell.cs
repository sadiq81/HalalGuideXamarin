﻿using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreGraphics;
using HalalGuide.Domain;
using HalalGuide.ViewModels;
using SimpleDBPersistence.Service;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;

namespace HalalGuide.iOS.CollectionView
{
	public class LocationPictureImageCell: UICollectionViewCell
	{
		public static NSString Indentifier = new NSString ("PictureCell");

		public SingleDiningViewModel ViewModel = ServiceContainer.Resolve<SingleDiningViewModel> ();

		public LocationPicture LocationPicture { get; set; }

		private bool IsExpanded { get; set; }

		public  UIImageView ImageView { get; set; }

		[Export ("initWithFrame:")]
		public LocationPictureImageCell (System.Drawing.RectangleF frame) : base (frame)
		{
			ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
			ContentView.Layer.BorderWidth = 2.0f;
			ContentView.BackgroundColor = UIColor.White;
			ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);
			ClipsToBounds = false;

			ImageView = new UIImageView (UIImage.FromBundle ("Dining.png"));
			ImageView.Center = ContentView.Center;
			ImageView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);
			ImageView.UserInteractionEnabled = true;

			ContentView.AddSubview (ImageView);
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
				ImageView.Image = value;
			}
		}

	}
}

