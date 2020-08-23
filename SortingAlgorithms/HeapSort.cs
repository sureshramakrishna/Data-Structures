using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class HeapSort
    {
        public static void Sort(int[] arr)
        {
            // Build heap (rearrange array) This is the same as using recursion to build max heap of left and right subtree
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
                Heapify(arr, arr.Length, i);

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                int temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
                Heapify(arr, i, 0);
            }
        }

        static void Heapify(int[] arr, int i, int n)
        {
            int largest = i;
            var left = 2 * i + 1;
            var right = 2 * i + 2;

            if (left > n || right > n)
                return;
            if (left < n && arr[left] > arr[largest])
                largest = left;
            if (right < n && arr[right] > arr[largest])
                largest = right;
            if (largest != i)
            {
                Swap(arr, largest, i);
                Heapify(arr, largest, n);
            }
        }

        static void Swap(int[] arr, int i, int j)
        {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
