using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Linq;

namespace HalalGuide.Util
{
	internal static class StringUtils
	{
		public static string FirstToUpper (this string s)
		{
			if (string.IsNullOrEmpty (s)) {
				return string.Empty;
			}
			// Return char and concat substring.
			return char.ToUpper (s [0]) + s.Substring (1).ToLower ();
		}

		public static string RemoveLastChar (this string s)
		{
			if (string.IsNullOrEmpty (s)) {
				return string.Empty;
			}
			return s.Remove (s.Length - 1);
		}

		public static string RemoveCharFromEnd (this string s, int numberOfChars)
		{
			if (string.IsNullOrEmpty (s)) {
				return string.Empty;
			}
			return s.Remove (s.Length - numberOfChars);
		}

		public static string Compress (string s)
		{
			var bytes = Encoding.Unicode.GetBytes (s);
			using (var msi = new MemoryStream (bytes))
			using (var mso = new MemoryStream ()) {
				using (var gs = new GZipStream (mso, CompressionMode.Compress)) {
					msi.CopyTo (gs);
				}
				return Convert.ToBase64String (mso.ToArray ());
			}
		}

		public static string Decompress (string s)
		{
			var bytes = Convert.FromBase64String (s);
			using (var msi = new MemoryStream (bytes))
			using (var mso = new MemoryStream ()) {
				using (var gs = new GZipStream (msi, CompressionMode.Decompress)) {
					gs.CopyTo (mso);
				}
				return Encoding.Unicode.GetString (mso.ToArray ());
			}
		}
	}
}

