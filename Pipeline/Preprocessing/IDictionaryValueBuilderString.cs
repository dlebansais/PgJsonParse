namespace Preprocessor;

public interface IDictionaryValueBuilderString<TElement, TRawElement>
{
    TElement FromRaw(string key, TRawElement rawElement);
    TRawElement ToRaw(TElement element);
}
