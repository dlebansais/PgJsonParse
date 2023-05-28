namespace Translator;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PgObjects;

public static class Inserter<T>
{
    public static bool SetItemByKey(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
    {
        return SetItemByStringKey(ParsingContext.ObjectKeyTable, setter, value, errorControl);
    }

    public static bool SetItemByName(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
    {
        return SetItemByStringKey(ParsingContext.ObjectNameTable, setter, value, errorControl);
    }

    public static bool SetItemByInternalName(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
    {
        return SetItemByStringKey(ParsingContext.ObjectInternalNameTable, setter, value, errorControl);
    }

    private static bool SetItemByStringKey(Dictionary<Type, Dictionary<string, ParsingContext>> table, Action<T> setter, object value, ErrorControl errorControl)
    {
        if (!(value is string ValueKey))
            return Program.ReportFailure($"Value '{value}' was expected to be a string");

        Type LinkType = typeof(T);
        if (!table.ContainsKey(LinkType))
            return Program.ReportFailure($"Type {LinkType} does not have items with keys");

        Dictionary<string, ParsingContext> KeyTable = table[LinkType];

        if (!KeyTable.ContainsKey(ValueKey))
            return Program.ReportFailure($"Key '{ValueKey}' is not a known key", errorControl);

        if (!(KeyTable[ValueKey].Item is T AsLink))
            return Program.ReportFailure($"Key '{ValueKey}' was found but for the wrong object type");

        setter(AsLink);
        return true;
    }

    public static bool SetItemById(Action<T> setter, object value, ErrorControl errorControl = ErrorControl.Normal)
    {
        return SetItemByIntKey(ParsingContext.ObjectIdTable, setter, value, errorControl);
    }

    private static bool SetItemByIntKey(Dictionary<Type, Dictionary<int, ParsingContext>> table, Action<T> setter, object value, ErrorControl errorControl)
    {
        if (!(value is int ValueId))
            return Program.ReportFailure($"Value '{value}' was expected to be an Id");

        Type LinkType = typeof(T);
        if (!table.ContainsKey(LinkType))
            return Program.ReportFailure($"Type {LinkType} does not have items with keys");

        Dictionary<int, ParsingContext> KeyTable = table[LinkType];

        if (!KeyTable.ContainsKey(ValueId))
            return Program.ReportFailure($"Key '{ValueId}' is not a known key", errorControl);

        if (!(KeyTable[ValueId].Item is T AsLink))
            return Program.ReportFailure($"Key '{ValueId}' was found but for the wrong object type");

        setter(AsLink);
        return true;
    }

    public static bool AddArrayByKey(List<T> linkList, object value)
    {
        return AddArrayByString(ParsingContext.ObjectKeyTable, linkList, value);
    }

    public static bool AddPgObjectArrayByKey<TObject>(List<string> keyList, object value)
        where TObject : PgObject
    {
        List<T> LinkList = new();
        if (!AddArrayByString(ParsingContext.ObjectKeyTable, LinkList, value))
            return false;

        foreach (T Item in LinkList)
            if (Item is TObject ItemObject)
                keyList.Add(ItemObject.Key);

        return true;
    }

    public static bool AddArrayByName(List<T> linkList, object value)
    {
        return AddArrayByString(ParsingContext.ObjectNameTable, linkList, value);
    }

    public static bool AddArrayByInternalName(List<T> linkList, object value)
    {
        return AddArrayByString(ParsingContext.ObjectInternalNameTable, linkList, value);
    }

    public static bool AddPgObjectArrayByInternalName<TObject>(List<string> keyList, object value)
        where TObject : PgObject
    {
        List<T> LinkList = new();
        if (!AddArrayByString(ParsingContext.ObjectInternalNameTable, LinkList, value))
            return false;

        foreach (T Item in LinkList)
            if (Item is TObject ItemObject)
                keyList.Add(ItemObject.Key);

        return true;
    }

    public static bool AddArrayByString(Dictionary<Type, Dictionary<string, ParsingContext>> table, List<T> linkList, object value)
    {
        if (!(value is List<object> ArrayKey))
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        Type LinkType = typeof(T);
        if (!table.ContainsKey(LinkType))
            return Program.ReportFailure($"Type {LinkType} does not have items with keys");

        Dictionary<string, ParsingContext> KeyTable = table[LinkType];

        foreach (object Item in ArrayKey)
        {
            if (!(Item is string ValueKey))
                return Program.ReportFailure($"Value '{Item}' was expected to be a string");

            if (!KeyTable.ContainsKey(ValueKey))
                return Program.ReportFailure($"Key '{Item}' is not a known key");

            if (!(KeyTable[ValueKey].Item is T AsLink))
                return Program.ReportFailure($"Key '{Item}' was found but for the wrong object type");

            linkList.Add(AsLink);
        }

        return true;
    }

    public static bool AddKeylessArray(List<T> linkList, object value)
    {
        if (value is List<object> ArrayObject)
        {
            foreach (object Item in ArrayObject)
            {
                if (!(Item is ParsingContext ItemContext))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a context");

                if (ItemContext.Item == null)
                    ItemContext.FinishItem();

                if (!(ItemContext.Item is T ValueObject))
                    return Program.ReportFailure($"Item was found but for the wrong object type");

                linkList.Add(ValueObject);
            }
        }
        else if (value is ParsingContext ItemContext)
        {
            if (ItemContext.Item == null)
                ItemContext.FinishItem();

            if (ItemContext.Item is T ValueObject)
                linkList.Add(ValueObject);
            else if (ItemContext.Item is ICollection<T> ValueCollection)
                linkList.AddRange(ValueCollection);
            else
                return Program.ReportFailure($"Item was found but for the wrong object type");
        }
        else
            return Program.ReportFailure($"Value '{value}' was expected to be a list");

        return true;
    }

    public static bool SetItemProperty(Action<T> setter, object value)
    {
        if (!(value is ParsingContext Context))
            return Program.ReportFailure($"Value '{value}' was expected to be a context");

        if (Context.Item == null)
            Context.FinishItem();

        if (!(Context.Item is T AsItem))
            return Program.ReportFailure($"Item was found but for the wrong object type");

        setter(AsItem);
        return true;
    }

    public static bool SetNpc(Action<PgNpcLocation> setter, object value, string parsedFile, string parsedKey, ErrorControl errorControl = ErrorControl.Normal)
    {
        if (!(value is string ValueName))
            return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

        if (ValueName.Length == 0)
            return true;

        string[] AreaNpc = ValueName.Split('/');
        switch (AreaNpc.Length)
        {
            case 1:
                return SetNpcNoZone(setter, MapAreaName.Internal_None, ValueName, parsedFile, parsedKey, errorControl);
            case 2:
                return SetNpcWithZone(setter, AreaNpc[0], AreaNpc[1], parsedFile, parsedKey, errorControl);
            default:
                return Program.ReportFailure(parsedFile, parsedKey, $"'{value}' is not a NPC name", errorControl);
        }
    }

    public static bool SetNpcWithZone(Action<PgNpcLocation> setter, string rawMapName, string npcId, string parsedFile, string parsedKey, ErrorControl errorControl)
    {
        Debug.Assert(!string.IsNullOrEmpty(npcId));

        if (!rawMapName.StartsWith("Area"))
            return Program.ReportFailure(parsedFile, parsedKey, $"'{rawMapName}' does not contain an area name", errorControl);

        string AreaName = rawMapName.Substring(4);
        if (!StringToEnumConversion<MapAreaName>.TryParse(AreaName, out MapAreaName ParsedAreaName))
            return false;

        return SetNpcNoZone(setter, ParsedAreaName, npcId, parsedFile, parsedKey, errorControl);
    }

    public static bool SetNpcNoZone(Action<PgNpcLocation> setter, MapAreaName areaName, string npcId, string parsedFile, string parsedKey, ErrorControl errorControl)
    {
        PgNpc ParsedNpc = null!;
        SpecialNpc NpcEnum = SpecialNpc.Internal_None;
        string NpcName = string.Empty;

        PgNpcLocation NpcLocation = new PgNpcLocation();
        NpcLocation.NpcId = npcId;

        if (Inserter<PgNpc>.SetItemByKey((PgNpc valueNpc) => ParsedNpc = valueNpc, npcId, ErrorControl.IgnoreIfNotFound))
            NpcLocation.Npc_Key = ParsedNpc.Key;
        else if (StringToEnumConversion<SpecialNpc>.TryParse(npcId, out NpcEnum, ErrorControl.IgnoreIfNotFound))
            NpcLocation.NpcEnum = NpcEnum;
        else if (npcId.ToUpper().StartsWith("NPC_"))
            NpcLocation.NpcName = npcId.Substring(4);
        else
            return Program.ReportFailure(parsedFile, parsedKey, $"'{npcId}' unknown NPC name", errorControl);

        ParsingContext.AddSuplementaryObject(NpcLocation);

        Debug.Assert(!string.IsNullOrEmpty(NpcLocation.NpcId));
        Debug.Assert(NpcLocation.Npc_Key != null || NpcLocation.NpcEnum != SpecialNpc.Internal_None || NpcLocation.NpcName.Length > 0);

        setter(NpcLocation);
        return true;
    }
}
