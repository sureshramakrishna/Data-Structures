using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithms
{
    class CountingSort
    {
        public static void Sort(int[] arr)
        {
            var max = Convert.ToInt32(arr.Max());
            int[] count = new int[max + 1];
            int[] outArray = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
                count[arr[i]]++;

            #region mainLogic
            for (int i = 1; i <= max; i++)
                count[i] += count[i - 1];

            // Find the index of each element of the original array in count array, and place the elements in output array
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                outArray[count[arr[i]] - 1] = arr[i];
                count[arr[i]]--;
            }
            #endregion mainLogic

            // Copy the sorted elements into original array
            for (int i = 0; i < arr.Length; i++)
                arr[i] = outArray[i];
        }
    }
}