using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpHeap;

using Node = CsharpHeap.NodeHeap<int>.Node;

namespace CsharpHeapTests
{
    /// <summary>
    /// Test class for testing internal Node class in NodeHeap class.
    /// All tests using int type as generic T type.
    /// </summary>
    [TestClass]
    public class NodeHeapNodeTest
    {

        /*
         * Allias for NodeHeap<int>.Node above.
         * using Node = CsharpHeap.NodeHeap<int>.Node;
         */

        #region Constructor Tests
        
        /// <summary>
        /// Value proprtie should be set to given value in constructor.
        /// </summary>
        [TestMethod]
        public void ArgConstructorNodeTest()
        {
            Node node = new Node(5);

            Assert.AreEqual(5, node.Value);
        }

        /// <summary>
        /// Constructor without any parameters.
        /// Value proprties should be null(default)
        /// </summary>
        [TestMethod]
        public void EmptyContructorNodeTest()
        {
            Node node = new Node();

            Assert.AreEqual(default(int), node.Value);
        }

        #endregion

        #region CreateTree Tests

        /// <summary>
        /// Prepare array with values and create Node Tree based on this array with CreateTree method.
        /// </summary>
        [TestMethod]
        public void CreateTreeTest()
        {
            int[] val = {5, 3, 5, 1, 6, 9, 2, 4, 7, 8 };

            Node tree = Node.CreateTree(val);

            // Root
            Assert.AreEqual(val[0], tree.Value);

            // Root Left Child
            Assert.AreEqual(val[1], tree.ChildLeft.Value);

            // Root Right Child
            Assert.AreEqual(val[2], tree.ChildRight.Value);

            Node left = tree.ChildLeft;
            // Left branch test
            Assert.AreEqual(val[3], left.ChildLeft.Value);
            Assert.AreEqual(val[4], left.ChildRight.Value);

            Node right = tree.ChildRight;
            // Right branch test
            Assert.AreEqual(val[5], right.ChildLeft.Value);
            Assert.AreEqual(val[6], right.ChildRight.Value);

            left = left.ChildLeft;
            // left->left branch
            Assert.AreEqual(val[7], left.ChildLeft.Value); // ->left
            Assert.AreEqual(val[8], left.ChildRight.Value); // ->right

            right = left.Parent.ChildRight;
            // left->right->left branch
            Assert.AreEqual(val[9], right.ChildLeft.Value);

            // left->right->right should be null
            Assert.IsNull(right.ChildRight);     
        }

        /// <summary>
        /// If we provide empty array(empty but not null), method should return null.
        /// </summary>
        [TestMethod]
        public void CreateTreeWithNoArgTest()
        {
            Node tree = Node.CreateTree(new int[0]);

            Assert.IsNull(tree);
        }

        #endregion

        #region IsHeapNode Test

        /// <summary>
        /// If Node has both children(both are not null) then return true.
        /// </summary>
        [TestMethod]
        public void IsHeapNodeBothChildrenTest()
        {
            Node tree = new Node(5);
            tree.ChildLeft = new Node(1);
            tree.ChildRight = new Node(2);

            Assert.IsTrue(tree.IsHeapNode());
        }

        /// <summary>
        /// If Node has only left child, return true.
        /// </summary>
        [TestMethod]
        public void IsHeapNodeWithLeftChildOnlyTest()
        {
            Node tree = new Node(5);
            tree.ChildLeft = new Node(1);

            Assert.IsTrue(tree.IsHeapNode());
        }

        /// <summary>
        /// If Node has right child, but not has left child, then return false.
        /// </summary>
        public void IsNotHeapNodeWithRightChildOnlyTest()
        {
            Node tree = new Node(5);
            tree.ChildRight = new Node(2);

            Assert.IsFalse(tree.IsHeapNode());
        }

        #endregion
    }
}
