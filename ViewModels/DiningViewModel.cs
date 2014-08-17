using HalalGuide.DAO;
using HalalGuide.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using Xamarin.Geolocation;
using HalalGuide.Util;
using HalalGuide.Domain.Enum;
using System.Linq;
using System.Globalization;
using System;
using XUbertestersSDK;

namespace HalalGuide.ViewModels
{
	public class DiningViewModel : BaseViewModel, ITableViewModel
	{
		private List<Location> List = new List<Location> ();
		private List<Location> Filtered = new List<Location> ();

		public  List<DiningCategory> CategoryFilter { get; set; }

		public double DistanceFilter { get; set; }

		public bool PorkFilter { get; set; }

		public bool AlcoholFilter { get; set; }

		public bool HalalFilter { get; set; }

		public DiningViewModel () : base ()
		{
			DistanceFilter = 5;
			CategoryFilter = new List<DiningCategory> ();
			PorkFilter = true;
			AlcoholFilter = true;
			HalalFilter = true;

			LocationChangedEvent += (sender, e) => FilterLocations ();
		}

		public int Rows ()
		{
			return Filtered.Count;
		}

		public Location GetLocationAtRow (int row)
		{
			return Filtered [row];
		}

		private void FilterLocations ()
		{

			XUbertesters.LogInfo (string.Format ("DiningViewModel: Filtering locations, before: {0}", List.Count));

			List<Location> temp = new List<Location> ();

			CalculateDistances (ref List);

			foreach (Location loc in List) {

				bool ok = true;

				ok &= CategoryFilter.Count == 0 || CategoryFilter.Intersect (loc.Categories).Any ();

				ok &= loc.Distance.CompareTo (0) == 0 || loc.Distance < DistanceFilter;


				ok &= PorkFilter || PorkFilter == loc.Pork; 

				ok &= AlcoholFilter || AlcoholFilter == loc.Alcohol; 

				ok &= HalalFilter || HalalFilter == loc.NonHalal;


				if (ok) {
					temp.Add (loc);
				}
			}
			temp = temp.OrderBy (l => l.Distance).ToList ();

			Filtered = new List<Location> (temp);

			XUbertesters.LogInfo (string.Format ("DiningViewModel: Filtering locations, after: {0} with args DistanceFilter: {1} PorkFilter: {2} AlcoholFilter: {3} HalalFilter: {4}", Filtered.Count, DistanceFilter, PorkFilter, AlcoholFilter, HalalFilter));

			OnLoadedListEvent (EventArgs.Empty);
		}

		public async Task Update ()
		{
			SelectQuery<Location> query = new SelectQuery<Location> ();
			query.Equal ("LocationStatus", LocationStatus.Approved.ToString ());
			query.Equal ("LocationType", LocationType.Dining.ToString ());
			query.NotNull ("Updated");
			query.SortOrder = "Updated";

			List = await LocationDAO.Select (query);

			FilterLocations ();
		}

	}
		
}

