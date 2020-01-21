using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000013 RID: 19
	public class Style : ICloneable
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000034E1 File Offset: 0x000024E1
		internal Style(XlsDocument doc, XF xf)
		{
			this._isInitializing = true;
			this._doc = doc;
			this._xf = xf;
			this._isInitializing = false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003511 File Offset: 0x00002511
		private void OnChange()
		{
			if (this._isInitializing)
			{
				return;
			}
			this._id = null;
			this._xf.OnChange();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003533 File Offset: 0x00002533
		public ushort ID
		{
			get
			{
				if (this._id == null)
				{
					this._id = new ushort?(this._doc.Workbook.Styles.Add(this));
				}
				return this._id.Value;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000356E File Offset: 0x0000256E
		public bool Equals(Style that)
		{
			return true;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003571 File Offset: 0x00002571
		internal Bytes Bytes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003578 File Offset: 0x00002578
		public object Clone()
		{
			return new Style(this._doc, this._xf)
			{
				_doc = this._doc
			};
		}

		// Token: 0x0400008E RID: 142
		private XlsDocument _doc;

		// Token: 0x0400008F RID: 143
		private XF _xf;

		// Token: 0x04000090 RID: 144
		private bool _isInitializing;

		// Token: 0x04000091 RID: 145
		private ushort? _id = null;
	}
}
