using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    enum Traversal
    {
        INORDER,
        PREORDER,
        POSTORDER
    }
    class Node<T>
    {
        public T Data;
        public Node<T> Right;
        public Node<T> Left;
        public Node(T data)
        {
            Data = data;
            Right = Left = null;
        }
    }
    class BinarySearchTree<T>
    {
        Node<T> root = null;
        public void Add(T data)
        {
            root = Insert(root, data);
        }
        Node<T> Insert(Node<T> root, T data)
        {
            if (root == null)
                return new Node<T>(data);

            var comparision = Comparer<T>.Default.Compare(root.Data, data) > 0;
            if (comparision)
                root.Left = Insert(root.Left, data);
            else
                root.Right = Insert(root.Right, data);
            return root;
        }
        public void Delete(T key)
        {
            root = Remove(root, key);
        }

        Node<T> Remove(Node<T> root, T key)
        {
            if (root == null) /* Base Case: If the tree is empty */
                return root;

            var comparision = Comparer<T>.Default.Compare(root.Data, key);
            if (comparision > 0) /* Otherwise, recur down the tree */
                root.Left = Remove(root.Left, key);
            else if (comparision < 0)
                root.Right = Remove(root.Right, key);
            else
            {
                if (root.Left == null) // node with only one child or no child  
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;

                root.Data = MinValue(root.Right); // node with two children: Get the inorder successor (smallest in the right subtree)

                root.Right = Remove(root.Right, root.Data); // Delete the inorder successor  
            }
            return root;
        }

        public T MinValue(Node<T> root)
        {
            T minValue = root.Data;
            while (root.Left != null)
            {
                minValue = root.Left.Data;
                root = root.Left;
            }
            return minValue;
        }

        public void Traverse(Traversal traversal)
        {
            if (traversal == Traversal.INORDER)
                InOrderTraversal(root);
            else if (traversal == Traversal.PREORDER)
                PreOrderTraversal(root);
            else
                PostOrderTraversal(root);
        }
        void InOrderTraversal(Node<T> root)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left);
                Console.WriteLine(root.Data);
                InOrderTraversal(root.Right);
            }
        }
        void PreOrderTraversal(Node<T> root)
        {
            if (root != null)
            {
                Console.WriteLine(root.Data);
                PreOrderTraversal(root.Left);
                PreOrderTraversal(root.Right);
            }
        }
        void PostOrderTraversal(Node<T> root)
        {
            if (root != null)
            {
                PostOrderTraversal(root.Left);
                PostOrderTraversal(root.Right);
                Console.WriteLine(root.Data);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();
            bst.Add(50);
            bst.Add(30);
            bst.Add(20);
            bst.Add(40);
            bst.Add(70);
            bst.Add(60);
            bst.Add(80);

            //bst.Traverse(Traversal.INORDER);

            bst.Delete(20);
            bst.Delete(30);
            bst.Delete(70);
            bst.Traverse(Traversal.INORDER);
        }
    }
}
