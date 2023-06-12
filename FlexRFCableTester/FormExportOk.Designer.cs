namespace FlexRFCableTester
{
    partial class FormExportOk
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
            this.buttonOkFinishIcon = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelStatusFinishIcon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOkFinishIcon
            // 
            this.buttonOkFinishIcon.BackColor = System.Drawing.Color.White;
            this.buttonOkFinishIcon.Location = new System.Drawing.Point(223, 91);
            this.buttonOkFinishIcon.Name = "buttonOkFinishIcon";
            this.buttonOkFinishIcon.Size = new System.Drawing.Size(70, 25);
            this.buttonOkFinishIcon.TabIndex = 0;
            this.buttonOkFinishIcon.Text = "OK";
            this.buttonOkFinishIcon.UseVisualStyleBackColor = false;
            this.buttonOkFinishIcon.Click += new System.EventHandler(this.buttonOkFinishIcon_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FlexRFCableTester.Properties.Resources.success_icon1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelStatusFinishIcon
            // 
            this.labelStatusFinishIcon.AutoSize = true;
            this.labelStatusFinishIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.labelStatusFinishIcon.Location = new System.Drawing.Point(70, 41);
            this.labelStatusFinishIcon.Name = "labelStatusFinishIcon";
            this.labelStatusFinishIcon.Size = new System.Drawing.Size(0, 13);
            this.labelStatusFinishIcon.TabIndex = 2;
            // 
            // FormExportOk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 122);
            this.ControlBox = false;
            this.Controls.Add(this.labelStatusFinishIcon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonOkFinishIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormExportOk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOkFinishIcon;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label labelStatusFinishIcon;
    }
}