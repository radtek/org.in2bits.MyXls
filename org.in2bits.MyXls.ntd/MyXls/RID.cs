using System;
using System.Collections.Generic;
using System.Reflection;

namespace org.in2bits.MyXls
{
	// Token: 0x02000018 RID: 24
	internal static class RID
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003BBC File Offset: 0x00002BBC
		static RID()
		{
			byte[] array = new byte[2];
			RID.Empty = array;
			RID.ARRAY = new byte[]
			{
				33,
				2
			};
			byte[] array2 = new byte[2];
			array2[0] = 64;
			RID.BACKUP = array2;
			byte[] array3 = new byte[2];
			array3[0] = 233;
			RID.BITMAP = array3;
			RID.BLANK = new byte[]
			{
				1,
				2
			};
			RID.BOF = new byte[]
			{
				9,
				8
			};
			byte[] array4 = new byte[2];
			array4[0] = 218;
			RID.BOOKBOOL = array4;
			RID.BOOLERR = new byte[]
			{
				5,
				2
			};
			byte[] array5 = new byte[2];
			array5[0] = 41;
			RID.BOTTOMMARGIN = array5;
			byte[] array6 = new byte[2];
			array6[0] = 133;
			RID.BOUNDSHEET = array6;
			byte[] array7 = new byte[2];
			array7[0] = 12;
			RID.CALCCOUNT = array7;
			byte[] array8 = new byte[2];
			array8[0] = 13;
			RID.CALCMODE = array8;
			byte[] array9 = new byte[2];
			array9[0] = 66;
			RID.CODEPAGE = array9;
			byte[] array10 = new byte[2];
			array10[0] = 125;
			RID.COLINFO = array10;
			RID.CONDFMT = new byte[]
			{
				176,
				1
			};
			byte[] array11 = new byte[2];
			array11[0] = 60;
			RID.CONTINUE = array11;
			byte[] array12 = new byte[2];
			array12[0] = 140;
			RID.COUNTRY = array12;
			byte[] array13 = new byte[2];
			array13[0] = 90;
			RID.CRN = array13;
			byte[] array14 = new byte[2];
			array14[0] = 34;
			RID.DATEMODE = array14;
			byte[] array15 = new byte[2];
			array15[0] = 215;
			RID.DBCELL = array15;
			byte[] array16 = new byte[2];
			array16[0] = 81;
			RID.DCONREF = array16;
			RID.DEFAULTROWHEIGHT = new byte[]
			{
				37,
				2
			};
			byte[] array17 = new byte[2];
			array17[0] = 85;
			RID.DEFCOLWIDTH = array17;
			byte[] array18 = new byte[2];
			array18[0] = 16;
			RID.DELTA = array18;
			RID.DIMENSIONS = new byte[]
			{
				0,
				2
			};
			RID.DSF = new byte[]
			{
				97,
				1
			};
			RID.DV = new byte[]
			{
				190,
				1
			};
			RID.DVAL = new byte[]
			{
				178,
				1
			};
			byte[] array19 = new byte[2];
			array19[0] = 10;
			RID.EOF = array19;
			byte[] array20 = new byte[2];
			array20[0] = 35;
			RID.EXTERNNAME = array20;
			byte[] array21 = new byte[2];
			array21[0] = 23;
			RID.EXTERNSHEET = array21;
			byte[] array22 = new byte[2];
			array22[0] = byte.MaxValue;
			RID.EXTSST = array22;
			byte[] array23 = new byte[2];
			array23[0] = 47;
			RID.FILEPASS = array23;
			byte[] array24 = new byte[2];
			array24[0] = 91;
			RID.FILESHARING = array24;
			byte[] array25 = new byte[2];
			array25[0] = 49;
			RID.FONT = array25;
			byte[] array26 = new byte[2];
			array26[0] = 21;
			RID.FOOTER = array26;
			RID.FORMAT = new byte[]
			{
				30,
				4
			};
			byte[] array27 = new byte[2];
			array27[0] = 6;
			RID.FORMULA = array27;
			byte[] array28 = new byte[2];
			array28[0] = 130;
			RID.GRIDSET = array28;
			byte[] array29 = new byte[2];
			array29[0] = 128;
			RID.GUTS = array29;
			byte[] array30 = new byte[2];
			array30[0] = 131;
			RID.HCENTER = array30;
			byte[] array31 = new byte[2];
			array31[0] = 20;
			RID.HEADER = array31;
			byte[] array32 = new byte[2];
			array32[0] = 141;
			RID.HIDEOBJ = array32;
			RID.HLINK = new byte[]
			{
				184,
				1
			};
			byte[] array33 = new byte[2];
			array33[0] = 27;
			RID.HORIZONTALPAGEBREAKS = array33;
			RID.INDEX = new byte[]
			{
				11,
				2
			};
			byte[] array34 = new byte[2];
			array34[0] = 17;
			RID.ITERATION = array34;
			RID.LABEL = new byte[]
			{
				4,
				2
			};
			RID.LABELRANGES = new byte[]
			{
				95,
				1
			};
			byte[] array35 = new byte[2];
			array35[0] = 253;
			RID.LABELSST = array35;
			byte[] array36 = new byte[2];
			array36[0] = 38;
			RID.LEFTMARGIN = array36;
			byte[] array37 = new byte[2];
			array37[0] = 229;
			RID.MERGEDCELLS = array37;
			byte[] array38 = new byte[2];
			array38[0] = 190;
			RID.MULBLANK = array38;
			byte[] array39 = new byte[2];
			array39[0] = 189;
			RID.MULRK = array39;
			byte[] array40 = new byte[2];
			array40[0] = 24;
			RID.NAME = array40;
			byte[] array41 = new byte[2];
			array41[0] = 28;
			RID.NOTE = array41;
			RID.NUMBER = new byte[]
			{
				3,
				2
			};
			byte[] array42 = new byte[2];
			array42[0] = 99;
			RID.OBJECTPROTECT = array42;
			array = new byte[2];
			array[0] = 146;
			RID.PALETTE = array;
			array = new byte[2];
			array[0] = 65;
			RID.PANE = array;
			array = new byte[2];
			array[0] = 19;
			RID.PASSWORD = array;
			array = new byte[2];
			array[0] = 239;
			RID.PHONETIC = array;
			array = new byte[2];
			array[0] = 14;
			RID.PRECISION = array;
			array = new byte[2];
			array[0] = 43;
			RID.PRINTGRIDLINES = array;
			array = new byte[2];
			array[0] = 42;
			RID.PRINTHEADERS = array;
			array = new byte[2];
			array[0] = 18;
			RID.PROTECT = array;
			RID.QUICKTIP = new byte[]
			{
				0,
				8
			};
			RID.RANGEPROTECTION = new byte[]
			{
				104,
				8
			};
			array = new byte[2];
			array[0] = 15;
			RID.REFMODE = array;
			array = new byte[2];
			array[0] = 39;
			RID.RIGHTMARGIN = array;
			RID.RK = new byte[]
			{
				126,
				2
			};
			array = new byte[2];
			array[0] = 214;
			RID.RSTRING = array;
			RID.ROW = new byte[]
			{
				8,
				2
			};
			array = new byte[2];
			array[0] = 95;
			RID.SAVERECALC = array;
			array = new byte[2];
			array[0] = 221;
			RID.SCENPROTECT = array;
			array = new byte[2];
			array[0] = 160;
			RID.SCL = array;
			array = new byte[2];
			array[0] = 29;
			RID.SELECTION = array;
			array = new byte[2];
			array[0] = 161;
			RID.SETUP = array;
			RID.SHEETLAYOUT = new byte[]
			{
				98,
				8
			};
			RID.SHEETPROTECTION = new byte[]
			{
				103,
				8
			};
			RID.SHRFMLA = new byte[]
			{
				188,
				4
			};
			array = new byte[2];
			array[0] = 144;
			RID.SORT = array;
			array = new byte[2];
			array[0] = 252;
			RID.SST = array;
			array = new byte[2];
			array[0] = 153;
			RID.STANDARDWIDTH = array;
			RID.STRING = new byte[]
			{
				7,
				2
			};
			RID.STYLE = new byte[]
			{
				147,
				2
			};
			RID.SUPBOOK = new byte[]
			{
				174,
				1
			};
			RID.TABLEOP = new byte[]
			{
				54,
				2
			};
			array = new byte[2];
			array[0] = 40;
			RID.TOPMARGIN = array;
			array = new byte[2];
			array[0] = 94;
			RID.UNCALCED = array;
			RID.USESELFS = new byte[]
			{
				96,
				1
			};
			array = new byte[2];
			array[0] = 132;
			RID.VCENTER = array;
			array = new byte[2];
			array[0] = 26;
			RID.VERTICALPAGEBREAKS = array;
			array = new byte[2];
			array[0] = 61;
			RID.WINDOW1 = array;
			RID.WINDOW2 = new byte[]
			{
				62,
				2
			};
			array = new byte[2];
			array[0] = 25;
			RID.WINDOWPROTECT = array;
			array = new byte[2];
			array[0] = 92;
			RID.WRITEACCESS = array;
			array = new byte[2];
			array[0] = 134;
			RID.WRITEPROT = array;
			array = new byte[2];
			array[0] = 129;
			RID.WSBOOL = array;
			array = new byte[2];
			array[0] = 89;
			RID.XCT = array;
			array = new byte[2];
			array[0] = 224;
			RID.XF = array;
			RID._names = new Dictionary<byte, Dictionary<byte, string>>();
			RID._rids = new Dictionary<string, byte[]>();
			RID.NAME_MAX_LENGTH = 0;
			foreach (FieldInfo fieldInfo in typeof(RID).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
			{
				if (fieldInfo.FieldType == typeof(byte[]))
				{
					byte[] array43 = (byte[])fieldInfo.GetValue(null);
					if (array43.Length == 2)
					{
						byte key = array43[0];
						if (!RID._names.ContainsKey(key))
						{
							RID._names[key] = new Dictionary<byte, string>();
						}
						RID._names[key][array43[1]] = fieldInfo.Name;
						RID._rids[fieldInfo.Name] = (byte[])fieldInfo.GetValue(null);
						RID.NAME_MAX_LENGTH = Math.Max(RID.NAME_MAX_LENGTH, fieldInfo.Name.Length);
					}
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004548 File Offset: 0x00003548
		internal static string Name(byte[] rid)
		{
			if (RID._names.ContainsKey(rid[0]) && RID._names[rid[0]].ContainsKey(rid[1]))
			{
				return RID._names[rid[0]][rid[1]];
			}
			return string.Format("??? {0:x2} {1:x2}", rid[0], rid[1]);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000045AC File Offset: 0x000035AC
		internal static byte[] ByteArray(byte[] rid)
		{
			string key = RID.Name(rid);
			if (RID._rids.ContainsKey(key))
			{
				return RID._rids[key];
			}
			return rid;
		}

		// Token: 0x0400009F RID: 159
		public static readonly byte[] Empty;

		// Token: 0x040000A0 RID: 160
		internal static readonly byte[] ARRAY;

		// Token: 0x040000A1 RID: 161
		internal static readonly byte[] BACKUP;

		// Token: 0x040000A2 RID: 162
		internal static readonly byte[] BITMAP;

		// Token: 0x040000A3 RID: 163
		internal static readonly byte[] BLANK;

		// Token: 0x040000A4 RID: 164
		internal static readonly byte[] BOF;

		// Token: 0x040000A5 RID: 165
		internal static readonly byte[] BOOKBOOL;

		// Token: 0x040000A6 RID: 166
		internal static readonly byte[] BOOLERR;

		// Token: 0x040000A7 RID: 167
		internal static readonly byte[] BOTTOMMARGIN;

		// Token: 0x040000A8 RID: 168
		internal static readonly byte[] BOUNDSHEET;

		// Token: 0x040000A9 RID: 169
		internal static readonly byte[] CALCCOUNT;

		// Token: 0x040000AA RID: 170
		internal static readonly byte[] CALCMODE;

		// Token: 0x040000AB RID: 171
		internal static readonly byte[] CODEPAGE;

		// Token: 0x040000AC RID: 172
		internal static readonly byte[] COLINFO;

		// Token: 0x040000AD RID: 173
		internal static readonly byte[] CONDFMT;

		// Token: 0x040000AE RID: 174
		internal static readonly byte[] CONTINUE;

		// Token: 0x040000AF RID: 175
		internal static readonly byte[] COUNTRY;

		// Token: 0x040000B0 RID: 176
		internal static readonly byte[] CRN;

		// Token: 0x040000B1 RID: 177
		internal static readonly byte[] DATEMODE;

		// Token: 0x040000B2 RID: 178
		internal static readonly byte[] DBCELL;

		// Token: 0x040000B3 RID: 179
		internal static readonly byte[] DCONREF;

		// Token: 0x040000B4 RID: 180
		internal static readonly byte[] DEFAULTROWHEIGHT;

		// Token: 0x040000B5 RID: 181
		internal static readonly byte[] DEFCOLWIDTH;

		// Token: 0x040000B6 RID: 182
		internal static readonly byte[] DELTA;

		// Token: 0x040000B7 RID: 183
		internal static readonly byte[] DIMENSIONS;

		// Token: 0x040000B8 RID: 184
		internal static readonly byte[] DSF;

		// Token: 0x040000B9 RID: 185
		internal static readonly byte[] DV;

		// Token: 0x040000BA RID: 186
		internal static readonly byte[] DVAL;

		// Token: 0x040000BB RID: 187
		internal static readonly byte[] EOF;

		// Token: 0x040000BC RID: 188
		internal static readonly byte[] EXTERNNAME;

		// Token: 0x040000BD RID: 189
		internal static readonly byte[] EXTERNSHEET;

		// Token: 0x040000BE RID: 190
		internal static readonly byte[] EXTSST;

		// Token: 0x040000BF RID: 191
		internal static readonly byte[] FILEPASS;

		// Token: 0x040000C0 RID: 192
		internal static readonly byte[] FILESHARING;

		// Token: 0x040000C1 RID: 193
		internal static readonly byte[] FONT;

		// Token: 0x040000C2 RID: 194
		internal static readonly byte[] FOOTER;

		// Token: 0x040000C3 RID: 195
		internal static readonly byte[] FORMAT;

		// Token: 0x040000C4 RID: 196
		internal static readonly byte[] FORMULA;

		// Token: 0x040000C5 RID: 197
		internal static readonly byte[] GRIDSET;

		// Token: 0x040000C6 RID: 198
		internal static readonly byte[] GUTS;

		// Token: 0x040000C7 RID: 199
		internal static readonly byte[] HCENTER;

		// Token: 0x040000C8 RID: 200
		internal static readonly byte[] HEADER;

		// Token: 0x040000C9 RID: 201
		internal static readonly byte[] HIDEOBJ;

		// Token: 0x040000CA RID: 202
		internal static readonly byte[] HLINK;

		// Token: 0x040000CB RID: 203
		internal static readonly byte[] HORIZONTALPAGEBREAKS;

		// Token: 0x040000CC RID: 204
		internal static readonly byte[] INDEX;

		// Token: 0x040000CD RID: 205
		internal static readonly byte[] ITERATION;

		// Token: 0x040000CE RID: 206
		internal static readonly byte[] LABEL;

		// Token: 0x040000CF RID: 207
		internal static readonly byte[] LABELRANGES;

		// Token: 0x040000D0 RID: 208
		internal static readonly byte[] LABELSST;

		// Token: 0x040000D1 RID: 209
		internal static readonly byte[] LEFTMARGIN;

		// Token: 0x040000D2 RID: 210
		internal static readonly byte[] MERGEDCELLS;

		// Token: 0x040000D3 RID: 211
		internal static readonly byte[] MULBLANK;

		// Token: 0x040000D4 RID: 212
		internal static readonly byte[] MULRK;

		// Token: 0x040000D5 RID: 213
		internal static readonly byte[] NAME;

		// Token: 0x040000D6 RID: 214
		internal static readonly byte[] NOTE;

		// Token: 0x040000D7 RID: 215
		internal static readonly byte[] NUMBER;

		// Token: 0x040000D8 RID: 216
		internal static readonly byte[] OBJECTPROTECT;

		// Token: 0x040000D9 RID: 217
		internal static readonly byte[] PALETTE;

		// Token: 0x040000DA RID: 218
		internal static readonly byte[] PANE;

		// Token: 0x040000DB RID: 219
		internal static readonly byte[] PASSWORD;

		// Token: 0x040000DC RID: 220
		internal static readonly byte[] PHONETIC;

		// Token: 0x040000DD RID: 221
		internal static readonly byte[] PRECISION;

		// Token: 0x040000DE RID: 222
		internal static readonly byte[] PRINTGRIDLINES;

		// Token: 0x040000DF RID: 223
		internal static readonly byte[] PRINTHEADERS;

		// Token: 0x040000E0 RID: 224
		internal static readonly byte[] PROTECT;

		// Token: 0x040000E1 RID: 225
		internal static readonly byte[] QUICKTIP;

		// Token: 0x040000E2 RID: 226
		internal static readonly byte[] RANGEPROTECTION;

		// Token: 0x040000E3 RID: 227
		internal static readonly byte[] REFMODE;

		// Token: 0x040000E4 RID: 228
		internal static readonly byte[] RIGHTMARGIN;

		// Token: 0x040000E5 RID: 229
		internal static readonly byte[] RK;

		// Token: 0x040000E6 RID: 230
		internal static readonly byte[] RSTRING;

		// Token: 0x040000E7 RID: 231
		internal static readonly byte[] ROW;

		// Token: 0x040000E8 RID: 232
		internal static readonly byte[] SAVERECALC;

		// Token: 0x040000E9 RID: 233
		internal static readonly byte[] SCENPROTECT;

		// Token: 0x040000EA RID: 234
		internal static readonly byte[] SCL;

		// Token: 0x040000EB RID: 235
		internal static readonly byte[] SELECTION;

		// Token: 0x040000EC RID: 236
		internal static readonly byte[] SETUP;

		// Token: 0x040000ED RID: 237
		internal static readonly byte[] SHEETLAYOUT;

		// Token: 0x040000EE RID: 238
		internal static readonly byte[] SHEETPROTECTION;

		// Token: 0x040000EF RID: 239
		internal static readonly byte[] SHRFMLA;

		// Token: 0x040000F0 RID: 240
		internal static readonly byte[] SORT;

		// Token: 0x040000F1 RID: 241
		internal static readonly byte[] SST;

		// Token: 0x040000F2 RID: 242
		internal static readonly byte[] STANDARDWIDTH;

		// Token: 0x040000F3 RID: 243
		internal static readonly byte[] STRING;

		// Token: 0x040000F4 RID: 244
		internal static readonly byte[] STYLE;

		// Token: 0x040000F5 RID: 245
		internal static readonly byte[] SUPBOOK;

		// Token: 0x040000F6 RID: 246
		internal static readonly byte[] TABLEOP;

		// Token: 0x040000F7 RID: 247
		internal static readonly byte[] TOPMARGIN;

		// Token: 0x040000F8 RID: 248
		internal static readonly byte[] UNCALCED;

		// Token: 0x040000F9 RID: 249
		internal static readonly byte[] USESELFS;

		// Token: 0x040000FA RID: 250
		internal static readonly byte[] VCENTER;

		// Token: 0x040000FB RID: 251
		internal static readonly byte[] VERTICALPAGEBREAKS;

		// Token: 0x040000FC RID: 252
		internal static readonly byte[] WINDOW1;

		// Token: 0x040000FD RID: 253
		internal static readonly byte[] WINDOW2;

		// Token: 0x040000FE RID: 254
		internal static readonly byte[] WINDOWPROTECT;

		// Token: 0x040000FF RID: 255
		internal static readonly byte[] WRITEACCESS;

		// Token: 0x04000100 RID: 256
		internal static readonly byte[] WRITEPROT;

		// Token: 0x04000101 RID: 257
		internal static readonly byte[] WSBOOL;

		// Token: 0x04000102 RID: 258
		internal static readonly byte[] XCT;

		// Token: 0x04000103 RID: 259
		internal static readonly byte[] XF;

		// Token: 0x04000104 RID: 260
		private static readonly Dictionary<byte, Dictionary<byte, string>> _names;

		// Token: 0x04000105 RID: 261
		private static readonly Dictionary<string, byte[]> _rids;

		// Token: 0x04000106 RID: 262
		internal static readonly int NAME_MAX_LENGTH;
	}
}
