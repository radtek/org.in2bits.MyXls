using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x02000004 RID: 4
	public class Msat
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020AD File Offset: 0x000010AD
		public Msat(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BC File Offset: 0x000010BC
		public int SectorCount
		{
			get
			{
				int num = this._doc.SAT.SectorCount;
				if (num <= 109)
				{
					return 0;
				}
				num -= 109;
				if (num % 127m == 0m)
				{
					return (int)Math.Floor(num / 127m);
				}
				return (int)Math.Floor(num / 127m) + 1;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002140 File Offset: 0x00001140
		public int SID0
		{
			get
			{
				if (this.SectorCount == 0)
				{
					return -2;
				}
				return 0;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000214E File Offset: 0x0000114E
		internal Bytes Head
		{
			get
			{
				return this.SectorBinData(0);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00001158
		private Bytes SectorBinData(int sectorIndex)
		{
			if (0 > sectorIndex || sectorIndex > this.SectorCount)
			{
				throw new ArgumentOutOfRangeException(string.Format("sectorIndex must be >= 0 and <= SectorCount {0}", this.SectorCount));
			}
			Bytes bytes = new Bytes();
			int sectorCount = this._doc.SAT.SectorCount;
			int sid = this._doc.SAT.SID0;
			int num;
			int num2;
			if (sectorIndex == 0)
			{
				num = 1;
				num2 = 109;
			}
			else
			{
				num = 110 + (sectorIndex - 1) * 127;
				num2 = num + 126;
			}
			for (int i = num; i <= num2; i++)
			{
				if (i < sectorCount + 1)
				{
					bytes.Append(BitConverter.GetBytes(sid + (i - 1)));
				}
				else
				{
					bytes.Append(BitConverter.GetBytes(-1));
				}
			}
			if (sectorIndex > 0)
			{
				if (num2 >= sectorCount)
				{
					bytes.Append(BitConverter.GetBytes(-2));
				}
				else
				{
					bytes.Append(BitConverter.GetBytes(this.SID0 + sectorIndex));
				}
			}
			return bytes;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002238 File Offset: 0x00001238
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				int sectorCount = this.SectorCount;
				for (int i = 1; i <= sectorCount; i++)
				{
					bytes.Append(this.SectorBinData(i));
				}
				return bytes;
			}
		}

		// Token: 0x04000006 RID: 6
		private readonly Ole2Document _doc;
	}
}
