using System;

namespace org.in2bits.MyOle2.Metadata
{
	// Token: 0x0200002C RID: 44
	public class DocumentSummaryInformationSection : MetadataStream.Section
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000642F File Offset: 0x0000542F
		public DocumentSummaryInformationSection()
		{
			base.FormatId = DocumentSummaryInformationSection.FORMAT_ID_SECTION_0;
			this.CodePage = new short?(1252);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006452 File Offset: 0x00005452
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006460 File Offset: 0x00005460
		public short? CodePage
		{
			get
			{
				return (short?)base.GetProperty(1U);
			}
			set
			{
				base.SetProperty(1U, Property.Types.VT_I2, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006470 File Offset: 0x00005470
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000647E File Offset: 0x0000547E
		public string Category
		{
			get
			{
				return (string)base.GetProperty(2U);
			}
			set
			{
				base.SetProperty(2U, Property.Types.VT_LPSTR, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000648A File Offset: 0x0000548A
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006498 File Offset: 0x00005498
		public string PresentationTarget
		{
			get
			{
				return (string)base.GetProperty(3U);
			}
			set
			{
				base.SetProperty(3U, Property.Types.VT_LPSTR, value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x000064A4 File Offset: 0x000054A4
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000064B2 File Offset: 0x000054B2
		public int? BytesProperty
		{
			get
			{
				return (int?)base.GetProperty(4U);
			}
			set
			{
				base.SetProperty(4U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000064C2 File Offset: 0x000054C2
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000064D0 File Offset: 0x000054D0
		public int? Lines
		{
			get
			{
				return (int?)base.GetProperty(5U);
			}
			set
			{
				base.SetProperty(5U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000064E0 File Offset: 0x000054E0
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000064EE File Offset: 0x000054EE
		public int? Paragraphs
		{
			get
			{
				return (int?)base.GetProperty(6U);
			}
			set
			{
				base.SetProperty(6U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000064FE File Offset: 0x000054FE
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000650C File Offset: 0x0000550C
		public int? Slides
		{
			get
			{
				return (int?)base.GetProperty(7U);
			}
			set
			{
				base.SetProperty(7U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000651C File Offset: 0x0000551C
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000652A File Offset: 0x0000552A
		public int? Notes
		{
			get
			{
				return (int?)base.GetProperty(8U);
			}
			set
			{
				base.SetProperty(8U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000653A File Offset: 0x0000553A
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006549 File Offset: 0x00005549
		public int? HiddenSlides
		{
			get
			{
				return (int?)base.GetProperty(9U);
			}
			set
			{
				base.SetProperty(9U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000655A File Offset: 0x0000555A
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006569 File Offset: 0x00005569
		public int? MmClips
		{
			get
			{
				return (int?)base.GetProperty(10U);
			}
			set
			{
				base.SetProperty(10U, Property.Types.VT_I4, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000657A File Offset: 0x0000557A
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00006589 File Offset: 0x00005589
		public string Manager
		{
			get
			{
				return (string)base.GetProperty(14U);
			}
			set
			{
				base.SetProperty(14U, Property.Types.VT_LPSTR, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006596 File Offset: 0x00005596
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x000065A5 File Offset: 0x000055A5
		public string Company
		{
			get
			{
				return (string)base.GetProperty(15U);
			}
			set
			{
				base.SetProperty(15U, Property.Types.VT_LPSTR, value);
			}
		}

		// Token: 0x040001AD RID: 429
		private const uint ID_DICTIONARY = 0U;

		// Token: 0x040001AE RID: 430
		private const uint ID_CODEPAGE = 1U;

		// Token: 0x040001AF RID: 431
		private const uint ID_CATEGORY = 2U;

		// Token: 0x040001B0 RID: 432
		private const uint ID_PRESENTATION_TARGET = 3U;

		// Token: 0x040001B1 RID: 433
		private const uint ID_BYTES = 4U;

		// Token: 0x040001B2 RID: 434
		private const uint ID_LINES = 5U;

		// Token: 0x040001B3 RID: 435
		private const uint ID_PARAGRAPHS = 6U;

		// Token: 0x040001B4 RID: 436
		private const uint ID_SLIDES = 7U;

		// Token: 0x040001B5 RID: 437
		private const uint ID_NOTES = 8U;

		// Token: 0x040001B6 RID: 438
		private const uint ID_HIDDEN_SLIDES = 9U;

		// Token: 0x040001B7 RID: 439
		private const uint ID_MM_CLIPS = 10U;

		// Token: 0x040001B8 RID: 440
		private const uint ID_SCALE_CROP = 11U;

		// Token: 0x040001B9 RID: 441
		private const uint ID_HEADING_PAIRS = 12U;

		// Token: 0x040001BA RID: 442
		private const uint ID_TITLES_OF_PARTS = 13U;

		// Token: 0x040001BB RID: 443
		private const uint ID_MANAGER = 14U;

		// Token: 0x040001BC RID: 444
		private const uint ID_COMPANY = 15U;

		// Token: 0x040001BD RID: 445
		private const uint ID_LINKS_UP_TO_DATE = 16U;

		// Token: 0x040001BE RID: 446
		private static readonly byte[] FORMAT_ID_SECTION_0 = new byte[]
		{
			2,
			213,
			205,
			213,
			156,
			46,
			27,
			16,
			147,
			151,
			8,
			0,
			43,
			44,
			249,
			174
		};
	}
}
