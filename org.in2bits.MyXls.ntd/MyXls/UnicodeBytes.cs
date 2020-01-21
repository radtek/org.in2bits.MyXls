using System;
using System.Collections.Generic;
using System.Text;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000049 RID: 73
	internal class UnicodeBytes
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000C83C File Offset: 0x0000B83C
		internal static void Write(string text, Bytes bytes)
		{
			ushort num = 0;
			UnicodeBytes.Write(text, bytes, ref num);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C854 File Offset: 0x0000B854
		internal static void Write(string text, Bytes bytes, ref ushort offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C85C File Offset: 0x0000B85C
		internal static string Read(Bytes bytes, int lengthBits)
		{
			Record record = new Record(RID.Empty, bytes);
			return UnicodeBytes.Read(record, lengthBits, 0);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C880 File Offset: 0x0000B880
		private static string Read(Record record, int lengthBits, ushort offset)
		{
			int num = -1;
			return UnicodeBytes.Read(record, lengthBits, ref num, ref offset);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C89C File Offset: 0x0000B89C
		internal static string Read(Record record, int lengthBits, ref int continueIndex, ref ushort offset)
		{
			string empty = string.Empty;
			UnicodeBytes.ReadState readState = new UnicodeBytes.ReadState(record, lengthBits, continueIndex, offset);
			UnicodeBytes.Read(readState);
			continueIndex = readState.ContinueIndex;
			offset = readState.Offset;
			return new string(readState.CharactersRead.ToArray());
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C8E4 File Offset: 0x0000B8E4
		private static void Read(UnicodeBytes.ReadState state)
		{
			Bytes recordData = state.GetRecordData();
			bool flag = (state.OptionsFlags & 1) == 0;
			ushort num = (ushort)(recordData.Length - (int)state.Offset);
			if (state.CharactersRead.Count < (int)state.TotalCharacters)
			{
				ushort num2 = (ushort)((int)state.TotalCharacters - state.CharactersRead.Count);
				if (!flag)
				{
					num2 *= 2;
				}
				ushort num3;
				if (num < num2)
				{
					num3 = num;
				}
				else
				{
					num3 = num2;
				}
				byte[] array = recordData.Get((int)state.Offset, (int)num3).ByteArray;
				if (flag)
				{
					byte[] array2 = new byte[array.Length * 2];
					for (int i = 0; i < array.Length; i++)
					{
						array2[2 * i] = array[i];
					}
					array = array2;
				}
				state.Offset += num3;
				num -= num3;
				state.CharactersRead.AddRange(Encoding.Unicode.GetChars(array));
			}
			bool flag2 = state.CharactersRead.Count == (int)state.TotalCharacters;
			if (state.HasRichTextSettings && num > 0 && flag2 && state.FormattingRunBytes.Count < (int)(state.FormattingRunCount * 4))
			{
				ushort num3 = Math.Min(num, (ushort)Math.Min((int)(state.FormattingRunCount * 4), 65535));
				state.FormattingRunBytes.AddRange(recordData.Get((int)state.Offset, (int)num3).ByteArray);
				state.Offset += num3;
				num -= num3;
			}
			if (state.HasAsianPhonetics && num > 0 && flag2 && (long)state.PhoneticSettingsBytes.Count < (long)((ulong)state.PhoneticSettingsByteCount))
			{
				ushort num3 = Math.Min(num, (ushort)Math.Min(state.PhoneticSettingsByteCount, 65535U));
				state.PhoneticSettingsBytes.AddRange(recordData.Get((int)state.Offset, (int)num3).ByteArray);
				state.Offset += num3;
				num -= num3;
			}
			if (state.CharactersRead.Count < (int)state.TotalCharacters || state.FormattingRunBytes.Count < (int)(state.FormattingRunCount * 4) || (long)state.PhoneticSettingsBytes.Count < (long)((ulong)state.PhoneticSettingsByteCount))
			{
				state.Continue(true);
				UnicodeBytes.Read(state);
				return;
			}
			if (num == 0 && state.ContinueIndex + 1 < state.Record.Continues.Count)
			{
				state.Continue(false);
			}
		}

		// Token: 0x0200004A RID: 74
		private class ReadState
		{
			// Token: 0x060002A7 RID: 679 RVA: 0x0000CB30 File Offset: 0x0000BB30
			public ReadState(Record record, int lengthBits, int continueIndex, ushort offset)
			{
				this.LengthBits = lengthBits;
				this.Record = record;
				this.ContinueIndex = continueIndex;
				this.Offset = offset;
				Bytes recordData = this.GetRecordData();
				if (this.LengthBits == 8)
				{
					this.TotalCharacters = (ushort)recordData.Get((int)offset, 1).ByteArray[0];
					this.Offset += 1;
				}
				else
				{
					this.TotalCharacters = recordData.Get((int)offset, 2).GetBits().ToUInt16();
					this.Offset += 2;
				}
				this.ReadOptionsFlags();
				this.HasAsianPhonetics = ((this.OptionsFlags & 4) == 4);
				this.HasRichTextSettings = ((this.OptionsFlags & 8) == 8);
				if (this.HasRichTextSettings)
				{
					this.FormattingRunCount = BitConverter.ToUInt16(recordData.Get((int)this.Offset, 2).ByteArray, 0);
					this.Offset += 2;
				}
				if (this.HasAsianPhonetics)
				{
					this.PhoneticSettingsByteCount = BitConverter.ToUInt32(recordData.Get((int)this.Offset, 4).ByteArray, 0);
					this.Offset += 4;
				}
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x0000CC74 File Offset: 0x0000BC74
			private void ReadOptionsFlags()
			{
				Bytes recordData = this.GetRecordData();
				ushort offset;
				this.Offset = (ushort)((offset = this.Offset) + 1);
				this.OptionsFlags = recordData.Get((int)offset, 1).ByteArray[0];
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x0000CCAC File Offset: 0x0000BCAC
			public Bytes GetRecordData()
			{
				if (this.ContinueIndex == -1)
				{
					return this.Record.Data;
				}
				return this.Record.Continues[this.ContinueIndex].Data;
			}

			// Token: 0x060002AA RID: 682 RVA: 0x0000CCDE File Offset: 0x0000BCDE
			public void Continue(bool readOptions)
			{
				this.ContinueIndex++;
				this.Offset = 0;
				if (readOptions)
				{
					this.ReadOptionsFlags();
				}
			}

			// Token: 0x0400028E RID: 654
			public Record Record;

			// Token: 0x0400028F RID: 655
			public int LengthBits;

			// Token: 0x04000290 RID: 656
			public ushort TotalCharacters;

			// Token: 0x04000291 RID: 657
			public int ContinueIndex;

			// Token: 0x04000292 RID: 658
			public ushort Offset;

			// Token: 0x04000293 RID: 659
			public List<char> CharactersRead = new List<char>();

			// Token: 0x04000294 RID: 660
			public bool HasAsianPhonetics;

			// Token: 0x04000295 RID: 661
			public bool HasRichTextSettings;

			// Token: 0x04000296 RID: 662
			public ushort FormattingRunCount;

			// Token: 0x04000297 RID: 663
			public List<byte> FormattingRunBytes = new List<byte>();

			// Token: 0x04000298 RID: 664
			public uint PhoneticSettingsByteCount;

			// Token: 0x04000299 RID: 665
			public List<byte> PhoneticSettingsBytes = new List<byte>();

			// Token: 0x0400029A RID: 666
			public byte OptionsFlags;
		}
	}
}
