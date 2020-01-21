using System;

namespace org.in2bits.MyXls
{
	// Token: 0x02000026 RID: 38
	internal class FormulaRecord : Record
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00005C13 File Offset: 0x00004C13
		internal FormulaRecord(Record formulaRecord, Record stringRecord)
		{
			this._rid = formulaRecord.RID;
			this._data = formulaRecord.Data;
			this._continues = formulaRecord.Continues;
			this.StringRecord = stringRecord;
		}

		// Token: 0x0400018B RID: 395
		internal Record StringRecord;
	}
}
