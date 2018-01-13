using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageQuantization;


namespace ImageEncryptCompress
{
    public class MinHeap : AbstractHeap
    {
        #region constructors
        public MinHeap() : base()
        {
        }
        #endregion

        #region internal methods
        internal override void heapifyDown()
        {
            int index = 0;
            while (hasLeftChild(index))
            {
                int smallerChildIndex = getLeftChildIndex(index);
                if (hasRightChild(index) && rightChild(index) < leftChild(index))
                {
                    smallerChildIndex = getRightChildIndex(index);
                }
                else if (hasRightChild(index) && rightChild(index) == leftChild(index))
                {
                    if (Nodes[getLeftChildIndex(index)].Internal == false)
                        smallerChildIndex = getLeftChildIndex(index);
                    else
                        swap(getRightChildIndex(index), getLeftChildIndex(index));
                }
                if (Nodes[smallerChildIndex].Freq == Nodes[index].Freq)
                {
                    if (Nodes[smallerChildIndex].Internal == false)
                        swap(index, smallerChildIndex);
                }
                if (Nodes[smallerChildIndex].Freq < Nodes[index].Freq)
                    swap(index, smallerChildIndex);
                else
                    break;
                index = smallerChildIndex;
            }
        }
        internal override void heapifyUp()
        {
            int index = Size - 1;
            while (hasParent(index) && parent(index) >= Nodes[index].Freq)
            {
                if (index == 0)
                    return;
                swap(index, getParentIndex(index));
                index = getParentIndex(index);
            }

        }
        #endregion
    }
}
