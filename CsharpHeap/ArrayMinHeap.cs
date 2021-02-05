using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CsharpHeap
{
    class ArrayMinHeap<T> : Heap<T> where T : IComparable
    {
        #region Properties
        public override int Count { get; protected set; }

        public override int MaxSize { get; set; }

        private T[] Items { get; set; }

        public override int Level { get; protected set; }
        #endregion

        #region Constructors and indexers
        public ArrayMinHeap()
        {
            this.Count = 0;
            this.MaxSize = 1;
            Items = new T[MaxSize];
        }

        public ArrayMinHeap(int maxSize)
        {
            this.Count = 0;
            this.MaxSize = maxSize;
            Items = new T[MaxSize];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                return Items[index];
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns possible index of left side child. Requires boundary check.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public int GetLeftChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 1;
        }

        /// <summary>
        /// Returns possible index of right side child. Requires boundary check.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public int GetRightChildIndex(int parentIndex)
        {
            return 2 * parentIndex + 2;
        }

        /// <summary>
        /// Returns possible parent of a child at given index. Requires checking if child has parent.
        /// </summary>
        /// <param name="childIndex">Index of a child.</param>
        public int GetParentIndex(int childIndex)
        {
            return (int)Math.Floor((childIndex - 1) / 2.0);
        }

        /// <summary>
        /// Returns value indicating whether a parent has a left child.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public Boolean HasLeftChild(int parentIndex)
        {
            return GetLeftChildIndex(parentIndex) < Count;
        }

        /// <summary>
        /// Returns value indicating whether a parent has a right child.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public Boolean HasRightChild(int parentIndex)
        {
            return GetRightChildIndex(parentIndex) < Count;
        }

        /// <summary>
        /// Returns value indicating whether a parent has a right child.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public Boolean HasParent(int childIndex)
        {
            return GetParentIndex(childIndex) >= 0;
        }

        /// <summary>
        /// Returns left child item of a parent.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public T GetLeftChild(int parentIndex)
        {
            return Items[GetLeftChildIndex(parentIndex)];
        }

        /// <summary>
        /// Returns right child item of a parent.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public T GetRightChild(int parentIndex)
        {
            return Items[GetRightChildIndex(parentIndex)];
        }

        /// <summary>
        /// Returns parent node.
        /// </summary>
        /// <param name="childIndex">Index of a child.</param>
        public T GetParent(int childIndex)
        {
            return Items[GetParentIndex(childIndex)];
        }

        /// <summary>
        /// Swaps items at two indices.
        /// </summary>
        /// <param name="firstIndex">First index to swap value at.</param>
        /// <param name="secondIndex">Second index to swap value at.</param>
        private void Swap(int firstIndex, int secondIndex)
        {
            (Items[firstIndex], Items[secondIndex]) = (Items[secondIndex], Items[firstIndex]);
        }

        /// <summary>
        /// Doubles the size of underlying array structure, if size of current heap reached maximum capacity
        /// </summary>
        private void ExpandHeap()
        {
            if(Count == MaxSize)
            {
                MaxSize = MaxSize * 2;
                T[] expandedArray = new T[MaxSize];
                for (int i = 0; i < Count; i++)
                {
                    expandedArray[i] = Items[i];
                }
                Items = expandedArray;
            }
        }

        /// <summary>
        /// Returns an item at given index.
        /// </summary>
        /// <param name="index">Index of item to be returned.</param>
        /// <returns></returns>
        public override T Seek(int index)
        {
            if (index < 0 || index >= Count - 1)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            return Items[index];
        }

        /// <summary>
        /// Removes root element, replace with last one and shift it down to maintain the minimum heap structure.
        /// </summary>
        public override T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Heap does not contain elements to be Popped");
            }
            T root = Items[0];
            Items[0] = Items[Count - 1];
            Count--;
            HeapifyDown();
            return root;
        }

        /// <summary>
        /// Adds element to the heap.
        /// </summary>
        /// <param name="item">Object to be added onto the heap.</param>
        public override int Push(T item)
        {
            ExpandHeap();
            Items[Count] = item;
            Count++;
            try
            {
                HeapifyUp();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets smaller values of children, if parent has lower value than smaller of children, then breaks, else swaps values and moves to another parent.
        /// </summary>
        private void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int smallerChildIndex = GetLeftChildIndex(index);
                if(HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }
                
                if(Items[index].CompareTo(Items[smallerChildIndex]) < 0)
                {
                    break;
                }
                else
                {
                    Swap(index, smallerChildIndex);
                }
                index = smallerChildIndex;
            }
        }

        /// <summary>
        /// Swaps new added value upwards by comparing next parents till heap requirement is fullfilled.
        /// </summary>
        private void HeapifyUp()
        {
            int index = Count - 1;
            while (HasParent(index) && GetParent(index).CompareTo(Items[index]) > 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

        public override T Child(ChildSide side)
        {
            throw new NotImplementedException();
        }

        public override int Current()
        {
            throw new NotImplementedException();
        }

        public override int MoveOn(int index)
        {
            throw new NotImplementedException();
        }

        public override T Parent()
        {
            throw new NotImplementedException();
        }

        public override int Push(T obj, int index)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
