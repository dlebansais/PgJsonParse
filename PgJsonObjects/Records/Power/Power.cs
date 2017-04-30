using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Power : GenericJsonObject<Power>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Prefix", ParseFieldPrefix },
            { "Suffix", ParseFieldSuffix },
            { "Tiers", ParseFieldTiers },
            { "Slots", ParseFieldSlots },
            { "Skill", ParseFieldSkill },
            { "IsUnavailable", ParseFieldIsUnavailable },
        };
        #endregion

        #region Properties
        public string Prefix { get; private set; }
        public string Suffix { get; private set; }
        public Dictionary<int, PowerTier> TierEffectTable { get; private set; }
        public List<ItemSlot> SlotList { get; private set; }
        public PowerSkill Skill { get; private set; }
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        private bool? RawIsUnavailable;
        #endregion

        #region Client Interface
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

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Power"; } }

        protected override void InitializeFields()
        {
            TierEffectTable = new Dictionary<int, PowerTier>();
            SlotList = new List<ItemSlot>();
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            foreach (KeyValuePair<int, PowerTier> Entry in TierEffectTable)
                IsConnected |= Entry.Value.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            return IsConnected;
        }
        #endregion
    }
}
