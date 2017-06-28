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
        public static Dictionary<PowerSkill, Ability> BasicAttackTable { get; private set; }
        public static Dictionary<PowerSkill, int> AnyIconTable { get; private set; }

        protected override string SortingName { get { return Name; } }

        public string CombinedCompatibleSkills
        {
            get
            {
                string Result = "";

                foreach (PowerSkill Item in CompatibleCombatSkillList)
                {
                    if (Result.Length > 0)
                        Result += ", ";

                    Result += TextMaps.PowerSkillTextMap[Item];
                }

                return Result;
            }
        }

        public string CombinedParentSkill
        {
            get
            {
                if (ConnectedParentSkill == null)
                    return TextMaps.PowerSkillTextMap[ParentSkill];
                else
                    return ConnectedParentSkill.Name;
            }
        }

        public string SearchResultIconFileName
        {
            get
            {
                PowerSkill IconSkill = CombatSkill;

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

                int IconId = BestIconIdForSkill(IconSkill);

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

            return IconId;
        }

        /*private static string PrepareReward(int Level, string s)
        {
            return "Level " + Level + ": " + s;
        }*/
        #endregion

        #region Init
        static Skill()
        {
            BasicAttackTable = new Dictionary<PowerSkill, Ability>();
            AnyIconTable = new Dictionary<PowerSkill, int>();
        }
        #endregion

        #region Parsing
        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw)
        {
            base.InitializeKey(EntryRaw);

            PowerSkill ParsedPowerSkill;
            StringToEnumConversion<PowerSkill>.TryParse(Key, out ParsedPowerSkill, null);
            CombatSkill = ParsedPowerSkill;
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Id", ParseFieldId },
            { "Description", ParseFieldDescription },
            { "HideWhenZero", ParseFieldHideWhenZero },
            { "XpTable", ParseFieldXpTable },
            { "AdvancementTable", ParseFieldAdvancementTable },
            { "Combat", ParseFieldCombat },
            { "CompatibleCombatSkills", ParseFieldCompatibleCombatSkills },
            { "MaxBonusLevels", ParseFieldMaxBonusLevels },
            { "InteractionFlagLevelCaps", ParseFieldInteractionFlagLevelCaps },
            { "AdvancementHints", ParseFieldAdvancementHints },
            { "Rewards", ParseFieldRewards },
            { "Reports", ParseFieldReports },
            { "Name", ParseFieldName },
            { "Parents", ParseFieldParents },
            { "SkipBonusLevelsIfSkillUnlearned", ParseFieldSkipBonusLevelsIfSkillUnlearned },
            { "AuxCombat", ParseFieldAuxCombat },
            { "TSysCategories", ParseFieldTSysCategories },
        };

        public override void Init(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.Init(EntryRaw, ErrorInfo);

            if (Name == null)
                Name = Key;
        }

        private static void ParseFieldId(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Id");
        }

        private void ParseId(int RawId, ParseErrorInfo ErrorInfo)
        {
            this.RawId = RawId;
        }

        private static void ParseFieldDescription(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldHideWhenZero(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseHideWhenZero((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill HideWhenZero");
        }

        private void ParseHideWhenZero(bool RawHideWhenZero, ParseErrorInfo ErrorInfo)
        {
            this.RawHideWhenZero = RawHideWhenZero;
        }

        private static void ParseFieldXpTable(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawXpTable;
            if ((RawXpTable = Value as string) != null)
                This.ParseXpTable(RawXpTable, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill XpTable");
        }

        private void ParseXpTable(string RawXpTable, ParseErrorInfo ErrorInfo)
        {
            this.RawXpTable = RawXpTable;
            XpTable = null;
            IsRawXpTableParsed = false;
        }

        private static void ParseFieldAdvancementTable(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAdvancementTable;

            if ((RawAdvancementTable = Value as string) != null)
                This.ParseAdvancementTable(RawAdvancementTable, ErrorInfo);
            else
                This.IsRawAdvancementTableEmpty = true;
        }

        private void ParseAdvancementTable(string RawAdvancementTable, ParseErrorInfo ErrorInfo)
        {
            this.RawAdvancementTable = RawAdvancementTable;
            AdvancementTable = null;
            IsRawAdvancementTableEmpty = false;
            IsRawAdvancementTableParsed = false;
        }

        private static void ParseFieldCombat(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseCombat((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Combat");
        }

        private void ParseCombat(bool RawCombat, ParseErrorInfo ErrorInfo)
        {
            this.RawCombat = RawCombat;
        }

        private static void ParseFieldCompatibleCombatSkills(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawCompatibleCombatSkills;
            if ((RawCompatibleCombatSkills = Value as ArrayList) != null)
                This.ParseCompatibleCombatSkills(RawCompatibleCombatSkills, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill CompatibleCombatSkills");
        }

        private void ParseCompatibleCombatSkills(ArrayList RawCompatibleCombatSkills, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<PowerSkill>.ParseList(RawCompatibleCombatSkills, CompatibleCombatSkillList, ErrorInfo);
        }

        private static void ParseFieldMaxBonusLevels(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseMaxBonusLevels((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill MaxBonusLevels");
        }

        private void ParseMaxBonusLevels(int RawMaxBonusLevels, ParseErrorInfo ErrorInfo)
        {
            this.RawMaxBonusLevels = RawMaxBonusLevels;
        }

        private static void ParseFieldInteractionFlagLevelCaps(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawInteractionFlagLevelCaps;
            if ((RawInteractionFlagLevelCaps = Value as Dictionary<string, object>) != null)
                This.ParseInteractionFlagLevelCaps(RawInteractionFlagLevelCaps, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill InteractionFlagLevelCaps");
        }

        private void ParseInteractionFlagLevelCaps(Dictionary<string, object> InteractionFlagLevelCaps, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> RawEntry in InteractionFlagLevelCaps)
            {
                if (!(RawEntry.Value is int))
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

                PowerSkill ConvertedPowerSkill;
                if (!StringToEnumConversion<PowerSkill>.TryParse(MergedSkill, out ConvertedPowerSkill, ErrorInfo))
                    break;

                int OtherLevel;
                if (!int.TryParse(Split[i], out OtherLevel) || OtherLevel <= 0)
                {
                    ErrorInfo.AddInvalidString("Skill InteractionFlagLevelCaps", RawInteraction);
                    break;
                }

                int Level = (int)RawEntry.Value;
                LevelCapInteraction NewInteraction = new LevelCapInteraction(ConvertedPowerSkill, OtherLevel, Level);
                InteractionFlagLevelCapList.Add(NewInteraction);
            }
        }

        private static void ParseFieldAdvancementHints(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawAdvancementHints;
            if ((RawAdvancementHints = Value as Dictionary<string, object>) != null)
                This.ParseAdvancementHints(RawAdvancementHints, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill AdvancementHints");
        }

        private void ParseAdvancementHints(Dictionary<string, object> RawAdvancementHints, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> RawEntry in RawAdvancementHints)
            {
                string Description;
                if ((Description = RawEntry.Value as string) != null)
                {
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

        private static void ParseFieldRewards(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawRewards;
            if ((RawRewards = Value as Dictionary<string, object>) != null)
                This.ParseRewards(RawRewards, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Rewards");
        }

        private void ParseRewards(Dictionary<string, object> RawRewards, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> Entry in RawRewards)
            {
                Dictionary<string, object> RawValue;
                if ((RawValue = Entry.Value as Dictionary<string, object>) != null)
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

        private static void ParseFieldReports(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawReports;
            if ((RawReports = Value as Dictionary<string, object>) != null)
                This.ParseReports(RawReports, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Reports");
        }

        private void ParseReports(Dictionary<string, object> RawReports, ParseErrorInfo ErrorInfo)
        {
            foreach (KeyValuePair<string, object> RawEntry in RawReports)
            {
                string RawValue;
                if ((RawValue = RawEntry.Value as string) != null)
                {
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

        private static void ParseFieldName(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldParents(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawParents;
            if ((RawParents = Value as ArrayList) != null)
                This.ParseParents(RawParents, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill Parents");
        }

        private void ParseParents(ArrayList RawParents, ParseErrorInfo ErrorInfo)
        {
            List<PowerSkill> ParsedParentList = new List<PowerSkill>();
            StringToEnumConversion<PowerSkill>.ParseList(RawParents, ParsedParentList, ErrorInfo);

            if (ParsedParentList.Count == 0)
                EmptyParentList = true;

            else if (ParsedParentList.Count == 1)
            {
                EmptyParentList = false;
                ParentSkill = ParsedParentList[0];
            }

            else
                ErrorInfo.AddInvalidObjectFormat("Skill Parents");
        }

        private static void ParseFieldSkipBonusLevelsIfSkillUnlearned(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseSkipBonusLevelsIfSkillUnlearned((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill SkipBonusLevelsIfSkillUnlearned");
        }

        private void ParseSkipBonusLevelsIfSkillUnlearned(bool RawSkipBonusLevelsIfSkillUnlearned, ParseErrorInfo ErrorInfo)
        {
            this.RawSkipBonusLevelsIfSkillUnlearned = RawSkipBonusLevelsIfSkillUnlearned;
        }

        private static void ParseFieldAuxCombat(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseAuxCombat((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill AuxCombat");
        }

        private void ParseAuxCombat(bool RawAuxCombat, ParseErrorInfo ErrorInfo)
        {
            this.RawAuxCombat = RawAuxCombat;
        }

        private static void ParseFieldTSysCategories(Skill This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawTSysCategories;
            if ((RawTSysCategories = Value as ArrayList) != null)
                This.ParseTSysCategories(RawTSysCategories, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Skill TSysCategories");
        }

        private void ParseTSysCategories(ArrayList RawTSysCategories, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<SkillCategory>.ParseList(RawTSysCategories, TSysCategoryList, ErrorInfo);
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
            Generator.AddString("XpTable", XpTable.ToString());

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
                AddWithFieldSeparator(ref Result, CombinedCompatibleSkills);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

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
                IsConnected |= Item.Connect(ErrorInfo, this, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable, XpTableTable, AdvancementTableTable);

            if (ParentSkill != PowerSkill.Internal_None && ParentSkill != PowerSkill.AnySkill && ParentSkill != PowerSkill.Unknown)
                ConnectedParentSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, ParentSkill, ConnectedParentSkill, ref IsParentSkillParsed, ref IsConnected, this);

            if (CombinedRewardList == null)
            {
                CombinedRewardList = new List<SkillRewardCommon>();

                foreach (LevelCapInteraction Item in InteractionFlagLevelCapList)
                    CombinedRewardList.Add(new SkillRewardUnlock(Item.Level, Item.Link, Item.OtherLevel));

                foreach (KeyValuePair<int, string> Entry in AdvancementHintTable)
                    CombinedRewardList.Add(new SkillRewardMisc(Entry.Key, Entry.Value));

                foreach (Reward Item in RewardList)
                {
                    if (Item.Ability != null)
                        CombinedRewardList.Add(new SkillRewardAbility(Item.RewardLevel, Item.Ability));
                    if (Item.ConnectedBonusSkill != null)
                        CombinedRewardList.Add(new SkillRewardBonusLevel(Item.RewardLevel, Item.ConnectedBonusSkill));
                    if (Item.Recipe != null)
                        CombinedRewardList.Add(new SkillRewardRecipe(Item.RewardLevel, Item.Recipe));
                    if (Item.Notes != null)
                        CombinedRewardList.Add(new SkillRewardMisc(Item.RewardLevel, Item.Notes));
                }

                foreach (KeyValuePair<int, string> Entry in ReportTable)
                    CombinedRewardList.Add(new SkillRewardMisc(Entry.Key, Entry.Value));

                CombinedRewardList.Sort(SortByLevel);
            }

            return IsConnected;
        }

        public static Skill ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Skill> SkillTable, string RawSkillName, Skill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            if (RawSkillName == null)
                return null;

            foreach (KeyValuePair<string, Skill> Entry in SkillTable)
                if (Entry.Key == RawSkillName)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawSkillName);
            return null;
        }

        public static Skill ConnectPowerSkill(ParseErrorInfo ErrorInfo, Dictionary<string, Skill> SkillTable, PowerSkill RawPowerSkill, Skill ParsedSkill, ref bool IsRawSkillParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawSkillParsed)
                return ParsedSkill;

            IsRawSkillParsed = true;

            foreach (KeyValuePair<string, Skill> Entry in SkillTable)
                if (Entry.Value.CombatSkill == RawPowerSkill)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            if (RawPowerSkill != PowerSkill.Unknown)
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
