using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000025 RID: 37
	internal class SharedStringTable
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005838 File Offset: 0x00004838
		internal uint CountUnique
		{
			get
			{
				return this._countUnique;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005840 File Offset: 0x00004840
		internal uint Add(string sharedString)
		{
			this._countAll += 1U;
			int num = this._stringsA.IndexOf(sharedString);
			if (num != -1)
			{
				List<uint> countsA;
				int index;
				(countsA = this._countsA)[index = num] = countsA[index] + 1U;
				return (uint)num;
			}
			if (!this._listAIsFull && this._stringsA.Count == 2147483647)
			{
				this._listAIsFull = true;
			}
			if (!this._listAIsFull)
			{
				this._stringsA.Add(sharedString);
				this._countsA.Add(1U);
				this._countUnique += 1U;
				return (uint)(this._stringsA.Count - 1);
			}
			num = this._stringsB.IndexOf(sharedString);
			if (num != -1)
			{
				List<uint> countsB;
				int index2;
				(countsB = this._countsB)[index2 = num] = countsB[index2] + 1U;
				return (uint)(int.MaxValue + num);
			}
			this._stringsB.Add(sharedString);
			this._countsB.Add(1U);
			this._countUnique += 1U;
			return (uint)(int.MaxValue + (this._stringsB.Count - 1));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005951 File Offset: 0x00004951
		internal string GetString(uint atIndex)
		{
			if (atIndex <= 2147483647U)
			{
				return this._stringsA[(int)atIndex];
			}
			atIndex -= 2147483647U;
			return this._stringsB[(int)atIndex];
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000597D File Offset: 0x0000497D
		internal uint GetCount(uint atIndex)
		{
			if (atIndex <= 2147483647U)
			{
				return this._countsA[(int)atIndex];
			}
			atIndex -= 2147483647U;
			return this._countsB[(int)atIndex];
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000059AC File Offset: 0x000049AC
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bool flag = true;
				Bytes bytes2 = new Bytes();
				bytes2.Append(BitConverter.GetBytes(this._countUnique));
				bytes2.Append(BitConverter.GetBytes(this._countAll));
				int num = 8224 - bytes2.Length;
				this.AddStrings(this._stringsA, ref num, ref bytes2, bytes, ref flag);
				this.AddStrings(this._stringsB, ref num, ref bytes2, bytes, ref flag);
				this.Continue(bytes, bytes2, out num, ref flag);
				return bytes;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005A2C File Offset: 0x00004A2C
		internal void ReadBytes(Record sstRecord)
		{
			uint countAll = sstRecord.Data.Get(0, 4).GetBits().ToUInt32();
			uint num = sstRecord.Data.Get(4, 4).GetBits().ToUInt32();
			int num2 = 0;
			ushort num3 = 8;
			int num4 = -1;
			while ((long)num2 < (long)((ulong)num))
			{
				string sharedString = UnicodeBytes.Read(sstRecord, 16, ref num4, ref num3);
				this.Add(sharedString);
				num2++;
			}
			this._countAll = countAll;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005A9C File Offset: 0x00004A9C
		private void AddStrings(List<string> stringList, ref int remainingRecordBytes, ref Bytes bytes, Bytes sst, ref bool isFirstContinue)
		{
			foreach (string text in stringList)
			{
				Bytes bytes2 = XlsDocument.GetUnicodeString(text, 16);
				byte b = byte.MaxValue;
				int num = int.MaxValue;
				if (bytes2.Length > remainingRecordBytes)
				{
					b = bytes2.Get(2, 1).ByteArray[0];
					num = (((b & 1) == 1) ? 5 : 4);
				}
				while (bytes2 != null)
				{
					if (bytes2.Length > remainingRecordBytes)
					{
						bool flag = false;
						if (remainingRecordBytes > num)
						{
							int getLength = bytes2.Length - remainingRecordBytes;
							bytes.Append(bytes2.Get(0, remainingRecordBytes));
							bytes2 = bytes2.Get(remainingRecordBytes, getLength);
							remainingRecordBytes -= remainingRecordBytes;
							flag = true;
						}
						bytes = this.Continue(sst, bytes, out remainingRecordBytes, ref isFirstContinue);
						if (flag)
						{
							bytes.Append(b);
							remainingRecordBytes--;
						}
					}
					else
					{
						bytes.Append(bytes2);
						remainingRecordBytes -= bytes2.Length;
						bytes2 = null;
					}
				}
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005BB0 File Offset: 0x00004BB0
		private Bytes Continue(Bytes sst, Bytes bytes, out int remainingRecordBytes, ref bool isFirstContinue)
		{
			sst.Append(Record.GetBytes(isFirstContinue ? RID.SST : RID.CONTINUE, bytes));
			remainingRecordBytes = 8224;
			isFirstContinue = false;
			return new Bytes();
		}

		// Token: 0x04000184 RID: 388
		private readonly List<string> _stringsA = new List<string>();

		// Token: 0x04000185 RID: 389
		private readonly List<string> _stringsB = new List<string>();

		// Token: 0x04000186 RID: 390
		private readonly List<uint> _countsA = new List<uint>();

		// Token: 0x04000187 RID: 391
		private readonly List<uint> _countsB = new List<uint>();

		// Token: 0x04000188 RID: 392
		private bool _listAIsFull;

		// Token: 0x04000189 RID: 393
		private uint _countUnique;

		// Token: 0x0400018A RID: 394
		private uint _countAll;
	}
}
