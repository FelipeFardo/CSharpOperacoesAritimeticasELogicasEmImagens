using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Encoder = System.Drawing.Imaging.Encoder;

namespace ProcessamentoDeImagens
{
    public partial class Form1 : Form
    {
        private Bitmap img1;
        private byte[,] vImg1Gray;
        private byte[,] vImg1R;
        private byte[,] vImg1G;
        private byte[,] vImg1B;
        private byte[,] vImg1A;

        private Bitmap img2;
        private byte[,] vImg2Gray;
        private byte[,] vImg2R;
        private byte[,] vImg2G;
        private byte[,] vImg2B;
        private byte[,] vImg2A;

        private Bitmap imgR;
        private byte[,] vImg3Gray;
        private byte[,] vImg3R;
        private byte[,] vImg3G;
        private byte[,] vImg3B;
        private byte[,] vImg3A;


        Bitmap img, imgEq, novaImg;
        Color pixel;
        int red, green, blue;
        int[] histogramaR, histogramaG, histogramaB;
        float[] histAcumuladoR, histAcumuladoG, histAcumuladoB;
        int[] mapaCoresR, mapaCoresG, mapaCoresB;
        int tomR, tomG, tomB, novoTomR, novoTomG, novoTomB;

        public Form1()
        {
            InitializeComponent();
        }

        private void verifyImage(Bitmap image)
        {
            if (image == null)
            {
                MessageBox.Show("Please insert valid images!", "Images not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                throw new InvalidOperationException("Image not found!");
            }
        }

        private Bitmap invertImageColor(Bitmap image)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);

            int x, y;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;

                    int R, G, B;

                    R = 255 - color.R;
                    G = 255 - color.G;
                    B = 255 - color.B;

