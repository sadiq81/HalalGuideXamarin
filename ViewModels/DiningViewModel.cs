using HalalGuide.DAO;
using HalalGuide.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using Xamarin.Geolocation;
using HalalGuide.Util;

namespace HalalGuide.ViewModels
{
	public class DiningViewModel : BaseViewModel, ITableViewModel
	{
		private readonly static LocationDAO DAO = SimpleDBPersistence.Service.ServiceContainer.Resolve<LocationDAO> ();

		private List<Location> List = new List<Location> ();

		public DiningViewModel () : base ()
		{

		}

		public int Rows ()
		{
			return List.Count;
		}

		public Location GetLocationAtRow (int row)
		{
			return List [row];
		}

		protected override void LocationChanged (object sender, PositionEventArgs e)
		{
			SortedList<double,Location> list = new SortedList<double, Location> ();

			foreach (Location location in List) {
				double distance = CalcUtil.GetDistanceKM (e.Position, new Position () {
					Latitude = double.Parse (location.Latitude),
					Longitude = double.Parse (location.Longtitude)
				});
				location.Distance = distance;
				list.Add (distance, location);
			}

			List = new List<Location> (list.Values);

			base.LocationChanged (sender, e);
		}

		public async Task Update ()
		{
			IsBusy = true;

			SelectQuery<Location> query = new SelectQuery<Location> ();
			query.Equal ("LocationType", "DINING");
			query.NotNull ("Updated");
			query.SortOrder = "Updated";

			List = await DAO.Select (query);

			IsBusy = false;

		}

	}
}

