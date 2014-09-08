using System;

#if __IOS__
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

#elif __ANDROID__
using Android.Graphics;
#endif

namespace HalalGuide.Util
{
	public class ImageResize
	{
		// resize the image to be contained within a maximum width and height, keeping aspect ratio
		public static byte [] MaxResizeImage (byte[] data, float maxWidth, float maxHeight)
		{
			#if __IOS__
			UIImage sourceImage = UIImage.LoadFromData (NSData.FromArray (data));
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max (maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1)
				return sourceImage.AsJPEG ().ToArray ();
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			sourceImage.Draw (new RectangleF (0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage.AsJPEG ().ToArray ();
			#elif __ANDROID__
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeByteArray (data, 0, data.Length);

			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > maxHeight || outWidth > maxWidth) {
				inSampleSize = (int)(outWidth > outHeight ? outHeight / maxHeight : outWidth / maxWidth);
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeByteArray (data, 0, data.Length);

			var ms = new MemoryStream ();
			resizedBitmap.Compress (Bitmap.CompressFormat.Png, 0, ms);
			var iconBytes = ms.ToArray ();
			#else 
			throw new ArgumentException ("Platform not supported");
			#endif

		}

		// resize the image (without trying to maintain aspect ratio)
		public static  byte[] ResizeImage (byte[] data, float width, float height)
		{
			#if __IOS__
			UIImage sourceImage = UIImage.LoadFromData (NSData.FromArray (data));
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			sourceImage.Draw (new RectangleF (0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return resultImage.AsJPEG ().ToArray ();
			#elif __ANDROID__

			#else 
			throw new ArgumentException ("Platform not supported");
			#endif
		}

		// crop the image, without resizing
		private  static byte[] CropImage (byte[] data, int crop_x, int crop_y, int width, int height)
		{
			#if __IOS__
			UIImage sourceImage = UIImage.LoadFromData (NSData.FromArray (data));
			var imgSize = sourceImage.Size;
			UIGraphics.BeginImageContextWithOptions (new SizeF (width, height), false, 2.0f);
			var context = UIGraphics.GetCurrentContext ();
			var clippedRect = new RectangleF (0, 0, width, height);
			context.ClipToRect (clippedRect);
			var drawRect = new RectangleF (-crop_x, -crop_y, imgSize.Width, imgSize.Height);
			sourceImage.Draw (drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			return modifiedImage.AsJPEG ().ToArray ();
			#elif __ANDROID__

			#else 
			throw new ArgumentException ("Platform not supported");
			#endif
		}
	}
}

