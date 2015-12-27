using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.ModelNew
{
	enum CellState
	{
		Fired,
		Checked,
		Empty,
		Fill
	}

	class Cell
	{
		public CellState state { get; private set; }

		public Cell(CellState state)
		{
			this.state = state;
		}
	}
}
