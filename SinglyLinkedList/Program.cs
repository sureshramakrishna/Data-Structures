using System;

namespace SinglyLinkedList
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next;
        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }
    class LinkedList<T>
    {
        Node<T> Head = null;
        Node<T> Last = null;
        public void Insert(T data)
        {
            Node<T> item = new Node<T>(data);
            if (Head == null)
                Head = Last = item;
            else
            {
                Last.Next = item;
                Last = item;
            }
        }

        public T RemoveLast()
        {
            if (Head == null)
            {
                Console.WriteLine("Linked list empty...");
                return default;
            }
            if (Head.Next == null)
            {
                var item = Head.Data;
                Head = Last = null;
                return item;
            }
            else
            {
                var iterator = Head;
                while (iterator.Next != Last)
                    iterator = iterator.Next;
                var item = Last.Data;
                Last = iterator;
                Last.Next = null;
                return item;
            }
        }

        public T RemoveFirst()
        {
            if (Head == null)
            {
                Console.WriteLine("Linked list empty...");
                return default;
            }
            var item = Head.Data;
            if (Head.Next == null)
                Head = Last = null;
            else
                Head = Head.Next;
            return item;
        }

        public T Remove(T searchItem)
        {
            if (Head == null)
            {
                Console.WriteLine($"Cannot find item {searchItem}");
                return default;
            }
            T item;
            if (Head.Data.Equals(searchItem))
            {
                item = Head.Data;
                Head = Head.Next;
                return item;
            }

            Node<T> current = Head, prev = null;
            while (current != null && !current.Data.Equals(searchItem))
            {
                prev = current;
                current = current.Next;
            }
            if (current == null)
            {
                Console.WriteLine($"Cannot find item {searchItem}");
                return default;
            }

            item = current.Data;
            if (current.Next == null)
            {
                Last = prev;
                prev.Next = null;
            }
            else
            {
                prev.Next = current.Next;
                current.Next = null; //not actually required.
            }
            return item;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();
            for (int i = 0; i < 7; i++)
                linkedList.Insert(i);
            Console.WriteLine($"Removing first item : {linkedList.RemoveFirst()}");
            Console.WriteLine($"Removing last item : {linkedList.RemoveLast()}");
            Console.WriteLine($"Removing item {linkedList.Remove(6)}"); //To check if last item was really removed.
            Console.WriteLine($"Removing item {linkedList.Remove(3)}");
            Console.WriteLine($"Removing item {linkedList.Remove(1)}");
            Console.WriteLine($"Removing item {linkedList.Remove(5)}");
            Console.WriteLine($"Removing item {linkedList.Remove(2)}");
            Console.WriteLine($"Removing item {linkedList.Remove(4)}");
        }
    }
}

