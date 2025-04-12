namespace Week11;

/***********************************************************
* CSC205 Assignment 11 
* Question 1
***********************************************************/

using System;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    /*
     * 1) CheckParenthesesA is a method that checks if a string has balanced parentheses.
     *    It uses a Stack to push every opening '(' and pop for every closing ')'. 
     *    The method also returns false early if the stack becomes empty before the last character,
     *    which means there were more closing than opening parentheses at some point before the end.
     *    If the loop finishes and the stack is empty, the parentheses are balanced.
     */
    static bool CheckParenthesesA(string str)
    {
        var stk = new Stack<char>();
        for (int i = 0; i < str.Length; i++)
        {
            // Push any '(' onto the stack
            if (str[i] == '(')
            {
                stk.Push(str[i]);
            }
            else if (str[i] == ')' && !stk.IsEmpty()) // Pop for each ')', but only if the stack is not empty
            {
                stk.Pop();
            }
            // If the stack is empty before the last character, return false
            if (stk.IsEmpty() && i < str.Length - 1)
            {
                return false;
            }
        } // At the end, return true if the stack is empty (all parentheses matched)
        return stk.IsEmpty();
    } // end of CheckParenthesesA

    /*
     * 2) CheckParenthesesB is a method that checks for the position of the first unmatched parenthesis.
     *    If all parentheses are balanced, it returns -1.
     *    If there is an extra closing ')', it returns its index.
     *    If there is an extra opening '(', it returns the index of the last unmatched '('.
     *    This method helps not just to check validity, but also to locate the error.
     */
    static int CheckParenthesesB(string str)
    {
        var stk = new Stack<int>();
        for (int i = 0; i < str.Length; i++)
        {
            // Push index onto stack when an opening '(' is found
            if (str[i] == '(')
            {
                stk.Push(i);
            }
            // If a closing ')' is found
            if (str[i] == ')')
            {
                if (!stk.IsEmpty())
                {
                    // Pop a matched '(' index off the stack
                    stk.Pop();
                }
                else
                {
                    // If stack is empty, this ')' has no matching '(', return its index
                    return i;
                }
            }
        }
        // If the stack is not empty, return the index of the last unmatched '('
        if (!stk.IsEmpty())
        {
            return (stk.Pop());
        }
        else
        {
            return -1;
        }
    } // end of CheckParenthesesB

    static void Main(string[] args)
    {
        string[] testStrings = {
        "",            // Empty string
        "()",          // Simple valid
        "(())",        // Nested valid
        "(()())",      // Balanced complex
        "(()",         // Missing closing
        "())",         // Extra closing
        ")(",          // Wrong order
        "((())())",    // Balanced, multi-layered
        "(abc(def)ghi)", // Balanced with characters
        "(abc(def)ghi",  // Missing closing
        "abc)def(",     // Incorrect extra closing then opening
    };

        Console.WriteLine("Testing CheckParenthesesA:");
        foreach (var test in testStrings)
        {
            Console.WriteLine($"Input: \"{test}\" => Balanced: {CheckParenthesesA(test)}");
        }

        Console.WriteLine("\nTesting CheckParenthesesB:");
        foreach (var test in testStrings)
        {
            int result = CheckParenthesesB(test);
            if (result == -1)
                Console.WriteLine($"Input: \"{test}\" => Balanced (no unmatched).");
            else
                Console.WriteLine($"Input: \"{test}\" => Unmatched parenthesis at index {result}.");
        }
    } // end of Main

    // A simple Stack implementation for your convenience.
    public class Stack<TYPE>
    {
        TYPE[] values; //data stored in an array
        int top = -1, capacity = 1024;
        public Stack()
        {
            values = new TYPE[capacity];
            top = -1;
        }
        public bool IsEmpty()
        {
            return (top < 0);
        }
        public bool Push(TYPE data)
        {
            if (top == capacity - 1)
            {
                throw new Exception("Stack overflow!");
            }
            else
            {
                values[++top] = data;
                return true;
            }
        }
        public TYPE Pop()
        {
            if (top < 0)
            {
                throw new Exception("Can't pop an empty stack");
            }
            else
                return values[top--];
        }
        public TYPE Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Peek - no item in an empty stack");
                return default(TYPE);
            }
            else
                return values[top];
        }
        public void Clear()
        {
            top = -1;
        }
        public void Display()
        {
            if (top < 0)
            {
                Console.WriteLine("Empty stack");
                return;
            }
            else
            {
                Console.Write("Stack content: \ntop ->");
                for (int i = top; i >= 0; i--)
                {
                    Console.Write(" " + values[i]);
                }
                Console.WriteLine();
            }
        }
    }
}