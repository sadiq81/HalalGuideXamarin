using System;
using S3Storage.S3;
using SimpleDBPersistence.SimpleDB;

namespace HalalGuide.ViewModels
{
	public class BaseViewModel
	{
		public event EventHandler IsBusyChanged = delegate { };

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

