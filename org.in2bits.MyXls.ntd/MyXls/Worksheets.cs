using System;
using System.Collections;
using System.Collections.Generic;

namespace org.in2bits.MyXls
{
	// Token: 0x02000031 RID: 49
	public class Worksheets : IEnumerable<Worksheet>, IEnumerable
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000084D8 File Offset: 0x000074D8
		internal Worksheets(XlsDocument doc)
		{
			this._doc = doc;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000084F4 File Offset: 0x000074F4
		internal void Add(Record boundSheetRecord, List<Record> sheetRecords)
		{
			Worksheet item = new Worksheet(this._doc, boundSheetRecord, sheetRecords);
			this._worksheets.Add(item);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000851C File Offset: 0x0000751C
		public Worksheet Add(string name)
		{
			Worksheet worksheet = new Worksheet(this._doc);
			worksheet.Name = name;
			this._worksheets.Add(worksheet);
			return worksheet;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00008549 File Offset: 0x00007549
		public int Count
		{
			get
			{
				return this._worksheets.Count;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008556 File Offset: 0x00007556
		[Obsolete]
		public Worksheet AddNamed(string name)
		{
			return this.Add(name);
		}

		// Token: 0x1700008A RID: 138
		public Worksheet this[int index]
		{
			get
			{
				return this._worksheets[index];
			}
		}

		// Token: 0x1700008B RID: 139
		public Worksheet this[string name]
		{
			get
			{
				return this[this.GetIndex(name)];
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000857C File Offset: 0x0000757C
		public int GetIndex(string sheetName)
		{
			int num = 0;
			foreach (Worksheet worksheet in this)
			{
				if (string.Compare(worksheet.Name, sheetName, false) == 0)
				{
					return num;
				}
				num++;
			}
			throw new IndexOutOfRangeException(sheetName);
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000085E0 File Offset: 0x000075E0
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000085E8 File Offset: 0x000075E8
		internal int StreamOffset
		{
			get
			{
				return this._streamOffset;
			}
			set
			{
				this._streamOffset = value;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000085F1 File Offset: 0x000075F1
		public IEnumerator<Worksheet> GetEnumerator()
		{
			return new Worksheets.WorksheetEnumerator(this);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000085F9 File Offset: 0x000075F9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040001DB RID: 475
		private List<Worksheet> _worksheets = new List<Worksheet>();

		// Token: 0x040001DC RID: 476
		private readonly XlsDocument _doc;

		// Token: 0x040001DD RID: 477
		private int _streamOffset;

		// Token: 0x02000032 RID: 50
		public class WorksheetEnumerator : IEnumerator<Worksheet>, IDisposable, IEnumerator
		{
			// Token: 0x06000181 RID: 385 RVA: 0x00008601 File Offset: 0x00007601
			public WorksheetEnumerator(Worksheets worksheets)
			{
				if (worksheets == null)
				{
					throw new ArgumentNullException("worksheets");
				}
				this._worksheets = worksheets;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00008625 File Offset: 0x00007625
			public void Dispose()
			{
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00008627 File Offset: 0x00007627
			public bool MoveNext()
			{
				if (this._worksheets.Count == 0)
				{
					return false;
				}
				if (this._index == this._worksheets.Count - 1)
				{
					return false;
				}
				this._index++;
				return true;
			}

			// Token: 0x06000184 RID: 388 RVA: 0x0000865E File Offset: 0x0000765E
			public void Reset()
			{
				this._index = -1;
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000185 RID: 389 RVA: 0x00008667 File Offset: 0x00007667
			public Worksheet Current
			{
				get
				{
					return this._worksheets[this._index];
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000186 RID: 390 RVA: 0x0000867A File Offset: 0x0000767A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x040001DE RID: 478
			private Worksheets _worksheets;

			// Token: 0x040001DF RID: 479
			private int _index = -1;
		}
	}
}
