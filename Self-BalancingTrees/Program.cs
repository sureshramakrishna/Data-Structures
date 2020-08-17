using System;

namespace Self_BalancingTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree btree = new BTree(1);
            btree.Add(10);
            btree.Add(15);
            btree.Add(9);
            btree.Add(8);
            btree.Add(7);
            btree.Add(16);
            btree.Add(17);
            btree.Add(18);
            btree.Add(19);
            btree.Add(20);
            btree.Add(21);
            btree.Add(14);
            btree.Add(13);
            btree.Add(12);
            btree.Add(11);
        }
    }
}
