using System;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedListDrive();
            HeapDrive();
        }
        static void DoublyLinkedListDrive()
        {
            DoublyLinkedList<int> linkedList = new DoublyLinkedList<int>();
            for (int i = 0; i < 7; i++)
                linkedList.Insert(i);
            Console.WriteLine($"Removing first item : {linkedList.RemoveFirst()}");
            Console.WriteLine($"Removing last item : {linkedList.RemoveLast()}");
            Console.WriteLine($"Removing item {linkedList.Remove(3)}");
            Console.WriteLine($"Removing item {linkedList.Remove(1)}");
            Console.WriteLine($"Removing item {linkedList.Remove(5)}");
            Console.WriteLine($"Removing item {linkedList.Remove(2)}");
            Console.WriteLine($"Removing item {linkedList.Remove(4)}");
        }
        static void HeapDrive()
        {
            int[] array = new int[10];
            Heap h = new Heap();
            h.Insert(array, 3);
            h.Insert(array, 4);
            h.Insert(array, 9);
            h.Insert(array, 5);
            h.Insert(array, 2);

            Console.Write("Max-Heap array: ");
            h.PrintArray(array);

            h.DeleteNode(array, 4);
            Console.Write("After deleting an element: ");
            h.PrintArray(array);
        }
    }
}
