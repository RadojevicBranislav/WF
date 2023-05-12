using DataLayer;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WF1
{
    public partial class Form1 : Form
    {
        private Model_PicRepository model_PicRepository;

        public Form1()
        {
            InitializeComponent();
            model_PicRepository = new Model_PicRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertImageMemoryStream(openFileDialog1);
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            loadImage(model_PicRepository.GetModel_PicById(1), pictureBox1);
        }

        public void insertImageMemoryStream(OpenFileDialog openFileDialog1)
        {
            this.openFileDialog1 = new OpenFileDialog();

            this.openFileDialog1.Multiselect = false;

            this.openFileDialog1.Title = "Select Photo: ";

            this.openFileDialog1.Filter =
            "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +
            "All files (*.*)|*.*";

            this.openFileDialog1.RestoreDirectory = true;

            this.openFileDialog1.InitialDirectory = "";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string filePath = this.openFileDialog1.FileName;

                Console.WriteLine(filePath);

                System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);

                MemoryStream memoryStream = new MemoryStream();

                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] readText = memoryStream.ToArray();

                Model_Pic mp = new Model_Pic();

                mp.Image = readText;

                model_PicRepository.InsertPic(mp);

            }
        }

        public void loadImage(byte[] pic, PictureBox pictureBox1)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(pic))
                {
                    using (System.Drawing.Image image = System.Drawing.Image.FromStream(ms))
                    {
                        pictureBox1.Image = new Bitmap(image);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Failed to create image from byte array: " + ex.Message);
            }
        }
    }
}
