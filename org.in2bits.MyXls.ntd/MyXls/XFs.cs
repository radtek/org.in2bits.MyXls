using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000046 RID: 70
	public class XFs
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000C01D File Offset: 0x0000B01D
		internal XFs(XlsDocument doc, Workbook workbook)
		{
			this._doc = doc;
			this._workbook = workbook;
			this._xfs = new List<XF>();
			this.AddDefaultStyleXFs();
			this.AddDefaultUserXF();
		}

		// Token: 0x170000FB RID: 251
		internal XF this[int index]
		{
			get
			{
				return (XF)this._xfs[index].Clone();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C062 File Offset: 0x0000B062
		internal XF DefaultUserXF
		{
			get
			{
				return (XF)this._defaultUserXf.Clone();
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000C074 File Offset: 0x0000B074
		internal ushort Add(XF xf)
		{
			this._workbook.Fonts.Add(xf.Font);
			this._workbook.Formats.Add(xf.Format);
			this._workbook.Styles.Add(xf.Style);
			short num = this.GetId(xf);
			if (num == -1)
			{
				num = (short)this._xfs.Count;
				this._xfs.Add((XF)xf.Clone());
			}
			return (ushort)num;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C0F8 File Offset: 0x0000B0F8
		private void AddDefaultStyleXFs()
		{
			XF xf = new XF(this._doc);
			xf.IsStyleXF = true;
			xf.CellLocked = true;
			this._xfs.Add(xf);
			xf = (XF)xf.Clone();
			xf.UseBackground = false;
			xf.UseBorder = false;
			xf.UseFont = true;
			xf.UseMisc = false;
			xf.UseNumber = false;
			xf.UseProtection = false;
			for (int i = 0; i < 15; i++)
			{
				this._xfs.Add((XF)xf.Clone());
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C184 File Offset: 0x0000B184
		private void AddDefaultUserXF()
		{
			XF xf = new XF(this._doc);
			xf.CellLocked = true;
			this.Add(xf);
			this._defaultUserXf = xf;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C1B4 File Offset: 0x0000B1B4
		private short GetId(XF xf)
		{
			short num = 0;
			while ((int)num < this._xfs.Count)
			{
				if (this._xfs[(int)num].Equals(xf))
				{
					return num;
				}
				num += 1;
			}
			return -1;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C1F0 File Offset: 0x0000B1F0
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				for (int i = 0; i < this._xfs.Count; i++)
				{
					bytes.Append(this._xfs[i].Bytes);
				}
				return bytes;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000C231 File Offset: 0x0000B231
		public object Count
		{
			get
			{
				return this._xfs.Count;
			}
		}

		// Token: 0x0400027E RID: 638
		private readonly XlsDocument _doc;

		// Token: 0x0400027F RID: 639
		private readonly Workbook _workbook;

		// Token: 0x04000280 RID: 640
		private XF _defaultUserXf;

		// Token: 0x04000281 RID: 641
		private readonly List<XF> _xfs;

		// Token: 0x04000282 RID: 642
		internal SortedList<ushort, ushort> XfIdxLookups;
	}
}
