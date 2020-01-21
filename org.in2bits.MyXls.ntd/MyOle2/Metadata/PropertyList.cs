using System;
using System.Collections.Generic;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2.Metadata
{
	// Token: 0x02000024 RID: 36
	public class PropertyList
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000056C3 File Offset: 0x000046C3
		internal PropertyList()
		{
		}

		// Token: 0x17000036 RID: 54
		public Property this[uint id]
		{
			get
			{
				return this._properties[id];
			}
			set
			{
				this.Add(value, true);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000056EE File Offset: 0x000046EE
		public uint Count
		{
			get
			{
				return (uint)this._properties.Count;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000056FC File Offset: 0x000046FC
		public void Add(Property property, bool overwrite)
		{
			if (!overwrite && this._properties.ContainsKey(property.Id))
			{
				throw new ApplicationException(string.Format("Can't overwrite existing property with id {0}", property.Id));
			}
			this._properties[property.Id] = property;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000574C File Offset: 0x0000474C
		internal Bytes Bytes
		{
			get
			{
				Bytes bytes = new Bytes();
				Bytes bytes2 = new Bytes();
				int num = 8 + 8 * this._properties.Count;
				foreach (uint key in this._properties.Keys)
				{
					Property property = this._properties[key];
					Bytes bytes3 = property.Bytes;
					bytes2.Append(BitConverter.GetBytes(property.Id));
					bytes2.Append(BitConverter.GetBytes((uint)num));
					num += bytes3.Length;
					bytes.Append(bytes3);
				}
				Bytes bytes4 = new Bytes();
				bytes4.Append(bytes2);
				bytes4.Append(bytes);
				return bytes4;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000581C File Offset: 0x0000481C
		public bool ContainsKey(uint id)
		{
			return this._properties.ContainsKey(id);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000582A File Offset: 0x0000482A
		public bool Remove(uint id)
		{
			return this._properties.Remove(id);
		}

		// Token: 0x04000183 RID: 387
		private Dictionary<uint, Property> _properties = new Dictionary<uint, Property>();
	}
}
