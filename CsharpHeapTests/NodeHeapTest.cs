using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpHeap;

using Heap = CsharpHeap.NodeHeap<int>;

namespace CsharpHeapTests
{

    [TestClass]
    public class NodeHeapTest
    {
        // Alias for Heap is above.
        // using Heap = CsharpHeap.NodeHeap<int>;

        #region Constrcutors

        /// <summary>
        /// The lowest value in heap should be root in Min NodeHeap.
        /// </summary>
        [TestMethod]
        public void ConstructorMinTest()
        {
            Heap heap = new Heap(MinMax.MIN);
            heap.Push(10);
            heap.Push(9);
            heap.Push(98);
            heap.Push(2);

            int root = heap[0]; // Element in 0 index place is a root.
            Assert.AreEqual(2, root);
        }

        /// <summary>
        /// The biggest value in heap should be root in Max NodeHeap.
        /// </summary>
        [TestMethod]
        public void ConstructorMaxTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(1);
            heap.Push(2);
            heap.Push(3);
            heap.Push(4);
            heap.Push(5);
            heap.Push(6);
            heap.Push(7);
            heap.Push(8);
            heap.Push(9);
            heap.Push(10);

            int root = heap[0]; // Element in 0 index place is a root.
            Assert.AreEqual(10, root);
        }

        #endregion

        #region Moving Methods

        #region Current Method

        /// <summary>
        /// After instaced NodeHeap Current method should return 0(points to root element).
        /// </summary>
        [TestMethod]
        public void CurrentAfterInstancedTest()
        {
            Heap heap = new Heap(MinMax.MIN);

            Assert.AreEqual(0, heap.Current());
        }

        #endregion

        #region Child Method

        [TestMethod]
        public void ChildTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(5);
            heap.Push(1);
            heap.Push(3);
            heap.Child(Heap.ChildSide.Left); // 0 -> Left Child 1 -> Right Child

            Assert.AreEqual(1, heap.Current());

            // Right child
            heap.Parent();
            heap.Child(Heap.ChildSide.Right);

            Assert.AreEqual(2, heap.Current());

        }

        [TestMethod]
        public void ChildReturnTest()
        {
            Heap heap = new Heap(MinMax.MIN);
            heap.Push(2);
            heap.Push(3);
            heap.Push(1);

            // Left child
            Assert.AreEqual(3, heap.Child(Heap.ChildSide.Left));

            // Back to root
            Assert.AreEqual(1, heap.Parent());

            // Right child
            Assert.AreEqual(2, heap.Child(Heap.ChildSide.Right));
        }



        #endregion

        #region Parent Method

        [TestMethod]
        public void ParentFromChildTest()
        {

        }

        #endregion

        #endregion


    }
}
