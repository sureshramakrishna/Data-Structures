using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    class ExponentialSearch
    {
        public static int Search<T>(T[] arr, T x) where T : IComparable
        {
            // If x is present at first location itself 
            if (arr[0].Equals(x))
                return 0;

            // Find range for binary search by repeated doubling 
            int i = 1;
            while (i < arr.Length && arr[i].CompareTo(x) <= 0)
                i *= 2;

            // Call binary search for the found range. 
            return BinarySearch(arr, x, i / 2, Math.Min(i, arr.Length));
        }

        static int BinarySearch<T>(T[] arr, T value, int left, int right) where T : IComparable
        {
            if (right < left)
                return int.MinValue;

            int mid = (left + right) / 2;
            if (arr[mid].Equals(value))
                return mid;
            else if (arr[mid].CompareTo(value) > 0)
                return BinarySearch(arr, value, left, mid - 1);
            else
                return BinarySearch(arr, value, mid + 1, right);
        }
    }
}
