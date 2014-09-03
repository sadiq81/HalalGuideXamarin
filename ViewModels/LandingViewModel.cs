using System.Collections.Generic;
using HalalGuide.Domain;
using System.Linq;
using HalalGuide.Domain.Enum;
using System;

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
			Cache = CalculateDistances (_LocationService.GetLastTenLocations (), true);
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

