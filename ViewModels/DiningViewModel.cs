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

namespace HalalGuide.ViewModels
{
	public class DiningViewModel : BaseViewModel, ITableViewModel
	{
		private readonly static LocationDAO DAO = SimpleDBPersistence.Service.ServiceContainer.Resolve<LocationDAO> ();


		private List<Location> List = new List<Location> ();
		private List<Location> Filtered = new List<Location> ();

		public  List<DiningCategory> CategoryFilter { get; set; }

		public double DistanceFilter { get; set; }

		public DiningViewModel () : base ()
		{
			DistanceFilter = 5;
			CategoryFilter = new List<DiningCategory> ();
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
			List<Location> temp = new List<Location> ();

			foreach (Location loc in List) {

				if (Position != null) {
					double distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = double.Parse (loc.Latitude, CultureInfo.InvariantCulture),
						Longitude = double.Parse (loc.Longtitude, CultureInfo.InvariantCulture)
					});
					loc.Distance = distance;
				}

				bool ok = true;

				ok &= CategoryFilter.Count == 0 || CategoryFilter.Intersect (loc.Categories).Any ();

				ok &= loc.Distance.CompareTo (0) == 0 || loc.Distance < DistanceFilter;

				if (ok) {
					temp.Add (loc);
				}
			}
			temp.OrderBy (l => l.Distance);
			Filtered = new List<Location> (temp);
		}

		protected override void LocationChanged (object sender, PositionEventArgs e)
		{
			IsBusy = true;
			Position = e.Position;

			base.LocationChanged (sender, e);

			FilterLocations ();

			IsBusy = false;
		}

		public async Task Update ()
		{
			IsBusy = true;

			SelectQuery<Location> query = new SelectQuery<Location> ();
			query.Equal ("LocationType", LocationType.Dining.ToString ());
			query.NotNull ("Updated");
			query.SortOrder = "Updated";

			List = await DAO.Select (query);

			FilterLocations ();

			IsBusy = false;


		}

	}
		
}

