using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    class BinarySearch
    {
        public static int Search<T>(T[] arr, T x) where T : IComparable
        {
            return InnerAlgorithm(arr, x, 0, arr.Length - 1);
        }
        static int InnerAlgorithm<T>(T[] arr, T value, int left, int right) where T : IComparable
        {
            if (right < left)
                return int.MinValue;

            int mid = (left + right) / 2;
            if (arr[mid].Equals(value))
                return mid;
            else if (arr[mid].CompareTo(value) > 0)
                return InnerAlgorithm(arr, value, left, mid - 1);
            else
                return InnerAlgorithm(arr, value, mid + 1, right);
        }
    }
}
