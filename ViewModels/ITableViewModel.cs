using System.Threading.Tasks;

namespace HalalGuide.ViewModels
{
	public interface ITableViewModel<T>
	{
		int Rows ();

		T GetLocationAtRow (int row);

		Task Update ();
	}
}

