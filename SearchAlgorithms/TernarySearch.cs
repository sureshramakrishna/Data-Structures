using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    class TernarySearch
    {
        public static int Search<T>(T[] arr, T x) where T : IComparable
        {
            return InnerAlgorithm(arr, x, 0, arr.Length - 1);
        }
        static int InnerAlgorithm<T>(T[] arr, T key, int l, int r) where T : IComparable
        {
            if (r >= l)
            {
                // Find the mid1 and mid2 
                int mid1 = l + (r - l) / 3;
                int mid2 = r - (r - l) / 3;

                // Check if key is present at any mid 
                if (arr[mid1].Equals(key))
                    return mid1;
                else if (arr[mid2].Equals(key))
                    return mid2;
                else
                {
                    if (key.CompareTo(arr[mid1]) < 0)
                        return InnerAlgorithm(arr, key, l, mid1 - 1);
                    else if (key.CompareTo(arr[mid2]) > 0)
                        return InnerAlgorithm(arr, key, mid2 + 1, r);
                    else
                        return InnerAlgorithm(arr, key, mid1 + 1, mid2 - 1);
                }
            }
            return int.MinValue;
        }
    }
}
