using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SortingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var arrSize = rnd.Next(1, 1000);
            int[] arr = Enumerable.Repeat(0, arrSize).Select(x => rnd.Next(int.MinValue, int.MaxValue)).ToArray();
            var bs = (int[])arr.Clone();
            BubbleSort.Sort(bs);
            Assert.IsTrue(IsSorted(bs));

            var ss = (int[])arr.Clone();
            SelectionSort.Sort(ss);
            Assert.IsTrue(IsSorted(ss));

            var @is = (int[])arr.Clone();
            InsertionSort.Sort(@is);
            Assert.IsTrue(IsSorted(@is));

            var ms = (int[])arr.Clone();
            MergeSort.Sort(ms);
            Assert.IsTrue(IsSorted(ms));

            var qs = (int[])arr.Clone();
            QuickSort.Sort(qs);
            Assert.IsTrue(IsSorted(qs));

            var cs = (int[])arr.Clone();
            CountingSort.Sort(cs);
            Assert.IsTrue(IsSorted(cs));

            var rs = (int[])arr.Clone();
            RadixSort.Sort(rs);
            Assert.IsTrue(IsSorted(rs));

            var hs = (int[])arr.Clone();
            HeapSort.Sort(hs);
            Assert.IsTrue(IsSorted(hs));
        }
        static bool IsSorted<T>(T[] arr) where T : IComparable
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i].CompareTo(arr[i + 1]) > 0)
                    return false;
            }
            return true;
        }
    }
}
