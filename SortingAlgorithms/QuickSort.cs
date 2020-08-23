using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class QuickSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable
        {
            InternalAlgorithm(arr, 0, arr.Length - 1);
        }

        static void InternalAlgorithm<T>(T[] arr, int left, int right) where T : IComparable
        {
            if (left >= right)
                return;
            int pivot = left;
            pivot = Partition(arr, left, right, pivot);
            InternalAlgorithm(arr, left, pivot - 1);
            InternalAlgorithm(arr, pivot + 1, right);
        }

        static int Partition<T>(T[] arr, int left, int right, int pivot) where T : IComparable
        {
            var last = right;
            while (left < right)
            {
                while (arr[left].CompareTo(arr[pivot]) <= 0 && left < last)
                    left++;
                while (arr[right].CompareTo(arr[pivot]) > 0)
                    right--;
                if (left < right)
                    Swap(arr, left, right);
            }
            Swap(arr, pivot, right);
            return right;
        }

        static void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
