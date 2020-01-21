using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using org.in2bits.MyOle2;
using org.in2bits.MyOle2.Metadata;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyXls
{
	// Token: 0x02000019 RID: 25
	public class XlsDocument
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000045DC File Offset: 0x000035DC
		public XlsDocument()
		{
			this._forceStandardOle2Stream = false;
			this._isLittleEndian = true;
			this._ole2Doc = new Ole2Document();
			this.SetOleDefaults();
			this._summaryInformation = new SummaryInformationSection();
			this._documentSummaryInformation = new DocumentSummaryInformationSection();
			this._workbook = new Workbook(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004642 File Offset: 0x00003642
		public XlsDocument(string fileName) : this(fileName, null)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000464C File Offset: 0x0000364C
		internal XlsDocument(string fileName, Workbook.BytesReadCallback workbookBytesReadCallback)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException("Can't be null or Empty", "fileName");
			}
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException("Excel File not found", fileName);
			}
			this._ole2Doc = new Ole2Document();
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				this._ole2Doc.Load(fileStream);
			}
			this._workbook = new Workbook(this, this._ole2Doc.Streams[this._ole2Doc.Streams.GetIndex(org.in2bits.MyOle2.Directory.Biff8Workbook)].Bytes, workbookBytesReadCallback);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004718 File Offset: 0x00003718
		public XlsDocument(System.IO.Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("Can't be null", "steam");
			}
			this._ole2Doc = new Ole2Document();
			this._ole2Doc.Load(stream);
			this._workbook = new Workbook(this, this._ole2Doc.Streams[this._ole2Doc.Streams.GetIndex(org.in2bits.MyOle2.Directory.Biff8Workbook)].Bytes, null);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000047A4 File Offset: 0x000037A4
		public XlsDocument(DataSet dataSet) : this()
		{
			this.FileName = dataSet.DataSetName;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				Worksheet worksheet = this.Workbook.Worksheets.Add(dataTable.TableName);
				worksheet.Write(dataTable, 1, 1);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004828 File Offset: 0x00003828
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004830 File Offset: 0x00003830
		public string FileName
		{
			get
			{
				return this._fileName;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("FileName cannot be null or Empty");
				}
				if (string.Compare(value.Substring(value.Length - 4), ".xls", true) != 0)
				{
					value = string.Format("{0}.xls", value);
				}
				this._fileName = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000487F File Offset: 0x0000387F
		private void SetOleDefaults()
		{
			this._ole2Doc.DocUID = new byte[16];
			this._ole2Doc.SectorSize = 9;
			this._ole2Doc.ShortSectorSize = 6;
			this._ole2Doc.StandardStreamMinBytes = 4096U;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000048BC File Offset: 0x000038BC
		public Ole2Document OLEDoc
		{
			get
			{
				return this._ole2Doc;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000048C4 File Offset: 0x000038C4
		public Workbook Workbook
		{
			get
			{
				return this._workbook;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000048CC File Offset: 0x000038CC
		public Bytes Bytes
		{
			get
			{
				this._ole2Doc.Streams.AddNamed(this._workbook.Bytes, BIFF8.NameWorkbook, true);
				MetadataStream metadataStream = new MetadataStream(this._ole2Doc);
				metadataStream.Sections.Add(this._summaryInformation);
				this._ole2Doc.Streams.AddNamed(this.GetStandardOLE2Stream(metadataStream.Bytes), BIFF8.NameSummaryInformation, true);
				MetadataStream metadataStream2 = new MetadataStream(this._ole2Doc);
				metadataStream2.Sections.Add(this._documentSummaryInformation);
				this._ole2Doc.Streams.AddNamed(this.GetStandardOLE2Stream(metadataStream2.Bytes), BIFF8.NameDocumentSummaryInformation, true);
				return this._ole2Doc.Bytes;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004986 File Offset: 0x00003986
		public SummaryInformationSection SummaryInformation
		{
			get
			{
				return this._summaryInformation;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000498E File Offset: 0x0000398E
		public DocumentSummaryInformationSection DocumentSummaryInformation
		{
			get
			{
				return this._documentSummaryInformation;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004996 File Offset: 0x00003996
		private static string GetContentDisposition(XlsDocument.SendMethods sendMethod)
		{
			if (sendMethod == XlsDocument.SendMethods.Attachment)
			{
				return "attachment";
			}
			if (sendMethod == XlsDocument.SendMethods.Inline)
			{
				return "inline";
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004A58 File Offset: 0x00003A58
		public void Save(bool overwrite)
		{
			this.Save(null, overwrite);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004A62 File Offset: 0x00003A62
		public void Save()
		{
			this.Save(null, false);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004A6C File Offset: 0x00003A6C
		public void Save(string path)
		{
			this.Save(path, false);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004A78 File Offset: 0x00003A78
		public void Save(string path, bool overwrite)
		{
			path = (path ?? Environment.CurrentDirectory);
			string path2 = Path.Combine(path, this.FileName);
			using (FileStream fileStream = new FileStream(path2, overwrite ? FileMode.Create : FileMode.CreateNew))
			{
				this.Bytes.WriteToStream(fileStream);
				fileStream.Flush();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004ADC File Offset: 0x00003ADC
		public void Save(System.IO.Stream outStream)
		{
			this.Bytes.WriteToStream(outStream);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004AEA File Offset: 0x00003AEA
		public bool IsLittleEndian
		{
			get
			{
				return this._isLittleEndian;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004AF2 File Offset: 0x00003AF2
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00004AFA File Offset: 0x00003AFA
		public bool ForceStandardOle2Stream
		{
			get
			{
				return this._forceStandardOle2Stream;
			}
			set
			{
				this._forceStandardOle2Stream = value;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B04 File Offset: 0x00003B04
		internal Bytes GetStandardOLE2Stream(Bytes bytes)
		{
			uint num = this._ole2Doc.StandardStreamMinBytes;
			uint num2;
			num = (num2 = (uint)(bytes.Length % (int)num));
			if (num2 < num)
			{
				bytes.Append(new byte[num2]);
			}
			return bytes;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B3C File Offset: 0x00003B3C
		internal static Bytes GetUnicodeString(string text, int lengthBits)
		{
			int num = (lengthBits == 8) ? 255 : 65535;
			byte[] byteArray = new byte[0];
			byte[] array = new byte[0];
			int length = text.Length;
			if (length > num)
			{
				text = text.Substring(0, num);
			}
			if (num == 255)
			{
				byteArray = new byte[]
				{
					(byte)text.Length
				};
			}
			else if (num == 65535)
			{
				byteArray = BitConverter.GetBytes((ushort)text.Length);
			}
			byte[] byteArray2;
			if (XlsDocument.IsCompressible(text))
			{
				byteArray2 = new byte[1];
				char[] array2 = text.ToCharArray();
				array = new byte[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array[i] = (byte)array2[i];
				}
			}
			else
			{
				byteArray2 = new byte[]
				{
					1
				};
			}
			Bytes bytes = new Bytes();
			bytes.Append(byteArray);
			bytes.Append(byteArray2);
			if (array.Length > 0)
			{
				bytes.Append(array);
			}
			else
			{
				bytes.Append(Encoding.Unicode.GetBytes(text));
			}
			return bytes;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004C44 File Offset: 0x00003C44
		private static bool IsCompressible(string text)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(text);
			for (int i = 1; i < bytes.Length; i += 2)
			{
				if (bytes[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004C73 File Offset: 0x00003C73
		public XF NewXF()
		{
			return new XF(this);
		}

		// Token: 0x04000107 RID: 263
		private readonly Ole2Document _ole2Doc;

		// Token: 0x04000108 RID: 264
		private readonly Workbook _workbook;

		// Token: 0x04000109 RID: 265
		private readonly SummaryInformationSection _summaryInformation;

		// Token: 0x0400010A RID: 266
		private readonly DocumentSummaryInformationSection _documentSummaryInformation;

		// Token: 0x0400010B RID: 267
		private string _fileName = "Book1.xls";

		// Token: 0x0400010C RID: 268
		private bool _isLittleEndian = true;

		// Token: 0x0400010D RID: 269
		private bool _forceStandardOle2Stream;

		// Token: 0x0200001A RID: 26
		public enum SendMethods
		{
			// Token: 0x0400010F RID: 271
			Inline,
			// Token: 0x04000110 RID: 272
			Attachment
		}
	}
}
