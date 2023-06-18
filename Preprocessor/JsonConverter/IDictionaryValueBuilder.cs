namespace Preprocessor;

internal interface IDictionaryValueBuilder<TItem, TRawItem>
{
    TItem ToItem(TRawItem rawItem);
    TRawItem ToRawItem(TItem item);
}
