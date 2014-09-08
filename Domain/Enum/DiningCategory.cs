using System.Collections.Generic;

namespace HalalGuide.Domain.Enum
{
	public  class DiningCategory
	{
		public string Title { get; set; }

		public DiningCategory (string title)
		{
			this.Title = title;
		}

		public override bool Equals (object obj)
		{
			if (!(obj is DiningCategory)) {
				return false;
			}

			if (this == obj) {
				return true;
			}
				
			if (Title.Equals (((DiningCategory)obj).Title)) {
				return true;
			}

			return object.ReferenceEquals (this, obj);
		}

		public override int GetHashCode ()
		{
			return Title.GetHashCode ();
		}

		public static DiningCategory Afghan = new DiningCategory ("afghan");
		public static DiningCategory African = new DiningCategory ("african");
		public static DiningCategory American = new DiningCategory ("american");
		public static DiningCategory Argentine = new DiningCategory ("argentine");
		public static DiningCategory Asien = new DiningCategory ("asian");

		public static DiningCategory Belgien = new DiningCategory ("belgian");
		public static DiningCategory Brasilien = new DiningCategory ("brasilian");
		public static DiningCategory British = new DiningCategory ("british");
		public static DiningCategory Buffet = new DiningCategory ("buffet");
		public static DiningCategory Burger = new DiningCategory ("burger");
		public static DiningCategory Bakery = new DiningCategory ("bakery");
		public static DiningCategory Bagel = new DiningCategory ("bagel");
		public static DiningCategory BubbleTea = new DiningCategory ("bubble.tea");
		public static DiningCategory Bucther = new DiningCategory ("butcher");

		public static DiningCategory Cafe = new DiningCategory ("cafe");
		public static DiningCategory Caribian = new DiningCategory ("caribian");
		public static DiningCategory Cupcake = new DiningCategory ("cupcake");
		public static DiningCategory Candy = new DiningCategory ("candy");
		public static DiningCategory Chinese = new DiningCategory ("chinese");

		public static DiningCategory Danish = new DiningCategory ("danish");
		public static DiningCategory Dessert = new DiningCategory ("dessert");

		public static DiningCategory Fish = new DiningCategory ("fish");
		public static DiningCategory Fruit = new DiningCategory ("fruit");
		public static DiningCategory Fastfood = new DiningCategory ("fastfood");
		public static DiningCategory French = new DiningCategory ("french");

		public static DiningCategory German = new DiningCategory ("german");
		public static DiningCategory Grill = new DiningCategory ("grill");
		public static DiningCategory Greek = new DiningCategory ("greek");

		public static DiningCategory Icecream = new DiningCategory ("icecream");

		public static DiningCategory Juice = new DiningCategory ("juice");

		public static DiningCategory Kiosk = new DiningCategory ("kiosk");

		public static DiningCategory Indian = new DiningCategory ("indian");
		public static DiningCategory Indonesian = new DiningCategory ("indonesian");
		public static DiningCategory Irish = new DiningCategory ("irish");
		public static DiningCategory Italien = new DiningCategory ("italien");
		public static DiningCategory Iranian = new DiningCategory ("iranian");

		public static DiningCategory Japanese = new DiningCategory ("japanese");

		public static DiningCategory Kebab = new DiningCategory ("kebab");
		public static DiningCategory Korean = new DiningCategory ("korean");
		public static DiningCategory Kosher = new DiningCategory ("kosher");

		public static DiningCategory Libanese = new DiningCategory ("libanese");

		public static DiningCategory Mediterranean = new DiningCategory ("mediterranean");
		public static DiningCategory Malyasian = new DiningCategory ("malaysian");
		public static DiningCategory Marocan = new DiningCategory ("marocan");
		public static DiningCategory Mexican = new DiningCategory ("mexican");

		public static DiningCategory Nordic = new DiningCategory ("nordic");
		public static DiningCategory Nepalise = new DiningCategory ("nepalise");

		public static DiningCategory Pastery = new DiningCategory ("pastery");
		public static DiningCategory Pakistani = new DiningCategory ("pakistani");
		public static DiningCategory Persian = new DiningCategory ("persian");
		public static DiningCategory Pizza = new DiningCategory ("pizza");
		public static DiningCategory Portugese = new DiningCategory ("portugese");

		public static DiningCategory Russian = new DiningCategory ("russian");

		public static DiningCategory Seafood = new DiningCategory ("seafood");
		public static DiningCategory Salat = new DiningCategory ("salat");
		public static DiningCategory Sandwhich = new DiningCategory ("sandwich");
		public static DiningCategory Spanish = new DiningCategory ("spanish");
		public static DiningCategory Steak = new DiningCategory ("steak");
		public static DiningCategory Soup = new DiningCategory ("soup");
		public static DiningCategory Sushi = new DiningCategory ("sushi");


		public static DiningCategory Tapas = new DiningCategory ("tapas");
		public static DiningCategory Thai = new DiningCategory ("thai");
		public static DiningCategory Tibetan = new DiningCategory ("tibetan");
		public static DiningCategory Tyrkish = new DiningCategory ("tyrkish");

		public static DiningCategory Vegan = new DiningCategory ("vegan");
		public static DiningCategory Vietnamese = new DiningCategory ("vietnamese");

		public static DiningCategory Wok = new DiningCategory ("wok");

		public static List<DiningCategory> Categories = new List<DiningCategory> () {
			Afghan, African, American, Argentine, Asien, Belgien, Brasilien, British, Buffet,
			Burger, Bakery, Bagel, BubbleTea, Bucther, Cafe, Caribian, Cupcake, Candy, Chinese, Danish,
			Dessert, Fish, Fruit, Fastfood, French, German, Grill, Greek,
			Icecream, Juice, Kiosk, Indian, Indonesian, Irish, Italien, Iranian, Japanese, Kebab, Korean,
			Kosher, Libanese, Mediterranean, Malyasian, Marocan, Mexican, Nordic, Nepalise, Pastery,
			Pakistani, Persian, Pizza, Portugese, Russian, Seafood, Salat, Sandwhich, Spanish, Steak, Soup,
			Sushi, Tapas, Thai, Tibetan, Tyrkish, Vegan, Vietnamese, Wok
		};

		public static List<DiningCategory> GetCategoriesFromString (string categoryString)
		{
			List<DiningCategory> categories = new List<DiningCategory> ();

			foreach (string cat in categoryString.Split (new []{ ',' })) {
				foreach (DiningCategory dcat in Categories) {
					if (cat.Equals (dcat.Title)) {
						categories.Add (dcat);
					}
				}
			}
			return categories;
		}
	}
}

