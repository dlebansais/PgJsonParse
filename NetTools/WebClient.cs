#if USE_RESTRICTED_FEATURES
#else
using System;
using System.Threading;
using Windows.UI.Xaml;

namespace NetTools
{
    public delegate void DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs e);

    public class DownloadStringCompletedEventArgs
    {
        public DownloadStringCompletedEventArgs(string result)
        {
            Result = result;
        }

        public string Result { get; private set; }
    }

    public class WebClient
    {
        public WebClient()
        {
            DatabaseTimer = new DispatcherTimer();
            DatabaseTimer.Interval = TimeSpan.FromMilliseconds(1);
            DatabaseTimer.Tick += OnTick;
            Address = null;
        }

        private DispatcherTimer DatabaseTimer;
        private Uri Address;

        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        public void DownloadStringAsync(Uri address)
        {
            Address = address;
            DatabaseTimer.Start();
        }

        private void OnTick(object sender, object e)
        {
            Uri LocalAddress = Address;
            Address = null;

            if (LocalAddress != null)
            {
                DispatcherTimer DatabaseTimer = (DispatcherTimer)sender;
                DatabaseTimer.Stop();

                string Result = OperationHandler.Execute(LocalAddress.OriginalString);
                DownloadStringCompleted?.Invoke(this, new DownloadStringCompletedEventArgs(Result));
            }
        }
    }
}
#endif
