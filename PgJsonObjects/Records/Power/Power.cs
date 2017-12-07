using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Power : GenericJsonObject<Power>
    {
        #region Direct Properties
        public string Prefix { get; private set; }
        public string Suffix { get; private set; }
        public Dictionary<int, PowerTier> TierEffectTable { get; } = new Dictionary<int, PowerTier>();
        public List<ItemSlot> SlotList { get; } = new List<ItemSlot>();
        public PowerSkill Skill { get; private set; }
        public Skill ConnectedSkill { get; private set; }
        private bool IsSkillParsed;
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ComposedName; } }
        public List<string> CombinedTierList { get; } = new List<string>();

        public string ComposedName
        {
            get
            {
                string Result;

                if (Prefix != null || Suffix != null)
                {
                    Result = "";

                    if (Prefix != null)
                        Result += Prefix;

                    if (Suffix != null)
                    {
                        if (Result.Length > 0)
                            Result += " ";

                        Result += Suffix;
                    }
                }
                else
                    Result = "(no name)";

                return Result;
            }
        }

        public string SearchResultIconFileName
        {
            get
            {
                int IconId = PgJsonObjects.Skill.BestIconIdForSkill(Skill);

                if (IconId == 0)
                    return null;

                return "icon_" + IconId;
            }
        }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];

            foreach (KeyValuePair<int, PowerTier> Entry in TierEffectTable)
            {
                int Tier = Entry.Key;

                foreach (PowerEffect Effect in Entry.Value.EffectList)
                {
                    PowerAttributeLink AsPowerAttributeLink;
                    PowerSimpleEffect AsPowerSimpleEffect;

                    if ((AsPowerAttributeLink = Effect as PowerAttributeLink) != null)
                    {
                        if (AttributeTable.ContainsKey(AsPowerAttributeLink.AttributeName))
                        {
                            Attribute PowerAttribute = AttributeTable[AsPowerAttributeLink.AttributeName] as Attribute;

                            bool IsPercent = PowerAttribute.IsLabelWithPercent;
                            string Label = PowerAttribute.LabelRippedOfPercent;
                            string Name = Label;

                            if (AsPowerAttributeLink.AttributeEffect != 0)
                            {
                                float PowerValue = AsPowerAttributeLink.AttributeEffect;

                                if (IsPercent)
                                {
                                    string PowerValueString = Tools.FloatToString(PowerValue * 100, AsPowerAttributeLink.AttributeEffectFormat);

                                    if (PowerValue > 0)
                                        PowerValueString = "+" + PowerValueString;

                                    Name += " " + PowerValueString + "%";
                                }
                                else
                                {
                                    string PowerValueString = Tools.FloatToString(PowerValue, AsPowerAttributeLink.AttributeEffectFormat);

                                    if (PowerValue > 0)
                                        PowerValueString = "+" + PowerValueString;

                                    Name += " " + PowerValueString;
                                }
                            }

                            CombinedTierList.Add(PrepareTier(Entry.Key, Name));
                        }
                    }

                    else if ((AsPowerSimpleEffect = Effect as PowerSimpleEffect) != null)
                    {
                        CombinedTierList.Add(PrepareTier(Entry.Key, AsPowerSimpleEffect.Description));
                    }
                }
            }

            CombinedTierList.Sort(SortByLevel);
        }

        private static int SortByLevel(string s1, string s2)
        {
            int i1 = s1.IndexOf(':');
            int i2 = s2.IndexOf(':');

            int l1 = 0;
            int.TryParse(s1.Substring(5, i1 - 5), out l1);
            int l2 = 0;
            int.TryParse(s2.Substring(5, i2 - 5), out l2);

            if (l1 > l2)
                return 1;
            else if (l1 < l2)
                return -1;
            else
                return 0;
        }

        private static string PrepareTier(int Level, string s)
        {
            return "Tier " + Level + ": " + s;
        }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Prefix", ParseFieldPrefix },
            { "Suffix", ParseFieldSuffix },
            { "Tiers", ParseFieldTiers },
            { "Slots", ParseFieldSlots },
            { "Skill", ParseFieldSkill },
            { "IsUnavailable", ParseFieldIsUnavailable },
        };

        private static void ParseFieldPrefix(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawPrefix;
            if ((RawPrefix = Value as string) != null)
                This.ParsePrefix(RawPrefix, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power Prefix");
        }

        private void ParsePrefix(string RawPrefix, ParseErrorInfo ErrorInfo)
        {
            Prefix = RawPrefix;
        }

        private static void ParseFieldSuffix(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSuffix;
            if ((RawSuffix = Value as string) != null)
                This.ParseSuffix(RawSuffix, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power Suffix");
        }

        private void ParseSuffix(string RawSuffix, ParseErrorInfo ErrorInfo)
        {
            Suffix = RawSuffix;
        }

        private static void ParseFieldTiers(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawTiers;
            if ((RawTiers = Value as Dictionary<string, object>) != null)
                This.ParseTiers(RawTiers, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power Tiers");
        }

        private void ParseTiers(Dictionary<string, object> RawTiers, ParseErrorInfo ErrorInfo)
        {
            int TierOffset = int.MaxValue;
            foreach (KeyValuePair<string, object> Entry in RawTiers)
            {
                if (!Entry.Key.Contains("id_"))
                    break;

                string s = Entry.Key.Substring(3);

                int Index;
                if (int.TryParse(s, out Index))
                    if (TierOffset > Index)
                        TierOffset = Index;
            }

            int i;
            for (i = 0; i < RawTiers.Count; i++)
            {
                string Key = "id_" + (i + TierOffset).ToString();
                if (!RawTiers.ContainsKey(Key))
                    break;

                Dictionary<string, object> RawValue;
                if ((RawValue = RawTiers[Key] as Dictionary<string, object>) != null)
                {
                    PowerTier Subitem;
                    JsonObjectParser<PowerTier>.InitAsSubitem(Key, RawValue, out Subitem, ErrorInfo);
                    TierEffectTable.Add(i, Subitem);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Power Tiers");
            }

            if (i < RawTiers.Count)
                ErrorInfo.AddInvalidObjectFormat("Power Tiers");
        }

        private static void ParseFieldSlots(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawSlots;
            if ((RawSlots = Value as ArrayList) != null)
                This.ParseSlots(RawSlots, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power Slots");
        }

        private void ParseSlots(ArrayList RawSlots, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<ItemSlot>.ParseList(RawSlots, SlotList, ErrorInfo);
        }

        private static void ParseFieldSkill(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ConvertedPowerSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ConvertedPowerSkill, ErrorInfo);
            Skill = ConvertedPowerSkill;
        }

        private static void ParseFieldIsUnavailable(Power This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseIsUnavailable((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Power IsUnavailable");
        }

        private void ParseIsUnavailable(bool RawIsUnavailable, ParseErrorInfo ErrorInfo)
        {
            this.RawIsUnavailable = RawIsUnavailable;
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Prefix", Prefix);
            Generator.AddString("Suffix", Suffix);

            Generator.OpenObject("Tiers");

            foreach (KeyValuePair<int, PowerTier> Entry in TierEffectTable)
                Entry.Value.GenerateObjectContent(Generator);

            Generator.CloseObject();

            Generator.AddBoolean("IsUnavailable", RawIsUnavailable);
            StringToEnumConversion<ItemSlot>.ListToString(Generator, "Slots", SlotList);
            Generator.AddString("Skill", StringToEnumConversion<PowerSkill>.ToString(Skill, null, PowerSkill.Internal_None));

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, ComposedName);
                foreach (ItemSlot Slot in SlotList)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemSlotTextMap[Slot]);
                if (Skill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[Skill]);
                if (RawIsUnavailable.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Unavailable");
                foreach (string Item in CombinedTierList)
                    AddWithFieldSeparator(ref Result, Item);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            foreach (KeyValuePair<int, PowerTier> Entry in TierEffectTable)
                IsConnected |= Entry.Value.Connect(ErrorInfo, this, AllTables);

            if (Skill != PowerSkill.Internal_None && Skill != PowerSkill.AnySkill && Skill != PowerSkill.Unknown)
                ConnectedSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, Skill, ConnectedSkill, ref IsSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion

        #region Crunching
        public bool IsValidForSlot(PowerSkill Skill, ItemSlot Slot)
        {
            if (this.Skill != Skill)
                return false;

            if (IsUnavailable)
                return false;

            if (TierEffectTable.Count == 0)
                return false;

            if (!SlotList.Contains(Slot))
                return false;

            return true;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Power"; } }
        #endregion
    }
}
