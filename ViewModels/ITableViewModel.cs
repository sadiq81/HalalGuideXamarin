using System;
using HalalGuide.Domain;
using System.Threading.Tasks;

namespace HalalGuide.ViewModels
{
	public interface ITableViewModel
	{

		int Rows ();

		Location GetLocationAtRow (int row);

		Task Update ();
	}
}

