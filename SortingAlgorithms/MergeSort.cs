using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class MergeSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable
        {
            InternalAlgorithm(arr, 0, arr.Length - 1);
        }

        // Divide the array into two subarrays, sort them and merge them
        public static void InternalAlgorithm<T>(T[] arr, int left, int right) where T : IComparable
        {
            if (left < right)
            {
                int mid = (left + right) / 2;

                InternalAlgorithm(arr, left, mid);
                InternalAlgorithm(arr, mid + 1, right);

                Merge(arr, left, right, mid);
            }
        }

        static void Merge<T>(T[] arr, int left, int right, int middle) where T : IComparable
        {
            var i = left;
            var j = middle + 1;
            var size = right - left + 1;
            T[] temp = new T[size];

            var index = 0;
            while (i <= middle && j <= right)
            {
                if (arr[i].CompareTo(arr[j]) <= 0)
                    temp[index] = arr[i++];
                else
                    temp[index] = arr[j++];
                index++;
            }

            Array.Copy(arr, i, temp, index, middle - i + 1);
            Array.Copy(arr, j, temp, index, right - j + 1);
            Array.Copy(temp, 0, arr, left, size);
        }

    }
}
