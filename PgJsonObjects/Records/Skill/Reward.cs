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
        public Ability Ability { get; set; }
        private string RawAbility;
        private bool IsRawAbilityParsed;
        public string Notes { get; set; }
        public Recipe Recipe { get; set; }
        private string RawRecipe;
        private bool IsRawRecipeParsed;
        public PowerSkill BonusToSkill { get; set; }
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
            BonusToSkill = ParsedBonusToSkill;
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
            Generator.AddString("BonusToSkill", StringToEnumConversion<PowerSkill>.ToString(BonusToSkill, null, PowerSkill.Internal_None));

            Generator.CloseObject();
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Reward"; } }

        protected override void InitializeFields()
        {
            Ability = null;
            IsRawAbilityParsed = false;
            Recipe = null;
            IsRawRecipeParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            Ability = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawAbility, Ability, ref IsRawAbilityParsed, ref IsConnected);
            Recipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipe, Recipe, ref IsRawRecipeParsed, ref IsConnected);

            return IsConnected;
        }
        #endregion
    }
}
