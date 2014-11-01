using System;
using HalalGuide.Domain.Enums;
using HalalGuide.Domain;
using HalalGuide.DAO;
using System.Collections.Generic;
using HalalGuide.Util;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;

namespace HalalGuide.Services
{
	public class LocationService
	{
		private LocationDAO locationDAO { get { return ServiceContainer.Resolve<LocationDAO> (); } }

		private  DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		private  PreferencesService preferences { get { return ServiceContainer.Resolve<PreferencesService> (); } }

		public LocationService ()
		{
		}

		public async  Task SaveLocation (Location location)
		{
			await locationDAO.SaveOrReplace (location);
		}

		public async Task RetrieveLatestLocations ()
		{
			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat, CultureInfo.InvariantCulture);

			string lastUpdatedString = preferences.GetString (Constants.LocationLastUpdated);

			DateTime updatedLast = DateTime.ParseExact (lastUpdatedString, Constants.DateFormat, CultureInfo.InvariantCulture);

			Console.WriteLine (updatedLast);

			await locationDAO.Where (loc => loc.updatedAt > updatedLast);
		
			preferences.StoreString (Constants.LocationLastUpdated, updatedTime);
		}

		public List<Location> RetrieveAllLocations ()
		{
			List<Location> locations = database.Table<Location> ().Where (l => l.deleted == false).ToList ();
			return locations;
		}

		public List<Location> LocationsByQuery (Expression<Func<Location, bool>> predicate)
		{
			List<Location> locations = database.Table<Location> ().Where (predicate).ToList ();
			return locations;
		}


		public List<Location> GetLastTenLocations ()
		{
			return database.Table<Location> ().Where (l => l.creationStatus == CreationStatus.Approved && l.deleted == false).OrderByDescending (l => l.updatedAt).Take (10).ToList ();
		}
	}
}

