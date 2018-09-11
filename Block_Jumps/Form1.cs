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

            public Box(int x, int y, BoxType type, bool canCollide, Color color)
            {
                picBox = new PictureBox();
                picBox.Location = new Point(x, y);
                picBox.Size = new Size(15, 15);
                picBox.BackColor = color;
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
                : base(x, y, BoxType.PLAYER, false, Color.Red)
            {

            }

            public void Jump()
            {
                // TODO: Logik für Jump hinzufügen
                //timer2.Stop();
                int zähler = 0;


                while (zähler != 4)
                {
                    picBox.Location = new Point(picBox.Location.X + 50);

                    /* if (umkehr)
                     {
                         picBox.Location = new Point(picBox.Location.X - 50);
                     }
                     else
                     {
                         picBox.Location = new Point(picBox.Location.X + 50);
                     }

                     */
                    zähler++;

                    /* if (zähler == 4)
                     {
                         timer1.Stop();
                     }
                     */

                }
            }

            public void gravitation()
            {
                picBox.Location = new Point(picBox.Location.X - 50);
            }
        }

        class Level
        {
            private Box[,] boxes;
            private Image levelImage;

            public Level(string levelFile)
            {
                levelImage = Image.FromFile(levelFile);
                boxes = new Box[levelImage.Width, levelImage.Height];
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
                int scale = 15;

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
                        //Weiß
                        if (pixels[x, y].A == 255 && pixels[x, y].R == 255 && pixels[x, y].G == 255 && pixels[x, y].B == 255)
                        {
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.AIR, false, Color.White);
                        }
                        //Schwarz
                        else if (pixels[x, y].A == 255 && pixels[x, y].R < 10 && pixels[x, y].G < 10 && pixels[x, y].B < 10)
                        {
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.BLOCK, true, Color.Black);
                        }
                        //Blau
                        else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 0 && pixels[x, y].B == 255)
                        {
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.SPAWN, true, Color.Blue);
                        }
                        //Grün
                        else if (pixels[x, y].A == 255 && pixels[x, y].R == 0 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
                        {
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.FINISH, false, Color.FromArgb(0, 255, 0));
                        }
                        //Gelb
                        else if (pixels[x, y].A == 255 && pixels[x, y].R == 255 && pixels[x, y].G == 255 && pixels[x, y].B == 0)
                        {
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.COIN, false, Color.Yellow);
                        }
                    }
                }
            }
        }

        public Block_Jump()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Level level = new Level("../../../testLevel.png");

        private void Start()
        {

            Player player = new Player(10, 10);

            //PictureBox box = new PictureBox();

            foreach (Box box in level.Boxes)
            {
                Controls.Add(box.PicBox);
                box.PicBox.Refresh();
            }

            MapTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start();
        }


        //Mapscroll zum scrollen der Map
        public void MapScroll()
        {
            Box[,] box = level.Boxes;

            for (int y = 0; y < level.LevelImage.Height; y++)
            {
                for (int x = 0; x < level.LevelImage.Width; x++)
                {
                    box[x, y].PicBox.Location = new Point(box[x, y].PicBox.Location.X - 1, box[x, y].PicBox.Location.Y);
                }
            }
            level.Boxes = box;
            //foreach (Box box in level.Boxes)
            //{
            //    xKoord = box.PicBox.Location.X;

            //    xKoord++;

            //    PictureBox pbox = new PictureBox();
            //    pbox.Location = box.PicBox.Location;
            //    pbox.Location = new Point(xKoord, box.PicBox.Location.Y);

            //}

        }

        // Timer für Mapscroll und co
        Timer mapTimer = new Timer();
        bool ende = false; //der bool wenn das levelEnde erreicht wurde

        private void MapTimer()
        {

            mapTimer.Interval = 10; // Timer Intervalle in Millisekunden (1000 = 1 Sekunde)
            mapTimer.Enabled = true; //Timer start

            mapTimer.Tick += new EventHandler(MapTimerTickEvents);
        }

        private void MapTimerTickEvents(object sender, EventArgs e)
        {

            if (ende == true)
            {
                mapTimer.Enabled = false; //Timer Endet
            }
            else
            {
                MapScroll();  //Das eine Event was aktuell ausgeführt wird
                              // evtl zu implementier Abfragen zu Collison check unbd sonstiges

            }
        }


        public Form1()
        {

            InitializeComponent();
        }
        bool umkehr = false;
        //Steuerung mit Keys
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            timer2.Enabled = true;

            if (e.KeyCode == Keys.Left)
            {
                pictureBox3.Left -= 20;
            }
            else if (e.KeyCode == Keys.Right)
            {
                pictureBox3.Left += 20;
            }
            else if (e.KeyCode == Keys.Space)
            {
                timer1.Enabled = true;
            }

            //wechsel Punkt für Graviation
            if (pictureBox3.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                umkehr = true;
            }
        }
        //Objeckte
        public void animation()
        {
            pictureBox3.Location = pictureBox1.Location;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        //Sprung   
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Gravitation stoppt
            timer2.Stop();
            int zähler = 0;

            //Der Sprung
            while (zähler != 4)
            {
                if (umkehr)
                {
                    pictureBox3.Top -= 50;
                }
                else
                {
                    pictureBox3.Top += 50;
                }


                zähler++;
                //der Sprung stoppt
                if (zähler == 4)
                {
                    timer1.Stop();
                }

            }

        }
        //Graviatation
        private void timer2_Tick(object sender, EventArgs e)
        {
            //wenn Graviattion umgekechrt wird
            if (umkehr == false)
            {
                pictureBox3.Top += 5;
            }
            else
            {
                pictureBox3.Top -= 5;
            }


        }

    }
}
