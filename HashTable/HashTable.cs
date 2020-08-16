using System;
namespace HashTable
{
    public class HashTable
    {
        private struct bucket
        {
            public object key;
            public object val;
        }

        private bucket[] buckets;
        private int Count;

        public HashTable(int capacity = 5)
        {
            buckets = new bucket[capacity];
        }
        /// <summary>
        /// Gets the Hashcode and converts it to Positive number (ignore the sign bit)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashsize"></param>
        /// <param name="seed">seed is the hashcode withotu sign bit</param>
        /// <param name="incr">If an item already exists in the bucket, we find next bucket to insert using incr</param>
        /// <returns></returns>
        private uint InitHash(object key, out uint seed, out uint incr)
        {
            var hashsize = buckets.Length;
            uint hashcode = (uint)key.GetHashCode() & 0x7FFFFFFF; // Hashcode must be positive.
            seed = hashcode;
            incr = 1 + (seed * 101 % ((uint)hashsize - 1));
            return hashcode;
        }

        /// <summary>
        /// Adds the key at the location based on bucket = GetHashCode%size. 
        /// In case of collision, we use second hash which is bucket = (bucket + incr) % size to find the next empty bucket. 
        /// This second for hash ensures that all the buckets are visited at least once.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="nvalue"></param>
        public virtual void Add(object key, object nvalue)
        {
            if(key == null)
                throw new Exception("Key cannot be null!");

            if(Count == buckets.Length)
                throw new Exception("Table Full");

            int ntry = 0;
            InitHash(key, out uint seed, out uint incr);
            int bucketNumber = (int)(seed % (uint)buckets.Length);
            do
            {
                if (buckets[bucketNumber].key == null || buckets[bucketNumber].key.Equals(key))
                {
                    buckets[bucketNumber].val = nvalue;
                    buckets[bucketNumber].key = key;
                    Count++;
                    return;
                }
                bucketNumber = (int)((bucketNumber + incr) % (uint)buckets.Length);
            } while (++ntry < buckets.Length);
        }

        public virtual bool ContainsKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException("key cannot be null");

            int ntry = 0;
            InitHash(key, out uint seed, out uint incr);
            int bucketNumber = (int)(seed % (uint)buckets.Length);
            do
            {
                if (buckets[bucketNumber].key == null)
                    return false;

                if (buckets[bucketNumber].key.Equals(key))
                    return true;
                bucketNumber = (int)((bucketNumber + incr) % (uint)buckets.Length);
            } while (++ntry < buckets.Length);
            return false;
        }

        public virtual void Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException("Key cannot be null");

            int ntry = 0;
            InitHash(key, out uint seed, out uint incr);
            int bucketNumber = (int)(seed % (uint)buckets.Length);
            do
            {
                if(buckets[bucketNumber].key == null)
                    throw new Exception("Key not found");

                if (buckets[bucketNumber].key.Equals(key))
                {
                    buckets[bucketNumber].key = null;
                    buckets[bucketNumber].val = null;
                    Count--;
                    return;
                }
                bucketNumber = (int)((bucketNumber + incr) % (uint)buckets.Length);
            } while (++ntry < buckets.Length);
        }
    }
}
