using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeConcussion.KVL.Utility
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T x in enumerable) action(x);
        }
    }
}
