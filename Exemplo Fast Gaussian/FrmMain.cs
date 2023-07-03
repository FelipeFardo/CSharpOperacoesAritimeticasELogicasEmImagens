using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BlurExample
{
    public partial class FrmMain : Form
    {
        private Image original;
        private bool closing            = false;
        private object uiUpdateLocker   = new object();

        public void UI(Action action) {
            if (closing) return;

            if (InvokeRequired) {
                try { Invoke((MethodInvoker)delegate () { action(); }); 
                } catch (Exception) { }
            } else {
                action();
            }
        }
        public FrmMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            original = pbImage.Image;
            Reset();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void tbDepth_Scroll(object sender, EventArgs e)
        {
            lblBlurSize.Text = "Blur Size: " + tbDepth.Value.ToString();

            if (tbDepth.Value == 1) {
                lblExecutionTime.Text   = "Execution Time: None (original image)";
                lblBlurSize.Text        = "Blur Size: None";
                pbImage.Image           = original;
                return;
            }

            // Do the blur on a background thread so the UI remains responsive:
            ThreadPool.QueueUserWorkItem((o) => {

                Bitmap thisImage    = (Bitmap)original;
                Stopwatch stopwatch = new Stopwatch();
                int depth           = (int)o;

                stopwatch.Start();
                ImageTools.Blur(ref thisImage, depth);
                stopwatch.Stop();

                // We have the speed results and the blurred image now. 
                // Display them, but lock it so that we don't ever see the wrong speed over the wrong image:
                lock (uiUpdateLocker) {

                    // Flip to the UI thread to display our results:
                    UI(() => {
                        lblExecutionTime.Text   = "Execution Time: " + 
                            stopwatch.ElapsedMilliseconds.ToString() + " Milliseconds.";
                        pbImage.Image           = thisImage;
                    });
                }
                
            }, tbDepth.Value);
        }

        private void Reset() {
            pbImage.Image           = original;
            tbDepth.Value           = 1;
            lblExecutionTime.Text   = "Execution Time: None (original image)";
            lblBlurSize.Text        = "Blur Size: None";
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }
    }
}
