using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000017 RID: 23
	public class ColumnInfo
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000039F8 File Offset: 0x000029F8
		public ColumnInfo(XlsDocument doc, Worksheet worksheet)
		{
			this._doc = doc;
			this._worksheet = worksheet;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003A19 File Offset: 0x00002A19
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003A21 File Offset: 0x00002A21
		public ushort ColumnIndexStart
		{
			get
			{
				return this._colIdxStart;
			}
			set
			{
				this._colIdxStart = value;
				if (this._colIDxEnd < this._colIdxStart)
				{
					this._colIDxEnd = this._colIdxStart;
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003A44 File Offset: 0x00002A44
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003A4C File Offset: 0x00002A4C
		public ushort ColumnIndexEnd
		{
			get
			{
				return this._colIDxEnd;
			}
			set
			{
				this._colIDxEnd = value;
				if (this._colIdxStart > this._colIDxEnd)
				{
					this._colIdxStart = this._colIDxEnd;
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003A6F File Offset: 0x00002A6F
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003A77 File Offset: 0x00002A77
		public ushort Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003A80 File Offset: 0x00002A80
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003A8C File Offset: 0x00002A8C
		public XF ExtendedFormat
		{
			get
			{
				throw new NotSupportedException("ColumnInfo.get_ExtendedFormat");
			}
			set
			{
				throw new NotSupportedException("ColumnInfo.set_ExtendedFormat");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003A98 File Offset: 0x00002A98
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003AA0 File Offset: 0x00002AA0
		public bool Hidden
		{
			get
			{
				return this._hidden;
			}
			set
			{
				this._hidden = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003AA9 File Offset: 0x00002AA9
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003AB1 File Offset: 0x00002AB1
		public bool Collapsed
		{
			get
			{
				return this._collapsed;
			}
			set
			{
				this._collapsed = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003ABA File Offset: 0x00002ABA
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003AC2 File Offset: 0x00002AC2
		public byte OutlineLevel
		{
			get
			{
				return this._outlineLevel;
			}
			set
			{
				if (value > 7)
				{
					throw new ArgumentException(string.Format("value {0} must be between 0 and 7", value));
				}
				this._outlineLevel = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003AE8 File Offset: 0x00002AE8
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(BitConverter.GetBytes(this._colIdxStart));
				bytes.Append(BitConverter.GetBytes(this._colIDxEnd));
				bytes.Append(BitConverter.GetBytes(this._width));
				bytes.Append(BitConverter.GetBytes(0));
				bytes.Append(this.COLINFO_OPTION_FLAGS());
				bytes.Append(new byte[0]);
				return Record.GetBytes(RID.COLINFO, bytes);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003B60 File Offset: 0x00002B60
		private Bytes COLINFO_OPTION_FLAGS()
		{
			bool[] array = new bool[16];
			array[0] = this._hidden;
			Bytes bytes = new Bytes(this._outlineLevel);
			bool[] values = bytes.GetBits().Get(3).Values;
			values.CopyTo(array, 8);
			array[12] = this._collapsed;
			return new Bytes.Bits(array).GetBytes();
		}

		// Token: 0x04000097 RID: 151
		private readonly XlsDocument _doc;

		// Token: 0x04000098 RID: 152
		private readonly Worksheet _worksheet;

		// Token: 0x04000099 RID: 153
		private ushort _colIdxStart;

		// Token: 0x0400009A RID: 154
		private ushort _colIDxEnd;

		// Token: 0x0400009B RID: 155
		private ushort _width = 2560;

		// Token: 0x0400009C RID: 156
		private bool _hidden;

		// Token: 0x0400009D RID: 157
		private bool _collapsed;

		// Token: 0x0400009E RID: 158
		private byte _outlineLevel;
	}
}
