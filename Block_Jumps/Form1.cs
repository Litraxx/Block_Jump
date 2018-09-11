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
            NON,
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

            public Box()
            {
                picBox = new PictureBox();
                type = BoxType.NON;
                canCollide = false;

            }

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
            public Player()
                : base()
            {

            }

            public Player(int x, int y)
                : base(x, y, BoxType.PLAYER, false, Color.Red)
            {

            }

            public void Jump()
            {
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
                }
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
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.SPAWN, true, Color.Blue); //TODO: zu weiß ändern
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
        Player player = new Player();
        bool running = false;

        private void Start()
        {
            Point spawn = new Point();



            Player player = new Player(10, 10);



            //PictureBox box = new PictureBox();

            foreach (Box box in level.Boxes)
            {
                if (box.Type == BoxType.SPAWN)
                {
                    spawn.X = box.PicBox.Location.X;
                    spawn.Y = box.PicBox.Location.Y;
                }
                Controls.Add(box.PicBox);
                box.PicBox.Refresh();
            }
            player = new Player(spawn.X, spawn.Y);
            Controls.Add(player.PicBox);
            player.PicBox.BringToFront();

            Console.WriteLine("Test");
            //MapTimer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Hide();
            button1.Hide();
            button1.Enabled = false;

            running = true;
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
                    box[x, y].PicBox.Location = new Point(box[x, y].PicBox.Location.X - 5, box[x, y].PicBox.Location.Y);
                }
            }
            //level.Boxes = box;
        }

        // Timer für Mapscroll und co
        Timer mapTimer = new Timer();
        //bool ende = false; //der bool wenn das levelEnde erreicht wurde

        private void MapTimer()
        {

            mapTimer.Interval = 20; // Timer Intervalle in Millisekunden (1000 = 1 Sekunde)
            mapTimer.Enabled = true; //Timer start

            mapTimer.Tick += new EventHandler(MapTimerTickEvents);
        }

        private void MapTimerTickEvents(object sender, EventArgs e)
        {

            //if (ende == true)
            //{
            //    mapTimer.Enabled = false; //Timer Endet
            //    levelAbgeschlossen();
            //}
            //else
            //{
            //    MapScroll();  //Das eine Event was aktuell ausgeführt wird
                // evtl zu implementier Abfragen zu Collison check unbd sonstiges

            //}
        }

        // evtl levelabschluss funktion
        public void levelAbgeschlossen()
        {

        }

        bool umkehr = false;

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
                    player.PicBox.Top -= 50;
                }
                else
                {
                    player.PicBox.Top += 50;
                }


                zähler++;
                //der Sprung stoppt
                if (zähler == 4)
                {
                    timer1.Stop();
                }

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //wenn Graviattion umgekechrt wird
            if (umkehr == false)
            {
                player.PicBox.Top += 5;
            }
            else
            {
                player.PicBox.Top -= 5;
            }


        }

        private void Block_Jump_KeyDown(object sender, KeyEventArgs e)
        {

            timer2.Enabled = true;
            if (e.KeyCode == Keys.Space)
            {
                timer1.Enabled = true;
            }
        }
    }
}