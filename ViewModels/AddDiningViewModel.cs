using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HalalGuide.Services.RestDomain;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using Xamarin.Contacts;

namespace HalalGuide.ViewModels
{
	public class AddDiningViewModel : BaseViewModel
	{
		public AddDiningViewModel () : base ()
		{
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

		public async Task<Dictionary<string, List<string>>> AddressNearPosition ()
		{
			Dictionary<string, List<string>> streetNumber = new Dictionary<string, List<string>> ();

			List<Adgangsadresse> adresses = await AddressService.AddressNearPosition (Position, 150);

			foreach (Adgangsadresse address in adresses) {

				string street = address.VejNavn.Navn;
				List<string> numbers;

				streetNumber.TryGetValue (street, out numbers);
				if (numbers != null) {
					numbers.Add (address.Husnr);
				} else {
					string number = address.Husnr;
					streetNumber.Add (street, new List<string> (){ number });
				}
			}
			return adresses;
		}
	}
}

