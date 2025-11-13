namespace Preprocessor;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

public partial class Preprocessor
{
    public static void InsertIntDictionary<TDictionary, TValue>(IFreeSql fsql, object content)
        where TDictionary : IDictionary<int, TValue>
        where TValue : class, IHasKey<int>
    {
        fsql.Insert((IEnumerable<TValue>)((TDictionary)content).Values).ExecuteAffrows();
    }

    public static void InsertStringDictionary<TDictionary, TValue>(IFreeSql fsql, object content)
        where TDictionary : IDictionary<string, TValue>
        where TValue : class, IHasKey<string>
    {
        fsql.Insert((IEnumerable<TValue>)((TDictionary)content).Values).ExecuteAffrows();
    }

    public static void InsertArray<TList, TItem>(IFreeSql fsql, object content)
        where TList : IList<TItem>
        where TItem : class
    {
        fsql.Insert((TList)content).ExecuteAffrows();
    }

    public static void InsertSingle<T>(IFreeSql fsql, object content)
        where T : class
    {
        fsql.Insert((T)content).ExecuteAffrows();
    }

    public static void AdditionalIntInserter<TItemKey, TPropertyKey, TItem, TProperty>(IFreeSql fsql, List<object> contents)
        where TItem : IHasKey<TItemKey>
        where TProperty : class, IHasKey<TPropertyKey>, IHasParentKey<TItemKey>
    {
        List<TItem>? Items = null;
        foreach (object Content in contents)
        {
            if (Content is IEnumerable<TItem> ItemCollection)
            {
                Items = new(ItemCollection);
                break;
            }
            else if (Content is TItem SingleItem)
            {
                Items = new() { SingleItem };
                break;
            }
        }

        if (Items is null)
            return;

        var Properties = typeof(TItem).GetProperties().Where(prop => prop.PropertyType == typeof(TProperty) || prop.PropertyType == typeof(TProperty[])).ToList();

        if (!AutoIncrementedKeys.ContainsKey(typeof(TProperty)))
            AutoIncrementedKeys.Add(typeof(TProperty), 1);
        int IncrementalKey = AutoIncrementedKeys[typeof(TProperty)];

        List<TProperty> AdditionalItems = new();
        foreach (TItem item in Items)
        {
            foreach (PropertyInfo Property in Properties)
            {
                if (Property.GetValue(item) is TProperty SingleValue)
                {
                    if (SingleValue is IHasKey<int> WithIntKey)
                        WithIntKey.Key = IncrementalKey++;
                    SingleValue.ParentKey = item.Key;
                    SingleValue.ParentProperty = Property.Name;

                    AdditionalItems.Add(SingleValue);
                }
                else if (Property.GetValue(item) is TProperty[] Values)
                {
                    foreach (var Value in Values)
                    {
                        if (Value is IHasKey<int> WithIntKey)
                            WithIntKey.Key = IncrementalKey++;

                        Value.ParentKey = item.Key;
                        Value.ParentProperty = Property.Name;

                        AdditionalItems.Add(Value);
                    }
                }
            }
        }

        AutoIncrementedKeys[typeof(TProperty)] = IncrementalKey;

        if (AdditionalItems.Count > 0)
            contents.Add(AdditionalItems);

        fsql.Insert((IEnumerable<TProperty>)AdditionalItems).ExecuteAffrows();
    }

    public static object SelectIntDictionary<TDictionary, TValue>(IFreeSql fsql, object content)
        where TDictionary : IDictionary<int, TValue>
        where TValue : class, IHasKey<int>
    {
        TDictionary DictionaryContent = (TDictionary)content;
        Dictionary<int, TValue> Entries = fsql.Select<TValue>().ToDictionary(item => item.Key);

        Dictionary<int, TValue> Copy = new();
        foreach (int Key in DictionaryContent.Keys)
            Copy.Add(Key, Entries[Key]);

        Entries = Copy;

        return Entries;
    }

    public static object SelectStringDictionary<TDictionary, TValue>(IFreeSql fsql, object content)
        where TDictionary : IDictionary<string, TValue>
        where TValue : class, IHasKey<string>
    {
        TDictionary DictionaryContent = (TDictionary)content;
        Dictionary<string, TValue> Entries = fsql.Select<TValue>().ToDictionary(item => item.Key);

        Dictionary<string, TValue> Copy = new();
        foreach (string Key in DictionaryContent.Keys)
            Copy.Add(Key, Entries[Key]);

        Entries = Copy;

        return Entries;
    }

    public static object SelectArray<TList, TItem>(IFreeSql fsql, object content)
        where TList : IList<TItem>
        where TItem : class
    {
        TList DictionaryContent = (TList)content;
        List<TItem> Entries = fsql.Select<TItem>().ToList();

        return Entries;
    }

    public static object SelectSingle<T>(IFreeSql fsql, object content)
        where T : class
    {
        List<T> Entries = fsql.Select<T>().ToList();

        return Entries.First();
    }

    public static void AdditionalIntSelector<TItemKey, TPropertyKey, TItem, TProperty>(IFreeSql fsql, List<object> contents, bool allowEmptyArray)
        where TItem : IHasKey<TItemKey>
        where TProperty : class, IHasKey<TPropertyKey>, IHasParentKey<TItemKey>
    {
        List<TItem>? Items = null;
        foreach (object Content in contents)
        {
            if (Content is IEnumerable<TItem> ItemCollection)
            {
                Items = new(ItemCollection);
                break;
            }
            else if (Content is TItem SingleItem)
            {
                Items = new() { SingleItem };
                break;
            }
        }

        if (Items is null)
            return;

        var Properties = typeof(TItem).GetProperties().Where(prop => prop.PropertyType == typeof(TProperty) || prop.PropertyType == typeof(TProperty[])).ToList();

        List<TProperty> AdditionalItems = new();
        foreach (TItem ParentItem in Items)
        {
            foreach (PropertyInfo Property in Properties)
            {
                List<TProperty> LocalAdditionalItems = fsql.Select<TProperty>().Where(item => item.ParentKey!.Equals(ParentItem.Key) && item.ParentProperty == Property.Name).ToList();
                bool localAllowEmptyArray = allowEmptyArray;

                if (LocalAdditionalItems.Count == 0 &&
                    Property.PropertyType.IsArray &&
                    !allowEmptyArray &&
                    typeof(TItem).GetProperty($"{Property.Name}IsEmpty") is PropertyInfo IsEmptyProperty &&
                    IsEmptyProperty.GetValue(ParentItem) is bool IsEmptyValue &&
                    IsEmptyValue)
                {
                    localAllowEmptyArray = true;
                }

                if (LocalAdditionalItems.Count > 0 || localAllowEmptyArray)
                {
                    if (!Property.PropertyType.IsArray && LocalAdditionalItems.Count > 0)
                    {
                        Property.SetValue(ParentItem, LocalAdditionalItems[0]);
                    }
                    else
                    {
                        Property.SetValue(ParentItem, LocalAdditionalItems.ToArray());
                    }

                    AdditionalItems.AddRange(LocalAdditionalItems);
                }
            }
        }

        if (AdditionalItems.Count > 0)
            contents.Add(AdditionalItems);
    }

    private static Dictionary<Type, int> AutoIncrementedKeys = new();
}
