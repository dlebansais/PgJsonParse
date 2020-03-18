using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Power : MainJsonObject<Power>, IPgPower
    {
        #region Direct Properties
        public string Prefix { get; private set; }
        public string Suffix { get; private set; }
        public List<ItemSlot> SlotList { get; } = new List<ItemSlot>();
        public IPgSkill Skill { get; private set; }
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get; private set; }
        public PowerSkill RawSkill { get; private set; }
        public IPgPowerTierCollection TierEffectList { get; } = new PowerTierCollection();
        public int TierOffset { get { return RawTierOffset.HasValue ? RawTierOffset.Value : 0; } }
        public int? RawTierOffset { get; private set; }

        private bool IsSkillParsed;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return ComposedName; } }
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
                int IconId = PgJsonObjects.Skill.BestIconIdForSkill(RawSkill);

                if (IconId == 0)
                    return null;

                return "icon_" + IconId;
            }
        }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IJsonKey>> AllTables, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];
            PgPower.FillcombinedTierList(CombinedTierList, AttributeTable, TierEffectList, TierOffset);
        }

        public void InitTierList(Dictionary<string, IJsonKey> attributeTable)
        {
        }

        private static int SortByLevel(string s1, string s2)
        {
            int i1 = s1.IndexOf(':');
            int i2 = s2.IndexOf(':');

            int l1 = 0;
            if (int.TryParse(s1.Substring(5, i1 - 5), out int Parsed1))
                l1 = Parsed1;
            int l2 = 0;
            if (int.TryParse(s2.Substring(5, i2 - 5), out int Parsed2))
                l2 = Parsed2;

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
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Prefix", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Prefix = value,
                GetString = () => Prefix } },
            { "Suffix", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Suffix = value,
                GetString = () => Suffix  } },
            { "Tiers", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseTiers,
                GetObject = GetTiers } },
            { "Slots", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<ItemSlot>.ParseList(value, SlotList, errorInfo),
                GetStringArray = () => StringToEnumConversion<ItemSlot>.ToStringList(SlotList) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "IsUnavailable", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawIsUnavailable = value,
                GetBool = () => RawIsUnavailable } },
        }; } }

        private void ParseTiers(JsonObject RawTiers, ParseErrorInfo ErrorInfo)
        {
            int TierOffset = int.MaxValue;

            foreach (KeyValuePair<string, IJsonValue> Entry in RawTiers)
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

            if (TierOffset != int.MaxValue)
            {
                for (i = 0; i < RawTiers.Count; i++)
                {
                    string Key = "id_" + (i + TierOffset).ToString();
                    if (!RawTiers.Has(Key))
                        break;

                    JsonObject RawValue;
                    if ((RawValue = RawTiers[Key] as JsonObject) != null)
                    {
                        PowerTier Subitem;
                        JsonObjectParser<PowerTier>.InitAsSubitem(Key, RawValue, out Subitem, ErrorInfo);
                        TierEffectList.Add(Subitem);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Power Tiers");
                }
            }
            else
                i = 0;

            if (i >= RawTiers.Count)
                RawTierOffset = TierOffset;
            else
                ErrorInfo.AddInvalidObjectFormat("Power Tiers");
        }

        private IObjectContentGenerator GetTiers()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Tiers");

            IList<IPgPowerTier> AsTierEffectList = TierEffectList;

            for (int i = 0; i < AsTierEffectList.Count; i++)
            {
                IPgPowerTier Item = AsTierEffectList[i];
                int Tier = TierOffset + i;

                string FieldKey = "id_" + Tier.ToString();
                Result.SetFieldValue(FieldKey, (PowerTier)Item);
            }

            return Result;
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
                if (RawSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[RawSkill]);
                if (RawIsUnavailable.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Unavailable");
                foreach (string Item in CombinedTierList)
                    AddWithFieldSeparator(ref Result, Item);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            IList<IPgPowerTier> AsTierEffectList = TierEffectList;

            for (int i = 0; i < AsTierEffectList.Count; i++)
            {
                PowerTier Item = AsTierEffectList[i] as PowerTier;
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);
            }

            if (RawSkill != PowerSkill.Internal_None && RawSkill != PowerSkill.AnySkill && RawSkill != PowerSkill.Unknown)
                Skill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkill, Skill, ref IsSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion

        #region Crunching
        public static bool IsValidForSlot(IPgPower power, PowerSkill RawSkill, PowerSkill RawParentSkill, ItemSlot Slot)
        {
            if (power.RawSkill != RawSkill && power.RawSkill != RawParentSkill)
                return false;

            if (power.IsUnavailable)
                return false;

            if ((power.TierEffectList as IList<IPgPowerTier>).Count == 0)
                return false;

            if (!power.SlotList.Contains(Slot))
                return false;

            return true;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Power"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddString(Prefix, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(Suffix, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddEnumList(SlotList, data, ref offset, BaseOffset, 12, StoredEnumListTable);
            AddObject(Skill as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddBool(RawIsUnavailable, data, ref offset, ref BitOffset, BaseOffset, 20, 0);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(RawSkill, data, ref offset, BaseOffset, 22);
            AddObjectList(TierEffectList, data, ref offset, BaseOffset, 24, StoredObjectListTable);
            AddInt(RawTierOffset, data, ref offset, BaseOffset, 28);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 32, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 36, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
