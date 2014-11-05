using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using HalalGuide.Domain;
using HalalGuide.Domain.Enums;
using HalalGuide.Domain.Dawa;
using System.Linq;
using System.Globalization;
using Xamarin.Media;
using HalalGuide.Services;

namespace HalalGuide.ViewModels
{
	public class AddDiningViewModel : BaseViewModel
	{
		protected MediaPicker picker { get { return ServiceContainer.Resolve<MediaPicker> (); } }

		private Dictionary<string, Address> StreetNumbersMap { get; set; }

		public AddDiningViewModel () : base ()
		{
			StreetNumbersMap = new Dictionary<string, Address> ();
		}

		public List<string> StreetNames ()
		{
			return new List<string> (StreetNumbersMap.Keys);
		}

		public List<string> StreetNumbers (string roadName)
		{
			Address address;
			StreetNumbersMap.TryGetValue (roadName, out address);
			if (address != null) {
				return address.StreetNumbers;
			} else {
				return new List<string> ();
			}
		}

		public string PostalCode (string roadName)
		{
			Address address;
			StreetNumbersMap.TryGetValue (roadName, out address);
			if (address != null) {
				return address.PostalCode;
			} else {
				return null;
			}
		}

		public async Task<string> GetCityNameFromPostalCode (string postalcode)
		{
			if (postalcode != null && postalcode.Length == 4 && Regex.IsMatch (postalcode, @"^[0-9]+$")) {
				var name = await addressService.GetNameOfPostDistrict (postalcode);
				return name;
			} else {
				return null;
			}
		}

		public async Task<Adgangsadresse> DoesAddressExists (string roadName, string roadNumber, string postalCode)
		{
			return await addressService.DoesAddressExits (roadName, roadNumber, postalCode);
		}

		//TODO Make sure position is known before calling this function
		public async Task LoadAddressNearPosition ()
		{
			Dictionary<string, Address> temp = new Dictionary<string, Address> ();

			List<Adgangsadresse> adresses = await addressService.AddressNearPosition (Position, 150);

			if (adresses != null) {
			
				foreach (Adgangsadresse address in adresses) {

					Address current = null;
					string streetName = address.Vejstykke.Navn;
					temp.TryGetValue (streetName, out current);

					if (current != null) {
						current.StreetNumbers.Add (address.Husnr);
					} else {
						current = new Address (streetName, address.Postnummer.Nr, address.Husnr);
						temp.Add (streetName, current);
					}
				}
			}

			StreetNumbersMap = temp;
		}

		public async Task<CreateEntityResult> CreateNewLocation (string name, string road, string roadNumber, string postalCode, string city, string telephone, string homePage, bool pork, bool alcohol, bool nonHalal, List<DiningCategory> categoriesChoosen, byte[] data)
		{
			name = name.Trim ();
			road = road.Trim ();
			roadNumber = roadNumber.Trim ();
			postalCode = postalCode.Trim ();
			city = city.Trim ();
			telephone = telephone.Trim ();
			homePage = homePage.Trim ();

			Adgangsadresse addressFromGeoService = await DoesAddressExists (road, roadNumber, postalCode);

			if (addressFromGeoService == null) {
				return CreateEntityResult.AddressDoesNotExist;
			}

			Location l = new Location (
				             name,
				             road,
				             roadNumber, 
				             postalCode, 
				             city,
				             double.Parse (addressFromGeoService.Adgangspunkt.Koordinater [1], CultureInfo.InvariantCulture),
				             double.Parse (addressFromGeoService.Adgangspunkt.Koordinater [0], CultureInfo.InvariantCulture),
				             telephone, 
				             homePage,
				             LocationType.Dining,
				             categoriesChoosen,
				             nonHalal, 
				             alcohol, 
				             pork,
				             0,
				             CreationStatus.Approved);

			await locationService.SaveLocation (l);

			//TODO
			//await uploadService.UploadFile (null,"test.jpg");

			selectedLocation = l;

			return CreateEntityResult.OK;
		}

		public async Task<MediaFile> TakePicture (string path, string fileName)
		{
			MediaFile image = null;

			await picker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = fileName,
				Directory = path
			}).ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
				} else {
					image = t.Result;
				}
			});
			return image;
		}

		public async Task<MediaFile> GetPictureFromDevice ()
		{
			MediaFile image = null;
			await picker.PickPhotoAsync ().ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
				} else {
					image = t.Result;
				}
			});
			return image;
		}

		public bool IsCameraAvailable ()
		{
			return picker.IsCameraAvailable;
		}
	}
}

