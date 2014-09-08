using System;
using Xamarin.Geolocation;
using SimpleDBPersistence.Service;
using HalalGuide.Services;
using System.Threading.Tasks;
using HalalGuide.Util;
using XUbertestersSDK;
using System.Collections.Generic;
using HalalGuide.Domain;
using Xamarin.Media;

namespace HalalGuide.ViewModels
{
	public abstract  class BaseViewModel
	{
		public event EventHandler RefreshLocationsCompletedEvent = delegate { };

		public event EventHandler LocationChangedEvent = delegate { };

		protected  Geolocator _Locator = ServiceContainer.Resolve<Geolocator> ();

		protected  KeyChainService _KeyChain = ServiceContainer.Resolve<KeyChainService> ();

		protected   AddressService _AddressService = ServiceContainer.Resolve<AddressService> ();

		protected   ImageService _ImageService = ServiceContainer.Resolve<ImageService> ();

		protected   LocationService _LocationService = ServiceContainer.Resolve<LocationService> ();

		protected   ReviewService _ReviewService = ServiceContainer.Resolve<ReviewService> ();

		protected   FacebookService _FacebookService = ServiceContainer.Resolve<FacebookService> ();

		public Location SelectedLocation{ get; set; }

		//------------------------------------------------------------------------

		protected static  MediaPicker MediaPicker = ServiceContainer.Resolve<MediaPicker> ();

		protected static Position Position { get; set; }

		protected readonly TaskScheduler UiScheduler = TaskScheduler.FromCurrentSynchronizationContext ();

