using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using ImageEncryptCompress;
using ImageQuantization;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageEncryptCompress
{
    class Decompress
    {
        public static long seed;
        public static int tap;
        public static int width_comp, hight_comp;
        public static RGBPixel[,] OUTPUT;
        public static string elapsedTime;
        public static Node Red_Root, Green_Root, Blue_Root;
        public static int cnt=0;
        public static void GetBuildDecompress()
        {
            Get_file();
        }
        // it does everything :D
        public static void Get_file() //Loads the compressed file to get the image from binarystreams
        {
            Node Root = Red_Root;
            Node source = Root;
            hight_comp = Huffman_Tree.Comp_w.ReadInt32();
            width_comp = Huffman_Tree.Comp_w.ReadInt32();
            OUTPUT = new RGBPixel[hight_comp, width_comp];
            int length_of_red = Huffman_Tree.Comp_w.ReadInt32() / 8;
            int length_of_green = Huffman_Tree.Comp_w.ReadInt32() / 8;
            int length_of_blue = Huffman_Tree.Comp_w.ReadInt32() / 8;

            int idx = 0, x, row = 0, col = 0;
            while (length_of_red != 0) //iterate over the binary stream of red value and reads a byte each time
            {
                byte val = Huffman_Tree.Comp_w.ReadByte(); //read byte from the stream, it doesn't have to be a specific value
                BitArray bits = new BitArray(8);
                bits = Convert_Byte_To_Bit(val); // converts the read byte into array of bits
                int f = 0;
                //iterate over the 8 bits and check if the current bit is 0, moves left in tree else move right
                //if we reached a leaf node, we retrive its value and start traversing from the source -root- again
                //else we continue traversing from the previous node with new 8 bits
                while (bits.Count > f)
                {
                    if (source.Left == null || source.Right == null)
                    {
                        x = source.Value;
                        OUTPUT[row, col].red = (byte)x;
                        col++;
                        if (col == width_comp)
                        {
                            row++;
                            if (row == hight_comp)
                                row = 0;
                            col = 0;
                        }
                        source = Root;
                    }

                    if (bits[f] == false)
                    {
                        source = source.Left;
                        idx++;
                    }

                    else if (bits[f] == true)
                    {
                        source = source.Right;
                        idx++;
                    }
                    f++;
                }

                length_of_red--;
            }
            //---------------------------Greeeen-----------------------------//
            //Same thing happens here
            Root = Green_Root;
            source = Root;

            idx = 0; row = 0; col = 0;
            while (length_of_green != 0)
            {
                byte val = Huffman_Tree.Comp_w.ReadByte();
                BitArray bits = new BitArray(8);
                bits = Convert_Byte_To_Bit(val);
                int f = 0;
                while (bits.Count > f)
                {
                    if (source.Left == null || source.Right == null)
                    {
                        x = source.Value;
                        OUTPUT[row, col].green = (byte)x;
                        col++;
                        if (col == width_comp)
                        {
                            row++;
                            if (row == hight_comp)
                                row = 0;
                            col = 0;
                        }
                        source = Root;
                    }

                    if (bits[f] == false)
                    {
                        source = source.Left;
                        idx++;
                    }
                    else if (bits[f] == true)
                    {
                        source = source.Right;
                        idx++;
                    }
                    f++;
                }

                length_of_green--;
            }
            //---------------------------Blueeeee-----------------------------//
            //Same thing happens here
            Root = Blue_Root;
            source = Root;
            idx = 0; row = 0; col = 0;
            while (length_of_blue != 0)
            {
                byte val = Huffman_Tree.Comp_w.ReadByte();
                BitArray bits = new BitArray(8);
                bits = Convert_Byte_To_Bit(val);
                int f = 0;
                while (bits.Count > f)
                {
                    if (source.Left == null || source.Right == null)
                    {
                        x = source.Value;
                        OUTPUT[row, col].blue = (byte)x;
                        col++;
                        if (col == width_comp)
                        {
                            row++;
                            if (row == hight_comp)
                                row = 0;
                            col = 0;
                        }
                        source = Root;
                    }

                    if (bits[f] == false)
                    {
                        source = source.Left;
                        idx++;
                    }
                    else if (bits[f] == true)
                    {
                        source = source.Right;
                        idx++;
                    }
                    f++;
                }

                length_of_blue--;
            }
            Program.seed1 = Huffman_Tree.Comp_w.ReadInt64();
            Program.tap2 = Huffman_Tree.Comp_w.ReadInt64();

            int H = OUTPUT.GetLength(0);
            int W = OUTPUT.GetLength(1);
            Bitmap ImageBMP = new Bitmap(W, H, PixelFormat.Format24bppRgb);
            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, W, H), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = W * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        p[2] = OUTPUT[i, j].red;
                        p[1] = OUTPUT[i, j].green;
                        p[0] = OUTPUT[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }
        
            ImageBMP.Save(@"C:\Users\JOE\Desktop\[TEMPLATE] ImageEncryptCompress\Decompress Video/" + cnt.ToString() + ".bmp");
            cnt++;
        }
        public static BitArray Convert_Byte_To_Bit(byte val) //converts byte to bit array 
        {
            BitArray bit = new BitArray(8);
            int idx = 0;
            while (val != 0) //O(log Val) at worst case val will be 255
            {
                if (val % 2 == 0)
                    bit[idx++] = false;
                else
                    bit[idx++] = true;
                val >>= 1;
            }
            while (idx < 8)
            {
                bit[idx++] = false;
            }
            return bit;
        }

        public static void GetReconstruct() //reconstruct the tree again
        {


            short sz = Huffman_Tree.Comp_w.ReadInt16();
            List<Node> arr = new List<Node>(); //this list carries the nodes -internal and external of tree-

            for (int i = 0; i < sz; i++)//iterate over the number of nodes of red value
            {
                Node NewNode = new Node();
                NewNode.Value = Huffman_Tree.Comp_w.ReadInt16();
                if (i == 0)
                    arr.Add(NewNode);
                else if (i != 0)
                {
                    //iterate over the list and get the 1st -1 value which doesn't have left/right children
                    //puts the priority to the left child then right child
                    for (int j = 0; j < arr.Count; j++)
                    {
                        if (arr[j].Value == -1)
                        {
                            if (arr[j].Left == null)
                            {
                                arr[j].Left = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                            else if (arr[j].Right == null)
                            {
                                arr[j].Right = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                        }
                    }
                }
            }
            //gets the root of the red tree which will be in the 1st node in the left
            Red_Root = arr[0];

            //same thing happens here
            sz = Huffman_Tree.Comp_w.ReadInt16();
            arr = new List<Node>();

            for (int i = 0; i < sz; i++)
            {
                Node NewNode = new Node();
                NewNode.Value = Huffman_Tree.Comp_w.ReadInt16();
                if (i == 0)
                    arr.Add(NewNode);
                else if (i != 0)
                {
                    for (int j = 0; j < arr.Count; j++)
                    {
                        if (arr[j].Value == -1)
                        {
                            if (arr[j].Left == null)
                            {
                                arr[j].Left = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                            else if (arr[j].Right == null)
                            {
                                arr[j].Right = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                        }
                    }
                }
            }
            Green_Root = arr[0];

            sz = Huffman_Tree.Comp_w.ReadInt16();
            arr = new List<Node>();

            for (int i = 0; i < sz; i++)
            {
                Node NewNode = new Node();
                NewNode.Value = Huffman_Tree.Comp_w.ReadInt16();
                if (i == 0)
                    arr.Add(NewNode);
                else if (i != 0)
                {
                    for (int j = 0; j < arr.Count; j++)
                    {
                        if (arr[j].Value == -1)
                        {
                            if (arr[j].Left == null)
                            {
                                arr[j].Left = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                            else if (arr[j].Right == null)
                            {
                                arr[j].Right = NewNode;
                                arr.Add(NewNode);
                                break;
                            }
                        }
                    }
                }
            }
            Blue_Root = arr[0];
        }

    }
}
