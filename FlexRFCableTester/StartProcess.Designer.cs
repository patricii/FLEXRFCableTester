namespace FlexRFCableTester
{
    partial class StartProcess
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
            this.buttonStartProcess = new System.Windows.Forms.Button();
            this.labelStartProcess = new System.Windows.Forms.Label();
            this.labelCalStatusStartProcess = new System.Windows.Forms.Label();
            this.pictureBoxStartProcess = new System.Windows.Forms.PictureBox();
            this.buttonAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStartProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartProcess
            // 
            this.buttonStartProcess.BackColor = System.Drawing.Color.Silver;
            this.buttonStartProcess.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStartProcess.Location = new System.Drawing.Point(471, 526);
            this.buttonStartProcess.Name = "buttonStartProcess";
            this.buttonStartProcess.Size = new System.Drawing.Size(80, 30);
            this.buttonStartProcess.TabIndex = 0;
            this.buttonStartProcess.Text = "OK";
            this.buttonStartProcess.UseVisualStyleBackColor = false;
            this.buttonStartProcess.Click += new System.EventHandler(this.buttonStartProcess_Click);
            // 
            // labelStartProcess
            // 
            this.labelStartProcess.AutoSize = true;
            this.labelStartProcess.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStartProcess.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelStartProcess.Location = new System.Drawing.Point(46, 23);
            this.labelStartProcess.Name = "labelStartProcess";
            this.labelStartProcess.Size = new System.Drawing.Size(774, 40);
            this.labelStartProcess.TabIndex = 1;
            this.labelStartProcess.Text = "-> Conecte o Cabo entre o PowerMeter e o Signal Generator";
            // 
            // labelCalStatusStartProcess
            // 
            this.labelCalStatusStartProcess.AutoSize = true;
            this.labelCalStatusStartProcess.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.labelCalStatusStartProcess.Location = new System.Drawing.Point(208, 486);
            this.labelCalStatusStartProcess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCalStatusStartProcess.Name = "labelCalStatusStartProcess";
            this.labelCalStatusStartProcess.Size = new System.Drawing.Size(0, 30);
            this.labelCalStatusStartProcess.TabIndex = 3;
            // 
            // pictureBoxStartProcess
            // 
            this.pictureBoxStartProcess.Image = global::FlexRFCableTester.Properties.Resources.StartProcess;
            this.pictureBoxStartProcess.Location = new System.Drawing.Point(12, 84);
            this.pictureBoxStartProcess.Name = "pictureBoxStartProcess";
            this.pictureBoxStartProcess.Size = new System.Drawing.Size(848, 410);
            this.pictureBoxStartProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStartProcess.TabIndex = 4;
            this.pictureBoxStartProcess.TabStop = false;
            // 
            // buttonAbort
            // 
            this.buttonAbort.BackColor = System.Drawing.Color.Red;
            this.buttonAbort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonAbort.Location = new System.Drawing.Point(326, 526);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(80, 30);
            this.buttonAbort.TabIndex = 5;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = false;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // StartProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(865, 570);
            this.ControlBox = false;
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.pictureBoxStartProcess);
            this.Controls.Add(this.labelCalStatusStartProcess);
            this.Controls.Add(this.labelStartProcess);
            this.Controls.Add(this.buttonStartProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Process";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStartProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartProcess;
        private System.Windows.Forms.Label labelStartProcess;
        private System.Windows.Forms.Label labelCalStatusStartProcess;
        private System.Windows.Forms.PictureBox pictureBoxStartProcess;
        private System.Windows.Forms.Button buttonAbort;
    }
}