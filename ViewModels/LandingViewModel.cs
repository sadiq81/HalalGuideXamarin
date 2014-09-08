using System.Collections.Generic;
using HalalGuide.Domain;

namespace HalalGuide.ViewModels
{
	public sealed class LandingViewModel : BaseViewModel
	{
		private  List<Location> Cache = new List<Location> ();

		public LandingViewModel () : base ()
		{
			RefreshCache ();
		}

		public override void RefreshCache ()
		{
			Cache = CalculateDistances (_LocationService.GetLastTenLocations (), false);
		}

		public int Rows ()
		{
			return Cache.Count;
		}

		public Location GetLocationAtRow (int row)
		{
			return  Cache [row];
		}
	}
}

