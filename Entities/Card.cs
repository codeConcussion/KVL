using System;
using System.Collections.Generic;

namespace CodeConcussion.KVL.Entities
{
    public sealed class Card
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public Operation Operation { get; set; }

        public int Answer => _answerStrategy[Operation](FirstNumber, SecondNumber);

        private readonly Dictionary<Operation, Func<int, int, int>> _answerStrategy = new Dictionary<Operation, Func<int, int, int>>
        {
            { Operation.Addition, (x, y) => x + y },
            { Operation.Multiplication, (x, y) => x * y }
        };
    }
}