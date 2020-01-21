using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000042 RID: 66
	internal class RowBlocks
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000ACC0 File Offset: 0x00009CC0
		internal RowBlocks(Worksheet worksheet)
		{
			this._worksheet = worksheet;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000ACD0 File Offset: 0x00009CD0
		private ushort BlockCount
		{
			get
			{
				if (this._worksheet.Rows.MaxRow == 0U)
				{
					return 0;
				}
				ushort value = (ushort)(this._worksheet.Rows.MaxRow - this._worksheet.Rows.MinRow);
				return (ushort)((int)Math.Floor(value / 32m) + 1);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AD34 File Offset: 0x00009D34
		private Row GetBlockRow(ushort rbIdx, ushort brIdx)
		{
			Row row = new Row();
			ushort num = 0;
			ushort num2 = (ushort)((ulong)this._worksheet.Rows.MinRow + (ulong)((long)((rbIdx - 1) * 32)));
			CachedBlockRow cachedBlockRow = this._worksheet.CachedBlockRow;
			ushort num3 = num2;
			ushort num4 = (ushort)(num3 + 31);
			if (cachedBlockRow.RowBlockIndex == rbIdx)
			{
				if (cachedBlockRow.BlockRowIndex == brIdx)
				{
					return cachedBlockRow.Row;
				}
				if (brIdx < cachedBlockRow.BlockRowIndex)
				{
					num4 = cachedBlockRow.Row.RowIndex;
				}
				else if (brIdx > cachedBlockRow.BlockRowIndex)
				{
					if (cachedBlockRow.RowBlockIndex > 0)
					{
						num3 = (ushort)(cachedBlockRow.Row.RowIndex + 1);
						num = cachedBlockRow.BlockRowIndex;
					}
					else
					{
						num3 = 1;
					}
				}
			}
			for (ushort num5 = num3; num5 <= num4; num5 += 1)
			{
				if (this._worksheet.Rows.RowExists(num5))
				{
					num += 1;
					if (num == brIdx)
					{
						row = this._worksheet.Rows[num5];
						this._worksheet.CachedBlockRow = new CachedBlockRow(rbIdx, brIdx, row);
					}
				}
			}
			return row;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000AE3C File Offset: 0x00009E3C
		private ushort GetBlockRowCount(ushort idx)
		{
			ushort num = 0;
			ushort num2 = (ushort)((ulong)this._worksheet.Rows.MinRow + (ulong)((long)((idx - 1) * 32)));
			for (ushort num3 = num2; num3 <= num2 + 31; num3 += 1)
			{
				if (this._worksheet.Rows.RowExists(num3))
				{
					num += 1;
				}
			}
			return num;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000AE90 File Offset: 0x00009E90
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				ushort blockCount = this.BlockCount;
				int[] array = new int[(int)(blockCount + 1)];
				for (ushort num = 1; num <= blockCount; num += 1)
				{
					ushort blockRowCount = this.GetBlockRowCount(num);
					ushort[] array2 = new ushort[(int)(blockRowCount + 1)];
					Bytes bytes2 = new Bytes();
					Bytes bytes3 = new Bytes();
					for (ushort num2 = 1; num2 <= blockRowCount; num2 += 1)
					{
						if (num2 == 1)
						{
							array2[(int)num2] = (ushort)((blockRowCount - 1) * 20);
						}
						else if (num2 == 2)
						{
							array2[(int)num2] = (ushort)bytes3.Length;
						}
						else
						{
							array2[(int)num2] = (ushort)(bytes3.Length - (int)array2[(int)(num2 - 1)]);
						}
						Row blockRow = this.GetBlockRow(num, num2);
						bytes2.Append(RowBlocks.ROW(blockRow));
						int cellCount = (int)blockRow.CellCount;
						ushort num3 = 1;
						while ((int)num3 <= cellCount)
						{
							Cell cell = blockRow.GetCell(num3);
							bytes3.Append(cell.Bytes);
							num3 += 1;
						}
					}
					bytes.Append(bytes2);
					bytes.Append(bytes3);
					array2[0] = (ushort)(bytes2.Length + bytes3.Length);
					array[(int)num] = bytes.Length;
					bytes.Append(RowBlocks.DBCELL(array2));
				}
				this._worksheet.DBCellOffsets = array;
				return bytes;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000AFD4 File Offset: 0x00009FD4
		private static Bytes ROW(Row row)
		{
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(row.RowIndex - 1));
			bytes.Append(BitConverter.GetBytes(row.MinCellCol - 1));
			bytes.Append(BitConverter.GetBytes(row.MaxCellCol));
			bytes.Append(new byte[]
			{
				8,
				1
			});
			Bytes bytes2 = bytes;
			byte[] byteArray = new byte[2];
			bytes2.Append(byteArray);
			Bytes bytes3 = bytes;
			byte[] byteArray2 = new byte[2];
			bytes3.Append(byteArray2);
			Bytes bytes4 = bytes;
			byte[] array = new byte[4];
			array[1] = 1;
			array[2] = 15;
			bytes4.Append(array);
			return Record.GetBytes(RID.ROW, bytes);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B078 File Offset: 0x0000A078
		private static Bytes DBCELL(ushort[] cOff)
		{
			Bytes bytes = new Bytes();
			for (int i = 0; i < cOff.Length; i++)
			{
				if (i == 0)
				{
					bytes.Append(BitConverter.GetBytes((uint)cOff[i]));
				}
				else
				{
					bytes.Append(BitConverter.GetBytes(cOff[i]));
				}
			}
			return Record.GetBytes(RID.DBCELL, bytes);
		}

		// Token: 0x04000250 RID: 592
		private Worksheet _worksheet;
	}
}
