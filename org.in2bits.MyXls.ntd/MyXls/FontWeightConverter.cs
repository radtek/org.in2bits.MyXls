using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000020 RID: 32
	public static class FontWeightConverter
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004FE4 File Offset: 0x00003FE4
		public static FontWeight Convert(ushort weight)
		{
			FontWeight result = FontWeight.Thin;
			if (weight >= 900)
			{
				result = FontWeight.Heavy;
			}
			else if (weight > 100)
			{
				result = (FontWeight)(Math.Round((double)weight / 100.0) * 100.0);
			}
			return result;
		}
	}
}
