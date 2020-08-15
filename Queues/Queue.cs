using System;

namespace Queues
{
    class Queue<T>
    {
        int _front, rear;
        readonly T[] items;

        public bool IsFull => rear == MaxSize - 1;
        public bool IsEmpty => _front == -1;
        public int MaxSize => items.Length;

        public Queue(int capacity)
        {
            _front = rear = -1;
            items = new T[capacity];
        }

        public void Enqueue(T n)
        {
            if (IsFull)
                throw new Exception("Queue full!");
            if (_front == -1)
                _front = 0;
            items[++rear] = n;
        }
        public T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Queue empty!");
            var item = items[_front];
            if (_front == rear)
                _front = rear = -1;
            else
                _front++;
            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
                throw new Exception("Queue empty!");
            return items[_front];
        }
    }

}
