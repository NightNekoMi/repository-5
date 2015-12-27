using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;
using System.Windows;

namespace SeaBattle.Model.ForShips
{
    public class Rect
    {
        private int _width;
        private int _height;

        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width
        {
            get { return _width; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("width must be >0");
                _width = value;
            }
        }
        public int Height
        {
            get { return _height; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("height must be >0");
                _height = value;
            }
        }
        public int Right
        {
            get { return X + Width - 1; }
        }

        public int Bottom
        {
            get { return Y + Height - 1; }
        }
        public bool IntersectsWith(Rect rect)
        {
            return !(
                X > rect.Right
                || Right < rect.X
                || Y > rect.Bottom
                || Bottom < rect.Y
            );
        }
        /* public void Inflate(int width, int height)
         {
             X -= width;
             Y -= height;
             Width += width * 2;
             Height += height * 2;
         }

         public bool Contains(Rect rect)
         {
             return X <= rect.X && Y <= rect.Y
                 && Right >= rect.Right && Bottom >= rect.Bottom;
         }

         public bool Contains(Point point)
         {
             return point.X >= X && point.X <= Right && point.Y >= Y && point.Y <= Bottom;
         }*/

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }
        public IList<System.Windows.Point> GetPoints()
        {
            var points = new List<System.Windows.Point>();

            for (var x = X; x <= Right; x++)
            {
                for (var y = Y; y <= Bottom; y++)
                {
                    points.Add(new System.Windows.Point(x, y));
                }
            }

            return points;
        }
    }
}
