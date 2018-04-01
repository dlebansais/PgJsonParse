namespace Presentation
{
    public class Cancellation
    {
        public bool IsCanceled { get; private set; }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
