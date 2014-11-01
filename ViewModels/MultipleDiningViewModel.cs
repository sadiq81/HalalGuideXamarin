using HalalGuide.Domain;
using System.Collections.Generic;
using HalalGuide.Util;
using HalalGuide.Domain.Enums;
using System.Linq;
using System;
using System.Text;
using System.Globalization;
using System.Linq.Expressions;
using MonoTouch.Foundation;
using System.Threading;
using Newtonsoft.Json.Serialization;
using HalalGuide.iOS.Tables.Cells;
using System.Threading.Tasks;

namespace HalalGuide.ViewModels
{
	public sealed class MultipleDiningViewModel : BaseViewModel
	{
		public event EventHandler filteredLocations = delegate { };

		private  List<Location> Cache { get; set ; }

		public  List<DiningCategory> CategoryFilter { get; set; }

		public double DistanceFilter { get; set; }

		public bool PorkFilter { get; set; }

		public bool AlcoholFilter { get; set; }

		public bool HalalFilter { get; set; }

		private string SearchText { get; set; }

		public MultipleDiningViewModel () : base ()
		{
			DistanceFilter = 5;
			CategoryFilter = new List<DiningCategory> ();
			PorkFilter = true;
			AlcoholFilter = true;
			HalalFilter = true;
			SearchText = "";
			Cache = new List<Location> ();
		}

		public override void RefreshCache ()
		{
			List<Location> list = locationService.LocationsByQuery (GetQuery ());
			Cache = CalculateDistances (list).OrderBy (loc => loc.distance).ToList ();
		}

		public bool SearchTextChanged (string searchText)
		{
			SearchText = searchText;
			List<Location> temp = locationService.LocationsByQuery (GetQuery ());
			if (!temp.Except (Cache).Union (Cache.Except (temp)).Any ()) {
				return false;
			} else {
				Cache = CalculateDistances (temp).OrderBy (loc => loc.distance).ToList ();
				return true;
			}
		}

		public int Rows ()
		{
			return Cache.Count;
		}

		public Expression<Func<Location, bool>> GetQuery ()
		{
			Expression<Func<Location, bool>> predicate = loc => loc.locationType == LocationType.Dining &&
			                                             loc.creationStatus == CreationStatus.Approved &&
			                                             loc.deleted == false;

			if (CategoryFilter != null && CategoryFilter.Count > 0) {
				predicate = predicate.AndAlso (loc => loc.categories.Intersect (CategoryFilter).Any ());
			}

			if (Position != null && DistanceFilter < Constants.MaxDistanceLimit) {

				CalcUtil.BoundingBox box = CalcUtil.GetBoundingBox (Position, DistanceFilter);

				predicate = predicate.AndAlso (loc => loc.latitude <= box.MaxPoint.Latitude);
				predicate = predicate.AndAlso (loc => loc.latitude >= box.MinPoint.Latitude);
				predicate = predicate.AndAlso (loc => loc.longtitude <= box.MaxPoint.Longitude);
				predicate = predicate.AndAlso (loc => loc.longtitude >= box.MinPoint.Longitude);
			}

			if (PorkFilter == false) {
				predicate = predicate.AndAlso (loc => loc.pork == PorkFilter);
			}

			if (AlcoholFilter == false) {
				predicate = predicate.AndAlso (loc => loc.alcohol == AlcoholFilter);
			}

			if (HalalFilter == false) {
				predicate = predicate.AndAlso (loc => loc.nonHalal == HalalFilter);
			}



			if (SearchText.Length > 0) {

				predicate = predicate.AndAlso (loc => loc.name.Contains (SearchText) ||
				loc.addressRoad.Contains (SearchText) ||
				loc.addressRoadNumber.Contains (SearchText) ||
				loc.addressPostalCode.Contains (SearchText) ||
				loc.addressCity.Contains (SearchText) ||
				loc.categoriesForDatabase.Contains (SearchText)
				);
			}

			return predicate;
		}

		public Location GetLocationAtRow (int row)
		{
			return  Cache [row];
		}
	}
		
}

