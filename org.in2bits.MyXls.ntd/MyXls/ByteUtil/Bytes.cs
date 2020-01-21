using System;
using System.Collections.Generic;
using System.IO;

namespace org.in2bits.MyXls.ByteUtil
{
	// Token: 0x02000007 RID: 7
	public class Bytes
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000246F File Offset: 0x0000146F
		public Bytes()
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002478 File Offset: 0x00001478
		public Bytes(byte b) : this(new byte[]
		{
			b
		})
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002497 File Offset: 0x00001497
		public Bytes(byte[] byteArray) : this()
		{
			this.CheckNewLength(byteArray);
			this._byteArray = byteArray;
			this._length = byteArray.Length;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024B6 File Offset: 0x000014B6
		public Bytes(Bytes bytes) : this()
		{
			this.CheckNewLength(bytes);
			this._bytesList = new List<Bytes>();
			this._bytesList.Add(bytes);
			this._length = bytes.Length;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024E8 File Offset: 0x000014E8
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024F0 File Offset: 0x000014F0
		internal bool IsEmpty
		{
			get
			{
				return this._length == 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000024FB File Offset: 0x000014FB
		internal bool IsArray
		{
			get
			{
				return this._byteArray != null;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000250C File Offset: 0x0000150C
		public void Append(byte[] byteArray)
		{
			if (byteArray.Length == 0)
			{
				return;
			}
			this.CheckNewLength(byteArray);
			if (this.IsEmpty)
			{
				this._byteArray = byteArray;
			}
			else if (this.IsArray)
			{
				this.ConvertToList();
				this._bytesList.Add(new Bytes(byteArray));
			}
			else
			{
				this._bytesList.Add(new Bytes(byteArray));
			}
			this._length += byteArray.Length;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000257C File Offset: 0x0000157C
		public void Append(byte b)
		{
			this.Append(new byte[]
			{
				b
			});
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000259C File Offset: 0x0000159C
		public void Append(Bytes bytes)
		{
			if (bytes.Length == 0)
			{
				return;
			}
			this.CheckNewLength(bytes);
			if (this.IsEmpty)
			{
				this._bytesList = new List<Bytes>();
				this._bytesList.Add(bytes);
			}
			else if (this.IsArray)
			{
				this.ConvertToList();
				this._bytesList.Add(bytes);
			}
			else
			{
				this._bytesList.Add(bytes);
			}
			this._length += bytes.Length;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002615 File Offset: 0x00001615
		public void Prepend(byte[] byteArray)
		{
			this.Prepend(new Bytes(byteArray));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002624 File Offset: 0x00001624
		public void Prepend(Bytes bytes)
		{
			if (bytes.Length == 0)
			{
				return;
			}
			this.CheckNewLength(bytes);
			if (this.IsEmpty)
			{
				this.Append(bytes);
				return;
			}
			if (this.IsArray)
			{
				this.ConvertToList();
			}
			this._bytesList.Insert(0, bytes);
			this._length += bytes.Length;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002680 File Offset: 0x00001680
		public byte[] ByteArray
		{
			get
			{
				if (this.IsEmpty)
				{
					return new byte[0];
				}
				MemoryStream memoryStream = new MemoryStream();
				this.WriteToStream(memoryStream);
				return memoryStream.ToArray();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026AF File Offset: 0x000016AF
		public Bytes Get(int getLength)
		{
			return this.Get(0, getLength);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026BC File Offset: 0x000016BC
		public Bytes Get(int offset, int getLength)
		{
			Bytes bytes = new Bytes();
			if (getLength == 0)
			{
				return bytes;
			}
			this.Get(offset, getLength, bytes);
			return bytes;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026E0 File Offset: 0x000016E0
		private void Get(int offset, int getLength, Bytes intoBytes)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("offset {0} must be >= 0", offset));
			}
			if (getLength < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("getLength {0} must be >= 0", getLength));
			}
			if (offset >= this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("offset {0} must be < Length {1}", offset, this.Length));
			}
			if (getLength + offset > this.Length)
			{
				throw new ArgumentOutOfRangeException(string.Format("offset {0} + getLength {1} = {2} must be < Length {3}", new object[]
				{
					offset,
					getLength,
					offset + getLength,
					this.Length
				}));
			}
			if (!this.IsArray)
			{
				foreach (Bytes bytes in this._bytesList)
				{
					if (bytes.Length <= offset)
					{
						offset -= bytes.Length;
					}
					else
					{
						if (bytes.Length >= offset + getLength)
						{
							bytes.Get(offset, getLength, intoBytes);
							break;
						}
						int num = bytes.Length - offset;
						bytes.Get(offset, num, intoBytes);
						getLength -= num;
						offset = 0;
					}
				}
				return;
			}
			if (offset == 0 && getLength == this.Length)
			{
				intoBytes.Append(this._byteArray);
				return;
			}
			intoBytes.Append(Bytes.MidByteArray(this._byteArray, offset, getLength));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002854 File Offset: 0x00001854
		internal static byte[] MidByteArray(byte[] byteArray, int offset, int length)
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

		// Token: 0x0600001E RID: 30 RVA: 0x000028D8 File Offset: 0x000018D8
		internal void WriteToStream(Stream stream)
		{
			if (this.IsEmpty)
			{
				return;
			}
			if (this.IsArray)
			{
				stream.Write(this._byteArray, 0, this._length);
				return;
			}
			foreach (Bytes bytes in this._bytesList)
			{
				bytes.WriteToStream(stream);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002950 File Offset: 0x00001950
		private void CheckNewLength(byte[] withAddition)
		{
			this.CheckNewLength(withAddition.Length);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000295B File Offset: 0x0000195B
		private void CheckNewLength(Bytes withAddition)
		{
			this.CheckNewLength(withAddition.Length);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002969 File Offset: 0x00001969
		private void CheckNewLength(int withAddition)
		{
			if (this._length + withAddition > 2147483647)
			{
				throw new Exception(string.Format("Addition of {0} bytes would exceed current limit of {1} bytes", withAddition, int.MaxValue));
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000299C File Offset: 0x0000199C
		private void ConvertToList()
		{
			this._bytesList = new List<Bytes>();
			if (this.IsEmpty)
			{
				return;
			}
			Bytes item = new Bytes(this._byteArray);
			this._byteArray = null;
			this._bytesList.Add(item);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029DC File Offset: 0x000019DC
		public static bool AreEqual(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A0C File Offset: 0x00001A0C
		public Bytes.Bits GetBits()
		{
			return new Bytes.Bits(this);
		}

		// Token: 0x0400000C RID: 12
		private byte[] _byteArray;

		// Token: 0x0400000D RID: 13
		internal List<Bytes> _bytesList;

		// Token: 0x0400000E RID: 14
		private int _length;

		// Token: 0x02000008 RID: 8
		public class Bits
		{
			// Token: 0x06000025 RID: 37 RVA: 0x00002A14 File Offset: 0x00001A14
			public Bits(Bytes bytes)
			{
				byte[] byteArray = bytes.ByteArray;
				this._bits = new bool[byteArray.Length * 8];
				byte b = 0;
				while ((int)b < byteArray.Length)
				{
					this.SetBits(b, byteArray[(int)b]);
					b += 1;
				}
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002A63 File Offset: 0x00001A63
			public Bits(bool[] bits)
			{
				this._bits = bits;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002A80 File Offset: 0x00001A80
			private void SetBits(byte byteIndex, byte fromByte)
			{
				byte b = 7;
				while (b >= 0 && b < 255)
				{
					byte b2 = (byte)Math.Pow(2.0, (double)b);
					if (fromByte >= b2)
					{
						this._bits[(int)(byteIndex * 8 + b)] = true;
						fromByte -= b2;
					}
					b -= 1;
				}
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002ACC File Offset: 0x00001ACC
			public void Prepend(bool bit)
			{
				bool[] array = new bool[this._bits.Length + 1];
				this._bits.CopyTo(array, 1);
				this._bits = array;
				this._bits[0] = bit;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002B06 File Offset: 0x00001B06
			public Bytes.Bits Get(int getLength)
			{
				return this.Get(0, getLength);
			}

			// Token: 0x0600002A RID: 42 RVA: 0x00002B10 File Offset: 0x00001B10
			public Bytes.Bits Get(int offset, int getLength)
			{
				if (offset < 0)
				{
					throw new ArgumentOutOfRangeException(string.Format("offset {0} must be >= 0", offset));
				}
				if (getLength < 0)
				{
					throw new ArgumentOutOfRangeException(string.Format("getLength {0} must be >= 0", getLength));
				}
				if (offset >= this.Length)
				{
					throw new ArgumentOutOfRangeException(string.Format("offset {0} must be < Length {1}", offset, this.Length));
				}
				if (getLength + offset > this.Length)
				{
					throw new ArgumentOutOfRangeException(string.Format("offset {0} + getLength {1} = {2} must be < Length {3}", new object[]
					{
						offset,
						getLength,
						offset + getLength,
						this.Length
					}));
				}
				bool[] array = new bool[getLength];
				Array.Copy(this._bits, offset, array, 0, getLength);
				return new Bytes.Bits(array);
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BE5 File Offset: 0x00001BE5
			public int Length
			{
				get
				{
					return this._bits.Length;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002BEF File Offset: 0x00001BEF
			public bool[] Values
			{
				get
				{
					return this._bits;
				}
			}

			// Token: 0x0600002D RID: 45 RVA: 0x00002BF8 File Offset: 0x00001BF8
			public uint ToUInt32()
			{
				int num = this._bits.Length;
				if (num > 32)
				{
					throw new ApplicationException(string.Format("Length {0} must be <= 32", num));
				}
				uint num2 = 0U;
				for (int i = num - 1; i >= 0; i--)
				{
					if (this._bits[i])
					{
						num2 += (uint)Math.Pow(2.0, (double)i);
					}
				}
				return num2;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002C58 File Offset: 0x00001C58
			public int ToInt32()
			{
				int num = this._bits.Length;
				if (num > 32)
				{
					throw new ApplicationException(string.Format("Length {0} must be <= 32", num));
				}
				int num2 = 0;
				for (int i = num - 1; i >= 0; i--)
				{
					if (this._bits[i])
					{
						num2 += (int)Math.Pow(2.0, (double)i);
					}
				}
				return num2;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002CB8 File Offset: 0x00001CB8
			public ushort ToUInt16()
			{
				int num = this._bits.Length;
				if (num > 16)
				{
					throw new ApplicationException(string.Format("Length {0} must be <= 16", num));
				}
				ushort num2 = 0;
				for (int i = 0; i < num; i++)
				{
					if (this._bits[i])
					{
						num2 += (ushort)Math.Pow(2.0, (double)i);
					}
				}
				return num2;
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002D18 File Offset: 0x00001D18
			public ulong ToUInt64()
			{
				int num = this._bits.Length;
				if (num > 64)
				{
					throw new ApplicationException(string.Format("Length {0} must be <= 64", num));
				}
				ushort num2 = 0;
				for (int i = 0; i < num; i++)
				{
					if (this._bits[i])
					{
						num2 += (ushort)Math.Pow(2.0, (double)i);
					}
				}
				return (ulong)num2;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00002D78 File Offset: 0x00001D78
			public Bytes GetBytes()
			{
				byte[] array = new byte[(int)Math.Ceiling((double)this._bits.Length / 8.0)];
				for (int i = array.Length - 1; i >= 0; i--)
				{
					byte b = 0;
					for (int j = 7; j >= 0; j--)
					{
						int num = 8 * i + j;
						if (num < this._bits.Length && this._bits[num])
						{
							b += (byte)Math.Pow(2.0, (double)j);
						}
					}
					array[i] = b;
				}
				return new Bytes(array);
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002E00 File Offset: 0x00001E00
			public double ToDouble()
			{
				List<bool> list = new List<bool>();
				list.AddRange(new bool[64 - this._bits.Length]);
				list.AddRange(this._bits);
				byte[] array = new byte[8];
				for (int i = 7; i >= 0; i--)
				{
					for (int j = 7; j >= 0; j--)
					{
						if (list[8 * i + j])
						{
							byte[] array2 = array;
							int num = i;
							array2[num] += (byte)Math.Pow(2.0, (double)j);
						}
					}
				}
				return BitConverter.ToDouble(array, 0);
			}

			// Token: 0x0400000F RID: 15
			private bool[] _bits = new bool[0];
		}
	}
}
