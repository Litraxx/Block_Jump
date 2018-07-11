using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Block_Jumps
{
	public partial class Block_Jump : Form
	{
		public Block_Jump()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void ImageToLevel(string levelFile)
		{
			//Einlesen des Bildes per File
			Image img = Image.FromFile(levelFile);

			//Kopie des Bildes zu Bitmap
			Bitmap map = new Bitmap(img);

			//Pixelarray
			Color[,] pixels = new Color[map.Width, map.Height];

			//Scale
			int scale = 10;

			//Initialisieren des Pixelarrays
			for (int y = 0; y < map.Height; y++)
			{
				for (int x = 0; x < map.Width; x++)
				{
					pixels[x, y] = map.GetPixel(x, y);
				}
			}

			//Geht jeden Pixel in Pixels durch
			for (int y = 0; y < map.Height; y++)
			{
				for (int x = 0; x < map.Width; x++)
				{
					if (pixels[x, y].A == 0 && pixels[x, y].R == 0 && pixels[x, y].G == 0 && pixels[x, y].B == 0)
					{
						// TODO: Code für tranzparente Pixel hinzufügen
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R < 10 && pixels[x, y].G < 10 && pixels[x, y].B < 10)
					{
						// TODO: Code für schwarze Pixel hinzufügen 
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 0 && pixels[x, y].B == 255)
					{
						// TODO: Code für blaue Pixel hinzufügen
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
					{
						// TODO: Code für grüne Pixel hinzufügen 
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 255 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
					{
						// TODO: Code für gelbe Pixel hinzufügen
					}
				}
			}
		}
	}
}
