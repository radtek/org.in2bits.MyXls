using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000039 RID: 57
	public class Font : ICloneable
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x00009FD4 File Offset: 0x00008FD4
		internal Font(XlsDocument doc)
		{
			this._isInitializing = true;
			this._doc = doc;
			this._id = null;
			this.SetDefaults();
			this._isInitializing = false;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A003 File Offset: 0x00009003
		internal Font(XlsDocument doc, XF xf) : this(doc)
		{
			this._target = xf;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000A013 File Offset: 0x00009013
		internal Font(XlsDocument doc, Bytes bytes) : this(doc)
		{
			this.ReadBytes(bytes);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A024 File Offset: 0x00009024
		public bool Equals(Font that)
		{
			return this._height == that._height && this._italic == that._italic && this._underlined == that._underlined && this._struckOut == that._struckOut && this._colorIndex == that._colorIndex && this._weight == that._weight && this._escapement == that._escapement && this._underline == that._underline && this._fontFamily == that._fontFamily && this._characterSet == that._characterSet && string.Compare(this._fontName, that._fontName, false) == 0;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A0E8 File Offset: 0x000090E8
		private void SetDefaults()
		{
			this._height = 200;
			this._italic = false;
			this._underlined = false;
			this._struckOut = false;
			this._colorIndex = 32767;
			this._weight = FontWeight.Normal;
			this._escapement = EscapementTypes.Default;
			this._underline = UnderlineTypes.Default;
			this._fontFamily = FontFamilies.Default;
			this._characterSet = CharacterSets.Default;
			this._notUsed = 0;
			this._fontName = "Arial";
			this.OnChange();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A15F File Offset: 0x0000915F
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000A19A File Offset: 0x0000919A
		internal ushort ID
		{
			get
			{
				if (this._id == null)
				{
					this._id = new ushort?(this._doc.Workbook.Fonts.Add(this));
				}
				return this._id.Value;
			}
			set
			{
				this._id = new ushort?(value);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000A1A8 File Offset: 0x000091A8
		private void OnChange()
		{
			if (this._isInitializing)
			{
				return;
			}
			this._id = null;
			this._id = new ushort?(this.ID);
			this._target.OnFontChange(this);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A1DC File Offset: 0x000091DC
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000A1E4 File Offset: 0x000091E4
		public ushort Height
		{
			get
			{
				return this._height;
			}
			set
			{
				if (value == this._height)
				{
					return;
				}
				this._height = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000A1FD File Offset: 0x000091FD
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000A205 File Offset: 0x00009205
		public bool Italic
		{
			get
			{
				return this._italic;
			}
			set
			{
				if (value == this._italic)
				{
					return;
				}
				this._italic = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000A21E File Offset: 0x0000921E
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000A226 File Offset: 0x00009226
		public bool StruckOut
		{
			get
			{
				return this._struckOut;
			}
			set
			{
				if (value == this._struckOut)
				{
					return;
				}
				this._struckOut = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000A23F File Offset: 0x0000923F
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000A247 File Offset: 0x00009247
		public ushort ColorIndex
		{
			get
			{
				return this._colorIndex;
			}
			set
			{
				if (value == this._colorIndex)
				{
					return;
				}
				this._colorIndex = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000A260 File Offset: 0x00009260
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000A272 File Offset: 0x00009272
		public bool Bold
		{
			get
			{
				return this._weight >= FontWeight.Bold;
			}
			set
			{
				this.Weight = (value ? FontWeight.Bold : FontWeight.Normal);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000A289 File Offset: 0x00009289
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000A291 File Offset: 0x00009291
		public FontWeight Weight
		{
			get
			{
				return this._weight;
			}
			set
			{
				if (value != this._weight)
				{
					this._weight = value;
					this.OnChange();
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A2A9 File Offset: 0x000092A9
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A2B1 File Offset: 0x000092B1
		public EscapementTypes Escapement
		{
			get
			{
				return this._escapement;
			}
			set
			{
				if (value == this._escapement)
				{
					return;
				}
				this._escapement = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A2CA File Offset: 0x000092CA
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000A2D2 File Offset: 0x000092D2
		public UnderlineTypes Underline
		{
			get
			{
				return this._underline;
			}
			set
			{
				if (value == this._underline)
				{
					return;
				}
				this._underline = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000A2EB File Offset: 0x000092EB
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000A2F3 File Offset: 0x000092F3
		public FontFamilies FontFamily
		{
			get
			{
				return this._fontFamily;
			}
			set
			{
				if (value == this._fontFamily)
				{
					return;
				}
				this._fontFamily = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000A30C File Offset: 0x0000930C
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000A314 File Offset: 0x00009314
		public CharacterSets CharacterSet
		{
			get
			{
				return this._characterSet;
			}
			set
			{
				if (value == this._characterSet)
				{
					return;
				}
				this._characterSet = value;
				this.OnChange();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000A32D File Offset: 0x0000932D
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000A338 File Offset: 0x00009338
		public string FontName
		{
			get
			{
				return this._fontName;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value.Length > 255)
				{
					value = value.Substring(0, 255);
				}
				if (string.Compare(value, this._fontName, true) == 0)
				{
					return;
				}
				this._fontName = value;
				this.OnChange();
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000A387 File Offset: 0x00009387
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000A38F File Offset: 0x0000938F
		internal XF Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A398 File Offset: 0x00009398
		private void ReadBytes(Bytes bytes)
		{
			byte[] byteArray = bytes.ByteArray;
			this._height = BitConverter.ToUInt16(byteArray, 0);
			this.SetOptionsValue(bytes.Get(2, 2));
			this._colorIndex = BitConverter.ToUInt16(byteArray, 4);
			this._weight = FontWeightConverter.Convert(BitConverter.ToUInt16(byteArray, 6));
			this._escapement = (EscapementTypes)BitConverter.ToUInt16(byteArray, 8);
			this._underline = (UnderlineTypes)byteArray[10];
			this._fontFamily = (FontFamilies)byteArray[11];
			this._characterSet = (CharacterSets)byteArray[12];
			this._fontName = UnicodeBytes.Read(bytes.Get(14, bytes.Length - 14), 8);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A430 File Offset: 0x00009430
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(BitConverter.GetBytes(this._height));
				bytes.Append(BitConverter.GetBytes(this.OptionsValue()));
				bytes.Append(BitConverter.GetBytes(this._colorIndex));
				bytes.Append(BitConverter.GetBytes((ushort)this._weight));
				bytes.Append(BitConverter.GetBytes((ushort)this._escapement));
				bytes.Append((byte)this._underline);
				bytes.Append((byte)this._fontFamily);
				bytes.Append((byte)this._characterSet);
				bytes.Append(this._notUsed);
				bytes.Append(XlsDocument.GetUnicodeString(this._fontName, 8));
				return Record.GetBytes(RID.FONT, bytes);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A4E8 File Offset: 0x000094E8
		private void SetOptionsValue(Bytes bytes)
		{
			ushort num = BitConverter.ToUInt16(bytes.ByteArray, 0);
			if (num >= 8)
			{
				this._struckOut = true;
				num -= 8;
			}
			else
			{
				this._struckOut = false;
			}
			if (num >= 4)
			{
				this._underlined = true;
				num -= 4;
			}
			else
			{
				this._underlined = false;
			}
			if (num >= 2)
			{
				this._italic = true;
				return;
			}
			this._italic = false;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000A548 File Offset: 0x00009548
		private ushort OptionsValue()
		{
			ushort num = 0;
			if (this.Bold)
			{
				num += 1;
			}
			if (this._italic)
			{
				num += 2;
			}
			if (this._underlined)
			{
				num += 4;
			}
			if (this._struckOut)
			{
				num += 8;
			}
			return num;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A58C File Offset: 0x0000958C
		public object Clone()
		{
			return new Font(this._doc, this._target)
			{
				_height = this._height,
				_italic = this._italic,
				_struckOut = this._struckOut,
				_colorIndex = this._colorIndex,
				_weight = this._weight,
				_escapement = this._escapement,
				_underline = this._underline,
				_fontFamily = this._fontFamily,
				_characterSet = this._characterSet,
				_fontName = this._fontName
			};
		}

		// Token: 0x04000219 RID: 537
		private readonly XlsDocument _doc;

		// Token: 0x0400021A RID: 538
		private XF _target;

		// Token: 0x0400021B RID: 539
		private ushort? _id;

		// Token: 0x0400021C RID: 540
		private bool _isInitializing;

		// Token: 0x0400021D RID: 541
		private ushort _height;

		// Token: 0x0400021E RID: 542
		private bool _italic;

		// Token: 0x0400021F RID: 543
		private bool _underlined;

		// Token: 0x04000220 RID: 544
		private bool _struckOut;

		// Token: 0x04000221 RID: 545
		private ushort _colorIndex;

		// Token: 0x04000222 RID: 546
		private FontWeight _weight;

		// Token: 0x04000223 RID: 547
		private EscapementTypes _escapement;

		// Token: 0x04000224 RID: 548
		private UnderlineTypes _underline;

		// Token: 0x04000225 RID: 549
		private FontFamilies _fontFamily;

		// Token: 0x04000226 RID: 550
		private CharacterSets _characterSet;

		// Token: 0x04000227 RID: 551
		private byte _notUsed;

		// Token: 0x04000228 RID: 552
		private string _fontName;
	}
}
