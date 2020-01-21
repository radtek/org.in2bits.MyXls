using System;

namespace org.in2bits.MyXls
{
	// Token: 0x0200000F RID: 15
	public static class BIFF8
	{
		// Token: 0x04000081 RID: 129
		public const ushort MaxRows = 65535;

		// Token: 0x04000082 RID: 130
		public const ushort MaxCols = 255;

		// Token: 0x04000083 RID: 131
		public const ushort MaxBytesPerRecord = 8228;

		// Token: 0x04000084 RID: 132
		public const ushort MaxDataBytesPerRecord = 8224;

		// Token: 0x04000085 RID: 133
		public const ushort MaxCharactersPerCell = 32767;

		// Token: 0x04000086 RID: 134
		public static readonly byte[] NameWorkbook = new byte[]
		{
			87,
			0,
			111,
			0,
			114,
			0,
			107,
			0,
			98,
			0,
			111,
			0,
			111,
			0,
			107,
			0,
			0,
			0
		};

		// Token: 0x04000087 RID: 135
		public static readonly byte[] NameSummaryInformation = new byte[]
		{
			5,
			0,
			83,
			0,
			117,
			0,
			109,
			0,
			109,
			0,
			97,
			0,
			114,
			0,
			121,
			0,
			73,
			0,
			110,
			0,
			102,
			0,
			111,
			0,
			114,
			0,
			109,
			0,
			97,
			0,
			116,
			0,
			105,
			0,
			111,
			0,
			110,
			0,
			0,
			0
		};

		// Token: 0x04000088 RID: 136
		public static readonly byte[] NameDocumentSummaryInformation = new byte[]
		{
			5,
			0,
			68,
			0,
			111,
			0,
			99,
			0,
			117,
			0,
			109,
			0,
			101,
			0,
			110,
			0,
			116,
			0,
			83,
			0,
			117,
			0,
			109,
			0,
			109,
			0,
			97,
			0,
			114,
			0,
			121,
			0,
			73,
			0,
			110,
			0,
			102,
			0,
			111,
			0,
			114,
			0,
			109,
			0,
			97,
			0,
			116,
			0,
			105,
			0,
			111,
			0,
			110,
			0,
			0,
			0
		};
	}
}
