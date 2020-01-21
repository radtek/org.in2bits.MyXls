using System;

namespace org.in2bits.MyXls
{
	// Token: 0x0200002D RID: 45
	public class Color : ICloneable
	{
		// Token: 0x060000EA RID: 234 RVA: 0x000065E1 File Offset: 0x000055E1
		internal Color(byte red, byte green, byte blue)
		{
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
			this.Id = null;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000660A File Offset: 0x0000560A
		internal Color(ushort id)
		{
			this.Red = 0;
			this.Green = 0;
			this.Blue = 0;
			this.Id = new ushort?(id);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006634 File Offset: 0x00005634
		public override bool Equals(object obj)
		{
			Color color = (Color)obj;
			if (this.Id != null || color.Id != null)
			{
				ushort? id = this.Id;
				ushort valueOrDefault = id.GetValueOrDefault();
				ushort? id2 = color.Id;
				return valueOrDefault == id2.GetValueOrDefault() && id != null == (id2 != null);
			}
			return this.Red == color.Red && this.Green == color.Green && this.Blue == color.Blue;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000066C0 File Offset: 0x000056C0
		public object Clone()
		{
			return new Color(this.Red, this.Green, this.Blue)
			{
				Id = this.Id
			};
		}

		// Token: 0x040001BF RID: 447
		internal byte Red;

		// Token: 0x040001C0 RID: 448
		internal byte Green;

		// Token: 0x040001C1 RID: 449
		internal byte Blue;

		// Token: 0x040001C2 RID: 450
		internal ushort? Id;
	}
}
