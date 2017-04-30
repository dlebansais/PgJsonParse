using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfo : GenericJsonObject<ServerInfo>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "RewardSkill", ParseFieldRewardSkill },
            { "RewardSkillXpFirstTime", ParseFieldRewardSkillXpFirstTime },
            { "RewardSkillXp", ParseFieldRewardSkillXp },
            { "RequiredHotspot", ParseFieldRequiredHotspot },
            { "RewardAllowBonusXp", ParseFieldRewardAllowBonusXp },
        };
        #endregion

        #region Properties
        public PowerSkill RewardSkill { get; private set; }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        private int? RawRewardSkillXpFirstTime;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        private int? RawRewardSkillXp;
        public RecipeRequiredHotspot RequiredHotspot { get; private set; }
        public bool RewardAllowBonusXp { get { return RawRewardAllowBonusXp.HasValue && RawRewardAllowBonusXp.Value; } }
        private bool? RawRewardAllowBonusXp;
        #endregion

        #region Client Interface
        private static void ParseFieldRewardSkill(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRewardSkill;
            if ((RawRewardSkill = Value as string) != null)
                This.ParseRewardSkill(RawRewardSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RewardSkill");
        }

        private void ParseRewardSkill(string RawRewardSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ConvertedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawRewardSkill, out ConvertedSkill, ErrorInfo);
            RewardSkill = ConvertedSkill;
        }

        private static void ParseFieldRewardSkillXpFirstTime(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardSkillXpFirstTime((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RewardSkillXpFirstTime");
        }

        private void ParseRewardSkillXpFirstTime(int RawRewardSkillXpFirstTime, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardSkillXpFirstTime = RawRewardSkillXpFirstTime;
        }

        private static void ParseFieldRewardSkillXp(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseRewardSkillXp((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RewardSkillXp");
        }

        private void ParseRewardSkillXp(int RawRewardSkillXp, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardSkillXp = RawRewardSkillXp;
        }

        private static void ParseFieldRequiredHotspot(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRequiredHotspot;
            if ((RawRequiredHotspot = Value as string) != null)
                This.ParseRequiredHotspot(RawRequiredHotspot, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RequiredHotspot");
        }

        private void ParseRequiredHotspot(string RawRequiredHotspot, ParseErrorInfo ErrorInfo)
        {
            RecipeRequiredHotspot ConvertedRequiredHotspot;
            StringToEnumConversion<RecipeRequiredHotspot>.TryParse(RawRequiredHotspot, out ConvertedRequiredHotspot, ErrorInfo);
            RequiredHotspot = ConvertedRequiredHotspot;
        }

        private static void ParseFieldRewardAllowBonusXp(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseRewardAllowBonusXp((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RewardAllowBonusXp");
        }

        private void ParseRewardAllowBonusXp(bool RawRewardAllowBonusXp, ParseErrorInfo ErrorInfo)
        {
            this.RawRewardAllowBonusXp = RawRewardAllowBonusXp;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject("ServerInfo");

            Generator.AddString("RewardSkill", StringToEnumConversion<PowerSkill>.ToString(RewardSkill, null, PowerSkill.Internal_None));
            Generator.AddInteger("RewardSkillXpFirstTime", RawRewardSkillXpFirstTime);
            Generator.AddInteger("RewardSkillXp", RawRewardSkillXp);
            Generator.AddBoolean("RewardAllowBonusXp", RawRewardAllowBonusXp);
            Generator.AddString("RequiredHotspot", StringToEnumConversion<RecipeRequiredHotspot>.ToString(RequiredHotspot, null, RecipeRequiredHotspot.Internal_None));

            Generator.CloseObject();
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "ServerInfo"; } }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            return false;
        }
        #endregion
    }
}
