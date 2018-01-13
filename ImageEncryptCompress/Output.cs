using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageEncryptCompress;
using ImageQuantization;

namespace ImageEncryptCompress
{
    public partial class Output : Form
    {
        public Output()
        {
            InitializeComponent();
            //Huffman_Tree t = new Huffman_Tree();
            //Program.OriginalImage = Decompress.OUTPUT;
            //Program.ApplyEncryptionOrDecryption(Convert.ToString(Program.seed1, 2), Program.tap2);
            ImageOperations.DisplayImage(Decompress.OUTPUT, pictureBox1);
            textBox1.Text = Decompress.elapsedTime;
        }

        private void Output_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
