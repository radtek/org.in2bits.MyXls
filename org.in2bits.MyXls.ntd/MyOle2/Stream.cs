using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x0200004B RID: 75
	public class Stream
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000CCFE File Offset: 0x0000BCFE
		public Stream(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000CD24 File Offset: 0x0000BD24
		public bool IsShort
		{
			get
			{
				return this.Name != Directory.RootName && (long)this._bytes.Length < (long)((ulong)this._doc.StandardStreamMinBytes);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000CD52 File Offset: 0x0000BD52
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000CD5A File Offset: 0x0000BD5A
		public Bytes Bytes
		{
			get
			{
				return this._bytes;
			}
			set
			{
				this._bytes = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000CD63 File Offset: 0x0000BD63
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000CD6B File Offset: 0x0000BD6B
		public byte[] Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this._name = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000CD80 File Offset: 0x0000BD80
		public int ByteCount
		{
			get
			{
				int num = 0;
				if (Bytes.AreEqual(this.Name, Directory.RootName))
				{
					int bytesPerShortSector = this._doc.BytesPerShortSector;
					int count = this._doc.Streams.Count;
					for (int i = 1; i <= count; i++)
					{
						num += this._doc.Streams[i].ShortSectorCount * bytesPerShortSector;
					}
				}
				else
				{
					num = this._bytes.Length;
				}
				return num;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000CDF9 File Offset: 0x0000BDF9
		public int SectorCount
		{
			get
			{
				if (!this.IsShort)
				{
					return this.GetSectorCount();
				}
				return 0;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000CE0B File Offset: 0x0000BE0B
		public int ShortSectorCount
		{
			get
			{
				if (this.IsShort)
				{
					return this.GetSectorCount();
				}
				return 0;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CE20 File Offset: 0x0000BE20
		private int GetSectorCount()
		{
			int value;
			if (this.IsShort)
			{
				value = this._doc.BytesPerShortSector;
			}
			else
			{
				value = this._doc.BytesPerSector;
			}
			decimal d = this.ByteCount / value;
			return (int)Math.Ceiling(d);
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CE74 File Offset: 0x0000BE74
		public int SID0
		{
			get
			{
				if (Bytes.AreEqual(this.Name, Directory.RootName))
				{
					return this._doc.SSAT.SID0 + this._doc.SSAT.SectorCount;
				}
				int num = this._doc.Streams.GetIndex(this.Name) - 1;
				int num2;
				if (this.IsShort)
				{
					num2 = 0;
					for (int i = 1; i <= num; i++)
					{
						Stream stream = this._doc.Streams[i];
						if (stream.IsShort)
						{
							num2 += stream.ShortSectorCount;
						}
					}
				}
				else
				{
					num2 = this._doc.SSAT.SID0;
					if (num2 == -2)
					{
						num2 = this._doc.SAT.SID0 + this._doc.SAT.SectorCount;
					}
					else
					{
						num2 += this._doc.SSAT.SectorCount;
					}
					num2 += this._doc.Streams.ShortSectorStorage.SectorCount;
					for (int j = 1; j <= num; j++)
					{
						Stream stream = this._doc.Streams[j];
						if (!stream.IsShort)
						{
							num2 += stream.SectorCount;
						}
					}
				}
				return num2;
			}
		}

		// Token: 0x0400029B RID: 667
		private readonly Ole2Document _doc;

		// Token: 0x0400029C RID: 668
		private byte[] _name = new byte[0];

		// Token: 0x0400029D RID: 669
		private Bytes _bytes = new Bytes();
	}
}
