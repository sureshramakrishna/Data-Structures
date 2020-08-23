using System;

namespace Self_BalancingTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree btree = new BTree(2);
            btree.Add(23);
            btree.Add(27);
            btree.Add(50);
            btree.Add(51);
            btree.Add(52);
            btree.Add(20);
            btree.Add(16);
            btree.Add(15);
            btree.Add(10);
            btree.Add(06);
            btree.Add(05);
            btree.Add(60);
            btree.Add(64);
            btree.Add(65);
            btree.Add(70);
            btree.Add(72);
            btree.Add(73);
            btree.Add(14);
            btree.Add(04);
            btree.Add(68);
            btree.Add(80);
            btree.Add(81);
            btree.Add(82);
            btree.Add(90);
            btree.Add(92);
            btree.Add(93);
            btree.Add(95);
            btree.Add(100);
            btree.Add(110);
            btree.Add(75);
            btree.Add(77);
            btree.Add(78);
            btree.Add(79);
            btree.Add(89);
            btree.Add(111);

            btree.Remove(64);
            btree.Remove(23);
            btree.Remove(72);
            btree.Remove(65);
            btree.Remove(20);
            btree.Remove(70);
            btree.Remove(95);
            btree.Remove(60);
            btree.Remove(77);
            btree.Remove(80);
            btree.Remove(100);
            btree.Remove(93);
            btree.Remove(6);
            btree.Remove(27);
            btree.Remove(68);
            btree.Remove(16);
            btree.Remove(50);
        }
    }
}
