namespace PgJsonParse
{
    public class Sequence
    {
        public static Sequence Create(int MaxCount, int MaxValue)
        {
            Sequence Result = new Sequence(MaxCount, MaxValue);
            return Result;
        }

        public static Sequence CreateSeparate(int MaxCount, int MaxValue)
        {
            Sequence Result = new Sequence(MaxCount, MaxValue);
            Result.NextSeparate();
            return Result;
        }

        private Sequence(int MaxCount, int MaxValue)
        {
            Array = new int[MaxCount];
            this.MaxValue = MaxValue;
            Index = 0;
            IsCompleted = false;
        }

        public void Next()
        {
            for (int ArrayIndex = 0; ArrayIndex < Array.Length; ArrayIndex++)
            {
                if (Array[ArrayIndex] + 1 < MaxValue)
                {
                    Array[ArrayIndex]++;
                    return;
                }
                else
                    Array[ArrayIndex] = 0;
            }

            IsCompleted = true;
        }

        public void NextSeparate()
        {
            do
                Next();
            while (!IsCompleted && !IsSequenceSeparate);
        }

        public bool IsSequenceSeparate
        {
            get
            {
                for (int i = 0; i < Array.Length; i++)
                    for (int j = i + 1; j < Array.Length; j++)
                        if (Array[i] <= Array[j])
                            return false;

                return true;
            }
        }
        public int[] Array;
        public int MaxValue;
        public bool IsSeparate;
        public int Index;
        public bool IsCompleted;
    }
}
