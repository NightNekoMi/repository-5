using SeaBattle.Model.ForShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    interface IForeignField
    {
        int Difficulty
        {
            set;
        }
        AI<BattleField> AI
        {
            get;

        }
        Cell[,] Field
        {
            get; 
        }
        void strike(pair coord);
    }
}
