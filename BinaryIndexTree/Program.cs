using System;
using System.Diagnostics;

namespace BinaryIndexTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 1, 2, 3, 4, 5, 6, 7 };
            FenwickTree ft = new FenwickTree(input);
            Debug.Assert(1 == ft.GetSum(0));
            Debug.Assert(3 == ft.GetSum(1));
            Debug.Assert(6 == ft.GetSum(2));
            Debug.Assert(10 == ft.GetSum(3));
            Debug.Assert(15 == ft.GetSum(4));
            Debug.Assert(21 == ft.GetSum(5));
            Debug.Assert(28 == ft.GetSum(6));
        }
    }
    /// <summary>
    /// https://www.youtube.com/watch?v=CWDQJGaN1gY
    /// </summary>
    public class FenwickTree
    {
        private readonly int[] _binaryIndexTree;
        public FenwickTree(int[] input)
        {
            _binaryIndexTree = new int[input.Length + 1];
            CreateTree(input);
        }
        
        public void CreateTree(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
                UpdateBinaryIndexedTree(input[i], i + 1);
        }

        /// <summary>
        /// Start from index+1 if you updating index in original array.Keep adding this value
        /// for next node till you reach outside range of tree
        /// </summary>
        public void UpdateBinaryIndexedTree(int val, int index)
        {
            while (index < _binaryIndexTree.Length)
            {
                _binaryIndexTree[index] += val;
                index = GetNext(index);
            }
        }

        /// <summary>
        /// Start from index+1 if you want prefix sum 0 to index. Keep adding value till you reach 0
        /// </summary>
        public int GetSum(int index)
        {
            index += 1;
            int sum = 0;
            while (index > 0)
            {
                sum += _binaryIndexTree[index];
                index = GetParent(index);
            }
            return sum;
        }


        /**
         * To get parent
         * 1) GET 2's complement of the index: -ve version of the index gives 2's complement
         * 2) AND this with index
         * 3) Subtract that from index
         */
        private static int GetParent(int index)
        {
            return index - (index & -index);
        }

        /**
         * To get next
         * 1) GET 2's complement of the index: -ve version of the index gives 2's complement
         * 2) AND this with index
         * 3) ADD it to index
         */
        private static int GetNext(int index)
        {
            return index + (index & -index);
        }
    }
}
