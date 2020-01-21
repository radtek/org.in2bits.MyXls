using System;
using System.Collections.Generic;
using System.Data;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000030 RID: 48
	public class Worksheet
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00007880 File Offset: 0x00006880
		internal Worksheet(XlsDocument doc)
		{
			this._doc = doc;
			this._visibility = WorksheetVisibilities.Default;
			this._sheettype = WorksheetTypes.Default;
			this._streamByteLength = 0;
			this._dbCellOffsets = new int[0];
			this._cells = new Cells(this);
			this._rows = new Rows();
			this._rowBlocks = new RowBlocks(this);
			this._cachedBlockRow = CachedBlockRow.Empty;
			this._columnInfos = new List<ColumnInfo>();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000790C File Offset: 0x0000690C
		internal Worksheet(XlsDocument doc, Record boundSheet, List<Record> sheetRecords) : this(doc)
		{
			byte[] byteArray = boundSheet.Data.ByteArray;
			byte b = byteArray[4];
			if (b == 0)
			{
				this._visibility = WorksheetVisibilities.Default;
			}
			else if (b == 1)
			{
				this._visibility = WorksheetVisibilities.Hidden;
			}
			else
			{
				if (b != 2)
				{
					throw new ApplicationException(string.Format("Unknown Visibility {0}", b));
				}
				this._visibility = WorksheetVisibilities.StrongHidden;
			}
			byte b2 = byteArray[5];
			if (b2 == 0)
			{
				this._sheettype = WorksheetTypes.Default;
			}
			else if (b2 == 2)
			{
				this._sheettype = WorksheetTypes.Chart;
			}
			else
			{
				if (b2 != 6)
				{
					throw new ApplicationException(string.Format("Unknown Sheet Type {0}", b2));
				}
				this._sheettype = WorksheetTypes.VBModule;
			}
			List<Record> list = new List<Record>();
			List<Record> list2 = new List<Record>();
			for (int i = 0; i < sheetRecords.Count; i++)
			{
				Record record = sheetRecords[i];
				if (record.IsCellRecord())
				{
					if (record.RID == RID.FORMULA)
					{
						Record record2 = null;
						if (i + i < sheetRecords.Count)
						{
							record2 = sheetRecords[i + 1];
							if (record2.RID != RID.STRING)
							{
								record2 = null;
							}
						}
						record = new FormulaRecord(record, record2);
					}
					list2.Add(record);
				}
				else if (record.RID == RID.ROW)
				{
					list.Add(record);
				}
			}
			foreach (Record record3 in list)
			{
				Bytes data = record3.Data;
				ushort rowNum = data.Get(0, 2).GetBits().ToUInt16();
				this.Rows.AddRow(rowNum);
				if (!data.Get(6, 2).GetBits().Values[15])
				{
					data.Get(6, 2).GetBits().Get(0, 14).ToUInt16();
				}
				bool flag = data.Get(10, 1).ByteArray[0] == 1;
			}
			foreach (Record record4 in list2)
			{
				this.AddCells(record4);
			}
			this._name = UnicodeBytes.Read(boundSheet.Data.Get(6, boundSheet.Data.Length - 6), 8);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007B74 File Offset: 0x00006B74
		private void AddCells(Record record)
		{
			Bytes data = record.Data;
			ushort num = data.Get(0, 2).GetBits().ToUInt16();
			ushort num2 = data.Get(2, 2).GetBits().ToUInt16();
			ushort num3 = num2;
			ushort num4 = 4;
			byte[] array = record.RID;
			bool flag = false;
			if (array == RID.MULBLANK)
			{
				flag = true;
				array = RID.BLANK;
			}
			else if (array == RID.MULRK)
			{
				flag = true;
				array = RID.RK;
			}
			if (flag)
			{
				num3 = data.Get(data.Length - 2, 2).GetBits().ToUInt16();
			}
			while (num2 <= num3)
			{
				Cell cell = this.Cells.Add((ushort)(num + 1), (ushort)(num2 + 1));
				data.Get((int)num4, 2).GetBits().ToUInt16();
				num4 += 2;
				if (array == RID.BLANK)
				{
					Bytes data2 = new Bytes();
				}
				else if (array == RID.RK)
				{
					Bytes data2 = data.Get((int)num4, 4);
					num4 += 4;
					cell.SetValue(array, data2);
				}
				else
				{
					Bytes data2 = data.Get((int)num4, data.Length - (int)num4);
					if (array == RID.FORMULA)
					{
						FormulaRecord formulaRecord = record as FormulaRecord;
						cell.SetFormula(data2, formulaRecord.StringRecord);
					}
					else
					{
						cell.SetValue(array, data2);
					}
				}
				num2 += 1;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007CC3 File Offset: 0x00006CC3
		internal XlsDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007CCB File Offset: 0x00006CCB
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00007CD3 File Offset: 0x00006CD3
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007CDC File Offset: 0x00006CDC
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00007CE4 File Offset: 0x00006CE4
		public WorksheetVisibilities Visibility
		{
			get
			{
				return this._visibility;
			}
			set
			{
				this._visibility = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007CED File Offset: 0x00006CED
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00007CF5 File Offset: 0x00006CF5
		public WorksheetTypes SheetType
		{
			get
			{
				return this._sheettype;
			}
			set
			{
				this._sheettype = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00007CFE File Offset: 0x00006CFE
		public Cells Cells
		{
			get
			{
				return this._cells;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007D06 File Offset: 0x00006D06
		public Rows Rows
		{
			get
			{
				return this._rows;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007D0E File Offset: 0x00006D0E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00007D16 File Offset: 0x00006D16
		public bool Protected
		{
			get
			{
				return this._protected;
			}
			set
			{
				this._protected = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007D1F File Offset: 0x00006D1F
		internal int StreamByteLength
		{
			get
			{
				return this._streamByteLength;
			}
		}

		// Token: 0x17000086 RID: 134
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00007D27 File Offset: 0x00006D27
		internal int[] DBCellOffsets
		{
			set
			{
				this._dbCellOffsets = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007D40 File Offset: 0x00006D40
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				Bytes bytes2 = new Bytes();
				Bytes bytes3 = new Bytes();
				Bytes bytes4 = new Bytes();
				Bytes bytes5 = new Bytes();
				bytes4.Append(this._rowBlocks.Bytes);
				bytes.Append(Record.GetBytes(RID.BOF, new byte[]
				{
					0,
					6,
					16,
					0,
					175,
					24,
					205,
					7,
					193,
					64,
					0,
					0,
					6,
					1,
					0,
					0
				}));
				if (this._protected)
				{
					Bytes bytes6 = bytes3;
					byte[] protect = RID.PROTECT;
					byte[] array = new byte[2];
					array[0] = 1;
					bytes6.Append(Record.GetBytes(protect, array));
				}
				bytes3.Append(this.COLINFOS());
				int num = this._doc.Workbook.Worksheets.StreamOffset;
				int index = this._doc.Workbook.Worksheets.GetIndex(this.Name);
				for (int i = 1; i < index; i++)
				{
					num += this._doc.Workbook.Worksheets[i].StreamByteLength;
				}
				num += bytes.Length + (20 + 4 * (this._dbCellOffsets.Length - 1)) + bytes3.Length;
				bytes2.Append(this.INDEX(num));
				bytes5.Append(this.WINDOW2());
				bytes5.Append(this.MERGEDCELLS());
				bytes5.Append(Record.GetBytes(RID.EOF, new byte[0]));
				bytes.Append(bytes2);
				bytes.Append(bytes3);
				bytes.Append(bytes4);
				bytes.Append(bytes5);
				this._streamByteLength = bytes.Length;
				return bytes;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007EC4 File Offset: 0x00006EC4
		private Bytes INDEX(int baseLength)
		{
			Bytes bytes = new Bytes();
			Bytes bytes2 = bytes;
			byte[] byteArray = new byte[4];
			bytes2.Append(byteArray);
			bytes.Append(BitConverter.GetBytes(this._rows.MinRow - 1U));
			bytes.Append(BitConverter.GetBytes(this._rows.MaxRow));
			bytes.Append(BitConverter.GetBytes(0U));
			for (int i = 1; i < this._dbCellOffsets.Length; i++)
			{
				bytes.Append(BitConverter.GetBytes((uint)(baseLength + this._dbCellOffsets[i])));
			}
			return Record.GetBytes(RID.INDEX, bytes);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007F54 File Offset: 0x00006F54
		private Bytes WINDOW2()
		{
			Bytes bytes = new Bytes();
			if (this._doc.Workbook.Worksheets.GetIndex(this.Name) == 0)
			{
				bytes.Append(new byte[]
				{
					182,
					6
				});
			}
			else
			{
				bytes.Append(new byte[]
				{
					182,
					4
				});
			}
			Bytes bytes2 = bytes;
			byte[] array = new byte[16];
			array[4] = 64;
			bytes2.Append(array);
			return Record.GetBytes(RID.WINDOW2, bytes);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007FDC File Offset: 0x00006FDC
		private Bytes COLINFOS()
		{
			Bytes bytes = new Bytes();
			for (int i = 0; i < this._columnInfos.Count; i++)
			{
				bytes.Append(this._columnInfos[i].Bytes);
			}
			return bytes;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008020 File Offset: 0x00007020
		private Bytes MERGEDCELLS()
		{
			Bytes bytes = new Bytes();
			int num = 0;
			int count = this._mergeAreas.Count;
			long num2 = 1027L;
			int num3 = (int)Math.Ceiling((double)this._mergeAreas.Count / (double)num2);
			for (int i = 0; i < num3; i++)
			{
				ushort num4 = 0;
				Bytes bytes2 = new Bytes();
				while (num < count && (ulong)num4 < (ulong)num2)
				{
					bytes2.Append(this.CellRangeAddress(this._mergeAreas[num]));
					num4 += 1;
					num++;
				}
				bytes2.Prepend(BitConverter.GetBytes(num4));
				bytes.Append(Record.GetBytes(RID.MERGEDCELLS, bytes2));
			}
			return bytes;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000080CB File Offset: 0x000070CB
		private Bytes CellRangeAddress(MergeArea mergeArea)
		{
			return this.CellRangeAddress(mergeArea.RowMin, mergeArea.RowMax, mergeArea.ColMin, mergeArea.ColMax);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000080F0 File Offset: 0x000070F0
		private Bytes CellRangeAddress(ushort minRow, ushort maxRow, ushort minCol, ushort maxCol)
		{
			minRow -= 1;
			maxRow -= 1;
			minCol -= 1;
			maxCol -= 1;
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(minRow));
			bytes.Append(BitConverter.GetBytes(maxRow));
			bytes.Append(BitConverter.GetBytes(minCol));
			bytes.Append(BitConverter.GetBytes(maxCol));
			return bytes;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000814E File Offset: 0x0000714E
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00008156 File Offset: 0x00007156
		internal CachedBlockRow CachedBlockRow
		{
			get
			{
				return this._cachedBlockRow;
			}
			set
			{
				this._cachedBlockRow = value;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000815F File Offset: 0x0000715F
		public void AddColumnInfo(ColumnInfo columnInfo)
		{
			this._columnInfos.Add(columnInfo);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008170 File Offset: 0x00007170
		public void AddMergeArea(MergeArea mergeArea)
		{
			foreach (MergeArea mergeArea2 in this._mergeAreas)
			{
				bool flag = false;
				bool flag2 = false;
				if (mergeArea.ColMin < mergeArea2.ColMin && mergeArea2.ColMax < mergeArea.ColMax)
				{
					flag = true;
				}
				else if ((mergeArea2.ColMin <= mergeArea.ColMin && mergeArea2.ColMax >= mergeArea.ColMin) || (mergeArea2.ColMin <= mergeArea.ColMax && mergeArea2.ColMax >= mergeArea.ColMax))
				{
					flag = true;
				}
				if (mergeArea.RowMin < mergeArea2.RowMin && mergeArea2.RowMax < mergeArea.RowMax)
				{
					flag2 = true;
				}
				else if ((mergeArea2.RowMin <= mergeArea.RowMin && mergeArea2.RowMax >= mergeArea.RowMin) || (mergeArea2.RowMin <= mergeArea.RowMax && mergeArea2.RowMax >= mergeArea.RowMax))
				{
					flag2 = true;
				}
				if (flag && flag2)
				{
					throw new ArgumentException("overlaps with existing MergeArea", "mergeArea");
				}
			}
			this._mergeAreas.Add(mergeArea);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000082C0 File Offset: 0x000072C0
		public void Write(DataTable table, int startRow, int startCol)
		{
			if (table.Columns.Count + startCol > 255)
			{
				throw new ApplicationException(string.Format("Table {0} has too many columns {1} to fit on Worksheet {2} with the given startCol {3}", new object[]
				{
					table.TableName,
					table.Columns.Count,
					255,
					startCol
				}));
			}
			if (table.Rows.Count + startRow > 65534)
			{
				throw new ApplicationException(string.Format("Table {0} has too many rows {1} to fit on Worksheet {2} with the given startRow {3}", new object[]
				{
					table.TableName,
					table.Rows.Count,
					65534,
					startRow
				}));
			}
			int num = startRow;
			int num2 = startCol;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				this.Cells.Add(num, num2++, dataColumn.ColumnName);
			}
			foreach (object obj2 in table.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				num++;
				num2 = startCol;
				foreach (object obj3 in dataRow.ItemArray)
				{
					object obj4 = obj3;
					if (obj3 == DBNull.Value)
					{
						obj4 = null;
					}
					if (dataRow.Table.Columns[num2 - startCol].DataType == typeof(byte[]))
					{
						obj4 = string.Format("[ByteArray({0})]", ((byte[])obj4).Length);
					}
					this.Cells.Add(num, num2++, obj4);
				}
			}
		}

		// Token: 0x040001CE RID: 462
		private readonly XlsDocument _doc;

		// Token: 0x040001CF RID: 463
		private readonly List<ColumnInfo> _columnInfos = new List<ColumnInfo>();

		// Token: 0x040001D0 RID: 464
		private readonly List<MergeArea> _mergeAreas = new List<MergeArea>();

		// Token: 0x040001D1 RID: 465
		private readonly Cells _cells;

		// Token: 0x040001D2 RID: 466
		private readonly Rows _rows;

		// Token: 0x040001D3 RID: 467
		private readonly RowBlocks _rowBlocks;

		// Token: 0x040001D4 RID: 468
		private WorksheetVisibilities _visibility;

		// Token: 0x040001D5 RID: 469
		private WorksheetTypes _sheettype;

		// Token: 0x040001D6 RID: 470
		private string _name;

		// Token: 0x040001D7 RID: 471
		private int _streamByteLength;

		// Token: 0x040001D8 RID: 472
		private int[] _dbCellOffsets;

		// Token: 0x040001D9 RID: 473
		private bool _protected;

		// Token: 0x040001DA RID: 474
		private CachedBlockRow _cachedBlockRow;
	}
}
