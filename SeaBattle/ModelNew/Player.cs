using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SeaBattle.ModelNew
{
	class Player : IPlayer
	{
		private Field field;
		private GUIField guiField;
		private EventHandler onTurnStart;
		private EventHandler onTurnEnd;

		public Player(Field field, GUIField guiField, EventHandler onTurnStart, EventHandler onTurnEnd)
		{
			this.field = field;
			this.guiField = guiField;
			this.onTurnStart = onTurnStart;
			this.onTurnEnd = onTurnEnd;
		}

		public Field getField()
		{
			return this.field;
		}

		public void turnStart()
		{
			onTurnStart(this, new EventArgs());
		}

		public void turnContinue()
		{

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
			onTurnEnd(this, new EventArgs());
		}
	}
}
