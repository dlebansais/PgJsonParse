namespace Preprocessor;

internal interface IDictionaryValueBuilder<TElement, TRawElement>
{
    TElement FromRaw(TRawElement rawElement);
    TRawElement ToRaw(TElement element);
}
