using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Reward : GenericJsonObject<Reward>
    {
        #region Direct Properties
        public int RewardLevel { get; private set; }
        public List<Race> RaceRestrictionList { get; private set; } = new List<Race>();
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
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        public Skill ParentSkill { get; private set; }
        #endregion

        #region Parsing
        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            string[] SplitKey = Key.Split('_');

            if (SplitKey.Length >= 1)
            {
                int ParsedRewardLevel;
                if (int.TryParse(SplitKey[0], out ParsedRewardLevel))
                    RewardLevel = ParsedRewardLevel;
                else
                    ErrorInfo.AddInvalidObjectFormat("Reward Level");

                for (int i = 1; i < SplitKey.Length; i++)
                {
                    Race ParsedRace;
                    if (StringToEnumConversion<Race>.TryParse(SplitKey[i], out ParsedRace, ErrorInfo))
                        RaceRestrictionList.Add(ParsedRace);
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Reward");
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Ability", ParseFieldAbility },
            { "BonusToSkill", ParseFieldBonusToSkill },
            { "Recipe", ParseFieldRecipe },
            { "Notes", ParseFieldNotes },
        };

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
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Ability", RawAbility);
            Generator.AddString("Notes", Notes);
            Generator.AddString("Recipe", RawRecipe);
            Generator.AddString("BonusToSkill", StringToEnumConversion<PowerSkill>.ToString(BonusSkill, null, PowerSkill.Internal_None));

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Ability != null)
                    AddWithFieldSeparator(ref Result, Ability.Name);
                AddWithFieldSeparator(ref Result, Notes);
                if (Recipe != null)
                    AddWithFieldSeparator(ref Result, Recipe.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
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

        #region Debugging
        protected override string FieldTableName { get { return "Reward"; } }
        #endregion
    }
}
