using System;
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
        private int? RawItemTypeId;
        private string RawNpcId;
        private string RawNpcName;
        private bool IsNpcParsed;
        public GameNpc Npc { get; private set; }
        public Effect ConnectedEffect { get; private set; }
        public Recipe ConnectedRecipeEffect { get; private set; }
        private string RawEffectName;
        private bool IsEffectParsed;
        private bool IsRecipeParsed;
        private bool IsMiscEffect;
        public Quest ConnectedQuest { get; private set; }
        private string RawQuestName;
        private int? RawQuestId;
        private bool IsQuestNameParsed;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            if (ConnectedAbility != null)
            {
                GenericSource ConvertedSource = ToSpecificSource(ErrorInfo);
                ConnectedAbility.SetSource(ConvertedSource, ErrorInfo);
            }
        }

        private GenericSource ToSpecificSource(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case SourceTypes.AutomaticFromSkill:
                    if (ConnectedSkillTypeId != null)
                        return new SkillupSource(ConnectedSkillTypeId);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (AutomaticFromSkill)");
                        return null;
                    }

                case SourceTypes.Item:
                    if (ConnectedItem != null)
                        return new ItemSource(ConnectedItem);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (Item)");
                        return null;
                    }

                case SourceTypes.Training:
                    if (RawNpcName != null)
                        return new TrainingSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (Training)");
                        return null;
                    }

                case SourceTypes.Effect:
                    if (ConnectedEffect != null)
                        return new EffectSource(ConnectedEffect);

                    else if (ConnectedRecipeEffect != null)
                        return new RecipeEffectSource(ConnectedRecipeEffect);

                    else if (IsMiscEffect)
                        return new MiscSource();

                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (Effect)");
                        return null;
                    }

                case SourceTypes.Quest:
                    if (ConnectedQuest != null)
                        return new QuestSource(ConnectedQuest);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (Quest)");
                        return null;
                    }

                case SourceTypes.Gift:
                    if (RawNpcName != null)
                        return new GiftSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (Gift)");
                        return null;
                    }

                case SourceTypes.HangOut:
                    if (RawNpcName != null)
                        return new HangOutSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Ability Source (HangOut)");
                        return null;
                    }

                default:
                    return null;
            }
        }
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
            { "ItemTypeId", ParseFieldItemTypeId },
            //{ "NpcName", ParseFieldNpcName }, //TODO: clean this up if the field doesn't come back
            { "Npc", ParseFieldNpc },
            { "EffectName", ParseFieldEffectName },
            { "EffectTypeId", ParseFieldEffectTypeId },
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
            if (Type == SourceTypes.AutomaticFromSkill)
            {
                PowerSkill ParsedSkillTypeId;
                StringToEnumConversion<PowerSkill>.TryParse(RawSkillTypeId, out ParsedSkillTypeId, ErrorInfo);
                SkillTypeId = ParsedSkillTypeId;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource SkillTypeId (type)");
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
            if (Type == SourceTypes.Item)
                this.RawItemName = RawItemName;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource ItemName (type)");
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
            if (Type == SourceTypes.Item)
                this.RawItemTypeId = RawItemTypeId;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource ItemTypeId (type)");
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
            if (Type == SourceTypes.Training || Type == SourceTypes.Gift || Type == SourceTypes.HangOut)
                this.RawNpcName = RawNpcName;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource NpcName (type)");
        }

        private static void ParseFieldNpc(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawNpc;
            if ((RawNpc = Value as string) != null)
                This.ParseNpc(RawNpc, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource NpcName");
        }

        private void ParseNpc(string RawNpc, ParseErrorInfo ErrorInfo)
        {
            string ParsedNpcId;
            string ParsedNpcName;
            if (Quest.TryParseNPC(RawNpc, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
            {
                RawNpcId = ParsedNpcId;
                RawNpcName = ParsedNpcName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Target (for deliver)");
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
            if (Type == SourceTypes.Effect)
                this.RawEffectName = RawEffectName;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource EffectName (type)");
        }

        private static void ParseFieldEffectTypeId(AbilitySource This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawEffectTypeId;
            if ((RawEffectTypeId = Value as string) != null)
                This.ParseEffectTypeId(RawEffectTypeId, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource EffectTypeId");
        }

        private void ParseEffectTypeId(string RawEffectTypeId, ParseErrorInfo ErrorInfo)
        {
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
            if (Type == SourceTypes.Quest)
                this.RawQuestName = RawQuestName;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource QuestName (type)");
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
            if (Type == SourceTypes.Quest)
                this.RawQuestId = RawQuestId;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource QuestId (type)");
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
                /*
                AddWithFieldSeparator(ref Result, TextMaps.SourceTypesTextMap[Type]);
                if (ConnectedAbility != null)
                    AddWithFieldSeparator(ref Result, ConnectedAbility.Name);
                if (ConnectedSkillTypeId != null)
                    AddWithFieldSeparator(ref Result, ConnectedSkillTypeId.Name);
                if (ConnectedItem != null)
                    AddWithFieldSeparator(ref Result, ConnectedItem.Name);
                if (ConnectedEffect != null)
                    AddWithFieldSeparator(ref Result, ConnectedEffect.Name);
                if (ConnectedRecipeEffect != null)
                    AddWithFieldSeparator(ref Result, ConnectedRecipeEffect.Name);
                if (ConnectedQuest != null)
                    AddWithFieldSeparator(ref Result, ConnectedQuest.Name);
                */

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> EffectTable = AllTables[typeof(Effect)];
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

            if (RawAbilityId.HasValue)
            {
                ConnectedAbility = PgJsonObjects.Ability.ConnectByKey(ErrorInfo, AbilityTable, RawAbilityId.Value, ConnectedAbility, ref IsAbilityIdParsed, ref IsConnected, null);

                if (RawEffectName == "Learn Ability")
                    IsMiscEffect = true;
            }

            if (SkillTypeId != PowerSkill.Internal_None && SkillTypeId != PowerSkill.AnySkill && SkillTypeId != PowerSkill.Unknown)
                ConnectedSkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, SkillTypeId, ConnectedSkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedAbility);

            if (RawItemTypeId.HasValue)
                ConnectedItem = PgJsonObjects.Item.ConnectByKey(ErrorInfo, ItemTable, RawItemTypeId.Value, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedAbility);
            else if (RawItemName != null)
                ConnectedItem = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedAbility);

            if (RawEffectName != null && ConnectedRecipeEffect == null && ConnectedEffect == null)
            {
                ConnectedRecipeEffect = PgJsonObjects.Recipe.ConnectSingleProperty(null, RecipeTable, RawEffectName, ConnectedRecipeEffect, ref IsRecipeParsed, ref IsConnected, ConnectedAbility);
                ConnectedEffect = PgJsonObjects.Effect.ConnectSingleProperty(null, EffectTable, RawEffectName, ConnectedEffect, ref IsEffectParsed, ref IsConnected, ConnectedAbility);
                if (ConnectedRecipeEffect == null && ConnectedEffect == null && !IsMiscEffect)
                    ErrorInfo.AddMissingKey(RawEffectName);
            }

            if (RawQuestId.HasValue)
                ConnectedQuest = PgJsonObjects.Quest.ConnectByKey(ErrorInfo, QuestTable, RawQuestId.Value, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedAbility);
            else if (RawQuestName != null)
                ConnectedQuest = PgJsonObjects.Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawQuestName, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedAbility);

            Npc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, RawNpcId, Npc, ref IsNpcParsed, ref IsConnected, ConnectedAbility);
            if (RawNpcId != null && Npc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(RawNpcId, out ParsedSpecialNpc, ErrorInfo))
                    RawNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }

        public static AbilitySource ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, AbilitySource> AbilitySourceTable, string AbilitySourceKey, AbilitySource ParsedAbilitySource, ref bool IsRawAbilitySourceParsed, ref bool IsConnected, GenericJsonObject LinkBack)
        {
            if (IsRawAbilitySourceParsed)
                return ParsedAbilitySource;

            IsRawAbilitySourceParsed = true;

            if (AbilitySourceKey == null)
                return null;

            foreach (KeyValuePair<string, AbilitySource> Entry in AbilitySourceTable)
                if (Entry.Value.Key == AbilitySourceKey)
                {
                    IsConnected = true;
                    Entry.Value.AddLinkBack(LinkBack);
                    return Entry.Value;
                }

            if (ErrorInfo != null)
                ErrorInfo.AddMissingKey(AbilitySourceKey);

            return null;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "AbilitySource"; } }
        #endregion
    }
}
