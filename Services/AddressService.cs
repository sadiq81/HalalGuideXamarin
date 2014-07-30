using System;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Xamarin.Geolocation;
using HalalGuide.Services.RestDomain;
using System.Collections.Generic;

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

		public async Task<Adgangsadresse> DoesAddressExits (string roadName, string roadNumber, string postalCode)
		{

			var client = new RestClient ("http://geo.oiorest.dk/");

			var request = new RestRequest ("roadName/{roadNumber},{postalCode}", Method.GET);
			CancellationToken token = new CancellationTokenSource (1500).Token;

			DateTime start = DateTime.Now;

			request.AddUrlSegment ("roadName", roadName);
			request.AddUrlSegment ("roadNumber", roadNumber);
			request.AddUrlSegment ("postalCode", postalCode);


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

		public async Task<List<Adgangsadresse>> AddressNearPosition (Position position, double distanceInMeters)
		{

			var client = new RestClient ("http://geo.oiorest.dk/");

			var request = new RestRequest ("adresser/{LatitudeSouthEast},{LongtitudeSouthEast};{LatitudeNorthWest},{LongtitudeNorthWest}", Method.GET);
			CancellationToken token = new CancellationTokenSource (15000).Token;

			DateTime start = DateTime.Now;

			string Latitude1NorthWest = (position.Latitude + (180 / Math.PI) * (distanceInMeters / 6378137)).ToString ("", CultureInfo.InvariantCulture);
			string	Longtitude1NorthWest = (position.Longitude + (180 / Math.PI) * (distanceInMeters / 6378137) / Math.Cos (position.Latitude)).ToString ("", CultureInfo.InvariantCulture);

			string Latitude2SouthEast = (position.Latitude - (180 / Math.PI) * (distanceInMeters / 6378137)).ToString ("", CultureInfo.InvariantCulture);
			string	Longtitude2SouthEast = (position.Longitude - (180 / Math.PI) * (distanceInMeters / 6378137) / Math.Cos (position.Latitude)).ToString ("", CultureInfo.InvariantCulture);

			request.AddUrlSegment ("LatitudeSouthEast", Latitude2SouthEast);
			request.AddUrlSegment ("LongtitudeSouthEast", Longtitude2SouthEast);
			request.AddUrlSegment ("LatitudeNorthWest", Latitude1NorthWest);
			request.AddUrlSegment ("LongtitudeNorthWest", Longtitude1NorthWest);

			//string url = "http://geo.oiorest.dk/adresser/" + Latitude2SouthEast + "," + Longtitude2SouthEast + ";" + Latitude1NorthWest + "," + Longtitude1NorthWest;

			try {
				IRestResponse<List<Adgangsadresse>> response = await client.ExecuteTaskAsync<List<Adgangsadresse>> (request, token);
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

