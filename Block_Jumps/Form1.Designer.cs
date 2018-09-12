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
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.jump = new System.Windows.Forms.Timer(this.components);
            this.gravity = new System.Windows.Forms.Timer(this.components);
            this.lblGameover = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(101, 89);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lblTitle.Location = new System.Drawing.Point(64, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(154, 31);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Block Jump";
            // 
            // jump
            // 
            this.jump.Interval = 50;
            // 
            // gravity
            // 
            this.gravity.Enabled = true;
            this.gravity.Interval = 50;
            // 
            // lblGameover
            // 
            this.lblGameover.AutoSize = true;
            this.lblGameover.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblGameover.Location = new System.Drawing.Point(27, 30);
            this.lblGameover.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGameover.Name = "lblGameover";
            this.lblGameover.Size = new System.Drawing.Size(224, 46);
            this.lblGameover.TabIndex = 2;
            this.lblGameover.Text = "Game Over";
            this.lblGameover.Visible = false;
            // 
            // Block_Jump
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 141);
            this.Controls.Add(this.lblGameover);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnStart);
            this.MaximumSize = new System.Drawing.Size(400, 180);
            this.Name = "Block_Jump";
            this.Text = "Block Jump";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Block_Jump_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Timer jump;
        private System.Windows.Forms.Timer gravity;
        private System.Windows.Forms.Label lblGameover;
    }
}

