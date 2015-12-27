using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ModelNew
{
	class Bot : IPlayer
	{
		private Player player;
		private Field field;
		private GUIField guiField;
		private EventHandler<CellPosition> onShot;
		private int difficulty;
		Random randomGen = new Random();

		public Bot(Field field, GUIField guiField, Player player, EventHandler<CellPosition> onShot, int difficulty)
		{
			this.field = field;
			this.guiField = guiField;
			this.player = player;
			this.onShot = onShot;
			this.difficulty = difficulty;
		}

		public Field getField()
		{
			return field;
		}

		public void turnStart()
		{

		}

		public void turnContinue()
		{
			onShot(this, AI());
		}

		public ShootResult getShoot(CellPosition position)
		{
			ShootResult s = field.makeShoot(position);
			if (s == ShootResult.Missed)
				guiField.drawChecked(position);
			else if (s == ShootResult.ShipHit)
				guiField.drawCross(position);
			else if (s == ShootResult.ShipDrowned)
			{
				guiField.drawCross(position);
				CellPosition start = field.getShipPosByPart(position);
				Ship ship = field.getShipByPos(start);
				guiField.drawShip(ship, start);
			}
			return s;
		}

		public void turnEnd()
		{

		}

		CellPosition AI()
		{
			int N = randomGen.Next(0, 100);
			if (N < difficulty)
			{
				for (int i = 0; i < 10; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						CellPosition target = new CellPosition(i, j);
						if (player.getField().getCell(target) == CellState.Fill)
							return target;
					}
				}
			}

			for (int i = 0; i < 33; i++)
			{
				CellPosition target = new CellPosition(randomGen.Next(10), randomGen.Next(10));
				if (player.getField().getCell(target) == CellState.Empty)
					return target;
			}

			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					CellPosition target = new CellPosition(i, j);
					if (player.getField().getCell(target) == CellState.Empty)
						return target;
				}
			}

			return new CellPosition(0, 0);
		}

		public void RandomPlace()
		{
			List<Ship> stackOfShips = new List<Ship>();
			stackOfShips.Add(new Ship(4, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(3, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(3, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(2, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(2, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(2, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(1, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(1, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(1, ShipOrientation.Horizontal));
			stackOfShips.Add(new Ship(1, ShipOrientation.Horizontal));

			foreach (Ship ship in stackOfShips)
			{
				Random N = new Random();
				CellPosition pos;
				do
				{
					pos = new CellPosition(N.Next(10), N.Next(10));
					ship.orientation = (N.Next(2) == 0 ? ShipOrientation.Horizontal : ShipOrientation.Vertical);
				}
				while (field.canAddShip(ship, pos) == false);
				field.addShip(ship, pos);
			}
		}

	}
}
