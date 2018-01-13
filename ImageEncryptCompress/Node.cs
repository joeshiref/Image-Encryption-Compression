using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageQuantization;
using System.Collections;
using System.IO;


namespace ImageEncryptCompress
{
    public class Node
    {
        public short Value { get; set; }
        public int Freq { get; set; }
        public bool Internal { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }
        public Node Root { get; set; }
        public List<bool> rep;
        public void Traverse(Node node, ref BitArray str, ref List<Tree> bitsInBinary, int idx, ref int sum)
        {
            if (node.Left == null)
            {
                if (node.Internal == false)
                {
                    Tree whole = new Tree();
                    whole.freq = node.Freq;
                    whole.value = node.Value;
                    whole.BinaryBits = new BitArray(idx);
                    whole.Binary_Bits_Test = new List<bool>();
                    node.rep = new List<bool>();
                    for (int i = 0; i < idx; i++)
                    {
                        whole.BinaryBits[i] = str[i];
                        whole.Binary_Bits_Test.Add(str[i]);
                        node.rep.Add(str[i]);
                    }
                    bitsInBinary.Add(whole);
                    sum += whole.BinaryBits.Count;
                }

                return;
            }
            else if (node.Right == null)
            {
                if (node.Internal == false)
                {
                    Tree whole = new Tree();
                    whole.freq = node.Freq;
                    whole.value = node.Value;
                    whole.BinaryBits = new BitArray(idx);
                    for (int i = 0; i < idx; i++)
                    {
                        whole.BinaryBits[i] = str[i];
                        whole.Binary_Bits_Test.Add(str[i]);
                        node.rep.Add(str[i]);

                    }
                    bitsInBinary.Add(whole);
                    sum += whole.BinaryBits.Count;
                }
                return;
            }
            else
            {
                if (node.Left != null)
                {
                    str[idx] = false;
                    Traverse(node.Left, ref str, ref bitsInBinary, idx + 1, ref sum);

                }
                if (node.Right != null)
                {
                    str[idx] = true;
                    Traverse(node.Right, ref str, ref bitsInBinary, idx + 1, ref sum);
                }
                return;
            }
        } //traverse the tree to get binarycode for each value of specific color

        public void BFS(Node source) //traverse on the tree to save it in file to reconstruct it
        {

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(source);
            while (q.Count != 0)
            {
                Node current = new Node();
                current = q.Dequeue();
                Huffman_Tree.Comp_r.Write(current.Value);
                if (current.Left != null)
                    q.Enqueue(current.Left);
                if (current.Right != null)
                    q.Enqueue(current.Right);
            }


        }
    }
}