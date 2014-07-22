using System;

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
	}
}

