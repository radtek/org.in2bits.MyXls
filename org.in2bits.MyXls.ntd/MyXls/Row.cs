using System;
using System.Collections.Generic;

namespace org.in2bits.MyXls
{
	// Token: 0x0200002E RID: 46
	public class Row
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000066F2 File Offset: 0x000056F2
		public Row()
		{
			this._minCellCol = 0;
			this._maxCellCol = 0;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00006713 File Offset: 0x00005713
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x0000671B File Offset: 0x0000571B
		public ushort RowIndex
		{
			get
			{
				return this._rowIndex;
			}
			internal set
			{
				this._rowIndex = value;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006724 File Offset: 0x00005724
		public bool CellExists(ushort atCol)
		{
			return this._cells.ContainsKey(atCol);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006734 File Offset: 0x00005734
		public void AddCell(Cell cell)
		{
			ushort column = cell.Column;
			if (this.CellExists(column))
			{
				throw new Exception(string.Format("Cell already exists at column {0}", column));
			}
			if (column < 1 || column > 256)
			{
				throw new ArgumentOutOfRangeException(string.Format("cell.Col {0} must be between 1 and 256", column));
			}
			if (this._minCellCol == 0)
			{
				this._minCellCol = column;
				this._maxCellCol = column;
			}
			else if (column < this._minCellCol)
			{
				this._minCellCol = column;
			}
			else if (column > this._maxCellCol)
			{
				this._maxCellCol = column;
			}
			this._cells.Add(column, cell);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000067D0 File Offset: 0x000057D0
		public ushort CellCount
		{
			get
			{
				return (ushort)this._cells.Count;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000067DE File Offset: 0x000057DE
		public Cell CellAtCol(ushort col)
		{
			if (!this.CellExists(col))
			{
				throw new Exception(string.Format("Cell at col {0} does not exist", col));
			}
			return this._cells[col];
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000680C File Offset: 0x0000580C
		public Cell GetCell(ushort cellIdx)
		{
			if (cellIdx < 1 || cellIdx > 256)
			{
				throw new ArgumentOutOfRangeException(string.Format("cellIdx {0} must be between 1 and 256", cellIdx));
			}
			if ((int)cellIdx > this._cells.Count)
			{
				throw new ArgumentOutOfRangeException(string.Format("cellIdx {0} is greater than the cell count {1}", cellIdx, this._cells.Count));
			}
			ushort num = 1;
			foreach (ushort key in this._cells.Keys)
			{
				if (num == cellIdx)
				{
					return this._cells[key];
				}
				num += 1;
			}
			throw new Exception(string.Format("Cell number {0} not found in row", cellIdx));
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000068E0 File Offset: 0x000058E0
		public ushort MinCellCol
		{
			get
			{
				return this._minCellCol;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000068E8 File Offset: 0x000058E8
		public ushort MaxCellCol
		{
			get
			{
				return this._maxCellCol;
			}
		}

		// Token: 0x040001C3 RID: 451
		private readonly SortedList<ushort, Cell> _cells = new SortedList<ushort, Cell>();

		// Token: 0x040001C4 RID: 452
		private ushort _rowIndex;

		// Token: 0x040001C5 RID: 453
		private ushort _minCellCol;

		// Token: 0x040001C6 RID: 454
		private ushort _maxCellCol;
	}
}
