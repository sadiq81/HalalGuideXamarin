using System;
using Xamarin.Geolocation;
using HalalGuide.Services;
using System.Threading.Tasks;
using HalalGuide.Util;
using System.Collections.Generic;
using HalalGuide.Domain;
using HalalGuide.Domain.Enums;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Media;
using MonoTouch.CoreVideo;

namespace HalalGuide.ViewModels
{
	public abstract  class BaseViewModel
	{
		public event EventHandler locationChangedEvent = delegate { };

		public event EventHandler refreshedLocations = delegate { };

		public event EventHandler refreshedLocationPictures = delegate { };

		public event EventHandler refreshedReviews = delegate { };

		public static event EventHandler<ProgressResult> busy = delegate{};

		protected Geolocator geoService { get { return ServiceContainer.Resolve<Geolocator> (); } }

		protected KeyChainService keychainService { get { return ServiceContainer.Resolve<KeyChainService> (); } }

		protected AddressService addressService { get { return ServiceContainer.Resolve<AddressService> (); } }

		protected LocationService locationService { get { return ServiceContainer.Resolve<LocationService> (); } }

		protected ImageService imageService { get { return ServiceContainer.Resolve<ImageService> (); } }

		protected ReviewService reviewService { get { return ServiceContainer.Resolve<ReviewService> (); } }

		protected FacebookService facebookService { get { return ServiceContainer.Resolve<FacebookService> (); } }

		protected DatabaseWrapper databaseService { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		protected MobileServiceClient azureService { get { return ServiceContainer.Resolve<MobileServiceClient> (); } }

		protected MediaPicker mediaPicker { get { return ServiceContainer.Resolve<MediaPicker> (); } }

		public Location selectedLocation{ get; set; }



		//------------------------------------------------------------------------

		protected static Position Position { get; set; }

		public BaseViewModel ()
		{
			if (geoService.IsGeolocationAvailable && !geoService.IsListening) {
				geoService.StartListening (10 * 60, 300);
			}

			geoService.PositionChanged += (object sender, PositionEventArgs e) => {
				Position = e.Position;
				if (selectedLocation != null) {
					selectedLocation.distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = selectedLocation.latitude,
						Longitude = selectedLocation.longtitude
					});
				}
				locationChangedEvent (this, e);
			};
		}

		static void OnBusy (ProgressResult e)
		{
			var handler = busy;
			if (handler != null)
				handler (null, e);
		}


		protected List<Location>  CalculateDistances (List<Location> locations, bool sortByDistance = false)
		{
			foreach (Location loc in locations) {

				if (Position != null) {
					double distance = CalcUtil.GetDistanceKM (Position, new Position () {
						Latitude = loc.latitude,
						Longitude = loc.longtitude
					});
					loc.distance = distance;
				}
			}

			if (sortByDistance) {
				locations.Sort ((x, y) => x.distance.CompareTo (y.distance));
			}
			return locations;
		}

		public virtual async Task RefreshLocations ()
		{
			await locationService.RetrieveLatestLocations ();
			RefreshCache ();
			refreshedLocations (this, EventArgs.Empty);
		}

		public virtual async Task RefreshLocationPictures ()
		{
			await imageService.RetrieveLatestLocationPictures ();
			refreshedLocationPictures (this, EventArgs.Empty);
		}

		public virtual void RefreshCache ()
		{
		}

		public async Task<string> GetFirstImageUriForLocation (Location location)
		{
			return "";
		}

		public bool IsAuthenticated ()
		{
			return keychainService.IsAuthenticated ();
		}

