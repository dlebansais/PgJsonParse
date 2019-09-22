using Presentation;
using System.Diagnostics;
#if CSHARP_XAML_FOR_HTML5
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
#else
using System;
using System.Windows;
using System.Windows.Threading;
#endif

namespace PgJsonParse
{
    public partial class App : Application
    {
        public App()
        {
#if CSHARP_XAML_FOR_HTML5
            try
            {
                InitializeComponent();

                RootControl Root = new PrologueWindow();
                Window.Current.Content = Root;
                Windows.Add(Root);
            }
            catch (Exception e)
            {
                Debug.WriteLine("App Exception: " + e.Message);
            }
#else
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            StartupUri = new Uri("PrologueWindow.xaml", UriKind.Relative);
#endif
        }

        void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine($"Unhandled Exception: " + e.Exception.Message);
        }

        public static void SetMainWindow(RootControl window)
        {
#if CSHARP_XAML_FOR_HTML5
            App CurrentApp = Current as App;

            foreach (RootControl ctrl in CurrentApp.Windows)
                if (ctrl is MainWindow)
                {
                    CurrentApp.Windows.Remove(ctrl);
                    break;
                }

            CurrentApp.Windows.Add(window);
#endif
        }

#if CSHARP_XAML_FOR_HTML5
        public List<RootControl> Windows { get; } = new List<RootControl>();
#endif
    }
}
