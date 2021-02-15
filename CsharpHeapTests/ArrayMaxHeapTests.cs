using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsharpHeap;
using System;

namespace CsharpHeapTests
{

    [TestClass]
    public class ArrayMaxHeapTests
    {
        [TestMethod]
        public void Create_NewInstance_InitializesValuesCorrectly()
        {
            ArrayMaxHeap<int> arrayMaxHeap = new ArrayMaxHeap<int>();

            int count = arrayMaxHeap.Count;
            int level = arrayMaxHeap.Level;

            Assert.AreEqual(0, count);
            Assert.AreEqual(0, level);
        }

        [TestMethod]
        public void Indexer_IndexOutOfRange_ThrowsException()
        {
            ArrayMaxHeap<int> arrayMaxHeap = new ArrayMaxHeap<int>();

            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                _ = arrayMaxHeap[1];
            });
        }

        [TestMethod]
        public void Indexer_CorrectIndex_ReturnsElement()
        {
            ArrayMaxHeap<int> arrayMaxHeap = new ArrayMaxHeap<int>();
            int anyInteger = 1;

            arrayMaxHeap.Push(anyInteger);
            int result = arrayMaxHeap[0];

            Assert.AreEqual(1, result);
        }
    }
}
