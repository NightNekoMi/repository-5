using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Diagnostics;
using SeaBattle.ViewModel;
using SeaBattle.ModelNew;


namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		private Game game;
		private Field playerField;

		public MainWindow()
		{
			InitializeComponent();
			game = new Game();
			game.registerShip(new Ship(4, ShipOrientation.Horizontal), ship4_0);
			game.registerShip(new Ship(3, ShipOrientation.Horizontal), ship3_0);
			game.registerShip(new Ship(3, ShipOrientation.Horizontal), ship3_1);
			game.registerShip(new Ship(2, ShipOrientation.Horizontal), ship2_0);
			game.registerShip(new Ship(2, ShipOrientation.Horizontal), ship2_1);
			game.registerShip(new Ship(2, ShipOrientation.Horizontal), ship2_2);
			game.registerShip(new Ship(1, ShipOrientation.Horizontal), ship1_0);
			game.registerShip(new Ship(1, ShipOrientation.Horizontal), ship1_1);
			game.registerShip(new Ship(1, ShipOrientation.Horizontal), ship1_2);
			game.registerShip(new Ship(1, ShipOrientation.Horizontal), ship1_3);
			newGameWithBot();
		}

		void botMakeShot(Object sender, CellPosition pos)
		{
			game.makeShoot(pos);
		}

		void playerTurnStart(Object sender, EventArgs e)
		{
			fDesk.MouseLeftButtonUp += Field_MouseDown;
		}

		void playerTurnEnd(Object sender, EventArgs e)
		{
			fDesk.MouseLeftButtonUp -= Field_MouseDown;
		}

		class ShipMovement
		{
			public Image image { get; private set; }
			public double top { get; private set; }
			public double left { get; private set; }
			public double offsetX { get; private set; }
			public double offsetY { get; private set; }
			public ShipOrientation orientation { get; set; }

			public ShipMovement()
			{
				image = null;
			}
			public ShipMovement(Image image, double offsetX, double offsetY, ShipOrientation orientation)
			{
				this.image = image;
				this.top = image.Margin.Top;
				this.left = image.Margin.Left;
				this.offsetX = offsetX;
				this.offsetY = offsetY;
				this.orientation = orientation;
			}
		}

		private ShipMovement movement = new ShipMovement();

		bool tryPlaceShip(Point p)
		{
			Ship ship = game.getShipByImage(movement.image);
			p.X -= movement.offsetX;
			p.Y -= movement.offsetY;
			int x = (int)((p.X + 12) / 25);
			int y = (int)((p.Y + 12) / 25);
			CellPosition pos = new CellPosition(x, y);

			if (playerField.canAddShip(ship, pos))
			{
				playerField.addShip(ship, pos);
				movement.image.Margin = new Thickness(mDesk.Margin.Left + x * 25, mDesk.Margin.Top + y * 25, 0, 0);
				return true;
			}
			else
				return false;
		}

		void changeShipOrientation(Ship ship, ShipOrientation orientation)
		{
			Image image = game.getImageByShip(ship);
			ship.orientation = orientation;
			GUIShip.loadImage(image, ship);
		}

		void startMovement(Image image, Point offset)
		{
			Ship ship = game.getShipByImage(image);
			if (playerField.containShip(ship))
				playerField.removeShip(ship);
			movement = new ShipMovement(image, offset.X, offset.Y, ship.orientation);
			image.SetValue(Canvas.ZIndexProperty, 3);
			image.CaptureMouse();
		}

		void stopMovement(Point p)
		{
			if (!tryPlaceShip(p))
			{
				Ship ship = game.getShipByImage(movement.image);
				changeShipOrientation(ship, movement.orientation);
				if (!tryPlaceShip(new Point(movement.left - mDesk.Margin.Left + movement.offsetX, movement.top - mDesk.Margin.Top + movement.offsetY)))
					movement.image.Margin = new Thickness(movement.left, movement.top, 0, 0);
			}
			movement.image.SetValue(Canvas.ZIndexProperty, 2);
			movement.image.ReleaseMouseCapture();
			movement = new ShipMovement();
			if (playerField.getCountAlive() == 10)
				startGame();
		}

		private void ship_MouseMove(object sender, MouseEventArgs e)
		{
			if (movement.image != null)
			{
				Image i = movement.image as Image;
				Thickness t = new Thickness();
				t.Left = e.GetPosition(grid).X - movement.offsetX;
				t.Top = e.GetPosition(grid).Y - movement.offsetY;
				t.Right = t.Left + i.Width * 2;
				t.Bottom = t.Top + i.Height;
				movement.image.Margin = t;
			}
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Image image = sender as Image;
			startMovement(image, e.GetPosition(image));
		}
		
		private void ship_MouseUp(object sender, MouseButtonEventArgs e)
		{
			if (movement.image != null)
				stopMovement(e.GetPosition(mDesk));
		}

		private void ship_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			Image image = sender as Image;
			if (movement.image != null)
			{
				Ship ship = game.getShipByImage(image);
				changeShipOrientation(ship, ship.orientation == ShipOrientation.Horizontal ?
					ShipOrientation.Vertical : ShipOrientation.Horizontal);
			}
			else
			{
				startMovement(image, e.GetPosition(image));
				Ship ship = game.getShipByImage(image);
				changeShipOrientation(ship, ship.orientation == ShipOrientation.Horizontal ?
					ShipOrientation.Vertical : ShipOrientation.Horizontal);
				stopMovement(e.GetPosition(mDesk));
			}
		}

		private void Field_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Canvas image = sender as Canvas;
			Point p = e.GetPosition(image);
			int x = (int)(p.X / 25);
			int y = (int)(p.Y / 25);
			ShootResult res = game.makeShoot(new CellPosition(x, y));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newGameWithBot();
		}

		void newGameWithBot()
		{
			(new GUIField(mDesk)).clear();
			(new GUIField(fDesk)).clear();
			foreach (ShipAndImage a in game.getAllShipsAndImages())
			{
				changeShipOrientation(a.ship, ShipOrientation.Horizontal);
				a.image.Visibility = System.Windows.Visibility.Visible;
				a.image.Margin = a.getInitialPosition();
			}
			slider.Visibility = System.Windows.Visibility.Visible;
			labelSlider.Visibility = System.Windows.Visibility.Visible;
			playerField = new Field();
			Player player = new Player(playerField, new GUIField(mDesk), playerTurnStart, playerTurnEnd);
			Bot player2 = new Bot(new Field(), new GUIField(fDesk), player, botMakeShot, (int)slider.Value);
			player2.RandomPlace();
			game.newGame(player, player2);
		}

		void startGame()
		{
			foreach (ShipAndImage a in game.getAllShipsAndImages())
			{
				a.image.Visibility = System.Windows.Visibility.Hidden;
				Thickness t = a.image.Margin;
				t.Left -= mDesk.Margin.Left;
				t.Top -= mDesk.Margin.Top;
				int x = (int)(t.Left / 25);
				int y = (int)(t.Top / 25);
				(new GUIField(mDesk)).drawShip(a.ship, new CellPosition(x, y));
			}
			labelSlider.Visibility = System.Windows.Visibility.Hidden;
			slider.Visibility = System.Windows.Visibility.Hidden;
			game.startGame();
		}

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}