		public BaseViewModel ()
		{
			if (_Locator.IsGeolocationAvailable && !_Locator.IsListening) {
				XUbertesters.LogInfo (string.Format ("BaseViewModel: started listening on location")); 
				_Locator.StartListening (10 * 60, 300);
			}

			_Locator.PositionChanged += (object sender, PositionEventArgs e) => {
				XUbertesters.LogInfo (string.Format ("BaseViewModel: Location changed")); 

				Position = e.Position;
				if (SelectedLocation != null) {
					SelectedLocation.Distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = SelectedLocation.Latitude,
						Longitude = SelectedLocation.Longtitude
					});
				}
				LocationChangedEvent (this, e);
			};
		}

		protected List<Location>  CalculateDistances (List<Location> locations, bool sortByDistance = false)
		{
			foreach (Location loc in locations) {

				if (Position != null) {
					double distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = loc.Latitude,
						Longitude = loc.Longtitude
					});
					loc.Distance = distance;
				}
			}

			if (sortByDistance) {
				locations.Sort ((x, y) => x.Distance.CompareTo (y.Distance));
			}

			return locations;
		}

		//TODO divide into smaller methods
		public async Task<string> GetFirstImagePathForLocation (Location location)
		{
			return await _ImageService.GetFirstImagePathForLocation (location);
		}

		public abstract void RefreshCache ();

		public async Task RefreshLocations ()
		{
			/*
			Location l0 = new Location () {
				Id = "0",
				Name = "Marco's Pizzabar",
				AddressRoad = "Hulgårdsvej",
				AddressRoadNumber = "7",
				AddressCity = "København N",
				AddressPostalCode = "2400",
				Latitude = 55.6951012,
				Longtitude = 12.5106906,
				Telephone = "00000000",
				HomePage = "www.currytakeaway.dk",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Pizza }.Select (cat => cat.Title).Aggregate ((current, next) => current + "," + next),
				NonHalal = true,
				Alcohol = true,
				Pork = true,
				CreationStatus = CreationStatus.Approved

			};

			_LocationService.SaveLocation (l0);

			Location l1 = new Location () {
				Id = "1",
				Name = "Curry Take Away",
				AddressRoad = "Borups Alle",
				AddressRoadNumber = "29",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Latitude = 55.6903656,
				Longtitude = 12.5428984,
				Telephone = "00000000",
				HomePage = "www.currytakeaway.dk",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Pakistani, DiningCategory.Indian }.Select (cat => cat.Title).Aggregate ((current, next) => current + "," + next),
				NonHalal = false,
				Alcohol = false,
				Pork = false,
				CreationStatus = CreationStatus.Approved
			};

			_LocationService.SaveLocation (l1);

			Location l2 = new Location () {
				Id = "2",
				Name = "Sultan's Café",
				AddressRoad = "Borups Alle",
				AddressRoadNumber = "112",
				AddressCity = "Frederiksberg",
				AddressPostalCode = "2000",
				Latitude = 55.6920414,
				Longtitude = 12.5352193,
				Telephone = "00000000",
				HomePage = "",
				LocationType = LocationType.Dining,
				Categories = new List<DiningCategory> (){ DiningCategory.Cafe }.Select (cat => cat.Title).Aggregate ((current, next) => current + "," + next),
				NonHalal = true,
				Alcohol = true,
				Pork = true,
				CreationStatus = CreationStatus.Approved

			};

			_LocationService.SaveLocation (l2);

			Location l3 = new Location () {
				Id = "3",
				Name = "Dansk Islamisk Råd",
				AddressRoad = "Vingelodden",
				AddressRoadNumber = "1",
				AddressCity = "København N",
				AddressPostalCode = "2200",
				Latitude = 55.7084999,
				Longtitude = 12.549223,
				Telephone = "00000000",
				HomePage = "http://www.disr.info/",
				LocationType = LocationType.Mosque,
				Language = Language.Danish,
				CreationStatus = CreationStatus.Approved
			};

			_LocationService.SaveLocation (l3);

			Location l4 = new Location () {
				Id = "4",
				Name = "Wakf",
				AddressRoad = "Dortheavej ",
				AddressRoadNumber = "45 - 47",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = 55.7083465,
				Longtitude = 12.5254281,
				Telephone = "00000000",
				HomePage = "http://www.wakf.com/",
				LocationType = LocationType.Mosque,
				Language = Language.Arabic,
				CreationStatus = CreationStatus.Approved
			};

			_LocationService.SaveLocation (l4);

			Location l5 = new Location () {
				Id = "5",
				Name = "Istanbul Bazar",
				AddressRoad = "Frederiksborgvej",
				AddressRoadNumber = "15",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = 55.702917,
				Longtitude = 12.532926,
				Telephone = "00000000",
				LocationType = LocationType.Shop,
				CreationStatus = CreationStatus.Approved
			};

			_LocationService.SaveLocation (l5);

			Location l6 = new Location () {
				Id = "6",
				Name = "J & B Supermarked ApS",
				AddressRoad = "Frederikssundsvej",
				AddressRoadNumber = "11",
				AddressCity = "København NV",
				AddressPostalCode = "2400",
				Latitude = 55.701255,
				Longtitude = 12.535705,
				Telephone = "00000000",
				LocationType = LocationType.Shop,
				CreationStatus = CreationStatus.Approved
			};

			_LocationService.SaveLocation (l6);

             */
			List<Location> locations = await _LocationService.RetrieveLatestLocations ();

			if (locations != null && locations.Count > 0) {
				RefreshCache ();
				RefreshLocationsCompletedEvent (this, EventArgs.Empty);
			}
		}

		public bool IsCameraAvailable ()
		{
			return MediaPicker.IsCameraAvailable;
		}

		public async Task<MediaFile> TakePicture (string path, string fileName)
		{
			MediaFile image = null;
			XUbertesters.LogInfo (String.Format ("BaseViewModel: TakePicture-Start with args path: {0} filename: {1}", path, fileName));

			XUbertesters.HideMenu ();
			await MediaPicker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = fileName,
				Directory = path
			}).ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					XUbertesters.LogError (String.Format ("BaseViewModel: TakePicture cancelled or faulted: {0}", t.Exception));
				} else {
					XUbertesters.LogError ("BaseViewModel: TakePicture ok");
					image = t.Result;
				}
			});
			XUbertesters.LogError ("BaseViewModel: TakePicture-End");
			return image;
		}

		public async Task<MediaFile> GetPictureFromDevice ()
		{
			MediaFile image = null;
			XUbertesters.LogInfo ("BaseViewModel: GetPictureFromDevice-Start");
			XUbertesters.HideMenu ();
			await MediaPicker.PickPhotoAsync ().ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					XUbertesters.LogError (String.Format ("BaseViewModel: GetPictureFromDevice cancelled or faulted: {0}", t.Exception));
				} else {
					XUbertesters.LogError ("BaseViewModel: GetPictureFromDevice ok");
					image = t.Result;
				}
			});
			XUbertesters.LogError ("BaseViewModel: GetPictureFromDevice-End");
			return image;
		}
	}
}

