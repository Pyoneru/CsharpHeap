using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpHeap
{
    /// <summary>
    /// Provides basic heap functionalities
    /// </summary>
    public abstract class Heap<T> where T : IComparable
    {
        public enum ChildSide
        {
            Left, Right
        }
        /// <summary>
        /// Returns number of elements in heap.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Return levels of current heap.
        /// </summary>
        public abstract int Level { get; }

        /// <summary>
        /// Gets or sets maximum size of heap. If returned value equals to -1, the size is managed dynamically.
        /// </summary>
        public abstract int MaxSize { get; set; }

        /// <summary>
        /// Returns element at given index. This method doesn't delete the element.
        /// </summary>
        /// <param name="index">Used to point location of given element on heap.</param>
        public abstract T Seek(int index);

        /// <summary>
        /// Pushes element into the end of the heap.
        /// </summary>
        /// <param name="obj">Object to be pushed to the end of the heap.</param>
        public abstract int Push(T obj);

        /// <summary>
        /// Pushes element at the given index of the heap.
        /// </summary>
        /// <param name="obj">Object to be pushed onto the heap.</param>
        /// <param name="index">Points location of given element on heap.</param>
        public abstract int Push(T obj, int index);

        /// <summary>
        /// Pops root from the heap.
        /// </summary>
        public abstract T Pop();

        /// <summary>
        /// Gets parent node of current node.
        /// </summary>
        /// <returns>Parent Node</returns>
        public abstract T Parent();

        /// <summary>
        /// Get child node of current node. With given side.
        /// </summary>
        /// <param name="side">Left or Right</param>
        /// <returns>Child Node</returns>
        public abstract T Child(ChildSide side);

        /// <summary>
        /// Get index of current node. If node was deleted return -1
        /// </summary>
        /// <returns>Index of current node</returns>
        public abstract int Current();

        /// <summary>
        /// Jump to selected node at given index
        /// </summary>
        /// <param name="index">Index of node in the heap</param>
        /// <returns>Index of node</returns>
        public abstract int MoveOn(int index);
    }
}
