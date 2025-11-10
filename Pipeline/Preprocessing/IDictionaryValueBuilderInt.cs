namespace Preprocessor;

public interface IDictionaryValueBuilderInt<TElement, TRawElement>
{
    TElement FromRaw(int key, TRawElement rawElement);
    TRawElement ToRaw(TElement element);
}
