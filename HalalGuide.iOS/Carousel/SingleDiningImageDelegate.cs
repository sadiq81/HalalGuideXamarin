﻿using System;
using Alliance.Carousel;

namespace HalalGuide.iOS.Carousel
{
	public class SingleDiningImageDelegate : CarouselViewDelegate
	{
		public SingleDiningImageDelegate ()
		{
		}

		public override float ValueForOption (CarouselView carousel, CarouselOption option, float aValue)
		{
			if (option == CarouselOption.Spacing) {
				return aValue * 1.3f;
			}
			return aValue;
		}

		public override void DidSelectItem (CarouselView carousel, int index)
		{

		}
	}
}

