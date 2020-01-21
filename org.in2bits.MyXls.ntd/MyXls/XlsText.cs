using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000021 RID: 33
	internal class XlsText
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00005027 File Offset: 0x00004027
		public XlsText(string text)
		{
			this._text = text;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00005036 File Offset: 0x00004036
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000503E File Offset: 0x0000403E
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x04000125 RID: 293
		private string _text;

		// Token: 0x02000022 RID: 34
		internal class FormattingRun
		{
			// Token: 0x060000A3 RID: 163 RVA: 0x00005047 File Offset: 0x00004047
			public FormattingRun(XF xf, ushort startOffset)
			{
				this._xfId = xf.Id;
				this._startOffset = startOffset;
			}

			// Token: 0x17000034 RID: 52
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005062 File Offset: 0x00004062
			public XF ExtendedFormat
			{
				set
				{
					this._xfId = value.Id;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005070 File Offset: 0x00004070
			// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005078 File Offset: 0x00004078
			public ushort StartOffset
			{
				get
				{
					return this._startOffset;
				}
				set
				{
					this._startOffset = value;
				}
			}

			// Token: 0x04000126 RID: 294
			private ushort _xfId;

			// Token: 0x04000127 RID: 295
			private ushort _startOffset;
		}
	}
}
