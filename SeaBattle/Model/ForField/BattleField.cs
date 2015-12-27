using SeaBattle.Model.ForShips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Model.ForField
{
    public class BattleField : IMyField, IForeignField
    {

        AI<BattleField> _ai;
        int _difficulty;
        Cell[,] Field = new Cell[10, 10];
        Cell[,] IForeignField.Field
        {
            get
            {
                Cell[,] foreignField = new Cell[10,10];
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        if(Field[i,j].State!=FieldStates.withShip)
                         foreignField[i, j] = Field[i, j];
                    }
                }
                return foreignField;
            }
        }
        Cell[,] IMyField.Field
        {
            get
            {
                return Field;
            }
        }

        AI<BattleField> IForeignField.AI
        {
            get
            {
                if (_ai == null)
                  _ai = new AI<BattleField>(this, _difficulty);
                return _ai;
            }
        }

        int IForeignField.Difficulty
        {
            set
            {
                _difficulty = value;
            }
        }

        public Dictionary<pair[], Ship> ShipTable;
        Stack<Ship> _stackOfShips = new Stack<Ship>();
        public BattleField()
        {
            foreach (Cell X in Field)
            {
				if (X != null)
					X.State = FieldStates.empty;
            }
            feedStackWithShips();
        }
        void feedStackWithShips()
        {
            AddStackWithShips(ShipType.OneDeckShip);
            AddStackWithShips(ShipType.TwoDeckShip);
            AddStackWithShips(ShipType.ThreeDeckShip);
            AddStackWithShips(ShipType.FourDeckShip);
        }
        void AddStackWithShips(ShipType type)
        {
            Stack<Ship> stack = new Stack<Ship>();
            for (int i = 0; i < 5 - (int)type; i++)
            {
                stack.Push(new Ship(type));
            }
            _stackOfShips.Concat(stack);
        }
        public bool IsAbleToPlace(pair[] shipCoordinates)
        {
            foreach (pair varPair in shipCoordinates)
            {
                if (IsOutOfField(varPair))
                {
                    return false;
                }
                foreach (Ship varShip in ShipTable.Values)
                {
                    if (IsNearWithShip(varPair, varShip))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool IsOutOfField(pair coord)
        {
            if (coord.x > 9 || coord.y > 9 || coord.y < 0 || coord.x < 0)
            {
                return true;
            }
            else
                return false;
        }
        bool IsNearWithShip(pair coord, Ship ship)
        {
            foreach (pair varPair in ship.Coordinates)
            {
                if (PairArrayContainsPair(AreaAroundCoordinate(varPair), coord))
                {
                    return true;
                }
            }
            return false;
        }
        bool PairArrayContainsPair(pair[,] array, pair pair)
        {
            foreach (pair varPair in array)
            {
                if (varPair == pair)
                    return true;
            }
            return false;
        }
        pair[,] AreaAroundCoordinate(pair coord)
        {
            pair[,] area = new pair[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    area[i, j] = new pair(coord.x - 1 + i, coord.y - 1 + j);
                }
            }
            return area;
        }
        public void placeShip(pair[] shipCoordinates, Ship ship)
        {
            if (IsAbleToPlace(shipCoordinates))
            {
                ShipTable.Add(shipCoordinates, ship);
                FillCellsWithShip(shipCoordinates);
            }
        }
        private void FillCellsWithShip(pair[] shipCoordinates)
        {
            foreach (pair varPair in shipCoordinates)
            {
                Field[varPair.x, varPair.y].State = FieldStates.withShip;
            }
        }
        public void replaceShip(pair[] shipCoordinates, Ship ship)
        {
            if (IsAbleToPlace(shipCoordinates))
            {
                ShipTable.Add(shipCoordinates, ship);
                ShipTable.Remove(ship.Coordinates);
                FillCellsWithShip(shipCoordinates);
                foreach (pair varPair in shipCoordinates)
                {
                    Field[varPair.x, varPair.y].State = FieldStates.empty;
                }
            }
        }
        private void ShotArea(pair[] coordinates)
        {
            foreach (pair varArea in coordinates)
            {
                pair[,] area = AreaAroundCoordinate(varArea);
                foreach (pair varPair in area)
                {
                    Cell currentCell = Field[varPair.x, varPair.y];
                    if (currentCell.State == FieldStates.empty)
                        currentCell.State = FieldStates.missed;
                }
            }
        }
        public void strike(pair coord)
        {
            Cell selectedCell = Field[coord.x, coord.y];
            switch (selectedCell.State)
            {
                case FieldStates.empty:
                    {
                        selectedCell.State = FieldStates.missed;
                        break;
                    }
                case FieldStates.withShip:
                    {
                        foreach (pair[] varKey in ShipTable.Keys)
                        {
                            foreach (pair varPair in varKey)
                            {
                                if (varPair.IsEqual(coord))
                                {
                                    ShipTable[varKey].HitCount++;
                                    if (ShipTable[varKey].IsDrowned)
                                    {
                                        selectedCell.State = FieldStates.shipDrown;
                                        ShipTable.Remove(varKey);
                                        ShotArea(varKey);
                                        break;
                                    }
                                    else
                                    {
                                        selectedCell.State = FieldStates.shipShot;
                                        break;
                                    }

                                }
                            }
                        }
                        throw new Exception("Несовпадение клеток поля и таблицы кораблей");
                    }
                default:
                    {
                        //make event govnyano vystrelil, davai po novoi
                        break;
                    }
            }
        }

    }
}
