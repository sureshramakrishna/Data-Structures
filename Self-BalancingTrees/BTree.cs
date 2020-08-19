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
        public bool IsLeaf;
        public bool IsFull => nCount == MAX;
        public bool IsOverLoaded => nCount == MAX + 1;
        public Node(int m)
        {
            MAX = m;
            Keys = new int[MAX + 1];
            Links = new Node[MAX + 2];
            IsLeaf = true;
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
        public bool HasKey(int k)
        {
            for (int i = 0; i < nCount; i++)
                if (Keys[i] == k)
                    return true;
            return false;
        }

        // A function to borrow a key from C[idx-1] and insert it 
        // into C[idx] 
        void BorrowFromPrev(int index)
        {
            Node child = Links[index];
            Node sibling = Links[index - 1];

            // The last key from C[idx-1] goes up to the parent and key[idx-1] 
            // from parent is inserted as the first key in C[idx]. Thus, the loses 
            // sibling one key and child gains one key 

            // Moving all key in C[idx] one step ahead 
            for (int i = child.nCount - 1; i >= 0; --i)
                child.Keys[i + 1] = child.Keys[i];

            // If C[idx] is not a leaf, move all its child pointers one step ahead 
            if (!child.IsLeaf)
            {
                for (int i = child.nCount; i >= 0; --i)
                    child.Links[i + 1] = child.Links[i];
            }

            // Setting child's first key equal to keys[idx-1] from the current node 
            child.Keys[0] = keys[index - 1];

            // Moving sibling's last child as C[idx]'s first child 
            if (!child->leaf)
                child.Links[0] = sibling.Links[sibling.nCount];

            // Moving the key from the sibling to the parent 
            // This reduces the number of keys in the sibling 
            keys[index - 1] = sibling.Keys[sibling.nCount - 1];

            child.nCount += 1;
            sibling.nCount -= 1;

            return;
        }

        // A function to borrow a key from the C[idx+1] and place 
        // it in C[idx] 
        void BTreeNode::borrowFromNext(int idx)
        {

            Node child = Links[idx];
            Node sibling = C[idx + 1];

            // keys[idx] is inserted as the last key in C[idx] 
            child.Keys[(child.nCount)] = keys[idx];

            // Sibling's first child is inserted as the last child 
            // into C[idx] 
            if (!(child->leaf))
                child.Links[(child.nCount) + 1] = sibling.Links[0];

            //The first key from sibling is inserted into keys[idx] 
            keys[idx] = sibling.Keys[0];

            // Moving all keys in sibling one step behind 
            for (int i = 1; i < sibling.nCount; ++i)
                sibling.Keys[i - 1] = sibling.Keys[i];

            // Moving the child pointers one step behind 
            if (!sibling->leaf)
            {
                for (int i = 1; i <= sibling.nCount; ++i)
                    sibling.Links[i - 1] = sibling.Links[i];
            }

            // Increasing and decreasing the key count of C[idx] and C[idx+1] 
            // respectively 
            child.nCount += 1;
            sibling.nCount -= 1;

            return;
        }

        // A function to merge C[idx] with C[idx+1] 
        // C[idx+1] is freed after merging 
        void BTreeNode::merge(int idx)
        {
            Node child = C[idx];
            Node sibling = C[idx + 1];

            // Pulling a key from the current node and inserting it into (t-1)th 
            // position of C[idx] 
            child.Keys[t - 1] = keys[idx];

            // Copying the keys from C[idx+1] to C[idx] at the end 
            for (int i = 0; i < sibling.nCount; ++i)
                child.Keys[i + t] = sibling.Keys[i];

            // Copying the child pointers from C[idx+1] to C[idx] 
            if (!child->leaf)
            {
                for (int i = 0; i <= sibling.nCount; ++i)
                    child.Links[i + t] = sibling.Links[i];
            }

            // Moving all keys after idx in the current node one step before - 
            // to fill the gap created by moving keys[idx] to C[idx] 
            for (int i = idx + 1; i < n; ++i)
                keys[i - 1] = keys[i];

            // Moving the child pointers after (idx+1) in the current node one 
            // step before 
            for (int i = idx + 2; i <= n; ++i)
                C[i - 1] = C[i];

            // Updating the key count of child and the current node 
            child.nCount += sibling.nCount + 1;
            n--;

            // Freeing the memory occupied by sibling 
            delete(sibling);
            return;
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
                root.IsLeaf = false;
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
                    x1.Links[i] = x.Links[i];
                    x2.Links[i] = x.Links[i + splitOrder + 1];
                }
            }
            x1.nCount = x2.nCount = splitOrder;
            return x.Keys[splitOrder];
        }
        public void Insert(Node root, int key)
        {
            if (root.IsLeaf)
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
                    root.IsLeaf = false;
                }
            }
        }

        public void Remove(int key)
        {

        }
        void DeleteFromLeaf(Node x, int key)
        {
            int index;
            for (index = 0; index < x.nCount && x.Keys[index] != key; index++) ;
            for (int i = index + 1; i < x.nCount; i++)
                x.Keys[i - 1] = x.Keys[i];
            x.nCount--;
        }
        public void Delete(Node x, int key)
        {
            if(x.HasKey(key))
            {
                if(x.IsLeaf)
                    DeleteFromLeaf(x, key);
            }
            else
            {
                // If this node is a leaf node, then the key is not present in tree 
                if (x.IsLeaf)
                    throw new Exception($"The Key {key} does not exist in the tree");

                int index = 0;
                while (index < x.nCount && x.Keys[index] < key)
                    index++;

                // The key to be removed is present in the sub-tree rooted with this node 
                // The flag indicates whether the key is present in the sub-tree rooted 
                // with the last child of this node 
                bool flag = ((index == x.nCount) ? true : false);

                // If the child where the key is supposed to exist has less that t keys, 
                // we fill that child 
                if (C[index].nCount < t)
                    fill(index);

                // If the last child has been merged, it must have merged with the previous 
                // child and so we recurse on the (idx-1)th child. Else, we recurse on the 
                // (idx)th child which now has atleast t keys 
                if (flag && index > n)
                    C[index - 1]->remove(k);
                else
                    C[index]->remove(k);
            }
        }



    }
}

