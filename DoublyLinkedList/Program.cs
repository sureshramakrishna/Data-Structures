using System;

namespace DoublyLinkedList
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next, Prev;
        public Node(T data)
        {
            Data = data;
        }
    }
    class DoublyLinkedList<T>
    {
        Node<T> Head, Last;
        public int Count;
        public void Insert(T data)
        {
            Node<T> item = new Node<T>(data);
            Count++;
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
                throw new Exception("Linked list empty...");
            
            Count--;
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
                throw new Exception("Linked list empty...");

            Count--;
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
                throw new Exception("Linked list empty...");

            T item;
            if (Head.Data.Equals(searchItem))
            {
                Count--;
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
                throw new Exception($"Cannot find item {searchItem}");

            Count--;
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
            Console.WriteLine($"Removing item {linkedList.Remove(3)}");
            Console.WriteLine($"Removing item {linkedList.Remove(1)}");
            Console.WriteLine($"Removing item {linkedList.Remove(5)}");
            Console.WriteLine($"Removing item {linkedList.Remove(2)}");
            Console.WriteLine($"Removing item {linkedList.Remove(4)}");
        }
    }
}

