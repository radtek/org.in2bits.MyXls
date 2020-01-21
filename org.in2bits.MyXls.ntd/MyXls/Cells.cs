using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000045 RID: 69
	public class Cells
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000BD74 File Offset: 0x0000AD74
		public Cells(Worksheet worksheet)
		{
			this._worksheet = worksheet;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BD84 File Offset: 0x0000AD84
		internal Cell Add(ushort cellRow, ushort cellColumn)
		{
			Cell cell = new Cell(this._worksheet);
			bool flag = false;
			if (cellColumn < 1)
			{
				throw new ArgumentOutOfRangeException("cellColumn", string.Format("{0} must be >= 1", cellColumn));
			}
			if (cellColumn > 255)
			{
				throw new ArgumentOutOfRangeException("cellColumn", string.Format("{0} cellColumn must be <= {1}", cellColumn, 255));
			}
			if (cellRow < 1)
			{
				throw new ArgumentOutOfRangeException("cellRow", string.Format("{0} must be >= 1", cellColumn));
			}
			if (cellRow > 65535)
			{
				throw new ArgumentOutOfRangeException("cellRow", string.Format("{0} cellRow must be <= {1}", cellRow, ushort.MaxValue));
			}
			if (this._worksheet.Rows.RowExists(cellRow))
			{
				if (this._worksheet.Rows[cellRow].CellExists(cellColumn))
				{
					cell = this._worksheet.Rows[cellRow].CellAtCol(cellColumn);
					flag = true;
				}
			}
			else
			{
				this._worksheet.Rows.AddRow(cellRow);
			}
			if (flag)
			{
				return cell;
			}
			cell.Coordinate = new CellCoordinate(cellRow, cellColumn);
			if (this._minRow == 0)
			{
				this._minRow = cellRow;
				this._minCol = cellColumn;
				this._maxRow = cellRow;
				this._maxCol = cellColumn;
			}
			else
			{
				if (cellRow < this._minRow)
				{
					this._minRow = cellRow;
				}
				else if (cellRow > this._maxRow)
				{
					this._maxRow = cellRow;
				}
				if (cellColumn < this._minCol)
				{
					this._minCol = cellColumn;
				}
				else if (cellColumn > this._maxCol)
				{
					this._maxCol = cellColumn;
				}
			}
			this._worksheet.Rows[cellRow].AddCell(cell);
			this._cellCount += 1;
			return cell;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BF34 File Offset: 0x0000AF34
		public Cell Add(int cellRow, int cellColumn, object cellValue)
		{
			Util.ValidateUShort(cellRow, "cellRow");
			Util.ValidateUShort(cellColumn, "cellColumn");
			return this.Add((ushort)cellRow, (ushort)cellColumn, cellValue);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000BF57 File Offset: 0x0000AF57
		[Obsolete]
		public Cell AddValueCell(int cellRow, int cellColumn, object cellValue)
		{
			return this.Add(cellRow, cellColumn, cellValue);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BF64 File Offset: 0x0000AF64
		public Cell Add(ushort cellRow, ushort cellColumn, object cellValue)
		{
			Cell cell = this.Add(cellRow, cellColumn);
			cell.Value = cellValue;
			return cell;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000BF82 File Offset: 0x0000AF82
		[Obsolete]
		public Cell AddValueCell(ushort cellRow, ushort cellColumn, object cellValue)
		{
			return this.Add(cellRow, cellColumn, cellValue);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BF8D File Offset: 0x0000AF8D
		public Cell Add(int cellRow, int cellColumn, object cellValue, XF xf)
		{
			Util.ValidateUShort(cellRow, "cellRow");
			Util.ValidateUShort(cellColumn, "cellColumn");
			return this.Add((ushort)cellRow, (ushort)cellColumn, cellValue, xf);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BFB2 File Offset: 0x0000AFB2
		[Obsolete]
		public Cell AddValueCellXF(int cellRow, int cellColumn, object cellValue, XF xf)
		{
			return this.Add(cellRow, cellColumn, cellValue, xf);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BFC0 File Offset: 0x0000AFC0
		public Cell Add(ushort cellRow, ushort cellColumn, object cellValue, XF xf)
		{
			Cell cell = this.Add(cellRow, cellColumn, cellValue);
			cell.ExtendedFormat = xf;
			return cell;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BFE0 File Offset: 0x0000AFE0
		[Obsolete]
		public Cell AddValueCellXF(ushort cellRow, ushort cellColumn, object cellValue, XF xf)
		{
			return this.Add(cellRow, cellColumn, cellValue, xf);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BFF0 File Offset: 0x0000AFF0
		public void Merge(int rowMin, int rowMax, int colMin, int colMax)
		{
			MergeArea mergeArea = new MergeArea(rowMin, rowMax, colMin, colMax);
			this._worksheet.AddMergeArea(mergeArea);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000C015 File Offset: 0x0000B015
		public ushort Count
		{
			get
			{
				return this._cellCount;
			}
		}

		// Token: 0x04000278 RID: 632
		private readonly Worksheet _worksheet;

		// Token: 0x04000279 RID: 633
		private ushort _cellCount;

		// Token: 0x0400027A RID: 634
		private ushort _minRow;

		// Token: 0x0400027B RID: 635
		private ushort _maxRow;

		// Token: 0x0400027C RID: 636
		private ushort _minCol;

		// Token: 0x0400027D RID: 637
		private ushort _maxCol;
	}
}
