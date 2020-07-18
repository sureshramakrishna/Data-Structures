using System;
using System.Collections.Generic;
using System.Text;

namespace Expression.Evaluation
{
    public class EvaluateString
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Expression.Evaluate("10 + 2 * 6"));
            Console.WriteLine(Expression.Evaluate("100 * 2 + 12"));
            Console.WriteLine(Expression.Evaluate("100 * ( 2 + 12 )"));
            Console.WriteLine(Expression.Evaluate("100 * ( 2 + 12 ) / 14"));
            Console.WriteLine(Expression.Evaluate("2 * (5 *(3+6))/15-2"));
        }
    }
}
