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

        #endregion Constructors

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

        /// <summary>
        /// If Current points to element not exists, should return -1
        /// </summary>
        [TestMethod]
        public void CurrentInEmptyHeapTest()
        {
            Heap heap = new Heap(MinMax.MAX);

            Assert.AreEqual(-1, heap.Current());
        }

        #endregion Current Method

        #region Child Method

        /// <summary>
        /// Move to left child at frist, back to root element and next move to right child.
        /// </summary>
        [TestMethod]
        public void ChildTest()
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

        /// <summary>
        /// When you move to child which not exists, should return defualt value of type and Current pointer should be stay in the same place
        /// where was before.
        /// </summary>
        [TestMethod]
        public void ChildInEmptyHeapTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(5); // Root element exists

            int child = heap.Child(Heap<int>.ChildSide.Left);

            Assert.AreEqual(default(int), child);

            // If child not exists, Current pointer should not move and stay in the same place.
            Assert.AreEqual(0, heap.Current());
            
        }

        #endregion Child Method

        #region Parent Method

        /// <summary>
        /// Test if Parent method work correct.
        /// </summary>
        [TestMethod]
        public void ParentFromChildTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(1);
            heap.Push(2);
            heap.Push(3);

            heap.Child(Heap.ChildSide.Left);

            int parent = heap.Parent();


            // Parent value from child should be 3(in Max heap)
            Assert.AreEqual(3, parent);


            // Now, Current should points to root(0 index)
            Assert.AreEqual(0, heap.Current());
        }

        /// <summary>
        /// Test Parent method if Parent not exist(e.g. from root node). 
        /// Should return defualt value and Current pointer should stay in the same place.
        /// </summary>
        [TestMethod]
        public void ParentFromRootTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            int parent = heap.Parent(); // Move to parent node from root node

            Assert.AreEqual(default(int), parent);

            // Current pointer should stay on root node.
            Assert.AreEqual(0, heap.Current());
        }

        #endregion Parent Method

        #endregion Moving Methods

        #region Modify Methods

        #region Seek Method

        /// <summary>
        /// If index is correct, Seek method should return value.
        /// </summary>
        [TestMethod]
        public void SeekTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(10);  // Index 0
            heap.Push(9);   // Index 1
            heap.Push(8);   // Index 2
            heap.Push(7);   // Index 3
            heap.Push(6);   // Index 4

            // Check last element.
            Assert.AreEqual(6, heap.Seek(4));

            // Check another element
            Assert.AreEqual(8, heap.Seek(2));
        }

        /// <summary>
        /// If index is incorrect, Seek method should expection.
        /// </summary>
        [TestMethod]
        public void SeekBadIndex()
        {
            Heap heap = new Heap(MinMax.MIN);
            Assert.ThrowsException<NullReferenceException>(
            () =>
            {
                heap.Seek(100);
            });
        }

        #endregion Seek Method

        #region Push Method

        /// <summary>
        /// Check if push update heap
        /// </summary>
        [TestMethod]
        public void PushTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(5);

            Assert.AreEqual(5, heap.Seek(heap.Current()));

            heap.Push(10); // <- this value will be root

            Assert.AreEqual(10, heap.Seek(heap.Current()));

        }

        #endregion Push Method

        #region Pop Method

        /// <summary>
        /// Test Pop Method.
        /// </summary>
        [TestMethod]
        public void PopTest()
        {
            Heap heap = new Heap(MinMax.MIN);
            heap.Push(1);
            heap.Push(2);
            heap.Push(3);
            heap.Push(4);
            heap.Push(5);
            heap.Push(6);
            heap.Push(7);

            int size = heap.Count;

            int pop = heap.Pop();

            // Return root element value
            Assert.AreEqual(1, pop);

            // Count should be less by 1
            Assert.AreEqual(size - 1, heap.Count);

            // New tree
            Assert.AreEqual(2, heap.Seek(0));
            Assert.AreEqual(4, heap.Seek(1));
            Assert.AreEqual(3, heap.Seek(2));
            Assert.AreEqual(7, heap.Seek(3));
            Assert.AreEqual(5, heap.Seek(4));
            Assert.AreEqual(6, heap.Seek(6));

        }

        #endregion Pop Method

        #endregion Modify Methods

        #region Properties

        #region Count Prop

        /// <summary>
        /// Return number of amount elements in heap, his size.
        /// </summary>
        [TestMethod]
        public void CountTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(5);
            heap.Push(2);
            heap.Push(26);
            heap.Push(89);
            heap.Push(105);
            heap.Push(23);

            Assert.AreEqual(6, heap.Count);
        }

        /// <summary>
        /// If Heap not conatains any element, should return 0.
        /// </summary>
        [TestMethod]
        public void CountWithEmptyHeap()
        {
            Heap heap = new Heap(MinMax.MAX);
            Assert.AreEqual(0, heap.Count);
        }

        #endregion Count Prop

        #region Level Prop

        /// <summary>
        /// If heap contains only root element, then Level prop should return 0
        /// </summary>
        [TestMethod]
        public void LevelOnlyRootTest()
        {
            Heap heap = new Heap(MinMax.MIN);
            heap.Push(5);

            Assert.AreEqual(0, heap.Level);

        }

        /// <summary>
        /// If heap do not contains any element, then Level prop should return -1.
        /// </summary>
        [TestMethod]
        public void LevelInEmptyRootTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            Assert.AreEqual(-1, heap.Level);
        }

        /// <summary>
        /// Check if correct level is given with Level prop.
        /// </summary>
        [TestMethod]
        public void LevelTest()
        {
            Heap heap = new Heap(MinMax.MAX);
            heap.Push(5);
            heap.Push(1);
            heap.Push(2);

            Assert.AreEqual(1, heap.Level);

            // Add new element
            heap.Push(6);
            //   6      <- level 0
            //  5 2     <- level 1
            // 1        <- level 2


            Assert.AreEqual(2, heap.Level);
        }

        #endregion Level Prop

        #region MaxSize Prop

        // ToDo: Implements MaxSize Prop in the future

        #endregion MaxSize Prop

        #endregion Properties
    }
}
