using ImageEncryptCompress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageQuantization;


namespace ImageEncryptCompress
{

    public abstract class AbstractHeap
    {
        #region internal properties
        private int Capacity { get; set; }
        public short Size { get; set; }
        internal Node[] Nodes;
        #endregion

        #region constructors
        public AbstractHeap()
        {
            Capacity = 100;
            Size = 0;
            Nodes = new Node[Capacity];
        }
        #endregion

        #region helperMethods
        public void EnlargeIfNeeded()
        {
            if (Size == Capacity)
            {

                Capacity = 2 * Capacity;
                Node[] temp = new Node[Size];
                Array.Copy(Nodes, temp, Size);
                Nodes = new Node[Capacity];
                Array.Copy(temp, Nodes, Size);

            }
        }

        public int getLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        public bool hasLeftChild(int parentIndex)
        {
            return getLeftChildIndex(parentIndex) < Size;
        }

        public long leftChild(int index)
        {
            return Nodes[getLeftChildIndex(index)].Freq;
        }

        public int getRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        public bool hasRightChild(int parentIndex)
        {
            return getRightChildIndex(parentIndex) < Size;
        }

        public long rightChild(int index)
        {
            return Nodes[getRightChildIndex(index)].Freq;
        }

        public int getParentIndex(int childIndex)
        {
            return (childIndex - 1) / 2;
        }

        public bool hasParent(int childIndex)
        {
            return getParentIndex(childIndex) >= 0;
        }

        public long parent(int index)
        {
            return Nodes[getParentIndex(index)].Freq;
        }

        public void swap(int index1, int index2)
        {
            Node temp = Nodes[index1];
            Nodes[index1] = Nodes[index2];
            Nodes[index2] = temp;
        }

        #endregion

        #region available public methods

        public Node pop()
        {
            if (Size == 0)
                throw new InvalidOperationException("Heap is empty");

            Node item = new Node();
            item = Nodes[0];
            Nodes[0] = Nodes[Size - 1];
            Nodes[Size - 1] = null;
            Size--;
            heapifyDown();
            return item;
        }

        /// <summary>
        /// Add a new item to heap, enlarging the array if needed
        /// </summary>
        /// <returns>void</returns>
        public void add(Node item)
        {
            EnlargeIfNeeded();
            Node temp = new Node();
            temp.Freq = item.Freq;
            temp.Internal = item.Internal;
            temp.Value = item.Value;
            temp.Right = item.Right;
            temp.Left = item.Left;
            temp.Root = item.Root;
            Nodes[Size] = temp;
            Size++;
            heapifyUp();
        }
        #endregion

        #region abstract methods
        internal abstract void heapifyUp();
        internal abstract void heapifyDown();
        #endregion
    }

}
