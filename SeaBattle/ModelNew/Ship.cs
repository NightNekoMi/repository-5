using System.Windows.Controls;

namespace SeaBattle.ModelNew
{
	//enum ShipSize
	//{
	//	Deck1,
	//	Deck2,
	//	Deck3,
	//	Deck4
	//}

	enum ShipOrientation
	{
		Vertical,
		Horizontal
	}

	class Ship
	{
		public int size { get; private set; }
		public ShipOrientation orientation { get; set; }

		public Ship(int size, ShipOrientation orientation)
		{
			this.size = size;
			this.orientation = orientation;
		}
	}
}
