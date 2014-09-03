using HalalGuide.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleDBPersistence.SimpleDB.Model.Parameters;
using HalalGuide.Util;
using HalalGuide.Domain.Enum;
using System.Linq;
using System;
using SimpleDBPersistence.Domain;
using HalalGuide.Services;
using System.Text;
using MonoTouch.CoreImage;

namespace HalalGuide.ViewModels
{
	public sealed class MultipleDiningViewModel : BaseViewModel
	{
		private  List<Location> Cache { get; set; }

		public  List<DiningCategory> CategoryFilter { get; set; }

		public double DistanceFilter { get; set; }

		public bool PorkFilter { get; set; }

		public bool AlcoholFilter { get; set; }

		public bool HalalFilter { get; set; }

		public MultipleDiningViewModel () : base ()
		{
			DistanceFilter = 5;
			CategoryFilter = new List<DiningCategory> ();
			PorkFilter = true;
			AlcoholFilter = true;
			HalalFilter = true;

			RefreshCache ();
		}

		public override void RefreshCache ()
		{
			Cache = CalculateDistances (_LocationService.GetByQuery (GetQuery ()), true);
		}

		public int Rows ()
		{
			return Cache.Count;
		}

		public string GetQuery ()
		{
			StringBuilder queryString = new StringBuilder ();

			queryString.Append (string.Format ("SELECT * FROM {0} WHERE {1} = {2}", Location.TableIdentifier, Location.LocationTypeIdentifier, (int)LocationType.Dining));

			if (CategoryFilter != null && CategoryFilter.Count > 0) {

				queryString.Append (string.Format (" AND ("));

				foreach (DiningCategory cat in CategoryFilter) {
					queryString.Append (string.Format (" {0} LIKE '%{1}%' OR", Location.DiningCategoryIdentifier, cat.Title));
				}

				queryString.Remove (queryString.Length - 2, 2);
				queryString.Append (")");
			}

			if (Position != null) {

				CalcUtil.BoundingBox box = CalcUtil.GetBoundingBox (Position, DistanceFilter / 2);

				queryString.Append (string.Format (" AND ({0} <= {1} AND {0} >= {2} AND {3} <= {4} AND {3} >= {5})", Location.LatitudeIdentifier, box.MaxPoint.Latitude, box.MinPoint.Latitude, Location.LongtitudeIdentifier, box.MaxPoint.Longitude, box.MinPoint.Longitude));

			}

			if (PorkFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.PorkIdentifier, Convert.ToInt32 (false)));
			}

			if (AlcoholFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.AlcoholIdentifier, Convert.ToInt32 (false)));
			}

			if (HalalFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.NonHalalIdentifier, Convert.ToInt32 (false)));
			}

			return queryString.ToString ();
		}

		public Location GetLocationAtRow (int row)
		{
			return  Cache [row];
		}
	}
		
}

