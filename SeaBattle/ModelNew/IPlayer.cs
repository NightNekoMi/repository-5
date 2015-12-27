using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ModelNew
{
	interface IPlayer
	{
		void turnStart();

		void turnContinue();

		ShootResult getShoot(CellPosition position);

		//void setField(Field field);
		Field getField();

		void turnEnd();

	}
}