                    colorOut = Color.FromArgb(R, G, B);
                    outputImage.SetPixel(x, y, colorOut);
                }
            }

            return outputImage;
        }

        private Bitmap toGray(Bitmap image)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);

            int x, y;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;

                    int grayScale = (int)((color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11));
                    colorOut = Color.FromArgb(grayScale, grayScale, grayScale);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }

            return outputImage;
        }

        private Bitmap toBinary(Bitmap image, double thresh)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);

            int x, y;
            double thresold = 255 * thresh;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;

                    int R, G, B;

                    if (color.R >= thresold && color.G >= thresold && color.B >= thresold)
                    {
                        R = 255;
                        G = 255;
                        B = 255;
                    }
                    else
                    {
                        R = 0;
                        G = 0;
                        B = 0;
                    }

                    colorOut = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }

            return outputImage;
        }

        private Bitmap multImage(Bitmap image, double num)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);

            int x, y;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;
                    int R, G, B;

                    R = (int)(color.R * num);
                    G = (int)(color.G * num);
                    B = (int)(color.B * num);

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

                    colorOut = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }
            return outputImage;
        }

        private Bitmap divImage(Bitmap image, double num)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);

            int x, y;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;
                    int R, G, B;

                    R = (int)(color.R / num);
                    G = (int)(color.G / num);
                    B = (int)(color.B / num);

                    colorOut = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }
            return outputImage;
        }

        private Bitmap mirrorImages(Bitmap image)
        {
            Bitmap outputImage = new Bitmap(image.Width * 2, image.Height);

            int x, y;
            int i = outputImage.Width - 1;

            for (x = 0; x < image.Width; x++, i--)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);

                    outputImage.SetPixel(x, y, color);
                    outputImage.SetPixel(i, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap generateImage(int seed=-1)
        {
            Random randNum = new Random();
            if (seed==-1)seed= randNum.Next();
            Bitmap image = new Bitmap(40, 40, PixelFormat.Format8bppIndexed);
            var bitmapData = image.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadWrite, image.PixelFormat);
            Random random = new Random(seed);
            byte[] buffer = new byte[image.Width * image.Height];
            random.NextBytes(buffer);
            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, bitmapData.Scan0, buffer.Length);
            image.UnlockBits(bitmapData);
            return image;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private Bitmap resizeImage(Bitmap imgToResize)
        {
            Size size;
            Size sizeA = new Size(img1.Width, img1.Height);
            Size sizeB = new Size(img2.Width, img2.Height);
            if (sizeA.Width > sizeB.Width && sizeA.Height > sizeB.Height) size = sizeA;
            else size = sizeB;

            return new Bitmap(imgToResize, size);
        }

        private Bitmap blendImages(Bitmap img1, Bitmap img2, double num)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;
                    int R, G, B;
                    if (color1 != color2)
                    {
                        R = (int)((num * color1.R) + (1 - num) * color2.R);
                        G = (int)((num * color1.G) + (1 - num) * color2.G);
                        B = (int)((num * color1.B) + (1 - num) * color2.B);

                        if (R < 0) R = 0;
                        else if (R > 255) R = 255;
                        if (G < 0) G = 0;
                        else if (G > 255) G = 255;
                        if (B < 0) B = 0;
                        else if (B > 255) B = 255;
                        color = Color.FromArgb(R, G, B);
                    }
                    else color = color1;
                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap addImages(Bitmap image, double num)
        {
            Bitmap outputImage = new Bitmap(image.Width, image.Height);
            int x, y;

            for (x = 0; x < image.Width; x++)
            {
                for (y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;
                    int R, G, B;

                    R = (int)(color.R + num);
                    G = (int)(color.G + num);
                    B = (int)(color.B + num);
                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

                    colorOut = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }
            return outputImage;

        }

        private bool exportImage(Bitmap image)
        {
            try
            {
                ImageCodecInfo myImageCodecInfo;
                Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;


                myImageCodecInfo = GetEncoderInfo("image/jpeg");

                myEncoder = Encoder.Quality;

                myEncoderParameters = new EncoderParameters(1);


                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                image.Save("../../../exported_result.jpg", myImageCodecInfo, myEncoderParameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private Bitmap subImages(Bitmap image, double num)
        {
                Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    Color colorOut;
                    int R, G, B;

                    R = (int)(color.R - num);
                    G = (int)(color.G - num);
                    B = (int)(color.B - num);
                    if (R < 0) R = 0;
                    if (G < 0) G = 0;
                    if (B < 0) B = 0;

                    colorOut = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, colorOut);
                }
            }
            return outputImage;
        }

        byte Average(byte a, byte b)
        {
            return (byte)((a + b) / 2);
        }
        private Bitmap avgImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color Color1 = imgA.GetPixel(x, y);
                    Color Color2 = imgB.GetPixel(x, y);
                    Color color = Color.FromArgb(Average(Color1.R, Color2.R), Average(Color1.G, Color2.G), Average(Color1.B, Color2.B));

                    outputImage.SetPixel(x, y, color);
                }
            }

            return outputImage;
        }

        private Bitmap divTwoImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    int R2 = color2.R, G2 = color2.G, B2 = color2.B;
                    Color color;
                    int R, G, B;
                    
                        if (color2.R < 1) R2 = 1;
                        if (color2.G < 1) G2 = 1;
                        if (color2.B < 1) B2 = 1;
                        R = (int)(color1.R / R2);
                        G = (int)(color1.G / G2);
                        B = (int)(color1.B / B2);


                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;


                        color = Color.FromArgb(R, G, B);
                    
                    outputImage.SetPixel(x, y, color);
                }
            }
                return outputImage;
            
        }
        
        private Bitmap multiplyTwoImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;

                    int R, G, B;

                    R = color1.R * color2.R;
                    G = color1.G * color2.G;
                    B = color1.B * color2.B;

                    if (R < 0) R = 0;
                    else if (R > 255) R = 255;
                    if (G < 0) G = 0;
                    else if (G > 255) G = 255;
                    if (B < 0) B = 0;
                    else if (B > 255) B = 255;
                    color = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, color);
                }
            }

            return outputImage;
        }
        

        private Bitmap andImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            imgA = toBinary(imgA, 0.5);
            imgB = toBinary(imgB, 0.5);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;

                    int R, G, B;

                    if (color1.R > 0 && color2.R > 0)
                    {
                        R = 255;
                    }
                    else R = 0;

                    if (color1.G > 0 && color2.G > 0)
                    {
                        G = 255;
                    }
                    else G = 0;
                    if (color1.B > 0 && color2.B > 0)
                    {
                        B = 255;
                    }
                    else B = 0;

                    color = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap orImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            imgA = toBinary(imgA, 0.5);
            imgB = toBinary(imgB, 0.5);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;

                    int R, G, B;

                    if (color1.R > 0 || color2.R > 0)
                    {
                        R = 255;
                    }
                    else R = 0;

                    if (color1.G > 0 || color2.G > 0)
                    {
                        G = 255;
                    }
                    else G = 0;
                    if (color1.B > 0 || color2.B > 0)
                    {
                        B = 255;
                    }
                    else B = 0;

                    color = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap xorImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            imgA = toBinary(imgA, 0.5);
            imgB = toBinary(imgB, 0.5);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;

                    int R, G, B;

                    if (color1.R > 0 ^ color2.R > 0)
                    {
                        R = 255;
                    }
                    else R = 0;

                    if (color1.G > 0 ^ color2.G > 0)
                    {
                        G = 255;
                    }
                    else G = 0;
                    if (color1.B > 0 ^ color2.B > 0)
                    {
                        B = 255;
                    }
                    else B = 0;

                    color = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap notImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            imgA = toBinary(imgA, 0.5);
            imgB = toBinary(imgB, 0.5);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;

                    int R, G, B;

                    if (!(color1.R > 0 && color2.R > 0))
                    {
                        R = 255;
                    }
                    else R = 0;

                    if (!(color1.G > 0 && color2.G > 0))
                    {
                        G = 255;
                    }
                    else G = 0;
                    if (!(color1.B > 0 && color2.B > 0))
                    {
                        B = 255;
                    }
                    else B = 0;

                    color = Color.FromArgb(R, G, B);

                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private Bitmap collageVBitmap(Bitmap[] images)
        {
            images[0] = resizeImage(images[0]);
            images[1] = resizeImage(images[1]);

            var outputHeight = images.Sum(x => x.Height);
            var outputWidth = images[0].Width;

            var outputImage = new Bitmap(outputWidth, outputHeight);

            using (var g = Graphics.FromImage(outputImage))
            {
                //set background color
                g.Clear(System.Drawing.Color.Black);

                //go through each image and draw it on the output image
                int offset = 0;
                foreach (System.Drawing.Bitmap image in images)
                {
                    g.DrawImage(image,
                        new System.Drawing.Rectangle(0, offset, image.Width, image.Height)
                    );
                    offset += image.Height;
                }
            }
            return new Bitmap(outputImage);
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\MatLab"; 
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif" +
                "|JPG image (*.jpg)|*.jpg" +
                "|BMP image (*.bmp)|*.bmp" +
                "|PNG image (*.png)|*.png" +
                "|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;

                bool bLoadImgOk = false;

                try
                {
                    img1 = new Bitmap(filePath);
                    bLoadImgOk = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error opening image A",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    bLoadImgOk = false;
                }

                if (bLoadImgOk)
                { 
                    pbA.Image = img1;
                    vImg1Gray = new byte[img1.Width, img1.Height];
                    vImg1R = new byte[img1.Width, img1.Height];
                    vImg1G = new byte[img1.Width, img1.Height];
                    vImg1B = new byte[img1.Width, img1.Height];
                    vImg1A = new byte[img1.Width, img1.Height];

                }
            }
        }


        private void btnNegaA_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                img1 = invertImageColor(img1);
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to NOT image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnGrayA_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                img1 = toGray(img1);
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to binary image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnBinaryA_Click(object sender, EventArgs e)
        {
            string txt = txtBinA.Text;
            if (txt == "") txt = "0,5";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0 || num > 1)
            {
                MessageBox.Show("Please insert a value in range 0.0 - 1.0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img1);
                img1 = toBinary(img1, double.Parse(txt));
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to binary image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMultA_Click(object sender, EventArgs e)
        {
            string txt = txtMultA.Text;
            if (txt == "") txt = "1";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0)
            {
                MessageBox.Show("Enter a value greater than 1", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img1);
                img1 = multImage(img1, double.Parse(txt));
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error to divide images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDivA_Click(object sender, EventArgs e)
        {
            string txt = txtDivA.Text;
            if (txt == "") txt = "1";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0 || num > 255)
            {
                MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img1);
                img1 = divImage(img1, double.Parse(txt));
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error to divide images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMirrorA_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);

                img1 = mirrorImages(img1);
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGenA_Click(object sender, EventArgs e)
        {
            try
            {
                img1 = generateImage();
                pbA.Image = img1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\MatLab"; 
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif" +
                "|JPG image (*.jpg)|*.jpg" +
                "|BMP image (*.bmp)|*.bmp" +
                "|PNG image (*.png)|*.png" +
                "|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;

                bool bLoadImgOk = false;

                try
                {
                    img2 = new Bitmap(filePath);
                    bLoadImgOk = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error opening image B",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    bLoadImgOk = false;
                }

                if (bLoadImgOk)
                {
                    pbB.Image = img2;
                }
            }
        }


        private void btnNegaB_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img2);
                img2 = invertImageColor(img2);
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to NOT image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGrayB_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img2);
                img2 = toGray(img2);
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to binary image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void btnBinaryB_Click(object sender, EventArgs e)
        {
            string txt = txtBinA.Text;
            if (txt == "") txt = "0,5";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0 || num > 1)
            {
                MessageBox.Show("Please insert a value in range 0.0 - 1.0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img2);
                img2 = toBinary(img2, double.Parse(txt));
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to binary image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMultB_Click(object sender, EventArgs e)
        {
            string txt = txtMultA.Text;
            if (txt == "") txt = "1";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0)
            {
                MessageBox.Show("Enter a value greater than 1", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img2);
                img2 = multImage(img2, double.Parse(txt));
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error to divide images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDivB_Click(object sender, EventArgs e)
        {
            string txt = txtDivA.Text;
            if (txt == "") txt = "1";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0 || num > 255)
            {
                MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img2);
                img2 = divImage(img2, double.Parse(txt));
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error to divide images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMirrorB_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img2);

                img2 = mirrorImages(img2);
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGenB_Click(object sender, EventArgs e)
        {
            try
            {
                img2 = generateImage();
                pbB.Image = img2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportR_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);

                exportImage(imgR);

                MessageBox.Show("Success when exporting image",
                    "Image result exported to executable directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error export image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportA_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);

                exportImage(img1);

                MessageBox.Show("Success when exporting image",
                    "Image result exported to executable directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error export image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportB_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img2);

                exportImage(img2);

                MessageBox.Show("Success when exporting image",
                    "Image result exported to executable directory",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error export image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private Bitmap addTwoImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;
                    int R, G, B;
                    
                        R = color1.R + color2.R;
                        G = color1.G + color2.G;
                        B = color1.B + color2.B;

                        if (R > 255) R = 255;
                        if (G > 255) G = 255;
                        if (B > 255) B = 255;
                        color = Color.FromArgb(R, G, B);
                    
                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string txt = txtAddR.Text;
            if (txt=="") {
                try
                {
                    verifyImage(img1);
                    verifyImage(img2);

                    imgR = addTwoImages(img1, img2);
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error adding images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else {
                if (!double.TryParse(txt, out double num))
                {
                    MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (num <= 0 || num > 255)
                {
                    MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    verifyImage(imgR);
                    imgR = addImages(imgR, double.Parse(txt));
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error to add images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private Bitmap subTwoImages(Bitmap img1, Bitmap img2)
        {
            Bitmap imgA = resizeImage(img1);
            Bitmap imgB = resizeImage(img2);
            Bitmap outputImage = new Bitmap(imgA.Width, imgB.Height);

            int x, y;

            for (x = 0; x < imgA.Width; x++)
            {
                for (y = 0; y < imgA.Height; y++)
                {
                    Color color1 = imgA.GetPixel(x, y);
                    Color color2 = imgB.GetPixel(x, y);
                    Color color;
                    int R, G, B;
                    
                        int R2 = color2.R, G2 = color2.G, B2 = color2.B;

                        R = (int)(color1.R - R2);
                        G = (int)(color1.G - G2);
                        B = (int)(color1.B - B2);

                        if (R < 0) R = 0;
                        if (G < 0) G = 0;
                        if (B < 0) B = 0;
                        color = Color.FromArgb(R, G, B);
               
                    outputImage.SetPixel(x, y, color);
                }
            }
            return outputImage;
        }
        private void btnSub_Click(object sender, EventArgs e)
        {
            string txt = txtSubR.Text;
            if (txt == "")
            {
                try
                {
                    verifyImage(img1);
                    verifyImage(img2);

                    imgR = subTwoImages(img1, img2);
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error subtraction images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                if (!double.TryParse(txt, out double num))
                {
                    MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (num <= 0 || num > 255)
                {
                    MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    verifyImage(imgR);
                    imgR = subImages(imgR, double.Parse(txt));
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error to add images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);
                imgR = avgImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error averaging images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            string txt = txtDivR.Text;
            if (txt == "")
            {
                try
                {
                    verifyImage(img1);
                    verifyImage(img2);

                    imgR = divTwoImages(img1, img2);
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error division images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else {
                if (!double.TryParse(txt, out double num))
                {
                    MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (num <= 0 || num > 255)
                {
                    MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    verifyImage(imgR);
                    imgR = divImage(imgR, double.Parse(txt));
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error to divide images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

 

        private void btnMult_Click(object sender, EventArgs e)
        {
            string txt = txtMultR.Text;
            if (txt == "")
            {
                try
                {
                    verifyImage(img1);
                    verifyImage(img2);

                    imgR = multiplyTwoImages(img1, img2);
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error division images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else {
                if (!double.TryParse(txt, out double num))
                {
                    MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (num <= 0 || num > 255)
                {
                    MessageBox.Show("Please insert a value in range 1 - 255", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    verifyImage(imgR);
                    imgR = multImage(imgR, double.Parse(txt));
                    pbResult.Image = imgR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error to divide images",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnBld_Click(object sender, EventArgs e)
        {
            string txt = txtBld.Text;
            if (txt == "")
            {
                MessageBox.Show("Field Required\nPlease insert a value in range 0.00 - 1.00", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num < 0 || num > 1)
            {
                MessageBox.Show("Please insert a value in range 0.00 - 1.00", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(img1);
                verifyImage(img2);
                imgR = blendImages(img1, img2, double.Parse(txt));
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error blending images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGrayR_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(imgR);
                imgR = toGray(imgR);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to gray image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBinaryR_Click(object sender, EventArgs e)
        {
            string txt = txtBinR.Text;
            if (txt == "") txt = "0,5";
            else if (!double.TryParse(txt, out double num))
            {
                MessageBox.Show("Only numbers", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (num <= 0 || num > 1)
            {
                MessageBox.Show("Please insert a value in range 0.0 - 1.0", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                verifyImage(imgR);
                imgR = toBinary(imgR, double.Parse(txt));
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to binary image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\MatLab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif" +
                "|JPG image (*.jpg)|*.jpg" +
                "|BMP image (*.bmp)|*.bmp" +
                "|PNG image (*.png)|*.png" +
                "|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;

                bool bLoadImgOk = false;

                try
                {
                    imgR = new Bitmap(filePath);
                    bLoadImgOk = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                        "Error opening image R",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    bLoadImgOk = false;
                }

                if (bLoadImgOk)
                {
                    pbResult.Image = imgR;
                }
            }
        }

        private void btnAND_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);

                imgR = andImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error AND images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnOR_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);

                imgR = orImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error OR images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnXOR_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);

                imgR = xorImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error XOR images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNOT_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(img1);
                verifyImage(img2);

                imgR = notImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error NOT images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnGenAndSum_Click(object sender, EventArgs e)
        {
            try
            {
                Random randNum = new Random();
       
                img1 = generateImage(randNum.Next());
                pbA.Image = img1;

                img2 = generateImage(randNum.Next());
                pbB.Image = img2;

                imgR = addTwoImages(img1, img2);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnColV_Click(object sender, EventArgs e)
        {
            Bitmap[] images = new Bitmap[2];
            images[0] = img1;
            images[1] = img2;
            try
            {
                imgR = collageVBitmap(images);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error add images",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMirror_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(imgR);

                imgR = mirrorImages(imgR);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Mirror image error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            try
            {
                verifyImage(imgR);
                imgR = invertImageColor(imgR);
                pbResult.Image = imgR;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error convert to NOT image",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private int maxValue(int[] hist)
        {
            for (int i = (hist.Length - 1); i >= 0; i--)
            {
                if (hist[i] != 0)
                    return i;
            }
            return 0;
        }

        private int minValue(int[] hist)
        {
            for (int i = 0; i <= (hist.Length - 1); i++)
            {
                if (hist[i] != 0)
                    return i;
            }
            return 0;
        }

        private int mediumValue(int[] hist)
        {
            int soma = 0;
            for (int i = 0; i <= (hist.Length - 1); i++)
            {
                    soma += hist[i];
            }
            return soma / (hist.Length - 1);
        }


        private void gerarGraficoHistogramaEq(string canal)
        {
            canal.ToUpper();
            switch (canal)
            {
                case "P":
                    for (int i = 0; i < 256; i++)
                    {
                        chart_P_Equalizado.Series[0].Points.AddXY(i + 1, histogramaR[i]);
                    }
                    break;
            }
        }
        private void calcularHistograma(Bitmap img, string canal)
        {
            if (img != null)
            {
                canal.ToUpper();
                switch (canal)
                {
                    case "P":
                        for (int x = 0; x < img.Width; x++)
                        {
                            for (int y = 0; y < img.Height; y++)
                            {
                                pixel = img.GetPixel(x, y);
                                red = pixel.R;
                                histogramaR[red]++;
                            }
                        }
                        break;
                }
            }
        }

      
        private Bitmap equalizarImagem(Bitmap imagem)
        {
            histogramaR = new int[256];
            histAcumuladoR = new float[256];

            //Calcula histograma
            calcularHistograma(imagem, "P");
            //Cria o gráfico do histograma
            //gerarGraficoHistograma("P");

            //Calcula histograma acumulado
            float aux = histAcumuladoR[0];
            for (int i = 1; i < histogramaR.Length; i++)
            {
                if (histogramaR[i] != 0)
                {
                    histAcumuladoR[i] = aux + ((float)histogramaR[i] / (imagem.Width * imagem.Height));
                    aux = histAcumuladoR[i];
                }
            }

            //Calcula mapa de cores
            int[] mapaCores = new int[256];

            String filter = cbValueFilter.Text;
            int numberFilter = 0;
            if (filter == "Max value") numberFilter = maxValue(histogramaR);
            else if (filter == "Medium value") numberFilter = mediumValue(histogramaR);
            else if (filter == "Min value") numberFilter = minValue(histogramaR);


            for (int i = 0; i < histogramaR.Length; i++)
                mapaCores[i] = (int)(Math.Round(histAcumuladoR[i] * numberFilter));



            //Equalizando imagem
            novaImg = new Bitmap(imagem.Width, imagem.Height);
            for (int m = 0; m < imagem.Width; m++)
            {
                for (int n = 0; n < imagem.Height; n++)
                {
                    pixel = imagem.GetPixel(m, n);
                    tomR = pixel.R;
                    novoTomR = mapaCores[tomR];
                    novaImg.SetPixel(m, n, Color.FromArgb(novoTomR, novoTomR, novoTomR));
                }
            }
            return novaImg;
        }

        private void bt_hist_Click(object sender, EventArgs e)
        {
            verifyImage(imgR);
            imgEq = equalizarImagem(imgR);
            chart_P_Equalizado.Series[0].Points.Clear();
            histogramaR = new int[256];

            calcularHistograma(imgEq, "P");
            gerarGraficoHistogramaEq("P");

        }


    }

}

