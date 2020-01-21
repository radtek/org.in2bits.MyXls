using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000003 RID: 3
	internal static class WorksheetType
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00001050
		internal static byte[] GetBytes(WorksheetTypes type)
		{
			switch (type)
			{
			case WorksheetTypes.Default:
				return new byte[1];
			case WorksheetTypes.Chart:
				return new byte[]
				{
					2
				};
			case WorksheetTypes.VBModule:
				return new byte[]
				{
					4
				};
			default:
				throw new ApplicationException(string.Format("Unexpected WorksheetTypes {0}", type));
			}
		}
	}
}
