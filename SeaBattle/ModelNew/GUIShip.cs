using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SeaBattle.ModelNew
{
	class GUIShip
	{
		public static void loadImage(Image control, Ship ship)
		{
			Uri uri;
			if (ship.orientation == ShipOrientation.Vertical)
				uri = new Uri("/Resources/" + ship.size + "DeckShip_v.gif", UriKind.RelativeOrAbsolute);
			else
				uri = new Uri("/Resources/" + ship.size + "DeckShip.gif", UriKind.RelativeOrAbsolute);
			BitmapImage bi = new BitmapImage(uri);
			control.Height = (ship.orientation == ShipOrientation.Vertical ? ship.size : 1) * 25;
			control.Width = (ship.orientation == ShipOrientation.Horizontal ? ship.size : 1) * 25;
			control.Source = bi;
		}
	}
}
