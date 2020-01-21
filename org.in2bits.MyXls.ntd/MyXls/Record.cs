using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000014 RID: 20
	internal class Record
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000035A4 File Offset: 0x000025A4
		protected Record()
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035C2 File Offset: 0x000025C2
		internal Record(byte[] rid, byte[] data) : this(rid, new Bytes(data))
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035D4 File Offset: 0x000025D4
		internal Record(byte[] rid, Bytes data)
		{
			this._rid = org.in2bits.MyXls.RID.ByteArray(rid);
			int num = 0;
			int i = data.Length;
			int num2 = -1;
			while (i > 0)
			{
				int num3 = Math.Min(i, 8224);
				if (num2 == -1)
				{
					this._data = data.Get(num, num3);
				}
				else
				{
					this._continues.Add(new Record(org.in2bits.MyXls.RID.CONTINUE, data.Get(num, num3)));
				}
				num += num3;
				i -= num3;
				num2++;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003663 File Offset: 0x00002663
		internal byte[] RID
		{
			get
			{
				return this._rid;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000366B File Offset: 0x0000266B
		internal Bytes Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003673 File Offset: 0x00002673
		internal List<Record> Continues
		{
			get
			{
				return this._continues;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000367B File Offset: 0x0000267B
		internal static Bytes GetBytes(byte[] rid, byte[] data)
		{
			return Record.GetBytes(rid, new Bytes(data));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000368C File Offset: 0x0000268C
		internal static Bytes GetBytes(byte[] rid, Bytes data)
		{
			if (rid.Length != 2)
			{
				throw new ArgumentException("must be 2 bytes", "rid");
			}
			Bytes bytes = new Bytes();
			ushort num = 0;
			ushort num2 = (ushort)data.Length;
			do
			{
				ushort num3 = (ushort)Math.Min(num2 - num, 8224);
				if (num == 0)
				{
					bytes.Append(rid);
					bytes.Append(BitConverter.GetBytes(num3));
					bytes.Append(data.Get((int)num, (int)num3));
				}
				else
				{
					bytes.Append(org.in2bits.MyXls.RID.CONTINUE);
					bytes.Append(BitConverter.GetBytes(num3));
					bytes.Append(data.Get((int)num, (int)num3));
				}
				num += num3;
			}
			while (num < num2);
			return bytes;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003724 File Offset: 0x00002724
		internal bool IsCellRecord()
		{
			return this._rid == org.in2bits.MyXls.RID.RK || this._rid == org.in2bits.MyXls.RID.NUMBER || this._rid == org.in2bits.MyXls.RID.LABEL || this._rid == org.in2bits.MyXls.RID.LABELSST || this._rid == org.in2bits.MyXls.RID.MULBLANK || this._rid == org.in2bits.MyXls.RID.MULRK || this._rid == org.in2bits.MyXls.RID.FORMULA;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003790 File Offset: 0x00002790
		public static List<Record> GetAll(Bytes stream)
		{
			int i = 0;
			List<Record> list = new List<Record>();
			Record record = Record.Empty;
			while (i < stream.Length - 4)
			{
				byte[] array = org.in2bits.MyXls.RID.ByteArray(stream.Get(i, 2).ByteArray);
				Bytes data = new Bytes();
				if (array == org.in2bits.MyXls.RID.Empty)
				{
					break;
				}
				int num = (int)BitConverter.ToUInt16(stream.Get(i + 2, 2).ByteArray, 0);
				data = stream.Get(i + 4, num);
				Record record2 = new Record(array, data);
				i += 4 + num;
				if (array == org.in2bits.MyXls.RID.CONTINUE)
				{
					if (record == Record.Empty)
					{
						throw new ApplicationException("Found CONTINUE record without previous/parent record.");
					}
					record.Continues.Add(record2);
				}
				else
				{
					record = record2;
					list.Add(record2);
				}
			}
			return list;
		}

		// Token: 0x04000092 RID: 146
		protected byte[] _rid;

		// Token: 0x04000093 RID: 147
		protected Bytes _data = new Bytes();

		// Token: 0x04000094 RID: 148
		protected List<Record> _continues = new List<Record>();

		// Token: 0x04000095 RID: 149
		public static Record Empty = new Record(org.in2bits.MyXls.RID.Empty, new byte[0]);
	}
}
