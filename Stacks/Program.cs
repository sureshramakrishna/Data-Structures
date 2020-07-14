using System;

namespace Stacks
{
    class Stack<T>
    {
        int maxSize;
        int top = -1;
        T[] items;
        public Stack(int capacity)
        {
            maxSize = capacity;
            items = new T[capacity];
        }
        public void Push(T item)
        {
            if (top == maxSize - 1)
            {
                Console.WriteLine("Stack Overflow...");
                return;
            }
            Console.WriteLine($"Inserting {item}");
            items[++top] = item;
        }
        public T Pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack Underflow...");
                return default; //we are returning default here, you can choose to throw an error instead.
            }
            Console.WriteLine($"Removing {items[top]}");
            return items[top--];
        }
        public T Peek()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack Empty...");
                return default; //we are returning default here, you can choose to throw an error instead.
            }
            return items[top];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>(5);
            for (int i = 0; i <= 5; i++) //Using <= to simulate overflow.
                stack.Push(i);
            Console.WriteLine($"Peeking stack: {stack.Peek()}");
            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                stack.Pop();
        }
    }
}
