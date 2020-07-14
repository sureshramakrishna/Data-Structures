using System;

namespace SearchAlgorithms
{
    class LinearSearch
    {
        public static int Search(int[] arr, int x)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == x)
                    return i;
            return -1;
        }
    }

    class BinarySearch
    {
        public static int Search(int[] arr, int x)
        {
            return InnerAlgorithm(arr, 0, arr.Length - 1, x);
        }
        static int InnerAlgorithm(int[] arr, int left, int right, int value)
        {
            if (right < left)
                return -1;
            int mid = (left + right) / 2;

            if (arr[mid] == value)
                return mid;
            else if (arr[mid] > value)
                return InnerAlgorithm(arr, left, mid - 1, value);
            else
                return InnerAlgorithm(arr, mid + 1, right, value);
        }
    }

    class JumpSearch
    {
        public static int Search(int[] arr, int x)
        {
            int n = arr.Length - 1;
            int step = (int)Math.Sqrt(n);
            int i, prev = 0;
            for (i = 0; i <= n; i = i + step)
            {
                if (arr[i] < x)
                    prev = i;
                else
                    break;
            }
            for (; prev <= (n < i ? n : i); prev++)
            {
                if (arr[prev] == x)
                    return prev;
            }
            return -1;
        }
    }

    class InterpolationSearch
    {
        public static int Search(int[] arr, int x)
        {
            int n = arr.Length - 1;

            // Find indexes of two corners 
            int lo = 0, hi = n;

            // Since array is sorted, an element present in array must be in range defined by corner 
            while (lo <= hi && x >= arr[lo] && x <= arr[hi])
            {
                if (lo == hi)
                {
                    if (arr[lo] == x)
                        return lo;
                    return -1;
                }
                // Probing the position keeping uniform distribution in mind. 
                int pos = lo + (hi - lo) * (x - arr[lo]) / (arr[hi] - arr[lo]);
                if (arr[pos] == x)
                    return pos;

                // If x is larger, x is in upper part else x is in the lower part
                if (arr[pos] < x)
                    lo = pos + 1;
                else
                    hi = pos - 1;
            }
            return -1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91, 92 };
            LinearSearch.Search(arr, 12);
            BinarySearch.Search(arr, 12);
            JumpSearch.Search(arr, 12);
            InterpolationSearch.Search(arr, 12);
        }
    }
}
