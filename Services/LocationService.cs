using System;
using HalalGuide.Domain.Enum;
using HalalGuide.Domain;
using HalalGuide.DAO;
using SimpleDBPersistence.Service;
using XUbertestersSDK;
using SimpleDBPersistence.SimpleDB.Model.AWSException;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using System.Collections.Generic;
using HalalGuide.Util;
using SimpleDBPersistence.Domain;
using System.Threading.Tasks;
using System.Linq;

namespace HalalGuide.Services
{
	public class LocationService
	{
		private LocationDAO _LocationDAO = ServiceContainer.Resolve<LocationDAO> ();

		private  DatabaseWrapper _SQLiteConnection = ServiceContainer.Resolve<DatabaseWrapper> ();

		private  PreferencesService _PreferencesService = ServiceContainer.Resolve<PreferencesService> ();

		public LocationService ()
		{
		}

		public async  Task<CreateEntityResult> SaveLocation (Location location)
		{
			try {
				await _LocationDAO.SaveOrReplace (location);
				_SQLiteConnection.Insert (location);
			} catch (AWSErrorException e) {
				XUbertesters.LogError ("LocationService: CouldNotCreateEntityInSimpleDB: " + e + " Entity: " + location);
				return CreateEntityResult.CouldNotCreateEntityInSimpleDB;
			}
			return CreateEntityResult.OK;
		}

		public async  Task<CreateEntityResult> DeleteLocation (Location location)
		{
			try {
				await _LocationDAO.Delete (location);
				_SQLiteConnection.Delete (location);
			} catch (AWSErrorException e) {
				XUbertesters.LogError ("LocationService: CouldNotDeleteEntityInSimpleDB: " + e + " Entity: " + location);
				return CreateEntityResult.CouldNotCreateEntityInSimpleDB;
			}
			return CreateEntityResult.OK;
		}

		public async Task<List<Location>> RetrieveLatestLocations ()
		{
			XUbertesters.LogInfo (string.Format ("LocationService-RetrieveLatestLocations"));

			string updatedTime = DateTime.UtcNow.ToString (Constants.DateFormat);
			string lastUpdated = _PreferencesService.GetString (Constants.LocationLastUpdated);

			SelectQuery<Location> locationQuery = new SelectQuery<Location> ()
				.GreatherThanOrEqual (Entity.UpdatedIdentifier, lastUpdated)
				.NotNull (Entity.UpdatedIdentifier)
				.SetSortOrder (Entity.UpdatedIdentifier);

			List<Location> locations = await _LocationDAO.Select (locationQuery);

			if (locations != null && locations.Count > 0) {
				XUbertesters.LogInfo (string.Format ("LocationService-RetrieveLatestLocations: found: {0}", locations.Count));
				locations.ForEach (_SQLiteConnection.InsertOrReplace);
			}

			_PreferencesService.StoreString (Constants.LocationLastUpdated, updatedTime);

			return locations;
		}


		public List<Location> GetLastTenLocations ()
		{
			return _SQLiteConnection.Table<Location> ().Where (l => l.CreationStatus == CreationStatus.Approved).OrderBy (l => l.LastUpdated).Take (10).ToList ();
		}

		public List<Location> GetByQuery (string query)
		{
			return _SQLiteConnection.Query<Location> (query);
		}
	}
}

