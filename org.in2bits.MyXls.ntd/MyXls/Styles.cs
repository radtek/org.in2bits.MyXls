using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000041 RID: 65
	public class Styles
	{
		// Token: 0x0600024D RID: 589 RVA: 0x0000AB51 File Offset: 0x00009B51
		public Styles(XlsDocument doc)
		{
			this._doc = doc;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000AB60 File Offset: 0x00009B60
		public int Count
		{
			get
			{
				return this._styles.Count;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AB70 File Offset: 0x00009B70
		public ushort Add(Style style)
		{
			ushort? id = this.GetID(style);
			if (id == null)
			{
				if (this._styles == null)
				{
					this._styles = new List<Style>();
				}
				id = new ushort?((ushort)this._styles.Count);
				this._styles.Add((Style)style.Clone());
			}
			return id.Value;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000ABD4 File Offset: 0x00009BD4
		public bool IsWritten(Style style)
		{
			return this.GetID(style) != null;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000ABF0 File Offset: 0x00009BF0
		public ushort? GetID(Style style)
		{
			ushort? result = null;
			if (this._styles == null)
			{
				return result;
			}
			ushort num = 0;
			while ((int)num < this._styles.Count)
			{
				Style style2 = this._styles[(int)num];
				if (style2.Equals(style))
				{
					result = new ushort?(num);
					break;
				}
				num += 1;
			}
			return result;
		}

		// Token: 0x170000E3 RID: 227
		public Style this[int index]
		{
			get
			{
				return (Style)this._styles[index].Clone();
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000AC60 File Offset: 0x00009C60
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				foreach (Style style in this._styles)
				{
					bytes.Append(style.Bytes);
				}
				return bytes;
			}
		}

		// Token: 0x0400024E RID: 590
		private readonly XlsDocument _doc;

		// Token: 0x0400024F RID: 591
		private List<Style> _styles;
	}
}
