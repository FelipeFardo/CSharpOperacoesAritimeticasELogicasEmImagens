using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlurExample
{
    public partial class Form1 : Form
    {
        Image original;
        Bitmap workingImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            original        = pbImage.Image;
            workingImage    = (Bitmap)pbImage.Image;
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            LockControls();

            if (tbDepth.Value == 1) {
                ImageTools.Blur(ref workingImage);
                pbImage.Image = workingImage;
            } else {
                //int times = 0;

                //while (times < tbDepth.Value) {
                //    ImageTools.Blur(ref workingImage);
                //    times += 1;
                //}

                ImageTools.Blur(ref workingImage, tbDepth.Value);

                pbImage.Image = workingImage;
            }
            
            LockControls(false);
        }

        private void LockControls(bool doit = true) {
            if (doit) {
                btRun.Enabled           = false;
                btReset.Enabled         = false;
            } else { 
                btRun.Enabled           = true;
                btReset.Enabled         = true;
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            workingImage    = (Bitmap)original;
            pbImage.Image   = original;
        }
    }
}
