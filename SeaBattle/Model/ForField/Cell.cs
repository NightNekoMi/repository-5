using SeaBattle.Model.ForShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    class Cell
    {
        FieldStates _state;
        pair _coord;
        public FieldStates State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }
}