//private void Remove(Node x, int key)
//{
//    int pos = x.Find(key);
//    if (pos != -1)
//    {
//        if (x.leaf)
//        {
//            int i = 0;
//            for (i = 0; i < x.n && x.key[i] != key; i++)
//            {
//            }
//          ;
//            for (; i < x.n; i++)
//            {
//                if (i != 2 * T - 2)
//                {
//                    x.key[i] = x.key[i + 1];
//                }
//            }
//            x.n--;
//            return;
//        }
//        if (!x.leaf)
//        {

//            Node pred = x.child[pos];
//            int predKey = 0;
//            if (pred.n >= T)
//            {
//                for (; ; )
//                {
//                    if (pred.leaf)
//                    {
//                        System.out.println(pred.n);
//                        predKey = pred.key[pred.n - 1];
//                        break;
//                    }
//                    else
//                    {
//                        pred = pred.child[pred.n];
//                    }
//                }
//                Remove(pred, predKey);
//                x.key[pos] = predKey;
//                return;
//            }

//            Node nextNode = x.child[pos + 1];
//            if (nextNode.n >= T)
//            {
//                int nextKey = nextNode.key[0];
//                if (!nextNode.leaf)
//                {
//                    nextNode = nextNode.child[0];
//                    for (; ; )
//                    {
//                        if (nextNode.leaf)
//                        {
//                            nextKey = nextNode.key[nextNode.n - 1];
//                            break;
//                        }
//                        else
//                        {
//                            nextNode = nextNode.child[nextNode.n];
//                        }
//                    }
//                }
//                Remove(nextNode, nextKey);
//                x.key[pos] = nextKey;
//                return;
//            }

