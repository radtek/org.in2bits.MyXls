using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x0200002F RID: 47
	public class Cell : IXFTarget
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x000068F0 File Offset: 0x000058F0
		internal Cell(Worksheet worksheet)
		{
			this._worksheet = worksheet;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000690D File Offset: 0x0000590D
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006915 File Offset: 0x00005915
		public CellCoordinate Coordinate
		{
			get
			{
				return this._coordinate;
			}
			set
			{
				this._coordinate = value;
				this._row = value.Row;
				this._column = value.Column;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006938 File Offset: 0x00005938
		public ushort Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00006940 File Offset: 0x00005940
		public ushort Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006948 File Offset: 0x00005948
		public CellTypes Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006950 File Offset: 0x00005950
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000069A0 File Offset: 0x000059A0
		public object Value
		{
			get
			{
				if (this._type == CellTypes.Text && !(this._value is string))
				{
					return this._worksheet.Document.Workbook.SharedStringTable.GetString((uint)this._value);
				}
				return this._value;
			}
			set
			{
				if (value == null)
				{
					this._type = CellTypes.Null;
				}
				else if (value is bool)
				{
					this._type = CellTypes.Integer;
				}
				else if (value is string)
				{
					this._type = CellTypes.Text;
				}
				else if (value is short)
				{
					this._type = CellTypes.Integer;
				}
				else if (value is int)
				{
					this._type = CellTypes.Integer;
				}
				else if (value is long)
				{
					this._type = CellTypes.Integer;
				}
				else if (value is float)
				{
					this._type = CellTypes.Float;
				}
				else if (value is double)
				{
					this._type = CellTypes.Float;
				}
				else if (value is decimal)
				{
					this._type = CellTypes.Float;
				}
				else
				{
					if (!(value is DateTime))
					{
						throw new NotSupportedException(string.Format("values of type {0}", value.GetType().Name));
					}
					value = ((DateTime)value).ToOADate();
					this._type = CellTypes.Float;
				}
				if (this._type == CellTypes.Text && (value as string).Length > 32767)
				{
					throw new ApplicationException(string.Format("Text in Cell Row {0} Col {1} is longer than maximum allowed {2}", this.Row, this.Column, 32767));
				}
				if (this._type == CellTypes.Text && this._worksheet.Document.Workbook.ShareStrings)
				{
					this._value = this._worksheet.Document.Workbook.SharedStringTable.Add((string)value);
					return;
				}
				this._value = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00006B30 File Offset: 0x00005B30
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006B8C File Offset: 0x00005B8C
		internal XF ExtendedFormat
		{
			get
			{
				XF xf;
				if (this._xfIdx == -1)
				{
					xf = this._worksheet.Document.Workbook.XFs.DefaultUserXF;
				}
				else
				{
					xf = this._worksheet.Document.Workbook.XFs[this._xfIdx];
				}
				xf.Target = this;
				return xf;
			}
			set
			{
				this._xfIdx = ((value == null) ? -1 : ((int)value.Id));
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006BA0 File Offset: 0x00005BA0
		internal void SetXfIndex(int xfIndex)
		{
			this._xfIdx = xfIndex;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006BA9 File Offset: 0x00005BA9
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006BB8 File Offset: 0x00005BB8
		public Font Font
		{
			get
			{
				return this.ExtendedFormat.Font;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.Font = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006BDF File Offset: 0x00005BDF
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006BEC File Offset: 0x00005BEC
		public string Format
		{
			get
			{
				return this.ExtendedFormat.Format;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.Format = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006C13 File Offset: 0x00005C13
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006C20 File Offset: 0x00005C20
		public Style Style
		{
			get
			{
				return this.ExtendedFormat.Style;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.Style = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006C47 File Offset: 0x00005C47
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006C54 File Offset: 0x00005C54
		public HorizontalAlignments HorizontalAlignment
		{
			get
			{
				return this.ExtendedFormat.HorizontalAlignment;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.HorizontalAlignment = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006C7B File Offset: 0x00005C7B
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006C88 File Offset: 0x00005C88
		public bool TextWrapRight
		{
			get
			{
				return this.ExtendedFormat.TextWrapRight;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.TextWrapRight = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006CAF File Offset: 0x00005CAF
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006CBC File Offset: 0x00005CBC
		public VerticalAlignments VerticalAlignment
		{
			get
			{
				return this.ExtendedFormat.VerticalAlignment;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.VerticalAlignment = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006CE3 File Offset: 0x00005CE3
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006CF0 File Offset: 0x00005CF0
		public short Rotation
		{
			get
			{
				return this.ExtendedFormat.Rotation;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.Rotation = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006D17 File Offset: 0x00005D17
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006D24 File Offset: 0x00005D24
		public ushort IndentLevel
		{
			get
			{
				return this.ExtendedFormat.IndentLevel;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.IndentLevel = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006D4B File Offset: 0x00005D4B
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006D58 File Offset: 0x00005D58
		public bool ShrinkToCell
		{
			get
			{
				return this.ExtendedFormat.ShrinkToCell;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.ShrinkToCell = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006D7F File Offset: 0x00005D7F
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006D8C File Offset: 0x00005D8C
		public TextDirections TextDirection
		{
			get
			{
				return this.ExtendedFormat.TextDirection;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.TextDirection = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006DB3 File Offset: 0x00005DB3
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006DC0 File Offset: 0x00005DC0
		public bool Locked
		{
			get
			{
				return this.ExtendedFormat.CellLocked;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.CellLocked = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006DE7 File Offset: 0x00005DE7
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006DF4 File Offset: 0x00005DF4
		public bool FormulaHidden
		{
			get
			{
				return this.ExtendedFormat.FormulaHidden;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.FormulaHidden = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006E1B File Offset: 0x00005E1B
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006E28 File Offset: 0x00005E28
		public bool UseNumber
		{
			get
			{
				return this.ExtendedFormat.UseNumber;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseNumber = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006E4F File Offset: 0x00005E4F
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006E5C File Offset: 0x00005E5C
		public bool UseFont
		{
			get
			{
				return this.ExtendedFormat.UseFont;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseFont = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006E83 File Offset: 0x00005E83
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006E90 File Offset: 0x00005E90
		public bool UseMisc
		{
			get
			{
				return this.ExtendedFormat.UseMisc;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseMisc = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006EB7 File Offset: 0x00005EB7
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006EC4 File Offset: 0x00005EC4
		public bool UseBorder
		{
			get
			{
				return this.ExtendedFormat.UseBorder;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseBorder = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006EEB File Offset: 0x00005EEB
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006EF8 File Offset: 0x00005EF8
		public bool UseBackground
		{
			get
			{
				return this.ExtendedFormat.UseBackground;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseBackground = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006F1F File Offset: 0x00005F1F
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006F2C File Offset: 0x00005F2C
		public bool UseProtection
		{
			get
			{
				return this.ExtendedFormat.UseProtection;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.UseProtection = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006F53 File Offset: 0x00005F53
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00006F60 File Offset: 0x00005F60
		public ushort LeftLineStyle
		{
			get
			{
				return this.ExtendedFormat.LeftLineStyle;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.LeftLineStyle = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006F87 File Offset: 0x00005F87
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00006F94 File Offset: 0x00005F94
		public ushort RightLineStyle
		{
			get
			{
				return this.ExtendedFormat.RightLineStyle;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.RightLineStyle = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006FBB File Offset: 0x00005FBB
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006FC8 File Offset: 0x00005FC8
		public ushort TopLineStyle
		{
			get
			{
				return this.ExtendedFormat.TopLineStyle;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.TopLineStyle = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006FEF File Offset: 0x00005FEF
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006FFC File Offset: 0x00005FFC
		public ushort BottomLineStyle
		{
			get
			{
				return this.ExtendedFormat.BottomLineStyle;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.BottomLineStyle = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007023 File Offset: 0x00006023
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00007030 File Offset: 0x00006030
		public Color LeftLineColor
		{
			get
			{
				return this.ExtendedFormat.LeftLineColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.LeftLineColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00007057 File Offset: 0x00006057
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00007064 File Offset: 0x00006064
		public Color RightLineColor
		{
			get
			{
				return this.ExtendedFormat.RightLineColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.RightLineColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000708B File Offset: 0x0000608B
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00007098 File Offset: 0x00006098
		public bool DiagonalDescending
		{
			get
			{
				return this.ExtendedFormat.DiagonalDescending;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.DiagonalDescending = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000070BF File Offset: 0x000060BF
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000070CC File Offset: 0x000060CC
		public bool DiagonalAscending
		{
			get
			{
				return this.ExtendedFormat.DiagonalAscending;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.DiagonalAscending = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000070F3 File Offset: 0x000060F3
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00007100 File Offset: 0x00006100
		public Color TopLineColor
		{
			get
			{
				return this.ExtendedFormat.TopLineColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.TopLineColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007127 File Offset: 0x00006127
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00007134 File Offset: 0x00006134
		public Color BottomLineColor
		{
			get
			{
				return this.ExtendedFormat.BottomLineColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.BottomLineColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000715B File Offset: 0x0000615B
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007168 File Offset: 0x00006168
		public Color DiagonalLineColor
		{
			get
			{
				return this.ExtendedFormat.DiagonalLineColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.DiagonalLineColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000718F File Offset: 0x0000618F
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000719C File Offset: 0x0000619C
		public LineStyle DiagonalLineStyle
		{
			get
			{
				return this.ExtendedFormat.DiagonalLineStyle;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.DiagonalLineStyle = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000071C3 File Offset: 0x000061C3
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000071D0 File Offset: 0x000061D0
		public ushort Pattern
		{
			get
			{
				return this.ExtendedFormat.Pattern;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.Pattern = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000071F7 File Offset: 0x000061F7
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00007204 File Offset: 0x00006204
		public Color PatternColor
		{
			get
			{
				return this.ExtendedFormat.PatternColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.PatternColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000722B File Offset: 0x0000622B
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00007238 File Offset: 0x00006238
		public Color PatternBackgroundColor
		{
			get
			{
				return this.ExtendedFormat.PatternBackgroundColor;
			}
			set
			{
				XF extendedFormat = this.ExtendedFormat;
				extendedFormat.PatternBackgroundColor = value;
				this._xfIdx = (int)extendedFormat.Id;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007260 File Offset: 0x00006260
		internal Bytes Bytes
		{
			get
			{
				if (this._xfIdx < 0)
				{
					this._xfIdx = (int)this.ExtendedFormat.Id;
				}
				switch (this.Type)
				{
				case CellTypes.Error:
				case CellTypes.Formula:
					throw new NotSupportedException(string.Format("CellType {0}", this.Type));
				case CellTypes.Null:
					return this.BLANK();
				case CellTypes.Integer:
					return this.RK(false);
				case CellTypes.Text:
					if (this._value is string)
					{
						return this.LABEL();
					}
					return this.LABELSST();
				case CellTypes.Float:
				{
					double num = Convert.ToDouble(this._value);
					bool flag = false;
					double num2 = num;
					num2 *= 10.0;
					num2 *= 10.0;
					if (Math.Floor(num2) == num2)
					{
						flag = true;
					}
					Bytes bytes = this.RK(flag);
					if (Cell.DecodeRKFloat(bytes.GetBits(), flag) == num)
					{
						return bytes;
					}
					return this.NUMBER();
				}
				default:
					throw new Exception(string.Format("unexpected CellTypes {0}", this.Type));
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007368 File Offset: 0x00006368
		private Bytes BLANK()
		{
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(this.Row - 1));
			bytes.Append(BitConverter.GetBytes(this.Column - 1));
			bytes.Append(BitConverter.GetBytes((ushort)this._xfIdx));
			return Record.GetBytes(RID.BLANK, bytes);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000073C0 File Offset: 0x000063C0
		private Bytes LABEL()
		{
			Bytes bytes = new Bytes();
			bytes.Append(this.LABELBase());
			bytes.Append(XlsDocument.GetUnicodeString(((string)this.Value) ?? string.Empty, 16));
			return Record.GetBytes(RID.LABEL, bytes);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000740C File Offset: 0x0000640C
		private Bytes LABELSST()
		{
			Bytes bytes = new Bytes();
			bytes.Append(this.LABELBase());
			bytes.Append(BitConverter.GetBytes((uint)this._value));
			return Record.GetBytes(RID.LABELSST, bytes);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000744C File Offset: 0x0000644C
		private Bytes LABELBase()
		{
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(this.Row - 1));
			bytes.Append(BitConverter.GetBytes(this.Column - 1));
			bytes.Append(BitConverter.GetBytes((ushort)this._xfIdx));
			return bytes;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000749C File Offset: 0x0000649C
		private Bytes RK(bool trueFalse)
		{
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(this.Row - 1));
			bytes.Append(BitConverter.GetBytes(this.Column - 1));
			bytes.Append(BitConverter.GetBytes((ushort)this._xfIdx));
			if (this.Type == CellTypes.Integer)
			{
				bytes.Append(Cell.RKIntegerValue(this.Value, trueFalse));
			}
			else if (this.Type == CellTypes.Float)
			{
				bytes.Append(Cell.RKDecimalValue(this.Value, trueFalse));
			}
			return Record.GetBytes(RID.RK, bytes);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000752C File Offset: 0x0000652C
		private Bytes NUMBER()
		{
			double val = Convert.ToDouble(this.Value);
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(this.Row - 1));
			bytes.Append(BitConverter.GetBytes(this.Column - 1));
			bytes.Append(BitConverter.GetBytes((ushort)this._xfIdx));
			bytes.Append(Cell.NUMBERVal(val));
			return Record.GetBytes(RID.NUMBER, bytes);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000759C File Offset: 0x0000659C
		private static Bytes RKDecimalValue(object val, bool div100)
		{
			double num = Convert.ToDouble(val);
			if (div100)
			{
				num *= 10.0;
				num *= 10.0;
			}
			Bytes bytes = new Bytes(BitConverter.GetBytes(num));
			List<bool> list = new List<bool>();
			list.Add(div100);
			list.Add(false);
			list.AddRange(bytes.GetBits().Get(34, 30).Values);
			return new Bytes.Bits(list.ToArray()).GetBytes();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007618 File Offset: 0x00006618
		private static Bytes RKIntegerValue(object val, bool div100)
		{
			int num = Convert.ToInt32(val);
			if (num < -536870912 || num >= 536870912)
			{
				throw new ArgumentOutOfRangeException("val", string.Format("{0}: must be between -536870912 and 536870911", num));
			}
			num <<= 2;
			if (div100)
			{
				num++;
			}
			num += 2;
			byte[] bytes = BitConverter.GetBytes(num);
			return new Bytes(bytes);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007673 File Offset: 0x00006673
		private static Bytes NUMBERVal(double val)
		{
			return new Bytes(BitConverter.GetBytes(val));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007680 File Offset: 0x00006680
		public void UpdateId(XF fromXF)
		{
			this._xfIdx = (int)fromXF.Id;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007690 File Offset: 0x00006690
		internal void SetValue(byte[] rid, Bytes data)
		{
			if (rid == RID.RK)
			{
				this.DecodeRK(data);
				return;
			}
			if (rid == RID.LABEL)
			{
				this.DecodeLABEL(data);
				return;
			}
			if (rid == RID.LABELSST)
			{
				this.DecodeLABELSST(data);
				return;
			}
			if (rid == RID.NUMBER)
			{
				this.DecodeNUMBER(data);
				return;
			}
			throw new ApplicationException(string.Format("Unsupported RID {0}", RID.Name(rid)));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000076F2 File Offset: 0x000066F2
		internal void SetFormula(Bytes data, Record stringRecord)
		{
			this.DecodeFORMULA(data, stringRecord);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000076FC File Offset: 0x000066FC
		private void DecodeFORMULA(Bytes data, Record stringRecord)
		{
			if (stringRecord != null)
			{
				this._value = UnicodeBytes.Read(stringRecord.Data, 16);
			}
			this._type = CellTypes.Formula;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000771B File Offset: 0x0000671B
		private void DecodeNUMBER(Bytes data)
		{
			this._value = BitConverter.ToDouble(data.ByteArray, 0);
			this._type = CellTypes.Float;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000773B File Offset: 0x0000673B
		private void DecodeLABELSST(Bytes data)
		{
			this._value = BitConverter.ToUInt32(data.ByteArray, 0);
			this._type = CellTypes.Text;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000775B File Offset: 0x0000675B
		private void DecodeLABEL(Bytes data)
		{
			this._value = UnicodeBytes.Read(data, 16);
			this._type = CellTypes.Text;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007774 File Offset: 0x00006774
		private void DecodeRK(Bytes bytes)
		{
			Bytes.Bits bits = bytes.GetBits();
			bool div = bits.Values[0];
			bool flag = bits.Values[1];
			if (flag)
			{
				this.Value = Cell.DecodeRKInt(bits, div);
				this._type = ((this.Value is int) ? CellTypes.Integer : CellTypes.Float);
				return;
			}
			this.Value = Cell.DecodeRKFloat(bits, div);
			this._type = CellTypes.Float;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000077DC File Offset: 0x000067DC
		private static double DecodeRKFloat(Bytes.Bits bits, bool div100)
		{
			Bytes.Bits bits2 = bits.Get(2, 30);
			bits2.Prepend(false);
			bits2.Prepend(false);
			byte[] array = new byte[8];
			bits2.GetBytes().ByteArray.CopyTo(array, 4);
			BitConverter.GetBytes(1.0);
			double num = BitConverter.ToDouble(array, 0);
			if (div100)
			{
				num /= 100.0;
			}
			return num;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007844 File Offset: 0x00006844
		private static object DecodeRKInt(Bytes.Bits bits, bool div100)
		{
			object obj = bits.Get(2, 30).ToInt32();
			if (div100)
			{
				obj = Convert.ToDouble(obj) / 100.0;
			}
			return obj;
		}

		// Token: 0x040001C7 RID: 455
		private Worksheet _worksheet;

		// Token: 0x040001C8 RID: 456
		private object _value;

		// Token: 0x040001C9 RID: 457
		private CellTypes _type = CellTypes.Null;

		// Token: 0x040001CA RID: 458
		private ushort _row;

		// Token: 0x040001CB RID: 459
		private ushort _column;

		// Token: 0x040001CC RID: 460
		private CellCoordinate _coordinate;

		// Token: 0x040001CD RID: 461
		private int _xfIdx = -1;
	}
}
