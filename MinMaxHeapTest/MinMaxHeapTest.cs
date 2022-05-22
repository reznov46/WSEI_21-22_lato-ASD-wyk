using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinMaxHeap;
using System.Collections.Generic;

namespace MinMaxHeapTest
{
    [TestClass]
    public class MinMaxHeapTest
    {
        [TestMethod]
        public void MakeHeapFromList()
        {
            List<int> asd = new List<int>() { 34, 12, 28, 9, 30, 19, 1, 40 };
            var lol = new MinMaxHeap<int>(asd);
            Assert.AreEqual(lol.array.ToString(), new List<int> { 1, 40, 34, 9, 30, 19, 28, 12 }.ToString());
        }
        [TestMethod]
        public void InsertTest()
        {
            MinMaxHeap<int> heap = new();
            heap.Insert(34);
            heap.Insert(12);
            heap.Insert(28);
            heap.Insert(9);
            heap.Insert(30);
            heap.Insert(19);
            heap.Insert(1);
            heap.Insert(40);
            Assert.AreEqual(heap.array.ToString(), new List<int> { 1, 40, 28, 12, 30, 19, 9, 34 }.ToString());
        }

    }
}
