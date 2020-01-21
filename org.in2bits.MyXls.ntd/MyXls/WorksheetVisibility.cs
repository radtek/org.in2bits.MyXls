using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000040 RID: 64
	internal static class WorksheetVisibility
	{
		// Token: 0x0600024C RID: 588 RVA: 0x0000AAF4 File Offset: 0x00009AF4
		internal static byte[] GetBytes(WorksheetVisibilities visibility)
		{
			switch (visibility)
			{
			case WorksheetVisibilities.Default:
				return new byte[1];
			case WorksheetVisibilities.Hidden:
				return new byte[]
				{
					1
				};
			case WorksheetVisibilities.StrongHidden:
				return new byte[]
				{
					2
				};
			default:
				throw new ApplicationException(string.Format("Unexpected WorksheetVisibilities {0}", visibility));
			}
		}
	}
}
