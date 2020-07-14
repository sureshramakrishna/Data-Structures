using System;

namespace DoublyLinkedList
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next;
        public Node<T> Prev;
        public Node(T data)
        {
            Data = data;
            Next = Prev = null;
        }
    }
    class DoublyLinkedList<T>
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
                item.Prev = Last;
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
                var item = Last.Data;
                Last = Last.Prev;
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
            {
                Head = Head.Next;
                Head.Prev = null;
            }
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
                if (Head != null)
                    Head.Prev = null;
                return item;
            }

            Node<T> current = Head;
            while (current != null && !current.Data.Equals(searchItem))
                current = current.Next;

            if (current == null)
            {
                Console.WriteLine($"Cannot find item {searchItem}");
                return default;
            }

            item = current.Data;
            if (current.Next == null)
            {
                Last = current.Prev;
                Last.Next = null;
            }
            else
            {
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }
            return item;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList<int> linkedList = new DoublyLinkedList<int>();
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

