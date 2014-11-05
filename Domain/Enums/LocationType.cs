using System.Collections;


namespace HalalGuide.Domain.Enums
{
	public enum LocationType
	{
		Mosque,
		Shop,
		Dining,
		ENumber
	}

	public static class LocationTypeExtensions
	{

		public static string DefaultImageName (this LocationType type)
		{
			switch (type) {
			case LocationType.Dining:
				{
					return "Dining.png";
				}
			case LocationType.ENumber:
				{
					return "ENumber.png";
				}
			case LocationType.Mosque:
				{
					return "Mosque.png";
				}
			case LocationType.Shop:
				{
					return "Shop.png";
				}
			default:
				{
					return "";
				}

			}
		}
	}
}

