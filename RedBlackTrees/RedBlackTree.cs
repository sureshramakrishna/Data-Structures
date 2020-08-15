using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTrees
{
    public enum Color
    {
        RED = 0,
        BLACK = 1
    }
    public class Node
    {
        public int Data;
        public Node Parent, Left, Right;
        public Color Color;
        public Node()
        {

        }
        public Node(int key)
        {
            Data = key;
            Color = Color.RED;
        }
    }

    public class RedBlackTree
    {
        private Node root;
        private Node Tnull;

        // Balance the tree after deletion of a node

        private void RBTransplant(Node u, Node v)
        {
            if (u.Parent == null)
                root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;
            if (v != null)
                v.Parent = u.Parent;
        }

        private Node GetNode(Node node, int key)
        {
            if (node == null)
                return null;
            else if (node.Data == key)
                return node;
            else if (key < node.Data)
                return GetNode(node.Left, key);
            else
                return GetNode(node.Right, key);
        }
        private void DeleteNodeHelper(Node node, int key)
        {
            Node replacementNode, nodeToDelete;
            
            Node actualNode = GetNode(node, key);
            if (actualNode == null)
                return;

            nodeToDelete = actualNode;
            Color originalColor = nodeToDelete.Color;
            if (actualNode.Left == null)
            {
                replacementNode = actualNode.Right;
                RBTransplant(actualNode, actualNode.Right);
            }
            else if (actualNode.Right == null)
            {
                replacementNode = actualNode.Left;
                RBTransplant(actualNode, actualNode.Left);
            }
            else
            {
                nodeToDelete = Minimum(actualNode.Right);
                originalColor = nodeToDelete.Color;
                replacementNode = nodeToDelete.Right;
                if (nodeToDelete.Parent == actualNode)
                {
                    replacementNode.Parent = nodeToDelete.Parent;
                }
                else
                {
                    RBTransplant(nodeToDelete, nodeToDelete.Right);
                    nodeToDelete.Right = actualNode.Right;
                    nodeToDelete.Right.Parent = nodeToDelete;
                }

                RBTransplant(actualNode, nodeToDelete);
                nodeToDelete.Left = actualNode.Left;
                nodeToDelete.Left.Parent = nodeToDelete;
                nodeToDelete.Color = actualNode.Color;
            }
            if (originalColor == Color.BLACK)
            {
                FixDelete(replacementNode);
            }
        }
        private void FixDelete(Node x)
        {
            Node s;
            while (x != null && x != root && x.Color == Color.BLACK)
            {
                if (x == x.Parent.Left)
                {
                    s = x.Parent.Right;
                    if (s.Color == Color.RED)
                    {
                        s.Color = Color.BLACK;
                        x.Parent.Color = Color.RED;
                        LeftRotate(x.Parent);
                        s = x.Parent.Right;
                    }

                    if (s.Left.Color == Color.BLACK && s.Right.Color == Color.BLACK)
                    {
                        s.Color = Color.RED;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Right.Color == Color.BLACK)
                        {
                            s.Left.Color = Color.BLACK;
                            s.Color = Color.RED;
                            RightRotate(s);
                            s = x.Parent.Right;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = Color.BLACK;
                        s.Right.Color = Color.BLACK;
                        LeftRotate(x.Parent);
                        x = root;
                    }
                }
                else
                {
                    s = x.Parent.Left;
                    if(s == null)
                        return;
                    if (s.Color == Color.RED)
                    {
                        s.Color = Color.BLACK;
                        x.Parent.Color = Color.RED;
                        RightRotate(x.Parent);
                        s = x.Parent.Left;
                    }

                    if (s.Right.Color == Color.BLACK && s.Right.Color == Color.BLACK)
                    {
                        s.Color = Color.RED;
                        x = x.Parent;
                    }
                    else
                    {
                        if (s.Left.Color == Color.BLACK)
                        {
                            s.Right.Color = Color.BLACK;
                            s.Color = Color.RED;
                            LeftRotate(s);
                            s = x.Parent.Left;
                        }

                        s.Color = x.Parent.Color;
                        x.Parent.Color = Color.BLACK;
                        s.Left.Color = Color.BLACK;
                        RightRotate(x.Parent);
                        x = root;
                    }
                }
            }
            x.Color = Color.BLACK;
        }

        /// <summary>
        /// Returns the minimum value node in the tree aka in-order successor.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public Node Minimum(Node node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public void LeftRotate(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;
            if (y.Left != Tnull)
                y.Left.Parent = x;

            y.Parent = x.Parent;
            if (x.Parent == null)
                this.root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }
        public void RightRotate(Node x)
        {
            Node y = x.Left;
            x.Left = y.Right;
            if (y.Right != Tnull)
                y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null)
                this.root = y;
            else if (x == x.Parent.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

        public void Insert(int key)
        {
            Node n = new Node(key);
            root = RecursiveInsert(root, n);
            FixInsert(n);
        }

        private Node RecursiveInsert(Node current, Node n)
        {
            if (current == null)
                return n;
            if (n.Data < current.Data)
            {
                current.Left = RecursiveInsert(current.Left, n);
                current.Left.Parent = current;
            }
            else if (n.Data > current.Data)
            {
                current.Right = RecursiveInsert(current.Right, n);
                current.Right.Parent = current;
            }
            return current;
        }
        private void FixInsert(Node pt)
        {
            Node parent, grandParent, uncle;
            if (pt.Parent == null)
                pt.Color = Color.BLACK;
            else if (pt.Parent.Parent == null)
                return;
            else
            {
                while (pt != root && pt.Color == Color.RED && pt.Parent.Color == Color.RED)
                {
                    parent = pt.Parent;
                    grandParent = pt.Parent.Parent;

                    /*  Case A:  Parent of pt is Left child of GrandParent of pt */
                    if (parent == grandParent.Left)
                    {
                        uncle = grandParent.Right;

                        /* Case 1 :  The uncle of pt is also red. Only ReColoring required */
                        if (uncle != null && uncle.Color == Color.RED)
                        {
                            parent.Color = Color.BLACK;
                            grandParent.Color = Color.RED;
                            uncle.Color = Color.BLACK;
                            pt = grandParent;
                        }
                        else
                        {
                            /* Case 2: pt is Right child of its Parent. Left-rotation required */
                            if (pt == parent.Right)
                            {
                                LeftRotate(pt.Parent);
                                pt = pt.Parent;
                            }
                            /* Case 3: pt is Left child of its Parent. Right-rotation required */
                            pt.Parent.Color = Color.BLACK;
                            pt.Parent.Parent.Color = Color.RED;
                            RightRotate(pt.Parent.Parent);
                        }
                    }
                    /* Case B: Parent of pt is Right child of GrandParent of pt */
                    else
                    {
                        uncle = grandParent.Left;

                        /*  Case 1: The uncle of pt is also red. Only ReColoring required */
                        if (uncle != null && uncle.Color == Color.RED)
                        {
                            grandParent.Color = Color.RED;
                            parent.Color = Color.BLACK;
                            uncle.Color = Color.BLACK;
                            pt = grandParent;
                        }
                        else
                        {
                            /* Case 2: pt is Left child of its Parent. Right-rotation required */
                            if (pt == parent.Left)
                            {
                                RightRotate(pt.Parent);
                                pt = pt.Parent;
                            }
                            /* Case 3: pt is Right child of its Parent. Left-rotation required */
                            pt.Parent.Color = Color.BLACK;
                            pt.Parent.Parent.Color = Color.RED;
                            LeftRotate(pt.Parent.Parent);
                        }
                    }
                }
                root.Color = Color.BLACK;
            }
        }

        public void DeleteNode(int data)
        {
            if (root.Data == data && root.Left == null && root.Right == null)
                root = null;
            else
                DeleteNodeHelper(this.root, data);
        }
    }
}
