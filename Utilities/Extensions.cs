using CodeConcussion.KVL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeConcussion.KVL.Utilities
{
    public static class Extensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var x in enumerable) action(x);
        }

        [DebuggerStepThrough]
        public static string GetSign(this Operation value)
        {
            return OperationSignMap[value];
        }

        [DebuggerStepThrough]
        public static string GetTiming(this decimal value)
        {
            if (value <= 0) return "";
            var minutes = (int)value / 60;
            var seconds = value - (minutes * 60);
            return $"{minutes:D2}:{seconds:00.0}";
        }

        private static Dictionary<Operation, string> OperationSignMap = new Dictionary<Operation, string>
        {
            [Operation.Addition] = "+",
            [Operation.Subtraction] = "-",
            [Operation.Multiplication] = "x",
            [Operation.Division] = "/"
        };
    }
}