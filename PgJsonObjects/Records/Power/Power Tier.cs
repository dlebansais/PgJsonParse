using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTier : GenericJsonObject<PowerTier>
    {
        #region Direct Properties
        public List<PowerEffect> EffectList { get; } = new List<PowerEffect>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "EffectDescs", ParseFieldEffectDescs },
        };

        private static void ParseFieldEffectDescs(PowerTier This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "PowerTier EffectDescs", This.ParseEffectDescs);
        }

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
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.OpenArray("EffectDescs");

            foreach (PowerEffect Item in EffectList)
                Generator.AddString(null, Item.AsEffectString());

            Generator.CloseArray();

            Generator.CloseObject();
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
    }
}
