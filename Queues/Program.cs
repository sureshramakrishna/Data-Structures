using System;

namespace Queues
{
    class Queue<T>
    {
        int front = 0, rear = -1, count = 0, maxSize;
        T[] items;
        public Queue(int capacity)
        {
            maxSize = capacity;
            items = new T[capacity];
        }
        public void Enqueue(T item)
        {
            if (count == maxSize)
            {
                Console.WriteLine("Queue Full...");
                return;
            }
            Console.WriteLine($"Inserting {item}");
            rear = (rear + 1) % maxSize;
            items[rear] = item;
            count++;
        }
        public T Dequeue()
        {
            if (count == 0)
            {
                Console.WriteLine("Queue empty...");
                return default; //we are returning default here, you can choose to throw an error instead.
            }
            Console.WriteLine($"Removing {items[front]}");
            var item = items[front];
            front = (front + 1) % maxSize;
            count--;
            return item;
        }
        public T Peek()
        {
            if (count == 0)
            {
                Console.WriteLine("Queue empty...");
                return default; //we are returning default here, you can choose to throw an error instead.
            }
            return items[front];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(5);
            for (int i = 0; i <= 5; i++) //Using <= to simulate overflow.
                queue.Enqueue(i);
            queue.Dequeue();
            queue.Enqueue(0);
            Console.WriteLine($"Peeking Queue: {queue.Peek()}");
            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                queue.Dequeue();
        }
    }
}
