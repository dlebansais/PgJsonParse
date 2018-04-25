namespace PgJsonParse
{
    public class Sequence
    {
        #region Init
        public static Sequence Create(int maxCount, int maxValue)
        {
            Sequence Result = new Sequence(maxCount, maxValue);
            return Result;
        }

        public static Sequence CreateSeparate(int maxCount, int maxValue)
        {
            Sequence Result = new Sequence(maxCount, maxValue);
            Result.NextSeparate();
            return Result;
        }

        private Sequence(int maxCount, int maxValue)
        {
            Array = new int[maxCount];
            MaxValue = maxValue;
            IsCompleted = false;
        }
        #endregion

        #region Properties
        public int[] Array { get; private set; }
        public int MaxValue { get; private set; }
        public bool IsCompleted { get; private set; }
        #endregion

        #region Client Interface
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

        private bool IsSequenceSeparate
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
        #endregion
    }
}
