using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTREE
{
    class Program
    {
        /*Group Memebers:
          Rizwan Khan Turk 17B-001-SE
          M Mahad Umer 17B-009-SE
          Saddam Hussain 17B-055-SE*/
        static void Main(string[] args)
        {
            AVL tree = new AVL();
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(2);
            tree.Add(11);
            tree.Add(12);
            tree.Add(13);
            //After Insertions BST Becomes AVL Tree by using rotations
            //The Tree Now displayed is AVL Tree 
            Console.WriteLine("In Order");
            tree.DisplayTreeInOrder();
            Console.WriteLine("Pre Order");
            tree.DisplayTreePreOrder();
            Console.WriteLine("Post Order");
            tree.DisplayTreePostOrder();
            //To Find a Inserted Node
            tree.FindNode(7);
            //After Deletion of Node
            int a = 0;
            Console.Write("Which element you want to delete:");
            a = Convert.ToInt16(Console.ReadLine());
            tree.Delete(a);
            Console.WriteLine("After Deleting: {0}",a);
            //The Tree checks the balance factor of each node after each insertion & deletion 
            //and performs the required rotation if needed
            Console.WriteLine("Now Display AVL Tree: InOrder");
            tree.DisplayTreeInOrder();
        }
    }
    class Node
    {
        public int data;
        public Node left;
        public Node right;
        public Node()
        {
            data = 0;
        }
        public Node(int e)
        {
            data = e;
        }
    }
    class AVL
    {
        Node root;
        public AVL() { }

        public void Add(int data)
        {
            Node n = new Node(data);
            //If there is no root of tree
            // Make it the root of tree
            if (root == null)
            {
                root = n;
            }
            // else check where to insert the node
            else
            {
                root = Insert(root, n);
            }
        }
        private Node Insert(Node current, Node node)
        {
            if (current == null)
            {
                current = node;
                return current;
            }
            //If the data of node to be inserted is less than current node data
            //Insert the node to the left of current node 
            else if (node.data < current.data)
            {
                current.left = Insert(current.left, node);
                current = BalanceTree(current);
            }
            //If the data of node to be inserted is greater than current node data
            //Insert the node to the right of current node 
            else if (node.data > current.data)
            {
                current.right = Insert(current.right, node);
                current = BalanceTree(current);
            }
            return current;
        }

        private Node BalanceTree(Node current)
        {
            //Get the Balance Factor of the Node
            int bfactor = BalanceFactor(current);
            //If Balance Factor is found to be greater than 1 
            if (bfactor > 1)
            {
                if (BalanceFactor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            //If Balance Factor is found to be less than -1 
            else if (bfactor < -1)
            {
                if (BalanceFactor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }
        //Balance Factor of a node can be found by the formula
        //BalanceFactor = height of Leftsubtree - Height of Rightsubtree;
        private int BalanceFactor(Node current)
        {
            int leftsubtree = GetHeight(current.left);
            int rightsubtree = GetHeight(current.right);
            int balance_factor = leftsubtree - rightsubtree;
            return balance_factor;
        }

        //To delete a Node
        public void Delete(int del)
        {
            root = Delete(root, del);
        }
        private Node Delete(Node current, int del)
        {
            Node parent;
            if (current == null)
            { return null; }
            else
            {
                // We check in left subtree
                if (del < current.data)
                {
                    current.left = Delete(current.left, del);
                    if (BalanceFactor(current) == -2)//here
                    {
                        if (BalanceFactor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                // We check in right subtree
                else if (del > current.data)
                {
                    current.right = Delete(current.right, del);
                    if (BalanceFactor(current) == 2)
                    {
                        if (BalanceFactor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                // if node to be delete is found
                else
                {
                    if (current.right != null)
                    {
                        // delete its inorder successor
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.data = parent.data;
                        current.right = Delete(current.right, parent.data);
                        // rebalancing
                        if (BalanceFactor(current) == 2)
                        {
                            if (BalanceFactor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }

        //To Find a Node in the AVL Tree
        public void FindNode(int node)
        {
            try
            {
                if (FindNode(node, root).data == node)
                {
                    Console.WriteLine("{0} was found!", node);
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Not Found!");
            }
        }

        //Private function to find the node
        private Node FindNode(int node, Node current)
        {

            if (node < current.data)
            {
                if (node == current.data)
                {
                    return current;
                }
                else
                {
                    return FindNode(node, current.left);
                }
            }
            else
            {
                if (node == current.data)
                {
                    return current;
                }
                else
                {
                    return FindNode(node, current.right);
                }
            }

        }

        // Display the AVL Tree using InOrder i.e left,root,right 
        public void DisplayTreeInOrder()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            InOrder(root);
            Console.WriteLine();
        }

        // Display the AVL Tree using PreOrder i.e root,left,right 
        public void DisplayTreePreOrder()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            PreOrder(root);
            Console.WriteLine();
        }

        // Display the AVL Tree using PostOrder i.e left,right,root 
        public void DisplayTreePostOrder()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            PostOrder(root);
            Console.WriteLine();
        }

        // Private Functions to display the tree according to the desired order by using roots
        private void InOrder(Node current)
        {
            if (current != null)
            {
                InOrder(current.left);
                Console.Write("({0}) ", current.data);
                InOrder(current.right);
            }
        }

        private void PreOrder(Node current)
        {
            if (current != null)
            {
                Console.Write("({0}) ", current.data);
                PreOrder(current.left);
                PreOrder(current.right);
            }
        }

        private void PostOrder(Node current)
        {
            if (current != null)
            {
                PostOrder(current.left);
                PostOrder(current.right);
                Console.Write("({0}) ", current.data);
            }
        }

        //to find Maximum Height of a Node by checking in its left and right sides
        private int Max(int l, int r)
        {
            if (l > r)
            {
                return l;
            }
            else
            {
                return r;
            }
        }

        //to get the height of a Node
        private int GetHeight(Node current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.left);
                int r = GetHeight(current.right);
                int m = Max(l, r);
                height = m + 1;
            }
            return height;
        }

        //We perform rotations after insertion and deletion of elements
        //To maintain the Characteristic of AVL Tree which is the balance factor
        //There are 4 types of rotations
 
        //We perform this rotation when an element is inserted in the right of right subtree
        private Node RotateLL(Node parent)
        {
            Node temp = parent.left;
            parent.left = temp.right;
            temp.right = parent;
            return temp;
        }

        //We perform this rotation when an alement is inserted in the left of left subtree
        private Node RotateRR(Node parent)
        {
            Node temp = parent.right;
            parent.right = temp.left;
            temp.left = parent;
            return temp;
        }

        //We perform this rotation when an element is inserted in the right of left subtree 
        private Node RotateLR(Node parent)
        {
            Node temp = parent.left;
            parent.left = RotateRR(temp);
            return RotateLL(parent);
        }

        //We perform this rotation when an element is inserted in the left of right subtree 
        private Node RotateRL(Node parent)
        {
            Node temp = parent.right;
            parent.right = RotateLL(temp);
            return RotateRR(parent);
        }
    }
}