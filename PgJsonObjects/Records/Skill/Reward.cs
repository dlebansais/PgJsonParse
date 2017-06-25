using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Reward : GenericJsonObject<Reward>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Ability", ParseFieldAbility },
            { "BonusToSkill", ParseFieldBonusToSkill },
            { "Recipe", ParseFieldRecipe },
            { "Notes", ParseFieldNotes },
        };
        #endregion

        #region Properties
        public int RewardLevel { get; private set; }
        public Ability Ability { get; private set; }
        private string RawAbility;
        private bool IsRawAbilityParsed;
        public string Notes { get; private set; }
        public Recipe Recipe { get; private set; }
        private string RawRecipe;
        private bool IsRawRecipeParsed;
        public PowerSkill BonusSkill { get; private set; }
        public Skill ConnectedBonusSkill { get; private set; }
        private bool IsBonusSkillParsed;

        protected override string SortingName { get { return null; } }
        public Skill ParentSkill { get; private set; }

        private static string AddRewardString(string Result, string Reward)
        {
            return (Result.Length > 0) ? (Result + ", " + Reward) : Reward;
        }

        public string CombinedReward
        {
            get
            {
                string Result = "";

                if (Ability != null)
                    Result = AddRewardString(Result, "Ability: " + Ability.Name);

                if (Notes != null)
                    Result = AddRewardString(Result, Notes);

                if (Recipe != null)
                    Result = AddRewardString(Result, "Recipe: " + Recipe.Name);

                if (BonusSkill != PowerSkill.Internal_None)
                    Result = AddRewardString(Result, "+1 " + TextMaps.PowerSkillTextMap[BonusSkill]);

                return Result;
            }
        }
        #endregion

        #region Client Interface
        private static void ParseFieldAbility(Reward This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAbility;
            if ((RawAbility = Value as string) != null)
                This.ParseAbility(RawAbility, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Reward Ability");
        }

        private void ParseAbility(string RawAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawAbility = RawAbility;
        }

        private static void ParseFieldBonusToSkill(Reward This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawBonusToSkill;
            if ((RawBonusToSkill = Value as string) != null)
                This.ParseBonusToSkill(RawBonusToSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Reward BonusToSkill");
        }

        private void ParseBonusToSkill(string RawBonusToSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedBonusToSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawBonusToSkill, out ParsedBonusToSkill, ErrorInfo);
            BonusSkill = ParsedBonusToSkill;
        }

        private static void ParseFieldRecipe(Reward This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRecipe;
            if ((RawRecipe = Value as string) != null)
                This.ParseRecipe(RawRecipe, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Reward Recipe");
        }

        private void ParseRecipe(string RawRecipe, ParseErrorInfo ErrorInfo)
        {
            this.RawRecipe = RawRecipe;
        }

        private static void ParseFieldNotes(Reward This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNotes;
            if ((RawNotes = Value as string) != null)
                This.ParseNotes(RawNotes, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Reward Notes");
        }

        private void ParseNotes(string RawNotes, ParseErrorInfo ErrorInfo)
        {
            Notes = RawNotes;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Ability", RawAbility);
            Generator.AddString("Notes", Notes);
            Generator.AddString("Recipe", RawRecipe);
            Generator.AddString("BonusToSkill", StringToEnumConversion<PowerSkill>.ToString(BonusSkill, null, PowerSkill.Internal_None));

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Ability != null)
                    Result += Ability.TextContent + JsonGenerator.FieldSeparator;
                Result += Notes + JsonGenerator.FieldSeparator;
                if (Recipe != null)
                    Result += Recipe.TextContent + JsonGenerator.FieldSeparator;

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Reward"; } }

        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw)
        {
            base.InitializeKey(EntryRaw);

            int ParsedRewardLevel;
            if (int.TryParse(Key, out ParsedRewardLevel))
                RewardLevel = ParsedRewardLevel;
        }

        protected override void InitializeFields()
        {
            Ability = null;
            IsRawAbilityParsed = false;
            Recipe = null;
            IsRawRecipeParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            ParentSkill = Parent as Skill;

            Ability = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawAbility, Ability, ref IsRawAbilityParsed, ref IsConnected, this);
            Recipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipe, Recipe, ref IsRawRecipeParsed, ref IsConnected, this);

            if (BonusSkill != PowerSkill.Internal_None && BonusSkill != PowerSkill.AnySkill && BonusSkill != PowerSkill.Unknown)
                ConnectedBonusSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, BonusSkill, ConnectedBonusSkill, ref IsBonusSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
