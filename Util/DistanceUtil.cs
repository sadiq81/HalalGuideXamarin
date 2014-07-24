using System;
using HalalGuide.Domain;
using Xamarin.Geolocation;

namespace HalalGuide.Util
{
	public static class CalcUtil
	{
		private const double EARTH_RADIUS_KM = 6371;

		public static double GetDistanceKM (Position point1, Position point2)
		{
			double dLat = ToRad (point2.Latitude - point1.Latitude);
			double dLon = ToRad (point2.Longitude - point1.Longitude);

			double a = Math.Pow (Math.Sin (dLat / 2), 2) +
			           Math.Cos (ToRad (point1.Latitude)) * Math.Cos (ToRad (point2.Latitude)) *
			           Math.Pow (Math.Sin (dLon / 2), 2);

			double c = 2 * Math.Atan2 (Math.Sqrt (a), Math.Sqrt (1 - a));

			double distance = EARTH_RADIUS_KM * c;
			return distance;
		}

		private static double ToRad (double input)
		{
			return input * (Math.PI / 180);
		}
	}
}

