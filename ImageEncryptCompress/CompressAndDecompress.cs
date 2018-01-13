using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ImageEncryptCompress;
using System.Diagnostics;

namespace ImageEncryptCompress
{
    public partial class CompressAndDecompress : Form
    {
        public String path = @"C:\Users\JOE\Desktop\Final-IsA_1 - Copy (4) - Copy - Copy\Final IsA\TEMPLATE-ImageEncryptCompress_4\[TEMPLATE] ImageEncryptCompress\ImageEncryptCompress\Compress.txt";

        public CompressAndDecompress()
        {
            InitializeComponent();

            tabPage1.Text = "Red";
            tabPage2.Text = "Green";
            tabPage3.Text = "Blue";


            DataTable dt = new DataTable();
            dt.Columns.Add("Color");
            dt.Columns.Add("Frequency");
            dt.Columns.Add("Total Bits");

            StreamReader sr = new StreamReader(path);
            String tmp;
            tmp = sr.ReadLine();
            int times = Int32.Parse(tmp);
            for (int j = 0; j < times; j++)
            {
                DataRow dr = dt.NewRow();

                tmp = sr.ReadLine();
                dr["Color"] = tmp;

                tmp = sr.ReadLine();
                dr["Frequency"] = tmp;



                tmp = sr.ReadLine();
                dr["Total Bits"] = tmp;

                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;

            //--------------------------
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Color");
            dt1.Columns.Add("Frequency");
            dt1.Columns.Add("Total Bits");

            tmp = sr.ReadLine();
            times = Int32.Parse(tmp);
            for (int j = 0; j < times; j++)
            {
                DataRow drr = dt1.NewRow();

                tmp = sr.ReadLine();
                drr["Color"] = tmp;

                tmp = sr.ReadLine();
                drr["Frequency"] = tmp;


                tmp = sr.ReadLine();
                drr["Total Bits"] = tmp;

                dt1.Rows.Add(drr);
            }

            dataGridView2.DataSource = dt1;
            //---------------------
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Color");
            dt2.Columns.Add("Frequency");
            dt2.Columns.Add("Total Bits");

            tmp = sr.ReadLine();
            times = Int32.Parse(tmp);
            for (int j = 0; j < times; j++)
            {
                DataRow drr = dt2.NewRow();

                tmp = sr.ReadLine();
                drr["Color"] = tmp;

                tmp = sr.ReadLine();
                drr["Frequency"] = tmp;



                tmp = sr.ReadLine();
                drr["Total Bits"] = tmp;

                dt2.Rows.Add(drr);
            }

            dataGridView3.DataSource = dt2;

            sr.Close();
            textBox1.Text = Huffman_Tree.Bytes.ToString();
            textBox2.Text = Huffman_Tree.CompressionRatio.ToString();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Compress c = new Compress();
            c.convert_Image_to_binary();
            MessageBox.Show("Compression Done, Check File Size :D");
            textBox3.Text = Compress.elapsedTime_Const;
            textBox4.Text = Compress.elapsedTime_Compress;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