//            int temp = pred.n + 1;
//            pred.key[pred.n++] = x.key[pos];
//            for (int i = 0, j = pred.n; i < nextNode.n; i++)
//            {
//                pred.key[j++] = nextNode.key[i];
//                pred.n++;
//            }
//            for (int i = 0; i < nextNode.n + 1; i++)
//            {
//                pred.child[temp++] = nextNode.child[i];
//            }

//            x.child[pos] = pred;
//            for (int i = pos; i < x.n; i++)
//            {
//                if (i != 2 * T - 2)
//                {
//                    x.key[i] = x.key[i + 1];
//                }
//            }
//            for (int i = pos + 1; i < x.n + 1; i++)
//            {
//                if (i != 2 * T - 1)
//                {
//                    x.child[i] = x.child[i + 1];
//                }
//            }
//            x.n--;
//            if (x.n == 0)
//            {
//                if (x == root)
//                {
//                    root = x.child[0];
//                }
//                x = x.child[0];
//            }
//            Remove(pred, key);
//            return;
//        }
//    }
//    else
//    {
//        pos = x.GetLinkIndex(key);
//        Node tmp = x.Links[pos];
//        if (tmp.nCount >= T)
//        {
//            Remove(tmp, key);
//            return;
//        }
//        else
//        {
//            Node nb = null;
//            int devider = -1;

//            if (pos != x.nCount && x.child[pos + 1].n >= T)
//            {
//                devider = x.key[pos];
//                nb = x.child[pos + 1];
//                x.key[pos] = nb.key[0];
//                tmp.key[tmp.n++] = devider;
//                tmp.child[tmp.n] = nb.child[0];
//                for (int i = 1; i < nb.n; i++)
//                {
//                    nb.key[i - 1] = nb.key[i];
//                }
//                for (int i = 1; i <= nb.n; i++)
//                {
//                    nb.child[i - 1] = nb.child[i];
//                }
//                nb.n--;
//                Remove(tmp, key);
//                return;
//            }
//            else if (pos != 0 && x.child[pos - 1].n >= T)
//            {

//                devider = x.key[pos - 1];
//                nb = x.child[pos - 1];
//                x.key[pos - 1] = nb.key[nb.n - 1];
//                Node child = nb.child[nb.n];
//                nb.n--;

//                for (int i = tmp.n; i > 0; i--)
//                {
//                    tmp.key[i] = tmp.key[i - 1];
//                }
//                tmp.key[0] = devider;
//                for (int i = tmp.n + 1; i > 0; i--)
//                {
//                    tmp.child[i] = tmp.child[i - 1];
//                }
//                tmp.child[0] = child;
//                tmp.n++;
//                Remove(tmp, key);
//                return;
//            }
//            else
//            {
//                Node lt = null;
//                Node rt = null;
//                boolean last = false;
//                if (pos != x.n)
//                {
//                    devider = x.key[pos];
//                    lt = x.child[pos];
//                    rt = x.child[pos + 1];
//                }
//                else
//                {
//                    devider = x.key[pos - 1];
//                    rt = x.child[pos];
//                    lt = x.child[pos - 1];
//                    last = true;
//                    pos--;
//                }
//                for (int i = pos; i < x.n - 1; i++)
//                {
//                    x.key[i] = x.key[i + 1];
//                }
//                for (int i = pos + 1; i < x.n; i++)
//                {
//                    x.child[i] = x.child[i + 1];
//                }
//                x.n--;
//                lt.key[lt.n++] = devider;

//                for (int i = 0, j = lt.n; i < rt.n + 1; i++, j++)
//                {
//                    if (i < rt.n)
//                    {
//                        lt.key[j] = rt.key[i];
//                    }
//                    lt.child[j] = rt.child[i];
//                }
//                lt.n += rt.n;
//                if (x.n == 0)
//                {
//                    if (x == root)
//                    {
//                        root = x.child[0];
//                    }
//                    x = x.child[0];
//                }
//                Remove(lt, key);
//                return;
//            }
//        }
//    }
//}

