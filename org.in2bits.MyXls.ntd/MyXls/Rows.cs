using System;
using System.Collections;
using System.Collections.Generic;

namespace org.in2bits.MyXls
{
	// Token: 0x0200003D RID: 61
	public class Rows : IEnumerable<Row>, IEnumerable
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000A641 File Offset: 0x00009641
		public Rows()
		{
			this._minRow = 0U;
			this._maxRow = 0U;
			this._rows = new SortedList<ushort, Row>();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A662 File Offset: 0x00009662
		public bool RowExists(ushort rowIdx)
		{
			return this._rows.ContainsKey(rowIdx);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A670 File Offset: 0x00009670
		public Row AddRow(ushort rowNum)
		{
			if (this.RowExists(rowNum))
			{
				return this._rows[rowNum];
			}
			if (this._minRow == 0U)
			{
				this._minRow = (uint)rowNum;
				this._maxRow = (uint)rowNum;
			}
			else
			{
				if ((uint)rowNum < this._minRow)
				{
					this._minRow = (uint)rowNum;
				}
				if ((uint)rowNum > this._maxRow)
				{
					this._maxRow = (uint)rowNum;
				}
			}
			Row row = new Row();
			row.RowIndex = rowNum;
			this._rows.Add(rowNum, row);
			return row;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000A6E6 File Offset: 0x000096E6
		public int Count
		{
			get
			{
				return this._rows.Count;
			}
		}

		// Token: 0x170000CD RID: 205
		public Row this[ushort rowNumber]
		{
			get
			{
				if (!this._rows.ContainsKey(rowNumber))
				{
					throw new Exception(string.Format("Row {0} not found", rowNumber));
				}
				return this._rows[rowNumber];
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000A725 File Offset: 0x00009725
		public uint MinRow
		{
			get
			{
				return this._minRow;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000A72D File Offset: 0x0000972D
		public uint MaxRow
		{
			get
			{
				return this._maxRow;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A834 File Offset: 0x00009834
		public IEnumerator<Row> GetEnumerator()
		{
			uint initialMinRow = this.MinRow;
			uint initialMaxRow = this.MaxRow;
			for (uint i = initialMinRow; i < initialMaxRow; i += 1U)
			{
				if (initialMinRow != this.MinRow || initialMaxRow != this.MaxRow)
				{
					throw new InvalidOperationException("The collection was modified after the enumerator was created.");
				}
				yield return this[(ushort)i];
			}
			yield break;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A850 File Offset: 0x00009850
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000232 RID: 562
		private readonly SortedList<ushort, Row> _rows;

		// Token: 0x04000233 RID: 563
		private uint _minRow;

		// Token: 0x04000234 RID: 564
		private uint _maxRow;
	}
}
