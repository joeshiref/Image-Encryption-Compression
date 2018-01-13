using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using ImageQuantization;
using AForge.Video.FFMPEG;
using System.IO;
//using Emgu.CV;
//using Emgu.CV.UI;
namespace ImageEncryptCompress
{
    public partial class EncryptDecryptVedio : Form
    {
        public static double FrameRate { get; set; }
        public EncryptDecryptVedio()
        {
            InitializeComponent();
        }

        private void EncryptDecryptVedio_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Tap = Int32.Parse(textBox3.Text);
            string Seed = textBox1.Text;
            Program.tap_vedio = Tap;
            Program.seed_len_vedio = Seed.Length;
            int seed = 0, Power = 1;
            int currunt = 0;
            for (int i = Seed.Length - 1; i > -1; i--)
            {
                if (Seed[i] == '1') seed += Power;
                Power *= 2;
                if (currunt < 63)
                {
                   Program.seed1_vedio = seed;
                }
                if (currunt == 62)
                {
                    Power = 1;
                    seed = 0;
                }
                if (currunt >= 63)
                {
                    Program.seed2_vedio = seed;
                }
                currunt++;
            }
            for (int i = 0; i < Program.index_vedio; i++)
            {
                string OpenedFilePath = Program.images_name[i];
                Program.OriginalImage = ImageOperations.OpenImage(OpenedFilePath +".bmp");
                int Width, Height;
                Width = ImageOperations.GetWidth(Program.OriginalImage);
                Height = ImageOperations.GetHeight(Program.OriginalImage);
                Program.ApplyEncryptionOrDecryption1();
            }
           
         VideoFileWriter writer = new VideoFileWriter();
         writer.Open(@"C:\Users\JOE\Desktop\Videos\mov2.avi", 320, 240, 25, VideoCodec.Default);
         
            // ... here you'll need to load your bitmaps
        for (int i = 0; i <=1312 ; i++)
        {
            string s = Convert.ToString(i);
            Bitmap original_bm = new Bitmap(@"C:\Users\JOE\Desktop\[TEMPLATE] ImageEncryptCompress\Put Pictures/" + s + ".bmp");
            writer.WriteVideoFrame(original_bm);
        }
                writer.Close();
           /* var size = new Size(1600, 1200);                    // The desired size of the video
            var fps = 25;                                       // The desired frames-per-second
            var codec = VideoCodec.MPEG4;                       // Which codec to use
            var destinationfile = @"d:\myfile.avi";             // Output file
            var srcdirectory = @"d:\foo\bar";                   // Directory to scan for images
            var pattern = "*.jpg";                              // Files to look for in srcdirectory
            var searchoption = SearchOption.TopDirectoryOnly;   // Search Top Directory Only or all subdirectories (recursively)?

            using (var writer = new VideoFileWriter())          // Initialize a VideoFileWriter
            {
                writer.Open(destinationfile, size.Width, size.Height, fps, codec);              // Start output of video
                foreach (var file in Directory.GetFiles(srcdirectory, pattern, searchoption))   // Iterate files
                {
                    using (var original = (Bitmap)Image.FromFile(file))     // Read bitmap
                    using (var resized = new Bitmap(original, size))        // Resize if necessary
                        writer.WriteVideoFrame(resized);                    // Write frame
                }
                writer.Close();                                 // Close VideoFileWriter
            }      */
           
        }
    }
}
