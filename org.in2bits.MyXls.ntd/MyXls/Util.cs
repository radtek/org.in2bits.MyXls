using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000010 RID: 16
	internal static class Util
	{
		// Token: 0x06000041 RID: 65 RVA: 0x0000344A File Offset: 0x0000244A
		internal static void ValidateUShort(int theInt, string fieldName)
		{
			if (theInt < 0 || theInt > 65535)
			{
				throw new ArgumentException(string.Format("{0} value {1} must be between 1 and {2}", fieldName, theInt, 65534));
			}
		}
	}
}
