#if CSHARP_XAML_FOR_HTML5
using Presentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
#else
using System;
using System.Windows;
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
            StartupUri = new Uri("PrologueWindow.xaml", UriKind.Relative);
#endif
        }

#if CSHARP_XAML_FOR_HTML5
        public List<RootControl> Windows { get; } = new List<RootControl>();
#endif
    }
}
