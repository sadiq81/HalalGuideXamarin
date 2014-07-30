using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HalalGuide.Services.RestDomain;

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
	}
}

