using System.Collections.Generic;

namespace PgJsonObjects
{
    public class DirectedGoal : GenericJsonObject<DirectedGoal>
    {
        #region Constants
        public const int SearchResultIconId = 2118;

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Label", ParseFieldLabel },
            { "Zone", ParseFieldZone },
            { "Hint", ParseFieldHint },
            { "NeededSkill", ParseFieldNeededSkill },
            { "NeededSkillLevel", ParseFieldNeededSkillLevel },
            { "NeededAbility", ParseFieldNeededAbility },
            { "NeededInteractionFlag", ParseFieldNeededInteractionFlag },
            { "NeededRecipe", ParseFieldNeededRecipe },
            { "NeededRecipeCompletions", ParseFieldNeededRecipeCompletions },
            { "NeededNotoriety", ParseFieldNeededNotoriety },
        };
        #endregion

        #region Properties
        public string Label { get; private set; }
        public string Zone { get; private set; }
        public string Hint { get; private set; }
        public PowerSkill NeededSkill { get; private set; }
        public Skill ConnectedNeededSkill { get; private set; }
        private bool IsNeededSkillParsed;
        public int NeededSkillLevel { get { return RawNeededSkillLevel.HasValue ? RawNeededSkillLevel.Value : 0; } }
        private int? RawNeededSkillLevel;
        public Ability NeededAbility { get; private set; }
        public string RawNeededAbility { get; private set; }
        private bool IsRawNeededAbilityParsed;
        public string NeededInteractionFlag { get; private set; }
        public Recipe NeededRecipe { get; private set; }
        private string RawNeededRecipe;
        private bool IsRawNeededRecipeParsed;
        public int NeededRecipeCompletions { get { return RawNeededRecipeCompletions.HasValue ? RawNeededRecipeCompletions.Value : 0; } }
        private int? RawNeededRecipeCompletions;
        public string NeededNotoriety { get; private set; }

        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }

        public string CombinedNeededSkill
        {
            get
            {
                if (ConnectedNeededSkill == null)
                    return TextMaps.PowerSkillTextMap[NeededSkill];
                else
                    return ConnectedNeededSkill.Name;
            }
        }

        public string CombinedNeededAbility
        {
            get
            {
                if (NeededAbility == null)
                    return "None";
                else
                    return NeededAbility.Name;
            }
        }

        public bool HasRecipeCompletions { get { return RawNeededRecipeCompletions != null && RawNeededRecipeCompletions.Value > 0; } }

        public string CombinedNeededRecipe
        {
            get
            {
                if (NeededRecipe == null)
                    return "None";

                string Completions = CombinedRecipeCompletions;

                if (Completions.Length > 0)
                    return NeededRecipe.Name + " " + Completions;
                else
                    return NeededRecipe.Name;
            }
        }

        public string CombinedRecipeCompletions
        {
            get
            {
                if (RawNeededRecipeCompletions != null && RawNeededRecipeCompletions.Value > 0)
                    return "(x" + RawNeededRecipeCompletions.Value.ToString() + ")";
                else
                    return "";
            }
        }
        #endregion

        #region Client Interface
        private static void ParseFieldLabel(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawLabel;
            if ((RawLabel = Value as string) != null)
                This.ParseLabel(RawLabel, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal Label");
        }

        private void ParseLabel(string RawLabel, ParseErrorInfo ErrorInfo)
        {
            Label = RawLabel;
            ErrorInfo.AddIconId(SearchResultIconId);
        }

        private static void ParseFieldZone(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawZone;
            if ((RawZone = Value as string) != null)
                This.ParseZone(RawZone, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal Zone");
        }

        private void ParseZone(string RawZone, ParseErrorInfo ErrorInfo)
        {
            Zone = RawZone;
        }

        private static void ParseFieldHint(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawHint;
            if ((RawHint = Value as string) != null)
                This.ParseHint(RawHint, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal Hint");
        }

        private void ParseHint(string RawHint, ParseErrorInfo ErrorInfo)
        {
            Hint = RawHint;
        }

        private static void ParseFieldNeededSkill(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNeededSkill;
            if ((RawNeededSkill = Value as string) != null)
                This.ParseNeededSkill(RawNeededSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededSkill");
        }

        private void ParseNeededSkill(string RawNeededSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedNeededSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawNeededSkill, out ParsedNeededSkill, ErrorInfo);
            NeededSkill = ParsedNeededSkill;
            IsNeededSkillParsed = false;
            ConnectedNeededSkill = null;
        }

        private static void ParseFieldNeededSkillLevel(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNeededSkillLevel((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededSkillLevel");
        }

        private void ParseNeededSkillLevel(int RawNeededSkillLevel, ParseErrorInfo ErrorInfo)
        {
            this.RawNeededSkillLevel = RawNeededSkillLevel;
        }

        private static void ParseFieldNeededAbility(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNeededAbility;
            if ((RawNeededAbility = Value as string) != null)
                This.ParseNeededAbility(RawNeededAbility, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededAbility");
        }

        private void ParseNeededAbility(string RawNeededAbility, ParseErrorInfo ErrorInfo)
        {
            this.RawNeededAbility = RawNeededAbility;
        }

        private static void ParseFieldNeededInteractionFlag(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNeededInteractionFlag;
            if ((RawNeededInteractionFlag = Value as string) != null)
                This.ParseNeededInteractionFlag(RawNeededInteractionFlag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededInteractionFlag");
        }

        private void ParseNeededInteractionFlag(string RawNeededInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            NeededInteractionFlag = RawNeededInteractionFlag;
        }

        private static void ParseFieldNeededRecipe(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNeededRecipe;
            if ((RawNeededRecipe = Value as string) != null)
                This.ParseNeededRecipe(RawNeededRecipe, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededRecipe");
        }

        private void ParseNeededRecipe(string RawNeededRecipe, ParseErrorInfo ErrorInfo)
        {
            this.RawNeededRecipe = RawNeededRecipe;
        }

        private static void ParseFieldNeededRecipeCompletions(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNeededRecipeCompletions((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededRecipeCompletions");
        }

        private void ParseNeededRecipeCompletions(int RawNeededRecipeCompletions, ParseErrorInfo ErrorInfo)
        {
            this.RawNeededRecipeCompletions = RawNeededRecipeCompletions;
        }

        private static void ParseFieldNeededNotoriety(DirectedGoal This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNeededNotoriety;
            if ((RawNeededNotoriety = Value as string) != null)
                This.ParseNeededNotoriety(RawNeededNotoriety, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("DirectedGoal NeededNotoriety");
        }

        private void ParseNeededNotoriety(string RawNeededNotoriety, ParseErrorInfo ErrorInfo)
        {
            NeededNotoriety = RawNeededNotoriety;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("Label", Label);
            Generator.AddString("Zone", Zone);
            Generator.AddString("Hint", Hint);
            Generator.AddString("NeededSkill", StringToEnumConversion<PowerSkill>.ToString(NeededSkill, null, PowerSkill.Internal_None));
            Generator.AddInteger("NeededSkillLevel", RawNeededSkillLevel);
            Generator.AddString("NeededAbility", RawNeededAbility);
            Generator.AddString("NeededInteractionFlag", NeededInteractionFlag);
            Generator.AddInteger("NeededRecipeCompletions", RawNeededRecipeCompletions);
            Generator.AddString("NeededNotoriety", NeededNotoriety);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Label);
                AddWithFieldSeparator(ref Result, Zone);
                AddWithFieldSeparator(ref Result, Hint);
                if (NeededSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[NeededSkill]);
                if (NeededAbility != null)
                    AddWithFieldSeparator(ref Result, NeededAbility.Name);
                AddWithFieldSeparator(ref Result, CombinedNeededRecipe);

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "DirectedGoal"; } }

        protected override void InitializeFields()
        {
            NeededRecipe = null;
            IsRawNeededRecipeParsed = false;
            NeededAbility = null;
            IsRawNeededAbilityParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            NeededAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawNeededAbility, NeededAbility, ref IsRawNeededAbilityParsed, ref IsConnected, this);
            NeededRecipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawNeededRecipe, NeededRecipe, ref IsRawNeededRecipeParsed, ref IsConnected, this);

            if (NeededSkill != PowerSkill.Internal_None && NeededSkill != PowerSkill.AnySkill && NeededSkill != PowerSkill.Unknown)
                ConnectedNeededSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, NeededSkill, ConnectedNeededSkill, ref IsNeededSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
