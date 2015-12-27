using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForShips
{
    public enum ShipType
    {
        OneDeckShip = 1,
        TwoDeckShip,
        ThreeDeckShip,
        FourDeckShip
    }
    public enum ShipStatus
    {
        Safe,
        Shot,
        Drown
    }
}
