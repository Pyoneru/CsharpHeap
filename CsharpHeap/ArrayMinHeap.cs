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
        /// <summary>
        /// Returns number of elements contained within the heap.
        /// </summary>
        public override int Count { get; protected set; }

        /// <summary>
        /// An internal array holding all elements contained within the heap.
        /// </summary>
        private T[] Items;

        /// <summary>
        /// Returns level of the heap.
        /// </summary>
        public override int Level { get; protected set; }

        /// <summary>
        /// Maximum size for future static implementation. Do not use.
        /// </summary>
        public override int MaxSize { 
            get => throw new NotImplementedException("Proszę tego nie używać.");
            set => throw new NotImplementedException("Proszę tego nie używać.");
        }
        #endregion

        #region Constructors and indexers
        /// <summary>
        /// Initializes a new, empty instance of ArrayMinHeap class.
        /// </summary>
        public ArrayMinHeap()
        {
            this.Count = 0;
            this.Level = 0;
            Items = new T[1];
        }

        /// <summary>
        /// Gets element associated with the specified index.
        /// </summary>
        /// <param name="index">Index of item to be returned.</param>
        /// <returns></returns>
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
        /// Updates heap internal level property.
        /// </summary>
        private void UpdateHeapLevel()
        {
            if (Count == 0) Level = 0;
            else Level = (int)Math.Floor(Math.Log(Count, 2));
        }
        /// <summary>
        /// Returns value indicating whether a node at given index exists.
        /// </summary>
        /// <param name="index">Index of item to check.</param>
        private Boolean HasElementAtIndex(int index)
        {
            return index < Count ? true : false;
        }

        /// <summary>
        /// Returns index of left side child if it exists.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public int GetLeftChildIndex(int parentIndex)
        {
            if(HasElementAtIndex(parentIndex))
            {
                return 2 * parentIndex + 1;
            }
            throw new IndexOutOfRangeException("Parent at given index does not exist.");
        }

        /// <summary>
        /// Returns index of right side child if it exists.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public int GetRightChildIndex(int parentIndex)
        {
            if (HasElementAtIndex(parentIndex))
            {
                return 2 * parentIndex + 2;
            }
            throw new IndexOutOfRangeException("Parent at given index does not exist.");
        }

        /// <summary>
        /// Returns parent of a child at given index if it exists.
        /// </summary>
        /// <param name="childIndex">Index of a child.</param>
        public int GetParentIndex(int childIndex)
        {
            if (HasElementAtIndex(childIndex))
            {
                return (int)Math.Floor((childIndex - 1) / 2.0);
            }
            throw new IndexOutOfRangeException("Child at given index does not exist.");
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
        /// Returns value indicating whether a parent has a left child.
        /// </summary>
        /// <param name="parentIndex">Index of a parent.</param>
        public Boolean HasRightChild(int parentIndex)
        {
            return GetRightChildIndex(parentIndex) < Count;
        }

        /// <summary>
        /// Returns value indicating whether a child has a parent.
        /// </summary>
        /// <param name="childIndex">Index of a child.</param>
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
            if (Count == Items.Length)
            {
                T[] expandedArray = new T[2 * Items.Length];
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
                throw new InvalidOperationException("Heap does not contain elements to be popped");
            }
            T root = Items[0];
            Items[0] = Items[Count - 1];
            Count--;
            HeapifyDown();
            UpdateHeapLevel();
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
                UpdateHeapLevel();
                return 0; // return 0 whether HeapifyUp was successful
            }
            catch
            {
                return 1; // return 1 whether HeapifyUp thrown an error
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
                if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) < 0)
                {
                    smallerChildIndex = GetRightChildIndex(index);
                }

                if (Items[index].CompareTo(Items[smallerChildIndex]) < 0)
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
