using System;

namespace Queues
{
    class Queue<T>
    {
        int _front, _rear, _maxSize;
        T[] _items;

        //Can be full if front is in 0th position and rear is at the end or if front is somewhere in middle and Rear is just behind that.
        public bool IsFull => (_front == _rear + 1) || (_front == 0 && _rear == _maxSize - 1);
        public bool IsEmpty => _front == -1;

        public Queue(int capacity)
        {
            _maxSize = capacity;
            _front = _rear = -1;
            _items = new T[capacity];
        }

        public void Enqueue(T item)
        {
            if (IsFull)
                throw new Exception("Queue full!");
            if (_front == -1)
                _front = 0;
            _rear = (_rear + 1) % _maxSize;
            _items[_rear] = item;
        }
        public T Dequeue()
        {
            if (IsEmpty)
                throw new Exception("Queue empty!");
            var item = _items[_front];
            if (_front == _rear)
                _front = _rear = -1;
            else
                _front = (_front + 1) % _maxSize;
            return item;
        }
        public T Peek()
        {
            if (IsEmpty)
                throw new Exception("Queue empty!");
            return _items[_front];
        }
    }
    class Program
    {
        static void Main(string[] _)
        {
            Queue<int> queue = new Queue<int>(5);

            for (int i = 0; i <= 5; i++) //Using <= to simulate overflow.
                if (!queue.IsFull)
                    queue.Enqueue(i);

            queue.Dequeue();
            queue.Enqueue(0);

            Console.WriteLine($"Peeking Queue: {queue.Peek()}");

            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                if (!queue.IsEmpty)
                    queue.Dequeue();
        }
    }
}
