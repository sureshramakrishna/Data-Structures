using System;

namespace AVLTrees
{
    internal class Node
    {
        public int data;
        public Node left;
        public Node right;
        public Node(int data)
        {
            this.data = data;
        }
    }
    class AVL
    {
        Node root;
        public void Add(int data)
        {
            Node newItem = new Node(data);
            root = RecursiveInsert(root, newItem);
        }
        public void Delete(int target)
        {
            root = RecursiveDelete(root, target);
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (n.data < current.data)
            {
                current.left = RecursiveInsert(current.left, n);
                current = BalanceTree(current);
            }
            else if (n.data > current.data)
            {
                current.right = RecursiveInsert(current.right, n);
                current = BalanceTree(current);
            }
            return current;
        }
        private Node RecursiveDelete(Node current, int target)
        {
            Node parent;
            if (current == null)
                return null;
            else
            {
                if (target > current.data)
                {
                    current.right = RecursiveDelete(current.right, target);
                    BalanceTree(current);
                }
                else if (target < current.data)
                {
                    current.left = RecursiveDelete(current.left, target);
                    BalanceTree(current);
                }
                else
                {
                    //Replace the current node with the smallest number in right sub tree and delete that smallest node in right subtree
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                            parent = parent.left;

                        current.data = parent.data;
                        current.right = RecursiveDelete(current.right, parent.data);
                        BalanceTree(current);
                    }
                    else
                    {
                        return current.left;
                    }
                }
            }
            return current;
        }
        private void InOrderDisplayTree(Node current)
        {
            if (current != null)
            {
                InOrderDisplayTree(current.left);
                Console.Write("({0}) ", current.data);
                InOrderDisplayTree(current.right);
            }
        }
        private int GetHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.left);
                int r = GetHeight(current.right);
                int m = Math.Max(l, r);
                height = m + 1;
            }
            return height;
        }

        #region Balance and Rotation

        /// <summary>
        /// This method checks if a tree is balanced or not and fixes it.
        /// If Balance Factor is greater than 1, it means than tree is unbalanced and left sub tree has more items
        ///  - Check whether Left or Right subtree of Left tree has more items, if Left has more items, than do RotateRR else RotateLR
        /// If Balance Factor is lesser than -1, it means than tree is unbalanced and right sub tree has more items
        ///  - Check whether Left or Right subtree of Right tree has more items, if Left has more items, than do RotateRL else RotateLL
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private Node BalanceTree(Node current)
        {
            int b_factor = BalanceFactor(current);
            if (b_factor > 1) //If left tree has more items
            {
                if (BalanceFactor(current.left) >= 0) //If Left child has more items
                    current = RotateRR(current);
                else
                    current = RotateLR(current);
            }
            else if (b_factor < -1) //If right tree has more items
            {
                if (BalanceFactor(current.right) <= 0) //If Right child has more items
                    current = RotateLL(current);
                else
                    current = RotateRL(current);
            }
            return current;
        }
        private int BalanceFactor(Node current)
        {
            int l = GetHeight(current.left);
            int r = GetHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }

        /// <summary>
        /// Rotates Left on parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Node RotateLL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }

        /// <summary>
        /// Rotate Right on parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Node RotateRR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }

        /// <summary>
        /// Rotate Left on left child then Right on parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Node RotateLR(Node parent)
        {
            Node pivot = parent.left;
            parent.left = RotateLL(pivot);
            return RotateRR(parent);
        }

        /// <summary>
        /// Rotate right on right child then left on parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private Node RotateRL(Node parent)
        {
            Node pivot = parent.right;
            parent.right = RotateRR(pivot);
            return RotateLL(parent);
        }
        #endregion Balance and Rotation
    }
}
