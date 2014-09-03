using System;
using System.Net.NetworkInformation;

#if __IOS__
using MonoTouch.SystemConfiguration;
using MonoTouch.Foundation;

#elif __ANDROID__
using Android.Content;

#endif

namespace HalalGuide.Services
{
	public  class PreferencesService
	{
		public const  string PreferencesIdentifier = "HalalGuide";

		public  string GetString (string key)
		{
			#if __IOS__

			return NSUserDefaults.StandardUserDefaults.StringForKey (key);

			#elif __ANDROID__

			var prefs = Application.Context.GetSharedPreferences(PreferencesIdentifier, FileCreationMode.Private);              
			return prefs.GetString(key, null);

			#endif

		}

		public  void StoreString (string key, string value)
		{
			#if __IOS__

			NSUserDefaults.StandardUserDefaults.SetString (value, key); 

			#elif __ANDROID__

			var prefs = Application.Context.GetSharedPreferences(PreferencesIdentifier, FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutString(key, value);
			prefEditor.Commit();
			#endif
		}
	}
}

