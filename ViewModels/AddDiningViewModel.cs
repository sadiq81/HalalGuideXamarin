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

		public async Task<Adgangsadresse> DoesAddressExists (string roadName, string roadNumber, string postalCode)
		{
			return await AddressService.DoesAddressExits (roadName, roadNumber, postalCode);
		}

		//TODO Make sure position is known before calling this function
		public async Task LoadAddressNearPosition ()
		{
			Dictionary<string, Address> temp = new Dictionary<string, Address> ();

			List<Adgangsadresse> adresses = await AddressService.AddressNearPosition (Position, 150);

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

		public async Task<CreateDiningResult> CreateNewLocation (string name, string road, string roadNumber, string postalCode, string city, string telephone, string homePage, bool pork, bool alcohol, bool nonHalal, List<DiningCategory> categoriesChoosen)
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
				return CreateDiningResult.AddressDoesNotExist;
			}

			Location l = new Location (
				             name,
				             road,
				             roadNumber, 
				             postalCode, 
				             city,
				             addressFromGeoService.Adgangspunkt.Koordinater [1],
				             addressFromGeoService.Adgangspunkt.Koordinater [0],
				             telephone, 
				             homePage,
				             LocationType.Dining,
				             categoriesChoosen,
				             nonHalal, 
				             alcohol, 
				             pork,
				             0,
				             LocationStatus.AwaitingApproval);

			l.Id = KeyChain.GetFaceBookAccount ().Username + "-" + DateTime.Now.Ticks;

			try {
				await LocationDAO.SaveOrReplace (l);
			} catch (AWSErrorException e) {
				XUbertesters.LogError ("AddDiningViewModel: CouldNotCreateEntityInSimpleDB: " + e);
				return CreateDiningResult.CouldNotCreateEntityInSimpleDB;
			}

			if (Image != null) {

				string objectName = l.Submitter + "-" + DateTime.Now.Ticks + ".jpeg";

				try {
					await S3.PutObject (Constants.S3Bucket, l.Id + "/" + objectName, StreamUtil.ReadToEnd (Image.GetStream ()));

					LocationPicture picture = new LocationPicture (){ Id = objectName, LocationId = l.Id };

					await LocationPictureDAO.SaveOrReplace (picture);

				} catch (AWSErrorException e) {
					XUbertesters.LogError ("AddDiningViewModel: CouldNotUploadPictureToS3: " + e);
					LocationDAO.Delete (l).RunSynchronously ();
					return CreateDiningResult.CouldNotUploadPictureToS3;

				} catch (SimpleDBPersistence.SimpleDB.Model.AWSException.AWSErrorException e) {

					XUbertesters.LogError ("AddDiningViewModel: CouldNotCreateEntityInSimpleDB: " + e);
					S3.DeleteObject (Constants.S3Bucket, objectName).RunSynchronously ();
					LocationDAO.Delete (l).RunSynchronously ();
					return CreateDiningResult.CouldNotUploadPictureToS3;
				}
			}

			BaseViewModel.SelectedLocation = l;
			return CreateDiningResult.OK;
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

