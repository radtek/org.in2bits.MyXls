using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000034 RID: 52
	public class Fonts
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00008682 File Offset: 0x00007682
		public Fonts(XlsDocument doc)
		{
			this._doc = doc;
			this._fonts = new List<Font>();
			this.AddDefaultFonts();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000086A4 File Offset: 0x000076A4
		private void AddDefaultFonts()
		{
			Font font = new Font(this._doc, default(XF));
			this._fonts.Add(font);
			this._fonts.Add((Font)font.Clone());
			this._fonts.Add((Font)font.Clone());
			this._fonts.Add((Font)font.Clone());
			this._fonts.Add((Font)font.Clone());
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008724 File Offset: 0x00007724
		public ushort Add(Font font)
		{
			ushort? id = this.GetId(font);
			if (id == null)
			{
				id = new ushort?((ushort)this._fonts.Count);
				this._fonts.Add((Font)font.Clone());
			}
			return id.Value;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008774 File Offset: 0x00007774
		public ushort? GetId(Font font)
		{
			for (ushort num = 0; num < (ushort)this._fonts.Count; num += 1)
			{
				if (this._fonts[(int)num].Equals(font))
				{
					return new ushort?(num);
				}
			}
			return null;
		}

		// Token: 0x1700008F RID: 143
		internal Font this[ushort idx]
		{
			get
			{
				return (Font)this._fonts[(int)idx].Clone();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000087D8 File Offset: 0x000077D8
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				int num = -1;
				foreach (Font font in this._fonts)
				{
					num++;
					if (num != 4)
					{
						bytes.Append(font.Bytes);
					}
				}
				return bytes;
			}
		}

		// Token: 0x040001EA RID: 490
		private readonly XlsDocument _doc;

		// Token: 0x040001EB RID: 491
		private readonly List<Font> _fonts;
	}
}
