namespace Preprocessor;

public interface IHasParentKey<T>
{
    T ParentKey { get; set; }

    string? ParentProperty { get; set; }
}
