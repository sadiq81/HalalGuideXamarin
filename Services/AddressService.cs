using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Geolocation;
using HalalGuide.Services.RestDomain;

namespace HalalGuide.Services
{
	public class AddressService
	{

		public async Task<string> GetNameOfPostDistrict (string postalcode)
		{
			var client = new RestClient ("http://geo.oiorest.dk/");

			var request = new RestRequest ("postnumre/{id}", Method.GET);
			CancellationToken token = new CancellationTokenSource (1500).Token;

			DateTime start = DateTime.Now;

			request.AddUrlSegment ("id", postalcode);
			try {
				IRestResponse<Postnummer> response = await client.ExecuteTaskAsync<Postnummer> (request, token);
				Console.WriteLine ("Task took " + DateTime.Now.Subtract (start).TotalSeconds + " seconds");
				if (response.StatusCode == HttpStatusCode.OK && response != null && response.Data != null) {
					return response.Data.Navn;
				} else {
					return null;
				}

			} catch (TaskCanceledException ex) {
				Console.WriteLine ("Task took " + DateTime.Now.Subtract (start).TotalSeconds + " seconds, but was cancelled");
				return null;
			} 
		}

		public async Task<Adgangsadresse> GetAddressOfCoordinate (Position position)
		{
			if (position == null) {
				return null;
			}

			var client = new RestClient ("http://geo.oiorest.dk/");

			var request = new RestRequest ("adresser/{bredde},{længde}", Method.GET);
			CancellationToken token = new CancellationTokenSource (1500).Token;

			DateTime start = DateTime.Now;

			request.AddUrlSegment ("bredde", position.Latitude.ToString ("N", CultureInfo.InvariantCulture));
			request.AddUrlSegment ("længde", position.Longitude.ToString ("N", CultureInfo.InvariantCulture));

			try {
				IRestResponse<Adgangsadresse> response = await client.ExecuteTaskAsync<Adgangsadresse> (request, token);
				Console.WriteLine ("Task took " + DateTime.Now.Subtract (start).TotalSeconds + " seconds");
				if (response.StatusCode == HttpStatusCode.OK && response != null && response.Data != null) {
					return response.Data;
				} else {
					return null;
				}

			} catch (TaskCanceledException ex) {
				Console.WriteLine ("Task took " + DateTime.Now.Subtract (start).TotalSeconds + " seconds, but was cancelled");
				return null;
			} 
		}

	}
}

