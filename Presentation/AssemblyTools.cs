using System.Reflection;

namespace Presentation
{
    public static class AssemblyTools
    {
#if CSHTML5
        public static string GetCurrentAssemblyName()
        {
            return "PgJsonObjects";
        }
#else
        public static string GetCurrentAssemblyName()
        {
            Assembly Current = Assembly.GetCallingAssembly();
            return Current.GetName().Name;
        }
#endif
    }
}
