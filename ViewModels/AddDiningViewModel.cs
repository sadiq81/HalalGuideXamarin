using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Media;
using HalalGuide.Domain;
using HalalGuide.Domain.Enum;
using S3Storage.AWSException;
using HalalGuide.Util;
using HalalGuide.Domain.Dawa;
using XUbertestersSDK;
using System.Linq;
using System.Globalization;

namespace HalalGuide.ViewModels
{
	public class AddDiningViewModel : BaseViewModel
	{
		private MediaFile Image { get; set; }

		private Dictionary<string, Address> StreetNumbersMap { get; set; }

		public AddDiningViewModel () : base ()
		{
			StreetNumbersMap = new Dictionary<string, Address> ();
		}

		public override void RefreshCache ()
		{
			//DO Nothing
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
				var name = await _AddressService.GetNameOfPostDistrict (postalcode);
				return name;
			} else {
				return null;
			}
		}

		public async Task<Adgangsadresse> DoesAddressExists (string roadName, string roadNumber, string postalCode)
		{
			return await _AddressService.DoesAddressExits (roadName, roadNumber, postalCode);
		}

		//TODO Make sure position is known before calling this function
		public async Task LoadAddressNearPosition ()
		{
			Dictionary<string, Address> temp = new Dictionary<string, Address> ();

			List<Adgangsadresse> adresses = await _AddressService.AddressNearPosition (Position, 150);

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

		public bool IsCameraAvailable ()
		{
			return MediaPicker.IsCameraAvailable;

		}

		public async Task<CreateEntityResult> CreateNewLocation (string name, string road, string roadNumber, string postalCode, string city, string telephone, string homePage, bool pork, bool alcohol, bool nonHalal, List<DiningCategory> categoriesChoosen)
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
				             String.Join (", ", categoriesChoosen.Select (s => s.Title)),
				             nonHalal, 
				             alcohol, 
				             pork,
				             0,
				             CreationStatus.AwaitingApproval);

			l.Id = _KeyChain.GetFaceBookAccount ().Username + "-" + DateTime.UtcNow.Ticks;

			CreateEntityResult result = await _LocationService.SaveLocation (l);

			if (result == CreateEntityResult.OK) {

				result = await _ImageService.UploadLocationPicture (l, Image != null ? StreamUtil.ReadToEnd (Image.GetStream ()) : null);

				if (result != CreateEntityResult.OK) {
					await _LocationService.DeleteLocation (l);
				}
			} 

			return result;
		}

		public async Task<MediaFile> TakePicture (string path, string fileName)
		{
			XUbertesters.LogInfo (String.Format ("AddDiningViewModel: TakePicture-Start with args path: {0} filename: {1}", path, fileName));

			XUbertesters.HideMenu ();
			Image = null;
			await MediaPicker.TakePhotoAsync (new StoreCameraMediaOptions {
				Name = fileName,
				Directory = path
			}).ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					XUbertesters.LogError (String.Format ("AddDiningViewModel: TakePicture cancelled or faulted: {0}", t.Exception));
					return;
				} else {
					XUbertesters.LogError ("AddDiningViewModel: TakePicture ok");
					Image = t.Result;
				}
			});
			XUbertesters.LogError ("AddDiningViewModel: TakePicture-End");
			return Image;
		}

		public async Task<MediaFile> GetPictureFromDevice ()
		{
			XUbertesters.LogInfo ("AddDiningViewModel: GetPictureFromDevice-Start");
			XUbertesters.HideMenu ();
			Image = null;
			await MediaPicker.PickPhotoAsync ().ContinueWith (t => {
				if (t.IsCanceled || t.IsFaulted) {
					XUbertesters.LogError (String.Format ("AddDiningViewModel: GetPictureFromDevice cancelled or faulted: {0}", t.Exception));
					return;
				} else {
					XUbertesters.LogError ("AddDiningViewModel: GetPictureFromDevice ok");
					Image = t.Result;
				}
			});
			XUbertesters.LogError ("AddDiningViewModel: GetPictureFromDevice-End");
			return Image;
		}
	}
}

