using System;
using System.Linq;
using Xunit;

namespace SearchAlgorithms
{
    /// <summary>
    /// Enumerable.Repeat generates an array with value 0 given number of times, in this case, its some random number
    /// </summary>
    class Program
    {
        static void Main(string[] _)
        {
            var rnd = new Random();
            var arrSize = rnd.Next(1, 100);
            int[] arr = Enumerable.Repeat(0, arrSize).Select(x => rnd.Next(int.MinValue, int.MaxValue)).OrderBy(x => x).ToArray();
            var itemToSearch = arr[rnd.Next(0, arrSize)];

            var ls = LinearSearch.Search(arr, itemToSearch);
            var bs = BinarySearch.Search(arr, itemToSearch);
            var js = JumpSearch.Search(arr, itemToSearch);
            var es = ExponentialSearch.Search(arr, itemToSearch);
            var ts = TernarySearch.Search(arr, itemToSearch);
            Assert.Equal(ls, bs);
            Assert.Equal(bs, js);
            Assert.Equal(js, es);
            Assert.Equal(es, ts);
        }
    }
}
