using System;
using System.Collections.Generic;

namespace Expression.Evaluation
{
    public class Expression
    {
        public static int Evaluate(string expression)
        {
            Stack<int> numbers = new Stack<int>();            //Stack for Numbers
            Stack<char> operations = new Stack<char>();       //Stack for operators

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsDigit(c))
                {
                    int num = GetNumber(expression, ref i, c);
                    numbers.Push(num);                      //Push number into stack
                }
                else if (c == '(')
                {
                    operations.Push(c);                    //Push it to operators stack
                }
                //Closed brace, evaluate the entire brace
                else if (c == ')')
                {
                    while (operations.Peek() != '(')
                    {
                        int output = PerformOperation(numbers, operations);
                        numbers.Push(output);               //Push it back to stack
                    }
                    operations.Pop();
                }
                else if (IsOperator(c))
                {
                    //1. If current operator has higher precedence than operator on top of the stack, the current operator can be placed in stack
                    // 2. else keep Popping operator from stack and perform the operation in  numbers stack till either stack is not empty or current operator has lower precedence than operator on top of the stack
                    while (operations.Count != 0 && Precedence(c) <= Precedence(operations.Peek()))
                    {
                        int output = PerformOperation(numbers, operations);
                        numbers.Push(output);                        //Push it back to stack
                    }
                    operations.Push(c);                    //now Push the current operator to stack
                }
            }
            //If here means entire expression has been processed, Perform the remaining operations in stack to the numbers stack
            while (operations.Count != 0)
            {
                int output = PerformOperation(numbers, operations);
                numbers.Push(output);                //Push it back to stack
            }
            return numbers.Pop();
        }

        //Entry is Digit, it could be greater than one digit number
        static int GetNumber(string expression, ref int i, char c)
        {
            int num = 0;
            while (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
                i++;
                if (i < expression.Length)
                    c = expression[i];
                else
                    break;
            }
            i--;
            return num;
        }

        static int Precedence(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                case '^':
                    return 3;
            }
            return -1;
        }

        static int PerformOperation(Stack<int> numbers, Stack<char> operations)
        {
            int a = numbers.Pop();
            int b = numbers.Pop();
            char operation = operations.Pop();
            switch (operation)
            {
                case '+':
                    return a + b;
                case '-':
                    return b - a;
                case '*':
                    return a * b;
                case '/':
                    if (a == 0)
                        throw new DivideByZeroException("Cannot divide by zero");
                    return b / a;
            }
            return 0;
        }

        static bool IsOperator(char c)
        {
            return (c == '+' || c == '-' || c == '/' || c == '*' || c == '^');
        }
    }
}