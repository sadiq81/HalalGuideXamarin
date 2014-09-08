using HalalGuide.Domain;
using System.Collections.Generic;
using HalalGuide.Util;
using HalalGuide.Domain.Enum;
using System.Linq;
using System;
using System.Text;
using System.Globalization;

namespace HalalGuide.ViewModels
{
	public sealed class MultipleDiningViewModel : BaseViewModel
	{
		private  List<Location> _cache { get; set; }

		private  List<Location> Cache { get { return _cache; } set { _cache = value.OrderBy (loc => loc.Distance).ToList (); } }

		public  List<DiningCategory> CategoryFilter { get; set; }

		public double DistanceFilter { get; set; }

		public bool PorkFilter { get; set; }

		public bool AlcoholFilter { get; set; }

		public bool HalalFilter { get; set; }

		private string SearchText { get; set; }

		public event EventHandler FilteredLocations = delegate {};

		public void OnFilteredLocations (EventArgs arg)
		{
			FilteredLocations (this, arg);
		}

		public MultipleDiningViewModel () : base ()
		{
			DistanceFilter = 5;
			CategoryFilter = new List<DiningCategory> ();
			PorkFilter = true;
			AlcoholFilter = true;
			HalalFilter = true;
			SearchText = "";

			RefreshCache ();
		}

		public bool SearchTextChanged (string searchText)
		{
			SearchText = searchText;
			List<Location> temp = _LocationService.GetByQuery (GetQuery ());
			if (!temp.Except (Cache).Union (Cache.Except (temp)).Any ()) {
				return false;
			} else {
				Cache = CalculateDistances (temp);
				return true;
			}
		}

		public override void RefreshCache ()
		{
			Cache = CalculateDistances (_LocationService.GetByQuery (GetQuery ()), true);
			//Bounding box can contain locations farter than distance filter
			if (DistanceFilter < Constants.MaxDistanceLimit) {
				Cache.RemoveAll (loc => loc.Distance > DistanceFilter);
			}
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

			if (Position != null && DistanceFilter < Constants.MaxDistanceLimit) {

				//                 {1}              {4}
				//MaxPoint: 55.7511577903661 12.6660371446668 
				//                 {0}              {3}                 
				//Position: 55.7061392798125 12.5861374073266
				//                 {2}              {5}
				//MinPoint: 55.6611207692588 12.5062376699864

				CalcUtil.BoundingBox box = CalcUtil.GetBoundingBox (Position, DistanceFilter);

				Console.WriteLine ("Position: " + Position.Latitude + "," + Position.Longitude + "MaxPoint: " + box.MaxPoint.Latitude + "," + box.MaxPoint.Longitude + " MinPoint: " + box.MinPoint.Latitude + "," + box.MinPoint.Longitude);

				queryString.Append (string.Format (" AND ({0} <= {1} AND {0} >= {2} AND {3} <= {4} AND {3} >= {5})", 
					Location.LatitudeIdentifier, 
					box.MaxPoint.Latitude.ToString (CultureInfo.InvariantCulture), 
					box.MinPoint.Latitude.ToString (CultureInfo.InvariantCulture), 
					Location.LongtitudeIdentifier.ToString (CultureInfo.InvariantCulture), 
					box.MaxPoint.Longitude.ToString (CultureInfo.InvariantCulture), 
					box.MinPoint.Longitude.ToString (CultureInfo.InvariantCulture)));

			}

			//SELECT * FROM Location WHERE LocationType = 2 AND (Latitude <= 55.7511480782793 AND Latitude >= 55.6611110572198 AND Longtitude <= 12.6659327543831 AND Longtitude >= 12.5061333195051)

			if (PorkFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.PorkIdentifier, Convert.ToInt32 (false)));
			}

			if (AlcoholFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.AlcoholIdentifier, Convert.ToInt32 (false)));
			}

			if (HalalFilter == false) {

				queryString.Append (string.Format (" AND {0} = {1}", Location.NonHalalIdentifier, Convert.ToInt32 (false)));
			}

			if (SearchText.Length > 0) {

				queryString.Append (string.Format (" AND ( {1} LIKE '%{0}%' OR {2} LIKE '%{0}%' OR {3} LIKE '%{0}%' OR {4} LIKE '%{0}%' OR {5} LIKE '%{0}%' OR {6} LIKE '%{0}%')", 
					SearchText, 
					Location.NameIdentifier, 
					Location.AddressRoadIdentifier, 
					Location.AddressRoadNumberIdentifier, 
					Location.AddressPostalCodeIdentifier, 
					Location.AddressCityIdentifier,
					Location.DiningCategoryIdentifier
				));
			}

			queryString.Append (string.Format (" AND {0} = {1}", Location.CreationStatusIdentifier, (int)CreationStatus.Approved));

			return queryString.ToString ();
		}

		public Location GetLocationAtRow (int row)
		{
			return  Cache [row];
		}
	}
		
}

