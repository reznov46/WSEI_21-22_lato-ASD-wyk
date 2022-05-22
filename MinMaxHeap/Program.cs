using System;

namespace MinMaxHeap
{
    public class Program
    {
        static void Main(string[] args)
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
        }
    }
}
