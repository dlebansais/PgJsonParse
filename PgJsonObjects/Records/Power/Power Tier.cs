using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PowerTier : GenericJsonObject<PowerTier>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "EffectDescs", ParseFieldEffectDescs },
        };
        #endregion

        #region Properties
        public List<PowerEffect> EffectList { get; set; }
        #endregion

        #region Client Interface
        private static void ParseFieldEffectDescs(PowerTier This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawEffectDescs;
            if ((RawEffectDescs = Value as ArrayList) != null)
                This.ParseEffectDescs(RawEffectDescs, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("PowerTier EffectDescs");
        }

        private void ParseEffectDescs(ArrayList RawEffectDescs, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawEffectDesc in RawEffectDescs)
            {
                string EffectDesc;
                if ((EffectDesc = RawEffectDesc as string) != null)
                {
                    PowerEffect NewPowerEffect;
                    if (PowerEffect.TryParse(EffectDesc, ErrorInfo, out NewPowerEffect))
                        EffectList.Add(NewPowerEffect);
                    else
                        ErrorInfo.AddInvalidString("PowerTier EffectDescs", EffectDesc);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("PowerTier EffectDescs");
            }
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.OpenArray("EffectDescs");

            foreach (PowerEffect Item in EffectList)
                Generator.AddString(null, Item.AsEffectString());

            Generator.CloseArray();

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "PowerTier"; } }

        protected override void InitializeFields()
        {
            EffectList = new List<PowerEffect>();
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            foreach (PowerEffect Effect in EffectList)
            {
                PowerAttributeLink AsPowerAttributeLink;
                if ((AsPowerAttributeLink = Effect as PowerAttributeLink) != null)
                {
                    if (!AsPowerAttributeLink.IsParsed)
                    {
                        IsConnected = true;

                        bool IsAttributeParsed = false;
                        Attribute AttributeLink = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsPowerAttributeLink.AttributeName, AsPowerAttributeLink.AttributeLink, ref IsAttributeParsed, ref IsConnected);

                        bool IsSkillParsed = false;
                        Skill SkillLink = Skill.ConnectPowerSkill(ErrorInfo, SkillTable, AsPowerAttributeLink.AttributeSkill, AsPowerAttributeLink.SkillLink, ref IsSkillParsed, ref IsConnected);

                        AsPowerAttributeLink.SetLinks(AttributeLink, SkillLink);
                    }
                }
            }

            return IsConnected;
        }
        #endregion
    }
}
