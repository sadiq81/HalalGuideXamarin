using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HalalGuide.Services.RestDomain;
using System.Collections.Generic;
using Xamarin.Media;
using SimpleDBPersistence.Service;
using HalalGuide.Domain;

namespace HalalGuide.ViewModels
{
	public class AddDiningViewModel : BaseViewModel
	{
		private readonly MediaPicker MediaPicker = ServiceContainer.Resolve<MediaPicker> ();

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
				var name = await AddressService.GetNameOfPostDistrict (postalcode);
				return name;
			} else {
				return null;
			}
		}

		public async Task<Adgangsadresse> GetAddressOfCoordinate ()
		{
			return await AddressService.GetAddressOfCoordinate (Position);
		}

		public async Task<Adgangsadresse> DoesAddressExists (string roadName, string roadNumber, string postalCode)
		{
			return await AddressService.DoesAddressExits (roadName, roadNumber, postalCode);
		}

		public async Task LoadAddressNearPosition ()
		{
			IsBusy = false;
			Dictionary<string, Address> temp = new Dictionary<string, Address> ();

			List<Adgangsadresse> adresses = await AddressService.AddressNearPosition (Position, 150);

			foreach (Adgangsadresse address in adresses) {

				Address current = null;
				string streetName = address.VejNavn.Navn;
				temp.TryGetValue (streetName, out current);

				if (current != null) {
					current.StreetNumbers.Add (address.Husnr);
				} else {
					current = new Address (streetName, address.Postnummer.Nr, address.Husnr);
					temp.Add (streetName, current);
				}
			}

			StreetNumbersMap = temp;
			IsBusy = true;
		}

		public bool IsCameraAvailable ()
		{
			return MediaPicker.IsCameraAvailable;

		}

		public async Task<MediaFile> TakePicture (string path, string fileName)
		{
			MediaFile file = null;
			await MediaPicker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = fileName,
				Directory = path
			}).ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					return;
				} else {
					file = t.Result;
				}
			});
			return file;
		}

		public async Task<MediaFile> GetPictureFromDevice ()
		{
			MediaFile file = null;
			await MediaPicker.PickPhotoAsync ().ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					return;
				} else {
					file = t.Result;
				}
			});
			return file;
		}
	}
}

