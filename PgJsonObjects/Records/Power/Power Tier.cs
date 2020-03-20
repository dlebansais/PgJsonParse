using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTier : GenericJsonObject<PowerTier>, IPgPowerTier
    {
        #region Direct Properties
        public IPgPowerEffectCollection EffectList { get; } = new PowerEffectCollection();
        public int SkillLevelPrereq { get { return RawSkillLevelPrereq.HasValue ? RawSkillLevelPrereq.Value : 0; } }
        public int? RawSkillLevelPrereq { get; private set; }
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "EffectDescs", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseEffectDescs,
                GetStringArray = GetEffectDescs } },
            { "SkillLevelPrereq", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawSkillLevelPrereq = value,
                GetInteger = () => RawSkillLevelPrereq } },
        }; } }

        private bool ParseEffectDescs(string RawEffectDesc, ParseErrorInfo ErrorInfo)
        {
            IPgPowerEffect NewPowerEffect;
            if (PowerEffect.TryParse(RawEffectDesc, ErrorInfo, out NewPowerEffect))
            {
                EffectList.Add(NewPowerEffect);
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidString("PowerTier EffectDescs", RawEffectDesc);
                return false;
            }
        }

        private List<string> GetEffectDescs()
        {
            List<string> Result = new List<string>();

            foreach (PowerEffect Effect in EffectList)
                Result.Add(Effect.AsEffectString());

            return Result;
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];

            foreach (PowerEffect Effect in EffectList)
            {
                PowerAttributeLink AsPowerAttributeLink;
                if ((AsPowerAttributeLink = Effect as PowerAttributeLink) != null)
                {
                    if (!AsPowerAttributeLink.IsParsed)
                    {
                        IsConnected = true;

                        bool IsAttributeParsed = false;
                        IPgAttribute AttributeLink = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsPowerAttributeLink.AttributeName, AsPowerAttributeLink.AttributeLink, ref IsAttributeParsed, ref IsConnected);

                        bool IsSkillParsed = false;
                        IPgSkill SkillLink = Skill.ConnectPowerSkill(ErrorInfo, SkillTable, AsPowerAttributeLink.AttributeSkill, AsPowerAttributeLink.SkillLink, ref IsSkillParsed, ref IsConnected, Parent);

                        if (AttributeLink != null)
                        {
                            AsPowerAttributeLink.SetLinks(AttributeLink, SkillLink);
                            foreach (int Id in AttributeLink.IconIdList)
                                ErrorInfo.AddIconId(Id);
                        }
                    }
                }
            }

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "PowerTier"; } }

        public override string ToString()
        {
            IList<IPgPowerEffect> Effects = EffectList;

            if (Effects.Count == 1)
                return Effects[Effects.Count - 1].ToString();
            else if (Effects.Count > 1)
                return $"{Effects.Count} Effects";
            else
                return base.ToString();
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObjectList(EffectList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddInt(RawSkillLevelPrereq, data, ref offset, BaseOffset, 8);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
