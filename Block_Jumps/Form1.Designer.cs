namespace Block_Jumps
{
    partial class Block_Jump
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.player = new System.Windows.Forms.PictureBox();
            this.block = new System.Windows.Forms.PictureBox();
            this.gameBorder = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.block)).BeginInit();
            this.gameBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.player.Location = new System.Drawing.Point(268, 390);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(20, 20);
            this.player.TabIndex = 0;
            this.player.TabStop = false;
            // 
            // block
            // 
            this.block.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.block.Location = new System.Drawing.Point(228, 448);
            this.block.Name = "block";
            this.block.Size = new System.Drawing.Size(100, 20);
            this.block.TabIndex = 1;
            this.block.TabStop = false;
            // 
            // gameBorder
            // 
            this.gameBorder.Controls.Add(this.player);
            this.gameBorder.Controls.Add(this.block);
            this.gameBorder.Location = new System.Drawing.Point(12, 81);
            this.gameBorder.Name = "gameBorder";
            this.gameBorder.Size = new System.Drawing.Size(560, 468);
            this.gameBorder.TabIndex = 2;
            // 
            // Block_Jump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.gameBorder);
            this.Name = "Block_Jump";
            this.Text = "Block Jump";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.block)).EndInit();
            this.gameBorder.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox block;
        private System.Windows.Forms.Panel gameBorder;
    }
}

