using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
{
    class CircularQueue<T>
    {
        int _front, _rear, _maxSize;
        readonly T[] _items;

        //Can be full if front is in 0th position and rear is at the end or if front is somewhere in middle and Rear is just behind that.
        public bool IsFull => (_front == _rear + 1) || (_front == 0 && _rear == _maxSize - 1);
        public bool IsEmpty => _front == -1;

        public CircularQueue(int capacity)
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
}
