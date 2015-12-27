using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ModelNew
{
	class CellPosition
	{
		public int x { get; private set; }
		public int y { get; private set; }
		public CellPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	class Field
	{
		private Cell[,] cells = new Cell[10, 10];
		private Dictionary<Ship, CellPosition> ships = new Dictionary<Ship, CellPosition>();

		public Field()
		{
			for (int i = 0; i < 10; i++)
				for (int j = 0; j < 10; j++)
					cells[i, j] = new Cell(CellState.Empty);
		}

		// разместить корабль
		public void addShip(Ship ship, CellPosition pos)
		{
			for (int i = 0; i < ship.size; i++)
			{
				if (ship.orientation == ShipOrientation.Horizontal)
					cells[pos.x + i, pos.y] = new Cell(CellState.Fill);
				else
					cells[pos.x, pos.y + i] = new Cell(CellState.Fill);
			}
			ships.Add(ship, pos);
		}

		// проверить, можно ли разместить фрагмент корабля в клетке
		bool isEmptyCell(CellPosition pos)
		{
			if (pos.x < 0 || pos.x > 9 || pos.y < 0 || pos.y > 9)
				return false;

			for (int i = pos.x - 1; i <= pos.x + 1; i++)
				for (int j = pos.y - 1; j <= pos.y + 1; j++)
					if (j >= 0 && j <= 9 && i >= 0 && i <= 9 && cells[i, j].state != CellState.Empty)
						return false;
			return true;
		}

		// проверить, можно ли разместить корабль
		public bool canAddShip(Ship ship, CellPosition pos)
		{
			if (ship.orientation == ShipOrientation.Horizontal)
				for (int i = 0; i < ship.size; i++)
				{
					if (isEmptyCell(new CellPosition(pos.x + i, pos.y)) == false)
						return false;
				}
			else
				for (int i = 0; i < ship.size; i++)
				{
					if (isEmptyCell(new CellPosition(pos.x, pos.y + i)) == false)
						return false;
				}
			return true;
		}

		// возвращает true, еслина поле размещен указанный корабль
		public bool containShip(Ship ship)
		{
			return ships.ContainsKey(ship);
		}

		// удаляет корабль с поля
		public void removeShip(Ship ship)
		{
			CellPosition head = ships[ship];
			for (int i = 0; i < ship.size; i++)
			{
				if (ship.orientation == ShipOrientation.Horizontal)
					cells[head.x + i, head.y] = new Cell(CellState.Empty);
				else
					cells[head.x, head.y + i] = new Cell(CellState.Empty);
			}
			ships.Remove(ship);
		}

		public bool checkDrown(CellPosition pos)
		{
			CellPosition start = getShipPosByPart(pos);
			Ship ship = getShipByPos(start);
			if (ship.orientation == ShipOrientation.Horizontal)
			{
				for (int i = 0; i < ship.size; i++)
					if (getCell(new CellPosition(start.x + i, start.y)) != CellState.Fired)
						return false;
			}
			else
			{
				for (int i = 0; i < ship.size; i++)
					if (getCell(new CellPosition(start.x, start.y + i)) != CellState.Fired)
						return false;
			}
			return true;
		}

		public CellPosition getShipPosByPart(CellPosition cell)
		{
			if (getCell(cell) != CellState.Fill && getCell(cell) != CellState.Fired)
				return null;
			if (cell.y > 0 && getShipPosByPart(new CellPosition(cell.x, cell.y - 1)) != null)
				return getShipPosByPart(new CellPosition(cell.x, cell.y - 1));
			else if (cell.x > 0 && getShipPosByPart(new CellPosition(cell.x - 1, cell.y)) != null)
				return getShipPosByPart(new CellPosition(cell.x - 1, cell.y));
			else
				return cell;
		}

		public Ship getShipByPos(CellPosition pos)
		{
			foreach (var s in ships)
				if (s.Value.x == pos.x && s.Value.y == pos.y)
					return s.Key;
			return null;
		}

		public ShootResult makeShoot(CellPosition pos)
		{
			if (getCell(pos) != CellState.Checked && getCell(pos) != CellState.Fired)
			{
				if (getCell(pos) == CellState.Fill)
				{
					setCell(pos, CellState.Fired);
					if (checkDrown(pos))
						return ShootResult.ShipDrowned;
					else
						return ShootResult.ShipHit;
				}
				else
				{
					setCell(pos, CellState.Checked);
					return ShootResult.Missed;
				}
			}
			else
				return ShootResult.Checked;
		}

		// отметить ячейку состоянием state
		public void setCell(CellPosition pos, CellState state)
		{
			cells[pos.x, pos.y] = new Cell(state);
		}

		// возвращает состояние ячейки
		public CellState getCell(CellPosition pos)
		{
			return cells[pos.x, pos.y].state;
		}

		public int getCountAlive()
		{
			int res = 0;
			foreach (var pair in ships)
				if (checkDrown(ships[pair.Key]) == false)
					res++;
			return res;
		}
	}
}
