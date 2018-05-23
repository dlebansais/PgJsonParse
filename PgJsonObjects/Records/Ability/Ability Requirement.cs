using PgJsonReader;
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
            switch (OtherRequirementType)
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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { OtherRequirementType = StringToEnumConversion<OtherRequirementType>.Parse(value, errorInfo); }} },
            { "Keyword", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawKeyword = value; }} },
            { "Name", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawName = value; }} },
            { "Item", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawItem = value; }} },
            { "Count", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawCount = value; }} },
            { "Health", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawHealth = value; }} },
            { "AllowedRace", new FieldParser() { Type = FieldType.String, ParserString = ParseAllowedRace } },
            { "Appearance", new FieldParser() { Type = FieldType.String, ParserString = ParseAppearance } },
            { "List", new FieldParser() { Type = FieldType.ObjectArray, ParserObjectArray = ParseList } },
            { "ErrorMsg", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawErrorMsg = value; }} },
            { "DisallowedStates", new FieldParser() { Type = FieldType.StringArray, ParserStringArray = ParseDisallowedStates } },
            { "PetTypeTag", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawPetTypeTag = value; }} },
            { "MaxCount", new FieldParser() { Type = FieldType.Float, ParserFloat = (float value, ParseErrorInfo errorInfo) => { RawMaxCount = value; }} },
            { "Recipe", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawRecipeKnown = value; }} },
            { "TypeTag", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawTypeTag = value; }} },
            { "Max", new FieldParser() { Type = FieldType.Integer, ParserInteger = (int value, ParseErrorInfo errorInfo) => { RawMax = value; }} },
            { "InteractionFlag", new FieldParser() { Type = FieldType.String, ParserString = (string value, ParseErrorInfo errorInfo) => { RawInteractionFlag = value; }} },
        }; } }

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

        private void ParseList(JsonObject RawList, ParseErrorInfo ErrorInfo)
        {
            AbilityRequirement ParsedAbilityRequirement;
            JsonObjectParser<AbilityRequirement>.InitAsSubitem("List", RawList, out ParsedAbilityRequirement, ErrorInfo);

            AbilityRequirement ConvertedAbilityRequirement = ParsedAbilityRequirement.ToSpecificAbilityRequirement(ErrorInfo);
            if (ConvertedAbilityRequirement != null)
                OrList.Add(ConvertedAbilityRequirement);
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

        private OtherRequirementType OtherRequirementType;
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
            throw new InvalidOperationException();
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
