using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000027 RID: 39
	public class Workbook
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00005C48 File Offset: 0x00004C48
		internal Workbook(XlsDocument doc)
		{
			this._doc = doc;
			this._worksheets = new Worksheets(this._doc);
			this._fonts = new Fonts(this._doc);
			this._formats = new Formats(this._doc);
			this._styles = new Styles(this._doc);
			this._xfs = new XFs(this._doc, this);
			this._palette = new Palette(this);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005CE5 File Offset: 0x00004CE5
		internal Workbook(XlsDocument doc, Bytes bytes, Workbook.BytesReadCallback bytesReadCallback) : this(doc)
		{
			this.ReadBytes(bytes, bytesReadCallback);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005CF6 File Offset: 0x00004CF6
		public Worksheets Worksheets
		{
			get
			{
				return this._worksheets;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005CFE File Offset: 0x00004CFE
		public Fonts Fonts
		{
			get
			{
				return this._fonts;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005D06 File Offset: 0x00004D06
		public Formats Formats
		{
			get
			{
				return this._formats;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005D0E File Offset: 0x00004D0E
		public Styles Styles
		{
			get
			{
				return this._styles;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00005D16 File Offset: 0x00004D16
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00005D1E File Offset: 0x00004D1E
		public bool ProtectContents
		{
			get
			{
				return this._protectContents;
			}
			set
			{
				this._protectContents = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005D27 File Offset: 0x00004D27
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005D2F File Offset: 0x00004D2F
		public bool ProtectWindowSettings
		{
			get
			{
				return this._protectWindowSettings;
			}
			set
			{
				this._protectWindowSettings = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005D38 File Offset: 0x00004D38
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00005D40 File Offset: 0x00004D40
		public bool ShareStrings
		{
			get
			{
				return this._shareStrings;
			}
			set
			{
				this._shareStrings = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005D49 File Offset: 0x00004D49
		internal SharedStringTable SharedStringTable
		{
			get
			{
				return this._sharedStringTable;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005D51 File Offset: 0x00004D51
		internal XFs XFs
		{
			get
			{
				return this._xfs;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005D59 File Offset: 0x00004D59
		internal Palette Palette
		{
			get
			{
				return this._palette;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005D94 File Offset: 0x00004D94
		internal Bytes Bytes
		{
			get
			{
				if (this._worksheets.Count == 0)
				{
					this._worksheets.Add("Sheet1");
				}
				Bytes bytes = new Bytes();
				Bytes bytes2 = new Bytes();
				Bytes bytes3 = new Bytes();
				Bytes bytes4 = new Bytes();
				bytes.Append(Record.GetBytes(RID.BOF, new byte[]
				{
					0,
					6,
					5,
					0,
					175,
					24,
					205,
					7,
					201,
					64,
					0,
					0,
					6,
					1,
					0,
					0
				}));
				if (this._protectContents)
				{
					Bytes bytes5 = bytes;
					byte[] protect = RID.PROTECT;
					byte[] array = new byte[2];
					array[0] = 1;
					bytes5.Append(Record.GetBytes(protect, array));
				}
				if (this._protectWindowSettings)
				{
					Bytes bytes6 = bytes;
					byte[] windowprotect = RID.WINDOWPROTECT;
					byte[] array2 = new byte[2];
					array2[0] = 1;
					bytes6.Append(Record.GetBytes(windowprotect, array2));
				}
				bytes.Append(Record.GetBytes(RID.WINDOW1, new byte[]
				{
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					56,
					0,
					0,
					0,
					0,
					0,
					0,
					0,
					88,
					2
				}));
				bytes.Append(this._fonts.Bytes);
				bytes.Append(this._formats.Bytes);
				bytes.Append(this._xfs.Bytes);
				bytes.Append(Record.GetBytes(RID.STYLE, new byte[]
				{
					16,
					128,
					0,
					byte.MaxValue
				}));
				if (this.SharedStringTable.CountUnique > 0U)
				{
					bytes3.Append(this.SharedStringTable.Bytes);
				}
				bytes3.Append(Record.GetBytes(RID.EOF, new byte[0]));
				int num = bytes.Length + bytes3.Length;
				for (int i = 0; i < this._worksheets.Count; i++)
				{
					num += XlsDocument.GetUnicodeString(this._worksheets[i].Name, 8).Length + 10;
				}
				this._worksheets.StreamOffset = num;
				for (int j = 0; j < this._worksheets.Count; j++)
				{
					if (j > 0)
					{
						num += this._worksheets[j - 1].StreamByteLength;
					}
					Worksheet worksheet = this._worksheets[j];
					bytes2.Append(Workbook.BOUNDSHEET(worksheet, num));
					bytes4.Append(worksheet.Bytes);
				}
				bytes.Append(bytes2);
				bytes.Append(bytes3);
				bytes.Append(bytes4);
				if (this._doc.ForceStandardOle2Stream)
				{
					bytes = this._doc.GetStandardOLE2Stream(bytes);
				}
				return bytes;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005FE0 File Offset: 0x00004FE0
		private void ReadBytes(Bytes bytes, Workbook.BytesReadCallback bytesReadCallback)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (bytes.Length == 0)
			{
				throw new ArgumentException("can't be zero-length", "bytes");
			}
			SortedList<ushort, ushort> sortedList = new SortedList<ushort, ushort>();
			List<Record> all = Record.GetAll(bytes);
			List<Record> list = new List<Record>();
			List<Record> list2 = new List<Record>();
			List<Record> list3 = new List<Record>();
			List<Record> list4 = new List<Record>();
			Record record = Record.Empty;
			SortedList<int, List<Record>> sortedList2 = new SortedList<int, List<Record>>();
			int num = -1;
			foreach (Record record2 in all)
			{
				if (num >= 0)
				{
					if (!sortedList2.ContainsKey(num))
					{
						sortedList2[num] = new List<Record>();
					}
					sortedList2[num].Add(record2);
					if (record2.RID == RID.EOF)
					{
						num++;
					}
				}
				else if (record2.RID == RID.FONT)
				{
					list.Add(record2);
				}
				else if (record2.RID == RID.FORMAT)
				{
					list2.Add(record2);
				}
				else if (record2.RID == RID.XF)
				{
					list3.Add(record2);
				}
				else if (record2.RID == RID.BOUNDSHEET)
				{
					list4.Add(record2);
				}
				else if (record2.RID == RID.SST)
				{
					record = record2;
				}
				else if (record2.RID == RID.EOF)
				{
					num++;
				}
			}
			SortedList<ushort, Font> sortedList3 = new SortedList<ushort, Font>();
			SortedList<ushort, string> sortedList4 = new SortedList<ushort, string>();
			new SortedList<ushort, XF>();
			ushort num2 = 0;
			foreach (Record record3 in list)
			{
				Font font = new Font(this._doc, record3.Data);
				SortedList<ushort, Font> sortedList5 = sortedList3;
				ushort num3 = num2;
				num2 = (ushort)(num3 + 1);
				sortedList5[num3] = font;
				this.Fonts.Add(font);
			}
			foreach (Record record4 in list2)
			{
				Bytes data = record4.Data;
				string text = UnicodeBytes.Read(data.Get(2, data.Length - 2), 16);
				num2 = BitConverter.ToUInt16(data.Get(2).ByteArray, 0);
				sortedList4[num2] = text;
				this.Formats.Add(text);
			}
			num2 = 0;
			while ((int)num2 < list3.Count)
			{
				Record record5 = list3[(int)num2];
				Bytes data2 = record5.Data;
				ushort key = BitConverter.ToUInt16(data2.Get(0, 2).ByteArray, 0);
				ushort num4 = BitConverter.ToUInt16(data2.Get(2, 2).ByteArray, 0);
				if (sortedList3.ContainsKey(key))
				{
					Font font2 = sortedList3[key];
					string format;
					if (sortedList4.ContainsKey(num4))
					{
						format = sortedList4[num4];
					}
					else
					{
						if (!this._formats.ContainsKey(num4))
						{
							throw new ApplicationException(string.Format("Format {0} not found in read FORMAT records or standard/default FORMAT records.", num4));
						}
						format = this._formats[num4];
					}
					sortedList[num2] = this.XFs.Add(new XF(this._doc, record5.Data, font2, format));
				}
				num2 += 1;
			}
			this.XFs.XfIdxLookups = sortedList;
			if (record != Record.Empty)
			{
				this.SharedStringTable.ReadBytes(record);
			}
			if (bytesReadCallback != null)
			{
				bytesReadCallback(all);
			}
			for (int i = 0; i < list4.Count; i++)
			{
				this._worksheets.Add(list4[i], sortedList2[i]);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000063BC File Offset: 0x000053BC
		private static Bytes BOUNDSHEET(Worksheet sheet, int basePosition)
		{
			Bytes bytes = new Bytes();
			Bytes unicodeString = XlsDocument.GetUnicodeString(sheet.Name, 8);
			bytes.Append(WorksheetVisibility.GetBytes(sheet.Visibility));
			bytes.Append(WorksheetType.GetBytes(sheet.SheetType));
			bytes.Append(unicodeString);
			bytes.Prepend(BitConverter.GetBytes(basePosition));
			bytes.Prepend(BitConverter.GetBytes((ushort)bytes.Length));
			bytes.Prepend(RID.BOUNDSHEET);
			return bytes;
		}

		// Token: 0x0400018C RID: 396
		private readonly XlsDocument _doc;

		// Token: 0x0400018D RID: 397
		private readonly Worksheets _worksheets;

		// Token: 0x0400018E RID: 398
		private readonly Fonts _fonts;

		// Token: 0x0400018F RID: 399
		private readonly Formats _formats;

		// Token: 0x04000190 RID: 400
		private readonly Styles _styles;

		// Token: 0x04000191 RID: 401
		private readonly XFs _xfs;

		// Token: 0x04000192 RID: 402
		private readonly Palette _palette;

		// Token: 0x04000193 RID: 403
		private readonly SharedStringTable _sharedStringTable = new SharedStringTable();

		// Token: 0x04000194 RID: 404
		private bool _shareStrings;

		// Token: 0x04000195 RID: 405
		private bool _protectContents;

		// Token: 0x04000196 RID: 406
		private bool _protectWindowSettings;

		// Token: 0x04000197 RID: 407
		private string _password = string.Empty;

		// Token: 0x04000198 RID: 408
		private bool _protectRevisions;

		// Token: 0x04000199 RID: 409
		private string _revisionsPassword = string.Empty;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060000CD RID: 205
		internal delegate void BytesReadCallback(List<Record> records);
	}
}
