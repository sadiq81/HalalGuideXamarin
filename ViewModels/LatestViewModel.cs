using HalalGuide.DAO;
using System.Collections.Generic;
using HalalGuide.Domain;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using System.Threading.Tasks;
using HalalGuide.Domain.Enum;
using System;

namespace HalalGuide.ViewModels
{
	public class LatestViewModel : BaseViewModel, ITableViewModel
	{
		private readonly static LocationDAO DAO = SimpleDBPersistence.Service.ServiceContainer.Resolve<LocationDAO> ();

		private List<Location> List = new List<Location> ();

		public LatestViewModel () : base ()
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

		public async Task Update ()
		{
			/*
			Location l0 = new Location () {
				Id = "0",
				Name = "Marco's Pizzabar",
				AddressRoad = "Hulgårdsvej",
				AddressRoadNumber = "7",
				AddressCity = "København N",
				AddressPostalCode = "2400",
				Latitude = "55.6951012",
				Longtitude = "12.5106906",
				Telephone = "00000000",
				HomePage = "www.currytakeaway.dk",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Pizza },
				NonHalal = true,
				Alcohol = true,
				Pork = true,
				LocationStatus = LocationStatus.Approved
				
			};

			await DAO.SaveOrReplace (l0);

			Location l1 = new Location () {
				Id = "1",
				Name = "Curry Take Away",
				AddressRoad = "Borups Alle",
				AddressRoadNumber = "29",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Latitude = "55.6903656",
				Longtitude = "12.5428984",
				Telephone = "00000000",
				HomePage = "www.currytakeaway.dk",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Pakistani, DiningCategory.Indian },
				NonHalal = false,
				Alcohol = false,
				Pork = false,
				LocationStatus = LocationStatus.Approved
			};

			await DAO.SaveOrReplace (l1);

			Location l2 = new Location () {
				Id = "2",
				Name = "Sultan's Café",
				AddressRoad = "Borups Alle",
				AddressRoadNumber = "112",
				AddressCity = "Frederiksberg",
				AddressPostalCode = "2000",
				Latitude = "55.6920414",
				Longtitude = "12.5352193",
				Telephone = "00000000",
				HomePage = "",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Cafe },
				NonHalal = true,
				Alcohol = true,
				Pork = true,
				LocationStatus = LocationStatus.Approved

			};

			await DAO.SaveOrReplace (l2);

			Location l3 = new Location () {
				Id = "3",
				Name = "Dansk Islamisk Råd",
				AddressRoad = "Vingelodden",
				AddressRoadNumber = "1",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Latitude = "55.7084999",
				Longtitude = "12.549223",
				Telephone = "00000000",
				HomePage = "http://www.disr.info/",
				LocationType = LocationType.Mosque,
				Language = Language.Danish,
				LocationStatus = LocationStatus.Approved
			};

			await DAO.SaveOrReplace (l3);

			Location l4 = new Location () {
				Id = "4",
				Name = "Wakf",
				AddressRoad = "Dortheavej ",
				AddressRoadNumber = "45 - 47",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = "55.7083465",
				Longtitude = "12.5254281",
				Telephone = "00000000",
				HomePage = "http://www.wakf.com/",
				LocationType = LocationType.Mosque,
				Language = Language.Arabic,
				LocationStatus = LocationStatus.Approved
			};

			await DAO.SaveOrReplace (l4);

			Location l5 = new Location () {
				Id = "5",
				Name = "Istanbul Bazar",
				AddressRoad = "Frederiksborgvej",
				AddressRoadNumber = "15",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = "55.702917",
				Longtitude = "12.532926",
				Telephone = "00000000",
				LocationType = LocationType.Shop,
				LocationStatus = LocationStatus.Approved
			};

			await DAO.SaveOrReplace (l5);

			Location l6 = new Location () {
				Id = "6",
				Name = "J & B Supermarked ApS",
				AddressRoad = "Frederikssundsvej",
				AddressRoadNumber = "11",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = "55.701255",
				Longtitude = "12.535705",
				Telephone = "00000000",
				LocationType = LocationType.Shop,
				LocationStatus = LocationStatus.Approved
			};

			await DAO.SaveOrReplace (l6);

            */

			SelectQuery<Location> query = new SelectQuery<Location> ();
			query.Equal ("LocationStatus", LocationStatus.Approved.ToString ());
			query.NotNull ("Updated");
			query.SortOrder = "Updated";
			query.Limit = 10;

			List = await DAO.Select (query);

			OnLoadedListEvent (EventArgs.Empty);
		}

	}
}

