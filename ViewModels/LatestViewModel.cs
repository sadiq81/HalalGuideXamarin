using System;
using HalalGuide.DAO;
using System.Net;
using System.Collections.Generic;
using HalalGuide.Domain;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using System.Threading.Tasks;
using System.Globalization;
using System.Security.Cryptography;

namespace HalalGuide.ViewModels
{
	public class LatestViewModel : BaseViewModel
	{
		private readonly static LocationDAO DAO = SimpleDBPersistence.Service.ServiceContainer.Resolve<LocationDAO> ();

		private List<Location> List = new List<Location> ();

		public LatestViewModel ()
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
			Location l1 = new Location () {
				Id = "1",
				Name = "Curry Take Away",
				AddressRoad = "Borups Alle 29",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Telephone = "00000000",
				HomePage = "www.currytakeaway.dk",
				LocationType = LocationType.DINING,
				Halal = true,
				Alcohol = false,
				Pork = false,
				Categories = new List<string> (){ "Pakistansk", "Indisk", "Krydret" }
			};

			await DAO.SaveOrReplace (l1);

			Location l2 = new Location () {
				Id = "2",
				Name = "Cafe Fatamorgana",
				AddressRoad = "Borups Plads 26",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Telephone = "00000000",
				HomePage = "",
				LocationType = LocationType.DINING,
				Halal = true,
				Alcohol = true,
				Pork = true,
				Categories = new List<string> (){ "Cafe", "Brunch", "Tyrkisk" }
			};

			await DAO.SaveOrReplace (l2);

			Location l3 = new Location () {
				Id = "3",
				Name = "Dansk Islamisk Råd",
				AddressRoad = "Vingelodden 1",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Telephone = "00000000",
				HomePage = "http://www.disr.info/",
				LocationType = LocationType.MOSQUE,
				Language = Language.DANISH
			};

			await DAO.SaveOrReplace (l3);

			Location l4 = new Location () {
				Id = "4",
				Name = "Wakf",
				AddressRoad = "Dortheavej 45 - 47",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Telephone = "00000000",
				HomePage = "http://www.wakf.com/",
				LocationType = LocationType.MOSQUE,
				Language = Language.ARABIC
			};

			await DAO.SaveOrReplace (l4);

			Location l5 = new Location () {
				Id = "5",
				Name = "Istanbul Bazar",
				AddressRoad = "Frederiksborgvej 15",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Telephone = "00000000",
				LocationType = LocationType.SHOP,
			};

			await DAO.SaveOrReplace (l5);

			Location l6 = new Location () {
				Id = "6",
				Name = "J & B Supermarked ApS",
				AddressRoad = "Frederikssundsvej 11",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Telephone = "00000000",
				LocationType = LocationType.SHOP,
			};

			await DAO.SaveOrReplace (l6);

            */
			IsBusy = true;

			SelectQuery<Location> query = new SelectQuery<Location> ();
			query.NotNull ("Updated");
			query.SortOrder = "Updated";
			query.Limit = 10;

			List = await DAO.Select (query);

			IsBusy = false;

		}

	}
}

