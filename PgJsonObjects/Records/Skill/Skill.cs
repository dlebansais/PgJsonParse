using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Skill : GenericJsonObject<Skill>
    {
        #region Direct Properties
        public PowerSkill CombatSkill { get; private set; }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        private int? RawId;
        public string Description { get; private set; }
        public bool HideWhenZero { get { return RawHideWhenZero.HasValue && RawHideWhenZero.Value; } }
        public bool? RawHideWhenZero { get; private set; }
        public XpTable XpTable { get; private set; }
        private string RawXpTable;
        private bool IsRawXpTableParsed;
        public AdvancementTable AdvancementTable { get; private set; }
        private string RawAdvancementTable;
        private bool IsRawAdvancementTableParsed;
        private bool IsRawAdvancementTableEmpty;
        public bool Combat { get { return RawCombat.HasValue && RawCombat.Value; } }
        public bool? RawCombat { get; private set; }
        public List<PowerSkill> CompatibleCombatSkillList { get; } = new List<PowerSkill>();
        public int MaxBonusLevels { get { return RawMaxBonusLevels.HasValue ? RawMaxBonusLevels.Value : 0; } }
        public int? RawMaxBonusLevels { get; private set; }
        public List<LevelCapInteraction> InteractionFlagLevelCapList { get; } = new List<LevelCapInteraction>();
        public Dictionary<int, string> AdvancementHintTable { get; } = new Dictionary<int, string>();
        public List<Reward> RewardList { get; } = new List<Reward>();
        private bool EmptyRewardList;
        public Dictionary<int, string> ReportTable { get; } = new Dictionary<int, string>();
        public List<SkillRewardCommon> CombinedRewardList { get; private set; }
        public string Name { get; private set; }
        public PowerSkill ParentSkill { get; private set; }
        public Skill ConnectedParentSkill { get; private set; }
        private bool IsParentSkillParsed;
        public bool EmptyParentList { get; private set; }
        public bool SkipBonusLevelsIfSkillUnlearned { get { return RawSkipBonusLevelsIfSkillUnlearned.HasValue && RawSkipBonusLevelsIfSkillUnlearned.Value; } }
        public bool? RawSkipBonusLevelsIfSkillUnlearned { get; private set; }
        public bool AuxCombat { get { return RawAuxCombat.HasValue && RawAuxCombat.Value; } }
        public bool? RawAuxCombat { get; private set; }
        public List<SkillCategory> TSysCategoryList { get; } = new List<SkillCategory>();
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
        protected override string SortingName { get { return Name; } }
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

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
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
                if (BasicAttackTable.ContainsKey(ParentSkill))
                    IconSkill = ParentSkill;
            }

            if (!BasicAttackTable.ContainsKey(IconSkill) && !AnyIconTable.ContainsKey(IconSkill))
            {
                if (ConnectedParentSkill != null && (ConnectedParentSkill.IconId != 0 || HardcodedSkillIconTable.ContainsKey(ParentSkill)))
                    IconSkill = ParentSkill;

                else
                {
                    foreach (SkillRewardCommon Reward in CombinedRewardList)
                    {
                        SkillRewardBonusLevel AsBonusLevelReward;
                        if ((AsBonusLevelReward = Reward as SkillRewardBonusLevel) != null)
                            if (AnyIconTable.ContainsKey(AsBonusLevelReward.Skill.CombatSkill) && AsBonusLevelReward.Skill.IconId != 0)
                            {
                                AnyIconTable.Add(IconSkill, AsBonusLevelReward.Skill.IconId);
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
        protected override void InitializeKey(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            PowerSkill ParsedPowerSkill;
            StringToEnumConversion<PowerSkill>.TryParse(Key, out ParsedPowerSkill, ErrorInfo);
            CombatSkill = ParsedPowerSkill;
        }

        public override void Init(KeyValuePair<string, IJsonValue> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.Init(EntryRaw, ErrorInfo);

            if (Name == null)
                Name = Key;
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Id", new FieldParser() { Type = FieldType.Integer, ParseInteger = (int value, ParseErrorInfo errorInfo) => { RawId = value; }} },
            { "Description", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { Description = value; }} },
            { "HideWhenZero", new FieldParser() { Type = FieldType.Bool, ParseBool = (bool value, ParseErrorInfo errorInfo) => { RawHideWhenZero = value; }} },
            { "XpTable", new FieldParser() { Type = FieldType.String, ParseString = ParseXpTable } },
            { "AdvancementTable", new FieldParser() { Type = FieldType.String, ParseString = ParseAdvancementTable } },
            { "Combat", new FieldParser() { Type = FieldType.Bool, ParseBool = (bool value, ParseErrorInfo errorInfo) => { RawCombat = value; }} },
            { "CompatibleCombatSkills", new FieldParser() { Type = FieldType.SimpleStringArray, ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { StringToEnumConversion<PowerSkill>.ParseList(value, CompatibleCombatSkillList, errorInfo); }} },
            { "MaxBonusLevels", new FieldParser() { Type = FieldType.Integer, ParseInteger = (int value, ParseErrorInfo errorInfo) => { RawMaxBonusLevels = value; }} },
            { "InteractionFlagLevelCaps", new FieldParser() { Type = FieldType.Object, ParseObject = ParseInteractionFlagLevelCaps } },
            { "AdvancementHints", new FieldParser() { Type = FieldType.Object, ParseObject = ParseAdvancementHints } },
            { "Rewards", new FieldParser() { Type = FieldType.Object, ParseObject = ParseRewards } },
            { "Reports", new FieldParser() { Type = FieldType.Object, ParseObject = ParseReports } },
            { "Name", new FieldParser() { Type = FieldType.String, ParseString = (string value, ParseErrorInfo errorInfo) => { Name = value; }} },
            { "Parents", new FieldParser() { Type = FieldType.SimpleStringArray, ParseSimpleStringArray = ParseParents } },
            { "SkipBonusLevelsIfSkillUnlearned", new FieldParser() { Type = FieldType.Bool, ParseBool = (bool value, ParseErrorInfo errorInfo) => { RawSkipBonusLevelsIfSkillUnlearned = value; }} },
            { "AuxCombat", new FieldParser() { Type = FieldType.Bool, ParseBool = (bool value, ParseErrorInfo errorInfo) => { RawAuxCombat = value; }} },
            { "TSysCategories", new FieldParser() { Type = FieldType.SimpleStringArray, ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => { StringToEnumConversion<SkillCategory>.ParseList(value, TSysCategoryList, errorInfo); }} },
        }; } }

        private void ParseXpTable(string RawXpTable, ParseErrorInfo ErrorInfo)
        {
            this.RawXpTable = RawXpTable;
            XpTable = null;
            IsRawXpTableParsed = false;
        }

        private void ParseAdvancementTable(string RawAdvancementTable, ParseErrorInfo ErrorInfo)
        {
            this.RawAdvancementTable = RawAdvancementTable;
            AdvancementTable = null;
            IsRawAdvancementTableEmpty = false;
            IsRawAdvancementTableParsed = false;
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
                        AdvancementHintTable.Add(Level, Description);
                    else
                        ErrorInfo.AddInvalidString("Skill AdvancementHints", RawEntry.Key);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill AdvancementHints");
            }
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

            EmptyRewardList = (RewardList.Count == 0);
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
                        ReportTable.Add(KeyLevel, RawValue);
                    else
                        ErrorInfo.AddInvalidObjectFormat("Skill Reports");
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill Reports");
            }
        }

        private void ParseParents(string RawParents, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<PowerSkill>.TryParse(RawParents, out PowerSkill ParsedParent, ErrorInfo))
            {
                if (ParentSkill == PowerSkill.Internal_None)
                    ParentSkill = ParsedParent;
                else
                    ErrorInfo.AddInvalidObjectFormat("Skill Parents");
            }
        }

        private void ParseTSysCategories(string RawTSysCategories, ParseErrorInfo ErrorInfo)
        {
            if (StringToEnumConversion<SkillCategory>.TryParse(RawTSysCategories, out SkillCategory ParsedTSysCategory, ErrorInfo))
                TSysCategoryList.Add(ParsedTSysCategory);
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddInteger("Id", RawId);
            Generator.AddString("Name", Name);
            Generator.AddString("Description", Description);
            Generator.AddBoolean("HideWhenZero", RawHideWhenZero);
            //Generator.AddString("XpTable", XpTable.ToString());

            if (IsRawAdvancementTableEmpty)
                Generator.AddNull("AdvancementTable");
            else if (AdvancementTable != null)
                Generator.AddString("AdvancementTable", AdvancementTable.ToString());

            if (TSysCategoryList.Count > 0)
            {
                Generator.OpenArray("TSysCategories");

                foreach (SkillCategory TSysCategory in TSysCategoryList)
                    Generator.AddString(null, TSysCategory.ToString());

                Generator.CloseArray();
            }
            
            if (CompatibleCombatSkillList.Count > 0)
            {
                Generator.OpenArray("CompatibleCombatSkills");

                foreach (PowerSkill Item in CompatibleCombatSkillList)
                    Generator.AddString(null, Item.ToString());

                Generator.CloseArray();
            }

            Generator.AddInteger("MaxBonusLevels", RawMaxBonusLevels);

            if (ParentSkill != PowerSkill.Internal_None)
            {
                Generator.OpenArray("Parents");
                Generator.AddString(null, ParentSkill.ToString());
                Generator.CloseArray();
            }
            else if (EmptyParentList)
                Generator.AddEmptyArray("Parents");

            Generator.AddBoolean("Combat", RawCombat);
            Generator.AddBoolean("AuxCombat", RawAuxCombat);
            Generator.AddBoolean("SkipBonusLevelsIfSkillUnlearned", RawSkipBonusLevelsIfSkillUnlearned);

            if (InteractionFlagLevelCapList.Count > 0)
            {
                Generator.OpenObject("InteractionFlagLevelCaps");

                foreach (LevelCapInteraction Interaction in InteractionFlagLevelCapList)
                    Generator.AddInteger("LevelCap_" + Interaction.OtherSkill.ToString() + "_" + Interaction.OtherLevel.ToString(), Interaction.Level);

                Generator.CloseObject();
            }

            if (AdvancementHintTable.Count > 0)
            {
                Generator.OpenObject("AdvancementHints");

                foreach (KeyValuePair<int, string> Entry in AdvancementHintTable)
                    Generator.AddString(Entry.Key.ToString(), Entry.Value);

                Generator.CloseObject();
            }

            if (RewardList.Count > 0 || EmptyRewardList)
            {
                Generator.OpenObject("Rewards");

                foreach (Reward Item in RewardList)
                    Item.GenerateObjectContent(Generator);

                Generator.CloseObject();
            }

            if (ReportTable.Count > 0)
            {
                Generator.OpenObject("Reports");

                foreach (KeyValuePair<int, string> Entry in ReportTable)
                    Generator.AddString(Entry.Key.ToString(), Entry.Value);

                Generator.CloseObject();
            }

            Generator.CloseObject();
        }
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
                if (ParentSkill != PowerSkill.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.PowerSkillTextMap[ParentSkill]);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> XpTableTable = AllTables[typeof(XpTable)];
            Dictionary<string, IGenericJsonObject> AdvancementTableTable = AllTables[typeof(AdvancementTable)];

            XpTable = PgJsonObjects.XpTable.ConnectSingleProperty(ErrorInfo, XpTableTable, RawXpTable, XpTable, ref IsRawXpTableParsed, ref IsConnected, this);
            AdvancementTable = PgJsonObjects.AdvancementTable.ConnectSingleProperty(ErrorInfo, AdvancementTableTable, RawAdvancementTable, AdvancementTable, ref IsRawAdvancementTableParsed, ref IsConnected, this);

            foreach (LevelCapInteraction Interaction in InteractionFlagLevelCapList)
                if (!Interaction.IsParsed)
                {
                    IsConnected = true;

                    bool IsSkillParsed = false;
                    Skill SkillLink = ConnectPowerSkill(ErrorInfo, SkillTable, Interaction.OtherSkill, Interaction.Link, ref IsSkillParsed, ref IsConnected, this);

                    Interaction.SetLink(SkillLink);
                }

            foreach (Reward Item in RewardList)
                IsConnected |= Item.Connect(ErrorInfo, this, AllTables);

            if (ParentSkill != PowerSkill.Internal_None && ParentSkill != PowerSkill.AnySkill && ParentSkill != PowerSkill.Unknown)
                ConnectedParentSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, ParentSkill, ConnectedParentSkill, ref IsParentSkillParsed, ref IsConnected, this);

            if (CombinedRewardList == null)
            {
                CombinedRewardList = new List<SkillRewardCommon>();

                foreach (LevelCapInteraction Item in InteractionFlagLevelCapList)
                    CombinedRewardList.Add(new SkillRewardUnlock(Item.Level, Item.Link, Item.OtherLevel));

                foreach (KeyValuePair<int, string> Entry in AdvancementHintTable)
                    CombinedRewardList.Add(new SkillRewardMisc(Entry.Key, new List<Race>(), Entry.Value));

                foreach (Reward Item in RewardList)
                {
                    if (Item.Ability != null)
                        CombinedRewardList.Add(new SkillRewardAbility(Item.RewardLevel, Item.RaceRestrictionList, Item.Ability));
                    if (Item.ConnectedBonusSkill != null)
                        CombinedRewardList.Add(new SkillRewardBonusLevel(Item.RewardLevel, Item.RaceRestrictionList, Item.ConnectedBonusSkill));
                    if (Item.Recipe != null)
                        CombinedRewardList.Add(new SkillRewardRecipe(Item.RewardLevel, Item.RaceRestrictionList, Item.Recipe));
                    if (Item.Notes != null)
                        CombinedRewardList.Add(new SkillRewardMisc(Item.RewardLevel, Item.RaceRestrictionList, Item.Notes));
                }

                foreach (KeyValuePair<int, string> Entry in ReportTable)
                    CombinedRewardList.Add(new SkillRewardMisc(Entry.Key, new List<Race>(), Entry.Value));

                CombinedRewardList.Sort(SortByLevel);
            }

            return IsConnected;
        }

        public static Skill ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> SkillTable, string RawSkillName, Skill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            if (RawSkillName == null)
                return null;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in SkillTable)
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

        public static Skill ConnectPowerSkill(ParseErrorInfo ErrorInfo, Dictionary<string, IGenericJsonObject> SkillTable, PowerSkill RawPowerSkill, Skill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            foreach (KeyValuePair<string, IGenericJsonObject> Entry in SkillTable)
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
    }
}
