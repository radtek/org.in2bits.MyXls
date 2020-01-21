using System;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x0200004C RID: 76
	public class Directory
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000CFB0 File Offset: 0x0000BFB0
		public Directory(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CFBF File Offset: 0x0000BFBF
		public int EntryCount
		{
			get
			{
				return this._doc.Streams.Count + 1;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000CFD3 File Offset: 0x0000BFD3
		public int SectorCount
		{
			get
			{
				return (int)Math.Ceiling((double)this.EntryCount / this.EntriesPerSector);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CFFA File Offset: 0x0000BFFA
		private int EntriesPerSector
		{
			get
			{
				return this._doc.BytesPerSector / 128;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D010 File Offset: 0x0000C010
		public int SID0
		{
			get
			{
				int num;
				if (this._doc.SSAT.SID0 != -2)
				{
					num = this._doc.SSAT.SID0 + this._doc.SSAT.SectorCount;
				}
				else
				{
					num = this._doc.SAT.SID0 + this._doc.SAT.SectorCount;
				}
				return num + this._doc.Streams.SectorCount;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000D08C File Offset: 0x0000C08C
		internal Bytes Bytes
		{
			get
			{
				int count = this._doc.Streams.Count;
				Bytes bytes = new Bytes();
				bytes.Append(this.StreamDirectoryBytes(this._doc.Streams.ShortSectorStorage));
				for (int i = 1; i <= count; i++)
				{
					bytes.Append(this.StreamDirectoryBytes(this._doc.Streams[i]));
				}
				int num = (count + 1) % this.EntriesPerSector;
				if (num > 0)
				{
					num = this.EntriesPerSector - num;
				}
				for (int j = 1; j <= num; j++)
				{
					bytes.Append(Directory.BlankEntryByteArray);
				}
				return bytes;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D130 File Offset: 0x0000C130
		private Bytes StreamDirectoryBytes(Stream stream)
		{
			Bytes bytes = new Bytes();
			bytes.Append(stream.Name);
			bytes.Append(new byte[64 - bytes.Length]);
			bytes.Append(BitConverter.GetBytes((ushort)stream.Name.Length));
			bytes.Append(Directory.HackDirectoryType(stream.Name));
			bytes.Append(1);
			bytes.Append(BitConverter.GetBytes(Directory.HackDirectoryDID(stream.Name, "LeftDID")));
			bytes.Append(BitConverter.GetBytes(Directory.HackDirectoryDID(stream.Name, "RightDID")));
			bytes.Append(BitConverter.GetBytes(Directory.HackDirectoryDID(stream.Name, "RootDID")));
			bytes.Append(new byte[16]);
			bytes.Append(new byte[4]);
			bytes.Append(new byte[8]);
			bytes.Append(new byte[8]);
			bytes.Append(BitConverter.GetBytes(stream.SID0));
			bytes.Append(BitConverter.GetBytes(stream.ByteCount));
			bytes.Append(new byte[4]);
			return bytes;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D240 File Offset: 0x0000C240
		private static Bytes HackDirectoryType(byte[] streamName)
		{
			if (Bytes.AreEqual(streamName, Directory.RootName))
			{
				return new Bytes(new byte[]
				{
					5
				});
			}
			if (Bytes.AreEqual(streamName, Directory.Biff8Workbook))
			{
				return new Bytes(new byte[]
				{
					2
				});
			}
			if (Bytes.AreEqual(streamName, Directory.StreamNameSumaryInformation))
			{
				return new Bytes(new byte[]
				{
					2
				});
			}
			if (Bytes.AreEqual(streamName, Directory.StreamNameDocumentSummaryInformation))
			{
				return new Bytes(new byte[]
				{
					2
				});
			}
			return new Bytes(new byte[]
			{
				byte.MaxValue
			});
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D320 File Offset: 0x0000C320
		private static int HackDirectoryDID(byte[] streamName, string didType)
		{
			if (Bytes.AreEqual(streamName, Directory.RootName))
			{
				if (didType != null)
				{
					if (didType == "LeftDID")
					{
						return -1;
					}
					if (didType == "RightDID")
					{
						return -1;
					}
					if (didType == "RootDID")
					{
						return 2;
					}
				}
			}
			else if (Bytes.AreEqual(streamName, Directory.Biff8Workbook))
			{
				if (didType != null)
				{
					if (didType == "LeftDID")
					{
						return -1;
					}
					if (didType == "RightDID")
					{
						return -1;
					}
					if (didType == "RootDID")
					{
						return -1;
					}
				}
			}
			else if (Bytes.AreEqual(streamName, Directory.StreamNameSumaryInformation))
			{
				if (didType != null)
				{
					if (didType == "LeftDID")
					{
						return 1;
					}
					if (didType == "RightDID")
					{
						return 3;
					}
					if (didType == "RootDID")
					{
						return -1;
					}
				}
			}
			else if (Bytes.AreEqual(streamName, new byte[]
			{
				5,
				0,
				68,
				0,
				111,
				0,
				99,
				0,
				117,
				0,
				109,
				0,
				101,
				0,
				110,
				0,
				116,
				0,
				83,
				0,
				117,
				0,
				109,
				0,
				109,
				0,
				97,
				0,
				114,
				0,
				121,
				0,
				73,
				0,
				110,
				0,
				102,
				0,
				111,
				0,
				114,
				0,
				109,
				0,
				97,
				0,
				116,
				0,
				105,
				0,
				111,
				0,
				110,
				0,
				0,
				0
			}))
			{
				if (didType != null)
				{
					if (didType == "LeftDID")
					{
						return -1;
					}
					if (didType == "RightDID")
					{
						return -1;
					}
					if (didType == "RootDID")
					{
						return -1;
					}
				}
			}
			else if (didType != null)
			{
				if (didType == "LeftDID")
				{
					return 1000000;
				}
				if (didType == "RightDID")
				{
					return 1000000;
				}
				if (didType == "RootDID")
				{
					return 1000000;
				}
			}
			throw new Exception(string.Format("Unexpected didType {0} for HackDirectoryDID", didType));
		}

		// Token: 0x0400029E RID: 670
		public static readonly byte[] RootName = new byte[]
		{
			82,
			0,
			111,
			0,
			111,
			0,
			116,
			0,
			32,
			0,
			69,
			0,
			110,
			0,
			116,
			0,
			114,
			0,
			121,
			0,
			0,
			0
		};

		// Token: 0x0400029F RID: 671
		public static readonly byte[] Biff8Workbook = new byte[]
		{
			87,
			0,
			111,
			0,
			114,
			0,
			107,
			0,
			98,
			0,
			111,
			0,
			111,
			0,
			107,
			0,
			0,
			0
		};

		// Token: 0x040002A0 RID: 672
		private static readonly byte[] StreamNameSumaryInformation = new byte[]
		{
			5,
			0,
			83,
			0,
			117,
			0,
			109,
			0,
			109,
			0,
			97,
			0,
			114,
			0,
			121,
			0,
			73,
			0,
			110,
			0,
			102,
			0,
			111,
			0,
			114,
			0,
			109,
			0,
			97,
			0,
			116,
			0,
			105,
			0,
			111,
			0,
			110,
			0,
			0,
			0
		};

		// Token: 0x040002A1 RID: 673
		private static readonly byte[] StreamNameDocumentSummaryInformation = new byte[]
		{
			5,
			0,
			68,
			0,
			111,
			0,
			99,
			0,
			117,
			0,
			109,
			0,
			101,
			0,
			110,
			0,
			116,
			0,
			83,
			0,
			117,
			0,
			109,
			0,
			109,
			0,
			97,
			0,
			114,
			0,
			121,
			0,
			73,
			0,
			110,
			0,
			102,
			0,
			111,
			0,
			114,
			0,
			109,
			0,
			97,
			0,
			116,
			0,
			105,
			0,
			111,
			0,
			110,
			0,
			0,
			0
		};

		// Token: 0x040002A2 RID: 674
		private readonly Ole2Document _doc;

		// Token: 0x040002A3 RID: 675
		private static readonly byte[] BlankEntryByteArray = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			254,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};
	}
}
