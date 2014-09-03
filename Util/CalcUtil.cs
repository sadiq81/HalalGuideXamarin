using System;
using HalalGuide.Domain;
using Xamarin.Geolocation;

namespace HalalGuide.Util
{
	public static class CalcUtil
	{
		private const double EARTH_RADIUS_KM = 6378;

		public static double GetDistanceKM (Position point1, Position point2)
		{
			double dLat = Deg2rad (point2.Latitude - point1.Latitude);
			double dLon = Deg2rad (point2.Longitude - point1.Longitude);

			double a = Math.Pow (Math.Sin (dLat / 2), 2) +
			           Math.Cos (Deg2rad (point1.Latitude)) * Math.Cos (Deg2rad (point2.Latitude)) *
			           Math.Pow (Math.Sin (dLon / 2), 2);

			double c = 2 * Math.Atan2 (Math.Sqrt (a), Math.Sqrt (1 - a));

			double distance = EARTH_RADIUS_KM * c;
			return distance;
		}

		// Semi-axes of WGS-84 geoidal reference
		private static double WGS84_a = 6378137.0;
		// Major semiaxis [m]
		private static double WGS84_b = 6356752.3;
		// Minor semiaxis [m]

		public class BoundingBox
		{
			public Position MinPoint { get; set; }

			public Position MaxPoint { get; set; }
		}

		// 'halfSideInKm' is the half length of the bounding box you want in kilometers.
		public static BoundingBox GetBoundingBox (Position point, double halfSideInKm)
		{            
			// Bounding box surrounding the point at given coordinates,
			// assuming local approximation of Earth surface as a sphere
			// of radius given by WGS84
			var lat = Deg2rad (point.Latitude);
			var lon = Deg2rad (point.Longitude);
			var halfSide = 1000 * halfSideInKm;

			// Radius of Earth at given latitude
			var radius = WGS84EarthRadius (lat);
			// Radius of the parallel at given latitude
			var pradius = radius * Math.Cos (lat);

			var latMin = lat - halfSide / radius;
			var latMax = lat + halfSide / radius;
			var lonMin = lon - halfSide / pradius;
			var lonMax = lon + halfSide / pradius;

			return new BoundingBox { 
				MinPoint = new Position { Latitude = Rad2deg (latMin), Longitude = Rad2deg (lonMin) },
				MaxPoint = new Position { Latitude = Rad2deg (latMax), Longitude = Rad2deg (lonMax) }
			};            
		}

		// degrees to radians
		private static double Deg2rad (double degrees)
		{
			return Math.PI * degrees / 180.0;
		}

		// radians to degrees
		private static double Rad2deg (double radians)
		{
			return 180.0 * radians / Math.PI;
		}

		// Earth radius at a given latitude, according to the WGS-84 ellipsoid [m]
		private static double WGS84EarthRadius (double lat)
		{
			// http://en.wikipedia.org/wiki/Earth_radius
			var An = WGS84_a * WGS84_a * Math.Cos (lat);
			var Bn = WGS84_b * WGS84_b * Math.Sin (lat);
			var Ad = WGS84_a * Math.Cos (lat);
			var Bd = WGS84_b * Math.Sin (lat);
			return Math.Sqrt ((An * An + Bn * Bn) / (Ad * Ad + Bd * Bd));
		}
	}
		
}

