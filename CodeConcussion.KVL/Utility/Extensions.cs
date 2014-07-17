using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodeConcussion.KVL.Entity;

namespace CodeConcussion.KVL.Utility
{
    public static class Extensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T x in enumerable) action(x);
        }

        [DebuggerStepThrough]
        public static string GetSign(this Operation value)
        {
            if (value == Operation.Addition) return "+";
            if (value == Operation.Multiplication) return "*";
            return "";
        }
    }
}