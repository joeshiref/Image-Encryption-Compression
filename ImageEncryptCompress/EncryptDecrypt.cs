using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImageQuantization;

namespace ImageEncryptCompress
{
    public partial class EncryptDecrypt : Form
    {
        public EncryptDecrypt()
        {
           
            InitializeComponent();
            ImageOperations.DisplayImage(Program.OriginalImage, pictureBox1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Please select an Encryption method.");
                return;
            }
            long Tap = Int32.Parse(textBox2.Text);
            string Seed = textBox4.Text;
            if (radioButton2.Checked == true) Seed = Program.ConvertAlpha(Seed);
            textBox2.Clear();
            textBox4.Clear();
            Program.ApplyEncryptionOrDecryption(Seed, Tap);
            ImageOperations.DisplayImage(Program.OriginalImage, pictureBox1);
            textBox6.Text = Program.elapsedTime;
            txtWidth.Clear();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (radioButton3.Checked == false && radioButton4.Checked == false)
            {
                MessageBox.Show("Please select a Decryption method.");
                return;
            }
            int Tap = Int32.Parse(textBox3.Text);
            string Seed = textBox1.Text;
            if (radioButton4.Checked == true) Seed = Program.ConvertAlpha(Seed);
            Program.ApplyEncryptionOrDecryption(Seed, Tap);
            ImageOperations.DisplayImage(Program.OriginalImage, pictureBox1);
            txtWidth.Text = Program.elapsedTime;
            textBox6.Clear();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Program.GenerateSeed(Int32.Parse(textBox5.Text));
            //ImageOperations.DisplayImage(Program.OriginalImage, pictureBox1);
            Program.all_possible();
            MessageBox.Show("Done!");
        }
        private void OpForm_Load(object sender, EventArgs e)
        {

        }
        private void OpForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        public void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
