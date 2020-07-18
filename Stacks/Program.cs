using System;

namespace Stacks
{
    class Stack<T>
    {
        T[] items;
        int maxSize, top = -1;
        public int Count => top + 1;
        public Stack(int capacity)
        {
            maxSize = capacity;
            items = new T[capacity];
        }
        public void Push(T item)
        {
            if (top == maxSize - 1)
                throw new StackOverflowException("Stack over flow!");
            items[++top] = item;
        }
        public T Pop()
        {
            if (top == -1)
                throw new Exception("Stack empty!");
            return items[top--];
        }
        public T Peek()
        {
            if (top == -1)
                throw new Exception("Stack empty!");
            return items[top];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>(5);
            for (int i = 0; i <= 5; i++) //Using <= to simulate overflow.
                if (stack.Count < 5)
                    stack.Push(i);

            Console.WriteLine($"Peeking stack: {stack.Peek()}");

            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                if (stack.Count > 0)
                    stack.Pop();
        }
    }
}
