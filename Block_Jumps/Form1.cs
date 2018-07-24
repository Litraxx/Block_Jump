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
	enum BoxType
	{
		AIR,
		BLOCK,
		COIN,
		SPAWN,
		FINISH,
		PLAYER
	}

	class Box
	{
		protected PictureBox picBox;
		private BoxType type;
		protected bool canCollide;

		public Box(int x, int y, BoxType type, bool canCollide)
		{
			picBox = new PictureBox();
			picBox.Location = new Point(x, y);
			this.type = type;
			this.canCollide = canCollide;
		}

		public PictureBox PicBox
		{
			get { return picBox; }
			set { picBox = value; }
		}

		public BoxType Type
		{
			get { return type; }
			set { type = value; }
		}

		public bool CanCollide
		{
			get { return canCollide; }
			set { canCollide = value; }
		}

		protected bool Collides()
		{
			// TODO: Logik für Collides hinzufügen
			return false;
		}
	}

	class Player : Box
	{
		public Player(int x, int y)
			: base(x, y, BoxType.PLAYER, false)
		{

		}

		public void Jump()
		{
			// TODO: Logik für Jump hinzufügen
		}
	}

	class Level
	{
		private Box[,] boxes;
		private Image levelImage;

		public Level(string levelFile)
		{
			levelImage = Image.FromFile(levelFile);
			boxes = new Box[levelImage.Width,levelImage.Height];
			ImageToLevel();
		}

		public Image LevelImage
		{
			get { return levelImage; }
			set { levelImage = value; }
		}

		

		public Box[,] Boxes
		{
			get { return boxes; }
			set { boxes = value; }
		}

		private void ImageToLevel()
		{
			//Kopie des Bildes zu Bitmap
			Bitmap map = new Bitmap(levelImage);

			//Pixelarray
			Color[,] pixels = new Color[levelImage.Width, levelImage.Height];

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
						boxes[x, y] = new Box(x * scale, y * scale, BoxType.AIR, false);
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R < 10 && pixels[x, y].G < 10 && pixels[x, y].B < 10)
					{
						boxes[x, y] = new Box(x * scale, y * scale, BoxType.BLOCK, true);
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 0 && pixels[x, y].B == 255)
					{
						boxes[x, y] = new Box(x * scale, y * scale, BoxType.SPAWN, true);
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
					{
						boxes[x, y] = new Box(x * scale, y * scale, BoxType.FINISH, false);
					}
					else if (pixels[x, y].A == 255 && pixels[x, y].R == 255 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
					{
						boxes[x, y] = new Box(x * scale, y * scale, BoxType.COIN, false);
					}
				}
			}
		}
	}

	public partial class Block_Jump : Form
	{
		private Level level = new Level("testLevel.png");

		Player player = new Player(10, 10);

		public Block_Jump()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
