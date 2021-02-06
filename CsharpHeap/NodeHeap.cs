﻿using System;
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
        protected Node root;
        protected Node current;
        protected int lastIndex;

        public override int Count { get; protected set; }
        public override int Level { get; protected set; }

        public override int MaxSize { 
            get => throw new NotImplementedException("Proszę tego nie używać"); 
            set => throw new NotImplementedException("Proszę tego nie używać"); 
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                             // Joke

        public NodeHeap(MinMax minMax)
        {
            this.minMax = minMax;
            this.current = root;
            this.Count = 0;
            this.Level = -1;
            this.lastIndex = -1;
        }

        public T this[int index]
        {
            get {
                if (index == 0)
                    return root.Value;
                else
                {
                    int[] path = PathFromRootToIndex(index);

                    Node node = MoveToNodeByPath(path);
                    return node.Value;
                }
            }
        }

        private int[] PathFromRootToIndex(int index)
        {
            int level = (int)Math.Log(
                (index %2 == 1) ? index +1 : index, 
                2
                );
            int[] path = new int[level]; // if number is even then move right, otherwise move left

            path[level - 1] = index;
            for (int i = level - 2; i >= 0; i--)
            {
                path[i] = GetParentIndex(path[i + 1]);
            }
            return path;
        }

        private Node MoveToNodeByPath(int[] path)
        {
            Node mover = root;
            for (int i = 0; i < path.Length; i++)
            {
                // If value is even then move to right child, if is odd, move to left child
                if (path[i] % 2 == 0) 
                    mover = mover.ChildRight;
                else 
                    mover = mover.ChildLeft;
            }
            return mover;
        }

        private int GetParentIndex(int index)
        {
            return (int)Math.Floor((index - 1) / 2.0);
        }

        public override T Child(ChildSide side)
        {
            if(side == ChildSide.Left && current.ChildLeft != null)
            {
                current = current.ChildLeft;
                return current.Value;
            }else if(current.ChildRight != null)
            {
                current = current.ChildRight;
                return current.Value;
            }
            return default(T);
        }

        public override int Current()
        {
            if(current != null)
                return this.current.Index;
            return -1;
        }

        public override int MoveOn(int index)
        {
            throw new NotImplementedException("Proszę tego nie używać.");
        }

        public override T Parent()
        {
            if (current != null)
            {
                if (current.Parent != null)
                {
                    current = current.Parent;
                    return current.Value;
                }
            }
            return default(T);
        }

        public override T Pop()
        {
            if(root != null)
            {
                if(Count == 1)
                {
                    T value = root.Value;
                    root = null;
                    Count--;
                    Level = -1;
                    lastIndex = -1;
                    return value;
                }
                else
                {
                    T value = root.Value;
                    int[] path = PathFromRootToIndex(lastIndex);
                    Node node = MoveToNodeByPath(path);

                    (root.Value, node.Value) = (node.Value, root.Value);
                    if(lastIndex % 2 == 0)
                    {
                        node.Parent.ChildRight = null;
                    }
                    else
                    {
                        node.Parent.ChildLeft = null;
                    }

                    Node nextParent = root;
                    Node child;
                    do
                    {
                        if (nextParent.ChildLeft == null) break;
                        child = nextParent.ChildLeft;
                        if (minMax == MinMax.MAX)
                        {
                            if (nextParent.ChildRight != null &&
                                nextParent.ChildRight.Value.CompareTo(child.Value) > 0)
                            {
                                child = nextParent.ChildRight;
                            }
                        }
                        else
                        {
                            if (nextParent.ChildRight != null &&
                                nextParent.ChildRight.Value.CompareTo(child.Value) < 0)
                            {
                                child = nextParent.ChildRight;
                            }
                        }
                        nextParent = child;

                    } while (Swap(child));

                    Count--;
                    lastIndex--;
                    UpdateLevel();
                    return value;
                }

            }
            return default(T);
        }

        public override int Push(T obj)
        {
            if (root == null)
            {
                root = new Node(++lastIndex, obj);
                current = root;
                Count++;
                UpdateLevel();
            }
            else
            {
                Node last;
                if (Count > 2)
                {
                    int[] path = PathFromRootToIndex(GetParentIndex(lastIndex + 1));
                    last = MoveToNodeByPath(path);
                }
                else
                {
                    last = root;
                }
                Node node;
                if (last.ChildLeft == null)
                {
                    last.ChildLeft = new Node(++lastIndex, obj);
                    last.ChildLeft.Parent = last;
                    node = last.ChildLeft;
                }
                else 
                {
                    last.ChildRight = new Node(++lastIndex, obj);
                    last.ChildRight.Parent = last;
                    node = last.ChildRight;
                }
                
                Count++;
                UpdateLevel();
                while (Swap(node))
                {
                     if (node.Parent != null)
                        node = node.Parent;
                     else break;
                }
            }


            return 0;
        }

        public override int Push(T obj, int index)
        {
            throw new NotImplementedException("Proszę tego nie używać.");
        }

        private bool Swap(Node target)
        {
            if (target.Parent == null) return false;
            if(minMax == MinMax.MAX)
            {
                Node parent = target.Parent;
                if(target.Value.CompareTo(parent.Value) > 0)
                {
                    (parent.Value, target.Value) = (target.Value, parent.Value);
                }
                else return false;
            }
            else
            {
                Node parent = target.Parent;
                if(target.Value.CompareTo(parent.Value) < 0)
                {
                    (parent.Value, target.Value) = (target.Value, parent.Value);
                }
                else return false;
            }

            return true;
        }

        private Node MoveToEndNode()
        {
            Node node = root;

            while (node.ChildRight != null) node = node.ChildRight;
            if (node.ChildLeft != null) node = node.ChildLeft;

            return node;
        }

        private void UpdateLevel()
        {
            if(root != null)
            {
                Node node = root;
                int lvl = 0;
                while(node.ChildLeft != null)
                {
                    node = node.ChildLeft;
                    lvl++;
                }
                Level = lvl;
            }
        }

        public override T Seek(int index)
        {
            if (index != 0)
            {
                int[] path = PathFromRootToIndex(index);

                Node node = MoveToNodeByPath(path);
                return node.Value;
            }
            return root.Value;
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
            /// Create Node root based on values in array.
            /// </summary>
            /// <param name="values">Values in array</param>
            /// <returns>Values mapped to Node root</returns>
            public static Node CreateTree(params T[] values)
            {
                Node root = null;
                if (values.Length > 0) { // If values have any element
                    root = new Node(0, values[0]); // create root node
                    MakeChildren(root, 0, values); // Start setting children in root
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
                    MakeChildren(node.ChildLeft, 2 * k + 1, values); // Go to next children in root
                }
                // Right child formula: 2k+2
                if(2 * k + 2 < values.Length)
                {
                    node.ChildRight = new Node(2 * k + 2, values[2 * k + 2]); // Create right child node
                    node.ChildRight.Parent = node; // Set parent for right child node
                    MakeChildren(node.ChildRight, 2 * k + 2, values);// Go to next children in root
                }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Check if node is part of Heap root. That's mean if right child exists, left child must exists too, 
            /// otherwise right child must be null. Can not be situation where right child exist but left child not exists.
            /// </summary>
            /// <returns>True if this node is part of heap root, false in otherwaise</returns>
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