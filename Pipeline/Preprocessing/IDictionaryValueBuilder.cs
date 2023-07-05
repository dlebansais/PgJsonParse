namespace Preprocessor;

public interface IDictionaryValueBuilder<TElement, TRawElement>
{
    TElement FromRaw(TRawElement rawElement);
    TRawElement ToRaw(TElement element);
}
