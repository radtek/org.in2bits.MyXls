using System;
using System.Collections.Generic;

namespace org.in2bits.MyXls
{
	// Token: 0x0200004D RID: 77
	internal class Palette
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000D63B File Offset: 0x0000C63B
		internal Palette(Workbook workbook)
		{
			this._workbook = workbook;
			this.InitDefaultPalette();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D668 File Offset: 0x0000C668
		private void InitDefaultPalette()
		{
			this._egaColors.Add(Colors.EgaBlack);
			this._egaColors.Add(Colors.EgaWhite);
			this._egaColors.Add(Colors.EgaRed);
			this._egaColors.Add(Colors.EgaGreen);
			this._egaColors.Add(Colors.EgaBlue);
			this._egaColors.Add(Colors.EgaYellow);
			this._egaColors.Add(Colors.EgaMagenta);
			this._egaColors.Add(Colors.EgaCyan);
			this._colors.Add(Colors.Black);
			this._colors.Add(Colors.White);
			this._colors.Add(Colors.Red);
			this._colors.Add(Colors.Green);
			this._colors.Add(Colors.Blue);
			this._colors.Add(Colors.Yellow);
			this._colors.Add(Colors.Magenta);
			this._colors.Add(Colors.Cyan);
			this._colors.Add(Colors.DarkRed);
			this._colors.Add(Colors.DarkGreen);
			this._colors.Add(Colors.DarkBlue);
			this._colors.Add(Colors.Olive);
			this._colors.Add(Colors.Purple);
			this._colors.Add(Colors.Teal);
			this._colors.Add(Colors.Silver);
			this._colors.Add(Colors.Grey);
			this._colors.Add(Colors.Default18);
			this._colors.Add(Colors.Default19);
			this._colors.Add(Colors.Default1A);
			this._colors.Add(Colors.Default1B);
			this._colors.Add(Colors.Default1C);
			this._colors.Add(Colors.Default1D);
			this._colors.Add(Colors.Default1E);
			this._colors.Add(Colors.Default1F);
			this._colors.Add(Colors.Default20);
			this._colors.Add(Colors.Default21);
			this._colors.Add(Colors.Default22);
			this._colors.Add(Colors.Default23);
			this._colors.Add(Colors.Default24);
			this._colors.Add(Colors.Default25);
			this._colors.Add(Colors.Default26);
			this._colors.Add(Colors.Default27);
			this._colors.Add(Colors.Default28);
			this._colors.Add(Colors.Default29);
			this._colors.Add(Colors.Default2A);
			this._colors.Add(Colors.Default2B);
			this._colors.Add(Colors.Default2C);
			this._colors.Add(Colors.Default2D);
			this._colors.Add(Colors.Default2E);
			this._colors.Add(Colors.Default2F);
			this._colors.Add(Colors.Default30);
			this._colors.Add(Colors.Default31);
			this._colors.Add(Colors.Default32);
			this._colors.Add(Colors.Default33);
			this._colors.Add(Colors.Default34);
			this._colors.Add(Colors.Default35);
			this._colors.Add(Colors.Default36);
			this._colors.Add(Colors.Default37);
			this._colors.Add(Colors.Default38);
			this._colors.Add(Colors.Default39);
			this._colors.Add(Colors.Default3A);
			this._colors.Add(Colors.Default3B);
			this._colors.Add(Colors.Default3C);
			this._colors.Add(Colors.Default3D);
			this._colors.Add(Colors.Default3E);
			this._colors.Add(Colors.Default3F);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000DA78 File Offset: 0x0000CA78
		internal ushort GetIndex(Color color)
		{
			if (this._colors.Contains(color))
			{
				return (ushort)(this._colors.IndexOf(color) + this._egaColors.Count);
			}
			if (this._egaColors.Contains(color))
			{
				return (ushort)this._egaColors.IndexOf(color);
			}
			if (color.Id == null)
			{
				throw new ArgumentOutOfRangeException("Could not locate color in palette");
			}
			return color.Id.Value;
		}

		// Token: 0x040002A4 RID: 676
		private Workbook _workbook;

		// Token: 0x040002A5 RID: 677
		private List<Color> _egaColors = new List<Color>();

		// Token: 0x040002A6 RID: 678
		private List<Color> _colors = new List<Color>();
	}
}
