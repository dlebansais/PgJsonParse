using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirement : GenericJsonObject<AbilityRequirement>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
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
        };
        #endregion

        #region Properties
        public OtherRequirementType T { get; private set; }
        public AbilityKeyword Keyword { get; private set; }
        public string Name { get; private set; }
        public Item Item { get; private set; }
        public double Count { get { return RawCount.HasValue ? RawCount.Value : 0; } }
        private double? RawCount;
        public double Health { get { return RawHealth.HasValue ? RawHealth.Value : 0; } }
        private double? RawHealth;
        public Race AllowedRace { get; private set; }
        public List<Race> AllowedRaceList { get; private set; }
        public Appearance Appearance { get; private set; }
        public List<Appearance> AppearanceList { get; private set; }
        public string ErrorMsg { get; private set; }
        public DisallowedState DisallowedState { get; private set; }
        public RecipeKeyword PetTypeTag { get; private set; }
        public double MaxCount { get { return RawMaxCount.HasValue ? RawMaxCount.Value : 0; } }
        private double? RawMaxCount;
        public Recipe RecipeKnown { get; private set; }
        private string RawRecipeKnown;
        private bool IsRawRecipeKnownParsed;
        public AbilityTypeTag TypeTag { get; private set; }
        public int Max { get { return RawMax.HasValue ? RawMax.Value : 0; } }
        private int? RawMax;

        protected override string SortingName { get { return Name; } }

        public string CombinedRequirement
        {
            get
            {
                string Result;

                switch (T)
                {
                    case OtherRequirementType.IsAdmin:
                        return "Game Admin Only";

                    case OtherRequirementType.IsLycanthrope:
                        return "Be a Lycanthrope";

                    case OtherRequirementType.CurHealth:
                        return "Health >= " + Health;

                    case OtherRequirementType.Race:
                        return "Be a " + TextMaps.RaceTextMap[AllowedRace];

                    case OtherRequirementType.HasEffectKeyword:
                        return "Effect " + TextMaps.AbilityKeywordTextMap[Keyword] + " is active";

                    case OtherRequirementType.FullMoon:
                        return "Full Moon";

                    case OtherRequirementType.IsHardcore:
                        return "Be Hardcore";

                    case OtherRequirementType.DruidEventState:
                        return "During druid event";

                    case OtherRequirementType.PetCount:
                        return "Max " + MaxCount + " " + TextMaps.RecipeKeywordTextMap[PetTypeTag];

                    case OtherRequirementType.RecipeKnown:
                        return "Knows recipe " + RecipeKnown.Name;

                    case OtherRequirementType.IsNotInCombat:
                        return "Out of combat";

                    case OtherRequirementType.IsLongtimeAnimal:
                        return "Animal for a long time";

                    case OtherRequirementType.InHotspot:
                        return Name;

                    case OtherRequirementType.HasInventorySpaceFor:
                        return "Has inventory space for " + Item.Name;

                    case OtherRequirementType.IsVegetarian:
                        return "Is Vegetarian";

                    case OtherRequirementType.InGraveyard:
                        return "In Graveyard";

                    case OtherRequirementType.Appearance:
                        return "Looks like: " + TextMaps.AppearanceTextMap[Appearance];

                    case OtherRequirementType.Or:
                        Result = "";

                        if (AppearanceList.Count > 0)
                        {
                            Result += "Looks like ";

                            for (int i = 0; i < AppearanceList.Count; i++)
                            {
                                if (i > 0 && i + 1 < AppearanceList.Count)
                                    Result += ", ";
                                else if (i + 1 >= AppearanceList.Count)
                                    Result += " or ";

                                Result += TextMaps.AppearanceTextMap[AppearanceList[i]];
                            }
                        }

                        if (AllowedRaceList.Count > 0)
                        {
                            if (Result.Length == 0)
                                Result += "Is ";
                            else
                                Result += ", or is ";

                            for (int i = 0; i < AllowedRaceList.Count; i++)
                            {
                                if (i > 0 && i + 1 < AllowedRaceList.Count)
                                    Result += ", ";
                                else if (i + 1 >= AllowedRaceList.Count)
                                    Result += " or ";

                                Result += TextMaps.RaceTextMap[AllowedRaceList[i]];
                            }
                        }

                        return "Either " + Result;

                    case OtherRequirementType.EquippedItemKeyword:
                        return "Has " + Count + " " + TextMaps.AbilityKeywordTextMap[Keyword] + " items equipped";

                    case OtherRequirementType.GardenPlantMax:
                        return "Max allowed: " + Max;
                }

                return "";
            }
        }
        #endregion

        #region Client Interface
        private static void ParseFieldT(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawT;
            if ((RawT = Value as string) != null)
                This.ParseT(RawT, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement T");
        }

        private void ParseT(string RawT, ParseErrorInfo ErrorInfo)
        {
            OtherRequirementType ParsedT;
            StringToEnumConversion<OtherRequirementType>.TryParse(RawT, out ParsedT, ErrorInfo);
            T = ParsedT;
        }

        private static void ParseFieldKeyword(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawKeyword;
            if ((RawKeyword = Value as string) != null)
                This.ParseKeyword(RawKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Keyword");
        }

        private void ParseKeyword(string RawKeyword, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword ParsedKeyword;
            StringToEnumConversion<AbilityKeyword>.TryParse(RawKeyword, out ParsedKeyword, ErrorInfo);
            Keyword = ParsedKeyword;
        }

        private static void ParseFieldName(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldItem(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItem;
            if ((RawItem = Value as string) != null)
                This.ParseItem(RawItem, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Item");
        }

        private void ParseItem(string RawItem, ParseErrorInfo ErrorInfo)
        {
            this.RawItem = RawItem;
            IsRawItemParsed = false;
        }

        private static void ParseFieldCount(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseCount((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseCount(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Count");
        }

        private void ParseCount(double RawCount, ParseErrorInfo ErrorInfo)
        {
            this.RawCount = RawCount;
        }

        private static void ParseFieldHealth(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseHealth((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseHealth(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Health");
        }

        private void ParseHealth(double RawHealth, ParseErrorInfo ErrorInfo)
        {
            this.RawHealth = RawHealth;
        }

        private static void ParseFieldAllowedRace(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAllowedRace;
            if ((RawAllowedRace = Value as string) != null)
                This.ParseAllowedRace(RawAllowedRace, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement AllowedRace");
        }

        private void ParseAllowedRace(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            if (AllowedRaceList.Count != 0)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement AllowedRace & AllowedRaceList");

            Race ParsedAllowedRace;
            StringToEnumConversion<Race>.TryParse(RawAllowedRace, out ParsedAllowedRace, ErrorInfo);
            AllowedRace = ParsedAllowedRace;
        }

        private void ParseAllowedRaceList(string RawAllowedRace, ParseErrorInfo ErrorInfo)
        {
            if (AllowedRace != Race.Internal_None)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement AllowedRace & AllowedRaceList");

            Race ParsedAllowedRace;
            StringToEnumConversion<Race>.TryParse(RawAllowedRace, out ParsedAllowedRace, ErrorInfo);

            AllowedRaceList.Add(ParsedAllowedRace);
        }

        private static void ParseFieldAppearance(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAppearance;
            if ((RawAppearance = Value as string) != null)
                This.ParseAppearance(RawAppearance, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Appearance");
        }

        private void ParseAppearance(string RawAppearance, ParseErrorInfo ErrorInfo)
        {
            if (AllowedRaceList.Count != 0)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Appearance & AppearanceList");

            Appearance ParsedAppearance;
            StringToEnumConversion<Appearance>.TryParse(RawAppearance, out ParsedAppearance, ErrorInfo);
            Appearance = ParsedAppearance;
        }

        private void ParseAppearanceList(string RawAppearance, ParseErrorInfo ErrorInfo)
        {
            if (Appearance != Appearance.Internal_None)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Appearance & AppearanceList");

            Appearance ParsedAppearance;
            StringToEnumConversion<Appearance>.TryParse(RawAppearance, out ParsedAppearance, ErrorInfo);

            AppearanceList.Add(ParsedAppearance);
        }

        private static void ParseFieldList(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (This.T != OtherRequirementType.Or)
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List");

            else
            {
                ArrayList RawList;
                Dictionary<string, object> AsDictionary;

                if ((RawList = Value as ArrayList) != null)
                {
                    foreach (object Item in RawList)
                    {
                        if ((AsDictionary = Item as Dictionary<string, object>) != null)
                            This.ParseList(AsDictionary, ErrorInfo);
                    }
                }

                else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                    This.ParseList(AsDictionary, ErrorInfo);

                else
                    ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List");
            }
        }

        private void ParseList(Dictionary<string, object> RawList, ParseErrorInfo ErrorInfo)
        {
            OtherRequirementType T = OtherRequirementType.Internal_None;

            foreach (KeyValuePair<string, object> Entry in RawList)
            {
                string ValueString;

                if ((ValueString = Entry.Value as string) != null)
                {
                    if (Entry.Key == "T")
                    {
                        if (T != OtherRequirementType.Internal_None)
                        {
                            ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List (twice T)");
                            break;
                        }

                        OtherRequirementType ParsedT;
                        StringToEnumConversion<OtherRequirementType>.TryParse(ValueString, out ParsedT, ErrorInfo);
                        T = ParsedT;
                    }

                    else if (Entry.Key == "AllowedRace")
                        if (T == OtherRequirementType.Race)
                            ParseAllowedRaceList(ValueString, ErrorInfo);
                        else
                            ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List AllowedRace");

                    else if (Entry.Key == "Appearance")
                        if (T == OtherRequirementType.Appearance)
                            ParseAppearanceList(ValueString, ErrorInfo);
                        else
                            ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List Appearance");

                    else
                        ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("AbilityRequirement List");
            }
        }

        private static void ParseFieldErrorMsg(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawErrorMsg;
            if ((RawErrorMsg = Value as string) != null)
                This.ParseErrorMsg(RawErrorMsg, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement ErrorMsg");
        }

        private void ParseErrorMsg(string RawErrorMsg, ParseErrorInfo ErrorInfo)
        {
            ErrorMsg = RawErrorMsg;
        }

        private static void ParseFieldDisallowedStates(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawDisallowedStates;
            if ((RawDisallowedStates = Value as ArrayList) != null)
                This.ParseDisallowedStates(RawDisallowedStates, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement DisallowedStates");
        }

        private void ParseDisallowedStates(ArrayList RawDisallowedStates, ParseErrorInfo ErrorInfo)
        {
            List<DisallowedState> ParsedDisallowedStates = new List<DisallowedState>();
            StringToEnumConversion<DisallowedState>.ParseList(RawDisallowedStates, ParsedDisallowedStates, ErrorInfo);

            if (ParsedDisallowedStates.Count == 1)
                DisallowedState = ParsedDisallowedStates[0];
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement DisallowedStates");
        }

        private static void ParseFieldPetTypeTag(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawPetTypeTag;
            if ((RawPetTypeTag = Value as string) != null)
                This.ParsePetTypeTag(RawPetTypeTag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement PetTypeTag");
        }

        private void ParsePetTypeTag(string RawPetTypeTag, ParseErrorInfo ErrorInfo)
        {
            RecipeKeyword ParsedPetTypeTag;
            StringToEnumConversion<RecipeKeyword>.TryParse(RawPetTypeTag, out ParsedPetTypeTag, ErrorInfo);
            PetTypeTag = ParsedPetTypeTag;
        }

        private static void ParseFieldMaxCount(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMaxCount((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseMaxCount(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement MaxCount");
        }

        private void ParseMaxCount(double RawMaxCount, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxCount = RawMaxCount;
        }

        private static void ParseFieldRecipe(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRecipe;
            if ((RawRecipe = Value as string) != null)
                This.ParseRecipe(RawRecipe, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Recipe");
        }

        private void ParseRecipe(string RawRecipe, ParseErrorInfo ErrorInfo)
        {
            RawRecipeKnown = RawRecipe;
        }

        private static void ParseFieldTypeTag(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawTypeTag;
            if ((RawTypeTag = Value as string) != null)
                This.ParseTypeTag(RawTypeTag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement TypeTag");
        }

        private void ParseTypeTag(string RawTypeTag, ParseErrorInfo ErrorInfo)
        {
            AbilityTypeTag ParsedTypeTag;
            StringToEnumConversion<AbilityTypeTag>.TryParse(RawTypeTag, out ParsedTypeTag, ErrorInfo);
            TypeTag = ParsedTypeTag;
        }

        private static void ParseFieldMax(AbilityRequirement This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMax((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilityRequirement Max");
        }

        private void ParseMax(int RawMax, ParseErrorInfo ErrorInfo)
        {
            this.RawMax = RawMax;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", T.ToString());
            Generator.AddString("Keyword", Keyword.ToString());
            Generator.AddString("Name", Name);
            Generator.AddString("Item", RawItem);
            Generator.AddDouble("Count", RawCount);

            Generator.CloseObject();
        }

        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, T.ToString());
                AddWithFieldSeparator(ref Result, Name);

                if (Item != null)
                    AddWithFieldSeparator(ref Result, Item.TextContent);

                return Result;
            }
        }

        private string RawItem;
        private bool IsRawItemParsed;
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "AbilityRequirement"; } }

        protected override void InitializeFields()
        {
            AllowedRaceList = new List<Race>();
            AppearanceList = new List<Appearance>();
            RecipeKnown = null;
            RawRecipeKnown = null;
            IsRawRecipeKnownParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            Item = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, Item, ref IsRawItemParsed, ref IsConnected, this);
            RecipeKnown = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipeKnown, RecipeKnown, ref IsRawRecipeKnownParsed, ref IsConnected, this);
            if (T == OtherRequirementType.RecipeKnown && RecipeKnown == null)
                RecipeKnown = null;

            return IsConnected;
        }
        #endregion
    }
}
