﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPower : MainPgObject<PgPower>, IPgPower, IBackLinkable
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

        public override void Init()
        {
            AddLinkBack(Skill);
            AddLinkBackCollection(TierEffectList, GetTierEffectkBacks);
        }

        public IList<IBackLinkable> GetTierEffectkBacks(IPgPowerTier value)
        {
            IPgPowerEffectCollection EffectList = value.EffectList;

            List<IBackLinkable> Result = new List<IBackLinkable>();

            if (EffectList != null)
                foreach (IPgPowerEffect Item in EffectList)
                {
                    IList<IBackLinkable> ItemResult = Item.GetLinkBack();
                    if (ItemResult != null)
                        Result.AddRange(ItemResult);
                }

            return Result;
        }

        public void InitTierList(Dictionary<string, IJsonKey> attributeTable)
        {
            PgPower.FillcombinedTierList(CombinedTierList, attributeTable, TierEffectList, TierOffset);
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
        public List<string> CombinedTierList { get; } = new List<string>();

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

        public static void FillcombinedTierList(List<string> combinedTierList, Dictionary<string, IJsonKey> attributeTable, IList<IPgPowerTier> tierEffectList, int TierOffset)
        {
            for (int i = 0; i < tierEffectList.Count; i++)
            {
                IPgPowerTier Item = tierEffectList[i];
                int Tier = TierOffset + i;

                foreach (IPgPowerEffect Effect in Item.EffectList)
                {
                    IPgPowerAttributeLink AsPowerAttributeLink;
                    IPgPowerSimpleEffect AsPowerSimpleEffect;

                    if ((AsPowerAttributeLink = Effect as IPgPowerAttributeLink) != null)
                    {
                        if (attributeTable.ContainsKey(AsPowerAttributeLink.AttributeName))
                        {
                            IPgAttribute PowerAttribute = attributeTable[AsPowerAttributeLink.AttributeName] as IPgAttribute;

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

                            combinedTierList.Add(PrepareTier(Tier, Name));
                        }
                    }

                    else if ((AsPowerSimpleEffect = Effect as IPgPowerSimpleEffect) != null)
                    {
                        combinedTierList.Add(PrepareTier(Tier, AsPowerSimpleEffect.Description));
                    }
                }
            }

            combinedTierList.Sort(SortByLevel);
        }

        private static string PrepareTier(int Level, string s)
        {
            return "Tier " + Level + ": " + s;
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
    }
}
