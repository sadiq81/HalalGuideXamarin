using System.Collections.Generic;
using HalalGuide.Domain;
using System;
using System.Threading.Tasks;
using HalalGuide.Util;
using HalalGuide.Domain.Enums;

namespace HalalGuide.ViewModels
{
	public sealed class LandingViewModel : BaseViewModel
	{
		private  List<Location> Cache = new List<Location> ();

		public LandingViewModel () : base ()
		{
		}

		public override void RefreshCache ()
		{
			Cache = locationService.GetLastTenLocations ();
			CalculateDistances ();
		}

		public void CalculateDistances ()
		{
			Cache = CalculateDistances (Cache, false);
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

