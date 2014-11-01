using System;
using Alliance.Carousel;

namespace HalalGuide.iOS.Carousel
{
	public class SingleDiningImageDelegate : CarouselViewDelegate
	{
		public SingleDiningImageDelegate ()
		{
		}

		public override float ValueForOption(CarouselView carousel, CarouselOption option, float aValue)
		{
			if (option == CarouselOption.Spacing)
			{
				return aValue * 1.1f;
			}
			return aValue;
		}

		public override void DidSelectItem(CarouselView carousel, int index)
		{
			Console.WriteLine("Selected: " + ++index);
		}
	}
}

