#if CSHTML5
using Windows.UI.Xaml;

namespace Presentation
{
    public interface ISelectorSelection
    {
        RootControl Owner { get; }
        bool IsSelected { get; set; }
    }
}
#endif
