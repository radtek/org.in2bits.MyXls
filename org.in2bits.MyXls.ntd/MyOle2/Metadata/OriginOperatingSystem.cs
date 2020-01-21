using System;

namespace org.in2bits.MyOle2.Metadata
{
	// Token: 0x02000012 RID: 18
	internal static class OriginOperatingSystem
	{
		// Token: 0x06000042 RID: 66 RVA: 0x0000347C File Offset: 0x0000247C
		internal static byte[] GetBytes(OriginOperatingSystems system)
		{
			switch (system)
			{
			case OriginOperatingSystems.Win16:
				return new byte[2];
			case OriginOperatingSystems.Macintosh:
			{
				byte[] array = new byte[2];
				array[0] = 1;
				return array;
			}
			case OriginOperatingSystems.Win32:
			{
				byte[] array2 = new byte[2];
				array2[0] = 2;
				return array2;
			}
			default:
				throw new ArgumentException(string.Format("unexpected value {0}", system.ToString()), "system");
			}
		}
	}
}
