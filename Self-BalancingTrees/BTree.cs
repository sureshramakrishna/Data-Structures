using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_BalancingTrees
{
    /// <summary>
    /// Watch this video for deletion.
    /// https://www.youtube.com/watch?v=GKa_t7fF8o0
    /// </summary>
    public class Node
    {
        public int MAX, nCount;
        public int[] Keys;
        public Node[] Children;
        public bool IsLeaf;
        public bool IsFull => nCount == MAX;
        public bool IsOverLoaded => nCount == MAX + 1;
        public Node(int m)
        {
            MAX = m;
            Keys = new int[MAX + 1];
            Children = new Node[MAX + 2];
            IsLeaf = true;
        }
        /// <summary>
        /// Makes space for the new key by pushing the existing keys to the right and inserts the new key at the appropriate position.
        /// </summary>
        /// <param name="k"></param>
        public void Push(int k)
        {
            int i;
            for (i = nCount; i > 0 && k < Keys[i - 1]; i--)
            {
                Keys[i] = Keys[i - 1];
                if (!IsLeaf)
                    Children[i + 1] = Children[i];
            }
            //needed when new key is being inserted in 1st index, then we should also push the first child to the right.
            if(i == 0 && !IsLeaf)
                Children[1] = Children[0];
            Keys[i] = k;
            nCount++;
        }
        /// <summary>
        /// Gets the index of the subtree which might contain the key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetChildIndex(int key)
        {
            int index;
            for (index = 0; index < nCount && Keys[index] < key; index++) ;
            return index;
        }
        /// <summary>
        /// returns the index of the key if exists, else returns int.MinValue.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int HasKey(int key)
        {
            for (int i = 0; i < nCount; i++)
                if (Keys[i] == key)
                    return i;
            return int.MinValue;
        }
        /// <summary>
        /// Merges current node with node n. Copies all the keys and children to the current node.
        /// NOTE: this method is invoked after adding parent key to the current node. See merge operation for more details.
        /// </summary>
        /// <param name="n"></param>
        public void Merge(Node n)
        {
            for (int index = nCount; index < n.nCount + this.nCount; index++)
                this.Keys[index] = n.Keys[index - this.nCount];
            if (!this.IsLeaf)
            {
                for (int index = nCount; index < this.nCount + n.nCount + 1; index++)
                    this.Children[index] = n.Children[index - this.nCount];
            }
            this.nCount = n.nCount + this.nCount;
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
                root.Children[0] = left;
                root.Children[1] = right;
                root.IsLeaf = false;
            }
        }

        public void Insert(Node root, int key)
        {
            if (root.IsLeaf)
                root.Push(key);
            else
            {
                var index = root.GetChildIndex(key);
                Insert(root.Children[index], key);
                if (root.Children[index].IsOverLoaded)
                {
                    var splitKey = Split(root.Children[index], out Node left, out Node right);
                    root.Push(splitKey);
                    root.Children[index] = left;
                    root.Children[index + 1] = right;
                    root.IsLeaf = false;
                }
            }
        }
        /// <summary>
        /// Splits Overloaded node in to 2 node and returns the middle element.
        /// NOTE: This Method assumes that the node is overloaded
        /// </summary>
        /// <param name="x">Node to be split</param>
        /// <param name="x1">left half of the node</param>
        /// <param name="x2">right half of the node</param>
        /// <returns>Middle element of the x node</returns>
        private int Split(Node x, out Node x1, out Node x2)
        {
            x1 = new Node(T);
            x2 = new Node(T);

            x1.IsLeaf = x2.IsLeaf = x.IsLeaf;

            var splitOrder = T / 2;
            for (int i = 0; i < splitOrder; i++)
            {
                x1.Keys[i] = x.Keys[i];
                x2.Keys[i] = x.Keys[i + splitOrder + 1]; // + 1 is because the keys array is overloaded
            }
            if (!x.IsLeaf)
            {
                for (int i = 0; i <= splitOrder; i++)
                {
                    x1.Children[i] = x.Children[i];
                    x2.Children[i] = x.Children[i + splitOrder + 1];
                }
            }
            x1.nCount = x2.nCount = splitOrder;
            return x.Keys[splitOrder];
        }

        public void Remove(int key)
        {
            if (root.HasKey(key) != int.MinValue)
            {
                if (root.IsLeaf)
                    DeleteFromNode(root, key);
                else
                {
                    var keyIndex = root.HasKey(key);
                    root.Keys[keyIndex] = InOrderSuccessor(root.Children[keyIndex]);
                    Delete(root, root.Keys[keyIndex], keyIndex);
                }
            }
            else
            {
                var cIndex = root.GetChildIndex(key);
                Delete(root, key, cIndex);
            }
            if (root.nCount == 0)
                root = root.Children[0];
        }
        /// <summary>
        /// Delete the key from the node and pulls the keys and children 1 step to the left.
        /// Optional region is to setting the slots after nCount to 0 and null for children, this is for better readability purpose. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="key"></param>
        void DeleteFromNode(Node x, int key)
        {
            int index;
            for (index = 0; index < x.nCount && x.Keys[index] != key; index++) ;
            for (int i = index; i < x.nCount - 1; i++)
                x.Keys[i] = x.Keys[i + 1];
            if (!x.IsLeaf)
            {
                for (int i = index; i < x.nCount; i++)
                    x.Children[i] = x.Children[i + 1];
            }
            x.nCount--;

            #region optional
            for (int i = x.nCount; i < x.MAX; i++)
                x.Keys[i] = 0;
            for (int i = x.nCount + 1; i <= x.MAX; i++)
                x.Children[i] = null;
            #endregion optional
        }

        /// <summary>
        /// Borrows the key from its sibling and pushes the parent key down
        /// if sibling is predecessor, get the last key, else get the first key
        /// if sibling is predecessor, assign the right child of sibling as 1st child of current node else assign the first child of sibling as last child of current node.
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="parent"></param>
        /// <param name="pIndex"></param>
        /// <param name="sIndex"></param>
        /// <param name="isPredecessor"></param>
        void Borrow(Node x, Node parent, int pIndex, int sIndex, bool isPredecessor)
        {
            var sibling = parent.Children[sIndex];
            var sItem = sibling.Keys[isPredecessor ? sibling.nCount - 1 : 0];
            var pItem = parent.Keys[pIndex];
            parent.Keys[pIndex] = sItem;
            x.Push(pItem);
            if(!x.IsLeaf)
            { 
                if(isPredecessor)
                { 
                    x.Children[0] = sibling.Children[sibling.nCount];
                    //We are deleting directly here instead of using DeleteFromNode, because deleting last key needs special handling.
                    sibling.nCount--;
                    sibling.Keys[sibling.nCount] = 0;
                    sibling.Children[sibling.nCount + 1] = null;
                }
                else
                { 
                    x.Children[x.nCount] = sibling.Children[0];
                    DeleteFromNode(sibling, sItem);
                }
            }
            else
                DeleteFromNode(sibling, sItem);
        }

        /// <summary>
        /// Merges 2 nodes based on the parent.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="cIndex"></param>
        /// <param name="sIndex"></param>
        private void Merge(Node parent, int cIndex, int sIndex)
        {
            var x = parent.Children[cIndex];
            var s = parent.Children[sIndex];
            if (sIndex < cIndex)
            {
                var pItem = parent.Keys[cIndex - 1];
                s.Push(pItem);
                s.Merge(x);
                DeleteFromNode(parent, pItem);
                parent.Children[sIndex] = s;
            }
            else
            {
                var pItem = parent.Keys[cIndex];
                x.Push(pItem);
                x.Merge(s);
                DeleteFromNode(parent, pItem);
                parent.Children[0] = x;
            }
        }
        /// <summary>
        /// Decides whether borrow is possible, if not, does merge.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="parent"></param>
        /// <param name="cIndex"></param>
        void BorrowOrMerge(Node x, Node parent, int cIndex)
        {
            var leftSibling = cIndex - 1;
            var rightSibling = cIndex + 1;

            if (leftSibling >= 0 && parent.Children[leftSibling].nCount > T / 2)
                Borrow(x, parent, cIndex - 1, leftSibling, true);
            else if (rightSibling <= parent.nCount && parent.Children[rightSibling].nCount > T / 2)
                Borrow(x, parent, cIndex, rightSibling, false);
            else
            {
                if (leftSibling >= 0)
                    Merge(parent, cIndex, leftSibling);
                else
                    Merge(parent, cIndex, rightSibling);
            }
        }
        public int InOrderSuccessor(Node x)
        {
            if (x.IsLeaf)
                return x.Keys[x.nCount - 1];
            return InOrderSuccessor(x.Children[x.nCount]);
        }
        public void Delete(Node parent, int key, int cIndex)
        {
            Node x = parent.Children[cIndex];
            if (x.HasKey(key) != int.MinValue)
            {
                if (x.IsLeaf)
                    DeleteFromNode(x, key);
                else
                {
                    var keyIndex = x.HasKey(key);
                    x.Keys[keyIndex] = InOrderSuccessor(x.Children[keyIndex]);
                    Delete(x, x.Keys[keyIndex], keyIndex);
                }
            }
            else
            {
                if (x.IsLeaf)
                    throw new Exception($"The Key {key} does not exist in the tree");
                else
                    Delete(x, key, x.GetChildIndex(key));
            }
            if (x.nCount < T / 2)
                BorrowOrMerge(x, parent, cIndex);
        }
    }
}
