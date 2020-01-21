using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x02000015 RID: 21
	public class Ssat
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003862 File Offset: 0x00002862
		public Ssat(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003874 File Offset: 0x00002874
		public int SectorCount
		{
			get
			{
				int num = 0;
				int count = this._doc.Streams.Count;
				for (int i = 1; i <= count; i++)
				{
					num += this._doc.Streams[i].ShortSectorCount;
				}
				return (int)Math.Ceiling(num / (this._doc.BytesPerSector / 4m));
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000038F0 File Offset: 0x000028F0
		public int SID0
		{
			get
			{
				if (this.SectorCount > 0)
				{
					return this._doc.SAT.SID0 + this._doc.SAT.SectorCount;
				}
				return -2;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003920 File Offset: 0x00002920
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				int num = 0;
				int num2 = this._doc.Streams.Count;
				for (int i = 1; i <= num2; i++)
				{
					Stream stream = this._doc.Streams[i];
					int shortSectorCount = stream.ShortSectorCount;
					num += shortSectorCount;
					int sid = stream.SID0;
					for (int j = 1; j <= shortSectorCount; j++)
					{
						if (j < shortSectorCount)
						{
							bytes.Append(BitConverter.GetBytes(sid + j));
						}
						else
						{
							bytes.Append(BitConverter.GetBytes(-2));
						}
					}
				}
				if (num > 0)
				{
					num2 = this._doc.BytesPerSector / 4 - num % (this._doc.BytesPerSector / 4);
					for (int k = 1; k <= num2; k++)
					{
						bytes.Append(BitConverter.GetBytes(-1));
					}
				}
				return bytes;
			}
		}

		// Token: 0x04000096 RID: 150
		private readonly Ole2Document _doc;
	}
}
