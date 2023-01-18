namespace Translator
{
    using System;
    using System.Collections.Generic;
    using PgJsonReader;
    using PgObjects;

    public class ParserItem : Parser
    {
        public override object CreateItem()
        {
            return new PgItem();
        }

        public override bool FinishItem(ref object? item, string objectKey, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            if (item is not PgItem AsPgItem)
                return Program.ReportFailure("Unexpected failure");

            return FinishItem(AsPgItem, contentTable, contentTypeTable, itemCollection, lastItemType, parsedFile, parsedKey);
        }

        private bool FinishItem(PgItem item, Dictionary<string, object> contentTable, Dictionary<string, Json.Token> contentTypeTable, List<object> itemCollection, Json.Token lastItemType, string parsedFile, string parsedKey)
        {
            bool Result = true;
            Dictionary<ItemKeyword, List<float>> KeywordTable = new Dictionary<ItemKeyword, List<float>>();
            List<string> KeywordValueList = new List<string>();
            Dictionary<string, int> SkillRequirementTable = new Dictionary<string, int>();

            foreach (KeyValuePair<string, object> Entry in contentTable)
            {
                string Key = Entry.Key;
                object Value = Entry.Value;

                switch (Key)
                {
                    case "BestowRecipes":
                        Result = ParseBestowRecipeList(item, Value, parsedFile, parsedKey);
                        break;
                    case "BestowAbility":
                        Result = Inserter<PgAbility>.SetItemByInternalName((PgAbility valueAbility) => item.BestowAbility_Key = valueAbility.Key, Value);
                        break;
                    case "BestowQuest":
                        Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => item.BestowQuest_Key = valueQuest.Key, Value);
                        break;
                    case "AllowPrefix":
                        Result = SetBoolProperty((bool valueBool) => item.SetAllowPrefix(valueBool), Value);
                        break;
                    case "AllowSuffix":
                        Result = SetBoolProperty((bool valueBool) => item.SetAllowSuffix(valueBool), Value);
                        break;
                    case "CraftPoints":
                        Result = SetIntProperty((int valueInt) => item.RawCraftPoints = valueInt, Value);
                        break;
                    case "CraftingTargetLevel":
                        Result = SetIntProperty((int valueInt) => item.RawCraftingTargetLevel = valueInt, Value);
                        break;
                    case "Description":
                        Result = SetStringProperty((string valueString) => item.Description = valueString, Value);
                        break;
                    case "DroppedAppearance":
                        Result = ParseDroppedAppearance(item, Value, parsedFile, parsedKey);
                        break;
                    case "EffectDescs":
                        Result = ParseEffectDescriptionList(item.EffectDescriptionList, Value, parsedFile, parsedKey);
                        break;
                    case "DyeColor":
                        Result = ParseDyeColor(item, Value, parsedFile, parsedKey);
                        break;
                    case "EquipAppearance":
                        Result = SetStringProperty((string valueString) => item.EquipAppearance = valueString, Value); // TODO: parse
                        break;
                    case "EquipSlot":
                        Result = StringToEnumConversion<ItemSlot>.SetEnum((ItemSlot valueEnum) => item.EquipSlot = valueEnum, Value);
                        break;
                    case "FoodDesc":
                        Result = SetStringProperty((string valueString) => item.FoodDesc = valueString, Value); // TODO: parse
                        break;
                    case "IconId":
                        Result = SetIconIdProperty((int valueInt) => item.RawIconId = valueInt, Value);
                        break;
                    case "InternalName":
                        Result = SetStringProperty((string valueString) => item.InternalName = valueString, Value);
                        break;
                    case "IsTemporary":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsTemporary(valueBool), Value);
                        break;
                    case "IsCrafted":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsCrafted(valueBool), Value);
                        break;
                    case "Keywords":
                        Result = ParseKeywordList(item, Value, KeywordTable, KeywordValueList, parsedFile, parsedKey);
                        break;
                    case "MacGuffinQuestName":
                        Result = Inserter<PgQuest>.SetItemByInternalName((PgQuest valueQuest) => item.MacGuffinQuest_Key = valueQuest.Key, Value);
                        break;
                    case "MaxCarryable":
                        Result = SetIntProperty((int valueInt) => item.RawMaxCarryable = valueInt, Value);
                        break;
                    case "MaxOnVendor":
                        Result = SetIntProperty((int valueInt) => item.RawMaxOnVendor = valueInt, Value);
                        break;
                    case "MaxStackSize":
                        Result = SetIntProperty((int valueInt) => item.RawMaxStackSize = valueInt, Value);
                        break;
                    case "Name":
                        Result = SetStringProperty((string valueString) => item.Name = valueString, Value);
                        break;
                    case "RequiredAppearance":
                        Result = StringToEnumConversion<Appearance>.SetEnum((Appearance valueEnum) => item.RequiredAppearance = valueEnum, Value);
                        break;
                    case "SkillReqs":
                        Result = ParseSkillRequirements(item, Value, SkillRequirementTable, parsedFile, parsedKey);
                        break;
                    case "StockDye":
                        Result = ParseStockDye(item, Value, parsedFile, parsedKey);
                        break;
                    case "TSysProfile":
                        Result = StringToEnumConversion<Profile>.SetEnum((Profile valueEnum) => item.TSysProfile = valueEnum, Value);
                        break;
                    case "Value":
                        Result = SetFloatProperty((float valueFloat) => item.RawValue = valueFloat, Value);
                        break;
                    case "NumUses":
                        Result = SetIntProperty((int valueInt) => item.RawNumUses = valueInt, Value);
                        break;
                    case "DestroyWhenUsedUp":
                        Result = SetBoolProperty((bool valueBool) => item.SetDestroyWhenUsedUp(valueBool), Value);
                        break;
                    case "Behaviors":
                        Result = Inserter<PgItemBehavior>.AddKeylessArray(item.BehaviorList, Value);
                        break;
                    case "DynamicCraftingSummary":
                        Result = SetStringProperty((string valueString) => item.DynamicCraftingSummary = valueString, Value);
                        break;
                    case "IsSkillReqsDefaults":
                        Result = SetBoolProperty((bool valueBool) => item.SetIsSkillReqsDefaults(valueBool), Value);
                        break;
                    case "BestowTitle":
                        Result = Inserter<PgPlayerTitle>.SetItemByKey((PgPlayerTitle valuePlayerTitle) => item.BestowTitle_Key = valuePlayerTitle.Key, $"Title_{Value}");
                        break;
                    case "BestowLoreBook":
                        Result = Inserter<PgLoreBook>.SetItemByKey((PgLoreBook valueLoreBook) => item.BestowLoreBook_Key = valueLoreBook.Key, $"Book_{Value}");
                        break;
                    case "Lint_VendorNpc":
                        Result = StringToEnumConversion<WorkOrderSign>.SetEnum((WorkOrderSign valueEnum) => item.LintVendorNpc = valueEnum, Value);
                        break;
                    case "MountedAppearance":
                        Result = SetStringProperty((string valueString) => item.MountedAppearance = valueString, Value);
                        break;
                    case "AttuneOnPickup":
                        Result = SetBoolProperty((bool valueBool) => item.SetAttuneOnPickup(valueBool), Value);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Key '{Key}' not handled");
                        break;
                }

                if (!Result)
                    break;
            }

            if (Result)
            {
                item.KeywordTable = KeywordTable;
                item.SkillRequirementTable = SkillRequirementTable;
            }

            return Result;
        }

        private bool ParseBestowRecipeList(PgItem item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ObjectList))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            item.BestowRecipeList = new PgRecipeCollection();

            foreach (object Item in ObjectList)
            {
                if (!(Item is string RecipeInternalName))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                PgRecipe ParsedRecipe = null!;
                if (!Inserter<PgRecipe>.SetItemByInternalName((PgRecipe valueRecipe) => ParsedRecipe = valueRecipe, RecipeInternalName))
                    return false;

                item.BestowRecipeList.Add(ParsedRecipe.Key);
            }

            return true;
        }

        private bool ParseDroppedAppearance(PgItem item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            string AppearanceString;

            int index = ValueString.IndexOf('(');
            if (index > 0)
            {
                if (index >= ValueString.Length - 2 || !ValueString.EndsWith(")"))
                    return Program.ReportFailure(parsedFile, parsedKey, $"'{value}' is an invalid dropped appareance");

                AppearanceString = ValueString.Substring(0, index);

                string[] Details = ValueString.Substring(index + 1, ValueString.Length - index - 2).Split(';');
                if (!ParseDroppedAppearanceDetails(item, Details, parsedFile, parsedKey))
                    return false;
            }
            else
                AppearanceString = ValueString;

            return StringToEnumConversion<ItemDroppedAppearance>.SetEnum((ItemDroppedAppearance valueEnum) => item.DroppedAppearance = valueEnum, AppearanceString);
        }

        private bool ParseDroppedAppearanceDetails(PgItem item, string[] details, string parsedFile, string parsedKey)
        {
            bool Result = true;

            foreach (string Detail in details)
            {
                string[] Splitted = Detail.Split('=');
                if (Splitted.Length != 2)
                    return Program.ReportFailure(parsedFile, parsedKey, $"Invalid pair in dropped appaearance detail '{Detail}'");

                string DetailKey = Splitted[0].Trim();
                string DetailValue = Splitted[1].Trim();

                if (string.IsNullOrEmpty(DetailKey) || string.IsNullOrEmpty(DetailValue))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Empty key or value in dropped appaearance detail '{Detail}'");

                switch (DetailKey)
                {
                    case "Skin":
                        if (DetailValue.StartsWith("^"))
                            Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.ItemAppearanceSkin = valueEnum, DetailValue.Substring(1));
                        else
                            Result = Program.ReportFailure(parsedFile, parsedKey, $"Unknown key in dropped appaearance detail '{Detail}'");
                        break;
                    case "^Skin":
                        Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.ItemAppearanceSkin = valueEnum, DetailValue);
                        break;
                    case "^Cork":
                        Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.ItemAppearanceCork = valueEnum, DetailValue);
                        break;
                    case "^Food":
                        Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.ItemAppearanceFood = valueEnum, DetailValue);
                        break;
                    case "^Plate":
                        Result = StringToEnumConversion<AppearanceSkin>.SetEnum((AppearanceSkin valueEnum) => item.ItemAppearancePlate = valueEnum, DetailValue);
                        break;
                    case "Color":
                        Result = ParseDroppedAppearanceColor(item, DetailValue, parsedFile, parsedKey);
                        break;
                    case "Skin_Color":
                        Result = ParseDroppedAppearanceSkinColor(item, DetailValue, parsedFile, parsedKey);
                        break;
                    default:
                        Result = Program.ReportFailure(parsedFile, parsedKey, $"Unknown key in dropped appaearance detail '{Detail}'");
                        break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        private bool ParseDroppedAppearanceColor(PgItem item, string detailValue, string parsedFile, string parsedKey)
        {
            if (Tools.TryParseColor(detailValue, out uint ParsedColor))
            {
                item.RawItemAppearanceColor = ParsedColor;
                return true;
            }
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown color in dropped appaearance detail '{detailValue}'");
        }

        private bool ParseDroppedAppearanceSkinColor(PgItem item, string detailValue, string parsedFile, string parsedKey)
        {
            if (Tools.TryParseColor(detailValue, out uint ParsedColor))
            {
                item.RawItemAppearanceSkinColor = ParsedColor;
                return true;
            }
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Unknown color in dropped appaearance detail '{detailValue}'");
        }

        public static bool ParseEffectDescriptionList(PgItemEffectCollection effectDescriptionList, object value, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ObjectList))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            foreach (object Item in ObjectList)
            {
                if (!(Item is string EffectDescription))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (!ParseEffectDescription(effectDescriptionList, EffectDescription, parsedFile, parsedKey))
                    return false;
            }

            return true;
        }

        private static bool ParseEffectDescription(PgItemEffectCollection effectDescriptionList, string effectDescription, string parsedFile, string parsedKey)
        {
            PgItemEffect ItemEffect;
            if (effectDescription.StartsWith("{") && effectDescription.EndsWith("}"))
            {
                string EffectString = effectDescription.Substring(1, effectDescription.Length - 2);
                if (!ParseItemEffectAttribute(EffectString, parsedFile, parsedKey, out ItemEffect))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{EffectString}'");
            }
            else if (effectDescription.Contains("{") || effectDescription.Contains("}"))
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid attribute format '{effectDescription}'");
            else
                ItemEffect = new PgItemEffectSimple() { Description = effectDescription };

            ParsingContext.AddSuplementaryObject(ItemEffect);
            effectDescriptionList.Add(ItemEffect);
            return true;
        }

        private static bool ParseItemEffectAttribute(string effectString, string parsedFile, string parsedKey, out PgItemEffect itemEffect)
        {
            itemEffect = null!;

            string[] Split = effectString.Split('{');
            if (Split.Length != 2)
                return false;

            string AttributeName = Split[0];
            string AttributeEffect = Split[1];

            if (!AttributeName.EndsWith("}"))
                return false;

            AttributeName = AttributeName.Substring(0, AttributeName.Length - 1);
            if (AttributeName.Contains("{") || AttributeName.Contains("}"))
                return false;

            if (AttributeName.Length == 0 || AttributeEffect.Length == 0)
                return false;

            PgAttribute ParsedAttribute = null!;
            if (!Inserter<PgAttribute>.SetItemByKey((PgAttribute valueAttribute) => ParsedAttribute = valueAttribute, AttributeName))
                return false;

            if (!Tools.TryParseFloat(AttributeEffect, out float ParsedEffect, out FloatFormat ParsedEffectFormat))
                return false;

            if (ParsedEffectFormat != FloatFormat.Standard)
                return false;

            itemEffect = new PgItemEffectAttribute() { Attribute_Key = ParsedAttribute.Key, AttributeEffect = ParsedEffect, AttributeEffectFormat = ParsedEffectFormat, Label = ParsedAttribute.Label };
            return true;
        }

        private bool ParseDyeColor(PgItem item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string ValueString))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            if (Tools.TryParseColor(ValueString, out uint NewColor))
            {
                item.RawDyeColor = NewColor;
                return true;
            }
            else
                return Program.ReportFailure(parsedFile, parsedKey, $"Invalid dye color format '{ValueString}'");
        }

        private bool ParseKeywordList(PgItem item, object value, Dictionary<ItemKeyword, List<float>> keywordTable, List<string> keywordValueList, string parsedFile, string parsedKey)
        {
            if (!(value is List<object> ObjectList))
                return Program.ReportFailure($"Value '{value}' was expected to be a list");

            foreach (object Item in ObjectList)
            {
                if (!(Item is string KeywordString))
                    return Program.ReportFailure($"Value '{Item}' was expected to be a string");

                if (!ParseKeyword(item, KeywordString, keywordTable, keywordValueList, parsedFile, parsedKey))
                    return Program.ReportFailure($"Invalid item keyword format '{KeywordString}'");
            }

            return true;
        }

        private bool ParseKeyword(PgItem item, string keywordString, Dictionary<ItemKeyword, List<float>> keywordTable, List<string> keywordValueList, string parsedFile, string parsedKey)
        {
            string KeyString;
            string ValueString;
            float Value;
            FloatFormat ValueFormat;

            string[] Pairs = keywordString.Split('=');
            if (Pairs.Length == 1)
            {
                KeyString = keywordString.Trim();
                Value = float.NaN;
            }
            else if (Pairs.Length == 2)
            {
                KeyString = Pairs[0].Trim();
                ValueString = Pairs[1].Trim();

                if (!Tools.TryParseFloat(ValueString, out Value, out ValueFormat))
                    return false;
            }
            else
                return false;

            if (!StringToEnumConversion<ItemKeyword>.TryParse(KeyString, out ItemKeyword Key))
                return false;

            List<float> ValueList;
            if (keywordTable.ContainsKey(Key))
                ValueList = keywordTable[Key];
            else
            {
                ValueList = new List<float>();
                keywordTable.Add(Key, ValueList);

                if (StringToEnumConversion<RecipeItemKey>.TryParse(KeyString, out RecipeItemKey ParsedKey, ErrorControl.IgnoreIfNotFound))
                    item.RecipeItemKeyList.Add(ParsedKey);
            }

            if (!float.IsNaN(Value))
                ValueList.Add(Value);

            int Index = -1;
            for (int i = 0; i < keywordValueList.Count; i++)
            {
                string[] Splitted = keywordValueList[i].Split(',');
                if (Splitted.Length > 0 && int.TryParse(Splitted[0], out int KeywordIndex) && KeywordIndex == (int)Key)
                {
                    Index = i;
                    break;
                }
            }

            if (Index < 0)
            {
                Index = keywordValueList.Count;
                keywordValueList.Add(string.Empty);
            }

            string Line = ((int)Key).ToString();
            foreach (float f in ValueList)
                Line += "," + Tools.SingleToString(f);

            keywordValueList[Index] = Line;

            return true;
        }

        private bool ParseSkillRequirements(PgItem item, object value, Dictionary<string, int> skillRequirementTable, string parsedFile, string parsedKey)
        {
            List<PgItemSkillLink> SkillRequirementList = new List<PgItemSkillLink>();
            if (!Inserter<PgItemSkillLink>.AddKeylessArray(SkillRequirementList, value))
                return false;

            foreach (PgItemSkillLink Item in SkillRequirementList)
                foreach (KeyValuePair<string, int> Entry in Item.SkillTable)
                {
                    string SkillKey = Entry.Key;

                    if (skillRequirementTable.ContainsKey(SkillKey))
                        return Program.ReportFailure($"Skill already added as requirement '{SkillKey}'");

                    skillRequirementTable.Add(SkillKey, Entry.Value);
                }

            return true;
        }

        private bool ParseStockDye(PgItem item, object value, string parsedFile, string parsedKey)
        {
            if (!(value is string RawStockDye))
                return Program.ReportFailure(parsedFile, parsedKey, $"Value '{value}' was expected to be a string");

            string[] Split = RawStockDye.Split(';');
            switch (Split.Length)
            {
                case 1:
                    return true;
                case 4:
                    return ParseStockDyeFourColors(item, Split, parsedFile, parsedKey);
                case 5:
                    return ParseStockDyeFourColorsAndGlow(item, Split, parsedFile, parsedKey);
                default:
                    return Program.ReportFailure(parsedFile, parsedKey, $"'{value}' is an invalid stock dye");
            }
        }

        private bool ParseStockDyeFourColors(PgItem item, string[] split, string parsedFile, string parsedKey)
        {
            if (split[0].Length != 0)
                return Program.ReportFailure(parsedFile, parsedKey, "First stock dye color must be empty");

            uint[] ParsedColors = new uint[3];
            string[] ParsedColorName = new string[3];

            int i;
            for (i = 1; i < split.Length; i++)
            {
                string ColorPrefix = $"Color{i}=";
                if (!split[i].StartsWith(ColorPrefix))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Stock dye color must start with {ColorPrefix}");

                string ColorString = split[i].Substring(ColorPrefix.Length);

                if (!Tools.TryParseColor(ColorString, out uint ParsedColor))
                    return Program.ReportFailure(parsedFile, parsedKey, $"{ColorString} is an invalid color");

                ParsedColors[i - 1] = ParsedColor;
                ParsedColorName[i - 1] = ColorString;
            }

            for (int n = 0; n < ParsedColors.Length; n++)
            {
                PgItemDye NewDye = new PgItemDye() { Color = ParsedColors[n], Name = ParsedColorName[n] };
                ParsingContext.AddSuplementaryObject(NewDye);

                item.StockDyeList.Add(NewDye);
            }

            return true;
        }

        private bool ParseStockDyeFourColorsAndGlow(PgItem item, string[] split, string parsedFile, string parsedKey)
        {
            if (split[0].Length != 0)
                return Program.ReportFailure(parsedFile, parsedKey, "First stock dye color must be empty");

            uint[] ParsedColors = new uint[3];
            string[] ParsedColorName = new string[3];

            int i;
            for (i = 1; i < split.Length && i < ParsedColors.Length + 1; i++)
            {
                string ColorPrefix = $"Color{i}=";
                if (!split[i].StartsWith(ColorPrefix))
                    return Program.ReportFailure(parsedFile, parsedKey, $"Stock dye color must start with {ColorPrefix}");

                string ColorString = split[i].Substring(ColorPrefix.Length);

                if (!Tools.TryParseColor(ColorString, out uint ParsedColor))
                    return Program.ReportFailure(parsedFile, parsedKey, $"{ColorString} is an invalid color");

                ParsedColors[i - 1] = ParsedColor;
                ParsedColorName[i - 1] = ColorString;
            }

            string GlowPattern = "GlowEnabled=";
            string GlowContent = split[4];

            if (!GlowContent.StartsWith(GlowPattern))
                return Program.ReportFailure(parsedFile, parsedKey, "Glow pattern expected");

            GlowContent = GlowContent.Substring(GlowPattern.Length);
            if (GlowContent != "y")
                return Program.ReportFailure(parsedFile, parsedKey, "Valid glow expected");

            item.SetHasGlow(true);

            for (int n = 0; n < ParsedColors.Length; n++)
            {
                PgItemDye NewDye = new PgItemDye() { Color = ParsedColors[n], Name = ParsedColorName[n] };
                ParsingContext.AddSuplementaryObject(NewDye);

                item.StockDyeList.Add(NewDye);
            }

            return true;
        }
    }
}
