using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilitySource : GenericJsonObject<AbilitySource>
    {
        #region Direct Properties
        public Ability ConnectedAbility { get; private set; }
        private int? RawAbilityId;
        private bool IsAbilityIdParsed;
        public SourceTypes Type { get; private set; }
        public PowerSkill SkillTypeId { get; private set; }
        public Skill ConnectedSkillTypeId { get; private set; }
        private bool IsSkillTypeIdParsed;
        public Item ConnectedItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        private int RawItemTypeId;
        private string RawNpcName;
        private string RawEffectName;
        public Quest ConnectedQuest { get; private set; }
        private string RawQuestName;
        private int? RawQuestId;
        private bool IsQuestNameParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override void InitializeKey(KeyValuePair<string, object> EntryRaw, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(EntryRaw, ErrorInfo);

            int Index = Key.LastIndexOf('_');
            if (Index >= 0)
            {
                int ParsedAbilityId;
                if (int.TryParse(Key.Substring(Index + 1), out ParsedAbilityId))
                    RawAbilityId = ParsedAbilityId;
            }
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Type", ParseFieldType },
            { "SkillTypeId", ParseFieldSkillTypeId },
            { "ItemName", ParseFieldItemName },
            { "ItemTypeId", ParseFieldItemTypeId },
            { "NpcName", ParseFieldNpcName },
            { "EffectName", ParseFieldEffectName },
            { "QuestName", ParseFieldQuestName },
            { "QuestId", ParseFieldQuestId },
        };

        private static void ParseFieldType(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawType;
            if ((RawType = Value as string) != null)
                This.ParseType(RawType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource Type");
        }

        private void ParseType(string RawType, ParseErrorInfo ErrorInfo)
        {
            SourceTypes ParsedType;
            StringToEnumConversion<SourceTypes>.TryParse(RawType, out ParsedType, ErrorInfo);
            Type = ParsedType;
        }

        private static void ParseFieldSkillTypeId(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkillTypeId;
            if ((RawSkillTypeId = Value as string) != null)
                This.ParseSkillTypeId(RawSkillTypeId, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource SkillTypeId");
        }

        private void ParseSkillTypeId(string RawSkillTypeId, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkillTypeId;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkillTypeId, out ParsedSkillTypeId, ErrorInfo);
            SkillTypeId = ParsedSkillTypeId;
        }

        private static void ParseFieldItemName(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemName;
            if ((RawItemName = Value as string) != null)
                This.ParseItemName(RawItemName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource ItemName");
        }

        private void ParseItemName(string RawItemName, ParseErrorInfo ErrorInfo)
        {
            this.RawItemName = RawItemName;
        }

        private static void ParseFieldItemTypeId(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseItemTypeId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource ItemTypeId");
        }

        private void ParseItemTypeId(int RawItemTypeId, ParseErrorInfo ErrorInfo)
        {
            this.RawItemTypeId = RawItemTypeId;
        }

        private static void ParseFieldNpcName(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNpcName;
            if ((RawNpcName = Value as string) != null)
                This.ParseNpcName(RawNpcName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource NpcName");
        }

        private void ParseNpcName(string RawNpcName, ParseErrorInfo ErrorInfo)
        {
            this.RawNpcName = RawNpcName;
        }

        private static void ParseFieldEffectName(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawEffectName;
            if ((RawEffectName = Value as string) != null)
                This.ParseEffectName(RawEffectName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource EffectName");
        }

        private void ParseEffectName(string RawEffectName, ParseErrorInfo ErrorInfo)
        {
            this.RawEffectName = RawEffectName;
        }

        private static void ParseFieldQuestName(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawQuestName;
            if ((RawQuestName = Value as string) != null)
                This.ParseQuestName(RawQuestName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource QuestName");
        }

        private void ParseQuestName(string RawQuestName, ParseErrorInfo ErrorInfo)
        {
            this.RawQuestName = RawQuestName;
        }

        private static void ParseFieldQuestId(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseQuestId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource QuestId");
        }

        private void ParseQuestId(int RawQuestId, ParseErrorInfo ErrorInfo)
        {
            this.RawQuestId = RawQuestId;
        }
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
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, TextMaps.SourceTypesTextMap[Type]);
                if (ConnectedAbility != null)
                    AddWithFieldSeparator(ref Result, ConnectedAbility.Name);
                if (ConnectedSkillTypeId != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkillTypeId.Name);
                if (ConnectedItem != null)
                    AddWithFieldSeparator(ref Result, ConnectedItem.Name);
                if (ConnectedQuest != null)
                    AddWithFieldSeparator(ref Result, ConnectedQuest.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            if (RawAbilityId.HasValue)
            {
                ConnectedAbility = PgJsonObjects.Ability.ConnectByKey(ErrorInfo, AbilityTable, RawAbilityId.Value, ConnectedAbility, ref IsAbilityIdParsed, ref IsConnected, null);
                if (ConnectedAbility != null)
                    ConnectedAbility.SetSource(this, ErrorInfo);
            }

            if (SkillTypeId != PowerSkill.Internal_None && SkillTypeId != PowerSkill.AnySkill && SkillTypeId != PowerSkill.Unknown)
                ConnectedSkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SkillTypeId, ConnectedSkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedAbility);

            if (RawItemName != null)
                ConnectedItem = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedAbility);

            if (RawQuestId.HasValue)
                ConnectedQuest = PgJsonObjects.Quest.ConnectByKey(ErrorInfo, QuestTable, RawQuestId.Value, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedAbility);
            else if (RawQuestName != null)
                ConnectedQuest = PgJsonObjects.Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawQuestName, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedAbility);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilitySource"; } }
        #endregion
    }
}
