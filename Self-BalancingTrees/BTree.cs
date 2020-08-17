using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancingTrees
{
    public class Node
    {
        public int MAX, nCount;
        public int[] Keys;
        public Node[] Links;
        public bool Leaf;
        public bool IsFull => nCount == MAX;
        public bool IsOverLoaded => nCount == MAX + 1;
        public Node(int m)
        {
            MAX = m;
            Keys = new int[MAX + 1];
            Links = new Node[MAX + 2];
            Leaf = true;
        }
        public void Push(int k)
        {
            int i;
            for (i = nCount; i > 0 && k < Keys[i - 1]; i--)
                Keys[i] = Keys[i - 1];
            Keys[i] = k;
            nCount++;
        }
        public int GetLinkIndex(int k)
        {
            int i;
            for (i = 0; i < nCount && Keys[i] < k; i++) ;
            return i;
        }
    }
    class BTree
    {
        private readonly int T;
        Node root;
        public BTree(int t)
        {
            T = 2 * t;
            root = new Node(T);
        }
        public void Add(int key)
        {
            Insert(root, key);
            if (root.IsOverLoaded)
            {
                var splitKey = Split(root, out Node left, out Node right);
                root = new Node(T);
                root.Push(splitKey);
                root.Links[0] = left;
                root.Links[1] = right;
                root.Leaf = false;
            }
        }
        /// <summary>
        /// Splits Overloaded node in to 2 node and returns the middle element.
        /// </summary>
        /// <param name="x">Node to be split</param>
        /// <param name="x1">left half of the node</param>
        /// <param name="x2">right half of the node</param>
        /// <returns>Middle element of the x node</returns>
        private int Split(Node x, out Node x1, out Node x2)
        {
            x1 = new Node(T);
            x2 = new Node(T);

            x1.Leaf = x2.Leaf = x.Leaf;

            var splitOrder = T / 2;
            for (int i = 0; i < splitOrder; i++)
            {
                x1.Keys[i] = x.Keys[i];
                x2.Keys[i] = x.Keys[i + splitOrder + 1]; // + 1 is because the keys array is overloaded
            }
            if (!x.Leaf)
            {
                for (int i = 0; i <= splitOrder; i++)
                {
                    x1.Links[i] = x.Links[i];
                    x2.Links[i] = x.Links[i + splitOrder + 1];
                }
            }
            x1.nCount = x2.nCount = splitOrder;
            return x.Keys[splitOrder];
        }
        public void Insert(Node root, int key)
        {
            if (root.Leaf)
                root.Push(key);
            else
            {
                var index = root.GetLinkIndex(key);
                Insert(root.Links[index], key);
                if (root.Links[index].IsOverLoaded)
                {
                    var splitKey = Split(root.Links[index], out Node left, out Node right);
                    //Make space for the new link to hold the extra node that we created while split.
                    for (int i = root.nCount; i > 0 && i > index; i--)
                        root.Links[i + 1] = root.Links[i];
                    root.Push(splitKey);
                    root.Links[index] = left;
                    root.Links[index + 1] = right;
                    root.Leaf = false;
                }
            }
        }

        public void Remove(int key)
        {

        }
    }
}