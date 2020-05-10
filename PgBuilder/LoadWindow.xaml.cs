namespace PgBuilder
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    public partial class LoadWindow : Window
    {
        public LoadWindow()
        {
            InitializeComponent();
            DataContext = this;

            Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(OnStartLoadWindow));
        }

        private void OnStartLoadWindow()
        {
            MainWindow Dlg = new MainWindow();
            this.Close();

            App.Current.MainWindow = Dlg;
            Dlg.Show();
        }
    }
}
