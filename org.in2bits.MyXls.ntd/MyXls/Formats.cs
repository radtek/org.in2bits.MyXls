using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000047 RID: 71
	public class Formats
	{
		// Token: 0x06000294 RID: 660 RVA: 0x0000C243 File Offset: 0x0000B243
		static Formats()
		{
			Formats.AddDefaults();
			Formats._defaultFormatsToWrite.Add(StandardFormats.Currency_1);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C277 File Offset: 0x0000B277
		internal Formats(XlsDocument doc)
		{
			this._doc = doc;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C29C File Offset: 0x0000B29C
		private static void AddDefaults()
		{
			Formats._defaultFormatIds[StandardFormats.General] = 0;
			Formats._defaultFormatIds[StandardFormats.Decimal_1] = 1;
			Formats._defaultFormatIds[StandardFormats.Decimal_2] = 2;
			Formats._defaultFormatIds[StandardFormats.Decimal_3] = 3;
			Formats._defaultFormatIds[StandardFormats.Decimal_4] = 4;
			Formats._defaultFormatIds[StandardFormats.Currency_1] = 5;
			Formats._defaultFormatIds[StandardFormats.Currency_2] = 6;
			Formats._defaultFormatIds[StandardFormats.Currency_3] = 7;
			Formats._defaultFormatIds[StandardFormats.Currency_4] = 8;
			Formats._defaultFormatIds[StandardFormats.Percent_1] = 9;
			Formats._defaultFormatIds[StandardFormats.Percent_2] = 10;
			Formats._defaultFormatIds[StandardFormats.Scientific_1] = 11;
			Formats._defaultFormatIds[StandardFormats.Fraction_1] = 12;
			Formats._defaultFormatIds[StandardFormats.Fraction_2] = 13;
			Formats._defaultFormatIds[StandardFormats.Date_1] = 14;
			Formats._defaultFormatIds[StandardFormats.Date_2] = 15;
			Formats._defaultFormatIds[StandardFormats.Date_3] = 16;
			Formats._defaultFormatIds[StandardFormats.Date_4] = 17;
			Formats._defaultFormatIds[StandardFormats.Time_1] = 18;
			Formats._defaultFormatIds[StandardFormats.Time_2] = 19;
			Formats._defaultFormatIds[StandardFormats.Time_3] = 20;
			Formats._defaultFormatIds[StandardFormats.Time_4] = 21;
			Formats._defaultFormatIds[StandardFormats.Date_Time] = 22;
			Formats._defaultFormatIds[StandardFormats.Accounting_1] = 37;
			Formats._defaultFormatIds[StandardFormats.Accounting_2] = 38;
			Formats._defaultFormatIds[StandardFormats.Accounting_3] = 39;
			Formats._defaultFormatIds[StandardFormats.Accounting_4] = 40;
			Formats._defaultFormatIds[StandardFormats.Currency_5] = 41;
			Formats._defaultFormatIds[StandardFormats.Currency_6] = 42;
			Formats._defaultFormatIds[StandardFormats.Currency_7] = 43;
			Formats._defaultFormatIds[StandardFormats.Currency_8] = 44;
			Formats._defaultFormatIds[StandardFormats.Time_5] = 45;
			Formats._defaultFormatIds[StandardFormats.Time_6] = 46;
			Formats._defaultFormatIds[StandardFormats.Time_7] = 47;
			Formats._defaultFormatIds[StandardFormats.Scientific_2] = 48;
			Formats._defaultFormatIds[StandardFormats.Text] = 49;
		}

		// Token: 0x170000FF RID: 255
		internal string this[ushort index]
		{
			get
			{
				foreach (string text in this._userFormatIds.Keys)
				{
					if (this._userFormatIds[text] == index)
					{
						return text;
					}
				}
				foreach (string text2 in Formats._defaultFormatIds.Keys)
				{
					if (Formats._defaultFormatIds[text2] == index)
					{
						return text2;
					}
				}
				throw new IndexOutOfRangeException(string.Format("index {0} not found", index));
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C5D4 File Offset: 0x0000B5D4
		public ushort Add(string format)
		{
			return this.Add(format, null);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C5F4 File Offset: 0x0000B5F4
		private ushort Add(string format, ushort? id)
		{
			bool flag = id == null;
			ushort? id2 = this.GetID(format);
			bool flag2 = id2 != null;
			if (flag2)
			{
				return id2.Value;
			}
			if (flag)
			{
				ushort nextUserFormatId;
				this._nextUserFormatId = (ushort)((nextUserFormatId = this._nextUserFormatId) + 1);
				id = new ushort?(nextUserFormatId);
			}
			this._userFormatIds[format] = id.Value;
			return id.Value;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C660 File Offset: 0x0000B660
		internal ushort GetFinalID(string format)
		{
			ushort? id = this.GetID(format);
			if (id == null)
			{
				throw new ApplicationException(string.Format("Format {0} does not exist", format));
			}
			return id.Value;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C698 File Offset: 0x0000B698
		internal ushort? GetID(string format)
		{
			ushort? result = null;
			if (Formats._defaultFormatIds.ContainsKey(format))
			{
				result = new ushort?(Formats._defaultFormatIds[format]);
			}
			if (this._userFormatIds.ContainsKey(format))
			{
				result = new ushort?(this._userFormatIds[format]);
			}
			return result;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000C6F0 File Offset: 0x0000B6F0
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				foreach (string text in Formats._defaultFormatsToWrite)
				{
					if (Formats._defaultFormatIds.ContainsKey(text))
					{
						bytes.Append(this.GetFormatRecord(Formats._defaultFormatIds[text], text));
					}
				}
				foreach (string text2 in this._userFormatIds.Keys)
				{
					bytes.Append(this.GetFormatRecord(this._userFormatIds[text2], text2));
				}
				return bytes;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000C7C4 File Offset: 0x0000B7C4
		public object Count
		{
			get
			{
				return this._userFormatIds.Count + Formats._defaultFormatsToWrite.Count;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C7E4 File Offset: 0x0000B7E4
		private Bytes GetFormatRecord(ushort id, string format)
		{
			Bytes bytes = new Bytes();
			bytes.Append(BitConverter.GetBytes(id));
			bytes.Append(XlsDocument.GetUnicodeString(format, 16));
			return Record.GetBytes(RID.FORMAT, bytes);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C81C File Offset: 0x0000B81C
		public bool ContainsKey(ushort index)
		{
			return Formats._defaultFormatIds.ContainsValue(index) || this._userFormatIds.ContainsValue(index);
		}

		// Token: 0x04000283 RID: 643
		private readonly XlsDocument _doc;

		// Token: 0x04000284 RID: 644
		private static readonly Dictionary<string, ushort> _defaultFormatIds = new Dictionary<string, ushort>();

		// Token: 0x04000285 RID: 645
		private readonly Dictionary<string, ushort> _userFormatIds = new Dictionary<string, ushort>();

		// Token: 0x04000286 RID: 646
		private static List<string> _defaultFormatsToWrite = new List<string>();

		// Token: 0x04000287 RID: 647
		internal static string Default = StandardFormats.General;

		// Token: 0x04000288 RID: 648
		private ushort _nextUserFormatId = 164;
	}
}