		public async Task<MediaFile> TakePicture (Location locationForPicture)
		{
			MediaFile image = null;
			await mediaPicker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = Guid.NewGuid () + ".jpeg",
				Directory = FileService.GetTempPath ()
			}).ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
				} else {
					image = t.Result;
				}
			});
			return image;
		}

		public async Task<MediaFile> GetPictureFromDevice (Location locationForPicture)
		{
			MediaFile image = null; 
			await mediaPicker.PickPhotoAsync ().ContinueWith (async t => {
				if (t.IsCanceled || t.IsFaulted) {
				} else {
					image = t.Result;
					if (locationForPicture != null) {
						OnBusy (ProgressResult.GetInstance ("Uploader billede", ProgressType.ShowWithText));
						await imageService.UploadImageForLocation (locationForPicture, image);
						OnBusy (ProgressResult.GetInstance (null, ProgressType.Dismiss));
					}
				}
			});
			return image;
		}




		public async Task CreateDummyData ()
		{


			Location l0 = new Location () {
				name = "Marco's Pizzabar",
				addressRoad = "Hulgårdsvej",
				addressRoadNumber = "7",
				addressPostalCode = "2400",
				addressCity = "København N",
				latitude = 55.6951012,
				longtitude = 12.5106906,
				telephone = "00000000",
				homePage = "www.currytakeaway.dk",
				locationType = LocationType.Dining,
				categories = new List<DiningCategory> (){ DiningCategory.pizza },
				nonHalal = true,
				alcohol = true,
				pork = true,
				language = Language.None,
				creationStatus = CreationStatus.Approved,
				submitterId = ""
			};


			await locationService.SaveLocation (l0);


			Location l1 = new Location () {
				name = "Curry Take Away",
				addressRoad = "Borups Alle",
				addressRoadNumber = "29",
				addressCity = "København N",
				addressPostalCode = "2200",
				latitude = 55.6903656,
				longtitude = 12.5428984,
				telephone = "00000000",
				homePage = "www.currytakeaway.dk",
				locationType = LocationType.Dining,
				categories = new List<DiningCategory> (){ DiningCategory.pakistani, DiningCategory.indian },
				nonHalal = false,
				alcohol = false,
				pork = false,
				creationStatus = CreationStatus.Approved
			};

			await locationService.SaveLocation (l1);

			Location l2 = new Location () {
				name = "Sultan's Café",
				addressRoad = "Borups Alle",
				addressRoadNumber = "112",
				addressCity = "Frederiksberg",
				addressPostalCode = "2000",
				latitude = 55.6920414,
				longtitude = 12.5352193,
				telephone = "00000000",
				homePage = "",
				locationType = LocationType.Dining,
				categories = new List<DiningCategory> () { DiningCategory.cafe },
				nonHalal = true,
				alcohol = true,
				pork = true,
				creationStatus = CreationStatus.Approved

			};

			await locationService.SaveLocation (l2);

			Location l3 = new Location () {
				name = "Dansk Islamisk Råd",
				addressRoad = "Vingelodden",
				addressRoadNumber = "1",
				addressCity = "København N",
				addressPostalCode = "2200",
				latitude = 55.7084999,
				longtitude = 12.549223,
				telephone = "00000000",
				homePage = "http://www.disr.info/",
				locationType = LocationType.Mosque,
				language = Language.Danish,
				creationStatus = CreationStatus.Approved
			};

			await locationService.SaveLocation (l3);

			Location l4 = new Location () {
				name = "Wakf",
				addressRoad = "Dortheavej ",
				addressRoadNumber = "45 - 47",
				addressCity = "København NV",
				addressPostalCode = "2400",
				latitude = 55.7083465,
				longtitude = 12.5254281,
				telephone = "00000000",
				homePage = "http://www.wakf.com/",
				locationType = LocationType.Mosque,
				language = Language.Arabic,
				creationStatus = CreationStatus.Approved
			};

			await locationService.SaveLocation (l4);

			Location l5 = new Location () {
				name = "Istanbul Bazar",
				addressRoad = "Frederiksborgvej",
				addressRoadNumber = "15",
				addressCity = "København NV",
				addressPostalCode = "2400",
				latitude = 55.702917,
				longtitude = 12.532926,
				telephone = "00000000",
				locationType = LocationType.Shop,
				creationStatus = CreationStatus.Approved
			};

			await locationService.SaveLocation (l5);

			Location l6 = new Location () {
				name = "J & B Supermarked ApS",
				addressRoad = "Frederikssundsvej",
				addressRoadNumber = "11",
				addressCity = "København NV",
				addressPostalCode = "2400",
				latitude = 55.701255,
				longtitude = 12.535705,
				telephone = "00000000",
				locationType = LocationType.Shop,
				creationStatus = CreationStatus.Approved
			};

			await locationService.SaveLocation (l6);

		}
	}
}

