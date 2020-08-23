using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class SelectionSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable
        {
            int size = arr.Length;
            for (int i = 0; i < size - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < size; j++)
                {
                    if (arr[j].CompareTo(arr[min]) < 0)
                        min = j;
                }
                Swap(arr, i, min);
            }
        }
        static void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}
