
namespace BlurExample
{
    partial class FrmMain
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
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btReset = new System.Windows.Forms.Button();
            this.tbDepth = new System.Windows.Forms.TrackBar();
            this.lblBlurSize = new System.Windows.Forms.Label();
            this.lblExecutionTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImage.Image = global::BlurExample.Properties.Resources.pexels_photo_15804295;
            this.pbImage.Location = new System.Drawing.Point(9, 29);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(500, 550);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // btReset
            // 
            this.btReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btReset.Location = new System.Drawing.Point(434, 585);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 38);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // tbDepth
            // 
            this.tbDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDepth.Location = new System.Drawing.Point(9, 595);
            this.tbDepth.Minimum = 1;
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(419, 45);
            this.tbDepth.TabIndex = 3;
            this.tbDepth.Value = 2;
            this.tbDepth.Scroll += new System.EventHandler(this.tbDepth_Scroll);
            // 
            // lblBlurSize
            // 
            this.lblBlurSize.AutoSize = true;
            this.lblBlurSize.Location = new System.Drawing.Point(12, 9);
            this.lblBlurSize.Name = "lblBlurSize";
            this.lblBlurSize.Size = new System.Drawing.Size(80, 13);
            this.lblBlurSize.TabIndex = 4;
            this.lblBlurSize.Text = "Blur Size: None";
            // 
            // lblExecutionTime
            // 
            this.lblExecutionTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExecutionTime.AutoSize = true;
            this.lblExecutionTime.Location = new System.Drawing.Point(234, 9);
            this.lblExecutionTime.Name = "lblExecutionTime";
            this.lblExecutionTime.Size = new System.Drawing.Size(185, 13);
            this.lblExecutionTime.TabIndex = 5;
            this.lblExecutionTime.Text = "Execution Time: None (original image)";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 628);
            this.Controls.Add(this.lblExecutionTime);
            this.Controls.Add(this.lblBlurSize);
            this.Controls.Add(this.tbDepth);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.pbImage);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(534, 667);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blur Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.TrackBar tbDepth;
        private System.Windows.Forms.Label lblBlurSize;
        private System.Windows.Forms.Label lblExecutionTime;
    }
}

