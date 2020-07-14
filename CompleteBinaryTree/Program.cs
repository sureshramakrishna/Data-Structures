using System;
using System.Collections.Generic;

namespace CompleteBinaryTree
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
        public Node<T> Parent;
        public Node(T data)
        {
            Data = data;
            Right = Left = Parent = null;
        }
    }

    class CompleteBinaryTree<T>
    {
        Node<T> root = null;

        public void Add(T data)
        {
            var item = new Node<T>(data);
            if (root == null)
                root = item;
            else
            {
                Queue<Node<T>> queue = new Queue<Node<T>>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    var node = queue.Dequeue();
                    if (node.Left == null)
                    {
                        node.Left = item;
                        item.Parent = node;
                        break;
                    }
                    else if (node.Right == null)
                    {
                        node.Right = item;
                        item.Parent = node;
                        break;
                    }
                    else
                    {
                        queue.Enqueue(node.Left);
                        queue.Enqueue(node.Right);
                    }
                }
            }
        }

        public void Delete(T key)
        {
            Node<T> nodeToDelete = null;
            Queue<Node<T>> queue = new Queue<Node<T>>();

            if (root == null)
            {
                Console.WriteLine("Item not found;");
                return;
            }
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node.Data.Equals(key))
                    nodeToDelete = node;
                if (node.Left == null && node.Right == null && queue.Count == 0)
                {
                    if (nodeToDelete == null)
                    {
                        Console.WriteLine("Item not found;");
                        break;
                    }
                    var parentNode = node.Parent;
                    if (parentNode == null)
                    {
                        root = null;
                        break;
                    }
                    if (parentNode.Right != null)
                    {
                        nodeToDelete.Data = parentNode.Right.Data;
                        parentNode.Right = null;
                    }
                    else
                    {
                        nodeToDelete.Data = parentNode.Left.Data;
                        parentNode.Left = null;
                    }
                }
                else
                {
                    if (node.Left != null)
                        queue.Enqueue(node.Left);
                    if (node.Right != null)
                        queue.Enqueue(node.Right);
                }
            }
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
            CompleteBinaryTree<int> cbt = new CompleteBinaryTree<int>();
            cbt.Add(50);
            cbt.Add(30);
            cbt.Add(20);
            cbt.Add(40);
            cbt.Add(70);
            cbt.Add(60);
            cbt.Add(80);

            cbt.Traverse(Traversal.INORDER);

            cbt.Delete(80);
            cbt.Traverse(Traversal.INORDER);

            cbt.Delete(20);
            cbt.Traverse(Traversal.INORDER);

            cbt.Delete(30);
            cbt.Traverse(Traversal.INORDER);
        }
    }
}
