using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPower : MainPgObject<PgPower>, IPgPower
    {
        public PgPower(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 36;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgPower CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPower CreateNew(byte[] data, ref int offset)
        {
            return new PgPower(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Prefix { get { return GetString(4); } }
        public string Suffix { get { return GetString(8); } }
        public List<ItemSlot> SlotList { get { return GetEnumList(12, ref _SlotList); } } private List<ItemSlot> _SlotList;
        public IPgSkill Skill { get { return GetObject(16, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public bool IsUnavailable { get { return RawIsUnavailable.HasValue && RawIsUnavailable.Value; } }
        public bool? RawIsUnavailable { get { return GetBool(20, 0); } }
        public PowerSkill RawSkill { get { return GetEnum<PowerSkill>(22); } }
        public IPgPowerTierCollection TierEffectList { get { return GetObjectList(24, ref _TierEffectList, PgPowerTierCollection.CreateItem, () => new PgPowerTierCollection()); } } private IPgPowerTierCollection _TierEffectList;
        public int TierOffset { get { return RawTierOffset.HasValue ? RawTierOffset.Value : 0; } }
        public int? RawTierOffset { get { return GetInt(28); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(32, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Prefix", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Prefix } },
            { "Suffix", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Suffix  } },
            { "Tiers", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetTiers } },
            { "Slots", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                GetStringArray = () => StringToEnumConversion<ItemSlot>.ToStringList(SlotList) } },
            { "Skill", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkill, null, PowerSkill.Internal_None) } },
            { "IsUnavailable", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawIsUnavailable } },
        }; } }

        private IObjectContentGenerator GetTiers()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Tiers");

            IList<IPgPowerTier> AsList = TierEffectList;

            for (int i = 0; i < AsList.Count; i++)
            {
                IPgPowerTier Item = AsList[i];
                int Tier = TierOffset + i;

                string FieldKey = "id_" + Tier.ToString();
                Result.SetFieldValue(FieldKey, Item as IObjectContentGenerator);
            }

            return Result;
        }

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

        public override string SortingName { get { return ComposedName; } }

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
    }
}
