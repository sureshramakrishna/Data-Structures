using System;

namespace RedBlackTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlackTree bst = new RedBlackTree();
            bst.Insert(50);
            bst.DeleteNode(50);
        }
    }
}