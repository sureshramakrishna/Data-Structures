using System;

namespace Collections
{
    class Heap
    {
        public int Count = 0;
        void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        public void Heapify(int[] arr, int i)
        {
            int size = arr.Length;
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < size && arr[left] > arr[largest])
                largest = left;
            if (right < size && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                Swap(arr, largest, i);
                Heapify(arr, largest);
            }
        }

        public void Insert(int[] arr, int newNum)
        {
            if (Count == 0)
                arr[Count++] = newNum;
            else
            {
                arr[Count++] = newNum;
                for (int i = Count / 2 - 1; i >= 0; i--)
                    Heapify(arr, i);
            }
        }

        public void DeleteNode(int[] arr, int num)
        {
            int size = Count;
            int i;
            for (i = 0; i < size; i++)
            {
                if (num == arr[i])
                    break;
            }
            Swap(arr, i, Count - 1);
            Count--;
            for (int j = size / 2 - 1; j >= 0; j--)
                Heapify(arr, j);
        }

        public void PrintArray(int[] arr)
        {
            for (int i = 0; i < Count; i++)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}