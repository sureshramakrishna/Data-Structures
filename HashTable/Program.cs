using System;
using System.Collections;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable hs = new HashTable();
            hs.Add(1, 1);
            hs.Add(8, 8);
            hs.Add(6, 6);
            hs.Remove(6);
            hs.ContainsKey(6);
        }
    }
}
