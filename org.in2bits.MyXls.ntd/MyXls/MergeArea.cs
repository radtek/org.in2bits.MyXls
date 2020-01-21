using System;

namespace org.in2bits.MyXls
{
	// Token: 0x0200004E RID: 78
	public struct MergeArea
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000DAEC File Offset: 0x0000CAEC
		public MergeArea(ushort rowMin, ushort rowMax, ushort colMin, ushort colMax)
		{
			this.RowMin = rowMin;
			this.RowMax = rowMax;
			this.ColMin = colMin;
			this.ColMax = colMax;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000DB0C File Offset: 0x0000CB0C
		public MergeArea(int rowMin, int rowMax, int colMin, int colMax)
		{
			this = new MergeArea((ushort)rowMin, (ushort)rowMax, (ushort)colMin, (ushort)colMax);
			if (rowMin < 1)
			{
				throw new ArgumentOutOfRangeException("rowMin", "must be >= 1");
			}
			if (rowMin > 65535)
			{
				throw new ArgumentOutOfRangeException("rowMin", "must be <= " + ushort.MaxValue);
			}
			if (rowMax < rowMin)
			{
				throw new ArgumentOutOfRangeException("rowMax", "must be >= rowMin (" + rowMin + ")");
			}
			if (rowMax > 65535)
			{
				throw new ArgumentOutOfRangeException("rowMax", "must be <=" + ushort.MaxValue);
			}
			if (colMin < 1)
			{
				throw new ArgumentOutOfRangeException("colMin", "must be >= 1");
			}
			if (colMin > 255)
			{
				throw new ArgumentOutOfRangeException("colMin", "must be <= " + 255);
			}
			if (colMax < colMin)
			{
				throw new ArgumentOutOfRangeException("colMax", "must be >= colMin (" + colMin + ")");
			}
			if (colMax > 255)
			{
				throw new ArgumentOutOfRangeException("colMax", "must be <= " + 255);
			}
			Util.ValidateUShort(rowMin, "rowMin");
			Util.ValidateUShort(rowMax, "rowMax");
			Util.ValidateUShort(colMin, "colMin");
			Util.ValidateUShort(colMax, "colMax");
		}

		// Token: 0x040002A7 RID: 679
		public ushort RowMin;

		// Token: 0x040002A8 RID: 680
		public ushort RowMax;

		// Token: 0x040002A9 RID: 681
		public ushort ColMin;

		// Token: 0x040002AA RID: 682
		public ushort ColMax;
	}
}
