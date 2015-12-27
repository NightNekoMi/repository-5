using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForShips
{
    public class pair
    {
        public int x, y;
        public pair(int X, int Y)
        {
            x = X;
            y = Y;
        }
        public bool IsEqual(pair A)
        {
            if (A.x == this.x || A.y == this.y)
                return true;
            else
                return false;
        }
    }
}
