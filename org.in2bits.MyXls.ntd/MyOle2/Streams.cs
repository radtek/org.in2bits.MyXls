using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2
{
	// Token: 0x02000035 RID: 53
	public class Streams
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00008844 File Offset: 0x00007844
		public Streams(Ole2Document doc)
		{
			this._doc = doc;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000885E File Offset: 0x0000785E
		public Stream AddNamed(byte[] bytes, byte[] name)
		{
			return this.AddNamed(new Bytes(bytes), name);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000886D File Offset: 0x0000786D
		public Stream AddNamed(Bytes bytes, byte[] name)
		{
			return this.AddNamed(bytes, name, false);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008878 File Offset: 0x00007878
		public Stream AddNamed(Bytes bytes, byte[] name, bool overwrite)
		{
			int index = this.GetIndex(name);
			if (index != -1 && !overwrite)
			{
				throw new ArgumentException("value already exists", "name");
			}
			Stream stream;
			if (index != -1)
			{
				stream = this[index];
			}
			else
			{
				stream = new Stream(this._doc);
				stream.Name = name;
				this._streams.Add(stream);
			}
			stream.Bytes = bytes;
			return stream;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000088DE File Offset: 0x000078DE
		public int Count
		{
			get
			{
				return this._streams.Count - 1;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000088F0 File Offset: 0x000078F0
		public int SectorCount
		{
			get
			{
				int num = 0;
				num += this.ShortSectorStorage.SectorCount;
				for (int i = 1; i <= this.Count; i++)
				{
					num += this[i].SectorCount;
				}
				return num;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00008933 File Offset: 0x00007933
		public Stream ShortSectorStorage
		{
			get
			{
				return this._streams[0];
			}
		}

		// Token: 0x17000094 RID: 148
		public Stream this[object idx]
		{
			get
			{
				if (idx is int)
				{
					return this._streams[(int)idx];
				}
				if (idx is byte[])
				{
					return this._streams[this.GetIndex((byte[])idx)];
				}
				if (idx is string)
				{
					throw new NotImplementedException();
				}
				return null;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000899C File Offset: 0x0000799C
		public int GetIndex(byte[] streamName)
		{
			for (int i = 0; i < this._streams.Count; i++)
			{
				if (Bytes.AreEqual(this._streams[i].Name, streamName))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000089DC File Offset: 0x000079DC
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				int num;
				for (int i = 1; i <= this.Count; i++)
				{
					Stream stream = this[i];
					if (stream.IsShort)
					{
						bytes.Append(stream.Bytes);
						num = (int)(stream.Bytes.Length % this._doc.BytesPerShortSector);
						if (num > 0)
						{
							bytes.Append(new byte[this._doc.BytesPerShortSector - num]);
						}
					}
				}
				num = (int)(bytes.Length % this._doc.BytesPerSector);
				if (num > 0)
				{
					bytes.Append(new byte[this._doc.BytesPerSector - num]);
				}
				for (int j = 1; j <= this.Count; j++)
				{
					Stream stream = this[j];
					if (!stream.IsShort)
					{
						bytes.Append(stream.Bytes);
						num = (int)(stream.Bytes.Length % this._doc.BytesPerSector);
						if (num > 0)
						{
							bytes.Append(new byte[this._doc.BytesPerSector - num]);
						}
					}
				}
				return bytes;
			}
		}

		// Token: 0x040001EC RID: 492
		private readonly Ole2Document _doc;

		// Token: 0x040001ED RID: 493
		private readonly List<Stream> _streams = new List<Stream>();
	}
}
