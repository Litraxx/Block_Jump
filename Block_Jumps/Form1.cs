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

        public bool Collides()
        {
            //hindernisCheck
            if (player.Bounds.IntersectsWith(obstacle.Bounds))    //überprüft ob player mit x(einem hindernis) kollidiert      //TODO: naming
            {
                return true;
            }
            
            return false;
        }


        //TODO: in gameloopTimer implementieren
        /*if (player.Left + player.Width - 19 > block.Left          //boden check
                    && player.Left + player.Width<block.Left + block.Width + player.Width
                    && player.Top + player.Height >= block.Top
                    && player.Top<block.Top)
            {
                player.Top = gameBorder.Height - block.Height - player.Height;
        }*/


    }
}
