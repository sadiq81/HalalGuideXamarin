using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MonoTouch.Foundation;

namespace HalalGuide.Util
{
	public static class Localization
	{

		private static readonly Dictionary<string, Dictionary<string, string>> Cache = new Dictionary<string, Dictionary<string, string>> ();

		public static string GetLocalizedValue (string key)
		{
			string locale = null;
			#if __IOS__
			locale = NSLocale.PreferredLanguages [0];
			#elif __ANDROID__

			#else
			throw new ArgumentException ("Platform not supported")
			#endif

			Dictionary<string, string> lang = null;

			if (!Cache.TryGetValue (locale, out lang)) {
				string file = string.Format ("Language/{0}.json", locale);
				if (!File.Exists (file)) {
					file = "en.json";
				}
				var json = File.ReadAllText (file);
				Cache.Add (locale, lang = JsonConvert.DeserializeObject<Dictionary<string, string>> (json));
			} 

			string value = null;
			if (lang.TryGetValue (key, out value)) {
				return value;
			} else {
				return null;
			}
		}
	}
}

