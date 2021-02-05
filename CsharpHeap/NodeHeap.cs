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
            #region Properties

            /// <summary>
            /// Value of Node
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Reference to Parent Node
            /// </summary>
            public Node Parent { get; set; }

            /// <summary>
            /// Refernece to Left Child Node
            /// </summary>
            public Node ChildLeft { get; set; }

            /// <summary>
            /// Reference to Right Child Node
            /// </summary>
            public Node ChildRight { get; set; }

            #endregion

            #region Constructors

            /// <summary>
            /// Empty constructor, do nothig, set nothig, all properties will be null.
            /// </summary>
            public Node() {}

            /// <summary>
            /// Constructor with type T parameter. Create node with given value.
            /// </summary>
            /// <param name="value">Set Value properties with given value.</param>
            public Node(T value)
            {
                this.Value = value;
            }

            #endregion

            #region Static_Methods

            /// <summary>
            /// Create Node tree based on values in array.
            /// </summary>
            /// <param name="values">Values in array</param>
            /// <returns>Values mapped to Node tree</returns>
            public static Node CreateTree(params T[] values)
            {
                Node root = null;
                if (values.Length > 0) { // If values have any element
                    root = new Node(values[0]); // create root node
                    MakeChildren(root, 0, values); // Start setting children in tree
                }
                return root; // Return root element
            }
            
            /// <summary>
            /// Auxilary recurisve method. Create children and set they value based on array.
            /// </summary>
            /// <param name="node">The Node we place the children on</param>
            /// <param name="k">Index of node in array. We need it to get children values.</param>
            /// <param name="values">Array with values.</param>
            private static void MakeChildren(Node node, int k, T[] values)
            {
                // Left child formula: 2k+1
                if(2 * k + 1 < values.Length)
                {
                    node.ChildLeft = new Node(values[2 * k + 1]); // Create left child node
                    node.ChildLeft.Parent = node; // Set parent for left child node
                    MakeChildren(node.ChildLeft, 2 * k + 1, values); // Go to next children in tree
                }
                // Right child formula: 2k+2
                if(2 * k + 2 < values.Length)
                {
                    node.ChildRight = new Node(values[2 * k + 2]); // Create right child node
                    node.ChildRight.Parent = node; // Set parent for right child node
                    MakeChildren(node.ChildRight, 2 * k + 2, values);// Go to next children in tree
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Check if node is part of Heap tree. That's mean if right child exists, left child must exists too, 
            /// otherwise right child must be null. Can not be situation where right child exist but left child not exists.
            /// </summary>
            /// <returns>True if this node is part of heap tree, false in otherwaise</returns>
            public Boolean IsHeapNode()
            {
                return 
                    (ChildRight != null && ChildLeft != null) // If Right child exist, left child must exists too
                    ||
                    (ChildRight == null); // Otherwise child right must be null.
            }

            #endregion
        }
    }
}
