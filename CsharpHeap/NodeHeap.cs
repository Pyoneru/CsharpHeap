using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpHeap
{
    public enum MinMax
    {
        MIN, MAX
    }

    public class NodeHeap<T> : Heap<T> where T : IComparable
    {
        protected MinMax minMax;
        protected Node tree;
        protected Node current;

        public override int Count { get; protected set; }
        public override int Level { get; protected set; }

        public override int MaxSize { 
            get => throw new NotImplementedException("Proszę tego nie używać"); 
            set => throw new NotImplementedException("Proszę tego nie używać"); 
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                             // Joke

        public NodeHeap(MinMax minMax)
        {
            this.minMax = minMax;
            this.tree = new Node(0);
            this.current = tree;
            this.Count = 0;
            this.Level = 0;
        }

        public T this[int index]
        {
            get {
                if (index == 0)
                    return tree.Value;
                else
                {
                    if (index % 2 == 1) index++;
                    int level = (int)Math.Log(index, 2);
                    int[] path = new int[level]; // if number is even then move right, otherwise move left

                    path[level - 1] = index;
                    for(int i = level-2; i >= 0; i--)
                    {
                        path[i] = GetParentIndex(path[i + 1]);
                    }

                    Node mover = tree;
                    for(int i = 0; i < level; i++)
                    {
                        // If value is even then move to right child, if is odd, move to left child
                        if (path[i] % 2 == 0) mover = mover.ChildRight;
                        else mover = mover.ChildLeft;
                    }
                    return mover.Value;
                }
            }
        }

        private int GetParentIndex(int index)
        {
            return (int)Math.Floor((index - 1) / 2.0);
        }

        public override T Child(ChildSide side)
        {
            throw new NotImplementedException();
        }

        public override int Current()
        {
            throw new NotImplementedException("");
        }

        public override int MoveOn(int index)
        {
            throw new NotImplementedException("Proszę tego nie używać.");
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
            throw new NotImplementedException("Proszę tego nie używać.");
        }

        public override T Seek(int index)
        {
            throw new NotImplementedException();
        }

        public class Node
        {
            #region Properties

            public int Index { get; set; }
            
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
            public Node(int index) {
                this.Index = index;
            }

            /// <summary>
            /// Constructor with type T parameter. Create node with given value.
            /// </summary>
            /// <param name="value">Set Value properties with given value.</param>
            public Node(int index, T value) : this(index)
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
                    root = new Node(0, values[0]); // create root node
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
                    node.ChildLeft = new Node(2 * k + 1,values[2 * k + 1]); // Create left child node
                    node.ChildLeft.Parent = node; // Set parent for left child node
                    MakeChildren(node.ChildLeft, 2 * k + 1, values); // Go to next children in tree
                }
                // Right child formula: 2k+2
                if(2 * k + 2 < values.Length)
                {
                    node.ChildRight = new Node(2 * k + 2, values[2 * k + 2]); // Create right child node
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
