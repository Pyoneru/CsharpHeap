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

        public override T Pop()
        {
            throw new NotImplementedException();
        }

        public override int Push(T obj)
        {
            throw new NotImplementedException();
        }

        public override int Push(T obj, int index)
        {
            throw new NotImplementedException();
        }

        public override T Seek(int index)
        {
            throw new NotImplementedException();
        }
    }
}
