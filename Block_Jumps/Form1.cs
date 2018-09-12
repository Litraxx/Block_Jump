using System;
using System.Drawing;
using System.Windows.Forms;

namespace Block_Jumps
{
    public partial class Block_Jump : Form
    {
        //Enum an Boxarten
        enum BoxType
        {
            AIR,
            BLOCK,
            SPAWN,
            PLAYER
        }

        //Klasse, welche die allgemeine Box beschreibt 
        class Box
        {
            protected PictureBox picBox;
            private BoxType type;

            public Box(int x, int y, BoxType type, bool canCollide, Color color)
            {
                picBox = new PictureBox();
                picBox.Location = new Point(x, y);
                picBox.Size = new Size(15, 15);
                picBox.BackColor = color;
                this.type = type;
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
        }

        //Klasse für den Player
        class Player : Box
        {
            public Player()
                : base(0, 0, BoxType.PLAYER, false, Color.Red)
            {

            }

            //Umsetzung des Sprunges
            public void Jump()
            {
                picBox.Location = new Point(picBox.Location.X, picBox.Location.Y - 5);
            }
        }

        //Klasse die alle Blöcke des Levels hält und das Level ausliest
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

            //Liest das Level aus dem Bild aus
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
                            boxes[x, y] = new Box(x * scale, y * scale, BoxType.SPAWN, true, Color.White);
                        }
                    }
                }
            }
        }




        //Anlegen von "global" benötigten Variablen
        Level level = new Level("../../../testLevel.png");
        Player player = new Player();
        int collisionPoint;
        int zähler = 0;

        public Block_Jump()
        {
            InitializeComponent();
        }

        //Metode die nach drücken des Startbuttons ausgeführt wird
        //Setup 
        private void Start()
        {
            //Zuweisen der Tickmetoden für die Timer
            jump.Tick += new EventHandler(jump_Tick);
            gravity.Tick += new EventHandler(gravity_Tick);

            Point spawn = new Point();

            //Suchen des Spawns und hinzufügen der PictureBoxen
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
            //Spieler Positions setzen
            player.PicBox.Location = spawn;

            //Spieler hinzufügen und in den Vordergrund setzen
            Controls.Add(player.PicBox);
            player.PicBox.BringToFront();

            //Starten der Gravitation
            gravity.Start();
        }

        //Startbutton
        private void button1_Click(object sender, EventArgs e)
        {
            label2.Hide();
            button1.Hide();
            button1.Enabled = false;

            Start();
        }

        //Scrollen der Map
        public void MapScroll()
        {
            for (int y = 0; y < level.LevelImage.Height; y++)
            {
                for (int x = 0; x < level.LevelImage.Width; x++)
                {
                    //Bewegen jeder Box in -X Richtung
                    level.Boxes[x, y].PicBox.Location = new Point(level.Boxes[x, y].PicBox.Location.X - 5, level.Boxes[x, y].PicBox.Location.Y);
                }
            }
        }

        //Sprung Timer
        private void jump_Tick(object sender, EventArgs e)
        {
            int max = 10;

            //Gravitation stoppt
            gravity.Stop();

            player.Jump();

            //Stoppen des Sprungs
            if (zähler == max)
            {
                jump.Stop();
                gravity.Start();
            }
            zähler++;

            MapScroll();
        }

        //Gravitation Timer
        private void gravity_Tick(object sender, EventArgs e)
        {
            zähler = 0;

            //Überprüfung ob die PlayerBox mit etwas untersich kollidiert
            if (!collides())
            {
                //Gravitation
                player.PicBox.Location = new Point(player.PicBox.Location.X, player.PicBox.Location.Y + 5);
            }
            else
            {
                //Playerposition "auf die kollision" setzen 
                player.PicBox.Location = new Point(player.PicBox.Location.X, collisionPoint);
            }

            MapScroll();
        }

        //KeyDown Schaut ob der User die Leertaste gedrückt hat
        private void Block_Jump_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                //Starten des Sprungtimers
                jump.Start();
            }
        }

        //Überprüfung ob die PlayerBox kollidiert
        bool collides()
        {
            //Punkt einen Block unter der PlayerBox
            Point underPlayer = player.PicBox.Location;
            underPlayer.Y += 15;

            for (int x = 0; x < level.LevelImage.Width; x++)
            {
                for (int y = 0; y < level.LevelImage.Height; y++)
                {
                    //Überprüfung ob die Box unter der PlayerBox in einem bestimmtem Schwellenwert liegt, ein Block ist und in der nähe vom der PlayerBox liegt
                    if (level.Boxes[x, y].PicBox.Location.Y < underPlayer.Y + 6 && level.Boxes[x, y].PicBox.Location.Y > underPlayer.Y - 6 && level.Boxes[x, y].Type == BoxType.BLOCK && level.Boxes[x, y].PicBox.Location.X > -6 && level.Boxes[x, y].PicBox.Location.X < 6)
                    {
                        //Setzen des Kollisionspunktes; die Höhe auf welcher die PlayerBox sein muss wenn sie kollidiert
                        collisionPoint = level.Boxes[x, y].PicBox.Location.Y - 15;
                        return true;
                    }
                }
            }
            return false;
        }

        //Nicht funktionierende Seitenkollision
        bool gameOver()
        {
            Point tmp = player.PicBox.Location;
            tmp.X += 15;

            for (int x = 0; x < level.LevelImage.Width; x++)
            {
                for (int y = 0; y < level.LevelImage.Height; y++)
                {
                    if (level.Boxes[x, y].PicBox.Location.X < tmp.X + 6 && level.Boxes[x, y].PicBox.Location.X > tmp.X - 6 && level.Boxes[x, y].Type == BoxType.BLOCK)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}