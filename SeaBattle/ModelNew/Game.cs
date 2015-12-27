using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattle.ModelNew
{
	public enum ShootResult
	{
		Checked,
		Missed,
		ShipHit,
		ShipDrowned
	}

	struct ShipAndImage
	{
		public Ship ship;
		public Image image;
		private Thickness initialPos;
		public ShipAndImage(Ship ship, Image image)
		{
			this.ship = ship;
			this.image = image;
			this.initialPos = image.Margin;
		}
		public Thickness getInitialPosition()
		{
			return initialPos;
		}
	}

	class Game
	{
		bool playerDo;
		private IPlayer player1;
		private IPlayer player2;

		public Field getPlayerField()
		{
			return player1.getField();
		}

		public Field getForeignField()
		{
			return player2.getField();
		}

		public Field foreignField { get; set; }

		List<ShipAndImage> images;

		public Game()
		{
			images = new List<ShipAndImage>();
		}

		public void newGame(IPlayer player, IPlayer foreign)
		{
			playerDo = true;
			player1 = player;
			player2 = foreign;
		}

		public void startGame()
		{
			player1.turnStart();
		}

		public void registerShip(Ship ship, Image image)
		{
			images.Add(new ShipAndImage(ship, image));
		}

		public Image getImageByShip(Ship ship)
		{
			foreach (ShipAndImage S in images)
			{
				if (S.ship == ship)
					return S.image;
			}
			return null;
		}

		public Ship getShipByImage(Image image)
		{
			foreach (ShipAndImage S in images)
			{
				if (S.image == image)
					return S.ship;
			}
			return null;
		}

		public List<ShipAndImage> getAllShipsAndImages()
		{
			return images;
		}

		public ShootResult makeShoot(CellPosition pos)
		{
			IPlayer curPlayer = playerDo ? player1 : player2;
			IPlayer nextPlayer = playerDo ? player2 : player1;
			ShootResult res = nextPlayer.getShoot(pos);
			if (res == ShootResult.Missed)	// при промахе
			{
				playerDo = !playerDo;		// ход переходит другому игроку
				curPlayer.turnEnd();
				nextPlayer.turnStart();
				nextPlayer.turnContinue();
			}
			else if (nextPlayer.getField().getCountAlive() == 0)
			{
				curPlayer.turnEnd();
				MessageBox.Show("Победу одержал игрок " + (playerDo ? "1" : "2"));
			}
			else
				curPlayer.turnContinue();
			return res;
		}
		
	}
}
