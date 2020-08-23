using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms
{
    class LinearSearch
    {
        public static int Search<T>(T[] arr, T x) where T : IComparable
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].Equals(x))
                    return i;
            return -1;
        }
    }
}
