using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class InsertionSort
    {
        public static void Sort<T>(T[]array) where T : IComparable
        {
            int size = array.Length;
            for (int i = 1; i < size; i++)
            {
                T key = array[i];
                int j = i - 1;
                while (j >= 0 && key.CompareTo(array[j]) < 0)
                {
                    array[j + 1] = array[j];
                    --j;
                }
                array[j + 1] = key;
            }
        }
    }
}
