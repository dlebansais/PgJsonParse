using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Skill : MainJsonObject<Skill>, IPgSkill
    {
        #region Direct Properties
        public PowerSkill CombatSkill { get; private set; }
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get; private set; }
        public bool Combat { get { return RawCombat.HasValue && RawCombat.Value; } }
        public bool? RawCombat { get; private set; }
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get; private set; }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get; private set; }
        public bool ParentSkillIsEmpty { get { return RawParentSkillIsEmpty.HasValue && RawParentSkillIsEmpty.Value; } }
        public bool? RawParentSkillIsEmpty { get; private set; }
        public bool IsAdvancementTableNull { get; private set; }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get; private set; }
        public string Description { get; private set; }
        public IPgXpTable XpTable { get; private set; }
        public string RawXpTable { get; private set; }
        public IPgAdvancementTable AdvancementTable { get; private set; }
        public List<PowerSkill> CompatibleCombatSkillList { get; } = new List<PowerSkill>();
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get; private set; }
        public IPgLevelCapInteractionCollection InteractionFlagLevelCapList { get; } = new LevelCapInteractionCollection();
        public IPgRewardCollection RewardList { get; } = new RewardCollection();
        public string Name { get; private set; }
        public IPgSkill ParentSkill { get; private set; }
        public List<SkillCategory> TSysCategoryList { get; } = new List<SkillCategory>();
        public List<ItemKeyword> RecipeIngredientKeywordList { get; } = new List<ItemKeyword>();
        public List<SkillRewardCommon> CombinedRewardList { get; private set; }
        private bool IsRawXpTableParsed;
        private string RawAdvancementTable;
        private bool IsRawAdvancementTableParsed;
        private bool EmptyRewardList;
        private bool IsParentSkillParsed;
        private PowerSkill RawParentSkill;
        private List<int> AdvancementHintTableKey = new List<int>();
        private List<string> AdvancementHintTableValue = new List<string>();
        private List<int> ReportTableKey = new List<int>();
        private List<string> ReportTableValue = new List<string>();
        #endregion

        #region Indirect Properties
        private static Dictionary<PowerSkill, int> HardcodedSkillIconTable = new Dictionary<PowerSkill, int>()
        {
            { PowerSkill.Mining, 5297 },
            { PowerSkill.Anatomy, 5055},
            { PowerSkill.AlcoholTolerance, 5744 },
            { PowerSkill.Skinning, 5171 },
            { PowerSkill.Performance, 5380 },
            { PowerSkill.Cartography, 4004 },
            { PowerSkill.Augmentation, 5492 },
        };

        private static Dictionary<PowerSkill, Ability> BasicAttackTable = new Dictionary<PowerSkill, Ability>();
        private static Dictionary<PowerSkill, int> AnyIconTable = new Dictionary<PowerSkill, int>();
        public override string SortingName { get { return Name; } }
        public int IconId { get; private set; }

        public static void UpdateBasicAttackTable(PowerSkill Skill, Ability Ability)
        {
            if (Skill != PowerSkill.Internal_None && Ability != null)
                if (!PgJsonObjects.Skill.BasicAttackTable.ContainsKey(Skill))
                    PgJsonObjects.Skill.BasicAttackTable.Add(Skill, Ability);
        }

        public static void UpdateAnySkillIcon(PowerSkill Skill, int? RawIconId)
        {
            if (Skill != PowerSkill.Internal_None && RawIconId.HasValue)
                if (!AnyIconTable.ContainsKey(Skill))
                    AnyIconTable.Add(Skill, RawIconId.Value);
        }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IJsonKey>> AllTables, ParseErrorInfo ErrorInfo)
        {
            PowerSkill IconSkill = CombatSkill;

            if (HardcodedSkillIconTable.ContainsKey(IconSkill))
            {
                IconId = HardcodedSkillIconTable[IconSkill];
                return;
            }

            if (!BasicAttackTable.ContainsKey(IconSkill))
            {
                foreach (PowerSkill SkillItem in CompatibleCombatSkillList)
                    if (BasicAttackTable.ContainsKey(IconSkill))
                    {
                        IconSkill = SkillItem;
                        break;
                    }
            }

            if (!BasicAttackTable.ContainsKey(IconSkill))
            {
                if (BasicAttackTable.ContainsKey(RawParentSkill))
                    IconSkill = RawParentSkill;
            }

            if (!BasicAttackTable.ContainsKey(IconSkill) && !AnyIconTable.ContainsKey(IconSkill))
            {
                if (ParentSkill != null && ((ParentSkill as Skill).IconId != 0 || HardcodedSkillIconTable.ContainsKey(RawParentSkill)))
                    IconSkill = RawParentSkill;

                else
                {
                    foreach (SkillRewardCommon Reward in CombinedRewardList)
                    {
                        SkillRewardBonusLevel AsBonusLevelReward;
                        if ((AsBonusLevelReward = Reward as SkillRewardBonusLevel) != null)
                            if (AnyIconTable.ContainsKey(AsBonusLevelReward.Skill.CombatSkill) && (AsBonusLevelReward.Skill as Skill).IconId != 0)
                            {
                                AnyIconTable.Add(IconSkill, (AsBonusLevelReward.Skill as Skill).IconId);
                                break;
                            }
                    }
                }
            }

            IconId = BestIconIdForSkill(IconSkill);
            if (IconId == 0)
                IconId = 0;
        }

        public string SearchResultIconFileName
        {
            get
            {
                if (IconId == 0)
                    return null;

                return "icon_" + IconId;
            }
        }

        public static int BestIconIdForSkill(PowerSkill IconSkill)
        {
            int IconId = 0;

            if (BasicAttackTable.ContainsKey(IconSkill))
                IconId = BasicAttackTable[IconSkill].IconId;

            if (IconId == 0 && AnyIconTable.ContainsKey(IconSkill))
                IconId = AnyIconTable[IconSkill];

            if (HardcodedSkillIconTable.ContainsKey(IconSkill))
                IconId = HardcodedSkillIconTable[IconSkill];

            return IconId;
        }
        #endregion

        #region Parsing
        protected override void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(key, index, value, ErrorInfo);

            PowerSkill ParsedPowerSkill;
            StringToEnumConversion<PowerSkill>.TryParse(Key, out ParsedPowerSkill, ErrorInfo);
            CombatSkill = ParsedPowerSkill;
        }

        public override void Init(string key, int index, IJsonValue value, bool loadAsArray, ParseErrorInfo ErrorInfo)
        {
            base.Init(key, index, value, loadAsArray, ErrorInfo);

            if (Name == null)
                Name = Key;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawId = value,
                GetInteger = () => RawId } },
            { "Description", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Description = value,
                GetString = () => Description } },
            { "HideWhenZero", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawHideWhenZero = value,
                GetBool = () => RawHideWhenZero } },
            { "XpTable", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawXpTable = value,
                GetString = () => RawXpTable } },
            { "AdvancementTable", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseAdvancementTable,
                GetString = () => IsAdvancementTableNull ? GenericJsonObject.NullString : (AdvancementTable != null ? AdvancementTable.InternalName : null)} },
            { "Combat", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawCombat = value,
                GetBool = () => RawCombat } },
            { "CompatibleCombatSkills", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<PowerSkill>.ParseList(value, CompatibleCombatSkillList, errorInfo),
                GetStringArray = () => StringToEnumConversion<PowerSkill>.ToStringList(CompatibleCombatSkillList) } },
            { "MaxBonusLevels", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawMaxBonusLevels = value,
                GetInteger = () => RawMaxBonusLevels } },
            { "InteractionFlagLevelCaps", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseInteractionFlagLevelCaps,
                GetObject = GetInteractionFlagLevelCaps } },
            { "AdvancementHints", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseAdvancementHints,
                GetObject = GetAdvancementHints } },
            { "Rewards", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseRewards,
                GetObject = GetRewards } },
            { "Reports", new FieldParser() {
                Type = FieldType.Object,
                ParseObject = ParseReports,
                GetObject = GetReports } },
            { "Name", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Name = value,
                GetString = () => Name } },
            { "Parents", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = ParseParents,
                SetArrayIsEmpty = () => RawParentSkillIsEmpty = true,
                GetStringArray = GetParents,
                GetArrayIsEmpty = () => ParentSkillIsEmpty } },
            { "SkipBonusLevelsIfSkillUnlearned", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawSkipBonusLevelsIfSkillUnlearned = value,
                GetBool = () => RawSkipBonusLevelsIfSkillUnlearned } },
            { "AuxCombat", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawAuxCombat = value,
                GetBool = () => RawAuxCombat } },
            { "TSysCategories", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<SkillCategory>.ParseList(value, TSysCategoryList, errorInfo),
                GetStringArray = () => StringToEnumConversion<SkillCategory>.ToStringList(TSysCategoryList) } },
            { "RecipeIngredientKeywords", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<ItemKeyword>.ParseList(value, RecipeIngredientKeywordList, errorInfo),
                GetStringArray = () => StringToEnumConversion<ItemKeyword>.ToStringList(RecipeIngredientKeywordList) } },
        }; } }

        private void ParseAdvancementTable(string value, ParseErrorInfo errorInfo)
        {
            RawAdvancementTable = value;

            if (value == null)
                IsAdvancementTableNull = true;
        }

        private void ParseInteractionFlagLevelCaps(JsonObject InteractionFlagLevelCaps, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> RawEntry in InteractionFlagLevelCaps)
            {
                JsonInteger AsJValue;
                if ((AsJValue = RawEntry.Value as JsonInteger) == null)
                {
                    ErrorInfo.AddInvalidObjectFormat("Skill InteractionFlagLevelCaps");
                    break;
                }

                string RawInteraction = RawEntry.Key;

                if (RawInteraction.Length > 0 && Char.IsDigit(RawInteraction[RawInteraction.Length - 1]))
                {
                    int FirstDigitIndex = RawInteraction.Length - 1;
                    while (FirstDigitIndex > 0 && Char.IsDigit(RawInteraction[FirstDigitIndex - 1]))
                        FirstDigitIndex--;

                    if (FirstDigitIndex > 0 && RawInteraction[FirstDigitIndex - 1] != '_' && RawInteraction[FirstDigitIndex - 1] != ' ')
                        RawInteraction = RawInteraction.Substring(0, FirstDigitIndex) + "_" + RawInteraction.Substring(FirstDigitIndex);
                }


                string[] Split = RawInteraction.Split('_');
                if (Split.Length < 3 || Split[0] != "LevelCap")
                {
                    ErrorInfo.AddInvalidString("Skill InteractionFlagLevelCaps", RawInteraction);
                    break;
                }

                string MergedSkill = "";
                int i;
                for (i = 1; i + 1 < Split.Length; i++)
                {
                    if (MergedSkill.Length > 0)
                        MergedSkill += "_";
                    MergedSkill += Split[i];
                }

                if (!StringToEnumConversion<PowerSkill>.TryParse(MergedSkill, out PowerSkill ConvertedPowerSkill, ErrorInfo))
                    break;

                int OtherLevel;
                if (!int.TryParse(Split[i], out OtherLevel) || OtherLevel <= 0)
                {
                    ErrorInfo.AddInvalidString("Skill InteractionFlagLevelCaps", RawInteraction);
                    break;
                }

                int Level = AsJValue.Number;
                LevelCapInteraction NewInteraction = new LevelCapInteraction(ConvertedPowerSkill, OtherLevel, Level);
                InteractionFlagLevelCapList.Add(NewInteraction);
            }
        }

        private IObjectContentGenerator GetInteractionFlagLevelCaps()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("InteractionFlagLevelCaps");

            foreach (LevelCapInteraction Interaction in InteractionFlagLevelCapList)
            {
                PowerSkill Skill = Interaction.Link.CombatSkill;
                string FieldKey = "LevelCap_" + StringToEnumConversion<PowerSkill>.ToString(Skill);

                switch (Skill)
                {
                    case PowerSkill.Performance_Strings:
                    case PowerSkill.Performance_Percussion:
                    case PowerSkill.Performance_Wind:
                        break;

                    default:
                        FieldKey += "_";
                        break;
                }

                FieldKey += Interaction.OtherLevel;

                int FieldValue = Interaction.Level;

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private void ParseAdvancementHints(JsonObject RawAdvancementHints, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> RawEntry in RawAdvancementHints)
            {
                JsonString AsJsonString;
                if ((AsJsonString = RawEntry.Value as JsonString) != null)
                {
                    string Description = AsJsonString.String;

                    int Level;
                    if (int.TryParse(RawEntry.Key, out Level) && Level > 0)
                    {
                        AdvancementHintTableKey.Add(Level);
                        AdvancementHintTableValue.Add(Description);
                    }
                    else
                        ErrorInfo.AddInvalidString("Skill AdvancementHints", RawEntry.Key);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill AdvancementHints");
            }
        }

        private IObjectContentGenerator GetAdvancementHints()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("AdvancementHints");

            for (int i = 0; i < AdvancementHintTableKey.Count; i++)
            {
                string FieldKey = AdvancementHintTableKey[i].ToString();
                string FieldValue = AdvancementHintTableValue[i];

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private void ParseRewards(JsonObject RawRewards, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> Entry in RawRewards)
            {
                JsonObject RawValue;
                if ((RawValue = Entry.Value as JsonObject) != null)
                {
                    Reward Subitem;
                    JsonObjectParser<Reward>.InitAsSubitem(Entry.Key, RawValue, out Subitem, ErrorInfo);
                    RewardList.Add(Subitem);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill Rewards");
            }

            EmptyRewardList = ((RewardList as IList<IPgReward>).Count == 0);
        }

        private IObjectContentGenerator GetRewards()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Rewards");

            foreach (Reward Reward in RewardList)
            {
                string FieldKey = Reward.Key;
                IObjectContentGenerator FieldValue = Reward;

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private void ParseReports(JsonObject RawReports, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, IJsonValue> RawEntry in RawReports)
            {
                JsonString AsJsonString;
                if ((AsJsonString = RawEntry.Value as JsonString) != null)
                {
                    string RawValue = AsJsonString.String;

                    int KeyLevel;
                    if (int.TryParse(RawEntry.Key, out KeyLevel) || KeyLevel <= 0)
                    {
                        ReportTableKey.Add(KeyLevel);
                        ReportTableValue.Add(RawValue);
                    }
                    else
                        ErrorInfo.AddInvalidObjectFormat("Skill Reports");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill Reports");
            }
        }

        private IObjectContentGenerator GetReports()
        {
            CustomObject Result = new CustomObject();
            Result.SetCustomKey("Reports");

            for (int i = 0; i < ReportTableKey.Count; i++)
            {
                string FieldKey = ReportTableKey[i].ToString();
                string FieldValue = ReportTableValue[i];

                Result.SetFieldValue(FieldKey, FieldValue);
            }

            return Result;
        }

        private void ParseParents(string RawParents, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<PowerSkill>.TryParse(RawParents, out PowerSkill ParsedParent, ErrorInfo))
            {
                if (RawParentSkill == PowerSkill.Internal_None)
                    RawParentSkill = ParsedParent;
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill Parents");
            }
        }

        private List<string> GetParents()
        {
            List<string> Result = new List<string>();

            if (ParentSkill != null)
                Result.Add(StringToEnumConversion<PowerSkill>.ToString(ParentSkill.CombatSkill, null, PowerSkill.Internal_None));

            return Result;
        }
/*
        private void ParseTSysCategories(string RawTSysCategories, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<SkillCategory>.TryParse(RawTSysCategories, out SkillCategory ParsedTSysCategory, ErrorInfo))
                TSysCategoryList.Add(ParsedTSysCategory);
        }*/
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Name);
                AddWithFieldSeparator(ref Result, Description);
                if (RawHideWhenZero.HasValue)
                    AddWithFieldSeparator(ref Result, "Hide When Zero");
                if (XpTable != null)
                    AddWithFieldSeparator(ref Result, XpTable.InternalName);
                if (AdvancementTable != null)
                    AddWithFieldSeparator(ref Result, AdvancementTable.InternalName);
                if (RawCombat.HasValue)
                    AddWithFieldSeparator(ref Result, "Combat");
                foreach (PowerSkill Item in CompatibleCombatSkillList)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[Item]);
                if (RawParentSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[RawParentSkill]);
                if (RawSkipBonusLevelsIfSkillUnlearned.HasValue)
                    AddWithFieldSeparator(ref Result, "Skip Bonus Levels If Skill Unlearned");
                if (RawAuxCombat.HasValue)
                    AddWithFieldSeparator(ref Result, "Is Auxiliary Combat Skill");
                foreach (SkillRewardCommon Item in CombinedRewardList)
                    AddWithFieldSeparator(ref Result, Item.TextContent);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IJsonKey> XpTableTable = AllTables[typeof(XpTable)];
            Dictionary<string, IJsonKey> AdvancementTableTable = AllTables[typeof(AdvancementTable)];

            XpTable = PgJsonObjects.XpTable.ConnectSingleProperty(ErrorInfo, XpTableTable, RawXpTable, XpTable, ref IsRawXpTableParsed, ref IsConnected, this);
            AdvancementTable = PgJsonObjects.AdvancementTable.ConnectSingleProperty(ErrorInfo, AdvancementTableTable, RawAdvancementTable, AdvancementTable, ref IsRawAdvancementTableParsed, ref IsConnected, this);

            foreach (LevelCapInteraction Interaction in InteractionFlagLevelCapList)
                if (!Interaction.IsParsed)
                {
                    IsConnected = true;

                    bool IsSkillParsed = false;
                    IPgSkill SkillLink = ConnectPowerSkill(ErrorInfo, SkillTable, Interaction.OtherSkill, Interaction.Link, ref IsSkillParsed, ref IsConnected, this);

                    Interaction.SetLink(SkillLink);
                }

            foreach (Reward Item in RewardList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            if (RawParentSkill != PowerSkill.Internal_None && RawParentSkill != PowerSkill.AnySkill && RawParentSkill != PowerSkill.Unknown)
                ParentSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawParentSkill, ParentSkill, ref IsParentSkillParsed, ref IsConnected, this);

            if (CombinedRewardList == null)
                CombinedRewardList = CreateCombinedRewardList(InteractionFlagLevelCapList, AdvancementHintTableKey, AdvancementHintTableValue, RewardList, ReportTableKey, ReportTableValue);

            return IsConnected;
        }

        public static List<SkillRewardCommon> CreateCombinedRewardList(IPgLevelCapInteractionCollection InteractionFlagLevelCapList, List<int> AdvancementHintTableKey, List<string> AdvancementHintTableValue, IPgRewardCollection RewardList, List<int> ReportTableKey, List<string> ReportTableValue)
        {
            List<SkillRewardCommon> CombinedRewardList = new List<SkillRewardCommon>();

            foreach (IPgLevelCapInteraction Item in InteractionFlagLevelCapList)
                CombinedRewardList.Add(new SkillRewardUnlock(Item.Level, Item.Link, Item.OtherLevel));

            for (int i = 0; i < AdvancementHintTableKey.Count; i++)
                CombinedRewardList.Add(new SkillRewardMisc(AdvancementHintTableKey[i], new List<Race>(), AdvancementHintTableValue[i]));

            foreach (IPgReward Item in RewardList)
            {
                if (Item.Ability != null)
                    CombinedRewardList.Add(new SkillRewardAbility(Item.RewardLevel, Item.RaceRestrictionList, Item.Ability));
                if (Item.BonusSkill != null)
                    CombinedRewardList.Add(new SkillRewardBonusLevel(Item.RewardLevel, Item.RaceRestrictionList, Item.BonusSkill));
                if (Item.Recipe != null)
                    CombinedRewardList.Add(new SkillRewardRecipe(Item.RewardLevel, Item.RaceRestrictionList, Item.Recipe));
                if (Item.Notes != null)
                    CombinedRewardList.Add(new SkillRewardMisc(Item.RewardLevel, Item.RaceRestrictionList, Item.Notes));
            }

            for (int i = 0; i < ReportTableKey.Count; i++)
                CombinedRewardList.Add(new SkillRewardMisc(ReportTableKey[i], new List<Race>(), ReportTableValue[i]));

            CombinedRewardList.Sort(SortByLevel);

            return CombinedRewardList;
        }

        public static IPgSkill ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> SkillTable, string RawSkillName, IPgSkill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            if (RawSkillName == null)
                return null;

            foreach (KeyValuePair<string, IJsonKey> Entry in SkillTable)
            {
                Skill SkillValue = Entry.Value as Skill;
                if (Entry.Key == RawSkillName)
                {
                    IsConnected = true;
                    SkillValue.AddLinkBack(LinkBack);
                    return SkillValue;
                }
            }

            ErrorInfo.AddMissingKey(RawSkillName);
            return null;
        }

        public static IPgSkill ConnectPowerSkill(ParseErrorInfo ErrorInfo, Dictionary<string, IJsonKey> SkillTable, PowerSkill RawPowerSkill, IPgSkill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, IBackLinkable LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            foreach (KeyValuePair<string, IJsonKey> Entry in SkillTable)
            {
                Skill SkillValue = Entry.Value as Skill;
                if (SkillValue.CombatSkill == RawPowerSkill)
                {
                    IsConnected = true;
                    SkillValue.AddLinkBack(LinkBack);
                    return SkillValue;
                }
            }

            if (RawPowerSkill != PowerSkill.Internal_None && RawPowerSkill != PowerSkill.Unknown)
                ErrorInfo.AddMissingKey(RawPowerSkill.ToString());
            else
                return null;

            return null;
        }

        private static int SortByLevel(SkillRewardCommon reward1, SkillRewardCommon reward2)
        {
            if (reward1.RewardLevel > reward2.RewardLevel)
                return 1;
            else if (reward1.RewardLevel < reward2.RewardLevel)
                return -1;
            else
                return 0;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Skill"; } }

        public override string ToString()
        {
            return Key;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BitOffset = 0;
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();
            Dictionary<int, List<int>> StoredIntListTable = new Dictionary<int, List<int>>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();
            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddEnum(CombatSkill, data, ref offset, BaseOffset, 4);
            AddBool(RawHideWhenZero, data, ref offset, ref BitOffset, BaseOffset, 6, 0);
            AddBool(RawCombat, data, ref offset, ref BitOffset, BaseOffset, 6, 2);
            AddBool(RawSkipBonusLevelsIfSkillUnlearned, data, ref offset, ref BitOffset, BaseOffset, 6, 4);
            AddBool(RawAuxCombat, data, ref offset, ref BitOffset, BaseOffset, 6, 6);
            AddBool(RawParentSkillIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 6, 8);
            AddBool(IsAdvancementTableNull, data, ref offset, ref BitOffset, BaseOffset, 6, 10);
            CloseBool(ref offset, ref BitOffset);
            AddInt(RawId, data, ref offset, BaseOffset, 8);
            AddString(Description, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddObject(XpTable as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddString(RawXpTable, data, ref offset, BaseOffset, 20, StoredStringtable);
            AddObject(AdvancementTable as ISerializableJsonObject, data, ref offset, BaseOffset, 24, StoredObjectTable);
            AddEnumList(CompatibleCombatSkillList, data, ref offset, BaseOffset, 28, StoredEnumListTable);
            AddInt(RawMaxBonusLevels, data, ref offset, BaseOffset, 32);
            AddObjectList(InteractionFlagLevelCapList, data, ref offset, BaseOffset, 36, StoredObjectListTable);
            AddObjectList(RewardList, data, ref offset, BaseOffset, 40, StoredObjectListTable);
            AddString(Name, data, ref offset, BaseOffset, 44, StoredStringtable);
            AddObject(ParentSkill as ISerializableJsonObject, data, ref offset, BaseOffset, 48, StoredObjectTable);
            AddEnumList(TSysCategoryList, data, ref offset, BaseOffset, 52, StoredEnumListTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 56, StoredStringListTable);
            AddInt(IconId, data, ref offset, BaseOffset, 60);
            AddEnumList(RecipeIngredientKeywordList, data, ref offset, BaseOffset, 64, StoredEnumListTable);
            AddIntList(AdvancementHintTableKey, data, ref offset, BaseOffset, 68, StoredIntListTable);
            AddStringList(AdvancementHintTableValue, data, ref offset, BaseOffset, 72, StoredStringListTable);
            AddIntList(ReportTableKey, data, ref offset, BaseOffset, 76, StoredIntListTable);
            AddStringList(ReportTableValue, data, ref offset, BaseOffset, 80, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 84, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, StoredIntListTable, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
