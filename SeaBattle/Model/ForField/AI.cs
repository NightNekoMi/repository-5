using SeaBattle.Model.ForShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    class AI<T>where T :BattleField
    {
        T _field;
        int _difficulty;
        public AI(T field, int difficulty)
        {
            _difficulty = difficulty;
            _field = field;
        }
        public event EventHandler AIHasShot;
        void aiHasShot()
        {
            if (AIHasShot != null)
            {
                AIHasShot(_field, null);
            }
        }
        public void AITurn(object sender, EventArgs ea)
        {
            if (performTurn())
            {
                aiHasShot();
            }
        }
        bool performTurn()
        {
            Random N = new Random();
            bool thereAreShips = false;
            if (N.Next(0, 100) < _difficulty)
            {
                    int taget = N.Next(0, _field.ShipTable.Count);
                    foreach (pair coord in _field.ShipTable.ElementAt(taget).Key)
                    {
                        if ((_field as IMyField).Field[coord.x, coord.y].State == FieldStates.withShip)
                        {
                            _field.strike(coord);
                            thereAreShips = true;
                            break;
                        }
                    }
            }
            else
            {
                bool isNotComplete = true;
                int count = 0;
                while (isNotComplete)
                {
                    
                    pair coord = new pair(N.Next(0, 10), N.Next(0, 10));
                    if((_field as IMyField).Field[coord.x,coord.y].State == FieldStates.empty)
                    {
                        _field.strike(coord);
                        isNotComplete = false;
                    }
                    if (++count > 100)
                    {
                        bool end = false;
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if ((_field as IMyField).Field[i, j].State == FieldStates.empty)
                                {
                                    _field.strike(new pair(i, j));
                                    end = true;
                                    break;
                                }
                            }
                            if (end)
                                break;
                        }
                    }
                }
            }
            return thereAreShips;
        }
    }
}
