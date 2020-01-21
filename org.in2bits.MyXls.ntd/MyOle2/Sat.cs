using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x02000036 RID: 54
	public class Sat
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00008B29 File Offset: 0x00007B29
		public Sat(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00008B38 File Offset: 0x00007B38
		public int SectorCount
		{
			get
			{
				int num = this._doc.SSAT.SectorCount + this._doc.Streams.ShortSectorStorage.SectorCount + this._doc.Streams.SectorCount + this._doc.Directory.SectorCount;
				int num2 = this._doc.BytesPerSector / 4;
				return (int)Math.Ceiling(((double)num + Math.Ceiling((double)num / (double)num2)) / (double)num2);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00008BB4 File Offset: 0x00007BB4
		public int SID0
		{
			get
			{
				if (this._doc.MSAT.SID0 == -2)
				{
					return 0;
				}
				return this._doc.MSAT.SectorCount;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008BDC File Offset: 0x00007BDC
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				int num = 0;
				int sectorCount = this._doc.MSAT.SectorCount;
				int num2 = num + (sectorCount - 1);
				int i;
				for (i = num; i <= num2; i++)
				{
					bytes.Append(BitConverter.GetBytes(-4));
				}
				num = i;
				sectorCount = this._doc.SAT.SectorCount;
				num2 = num + (sectorCount - 1);
				for (i = num; i <= num2; i++)
				{
					bytes.Append(BitConverter.GetBytes(-3));
				}
				num = i;
				sectorCount = this._doc.SSAT.SectorCount;
				num2 = num + (sectorCount - 1);
				for (i = num; i <= num2; i++)
				{
					if (i < num2)
					{
						bytes.Append(BitConverter.GetBytes(i + 1));
					}
					else
					{
						bytes.Append(BitConverter.GetBytes(-2));
					}
				}
				num = i;
				sectorCount = this._doc.Streams.ShortSectorStorage.SectorCount;
				num2 = num + (sectorCount - 1);
				for (i = num; i <= num2; i++)
				{
					if (i < num2)
					{
						bytes.Append(BitConverter.GetBytes(i + 1));
					}
					else
					{
						bytes.Append(BitConverter.GetBytes(-2));
					}
				}
				num = i;
				int count = this._doc.Streams.Count;
				for (int j = 1; j <= count; j++)
				{
					sectorCount = this._doc.Streams[j].SectorCount;
					num2 = num + (sectorCount - 1);
					for (i = num; i <= num2; i++)
					{
						if (i < num2)
						{
							bytes.Append(BitConverter.GetBytes(i + 1));
						}
						else
						{
							bytes.Append(BitConverter.GetBytes(-2));
						}
					}
					num = i;
				}
				sectorCount = this._doc.Directory.SectorCount;
				num2 = num + (sectorCount - 1);
				for (i = num; i <= num2; i++)
				{
					if (i < num2)
					{
						bytes.Append(BitConverter.GetBytes(i + 1));
					}
					else
					{
						bytes.Append(BitConverter.GetBytes(-2));
					}
				}
				num = i;
				int num3 = (int)(num % (this._doc.BytesPerSector / 4m));
				num2 = this._doc.BytesPerSector / 4 - num3;
				for (i = 1; i <= num2; i++)
				{
					bytes.Append(BitConverter.GetBytes(-1));
				}
				return bytes;
			}
		}

		// Token: 0x040001EE RID: 494
		private readonly Ole2Document _doc;
	}
}
