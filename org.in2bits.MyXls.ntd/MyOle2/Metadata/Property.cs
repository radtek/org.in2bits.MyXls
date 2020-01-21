using System;
using System.Text;
using org.in2bits.MyXls.ByteUtil;

namespace org.in2bits.MyOle2.Metadata
{
	// Token: 0x0200000B RID: 11
	public class Property
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002E8D File Offset: 0x00001E8D
		public Property(uint id, Property.Types type, object value)
		{
			this._id = id;
			this._type = type;
			this._value = value;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002EAA File Offset: 0x00001EAA
		public Property.Types Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002EB2 File Offset: 0x00001EB2
		public uint Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002EBA File Offset: 0x00001EBA
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002EC2 File Offset: 0x00001EC2
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002ECC File Offset: 0x00001ECC
		internal Bytes Bytes
		{
			get
			{
				if (this._value == null)
				{
					throw new ApplicationException(string.Format("The Value of a Property can't be null - Property ID {0}", this._id));
				}
				Bytes bytes = new Bytes();
				bytes.Append(BitConverter.GetBytes((uint)this._type));
				bytes.Append(Property.GetBytes(this));
				return bytes;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F20 File Offset: 0x00001F20
		private static Bytes GetBytes(Property property)
		{
			Bytes bytes;
			switch (property.Type)
			{
			case Property.Types.VT_EMPTY:
			case Property.Types.VT_NULL:
			case Property.Types.VT_R4:
			case Property.Types.VT_R8:
			case Property.Types.VT_CY:
			case Property.Types.VT_DATE:
			case Property.Types.VT_BSTR:
			case Property.Types.VT_DISPATCH:
			case Property.Types.VT_ERROR:
			case Property.Types.VT_BOOL:
			case Property.Types.VT_VARIANT:
			case Property.Types.VT_UNKNOWN:
			case Property.Types.VT_DECIMAL:
			case Property.Types.VT_I1:
			case Property.Types.VT_UI1:
			case Property.Types.VT_UI2:
			case Property.Types.VT_UI4:
			case Property.Types.VT_I8:
			case Property.Types.VT_UI8:
			case Property.Types.VT_INT:
			case Property.Types.VT_UINT:
			case Property.Types.VT_VOID:
			case Property.Types.VT_HRESULT:
			case Property.Types.VT_PTR:
			case Property.Types.VT_SAFEARRAY:
			case Property.Types.VT_CARRAY:
			case Property.Types.VT_USERDEFINED:
			case Property.Types.VT_LPWSTR:
			case Property.Types.VT_BLOB:
			case Property.Types.VT_STREAM:
			case Property.Types.VT_STORAGE:
			case Property.Types.VT_STREAMED_OBJECT:
			case Property.Types.VT_STORED_OBJECT:
			case Property.Types.VT_BLOB_OBJECT:
			case Property.Types.VT_CF:
			case Property.Types.VT_CLSID:
			case Property.Types.VT_VECTOR:
			case Property.Types.VT_ARRAY:
			case Property.Types.VT_BYREF:
			case Property.Types.VT_RESERVED:
			case Property.Types.VT_ILLEGAL:
			case Property.Types.VT_ILLEGALMASKED:
			case Property.Types.VT_TYPEMASK:
				throw new NotSupportedException(string.Format("Property Type {0}", property.Type));
			case Property.Types.VT_I2:
				bytes = Property.GetBytesI2(property.Value);
				goto IL_1BD;
			case Property.Types.VT_I4:
				bytes = Property.GetBytesI4(property.Value);
				goto IL_1BD;
			case Property.Types.VT_LPSTR:
				bytes = Property.GetBytesLPSTR(property.Value);
				goto IL_1BD;
			case Property.Types.VT_FILETIME:
				bytes = Property.GetBytesFILETIME(property.Value);
				goto IL_1BD;
			}
			throw new ApplicationException(string.Format("unexpected value {0}", property.Type));
			IL_1BD:
			int num = bytes.Length % 4;
			if (num != 0)
			{
				bytes.Append(new byte[4 - num]);
			}
			return bytes;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003108 File Offset: 0x00002108
		private static Bytes GetBytesBOOL(object value)
		{
			int value2 = ((bool)value) ? -1 : 0;
			return new Bytes(BitConverter.GetBytes(value2));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003130 File Offset: 0x00002130
		private static Bytes GetBytesFILETIME(object value)
		{
			return new Bytes(BitConverter.GetBytes(((DateTime)value).ToFileTime()));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003158 File Offset: 0x00002158
		private static Bytes GetBytesI4(object value)
		{
			int value2 = (int)value;
			return new Bytes(BitConverter.GetBytes(value2));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003178 File Offset: 0x00002178
		private static Bytes GetBytesI2(object value)
		{
			short value2 = (short)value;
			return new Bytes(BitConverter.GetBytes(value2));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003198 File Offset: 0x00002198
		private static Bytes GetBytesLPSTR(object value)
		{
			Bytes bytes = new Bytes();
			string text = value as string;
			Encoder encoder = Encoding.ASCII.GetEncoder();
			char[] array = text.ToCharArray();
			int num = array.Length + 1;
			num += num % 4;
			byte[] array2 = new byte[num];
			encoder.GetBytes(array, 0, array.Length, array2, 0, true);
			bytes.Append(BitConverter.GetBytes((uint)num));
			bytes.Append(array2);
			return bytes;
		}

		// Token: 0x0400001B RID: 27
		private object _value;

		// Token: 0x0400001C RID: 28
		private Property.Types _type;

		// Token: 0x0400001D RID: 29
		private uint _id;

		// Token: 0x0200000C RID: 12
		public enum Types : uint
		{
			// Token: 0x0400001F RID: 31
			VT_EMPTY,
			// Token: 0x04000020 RID: 32
			VT_NULL,
			// Token: 0x04000021 RID: 33
			VT_I2,
			// Token: 0x04000022 RID: 34
			VT_I4,
			// Token: 0x04000023 RID: 35
			VT_R4,
			// Token: 0x04000024 RID: 36
			VT_R8,
			// Token: 0x04000025 RID: 37
			VT_CY,
			// Token: 0x04000026 RID: 38
			VT_DATE,
			// Token: 0x04000027 RID: 39
			VT_BSTR,
			// Token: 0x04000028 RID: 40
			VT_DISPATCH,
			// Token: 0x04000029 RID: 41
			VT_ERROR,
			// Token: 0x0400002A RID: 42
			VT_BOOL,
			// Token: 0x0400002B RID: 43
			VT_VARIANT,
			// Token: 0x0400002C RID: 44
			VT_UNKNOWN,
			// Token: 0x0400002D RID: 45
			VT_DECIMAL,
			// Token: 0x0400002E RID: 46
			VT_I1 = 16U,
			// Token: 0x0400002F RID: 47
			VT_UI1,
			// Token: 0x04000030 RID: 48
			VT_UI2,
			// Token: 0x04000031 RID: 49
			VT_UI4,
			// Token: 0x04000032 RID: 50
			VT_I8,
			// Token: 0x04000033 RID: 51
			VT_UI8,
			// Token: 0x04000034 RID: 52
			VT_INT,
			// Token: 0x04000035 RID: 53
			VT_UINT,
			// Token: 0x04000036 RID: 54
			VT_VOID,
			// Token: 0x04000037 RID: 55
			VT_HRESULT,
			// Token: 0x04000038 RID: 56
			VT_PTR,
			// Token: 0x04000039 RID: 57
			VT_SAFEARRAY,
			// Token: 0x0400003A RID: 58
			VT_CARRAY,
			// Token: 0x0400003B RID: 59
			VT_USERDEFINED,
			// Token: 0x0400003C RID: 60
			VT_LPSTR,
			// Token: 0x0400003D RID: 61
			VT_LPWSTR,
			// Token: 0x0400003E RID: 62
			VT_FILETIME = 64U,
			// Token: 0x0400003F RID: 63
			VT_BLOB,
			// Token: 0x04000040 RID: 64
			VT_STREAM,
			// Token: 0x04000041 RID: 65
			VT_STORAGE,
			// Token: 0x04000042 RID: 66
			VT_STREAMED_OBJECT,
			// Token: 0x04000043 RID: 67
			VT_STORED_OBJECT,
			// Token: 0x04000044 RID: 68
			VT_BLOB_OBJECT,
			// Token: 0x04000045 RID: 69
			VT_CF,
			// Token: 0x04000046 RID: 70
			VT_CLSID,
			// Token: 0x04000047 RID: 71
			VT_VECTOR,
			// Token: 0x04000048 RID: 72
			VT_ARRAY,
			// Token: 0x04000049 RID: 73
			VT_BYREF,
			// Token: 0x0400004A RID: 74
			VT_RESERVED,
			// Token: 0x0400004B RID: 75
			VT_ILLEGAL,
			// Token: 0x0400004C RID: 76
			VT_ILLEGALMASKED,
			// Token: 0x0400004D RID: 77
			VT_TYPEMASK
		}
	}
}
