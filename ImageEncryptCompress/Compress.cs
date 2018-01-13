using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using ImageEncryptCompress;
using ImageQuantization;
using System.Diagnostics;

namespace ImageEncryptCompress
{
    class Compress
    {
        public static string elapsedTime_Const, elapsedTime_Compress;
        public byte Convert_To_Byte(BitArray bits) //O(1)
        {
            byte[] bytes = new byte[1]; //O(1)
            bits.CopyTo(bytes, 0); //O(1)
            return bytes[0]; //O(1)
        }
        public void convert_Image_to_binary() //it converts the picture O(N^2) where N is dimension of image
        {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Huffman_Tree.Comp_r.Write(MainForm.Height);
            Huffman_Tree.Comp_r.Write(MainForm.Width);
            //these 3 lists have the binarystream of each color
            List<bool> Redo = new List<bool>();
            List<bool> Greeno = new List<bool>();
            List<bool> Blueo = new List<bool>();

            for (int i = 0; i < MainForm.Height; i++) //O(Height)
            {
                for (int j = 0; j < MainForm.Width; j++) //O(Width) 
                {
                    //We have tried to insert element by element by random access like: redo[i]=binarycode[i]
                    //it took time more than addrange
                    Redo.AddRange(Huffman_Tree.Mapping_Red[Program.OriginalImage[i, j].red]); //O(1)
                    Greeno.AddRange(Huffman_Tree.Mapping_Green[Program.OriginalImage[i, j].green]); //O(1)
                    Blueo.AddRange(Huffman_Tree.Mapping_Blue[Program.OriginalImage[i, j].blue]); //O(1)                   
                }
            }
            int sz1 = Redo.Count;
            int sz2 = Greeno.Count;
            int sz3 = Blueo.Count;

            int len1 = 0, len2 = 0, len3 = 0;
            if ((Redo.Count) % 8 != 0)
                len1 = 8 - (Redo.Count) % 8;
            if ((Greeno.Count) % 8 != 0)
                len2 = 8 - (Greeno.Count) % 8;
            if ((Blueo.Count) % 8 != 0)
                len3 = 8 - (Blueo.Count) % 8;
            sz1 += len1;
            sz2 += len2;
            sz3 += len3;
            Huffman_Tree.Comp_r.Write(sz1);
            Huffman_Tree.Comp_r.Write(sz2);
            Huffman_Tree.Comp_r.Write(sz3);
            //if the last byte doesn't have 8 bits, we start to add 0 from the end
            while (len1 != 0)
            {
                Redo.Add(false); //O(1)
                len1--;
            }
            //iterate over the binarystream of red color, take 8 bits from it and convert it to byte
            //and then write the byte in the file
            for (int i = 0; i < Redo.Count; i += 8) //O(R) where R is the length of red stream
            {
                BitArray bb = new BitArray(8);
                for (int k = 0; k < 8; k++) //O(1)
                    bb[k] = Redo[k + i]; //O(1)
                byte bbb = Convert_To_Byte(bb); //O(1)
                Huffman_Tree.Comp_r.Write(bbb);
            }
            //if the last byte doesn't have 8 bits, we start to add 0 from the end
            while (len2 != 0)
            {
                Greeno.Add(false);
                len2--;
            }
            //iterate over the binarystream of green color, take 8 bits from it and convert it to byte
            //and then write the byte in the file
            for (int i = 0; i < Greeno.Count; i += 8)//O(G) where G is the length of green stream
            {
                BitArray bb = new BitArray(8);
                for (int j = 0; j < 8; j++) //O(1)
                    bb[j] = Greeno[j + i]; //O(1)
                byte bbb = Convert_To_Byte(bb); //O(1)
                Huffman_Tree.Comp_r.Write(bbb);
            }
            //if the last byte doesn't have 8 bits, we start to add 0 from the end
            while (len3 != 0)
            {
                Blueo.Add(false); //O(1)
                len3--;
            }
            //iterate over the binarystream of blue color, take 8 bits from it and convert it to byte
            //and then write the byte in the file
            for (int i = 0; i < Blueo.Count; i += 8) //O(B) where B is the length of blue stream
            {
                BitArray bb = new BitArray(8);
                for (int j = 0; j < 8; j++) //O(1)
                    bb[j] = Blueo[j + i]; //O(1)
                byte bbb = Convert_To_Byte(bb); //O(1)
                Huffman_Tree.Comp_r.Write(bbb);
            }
            Huffman_Tree.Comp_r.Write(Program.tmpoooo);
            Huffman_Tree.Comp_r.Write(Program.tap2);
            Huffman_Tree.Comp_r.Close();
            Huffman_Tree.Comp.Close();
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            Compress.elapsedTime_Compress = String.Format
                ("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);


        }
    }
}
