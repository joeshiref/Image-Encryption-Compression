using ImageQuantization;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ImageEncryptCompress
{
    public struct Tree
    {
        public long freq;
        public long value;
        public BitArray BinaryBits;
        public List<bool> Binary_Bits_Test;
    };
    public class Huffman_Tree
    {
        public int len_of_red = 0, len_of_green = 0, len_of_blue = 0;
        public StreamWriter myfile;
        public StreamWriter Binary_R, Binary_G, Binary_B;
        public static double CompressionRatio = 0;
        public static double Bytes;
        public int[,] freq;
        public int Height = 0;
        public int Width = 0;
        public static short szR = 0;
        public static short szG = 0;
        public static short szB = 0;
        public static FileStream Comp;
        public static BinaryWriter Comp_r;
        public static BinaryReader Comp_w;



        public static List<bool>[] Mapping_Red, Mapping_Green, Mapping_Blue;


        public void FreqMatrix(RGBPixel[,] ImageMatrix) //count the freq of each color O(N^2) N is dimention of image
        {


            Mapping_Red = new List<bool>[256];
            Mapping_Green = new List<bool>[256];
            Mapping_Blue = new List<bool>[256];
            freq = new int[260, 3];
            Height = MainForm.Height;
            Width = MainForm.Width;

            for (int i = 0; i < Height; i++) //O(H) where H is the height of image
            {
                for (int j = 0; j < Width; j++) //O(W) where W is the height of image
                {
                    freq[Program.OriginalImage[i, j].red, 0]++; //O(1)
                    freq[Program.OriginalImage[i, j].green, 1]++; //O(1)
                    freq[Program.OriginalImage[i, j].blue, 2]++; //O(1)
                }
            }

            GetInHeap();
        }

        public long CompressedValue(ref List<Tree> bitsInBinary) //calculates the compression value
        {
            long SS = 0;
            foreach (Tree bits in bitsInBinary) //O(K)
            {
                SS += (bits.BinaryBits.Count * bits.freq);
            }
            return SS;
        }
        public void Saveinfile(ref List<Tree> arr, int color) //writes the data of the external colors in a file to use em in data grid view
        {

            myfile.WriteLine(arr.Count.ToString());
            for (int i = 0; i < arr.Count; i++) //O(K) where k is the is the number of nodes 
            {
                myfile.WriteLine(arr[i].value.ToString());
                myfile.WriteLine(arr[i].freq.ToString());
                myfile.WriteLine((arr[i].BinaryBits.Count * arr[i].freq).ToString());
            }

        }
        public void GetInHeap() //it makes the huffman tree for each color
        {


            File.Delete("Compress.txt");
            myfile = new StreamWriter("Compress.txt", true);

            List<Tree> bitsInBinary = new List<Tree>();
            bitsInBinary = InsertInHeap(0);
            Get_stream(ref bitsInBinary, 0);
            CompressionRatio = CompressedValue(ref bitsInBinary);
            Saveinfile(ref bitsInBinary, 0);

            bitsInBinary = InsertInHeap(1);
            Get_stream(ref bitsInBinary, 1);
            CompressionRatio += CompressedValue(ref bitsInBinary);
            Saveinfile(ref bitsInBinary, 1);

            bitsInBinary = InsertInHeap(2);
            Get_stream(ref bitsInBinary, 2);
            CompressionRatio += CompressedValue(ref bitsInBinary);
            Saveinfile(ref bitsInBinary, 2);

            Bytes = CompressionRatio / 8.0;
            double p = Width * Height * 24;
            CompressionRatio = CompressionRatio / p;
            CompressionRatio *= 100;

            myfile.Close();



        }
        public void Get_stream(ref List<Tree> arr, int color) //it takes the color and the external nodes and preprocess the binarycode of each value
        {
            for (int i = 0; i < arr.Count; i++) //O(K) where k is the number of nodes, at worst case 255
            {
                if (color == 0)
                    Mapping_Red[arr[i].value] = arr[i].Binary_Bits_Test;
                else if (color == 1)
                    Mapping_Green[arr[i].value] = arr[i].Binary_Bits_Test;
                else
                    Mapping_Blue[arr[i].value] = arr[i].Binary_Bits_Test;
            }
        }


        public List<Tree> InsertInHeap(int color) //gets everything ready for specific color O(N)
        {
            MinHeap priorityQueue = new MinHeap();
            priorityQueue = GetpriorityQueue(color); //Makes priority queue of each color O(K*LogK)           
            // where K is the number of nodes
            Node rootNode = new Node();
            rootNode = RootOfTree(priorityQueue, color); //get the root the tree O(K*logK)
            rootNode.BFS(rootNode); //traverse the tree to save it in file O(N)
            List<Tree> bitsInBinary = new List<Tree>();
            BitArray arr = new BitArray(100);
            int cnt = 0;
            rootNode.Traverse(rootNode, ref arr, ref bitsInBinary, 0, ref cnt); //traverse the tree to get binarycode for each value of specific color
            if (color == 0)
                len_of_red = cnt;
            else if (color == 1)
                len_of_green = cnt;
            else
                len_of_blue = cnt;
            return bitsInBinary;
        }
        public MinHeap GetpriorityQueue(int color) //iterate over all values of specific color and insert values which appeared in the picture
        {
            //create pri_queue of the color
            MinHeap priorityQueue = new MinHeap();
            for (short i = 0; i <= 255; i++) //O(1)
            {
                Node node = new Node();
                if (freq[i, color] != 0)
                {
                    node.Freq = freq[i, color];
                    node.Internal = false;
                    node.Value = i;
                    node.Right = node.Left = node.Root = null;
                    priorityQueue.add(node); // O(log n)
                }
            }
            return priorityQueue;
        }

        public Node RootOfTree(MinHeap priorityQueue, int color) //gets the root of the tree
        {

            short sz = 0;
            if (color == 0)
                sz = szR = priorityQueue.Size;
            if (color == 1)
                sz = szG = priorityQueue.Size;
            if (color == 2)
                sz = szB = priorityQueue.Size;
            Node firstNode;
            Node rootNode = new Node();
            //makes some insertions and deletions to get the root of the tree
            while (priorityQueue.Size >= 1) //O(KlogK) where k is the number of nodes
            {
                firstNode = priorityQueue.pop(); //O(logK)
                rootNode = firstNode;
                if (priorityQueue.Size != 0)
                {

                    Node secondNode = priorityQueue.pop(); //O(logK)                  
                    Node newNode = new Node();
                    newNode.Freq = firstNode.Freq + secondNode.Freq;
                    newNode.Internal = true;
                    newNode.Value = -1;
                    newNode.Left = secondNode;
                    newNode.Right = firstNode;
                    firstNode.Root = newNode;
                    secondNode.Root = newNode;
                    priorityQueue.add(newNode); //O(logK)
                    sz++;
                }
                else
                {
                    Huffman_Tree.Comp_r.Write(sz);

                    return rootNode;
                }
            }
            Huffman_Tree.Comp_r.Write(sz);

            return rootNode;
        }



    }
}