namespace Translator;

using System.Collections.Generic;
using System.Diagnostics;
using PgJsonReader;
using PgObjects;

public class ParserPower : Parser
{
    public override object CreateItem()
    {
        return new PgPower();
    }

    public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        if (item is not PgPower AsPgPower)
            return Program.ReportFailure("Unexpected failure");

        return FinishItem(AsPgPower, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
    }

    private bool FinishItem(PgPower item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
    {
        bool Result = true;

        foreach (KeyValuePair<string, object> Entry in contentTable)
        {
            string Key = Entry.Key;
            object Value = Entry.Value;

            switch (Key)
            {
                case "Prefix":
                    Result = SetStringProperty((string valueString) => item.Prefix = valueString, Value);
                    break;
                case "Suffix":
                    Result = SetStringProperty((string valueString) => item.Suffix = valueString, Value);
                    break;
                case "Tiers":
                    Result = ParseTiers(item, Value, parsedFile, parsedKey);
                    break;
                case "Slots":
                    Result = StringToEnumConversion<ItemSlot>.TryParseList(Value, item.SlotList);
                    break;
                case "Skill":
                    Result = ParserSkill.Parse((PgSkill valueSkill) => item.Skill_Key = valueSkill.Key, Value, parsedFile, parsedKey);
                    break;
                case "IsUnavailable":
                    Result = SetBoolProperty((bool valueBool) => item.RawIsUnavailable = valueBool, Value);
                    break;
                default:
                    Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                    break;
            }

            if (!Result)
                break;
        }

        return Result;
    }

    private static bool ParseTiers(PgPower item, object value, string parsedFile, string parsedKey)
    {
        PgPowerTierList PowerTierList = null!;
        if (!Inserter<PgPowerTierList>.SetItemProperty((PgPowerTierList valuePowerTierList) => PowerTierList = valuePowerTierList, value))
            return false;

        if (PowerTierList.TierList.Count == 0)
            return Program.ReportFailure(parsedFile, parsedKey, $"Power with no tiers");

        item.TierList = PowerTierList.TierList;
        return true;
    }

    public static void UpdateIconsAndNames()
    {
        Dictionary<string, ParsingContext> PowerParsingTable = ParsingContext.ObjectKeyTable[typeof(PgPower)];
        foreach (KeyValuePair<string, ParsingContext> Entry in PowerParsingTable)
        {
            PgPower Power = (PgPower)Entry.Value.Item;
            UpdateIconsAndNames(Power);

            Debug.Assert(Power.ObjectIconId != 0);
            Debug.Assert(Power.ObjectName.Length > 0);
        }
    }

    private static void UpdateIconsAndNames(PgPower power)
    {
        int IconId = 0;

        UpdateIconFromPowerTiers(power, ref IconId);
        if (IconId != 0)
            power.IconId = IconId;
        else
            power.IconId = PgObject.PowerIconId;

        string ComposedName = string.Empty;

        if (power.Prefix.Length > 0 || power.Suffix.Length > 0)
        {
            ComposedName = string.Empty;

            if (power.Prefix.Length > 0)
                ComposedName += power.Prefix;

            if (power.Suffix.Length > 0)
            {
                if (ComposedName.Length > 0)
                    ComposedName += " ";

                ComposedName += power.Suffix;
            }
        }
        else
            ComposedName = "(no name)";

        power.ComposedName = ComposedName;
    }

    private static void UpdateIconFromPowerTiers(PgPower power, ref int iconId)
    {
        Debug.Assert(power.TierList.Count > 0);

        PgPowerTier PowerTier = power.TierList[power.TierList.Count - 1];
        if (PowerTier.EffectList.Count > 0)
        {
            foreach (PgPowerEffect PowerEffect in PowerTier.EffectList)
            {
                List<int> IconIdList = new List<int>();

                if (PowerEffect is PgPowerEffectSimple AsSimple)
                    IconIdList = AsSimple.IconIdList;
                else if (PowerEffect is PgPowerEffectAttribute AsAttribute && AsAttribute.AttributeRef != null)
                    IconIdList = AsAttribute.AttributeRef.IconIdList;

                foreach (int Id in IconIdList)
                    if (Id > 0)
                    {
                        iconId = Id;
                        break;
                    }

                if (iconId > 0)
                    break;
            }
        }
    }
}
