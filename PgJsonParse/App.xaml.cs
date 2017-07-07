using System.Windows;

namespace PgJsonParse
{
    public partial class App : Application
    {
        private void OnDeactivated(object sender, System.EventArgs e)
        {
            MainWindow AsMainWindow = MainWindow as MainWindow;
            AsMainWindow.CloseXpTable();
        }
    }
}
