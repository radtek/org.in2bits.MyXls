using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x02000005 RID: 5
	public class Header
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000226C File Offset: 0x0000126C
		public Header(Ole2Document doc)
		{
			this._doc = doc;
			this.SetDefaults();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002281 File Offset: 0x00001281
		private void SetDefaults()
		{
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002284 File Offset: 0x00001284
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(this._doc.DocFileID);
				bytes.Append(this._doc.DocUID);
				bytes.Append(this._doc.FileFormatRevision);
				bytes.Append(this._doc.FileFormatVersion);
				bytes.Append(this._doc.IsLittleEndian ? Header.LITTLE_ENDIAN : Header.BIG_ENDIAN);
				bytes.Append(BitConverter.GetBytes(this._doc.SectorSize));
				bytes.Append(BitConverter.GetBytes(this._doc.ShortSectorSize));
				bytes.Append(this._doc.Blank1);
				bytes.Append(BitConverter.GetBytes(this._doc.SAT.SectorCount));
				bytes.Append(BitConverter.GetBytes(this._doc.Directory.SID0));
				bytes.Append(this._doc.Blank2);
				bytes.Append(BitConverter.GetBytes(this._doc.StandardStreamMinBytes));
				bytes.Append(BitConverter.GetBytes(this._doc.SSAT.SID0));
				bytes.Append(BitConverter.GetBytes(this._doc.SSAT.SectorCount));
				bytes.Append(BitConverter.GetBytes(this._doc.MSAT.SID0));
				bytes.Append(BitConverter.GetBytes(this._doc.MSAT.SectorCount));
				bytes.Append(this._doc.MSAT.Head);
				return bytes;
			}
		}

		// Token: 0x04000007 RID: 7
		private static readonly byte[] LITTLE_ENDIAN = new byte[]
		{
			254,
			byte.MaxValue
		};

		// Token: 0x04000008 RID: 8
		private static readonly byte[] BIG_ENDIAN = new byte[]
		{
			byte.MaxValue,
			254
		};

		// Token: 0x04000009 RID: 9
		private readonly Ole2Document _doc;
	}
}
