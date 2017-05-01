using System;
using System.Collections.Generic;
using System.Diagnostics;
using CodeConcussion.KVL.Entities;

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
            if (value == Operation.Addition) return "+";
            if (value == Operation.Multiplication) return "x";
            return "";
        }

        [DebuggerStepThrough]
        public static string GetTiming(this decimal value)
        {
            if (value <= 0) return "";
            var minutes = (int)value / 60;
            var seconds = value - (minutes * 60);
            return $"{minutes:D2}:{seconds:00.0}";
        }
    }
}