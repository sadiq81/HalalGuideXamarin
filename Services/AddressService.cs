using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Geolocation;
using System.Collections.Generic;
using HalalGuide.Domain.Dawa;
using System.Net.Http;
using System.Linq;

namespace HalalGuide.Services
{
	public class AddressService
	{

		public async Task<string> GetNameOfPostDistrict (string postalcode)
		{
			string url = String.Format ("http://dawa.aws.dk/postnumre?nr={0}", postalcode);


			using (HttpClient client = new HttpClient ()) {

				CancellationToken token = new CancellationTokenSource (1500).Token;
				try {
					HttpResponseMessage message = await client.GetAsync (url, token);
					if (message.IsSuccessStatusCode) {
						var json = await message.Content.ReadAsStringAsync ();
						var postnumre = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Postnummer>> (json);
						if (postnumre != null && postnumre.Count > 0) {

							return postnumre [0].Navn;
						} 
					} else {
					}
				} catch (TaskCanceledException ex) {
					return null;
				}
			}
			return null;
		}

		public async Task<Adgangsadresse> DoesAddressExits (string roadName, string roadNumber, string postalCode)
		{
			string url = String.Format ("http://dawa.aws.dk/adgangsadresser?vejnavn={0}&husnummer={1}&postnr={2}", roadName, roadNumber, postalCode);


			using (HttpClient client = new HttpClient ()) {

				CancellationToken token = new CancellationTokenSource (1500).Token;
				try {
					HttpResponseMessage message = await client.GetAsync (url, token);
					if (message.IsSuccessStatusCode) {
						var json = await message.Content.ReadAsStringAsync ();
						var adresser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Adgangsadresse>> (json);
						Adgangsadresse found = adresser.FirstOrDefault (x => x.Postnummer.Nr == postalCode && x.Vejstykke.Navn == roadName && x.Husnr == roadNumber);


						return found;
					} else {
					}
				} catch (TaskCanceledException ex) {
					return null;
				}
			}
			return null;
		}

		//TODO Make sure position is known before calling this function
		public async Task<List<Adgangsadresse>> AddressNearPosition (Position position, double distanceInMeters)
		{
		
			if (position == null) {
				return null;
			}

			string url = String.Format ("http://dawa.aws.dk/adgangsadresser?cirkel={0},{1},{2}&srid=4326", position.Longitude.ToString (CultureInfo.InvariantCulture), position.Latitude.ToString (CultureInfo.InvariantCulture), distanceInMeters);


			using (HttpClient client = new HttpClient ()) {

				CancellationToken token = new CancellationTokenSource (3000).Token;
				try {
		
					HttpResponseMessage message = await client.GetAsync (url, token);
					if (message.IsSuccessStatusCode) {
						var json = await message.Content.ReadAsStringAsync ();
						var adresses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Adgangsadresse>> (json);
						return adresses;
					} else {
					}
				} catch (TaskCanceledException ex) {
					return null;
				}
			}
			return null;
		}
	}
}
