using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    class JumpSearch
    {
        public static int Search<T>(T[] arr, T x) where T : IComparable
        {
            int n = arr.Length;
            int step = (int)Math.Sqrt(n);
            int i, prev = 0;
            for (i = 0; i < n; i += step)
            {
                if (arr[i].CompareTo(x) < 0)
                    prev = i;
                else
                    break;
            }
            for (; prev <= (n < i ? n : i); prev++)
                if (arr[prev].Equals(x))
                    return prev;
            
            return int.MinValue;
        }
    }
}
