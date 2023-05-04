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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStartProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartProcess
            // 
            this.buttonStartProcess.BackColor = System.Drawing.Color.White;
            this.buttonStartProcess.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStartProcess.Location = new System.Drawing.Point(400, 530);
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
            this.labelStartProcess.Font = new System.Drawing.Font("Segoe UI", 18.75F, System.Drawing.FontStyle.Bold);
            this.labelStartProcess.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelStartProcess.Location = new System.Drawing.Point(64, 9);
            this.labelStartProcess.Name = "labelStartProcess";
            this.labelStartProcess.Size = new System.Drawing.Size(726, 35);
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
            this.pictureBoxStartProcess.Location = new System.Drawing.Point(8, 80);
            this.pictureBoxStartProcess.Name = "pictureBoxStartProcess";
            this.pictureBoxStartProcess.Size = new System.Drawing.Size(848, 410);
            this.pictureBoxStartProcess.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxStartProcess.TabIndex = 4;
            this.pictureBoxStartProcess.TabStop = false;
            // 
            // StartProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 570);
            this.Controls.Add(this.pictureBoxStartProcess);
            this.Controls.Add(this.labelCalStatusStartProcess);
            this.Controls.Add(this.labelStartProcess);
            this.Controls.Add(this.buttonStartProcess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
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
    }
}