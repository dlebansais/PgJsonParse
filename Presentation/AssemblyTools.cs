using System.Reflection;

namespace Presentation
{
    public class AssemblyTools
    {
        public static string GetCurrentAssemblyName()
        {
            Assembly Current = Assembly.GetCallingAssembly();
            return Current.GetName().Name;
        }
    }
}
