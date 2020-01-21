namespace org.in2bits.MyOle2
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using org.in2bits.MyXls.ByteUtil;

	// Token: 0x02000043 RID: 67
	public class Ole2Document
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000B0C8 File Offset: 0x0000A0C8
		public Ole2Document()
		{
			this.SetDefaults();
			this._header = new Header(this);
			this._msat = new Msat(this);
			this._sat = new Sat(this);
			this._ssat = new Ssat(this);
			this._directory = new Directory(this);
			this._streams = new Streams(this);
			this._streams.AddNamed(new Bytes(), Directory.RootName);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B13F File Offset: 0x0000A13F
		public Header Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000B147 File Offset: 0x0000A147
		public Msat MSAT
		{
			get
			{
				return this._msat;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B14F File Offset: 0x0000A14F
		public Directory Directory
		{
			get
			{
				return this._directory;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B157 File Offset: 0x0000A157
		public Streams Streams
		{
			get
			{
				return this._streams;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000B15F File Offset: 0x0000A15F
		public Ssat SSAT
		{
			get
			{
				return this._ssat;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B167 File Offset: 0x0000A167
		public Sat SAT
		{
			get
			{
				return this._sat;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B16F File Offset: 0x0000A16F
		public int BytesPerShortSector
		{
			get
			{
				return this._bytesPerShortSector;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B177 File Offset: 0x0000A177
		public int BytesPerSector
		{
			get
			{
				return this._bytesPerSector;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B17F File Offset: 0x0000A17F
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000B187 File Offset: 0x0000A187
		public uint StandardStreamMinBytes
		{
			get
			{
				return this._standardStreamMinBytes;
			}
			set
			{
				if (value <= 2U)
				{
					throw new ArgumentOutOfRangeException("value", "must be > 2");
				}
				this._standardStreamMinBytes = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000B1A4 File Offset: 0x0000A1A4
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000B1AC File Offset: 0x0000A1AC
		public byte[] DocFileID
		{
			get
			{
				return this._docFileID;
			}
			set
			{
				if (value.Length != 8)
				{
					throw new ArgumentOutOfRangeException("value", "must be 8 bytes in length");
				}
				this._docFileID = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000B1CB File Offset: 0x0000A1CB
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000B1D3 File Offset: 0x0000A1D3
		public byte[] DocUID
		{
			get
			{
				return this._docUID;
			}
			set
			{
				if (value.Length != 16)
				{
					throw new ArgumentOutOfRangeException("value", "must be 16 bytes in length");
				}
				this._docUID = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000B1F3 File Offset: 0x0000A1F3
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000B1FB File Offset: 0x0000A1FB
		public byte[] FileFormatRevision
		{
			get
			{
				return this._fileFormatRevision;
			}
			set
			{
				if (value.Length != 2)
				{
					throw new ArgumentOutOfRangeException("value", "must be 2 bytes in length");
				}
				this._fileFormatRevision = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000B21A File Offset: 0x0000A21A
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000B222 File Offset: 0x0000A222
		public byte[] FileFormatVersion
		{
			get
			{
				return this._fileFormatVersion;
			}
			set
			{
				if (value.Length != 2)
				{
					throw new ArgumentOutOfRangeException("value", "must be 2 bytes in length");
				}
				this._fileFormatVersion = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000B241 File Offset: 0x0000A241
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000B249 File Offset: 0x0000A249
		public bool IsLittleEndian
		{
			get
			{
				return this._isLittleEndian;
			}
			set
			{
				if (!value)
				{
					throw new NotSupportedException("Big Endian not currently supported");
				}
				this._isLittleEndian = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B260 File Offset: 0x0000A260
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000B268 File Offset: 0x0000A268
		public ushort SectorSize
		{
			get
			{
				return this._sectorSize;
			}
			set
			{
				if (value < 7)
				{
					throw new ArgumentOutOfRangeException("value", "must be >= 7");
				}
				this._sectorSize = value;
				this._bytesPerSector = (int)Math.Pow(2.0, (double)value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B29C File Offset: 0x0000A29C
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000B2A4 File Offset: 0x0000A2A4
		public ushort ShortSectorSize
		{
			get
			{
				return this._shortSectorSize;
			}
			set
			{
				if (value > this.SectorSize)
				{
					throw new ArgumentOutOfRangeException(string.Format("value must be <= SectorSize {0}", this.SectorSize));
				}
				this._shortSectorSize = value;
				this._bytesPerShortSector = (int)Math.Pow(2.0, (double)this.ShortSectorSize);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B300 File Offset: 0x0000A300
		private void SetDefaults()
		{
			this.DocFileID = new byte[]
			{
				208,
				207,
				17,
				224,
				161,
				177,
				26,
				225
			};
			this.DocUID = new byte[16];
			byte[] array = new byte[2];
			array[0] = 62;
			this.FileFormatRevision = array;
			byte[] array2 = new byte[2];
			array2[0] = 3;
			this.FileFormatVersion = array2;
			this.IsLittleEndian = true;
			this.SectorSize = 9;
			this.ShortSectorSize = 6;
			this._blank1 = new byte[10];
			this._blank2 = new byte[4];
			this.StandardStreamMinBytes = 4096U;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B390 File Offset: 0x0000A390
		public Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(this.Header.Bytes);
				bytes.Append(this.MSAT.Bytes);
				bytes.Append(this.SAT.Bytes);
				bytes.Append(this.SSAT.Bytes);
				bytes.Append(this.Streams.Bytes);
				bytes.Append(this.Directory.Bytes);
				return bytes;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B40C File Offset: 0x0000A40C
		public void Load(System.IO.Stream stream)
		{
			if (stream.Length == 0L)
			{
				throw new Exception("No data (or zero-length) found!");
			}
			if (stream.Length < 512L)
			{
				throw new Exception(string.Format("File length {0} < 512 bytes", stream.Length));
			}
			byte[] array = new byte[512];
			stream.Read(array, 0, 512);
			bool flag = false;
			if (array[28] == 254 && array[29] == 255)
			{
				flag = true;
			}
			if (!flag)
			{
				throw new NotSupportedException("File is not Little-Endian");
			}
			this._isLittleEndian = flag;
			ushort num = BitConverter.ToUInt16(Ole2Document.MidByteArray(array, 30, 2), 0);
			if (num < 7 || num > 32)
			{
				throw new Exception(string.Format("Invalid Sector Size [{0}] (should be 7 <= sectorSize <= 32", num));
			}
			this._sectorSize = num;
			ushort num2 = BitConverter.ToUInt16(Ole2Document.MidByteArray(array, 32, 2), 0);
			if (num2 > num)
			{
				throw new Exception(string.Format("Invalid Short Sector Size [{0}] (should be < sectorSize; {1})", num2, num));
			}
			this._shortSectorSize = num2;
			uint num3 = BitConverter.ToUInt32(Ole2Document.MidByteArray(array, 44, 4), 0);
			if (num3 < 0U)
			{
				throw new Exception(string.Format("Invalid SAT Sector Count [{0}] (should be > 0)", num3));
			}
			int num4 = BitConverter.ToInt32(Ole2Document.MidByteArray(array, 48, 4), 0);
			if (num4 < 0)
			{
				throw new Exception(string.Format("Invalid Directory SID0 [{0}] (should be > 0)", num4));
			}
			uint num5 = BitConverter.ToUInt32(Ole2Document.MidByteArray(array, 56, 4), 0);
			if (num5 < Math.Pow(2.0, (double)num) || num5 % Math.Pow(2.0, (double)num) > 0.0)
			{
				throw new Exception(string.Format("Invalid MinStdStreamSize [{0}] (should be multiple of (2^SectorSize)", num5));
			}
			this._standardStreamMinBytes = num5;
			int num6 = BitConverter.ToInt32(Ole2Document.MidByteArray(array, 60, 4), 0);
			uint num7 = BitConverter.ToUInt32(Ole2Document.MidByteArray(array, 64, 4), 0);
			if (num6 < 0 && num6 != -2)
			{
				throw new Exception(string.Format("Invalid SSAT SID0 [{0}] (must be >=0 or -2", num6));
			}
			if (num7 > 0U && num6 < 0)
			{
				throw new Exception(string.Format("Invalid SSAT SID0 [{0}] (must be >=0 when SSAT Sector Count > 0)", num6));
			}
			if (num7 < 0U)
			{
				throw new Exception(string.Format("Invalid SSAT Sector Count [{0}] (must be >= 0)", num7));
			}
			int num8 = BitConverter.ToInt32(Ole2Document.MidByteArray(array, 68, 4), 0);
			if (num8 < 1 && num8 != -2)
			{
				throw new Exception(string.Format("Invalid MSAT SID0 [{0}]", num8));
			}
			uint num9 = BitConverter.ToUInt32(Ole2Document.MidByteArray(array, 72, 4), 0);
			if (num9 < 0U)
			{
				throw new Exception(string.Format("Invalid MSAT Sector Count [{0}]", num9));
			}
			if (num9 == 0U && num8 != -2)
			{
				throw new Exception(string.Format("Invalid MSAT SID0 [{0}] (should be -2)", num8));
			}
			int i = 0;
			int j = (int)Math.Pow(2.0, (double)num) / 4 - 1;
			int[] array2 = new int[108L + (long)j * (long)((ulong)num9) + 1L];
			for (int k = 0; k < 109; k++)
			{
				array2[k] = BitConverter.ToInt32(Ole2Document.MidByteArray(array, 76 + k * 4, 4), 0);
			}
			int num10 = num8;
			while ((long)i < (long)((ulong)num9))
			{
				Bytes sector = Ole2Document.GetSector(stream, (int)num, num10);
				if (sector.Length == 0)
				{
					throw new Exception(string.Format("MSAT SID Chain broken - SID [{0}] not found / EOF reached", num10));
				}
				for (int l = 0; l < j; l++)
				{
					array2[109 + i * j + l] = BitConverter.ToInt32(sector.Get(l * 4, 4).ByteArray, 0);
				}
				num10 = BitConverter.ToInt32(sector.Get(j * 4, 4).ByteArray, 0);
				i++;
			}
			i = array2.Length;
			while (array2[i - 1] < 0)
			{
				i--;
			}
			int[] array3 = new int[(uint)((double)i * (Math.Pow(2.0, (double)num) / 4.0))];
			int num11 = (int)(Math.Pow(2.0, (double)num) / 4.0);
			for (int m = 0; m < i; m++)
			{
				Bytes sector2 = Ole2Document.GetSector(stream, (int)num, array2[m]);
				if (sector2.Length == 0)
				{
					throw new Exception(string.Format("SAT SID Chain broken - SAT Sector SID{0} not found / EOF reached", array2[m]));
				}
				for (j = 0; j < num11; j++)
				{
					array3[m * num11 + j] = BitConverter.ToInt32(sector2.Get(j * 4, 4).ByteArray, 0);
				}
			}
			i = 0;
			int n = num6;
			int[] array4 = new int[(ulong)(num7 + 1U) * (ulong)((long)num11)];
			while (n > -2)
			{
				Bytes sector3 = Ole2Document.GetSector(stream, (int)num, n);
				if (sector3.Length == 0)
				{
					throw new Exception(string.Format("SSAT Sector SID{0} not found", n));
				}
				for (int num12 = 0; num12 < num11; num12++)
				{
					array4[i * num11 + num12] = BitConverter.ToInt32(sector3.Get(num12 * 4, 4).ByteArray, 0);
				}
				n = array3[n];
				i++;
			}
			if ((long)i < (long)((ulong)num7))
			{
				throw new Exception(string.Format("SSAT Sector chain broken: {0} found, header indicates {1}", i, num7));
			}
			int num13 = 0;
			int num14 = num4;
			num11 = (int)(Math.Pow(2.0, (double)num) / 128.0);
			Dictionary<int, byte[]> dictionary = new Dictionary<int, byte[]>();
			while (num14 > -2)
			{
				Bytes sector4 = Ole2Document.GetSector(stream, (int)num, num14);
				if (sector4.Length == 0)
				{
					throw new Exception(string.Format("Directory Sector SID{0} not found", num14));
				}
				for (int num15 = 0; num15 < num11; num15++)
				{
					dictionary[num13 * num11 + num15] = sector4.Get(num15 * 128, 128).ByteArray;
				}
				num14 = array3[num14];
				num13++;
			}
			for (i = 0; i < dictionary.Count; i++)
			{
				byte[] byteArray = dictionary[i];
				int length = (int)BitConverter.ToInt16(Ole2Document.MidByteArray(byteArray, 64, 2), 0);
				byte[] array5 = Ole2Document.MidByteArray(byteArray, 0, length);
				bool overwrite = false;
				if (Bytes.AreEqual(array5, Directory.RootName))
				{
					overwrite = true;
				}
				Bytes stream2 = this.GetStream(stream, i, dictionary, num, array3, num2, array4, num5);
				if (array5.Length != 0 || stream2.Length != 0)
				{
					this.Streams.AddNamed(stream2, array5, overwrite);
				}
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000BA70 File Offset: 0x0000AA70
		private Bytes GetStream(System.IO.Stream fromDocumentStream, int did, Dictionary<int, byte[]> dir, ushort sectorSize, int[] sat, ushort shortSectorSize, int[] ssat, uint minStandardStreamSize)
		{
			Bytes bytes = new Bytes();
			int num = BitConverter.ToInt32(Ole2Document.MidByteArray(dir[did], 120, 4), 0);
			Bytes bytes2 = null;
			if (did == 0 || (long)num >= (long)((ulong)minStandardStreamSize))
			{
				byte[] array = new byte[fromDocumentStream.Length];
				fromDocumentStream.Position = 0L;
				fromDocumentStream.Read(array, 0, array.Length);
				bytes2 = new Bytes(array);
			}
			ushort num2;
			int[] array2;
			string arg;
			Bytes bytes3;
			if (did == 0)
			{
				num2 = sectorSize;
				array2 = sat;
				arg = string.Empty;
				bytes3 = bytes2;
			}
			else if ((long)num < (long)((ulong)minStandardStreamSize))
			{
				num2 = shortSectorSize;
				array2 = ssat;
				arg = "Short ";
				bytes3 = this.GetStream(fromDocumentStream, 0, dir, sectorSize, sat, shortSectorSize, ssat, minStandardStreamSize);
			}
			else
			{
				num2 = sectorSize;
				array2 = sat;
				arg = string.Empty;
				bytes3 = bytes2;
			}
			for (int i = BitConverter.ToInt32(Ole2Document.MidByteArray(dir[did], 116, 4), 0); i > -2; i = array2[i])
			{
				Bytes bytes4;
				if (did > 0 && (long)num < (long)((ulong)minStandardStreamSize))
				{
					bytes4 = Ole2Document.GetShortSectorBytes(bytes3, (int)num2, i);
				}
				else
				{
					bytes4 = Ole2Document.GetSectorBytes(bytes3, (int)num2, i);
				}
				if (bytes4.Length == 0)
				{
					throw new Exception(string.Format("{0}Sector not found [SID{1}]", arg, i));
				}
				bytes.Append(bytes4);
			}
			return bytes.Get(num);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000BBA0 File Offset: 0x0000ABA0
		private static Bytes GetSectorBytes(Bytes fromStream, int sectorSize, int sid)
		{
			int num = (int)Math.Pow(2.0, (double)sectorSize);
			if (fromStream.Length < sid * num)
			{
				throw new Exception(string.Format("Invalid SID [{0}] (EOF reached)", sid));
			}
			return fromStream.Get(512 + sid * num, num);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000BBF0 File Offset: 0x0000ABF0
		private static Bytes GetShortSectorBytes(Bytes fromShortSectorStream, int shortSectorSize, int sid)
		{
			int num = (int)Math.Pow(2.0, (double)shortSectorSize);
			if (fromShortSectorStream.Length < sid * num)
			{
				throw new Exception(string.Format("Invalid SID [{0}] (EOF reached)", sid));
			}
			return fromShortSectorStream.Get(sid * num, num);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000BC3C File Offset: 0x0000AC3C
		private static Bytes GetSector(System.IO.Stream stream, int sectorSize, int sidIndex)
		{
			int num = (int)Math.Pow(2.0, (double)sectorSize);
			int num2 = 512 + sidIndex * num;
			if (stream.Length < (long)(num2 + num))
			{
				return new Bytes();
			}
			byte[] array = new byte[num];
			stream.Seek((long)num2, SeekOrigin.Begin);
			Ole2Document.ReadWholeArray(stream, array);
			return new Bytes(array);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000BC98 File Offset: 0x0000AC98
		public static void ReadWholeArray(System.IO.Stream stream, byte[] data)
		{
			int num = 0;
			int i = data.Length;
			while (i > 0)
			{
				int num2 = stream.Read(data, num, i);
				if (num2 <= 0)
				{
					throw new EndOfStreamException(string.Format("End of stream reached with {0} bytes left to read", i));
				}
				i -= num2;
				num += num2;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000BCE0 File Offset: 0x0000ACE0
		private static byte[] MidByteArray(byte[] byteArray, int offset, int length)
		{
			if (offset >= byteArray.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("offset {0} must be less than byteArray.Length {1}", offset, byteArray.Length));
			}
			if (offset + length > byteArray.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("offset {0} + length {1} must be <= byteArray.Length {2}", offset, length, byteArray.Length));
			}
			if (offset == 0 && length == byteArray.Length)
			{
				return byteArray;
			}
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = byteArray[offset + i];
			}
			return array;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000BD64 File Offset: 0x0000AD64
		public byte[] Blank1
		{
			get
			{
				return this._blank1;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000BD6C File Offset: 0x0000AD6C
		public byte[] Blank2
		{
			get
			{
				return this._blank2;
			}
		}

		// Token: 0x04000251 RID: 593
		private readonly Header _header;

		// Token: 0x04000252 RID: 594
		private readonly Msat _msat;

		// Token: 0x04000253 RID: 595
		private readonly Sat _sat;

		// Token: 0x04000254 RID: 596
		private readonly Ssat _ssat;

		// Token: 0x04000255 RID: 597
		private readonly Directory _directory;

		// Token: 0x04000256 RID: 598
		private readonly Streams _streams;

		// Token: 0x04000257 RID: 599
		private byte[] _docFileID;

		// Token: 0x04000258 RID: 600
		private byte[] _docUID;

		// Token: 0x04000259 RID: 601
		private byte[] _fileFormatRevision;

		// Token: 0x0400025A RID: 602
		private byte[] _fileFormatVersion;

		// Token: 0x0400025B RID: 603
		private bool _isLittleEndian;

		// Token: 0x0400025C RID: 604
		private ushort _sectorSize;

		// Token: 0x0400025D RID: 605
		private ushort _shortSectorSize;

		// Token: 0x0400025E RID: 606
		private byte[] _blank1;

		// Token: 0x0400025F RID: 607
		private byte[] _blank2;

		// Token: 0x04000260 RID: 608
		private uint _standardStreamMinBytes;

		// Token: 0x04000261 RID: 609
		private int _bytesPerSector;

		// Token: 0x04000262 RID: 610
		private int _bytesPerShortSector;
	}
}
