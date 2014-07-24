using System;
using Xamarin.Geolocation;
using SimpleDBPersistence.Service;

namespace HalalGuide.ViewModels
{
	public abstract class BaseViewModel
	{
		public event EventHandler IsBusyChanged = delegate { };

		public event EventHandler LocationChangedEvent = delegate { };

		public Geolocator Locator = ServiceContainer.Resolve<Geolocator> ();

		public BaseViewModel ()
		{
			if (Locator.IsGeolocationAvailable && !Locator.IsListening) {
				Locator.StartListening (10 * 60, 300);
			}

			Locator.PositionChanged += (object sender, PositionEventArgs e) => {
				LocationChanged (this, e);
			};
		}

		protected virtual void LocationChanged (object sender, PositionEventArgs e)
		{
			LocationChangedEvent (sender, e);
		}



		private bool isBusy = false;

		public bool IsBusy {
			get { return isBusy; }
			set {
				isBusy = value;
				IsBusyChanged (this, EventArgs.Empty);
			}
		}
	}
}

