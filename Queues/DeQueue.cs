using System;

namespace Queues
{
    class DeQueue<T>
    {
        int _front, _rear, _maxSize;
        readonly T[] _items;

        //Can be full if front is in 0th position and rear is at the end or if front is somewhere in middle and Rear is just behind that.
        public bool IsFull => (_front == _rear + 1) || (_front == 0 && _rear == _maxSize - 1);
        public bool IsEmpty => _front == -1;

        public DeQueue(int capacity)
        {
            _maxSize = capacity;
            _front = _rear = -1;
            _items = new T[capacity];
        }

        public void InsertFront(T item)
        {
            if (IsFull)
                throw new Exception("DeQueue full!");

            if (_front == -1)
                _front = _rear = 0;
            else if (_front == 0)
                _front = _maxSize - 1;
            else
                _front--;
            _items[_front] = item;
        }
        public void InsertRear(T item)
        {
            if (IsFull)
                throw new Exception(" Overflow ");

            if (_front == -1)
                _front = _rear = 0;
            else if (_rear == _maxSize - 1)
                _rear = 0;
            else
                _rear++;
            _items[_rear] = item;
        }

        public T DeleteFront()
        {
            if (IsEmpty)
                throw new Exception("Queue Underflow\n");

            var item = _items[_front];
            if (_front == _rear)
                _front = _rear = -1;
            else
                _front = (_front + 1) % _maxSize;
            return item;
        }
        public T DeleteRear()
        {
            if (IsEmpty)
                throw new Exception("Queue Underflow\n");

            var item = _items[_rear];
            if (_front == _rear)
                _front = _rear = -1;
            else if (_rear == 0)
                _rear = _maxSize - 1;
            else
                _rear--;
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