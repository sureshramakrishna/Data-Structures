using System.Linq;

namespace SortingAlgorithms
{
    class RadixSort
    {
        public static void Sort(int[] array)
        {
            int max = array.Max();

            // Apply counting sort to sort elements based on place value.
            for (int place = 1; max / place > 0; place *= 10)
                CountingSort(array, array.Length, place);
        }

        static void CountingSort(int[] array, int size, int place)
        {
            int[] output = new int[size + 1];
            int max = array.Max();

            int[] count = new int[max + 1];

            // Calculate count of elements
            for (int i = 0; i < size; i++)
                count[array[i] / place % 10]++;

            // Calculate cummulative count
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Place the elements in sorted order
            for (int i = size - 1; i >= 0; i--)
            {
                var x = array[i] / place % 10;
                output[count[x] - 1] = array[i];
                count[x]--;
            }

            for (int i = 0; i < size; i++)
                array[i] = output[i];
        }
    }
}