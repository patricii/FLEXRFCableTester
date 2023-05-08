namespace FlexRFCableTester
{
    partial class zeroCalPowerMeter
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
            this.labelZeroCalPm = new System.Windows.Forms.Label();
            this.buttonOkPm = new System.Windows.Forms.Button();
            this.labelCalStatusPm = new System.Windows.Forms.Label();
            this.pictureBoxZeroCalPm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZeroCalPm)).BeginInit();
            this.SuspendLayout();
            // 
            // labelZeroCalPm
            // 
            this.labelZeroCalPm.AutoSize = true;
            this.labelZeroCalPm.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Italic);
            this.labelZeroCalPm.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelZeroCalPm.Location = new System.Drawing.Point(159, 9);
            this.labelZeroCalPm.Name = "labelZeroCalPm";
            this.labelZeroCalPm.Size = new System.Drawing.Size(562, 40);
            this.labelZeroCalPm.TabIndex = 0;
            this.labelZeroCalPm.Text = "-> Conecte o Power Sensor no Power Meter";
            // 
            // buttonOkPm
            // 
            this.buttonOkPm.BackColor = System.Drawing.SystemColors.Window;
            this.buttonOkPm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOkPm.Location = new System.Drawing.Point(400, 530);
            this.buttonOkPm.Name = "buttonOkPm";
            this.buttonOkPm.Size = new System.Drawing.Size(80, 30);
            this.buttonOkPm.TabIndex = 2;
            this.buttonOkPm.Text = "OK";
            this.buttonOkPm.UseVisualStyleBackColor = false;
            this.buttonOkPm.Click += new System.EventHandler(this.buttonOkPm_Click);
            // 
            // labelCalStatusPm
            // 
            this.labelCalStatusPm.AutoSize = true;
            this.labelCalStatusPm.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelCalStatusPm.Location = new System.Drawing.Point(208, 486);
            this.labelCalStatusPm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCalStatusPm.Name = "labelCalStatusPm";
            this.labelCalStatusPm.Size = new System.Drawing.Size(0, 30);
            this.labelCalStatusPm.TabIndex = 2;
            // 
            // pictureBoxZeroCalPm
            // 
            this.pictureBoxZeroCalPm.Image = global::FlexRFCableTester.Properties.Resources.zeroCal;
            this.pictureBoxZeroCalPm.Location = new System.Drawing.Point(8, 63);
            this.pictureBoxZeroCalPm.Name = "pictureBoxZeroCalPm";
            this.pictureBoxZeroCalPm.Size = new System.Drawing.Size(848, 410);
            this.pictureBoxZeroCalPm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxZeroCalPm.TabIndex = 1;
            this.pictureBoxZeroCalPm.TabStop = false;
            // 
            // zeroCalPowerMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(865, 570);
            this.Controls.Add(this.labelCalStatusPm);
            this.Controls.Add(this.buttonOkPm);
            this.Controls.Add(this.pictureBoxZeroCalPm);
            this.Controls.Add(this.labelZeroCalPm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "zeroCalPowerMeter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zero Cal Power Meter";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZeroCalPm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelZeroCalPm;
        private System.Windows.Forms.PictureBox pictureBoxZeroCalPm;
        private System.Windows.Forms.Button buttonOkPm;
        private System.Windows.Forms.Label labelCalStatusPm;
    }
}