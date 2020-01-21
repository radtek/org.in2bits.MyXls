using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2.Metadata
{
	// Token: 0x0200001B RID: 27
	public class MetadataStream
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004C7B File Offset: 0x00003C7B
		public MetadataStream(Ole2Document parentDocument)
		{
			this._parentDocument = parentDocument;
			this._header = new MetadataStream.Header(this);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004CA1 File Offset: 0x00003CA1
		public MetadataStream.SectionList Sections
		{
			get
			{
				return this._sectionList;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004CAC File Offset: 0x00003CAC
		public Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				bytes.Append(this._header.Bytes);
				bytes.Append(this._sectionList.Bytes);
				return bytes;
			}
		}

		// Token: 0x04000111 RID: 273
		private Ole2Document _parentDocument;

		// Token: 0x04000112 RID: 274
		private MetadataStream.Header _header;

		// Token: 0x04000113 RID: 275
		private MetadataStream.SectionList _sectionList = new MetadataStream.SectionList();

		// Token: 0x0200001C RID: 28
		public class Header
		{
			// Token: 0x0600008C RID: 140 RVA: 0x00004CE2 File Offset: 0x00003CE2
			internal Header(MetadataStream parent)
			{
				if (parent == null)
				{
					throw new ArgumentNullException("parent");
				}
				this._parent = parent;
			}

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600008D RID: 141 RVA: 0x00004D13 File Offset: 0x00003D13
			// (set) Token: 0x0600008E RID: 142 RVA: 0x00004D1B File Offset: 0x00003D1B
			public OriginOperatingSystems OriginOperatingSystem
			{
				get
				{
					return this._originOperatingSystem;
				}
				set
				{
					this._originOperatingSystem = value;
				}
			}

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x0600008F RID: 143 RVA: 0x00004D24 File Offset: 0x00003D24
			// (set) Token: 0x06000090 RID: 144 RVA: 0x00004D2C File Offset: 0x00003D2C
			public OriginOperatingSystemVersions OriginOperatingSystemVersion
			{
				get
				{
					return this._originOperatingSystemVersion;
				}
				set
				{
					this._originOperatingSystemVersion = value;
				}
			}

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000091 RID: 145 RVA: 0x00004D35 File Offset: 0x00003D35
			// (set) Token: 0x06000092 RID: 146 RVA: 0x00004D3D File Offset: 0x00003D3D
			public byte[] ClassID
			{
				get
				{
					return this._classId;
				}
				set
				{
					this._classId = value;
				}
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00004D48 File Offset: 0x00003D48
			internal Bytes Bytes
			{
				get
				{
					Bytes bytes = new Bytes();
					bytes.Append(new byte[]
					{
						254,
						byte.MaxValue
					});
					Bytes bytes2 = bytes;
					byte[] byteArray = new byte[2];
					bytes2.Append(byteArray);
					bytes.Append(org.in2bits.MyOle2.Metadata.OriginOperatingSystemVersion.GetBytes(this._originOperatingSystemVersion));
					bytes.Append(org.in2bits.MyOle2.Metadata.OriginOperatingSystem.GetBytes(this._originOperatingSystem));
					bytes.Append(this._classId);
					bytes.Append(BitConverter.GetBytes(this._parent.Sections.Count));
					return bytes;
				}
			}

			// Token: 0x04000114 RID: 276
			private OriginOperatingSystems _originOperatingSystem = OriginOperatingSystems.Win32;

			// Token: 0x04000115 RID: 277
			private OriginOperatingSystemVersions _originOperatingSystemVersion;

			// Token: 0x04000116 RID: 278
			private byte[] _classId = new byte[16];

			// Token: 0x04000117 RID: 279
			private MetadataStream _parent;
		}

		// Token: 0x0200001D RID: 29
		public abstract class Section
		{
			// Token: 0x1700002E RID: 46
			// (get) Token: 0x06000094 RID: 148 RVA: 0x00004DD4 File Offset: 0x00003DD4
			internal Bytes Bytes
			{
				get
				{
					Bytes bytes = this.Properties.Bytes;
					uint value = (uint)(8 + bytes.Length);
					Bytes bytes2 = new Bytes();
					bytes2.Append(BitConverter.GetBytes(value));
					bytes2.Append(BitConverter.GetBytes(this.Properties.Count));
					bytes2.Append(bytes);
					return bytes2;
				}
			}

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x06000095 RID: 149 RVA: 0x00004E26 File Offset: 0x00003E26
			// (set) Token: 0x06000096 RID: 150 RVA: 0x00004E2E File Offset: 0x00003E2E
			public byte[] FormatId
			{
				get
				{
					return this._formatId;
				}
				set
				{
					if (value == null || value.Length != 16)
					{
						throw new ArgumentException("Section FormatId must be 16 bytes in length and cannot be null");
					}
					this._formatId = value;
				}
			}

			// Token: 0x17000030 RID: 48
			// (get) Token: 0x06000097 RID: 151 RVA: 0x00004E4C File Offset: 0x00003E4C
			public PropertyList Properties
			{
				get
				{
					return this._properties;
				}
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00004E54 File Offset: 0x00003E54
			protected void SetProperty(uint id, Property.Types type, object value)
			{
				if (value == null)
				{
					if (this.Properties.ContainsKey(id))
					{
						this.Properties.Remove(id);
					}
					return;
				}
				this.Properties.Add(new Property(id, type, value), true);
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00004E89 File Offset: 0x00003E89
			protected object GetProperty(uint id)
			{
				if (!this.Properties.ContainsKey(id))
				{
					return null;
				}
				return this.Properties[id].Value;
			}

			// Token: 0x04000118 RID: 280
			private byte[] _formatId = new byte[16];

			// Token: 0x04000119 RID: 281
			private PropertyList _properties = new PropertyList();
		}

		// Token: 0x0200001E RID: 30
		public class SectionList
		{
			// Token: 0x17000031 RID: 49
			// (get) Token: 0x0600009B RID: 155 RVA: 0x00004ECC File Offset: 0x00003ECC
			internal Bytes Bytes
			{
				get
				{
					Bytes bytes = new Bytes();
					List<Bytes> list = new List<Bytes>();
					int num = 28 + 20 * this._sections.Count;
					foreach (MetadataStream.Section section in this._sections)
					{
						bytes.Append(section.FormatId);
						bytes.Append(BitConverter.GetBytes((uint)num));
						list.Add(section.Bytes);
						num += list[list.Count - 1].Length;
					}
					foreach (Bytes bytes2 in list)
					{
						bytes.Append(bytes2);
					}
					return bytes;
				}
			}

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x0600009C RID: 156 RVA: 0x00004FB4 File Offset: 0x00003FB4
			public uint Count
			{
				get
				{
					return (uint)this._sections.Count;
				}
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00004FC1 File Offset: 0x00003FC1
			public void Add(MetadataStream.Section section)
			{
				this._sections.Add(section);
			}

			// Token: 0x0400011A RID: 282
			private List<MetadataStream.Section> _sections = new List<MetadataStream.Section>();
		}
	}
}
