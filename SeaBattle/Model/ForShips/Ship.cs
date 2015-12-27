using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SeaBattle.Model.ForShips
{
    public class Ship
    {
        public Ship(ShipType type)
        {
            TypeOfShip = type;
            StatusOfShip = ShipStatus.Safe;
        }
        pair[] _coordinates;
        public int Length
        {
            get
            {
                return (int)TypeOfShip;
            }
        }
        public ShipOrientation Orientation { get; set; }
        public pair[] Coordinates
        {
            get
            {
                return _coordinates;
            }
            set
            {
                _coordinates = value;
            }
        }
        public int HitCount { get; set; }
        public bool IsDrowned
        {
            get
            {
                if (HitCount == Length)
                {
                    StatusOfShip = ShipStatus.Drown;
                    return true;
                }
                else
                    return false;
            }
        }

        public void Rotate()
        {
            Orientation = Orientation == ShipOrientation.Horizontal ? ShipOrientation.Vertical : ShipOrientation.Horizontal;
        }
        public ShipType TypeOfShip
        {
            get;
            private set;
        }
        public ShipStatus StatusOfShip
        {
            get;
            set;
        }

    }
}
