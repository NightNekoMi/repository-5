using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SeaBattle.ModelNew
{
	class GUIField
	{
		private Canvas control;
		public GUIField(Canvas control)
		{
			this.control = control;
		}

		public void drawGrid()
		{
			double cellSize = control.Width / 10;
			for (int i = 0; i < 11; i++)
			{
				Line line = new Line();
				line.X2 = line.X1 = i * cellSize;
				line.Y1 = 0; line.Y2 = control.Height;
				line.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
				control.Children.Add(line);
			}
			for (int i = 0; i < 11; i++)
			{
				Line line = new Line();
				line.Y2 = line.Y1 = i * cellSize;
				line.X1 = 0; line.X2 = control.Width;
				line.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
				control.Children.Add(line);
			}
		}

		public void drawCross(CellPosition pos)
		{
			double cellSize = control.Width / 10;
			Line line1 = new Line();
			line1.X1 = pos.x * cellSize;
			line1.Y1 = pos.y * cellSize;
			line1.X2 = (pos.x + 1) * cellSize;
			line1.Y2 = (pos.y + 1) * cellSize;
			line1.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
			line1.SetValue(Canvas.ZIndexProperty, 4);
			control.Children.Add(line1);

			Line line2 = new Line();
			line2.X1 = (pos.x + 1) * cellSize;
			line2.Y1 = pos.y * cellSize;
			line2.X2 = pos.x * cellSize;
			line2.Y2 = (pos.y + 1) * cellSize;
			line2.Stroke = new SolidColorBrush(Color.FromRgb(255, 0, 0));
			line2.SetValue(Canvas.ZIndexProperty, 4);
			control.Children.Add(line2);
		}

		public void drawShip(Ship ship, CellPosition pos)
		{
			double cellSize = control.Width / 10;
			Image image = new Image();
			GUIShip.loadImage(image, ship);
			image.Margin = new Thickness(pos.x * cellSize, pos.y * cellSize, 0, 0);
			control.Children.Add(image);
		}

		public void drawChecked(CellPosition pos)
		{
			double cellSize = control.Width / 10;
			Rectangle background = new Rectangle();
			background.Margin = new Thickness(pos.x * cellSize + 1, pos.y * cellSize + 1, 0, 0);
			background.Fill = new SolidColorBrush(Color.FromRgb(0, 200, 200));
			background.Width = cellSize - 1;
			background.Height = cellSize - 1;
			control.Children.Add(background);
		}

		public void clear()
		{
			control.Children.Clear();
			Rectangle background = new Rectangle();
			background.Margin = new Thickness(0, 0, control.Width, control.Height);
			background.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 255));
			background.Width = control.Width;
			background.Height = control.Height;
			control.Children.Add(background);
			drawGrid();
		}
	}
}
