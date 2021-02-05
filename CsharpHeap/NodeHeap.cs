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
                this.Value = value;
            }

            public static Node CreateTree(params T[] values)
            {
                Node root = null;
                if (values.Length > 0) {
                    root = new Node(values[0]);
                    MakeChildren(root, 0, values);
                }
                return root;
            }

            private static void MakeChildren(Node node, int k, T[] values)
            {
                // Left child formula: 2k+1
                if(2 * k + 1 < values.Length)
                {
                    node.ChildLeft = new Node(values[2 * k + 1]);
                    node.ChildLeft.Parent = node;
                    MakeChildren(node.ChildLeft, 2 * k + 1, values);
                }
                // Right child forumla: 2k+2
                if(2 * k + 2 < values.Length)
                {
                    node.ChildRight = new Node(values[2 * k + 2]);
                    node.ChildRight.Parent = node;
                    MakeChildren(node.ChildRight, 2 * k + 2, values);
                }
            }

            public Boolean IsHeapNode()
            {
                return 
                    (ChildRight != null && ChildLeft != null)
                    ||
                    (ChildRight == null);
            }
        }
    }
}
