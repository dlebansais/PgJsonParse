using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirement : GenericJsonObject<AbilityRequirement>
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public AbilityRequirement ToSpecificAbilityRequirement(ParseErrorInfo ErrorInfo)
        {
            switch (T)
            {
                case OtherRequirementType.IsAdmin:
                    return new IsAdminAbilityRequirement();

                case OtherRequirementType.IsLycanthrope:
                    return new IsLycanthropeAbilityRequirement();

                case OtherRequirementType.CurHealth:
                    return new CurHealthAbilityRequirement(RawHealth);

                case OtherRequirementType.Race:
                    return new RaceAbilityRequirement(RawAllowedRace, RawAllowedRaceList, ErrorInfo);

                case OtherRequirementType.HasEffectKeyword:
                    return new HasEffectKeywordAbilityRequirement(RawKeyword, ErrorInfo);

                case OtherRequirementType.FullMoon:
                    return new FullMoonAbilityRequirement();

                case OtherRequirementType.IsHardcore:
                    return new IsHardcoreAbilityRequirement();

                case OtherRequirementType.DruidEventState:
                    return new DruidEventStateAbilityRequirement(RawDisallowedState, ErrorInfo);

                case OtherRequirementType.PetCount:
                    return new PetCountAbilityRequirement(RawPetTypeTag, RawMaxCount, ErrorInfo);

                case OtherRequirementType.RecipeKnown:
                    return new RecipeKnownAbilityRequirement(RawRecipeKnown);

                case OtherRequirementType.IsNotInCombat:
                    return new IsNotInCombatAbilityRequirement();

                case OtherRequirementType.IsLongtimeAnimal:
                    return new IsLongtimeAnimalAbilityRequirement();

                case OtherRequirementType.InHotspot:
                    return new InHotspotAbilityRequirement(RawName);

                case OtherRequirementType.HasInventorySpaceFor:
                    return new HasInventorySpaceForAbilityRequirement(RawItem);

                case OtherRequirementType.IsVegetarian:
                    return new IsVegetarianAbilityRequirement();

                case OtherRequirementType.InGraveyard:
                    return new InGraveyardAbilityRequirement();

                case OtherRequirementType.Appearance:
                    return new AppearanceAbilityRequirement(RawAppearance, RawAppearanceList, ErrorInfo);

                case OtherRequirementType.Or:
                    return new OrAbilityRequirement(OrList, RawErrorMsg);

                case OtherRequirementType.EquippedItemKeyword:
                    return new EquippedItemKeywordAbilityRequirement(RawKeyword, RawCount, ErrorInfo);

                case OtherRequirementType.GardenPlantMax:
                    return new GardenPlantMaxAbilityRequirement(RawTypeTag, RawMax, ErrorInfo);

                case OtherRequirementType.InteractionFlagSet:
                    return new InteractionFlagSetAbilityRequirement(RawInteractionFlag);

                default:
                    return null;
            }
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "T", ParseFieldT },
            { "Keyword", ParseFieldKeyword },
            { "Name", ParseFieldName },
            { "Item", ParseFieldItem },
            { "Count", ParseFieldCount },
            { "Health", ParseFieldHealth },
            { "AllowedRace", ParseFieldAllowedRace },
            { "Appearance", ParseFieldAppearance },
            { "List", ParseFieldList },
            { "ErrorMsg", ParseFieldErrorMsg },
            { "DisallowedStates", ParseFieldDisallowedStates },
            { "PetTypeTag", ParseFieldPetTypeTag },
            { "MaxCount", ParseFieldMaxCount },
            { "Recipe", ParseFieldRecipe },
            { "TypeTag", ParseFieldTypeTag },
            { "Max", ParseFieldMax },
            { "InteractionFlag", ParseFieldInteractionFlag },
        };

        private static void ParseFieldT(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement T", This.ParseT);
        }

        private void ParseT(string RawT, ParseErrorInfo ErrorInfo)
        {
            OtherRequirementType ParsedT;
            StringToEnumConversion<OtherRequirementType>.TryParse(RawT, out ParsedT, ErrorInfo);
            T = ParsedT;
        }

        private static void ParseFieldKeyword(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement Keyword", This.ParseKeyword);
        }

        private void ParseKeyword(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            this.RawKeyword = RawKeyword;
        }

        private static void ParseFieldName(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement Name", This.ParseName);
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            this.RawName = RawName;
        }

        private static void ParseFieldItem(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement Item", This.ParseItem);
        }

        private void ParseItem(string RawItem, ParseErrorInfo ErrorInfo)
        {
            this.RawItem = RawItem;
        }

        private static void ParseFieldCount(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is long)
                This.ParseCount((long)Value, ErrorInfo);
            else if (Value is double)
                This.ParseCount((double)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Count");
        }

        private void ParseCount(double RawCount, ParseErrorInfo ErrorInfo)
        {
            this.RawCount = RawCount;
        }

        private static void ParseFieldHealth(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is long)
                This.ParseHealth((long)Value, ErrorInfo);
            else if (Value is double)
                This.ParseHealth((double)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Health");
        }

        private void ParseHealth(double RawHealth, ParseErrorInfo ErrorInfo)
        {
            this.RawHealth = RawHealth;
        }

        private static void ParseFieldAllowedRace(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement AllowedRace", This.ParseAllowedRace);
        }

        private void ParseAllowedRace(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            if (RawAllowedRaceList.Count != 0)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement AllowedRace & AllowedRaceList");
            else
                this.RawAllowedRace = RawAllowedRace;
        }

        private void ParseAllowedRaceList(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            if (this.RawAllowedRace != null)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement AllowedRace & AllowedRaceList");
            else
                RawAllowedRaceList.Add(RawAllowedRace);
        }

        private static void ParseFieldAppearance(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement Appearance", This.ParseAppearance);
        }

        private void ParseAppearance(string RawAppearance, ParseErrorInfo ErrorInfo)
        {
            if (RawAppearanceList.Count != 0)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Appearance & AppearanceList");
            else
                this.RawAppearance = RawAppearance;
        }

        private void ParseAppearanceList(string RawAppearance, ParseErrorInfo ErrorInfo)
        {
            if (this.RawAppearance != null)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Appearance & AppearanceList");
            else
                RawAppearanceList.Add(RawAppearance);
        }

        private static void ParseFieldList(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            JArray AsJArray;
            if ((AsJArray = Value as JArray) != null)
                This.ParseList(AsJArray, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List");
        }

        private void ParseList(JArray RawList, ParseErrorInfo ErrorInfo)
        {
            List<AbilityRequirement> ParsedAbilityRequirementList;
            JsonObjectParser<AbilityRequirement>.InitAsSublist(RawList, out ParsedAbilityRequirementList, ErrorInfo);

            foreach (AbilityRequirement Item in ParsedAbilityRequirementList)
            {
                AbilityRequirement ConvertedAbilityRequirement = Item.ToSpecificAbilityRequirement(ErrorInfo);
                if (ConvertedAbilityRequirement != null)
                    OrList.Add(ConvertedAbilityRequirement);
            }
        }

        private static void ParseFieldErrorMsg(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement ErrorMsg", This.ParseErrorMsg);
        }

        private void ParseErrorMsg(string RawErrorMsg, ParseErrorInfo ErrorInfo)
        {
            this.RawErrorMsg = RawErrorMsg;
        }

        private static void ParseFieldDisallowedStates(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueStringArray(Value, ErrorInfo, "AbilityRequirement DisallowedStates", This.ParseDisallowedStates);
        }

        private bool ParseDisallowedStates(string RawDisallowedState, ParseErrorInfo ErrorInfo)
        {
            if (this.RawDisallowedState == null)
            {
                this.RawDisallowedState = RawDisallowedState;
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement DisallowedStates");
                return false;
            }
        }

        private static void ParseFieldPetTypeTag(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement PetTypeTag", This.ParsePetTypeTag);
        }

        private void ParsePetTypeTag(string RawPetTypeTag, ParseErrorInfo ErrorInfo)
        {
            this.RawPetTypeTag = RawPetTypeTag;
        }

        private static void ParseFieldMaxCount(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is long)
                This.ParseMaxCount((long)Value, ErrorInfo);
            else if (Value is double)
                This.ParseMaxCount((double)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement MaxCount");
        }

        private void ParseMaxCount(double RawMaxCount, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxCount = RawMaxCount;
        }

        private static void ParseFieldRecipe(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement Recipe", This.ParseRecipe);
        }

        private void ParseRecipe(string RawRecipe, ParseErrorInfo ErrorInfo)
        {
            RawRecipeKnown = RawRecipe;
        }

        private static void ParseFieldTypeTag(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement TypeTag", This.ParseTypeTag);
        }

        private void ParseTypeTag(string RawTypeTag, ParseErrorInfo ErrorInfo)
        {
            this.RawTypeTag = RawTypeTag;
        }

        private static void ParseFieldMax(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueLong(Value, ErrorInfo, "AbilityRequirement Max", This.ParseMax);
        }

        private void ParseMax(long RawMax, ParseErrorInfo ErrorInfo)
        {
            this.RawMax = (int)RawMax;
        }

        private static void ParseFieldInteractionFlag(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ParseFieldValueString(Value, ErrorInfo, "AbilityRequirement InteractionFlag", This.ParseInteractionFlag);
        }

        private void ParseInteractionFlag(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            this.RawInteractionFlag = RawInteractionFlag;
        }

        private OtherRequirementType T;
        private double? RawHealth;
        private List<string> RawAllowedRaceList { get; } = new List<string>();
        private string RawAllowedRace;
        private string RawKeyword;
        private string RawDisallowedState;
        private string RawPetTypeTag;
        private double? RawMaxCount;
        private string RawRecipeKnown;
        private string RawName;
        private string RawItem;
        private List<string> RawAppearanceList { get; } = new List<string>();
        private string RawAppearance;
        private List<AbilityRequirement> OrList { get; } = new List<AbilityRequirement>();
        private string RawErrorMsg;
        private double? RawCount;
        private string RawTypeTag;
        private string RawInteractionFlag;
        private int? RawMax;
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

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
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilityRequirement"; } }
        #endregion
    }
}
