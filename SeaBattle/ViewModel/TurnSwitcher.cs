using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    public class TurnSwitcher
    {
        BattleField myField;
        BattleField foreignField;
        public TurnSwitcher(BattleField my, BattleField his)
        {
            myField = my;
            foreignField = his;
        }
        
    }
}
