namespace CodeConcussion.KVL.Entity
{
    internal sealed class Card
    {
        public Operation Operation { get; set; }
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }

        public int Answer
        {
            get
            {
                if (Operation == Operation.Addition) return FirstNumber + SecondNumber;
                if (Operation == Operation.Multiplication) return FirstNumber * SecondNumber;
                return 0;
            }
        }
    }
}