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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
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
