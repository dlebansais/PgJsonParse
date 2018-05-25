using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirement : GenericJsonObject<AbilityRequirement>, ISpecificRecord
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public object ToSpecific(ParseErrorInfo errorInfo)
        {
            return ToSpecificAbilityRequirement(errorInfo);
        }

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
            { "T", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => OtherRequirementType = StringToEnumConversion<OtherRequirementType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawKeyword = value,
                GetString  = () => RawKeyword } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawName = value,
                GetString  = () => RawName } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawItem = value,
                GetString  = () => RawItem } },
            { "Count", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawCount = value,
                GetFloat = () => RawCount } },
            { "Health", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawHealth = value,
                GetFloat = () => RawHealth } },
            { "AllowedRace", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAllowedRace,
                GetString = () => "" } },//TODO
            { "Appearance", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAppearance,
                GetString = () => "" } },//TODO
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<AbilityRequirement>.ParseList("List", value, OrList, errorInfo),
                GetObjectArray = () => OrList } },
            { "ErrorMsg", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawErrorMsg = value,
                GetString = () => RawErrorMsg } },
            { "DisallowedStates", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseDisallowedStates,
                GetStringArray = () => GenerateDisallowedStates() } },
            { "PetTypeTag", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawPetTypeTag = value,
                GetString = () => RawPetTypeTag } },
            { "MaxCount", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawMaxCount = value,
                GetFloat = () => RawMaxCount } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawRecipeKnown = value,
                GetString = () => RawRecipeKnown } },
            { "TypeTag", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawTypeTag = value,
                GetString = () => RawTypeTag } },
            { "Max", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMax = value,
                GetInteger = () => RawMax } },
            { "InteractionFlag", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawInteractionFlag = value,
                GetString = () => RawInteractionFlag } },
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

        private List<string> GenerateDisallowedStates()
        {
            List<string> Result = new List<string>();

            if (RawDisallowedState != null)
                Result.Add(RawDisallowedState);

            return Result;
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
