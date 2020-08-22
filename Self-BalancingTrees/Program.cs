using System;

namespace Self_BalancingTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree btree = new BTree(1);
            for (int i = 100; i > 85; i--)
                btree.Add(i);
            btree.Remove(89);
        }
    }
}
