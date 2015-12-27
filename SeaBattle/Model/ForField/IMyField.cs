using SeaBattle.Model.ForShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    interface IMyField
    {
        Cell[,] Field
        {
            get;
        }
        void strike(pair coord);
        void placeShip(pair[] shipCoordinates, Ship ship);
        bool IsAbleToPlace(pair[] shipCoordinates);
        bool IsOutOfField(pair coord);
        void replaceShip(pair[] shipCoordinates, Ship ship);
    }
}
