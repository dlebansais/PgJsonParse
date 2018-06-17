using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTier : GenericJsonObject<PowerTier>, IPgPowerTier
    {
        #region Direct Properties
        public PowerEffectCollection EffectList { get; } = new PowerEffectCollection();
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
        }; } }

        private bool ParseEffectDescs(string RawEffectDesc, ParseErrorInfo ErrorInfo)
        {
            PowerEffect NewPowerEffect;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            foreach (PowerEffect Effect in EffectList)
            {
                PowerAttributeLink AsPowerAttributeLink;
                if ((AsPowerAttributeLink = Effect as PowerAttributeLink) != null)
                {
                    if (!AsPowerAttributeLink.IsParsed)
                    {
                        IsConnected = true;

                        bool IsAttributeParsed = false;
                        Attribute AttributeLink = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsPowerAttributeLink.AttributeName, AsPowerAttributeLink.AttributeLink, ref IsAttributeParsed, ref IsConnected, this);

                        bool IsSkillParsed = false;
                        Skill SkillLink = Skill.ConnectPowerSkill(ErrorInfo, SkillTable, AsPowerAttributeLink.AttributeSkill, AsPowerAttributeLink.SkillLink, ref IsSkillParsed, ref IsConnected, this);

                        AsPowerAttributeLink.SetLinks(AttributeLink, SkillLink);
                        foreach (int Id in AttributeLink.IconIdList)
                            ErrorInfo.AddIconId(Id);
                    }
                }
            }

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "PowerTier"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObjectCollection> StoredObjectListTable = new Dictionary<int, ISerializableJsonObjectCollection>();

            AddObjectList(EffectList, data, ref offset, BaseOffset, 0, StoredObjectListTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, null, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
