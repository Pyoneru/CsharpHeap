using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpHeap
{
    public class NodeHeap<T> : Heap<T> where T : IComparable
    {
        public override int Count => throw new NotImplementedException();

        public override int Level => throw new NotImplementedException();

        public override int MaxSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public class Node
        {
            public T Value { get; set; }
            public Node Parent { get; set; }
            public Node ChildLeft { get; set; }
            public Node ChildRight { get; set; }

            public Node()
            {

            }

            public Node(T value)
            {
            }

            public static Node CreateTree(params T[] values)
            {
                return null;
            }

            public Boolean IsHeapNode()
            {
                return false;
            }
        }
    }
}
