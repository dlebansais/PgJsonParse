using System;

namespace Presentation
{
    public class PresentationEnvironment
    {
        public static string UserRootFolder { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); } }
    }
}
