using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpHeap
{
    public class ArrayMaxHeap<T> : Heap<T> where T : IComparable
    {
        #region Properties
        private T[] Items;
        public override int Count { get; protected set; }
        public override int Level { get; protected set; }
        public override int MaxSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region Ctors and indexers
        public ArrayMaxHeap()
        {
            Count = Level = 0;
            Items = new T[1];
        }

        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return Items[index];
            }
        }
        #endregion

        private Boolean HasElementAtIndex(int index)
        {
            return index < Count ? true : false;
        }

        public int GetLeftChildIndex(int parentIndex)
        {
            if (HasElementAtIndex(parentIndex))
            {
                return 2 * parentIndex + 1;
            }
            throw new IndexOutOfRangeException("Parent at given index does not exist.");
        }

        public int GetRightChildIndex(int parentIndex)
        {
            if (HasElementAtIndex(parentIndex))
            {
                return 2 * parentIndex + 2;
            }
            throw new IndexOutOfRangeException("Parent at given index does not exist.");
        }

        public int GetParentIndex(int childIndex)
        {
            if (HasElementAtIndex(childIndex))
            {
                return (int)Math.Floor((childIndex - 1) / 2.0);
            }
            throw new IndexOutOfRangeException("Child at given index does not exist.");
        }

        public Boolean HasLeftChild(int parentIndex)
        {
            return GetLeftChildIndex(parentIndex) < Count;
        }

        public Boolean HasRightChild(int parentIndex)
        {
            return GetRightChildIndex(parentIndex) < Count;
        }

        public Boolean HasParent(int childIndex)
        {
            return GetParentIndex(childIndex) >= 0;
        }

        public T GetLeftChild(int parentIndex)
        {
            return Items[GetLeftChildIndex(parentIndex)];
        }

        public T GetRightChild(int parentIndex)
        {
            return Items[GetRightChildIndex(parentIndex)];
        }

        public T GetParent(int childIndex)
        {
            return Items[GetParentIndex(childIndex)];
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            (Items[firstIndex], Items[secondIndex]) = (Items[secondIndex], Items[firstIndex]);
        }

        public override T Seek(int index)
        {
            if (index < 0 || index >= Count - 1)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            return Items[index];
        }

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

        private void HeapifyDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                int biggerChildIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index).CompareTo(GetLeftChild(index)) > 0)
                {
                    biggerChildIndex = GetRightChildIndex(index);
                }

                if (Items[index].CompareTo(Items[biggerChildIndex]) > 0)
                {
                    break;
                }
                else
                {
                    Swap(index, biggerChildIndex);
                }
                index = biggerChildIndex;
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

        private void UpdateHeapLevel()
        {
            if (Count == 0) Level = 0;
            else Level = (int)Math.Floor(Math.Log(Count, 2));
        }

        private void HeapifyUp()
        {
            int index = Count - 1;
            while (HasParent(index) && GetParent(index).CompareTo(Items[index]) < 0)
            {
                Swap(GetParentIndex(index), index);
                index = GetParentIndex(index);
            }
        }

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

        public override int Push(T obj, int index)
        {
            throw new NotImplementedException();
        }
    }
}
