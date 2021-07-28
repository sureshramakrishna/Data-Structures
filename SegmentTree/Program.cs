using System;
using System.Diagnostics;

namespace SegmentTree
{
    class Program
    {
        static void Main()
        {
            int[] input = { 0, 3, 4, 2, 1, 6, -1 };

            SegmentTree st = new SegmentTree(input);
            Debug.Assert(0 == st.RangeMinimumQuery(0, 3));
            Debug.Assert(1 == st.RangeMinimumQuery(1, 5));
            Debug.Assert(1 == st.RangeMinimumQuery(1, 6));
        }
    }

    /// <summary>
    /// Implementation of segment tree for Min in a give range.
    /// Can also be used for Max or Sum of a given range
    /// </summary>
    class SegmentTree
    {
        private readonly int[] _segmentTree;
        private int _inputLength;
        public SegmentTree(int[] inputArray)
        {
            _segmentTree = new int[2 * NextPowerOf2(inputArray.Length) - 1];
            for (var i = 0; i < _segmentTree.Length; i++)
                _segmentTree[i] = int.MaxValue;
            _inputLength = inputArray.Length;
            ConstructSegmentTree(inputArray, 0, inputArray.Length - 1, 0);
        }

        public void ConstructSegmentTree(int[] inputArray, int low, int high, int pos)
        {
            if (low == high)
                _segmentTree[pos] = inputArray[low];
            else
            {
                int mid = (low + high) / 2;
                ConstructSegmentTree(inputArray, low, mid, 2 * pos + 1);
                ConstructSegmentTree(inputArray, mid + 1, high, 2 * pos + 2);
                _segmentTree[pos] = Math.Min(_segmentTree[2 * pos + 1], _segmentTree[2 * pos + 2]);
            }
        }
        public int RangeMinimumQuery(int qLow, int qHigh)
        {
            return RangeMinimumQuery(0, _inputLength - 1, qLow, qHigh, 0);
        }

        private int RangeMinimumQuery(int low, int high, int qLow, int qHigh, int pos)
        {
            if (qLow <= low && qHigh >= high)
                return _segmentTree[pos];
            
            if (qLow > high || qHigh < low)
                return int.MaxValue;
            
            int mid = (low + high) / 2;
            return Math.Min(RangeMinimumQuery(low, mid, qLow, qHigh, 2 * pos + 1), RangeMinimumQuery(mid + 1, high, qLow, qHigh, 2 * pos + 2));
        }
        /// <summary>
        /// This method works by finding the most signification bit set in the input
        /// Since a binary is represented using power of 2's, if a number is power of 2, then only 1 bit will be set in the number.
        /// AND of number and number - 1 will give 0 if only 1 bit is set. otherwise it gives > 0.
        /// While loops until only 1 bit (MSB) remains in the number
        /// </summary>
        /// <returns>Returns next power of the number if number is not already a power of 2, otherwise returns number</returns>
        private int NextPowerOf2(int num)
        {
            if (num == 0)
                return 1;
            if (num > 0 && (num & (num - 1)) == 0)
                return num;

            while ((num & (num - 1)) > 0)
                num &= num - 1;
            return num << 1;
        }
    }
}
