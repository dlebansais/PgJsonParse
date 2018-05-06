using Presentation;

namespace PgJsonParse
{
    public partial class App : RootApp
    {
        public App()
        {
            InitializeComponent();
            InitializeApp();
        }

        protected override RootControl CreateRootControl()
        {
            return new PrologueWindow();
        }
    }
}
