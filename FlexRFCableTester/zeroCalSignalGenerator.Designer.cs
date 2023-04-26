namespace FlexRFCableTester
{
    partial class zeroCalSignalGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(zeroCalSignalGenerator));
            this.buttonOkSg = new System.Windows.Forms.Button();
            this.labelCalStatusSg = new System.Windows.Forms.Label();
            this.labelZeroCalSignalGen = new System.Windows.Forms.Label();
            this.pictureBoxZeroCalSg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZeroCalSg)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOkSg
            // 
            this.buttonOkSg.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOkSg.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonOkSg.Location = new System.Drawing.Point(400, 530);
            this.buttonOkSg.Name = "buttonOkSg";
            this.buttonOkSg.Size = new System.Drawing.Size(80, 30);
            this.buttonOkSg.TabIndex = 0;
            this.buttonOkSg.Text = "OK";
            this.buttonOkSg.UseVisualStyleBackColor = false;
            this.buttonOkSg.Click += new System.EventHandler(this.buttonOkSg_Click);
            // 
            // labelCalStatusSg
            // 
            this.labelCalStatusSg.AutoSize = true;
            this.labelCalStatusSg.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelCalStatusSg.Location = new System.Drawing.Point(153, 497);
            this.labelCalStatusSg.Name = "labelCalStatusSg";
            this.labelCalStatusSg.Size = new System.Drawing.Size(0, 30);
            this.labelCalStatusSg.TabIndex = 1;
            // 
            // labelZeroCalSignalGen
            // 
            this.labelZeroCalSignalGen.AutoSize = true;
            this.labelZeroCalSignalGen.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelZeroCalSignalGen.Location = new System.Drawing.Point(164, 19);
            this.labelZeroCalSignalGen.Name = "labelZeroCalSignalGen";
            this.labelZeroCalSignalGen.Size = new System.Drawing.Size(538, 30);
            this.labelZeroCalSignalGen.TabIndex = 3;
            this.labelZeroCalSignalGen.Text = "Conecte o Power Sensor no Cabo do Signal Generator";
            // 
            // pictureBoxZeroCalSg
            // 
            this.pictureBoxZeroCalSg.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxZeroCalSg.Image")));
            this.pictureBoxZeroCalSg.Location = new System.Drawing.Point(8, 63);
            this.pictureBoxZeroCalSg.Name = "pictureBoxZeroCalSg";
            this.pictureBoxZeroCalSg.Size = new System.Drawing.Size(848, 410);
            this.pictureBoxZeroCalSg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxZeroCalSg.TabIndex = 2;
            this.pictureBoxZeroCalSg.TabStop = false;
            // 
            // zeroCalSignalGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 570);
            this.Controls.Add(this.labelZeroCalSignalGen);
            this.Controls.Add(this.pictureBoxZeroCalSg);
            this.Controls.Add(this.labelCalStatusSg);
            this.Controls.Add(this.buttonOkSg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "zeroCalSignalGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zero Cal Signal Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZeroCalSg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOkSg;
        private System.Windows.Forms.Label labelCalStatusSg;
        private System.Windows.Forms.PictureBox pictureBoxZeroCalSg;
        private System.Windows.Forms.Label labelZeroCalSignalGen;
    }
}