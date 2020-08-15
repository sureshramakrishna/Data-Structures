using System;

namespace Queues
{
    class Program
    {
        static void Main(string[] _)
        {
            QueueDriver();
            CircularQueueDriver();
            DeQueueDriver();
        }

        /// <summary>
        /// Standard Queue test drive
        /// </summary>
        static void QueueDriver()
        {
            Queue<int> queue = new Queue<int>(5);

            for (int i = 0; i <= 5; i++)
                if (!queue.IsFull)
                    queue.Enqueue(i);
            queue.Dequeue();
            if (!queue.IsFull)  //Fails with Queue and Succeeds with Circular Queue.
                queue.Enqueue(0);
            Console.WriteLine($"Peeking Queue: {queue.Peek()}");
            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                if (!queue.IsEmpty)
                    queue.Dequeue();
        }

        /// <summary>
        /// Circular Queue test drive
        /// </summary>
        static void CircularQueueDriver()
        {
            CircularQueue<int> cQueue = new CircularQueue<int>(5);

            for (int i = 0; i <= 5; i++)
                if (!cQueue.IsFull)
                    cQueue.Enqueue(i);
            cQueue.Dequeue();
            if (!cQueue.IsFull)  //Fails with Queue and Succeeds with Circular Queue.
                cQueue.Enqueue(0);
            Console.WriteLine($"Peeking Queue: {cQueue.Peek()}");
            for (int i = 0; i <= 5; i++) //Using <= to simulate underflow.
                if (!cQueue.IsEmpty)
                    cQueue.Dequeue();
        }

        /// <summary>
        /// DeQueue test drive
        /// </summary>
        static void DeQueueDriver()
        {
            DeQueue<int> deQueue = new DeQueue<int>(5);
            deQueue.InsertFront(0);
            deQueue.InsertFront(1);
            deQueue.InsertRear(2);
            deQueue.InsertRear(3);
            deQueue.DeleteFront();
            deQueue.DeleteRear();
        }
    }
}
