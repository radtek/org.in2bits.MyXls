using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000037 RID: 55
	internal struct CachedBlockRow
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00008E13 File Offset: 0x00007E13
		internal CachedBlockRow(ushort rowBlockIndex, ushort blockRowIndex, Row row)
		{
			this.RowBlockIndex = rowBlockIndex;
			this.BlockRowIndex = blockRowIndex;
			this.Row = row;
		}

		// Token: 0x040001EF RID: 495
		internal ushort RowBlockIndex;

		// Token: 0x040001F0 RID: 496
		internal ushort BlockRowIndex;

		// Token: 0x040001F1 RID: 497
		internal Row Row;

		// Token: 0x040001F2 RID: 498
		internal static CachedBlockRow Empty = new CachedBlockRow(0, 0, null);
	}
}
