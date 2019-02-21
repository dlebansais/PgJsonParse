using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirement : GenericJsonObject<AbilityRequirement>, IPgAbilityRequirement, ISpecificRecord
    {
        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public object ToSpecific(ParseErrorInfo errorInfo)
        {
            AbilityRequirement Result = ToSpecificAbilityRequirement(errorInfo);

            if (Result != null)
                Result.CopyFieldTableOrder(null, FieldTableOrder);

            return Result;
        }

        public void CopyFieldTableOrder(string key, List<string> fieldTableOrder)
        {
            if (key != null)
                InitializeKey(key, 0, null, null);

            foreach (string Key in fieldTableOrder)
                FieldTableOrder.Add(Key);
        }

        public AbilityRequirement ToSpecificAbilityRequirement(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case OtherRequirementType.IsLycanthrope:
                    return new AbilityRequirementIsLycanthrope();

                case OtherRequirementType.HasEffectKeyword:
                    return new AbilityRequirementHasEffectKeyword(Keyword);

                case OtherRequirementType.FullMoon:
                    return new AbilityRequirementIsFullMoon();

                case OtherRequirementType.IsHardcore:
                    return new AbilityRequirementIsHardcore();

                case OtherRequirementType.DruidEventState:
                    return new AbilityRequirementDruidEventState(DisallowedState);

                case OtherRequirementType.PetCount:
                    return new AbilityRequirementPetCount(PetTypeTag, RawMaxCount);

                case OtherRequirementType.RecipeKnown:
                    return new AbilityRequirementRecipeKnown(RawRecipeKnown);

                case OtherRequirementType.IsNotInCombat:
                    return new AbilityRequirementIsNotInCombat();

                case OtherRequirementType.IsLongtimeAnimal:
                    return new AbilityRequirementIsLongtimeAnimal();

                case OtherRequirementType.InHotspot:
                    return new AbilityRequirementInHotspot(RawName);

                case OtherRequirementType.HasInventorySpaceFor:
                    return new AbilityRequirementHasInventorySpaceFor(RawItem);

                case OtherRequirementType.IsVegetarian:
                    return new AbilityRequirementIsVegetarian();

                case OtherRequirementType.InGraveyard:
                    return new AbilityRequirementIsInGraveyard();

                case OtherRequirementType.Or:
                    return new AbilityRequirementOr(OrList, RawErrorMsg);

                case OtherRequirementType.EquippedItemKeyword:
                    return new AbilityRequirementEquippedItemKeyword(Keyword, RawMinCount, RawMaxCount.HasValue ? (int?)RawMaxCount.Value : null, ErrorInfo);

                case OtherRequirementType.InteractionFlagSet:
                    return new AbilityRequirementInteractionFlagSet(RawInteractionFlag);

                case OtherRequirementType.IsVolunteerGuide:
                    return new AbilityRequirementIsVolunteerGuide();

                case OtherRequirementType.IsNotGuest:
                    return new AbilityRequirementIsNotGuest();

                case OtherRequirementType.IsNotInHotspot:
                    return new AbilityRequirementNotInHotspot(RawName);

                default:
                    return null;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Type = StringToEnumConversion<OtherRequirementType>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type, null, OtherRequirementType.Internal_None) } },
            { "Keyword", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Keyword = StringToEnumConversion<AbilityKeyword>.Parse(value, errorInfo),
                GetString  = () => StringToEnumConversion<AbilityKeyword>.ToString(Keyword) } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawName = value,
                GetString  = () => RawName } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawItem = value,
                GetString  = () => RawItem } },
            { "MinCount", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMinCount = value,
                GetInteger = () => RawMinCount } },
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
                GetStringArray = () => StringToEnumConversion<DisallowedState>.ToSingleOrEmptyStringList(DisallowedState) } },
            { "PetTypeTag", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => PetTypeTag = StringToEnumConversion<RecipeKeyword>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<RecipeKeyword>.ToString(PetTypeTag) } },
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
            { "HangOut", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawHangOut = value,
                GetString  = () => RawHangOut } },
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

        private bool ParseDisallowedStates(string value, ParseErrorInfo ErrorInfo)
        {
            if (DisallowedState == DisallowedState.Internal_None)
            {
                DisallowedState ParsedDisallowedState;
                StringToEnumConversion<DisallowedState>.TryParse(value, out ParsedDisallowedState, ErrorInfo);
                DisallowedState = ParsedDisallowedState;
                return true;
            }
            else
            {
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement DisallowedStates");
                return false;
            }
        }

        public virtual OtherRequirementType Type { get; private set; }
        private double? RawHealth;
        private List<string> RawAllowedRaceList { get; } = new List<string>();
        private string RawAllowedRace;
        private AbilityKeyword Keyword;
        private DisallowedState DisallowedState;
        private RecipeKeyword PetTypeTag;
        private double? RawMaxCount;
        private string RawRecipeKnown;
        private string RawName;
        private string RawHangOut;
        private string RawItem;
        private List<string> RawAppearanceList { get; } = new List<string>();
        private string RawAppearance;
        private IPgAbilityRequirementCollection OrList { get; } = new AbilityRequirementCollection();
        private string RawErrorMsg;
        private int? RawMinCount;
        private string RawTypeTag;
        private string RawInteractionFlag;
        private int? RawMax;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get { return ""; }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }

        public virtual IList<IBackLinkable> GetLinkBack()
        {
            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilityRequirement"; } }
        #endregion

        #region Serializing
        protected void SerializeJsonObjectInternalProlog(byte[] data, ref int offset, Dictionary<int, string> StoredStringtable, Dictionary<int, List<string>> StoredStringListTable)
        {
            int BaseOffset = offset;

            AddInt((int)Type, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 8, StoredStringListTable);
        }

        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            throw new InvalidOperationException();
        }
        #endregion
    }
}
