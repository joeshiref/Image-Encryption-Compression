using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ImageEncryptCompress;
using ImageQuantization;
using DexterLib;
using System.Threading;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;
using System.Threading;
using AForge.Video.FFMPEG;
using System.Diagnostics;

namespace ImageQuantization
{


    public partial class MainForm : Form
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MainMenu File;
        private System.Windows.Forms.MenuItem miFile;
        private System.Windows.Forms.MenuItem miOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.Button backward;
        private System.Windows.Forms.Button forward;
        private System.Windows.Forms.MenuItem miSpeed;
        private System.Windows.Forms.MenuItem miNormalSpeed;
        private System.Windows.Forms.MenuItem miFastSpeed;
        private System.ComponentModel.Container components = null;
        public static string fileName;           //used to save the movie file name 
        public string storagePath;        //used for the path where we save files
        MediaDetClass md;          //needed to extract pictures
        public static int counter = 0;    //to generate different file names
        public float interval = 1.0f;     //default time interval
        public static int Width, Height;


        public MainForm()
        {
            InitializeComponent();
        }
        RGBPixel[,] ImageMatrix;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Open the browsed image and display it
                    string OpenedFilePath = openFileDialog1.FileName;

                    ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                    Width = ImageOperations.GetWidth(ImageMatrix);
                    Height = ImageOperations.GetHeight(ImageMatrix);
                    Program.OriginalImage = new RGBPixel[Height, Width];
                    Program.OriginalImage = ImageMatrix;
                }
                txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
                txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            }
            catch
            {
                MessageBox.Show("No image selected.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            openEncryptionForm();
        }

        private static void openEncryptionForm()
        {
            try
            {
                EncryptDecrypt Opform = new EncryptDecrypt();
                Opform.Show();
            }
            catch
            {
                MessageBox.Show("You must select an image first.");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Huffman_Tree tree = new Huffman_Tree();
            Huffman_Tree.Comp = new FileStream("Compressed_Picture.bin", FileMode.Append);
            Huffman_Tree.Comp_r = new BinaryWriter(Huffman_Tree.Comp);
            tree.FreqMatrix(Program.OriginalImage);
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Compress.elapsedTime_Const = String.Format
                ("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            CompressAndDecompress Opform = new CompressAndDecompress();
            Opform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Huffman_Tree.Comp = new FileStream("Compressed_Picture.bin", FileMode.Open);
            Huffman_Tree.Comp_w = new BinaryReader(Huffman_Tree.Comp);
            Decompress.GetReconstruct();
            Decompress.GetBuildDecompress();
            Huffman_Tree.Comp.Close();
            Huffman_Tree.Comp_w.Close();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Decompress.elapsedTime = String.Format
                ("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Output print = new Output();
            print.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = op.FileName;

                // create instance of video reader
                VideoFileReader reader = new VideoFileReader();
                // open video file
                reader.Open(fileName);
                // read 100 video frames out of it
                for (int i = 0; i <= 1312; i++)
                {
                    Bitmap videoFrame = reader.ReadVideoFrame();
                    Program.images_name[Program.index_vedio] = "C:\\Users\\JOE\\Desktop\\[TEMPLATE] ImageEncryptCompress\\Take Pictures\\" + i.ToString();
                    Program.index_vedio++;
                    videoFrame.Save(@"C:\\Users\\JOE\\Desktop\\[TEMPLATE] ImageEncryptCompress\Take Pictures\\" + i.ToString() + ".bmp");
                    // dispose the frame when it is no longer required
                    videoFrame.Dispose();
                }
                reader.Close();
            }
            
            Huffman_Tree tree = new Huffman_Tree();
            Huffman_Tree.Comp = new FileStream("Compressed_Picture.bin", FileMode.Append);
            Huffman_Tree.Comp_r = new BinaryWriter(Huffman_Tree.Comp);
            Huffman_Tree.Comp_r.Write(Program.index_vedio);
            Huffman_Tree.Comp_r.Close();
            Huffman_Tree.Comp.Close();
            for (int i=0;i<Program.index_vedio;i++)
            {
                Huffman_Tree.Comp = new FileStream("Compressed_Picture.bin", FileMode.Append);
                Huffman_Tree.Comp_r = new BinaryWriter(Huffman_Tree.Comp);
                string OpenedFilePath = Program.images_name[i];
                Program.OriginalImage = ImageOperations.OpenImage(OpenedFilePath + ".bmp");
                Width = ImageOperations.GetWidth(Program.OriginalImage);
                Height = ImageOperations.GetHeight(Program.OriginalImage);
                tree.FreqMatrix(Program.OriginalImage);
                Compress c = new Compress();
                c.convert_Image_to_binary();
            }
            MessageBox.Show("DONE");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Huffman_Tree.Comp = new FileStream("Compressed_Picture.bin", FileMode.Open);
            Huffman_Tree.Comp_w = new BinaryReader(Huffman_Tree.Comp);
            int idxo = Huffman_Tree.Comp_w.ReadInt32();
            for(int i=0;i<idxo;i++)
            {
                Decompress.GetReconstruct();
                Decompress.GetBuildDecompress();
            }
            
            Huffman_Tree.Comp.Close();
            Huffman_Tree.Comp_w.Close();

            VideoFileWriter writer = new VideoFileWriter();
            writer.Open(@"C:\Users\JOE\Desktop\Videos\Decomp.avi", 320, 240, 25, VideoCodec.Default);

            // ... here you'll need to load your bitmaps
            for (int i = 0; i <= 1312; i++)
            {
                string s = Convert.ToString(i);
                Bitmap original_bm = new Bitmap(@"C:\Users\JOE\Desktop\[TEMPLATE] ImageEncryptCompress\Decompress Video/" + s + ".bmp");
                writer.WriteVideoFrame(original_bm);
            }
            writer.Close();

        }

        public void button3_Click(object sender, EventArgs e)
        {

            EncryptDecryptVedio open = new EncryptDecryptVedio();
            open.Show();
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = op.FileName;

                // create instance of video reader
                VideoFileReader reader = new VideoFileReader();
                // open video file
                reader.Open(fileName);
                // read 100 video frames out of it
                for (int i = 0; i <= 1312; i++)
                {
                    Bitmap videoFrame = reader.ReadVideoFrame();
                    Program.images_name[Program.index_vedio] = "C:\\Users\\JOE\\Desktop\\[TEMPLATE] ImageEncryptCompress\\Take Pictures\\" + i.ToString();
                    Program.index_vedio++;
                    videoFrame.Save(@"C:\\Users\\JOE\\Desktop\\[TEMPLATE] ImageEncryptCompress\\Take Pictures\\" + i.ToString() + ".bmp");


                    // dispose the frame when it is no longer required
                    videoFrame.Dispose();
                }
                reader.Close();/*
            EncryptDecryptVedio open = new EncryptDecryptVedio();
            open.Show();
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = op.FileName;
                //this.miOpenFile.Index = 0;
                //// this.miOpenFile.Text = "Open File";
                //// this.miOpenFile.Click += new System.EventHandler(this.miOpenFile_Click);
                //if (md == null) return;
                storagePath = "C:\\Users\\Abdelrahman\\Desktop\\Test\\";
                ScanThread st = new ScanThread(storagePath, fileName, interval);
                do
                {
                    //waits until the processing is done, displaying the
                    //number of the file we are currently saving
                    Thread.Sleep(1000);
                } while (st.t.IsAlive);
                */
            }
            //string[] filePaths = Directory.GetFiles(@"C:\Users\Abdelrahman\Desktop/Test", "*.txt",
            //                           SearchOption.TopDirectoryOnly);



        }

    }
    public class ScanThread
    {


        MediaDetClass md;
        string fileName;
        string storagePath;
        float interval;

        public Thread t;
        public ScanThread(string s, string f, float ival)
        {
            storagePath = s;
            fileName = f;
            interval = ival;
            t = new Thread(new ThreadStart(this.Scan));
            t.Start();
        }
        void Scan()
        {
            md = new MediaDetClass();
            Image img;
            md.Filename = fileName;
            md.CurrentStream = 0;
            int len = (int)md.StreamLength;
            EncryptDecryptVedio.FrameRate = md.FrameRate;
            for (float i = 0.0f; i < len; i = i + interval)
            {
                MainForm.counter++;
                string fBitmapName = storagePath + Path.GetFileNameWithoutExtension(fileName)
                  + "_" + MainForm.counter.ToString();

                md.WriteBitmapBits(i, 320, 240, fBitmapName + ".bmp");

                img = Image.FromFile(fBitmapName + ".bmp");
                img.Save(fBitmapName + ".jpg", ImageFormat.Jpeg);
                Program.images_name[Program.index_vedio] = fBitmapName;
                Program.index_vedio++;
                img.Dispose();
                System.IO.File.Delete(fBitmapName + ".bmp");
            }
        }
    }
}