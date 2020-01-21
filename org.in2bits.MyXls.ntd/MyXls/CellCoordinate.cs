using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000006 RID: 6
	public struct CellCoordinate
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000245F File Offset: 0x0000145F
		public CellCoordinate(ushort row, ushort column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x0400000A RID: 10
		public ushort Row;

		// Token: 0x0400000B RID: 11
		public ushort Column;
	}
}
