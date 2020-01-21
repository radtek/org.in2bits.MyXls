using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000038 RID: 56
	public class XF : ICloneable
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00008E39 File Offset: 0x00007E39
		internal XF(XlsDocument doc)
		{
			this._doc = doc;
			this._id = null;
			this.SetDefaults();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008E71 File Offset: 0x00007E71
		internal XF(XlsDocument doc, Bytes bytes, Font font, string format) : this(doc)
		{
			this.ReadBytes(bytes, font, format);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00008E84 File Offset: 0x00007E84
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00008E8C File Offset: 0x00007E8C
		internal IXFTarget Target
		{
			get
			{
				return this._targetObject;
			}
			set
			{
				this._targetObject = value;
				this._font.Target = this;
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008EA1 File Offset: 0x00007EA1
		internal void OnFontChange(Font newFont)
		{
			this._font = (Font)newFont.Clone();
			this.OnChange();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008EBA File Offset: 0x00007EBA
		internal void OnChange()
		{
			this._id = null;
			if (this._targetObject != null)
			{
				this._targetObject.UpdateId(this);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008EDC File Offset: 0x00007EDC
		private void SetDefaults()
		{
			this._font = new Font(this._doc, this);
			this._format = Formats.Default;
			this._style = new Style(this._doc, this);
			this._horizontalAlignment = HorizontalAlignments.Default;
			this._textWrapRight = false;
			this._verticalAlignment = VerticalAlignments.Default;
			this._rotation = 0;
			this._indentLevel = 0;
			this._shrinkToCell = false;
			this._textDirection = TextDirections.Default;
			this._cellLocked = false;
			this._formulaHidden = false;
			this._isStyleXF = false;
			this._useNumber = true;
			this._useFont = true;
			this._useMisc = true;
			this._useBorder = true;
			this._useBackground = true;
			this._useProtection = true;
			this._leftLineStyle = 0;
			this._rightLineStyle = 0;
			this._topLineStyle = 0;
			this._bottomLineStyle = 0;
			this._leftLineColor = Colors.DefaultLineColor;
			this._rightLineColor = Colors.DefaultLineColor;
			this._diagonalDescending = false;
			this._diagonalAscending = false;
			this._topLineColor = Colors.DefaultLineColor;
			this._bottomLineColor = Colors.DefaultLineColor;
			this._diagonalLineColor = Colors.DefaultLineColor;
			this._diagonalLineStyle = LineStyle.None;
			this._pattern = 0;
			this._patternColor = Colors.DefaultPatternColor;
			this._patternBackgroundColor = Colors.DefaultPatternBackgroundColor;
			this.OnChange();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00009013 File Offset: 0x00008013
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00009038 File Offset: 0x00008038
		public Font Font
		{
			get
			{
				if (this._font == null)
				{
					this._font = new Font(this._doc, this);
				}
				return this._font;
			}
			set
			{
				if (this._font == null && value == null)
				{
					return;
				}
				if (this._font != null && value != null && value.Equals(this._font))
				{
					return;
				}
				this._font = ((value == null) ? null : ((Font)value.Clone()));
				this.OnChange();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00009088 File Offset: 0x00008088
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00009090 File Offset: 0x00008090
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				if (value == null)
				{
					value = Formats.Default;
				}
				if (value.Length > 65535)
				{
					value = value.Substring(0, 65535);
				}
				if (string.Compare(value, this._format, false) == 0)
				{
					return;
				}
				this._format = (string)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000090E9 File Offset: 0x000080E9
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000910C File Offset: 0x0000810C
		public Style Style
		{
			get
			{
				if (this._style == null)
				{
					this._style = new Style(this._doc, this);
				}
				return this._style;
			}
			set
			{
				if (this._style == null && value == null)
				{
					return;
				}
				if (this._style != null && value != null && value.Equals(this._style))
				{
					return;
				}
				this._style = ((value == null) ? null : ((Style)value.Clone()));
				this.OnChange();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000915C File Offset: 0x0000815C
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00009164 File Offset: 0x00008164
		public HorizontalAlignments HorizontalAlignment
		{
			get
			{
				return this._horizontalAlignment;
			}
			set
			{
				if (value == this._horizontalAlignment)
				{
					return;
				}
				this._horizontalAlignment = value;
				this.OnChange();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000917D File Offset: 0x0000817D
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00009185 File Offset: 0x00008185
		public bool TextWrapRight
		{
			get
			{
				return this._textWrapRight;
			}
			set
			{
				if (value == this._textWrapRight)
				{
					return;
				}
				this._textWrapRight = value;
				this.OnChange();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000919E File Offset: 0x0000819E
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000091A6 File Offset: 0x000081A6
		public VerticalAlignments VerticalAlignment
		{
			get
			{
				return this._verticalAlignment;
			}
			set
			{
				if (value == this._verticalAlignment)
				{
					return;
				}
				this._verticalAlignment = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000091BF File Offset: 0x000081BF
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000091C7 File Offset: 0x000081C7
		public short Rotation
		{
			get
			{
				return this._rotation;
			}
			set
			{
				if (value == this._rotation)
				{
					return;
				}
				if (value < 0)
				{
					value = 0;
				}
				if (value > 180 && value != 255)
				{
					value = 0;
				}
				this._rotation = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000091FA File Offset: 0x000081FA
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00009202 File Offset: 0x00008202
		public ushort IndentLevel
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				if (value == this._indentLevel)
				{
					return;
				}
				this._indentLevel = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000921B File Offset: 0x0000821B
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009223 File Offset: 0x00008223
		public bool ShrinkToCell
		{
			get
			{
				return this._shrinkToCell;
			}
			set
			{
				if (value == this._shrinkToCell)
				{
					return;
				}
				this._shrinkToCell = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000923C File Offset: 0x0000823C
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009244 File Offset: 0x00008244
		public TextDirections TextDirection
		{
			get
			{
				return this._textDirection;
			}
			set
			{
				if (value == this._textDirection)
				{
					return;
				}
				this._textDirection = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000925D File Offset: 0x0000825D
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009265 File Offset: 0x00008265
		public bool CellLocked
		{
			get
			{
				return this._cellLocked;
			}
			set
			{
				if (value == this._cellLocked)
				{
					return;
				}
				this._cellLocked = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000927E File Offset: 0x0000827E
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00009286 File Offset: 0x00008286
		public bool FormulaHidden
		{
			get
			{
				return this._formulaHidden;
			}
			set
			{
				if (value == this._formulaHidden)
				{
					return;
				}
				this._formulaHidden = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000929F File Offset: 0x0000829F
		// (set) Token: 0x060001BD RID: 445 RVA: 0x000092A7 File Offset: 0x000082A7
		internal bool IsStyleXF
		{
			get
			{
				return this._isStyleXF;
			}
			set
			{
				if (value == this._isStyleXF)
				{
					return;
				}
				this._isStyleXF = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000092C0 File Offset: 0x000082C0
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000092C8 File Offset: 0x000082C8
		public bool UseNumber
		{
			get
			{
				return this._useNumber;
			}
			set
			{
				if (value == this._useNumber)
				{
					return;
				}
				this._useNumber = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000092E1 File Offset: 0x000082E1
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000092E9 File Offset: 0x000082E9
		public bool UseFont
		{
			get
			{
				return this._useFont;
			}
			set
			{
				if (value == this._useFont)
				{
					return;
				}
				this._useFont = value;
				this.OnChange();
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00009302 File Offset: 0x00008302
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000930A File Offset: 0x0000830A
		public bool UseMisc
		{
			get
			{
				return this._useMisc;
			}
			set
			{
				if (value == this._useMisc)
				{
					return;
				}
				this._useMisc = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009323 File Offset: 0x00008323
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000932B File Offset: 0x0000832B
		public bool UseBorder
		{
			get
			{
				return this._useBorder;
			}
			set
			{
				if (value == this._useBorder)
				{
					return;
				}
				this._useBorder = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00009344 File Offset: 0x00008344
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000934C File Offset: 0x0000834C
		public bool UseBackground
		{
			get
			{
				return this._useBackground;
			}
			set
			{
				if (value == this._useBackground)
				{
					return;
				}
				this._useBackground = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009365 File Offset: 0x00008365
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000936D File Offset: 0x0000836D
		public bool UseProtection
		{
			get
			{
				return this._useProtection;
			}
			set
			{
				if (value == this._useProtection)
				{
					return;
				}
				this._useProtection = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00009386 File Offset: 0x00008386
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000938E File Offset: 0x0000838E
		public ushort LeftLineStyle
		{
			get
			{
				return this._leftLineStyle;
			}
			set
			{
				if (value == this._leftLineStyle)
				{
					return;
				}
				this._leftLineStyle = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000093A7 File Offset: 0x000083A7
		// (set) Token: 0x060001CD RID: 461 RVA: 0x000093AF File Offset: 0x000083AF
		public ushort RightLineStyle
		{
			get
			{
				return this._rightLineStyle;
			}
			set
			{
				if (value == this._rightLineStyle)
				{
					return;
				}
				this._rightLineStyle = value;
				this.OnChange();
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001CE RID: 462 RVA: 0x000093C8 File Offset: 0x000083C8
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000093D0 File Offset: 0x000083D0
		public ushort TopLineStyle
		{
			get
			{
				return this._topLineStyle;
			}
			set
			{
				if (value == this._topLineStyle)
				{
					return;
				}
				this._topLineStyle = value;
				this.OnChange();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x000093E9 File Offset: 0x000083E9
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x000093F1 File Offset: 0x000083F1
		public ushort BottomLineStyle
		{
			get
			{
				return this._bottomLineStyle;
			}
			set
			{
				if (value == this._bottomLineStyle)
				{
					return;
				}
				this._bottomLineStyle = value;
				this.OnChange();
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000940A File Offset: 0x0000840A
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009412 File Offset: 0x00008412
		public Color LeftLineColor
		{
			get
			{
				return this._leftLineColor;
			}
			set
			{
				if (value.Equals(this._leftLineColor))
				{
					return;
				}
				this._leftLineColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000943A File Offset: 0x0000843A
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009442 File Offset: 0x00008442
		public Color RightLineColor
		{
			get
			{
				return this._rightLineColor;
			}
			set
			{
				if (value.Equals(this._rightLineColor))
				{
					return;
				}
				this._rightLineColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000946A File Offset: 0x0000846A
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009472 File Offset: 0x00008472
		public bool DiagonalDescending
		{
			get
			{
				return this._diagonalDescending;
			}
			set
			{
				if (value == this._diagonalDescending)
				{
					return;
				}
				this._diagonalDescending = value;
				this.OnChange();
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000948B File Offset: 0x0000848B
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009493 File Offset: 0x00008493
		public bool DiagonalAscending
		{
			get
			{
				return this._diagonalAscending;
			}
			set
			{
				if (value == this._diagonalAscending)
				{
					return;
				}
				this._diagonalAscending = value;
				this.OnChange();
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000094AC File Offset: 0x000084AC
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000094B4 File Offset: 0x000084B4
		public Color TopLineColor
		{
			get
			{
				return this._topLineColor;
			}
			set
			{
				if (value.Equals(this._topLineColor))
				{
					return;
				}
				this._topLineColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000094DC File Offset: 0x000084DC
		// (set) Token: 0x060001DD RID: 477 RVA: 0x000094E4 File Offset: 0x000084E4
		public Color BottomLineColor
		{
			get
			{
				return this._bottomLineColor;
			}
			set
			{
				if (value.Equals(this._bottomLineColor))
				{
					return;
				}
				this._bottomLineColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000950C File Offset: 0x0000850C
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00009514 File Offset: 0x00008514
		public Color DiagonalLineColor
		{
			get
			{
				return this._diagonalLineColor;
			}
			set
			{
				if (value.Equals(this._diagonalLineColor))
				{
					return;
				}
				this._diagonalLineColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000953C File Offset: 0x0000853C
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00009544 File Offset: 0x00008544
		public LineStyle DiagonalLineStyle
		{
			get
			{
				return this._diagonalLineStyle;
			}
			set
			{
				if (value == this._diagonalLineStyle)
				{
					return;
				}
				this._diagonalLineStyle = value;
				this.OnChange();
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000955D File Offset: 0x0000855D
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00009565 File Offset: 0x00008565
		public ushort Pattern
		{
			get
			{
				return this._pattern;
			}
			set
			{
				if (value == this._pattern)
				{
					return;
				}
				this._pattern = value;
				this.OnChange();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000957E File Offset: 0x0000857E
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00009586 File Offset: 0x00008586
		public Color PatternColor
		{
			get
			{
				return this._patternColor;
			}
			set
			{
				if (value.Equals(this._patternColor))
				{
					return;
				}
				this._patternColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000095AE File Offset: 0x000085AE
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000095B6 File Offset: 0x000085B6
		public Color PatternBackgroundColor
		{
			get
			{
				return this._patternBackgroundColor;
			}
			set
			{
				if (value.Equals(this._patternBackgroundColor))
				{
					return;
				}
				this._patternBackgroundColor = (Color)value.Clone();
				this.OnChange();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000095DE File Offset: 0x000085DE
		private void ReadBytes(Bytes bytes, Font font, string format)
		{
			this._font = font;
			this._format = format;
			this.ReadXF_3(bytes.Get(4, 2));
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000095FC File Offset: 0x000085FC
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(BitConverter.GetBytes(this._font.ID));
				bytes.Append(BitConverter.GetBytes(this._doc.Workbook.Formats.GetFinalID(this._format)));
				bytes.Append(this.XF_3());
				bytes.Append(this.XF_ALIGN());
				bytes.Append((byte)this._rotation);
				bytes.Append(this.XF_6());
				bytes.Append(this.XF_USED_ATTRIB());
				bytes.Append(this.XF_BORDER_LINES_BG());
				bytes.Append(this.XF_LINE_COLOUR_STYLE_FILL());
				bytes.Append(this.XF_PATTERN());
				return Record.GetBytes(RID.XF, bytes);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000096B8 File Offset: 0x000086B8
		private void ReadXF_3(Bytes bytes)
		{
			Bytes.Bits bits = bytes.GetBits();
			ushort num = bits.Get(4, 12).ToUInt16();
			if (num != 4095)
			{
				this.ReadStyleXfIndex = new ushort?(num);
			}
			this.ReadXF_TYPE_PROT(bits.Get(4));
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000096FC File Offset: 0x000086FC
		private byte[] XF_3()
		{
			ushort num;
			if (this._isStyleXF)
			{
				num = 4095;
			}
			else
			{
				num = 0;
			}
			ushort num2 = 0;
			num2 += this.XF_TYP_PROT();
			num2 += (ushort)(num * 16);
			return BitConverter.GetBytes(num2);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00009736 File Offset: 0x00008736
		private void ReadXF_TYPE_PROT(Bytes.Bits bits)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009738 File Offset: 0x00008738
		private ushort XF_TYP_PROT()
		{
			ushort num = 0;
			if (this._cellLocked)
			{
				num += 1;
			}
			if (this._formulaHidden)
			{
				num += 2;
			}
			if (this._isStyleXF)
			{
				num += 4;
			}
			return num;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00009770 File Offset: 0x00008770
		private byte XF_ALIGN()
		{
			byte b = 0;
			b += (byte)((byte)this._verticalAlignment * 16);
			if (this._textWrapRight)
			{
				b += 8;
			}
			return (byte)(b + this._horizontalAlignment);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000097A8 File Offset: 0x000087A8
		private byte XF_6()
		{
			ushort num = 0;
			num += (ushort)((byte)this._textDirection * 64);
			if (this._shrinkToCell)
			{
				num += 16;
			}
			num += this._indentLevel;
			return (byte)num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000097E0 File Offset: 0x000087E0
		private byte XF_USED_ATTRIB()
		{
			ushort num = 0;
			if (this._isStyleXF ? (!this._useNumber) : this._useNumber)
			{
				num += 1;
			}
			if (this._isStyleXF ? (!this._useFont) : this._useFont)
			{
				num += 2;
			}
			if (this._isStyleXF ? (!this._useMisc) : this._useMisc)
			{
				num += 4;
			}
			if (this._isStyleXF ? (!this._useBorder) : this._useBorder)
			{
				num += 8;
			}
			if (this._isStyleXF ? (!this._useBackground) : this._useBackground)
			{
				num += 16;
			}
			if (this._isStyleXF ? (!this._useProtection) : this._useProtection)
			{
				num += 32;
			}
			num *= 4;
			return (byte)num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000098B8 File Offset: 0x000088B8
		private byte[] XF_BORDER_LINES_BG()
		{
			uint num = 0U;
			num += (uint)this._leftLineStyle;
			num += (uint)(Math.Pow(2.0, 4.0) * (double)this._rightLineStyle);
			num += (uint)(Math.Pow(2.0, 8.0) * (double)this._topLineStyle);
			num += (uint)(Math.Pow(2.0, 12.0) * (double)this._bottomLineStyle);
			num += (uint)(Math.Pow(2.0, 16.0) * (double)this._doc.Workbook.Palette.GetIndex(this._leftLineColor));
			num += (uint)(Math.Pow(2.0, 23.0) * (double)this._doc.Workbook.Palette.GetIndex(this._rightLineColor));
			if (this._diagonalDescending)
			{
				num += (uint)Math.Pow(2.0, 30.0);
			}
			if (this._diagonalAscending)
			{
				num += (uint)Math.Pow(2.0, 31.0);
			}
			return BitConverter.GetBytes(num);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000099F8 File Offset: 0x000089F8
		private byte[] XF_LINE_COLOUR_STYLE_FILL()
		{
			uint num = 0U;
			num += (uint)this._doc.Workbook.Palette.GetIndex(this._topLineColor);
			num += (uint)(Math.Pow(2.0, 7.0) * (double)this._doc.Workbook.Palette.GetIndex(this._bottomLineColor));
			num += (uint)(Math.Pow(2.0, 14.0) * (double)this._doc.Workbook.Palette.GetIndex(this._diagonalLineColor));
			num += (uint)(Math.Pow(2.0, 21.0) * (double)this._diagonalLineStyle);
			num += (uint)(Math.Pow(2.0, 26.0) * (double)this._pattern);
			return BitConverter.GetBytes(num);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009AE4 File Offset: 0x00008AE4
		private byte[] XF_PATTERN()
		{
			ushort num = 0;
			num += this._doc.Workbook.Palette.GetIndex(this._patternColor);
			num += (ushort)(Math.Pow(2.0, 7.0) * (double)this._doc.Workbook.Palette.GetIndex(this._patternBackgroundColor));
			return BitConverter.GetBytes(num);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009B54 File Offset: 0x00008B54
		internal bool Equals(XF that)
		{
			return this._horizontalAlignment == that._horizontalAlignment && this._textWrapRight == that._textWrapRight && this._verticalAlignment == that._verticalAlignment && this._rotation == that._rotation && this._indentLevel == that._indentLevel && this._shrinkToCell == that._shrinkToCell && this._textDirection == that._textDirection && this._cellLocked == that._cellLocked && this._formulaHidden == that._formulaHidden && this._isStyleXF == that._isStyleXF && this._useNumber == that._useNumber && this._useFont == that._useFont && this._useMisc == that._useMisc && this._useBorder == that._useBorder && this._useBackground == that._useBackground && this._useProtection == that._useProtection && this._leftLineStyle == that._leftLineStyle && this._rightLineStyle == that._rightLineStyle && this._topLineStyle == that._topLineStyle && this._bottomLineStyle == that._bottomLineStyle && this._leftLineColor.Equals(that._leftLineColor) && this._rightLineColor.Equals(that._rightLineColor) && this._diagonalDescending == that._diagonalDescending && this._diagonalAscending == that._diagonalAscending && this._topLineColor.Equals(that._topLineColor) && this._bottomLineColor.Equals(that._bottomLineColor) && this._diagonalLineColor.Equals(that._diagonalLineColor) && this._diagonalLineStyle == that._diagonalLineStyle && this._pattern == that._pattern && this._patternColor.Equals(that._patternColor) && this._patternBackgroundColor.Equals(that._patternBackgroundColor) && this.Font.Equals(that.Font) && this.Format.Equals(that.Format) && this.Style.Equals(that.Style);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00009DB4 File Offset: 0x00008DB4
		internal ushort Id
		{
			get
			{
				if (this._id == null)
				{
					this._id = new ushort?(this._doc.Workbook.XFs.Add(this));
				}
				return this._id.Value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009DF0 File Offset: 0x00008DF0
		public object Clone()
		{
			XF xf = new XF(this._doc);
			xf.Font = (Font)this._font.Clone();
			xf.Format = (string)this._format.Clone();
			if (!this.IsStyleXF)
			{
				xf.Style = (Style)this._style.Clone();
			}
			xf.HorizontalAlignment = this.HorizontalAlignment;
			xf.TextWrapRight = this.TextWrapRight;
			xf.VerticalAlignment = this.VerticalAlignment;
			xf.Rotation = this.Rotation;
			xf.IndentLevel = this.IndentLevel;
			xf.ShrinkToCell = this.ShrinkToCell;
			xf.TextDirection = this.TextDirection;
			xf.CellLocked = this.CellLocked;
			xf.FormulaHidden = this.FormulaHidden;
			xf.IsStyleXF = this.IsStyleXF;
			xf.UseNumber = this.UseNumber;
			xf.UseFont = this.UseFont;
			xf.UseMisc = this.UseMisc;
			xf.UseBorder = this.UseBorder;
			xf.UseBackground = this.UseBackground;
			xf.UseProtection = this.UseProtection;
			xf.LeftLineStyle = this.LeftLineStyle;
			xf.RightLineStyle = this.RightLineStyle;
			xf.TopLineStyle = this.TopLineStyle;
			xf.BottomLineStyle = this.BottomLineStyle;
			xf.LeftLineColor = this.LeftLineColor;
			xf.RightLineColor = this.RightLineColor;
			xf.DiagonalDescending = this.DiagonalDescending;
			xf.DiagonalAscending = this.DiagonalAscending;
			xf.TopLineColor = this.TopLineColor;
			xf.BottomLineColor = this.BottomLineColor;
			xf.DiagonalLineColor = this.DiagonalLineColor;
			xf.DiagonalLineStyle = this.DiagonalLineStyle;
			xf.Pattern = this.Pattern;
			xf.PatternColor = this.PatternColor;
			xf.PatternBackgroundColor = this.PatternBackgroundColor;
			xf.Target = this.Target;
			return xf;
		}

		// Token: 0x040001F3 RID: 499
		private IXFTarget _targetObject;

		// Token: 0x040001F4 RID: 500
		private readonly XlsDocument _doc;

		// Token: 0x040001F5 RID: 501
		private ushort? _id;

		// Token: 0x040001F6 RID: 502
		internal ushort? ReadStyleXfIndex = null;

		// Token: 0x040001F7 RID: 503
		private Font _font;

		// Token: 0x040001F8 RID: 504
		private string _format = Formats.Default;

		// Token: 0x040001F9 RID: 505
		private Style _style;

		// Token: 0x040001FA RID: 506
		private HorizontalAlignments _horizontalAlignment;

		// Token: 0x040001FB RID: 507
		private bool _textWrapRight;

		// Token: 0x040001FC RID: 508
		private VerticalAlignments _verticalAlignment;

		// Token: 0x040001FD RID: 509
		private short _rotation;

		// Token: 0x040001FE RID: 510
		private ushort _indentLevel;

		// Token: 0x040001FF RID: 511
		private bool _shrinkToCell;

		// Token: 0x04000200 RID: 512
		private TextDirections _textDirection;

		// Token: 0x04000201 RID: 513
		private bool _cellLocked;

		// Token: 0x04000202 RID: 514
		private bool _formulaHidden;

		// Token: 0x04000203 RID: 515
		private bool _isStyleXF;

		// Token: 0x04000204 RID: 516
		private bool _useNumber;

		// Token: 0x04000205 RID: 517
		private bool _useFont;

		// Token: 0x04000206 RID: 518
		private bool _useMisc;

		// Token: 0x04000207 RID: 519
		private bool _useBorder;

		// Token: 0x04000208 RID: 520
		private bool _useBackground;

		// Token: 0x04000209 RID: 521
		private bool _useProtection;

		// Token: 0x0400020A RID: 522
		private ushort _leftLineStyle;

		// Token: 0x0400020B RID: 523
		private ushort _rightLineStyle;

		// Token: 0x0400020C RID: 524
		private ushort _topLineStyle;

		// Token: 0x0400020D RID: 525
		private ushort _bottomLineStyle;

		// Token: 0x0400020E RID: 526
		private Color _leftLineColor;

		// Token: 0x0400020F RID: 527
		private Color _rightLineColor;

		// Token: 0x04000210 RID: 528
		private bool _diagonalDescending;

		// Token: 0x04000211 RID: 529
		private bool _diagonalAscending;

		// Token: 0x04000212 RID: 530
		private Color _topLineColor;

		// Token: 0x04000213 RID: 531
		private Color _bottomLineColor;

		// Token: 0x04000214 RID: 532
		private Color _diagonalLineColor;

		// Token: 0x04000215 RID: 533
		private LineStyle _diagonalLineStyle;

		// Token: 0x04000216 RID: 534
		private ushort _pattern;

		// Token: 0x04000217 RID: 535
		private Color _patternColor;

		// Token: 0x04000218 RID: 536
		private Color _patternBackgroundColor;
	}
}
