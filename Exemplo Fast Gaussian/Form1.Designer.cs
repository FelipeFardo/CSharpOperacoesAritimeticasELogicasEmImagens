
namespace BlurExample
{
    partial class Form1
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
            this.btRun = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.tbDepth = new System.Windows.Forms.TrackBar();
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
            this.pbImage.Location = new System.Drawing.Point(9, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(500, 550);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // btRun
            // 
            this.btRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRun.Location = new System.Drawing.Point(431, 576);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(75, 32);
            this.btRun.TabIndex = 1;
            this.btRun.Text = "Blur";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // btReset
            // 
            this.btReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btReset.Location = new System.Drawing.Point(350, 576);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 32);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // tbDepth
            // 
            this.tbDepth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDepth.Location = new System.Drawing.Point(12, 578);
            this.tbDepth.Maximum = 6;
            this.tbDepth.Minimum = 1;
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Size = new System.Drawing.Size(332, 45);
            this.tbDepth.TabIndex = 3;
            this.tbDepth.Value = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 611);
            this.Controls.Add(this.tbDepth);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.pbImage);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blur Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDepth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.TrackBar tbDepth;
    }
}

