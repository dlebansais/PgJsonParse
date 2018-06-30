using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilitySource : MainJsonObject<AbilitySource>, IPgAbilitySource
    {
        #region Direct Properties
        public IPgAbility ConnectedAbility { get; private set; }
        public IPgSkill SkillTypeId { get; private set; }
        public IPgItem ConnectedItem { get; private set; }
        public IPgGameNpc Npc { get; private set; }
        public IPgEffect ConnectedEffect { get; private set; }
        public IPgRecipe ConnectedRecipeEffect { get; private set; }
        public IPgQuest ConnectedQuest { get; private set; }
        public SourceTypes Type { get; private set; }

        private PowerSkill RawSkillTypeId;
        private bool IsSkillTypeIdParsed;
        private int? RawQuestId;
        private bool IsQuestNameParsed;
        private string RawEffectTypeId;
        private int? RawAbilityId;
        private bool IsAbilityIdParsed;
        private bool IsItemNameParsed;
        private int? RawItemTypeId;
        private string RawNpcId;
        private string RawNpcName;
        private bool IsNpcParsed;
        private string RawEffectName;
        private bool IsEffectParsed;
        private bool IsRecipeParsed;
        private bool IsMiscEffect;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            if (ConnectedAbility != null)
            {
                GenericSource ConvertedSource = ToSpecificSource(ErrorInfo);
                if (ConvertedSource != null)
                {
                    Ability a = ConnectedAbility as Ability;
                    a.SetSource(ConvertedSource, ErrorInfo);
                }
            }
        }

        private GenericSource ToSpecificSource(ParseErrorInfo ErrorInfo)
        {
            switch (Type)
            {
                case SourceTypes.AutomaticFromSkill:
                    if (SkillTypeId != null)
                        return new SkillupSource(SkillTypeId);
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
        protected override void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(key + "#" + index.ToString(), 0, value, ErrorInfo);

            int IdIndex = key.LastIndexOf('_');
            if (IdIndex >= 0)
            {
                int ParsedAbilityId;
                if (int.TryParse(key.Substring(IdIndex + 1), out ParsedAbilityId))
                    RawAbilityId = ParsedAbilityId;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Type = StringToEnumConversion<SourceTypes>.Parse(value, errorInfo),
                GetString  = () => StringToEnumConversion<SourceTypes>.ToString(Type) } },
            { "SkillTypeId", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkillTypeId,
                GetString  = () => StringToEnumConversion<PowerSkill>.ToString(RawSkillTypeId) } },
            { "ItemTypeId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseItemTypeId,
                GetInteger = () => RawItemTypeId } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseNpc,
                GetString  = () => Quest.NpcToString(RawNpcId, RawNpcName) } },
            { "EffectName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseEffectName,
                GetString  = () => RawEffectName } },
            { "EffectTypeId", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawEffectTypeId = value,
                GetString = () => RawEffectTypeId } },
            { "QuestId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseQuestId,
                GetInteger = () => RawQuestId } },
        }; } }

        private void ParseSkillTypeId(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.AutomaticFromSkill)
                RawSkillTypeId = StringToEnumConversion<PowerSkill>.Parse(value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource RawSkillTypeId (type)");
        }

        private void ParseItemTypeId(int value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Item)
                RawItemTypeId = (int)value;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource ItemTypeId (type)");
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

        private void ParseEffectName(string RawEffectName, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Effect)
                this.RawEffectName = RawEffectName;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource EffectName (type)");
        }

        private void ParseQuestId(int value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Quest)
                RawQuestId = value;
            else
                ErrorInfo.AddInvalidObjectFormat("AbilitySource QuestId (type)");
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
                if (SkillTypeId != null)
                    AddWithFieldSeparator(ref Result, SkillTypeId.Name);
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

            if (RawSkillTypeId != PowerSkill.Internal_None && RawSkillTypeId != PowerSkill.AnySkill && RawSkillTypeId != PowerSkill.Unknown)
                SkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkillTypeId, SkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedAbility as IBackLinkable);

            if (RawItemTypeId.HasValue)
                ConnectedItem = PgJsonObjects.Item.ConnectByKey(ErrorInfo, ItemTable, RawItemTypeId.Value, ConnectedItem, ref IsItemNameParsed, ref IsConnected, ConnectedAbility as IBackLinkable);

            if (RawEffectName != null && ConnectedRecipeEffect == null && ConnectedEffect == null)
            {
                ConnectedRecipeEffect = PgJsonObjects.Recipe.ConnectSingleProperty(null, RecipeTable, RawEffectName, ConnectedRecipeEffect, ref IsRecipeParsed, ref IsConnected, ConnectedAbility as IBackLinkable);
                ConnectedEffect = PgJsonObjects.Effect.ConnectSingleProperty(null, EffectTable, RawEffectName, ConnectedEffect, ref IsEffectParsed, ref IsConnected, ConnectedAbility as IBackLinkable);
                if (ConnectedRecipeEffect == null && ConnectedEffect == null && !IsMiscEffect)
                    ErrorInfo.AddMissingKey(RawEffectName);
            }

            if (RawQuestId.HasValue)
                ConnectedQuest = PgJsonObjects.Quest.ConnectByKey(ErrorInfo, QuestTable, RawQuestId.Value, ConnectedQuest, ref IsQuestNameParsed, ref IsConnected, ConnectedAbility as IBackLinkable);

            Npc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, RawNpcId, Npc, ref IsNpcParsed, ref IsConnected, ConnectedAbility as IBackLinkable);
            if (RawNpcId != null && Npc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(RawNpcId, out ParsedSpecialNpc, ErrorInfo))
                    RawNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }

        public static AbilitySource ConnectByKey(ParseErrorInfo ErrorInfo, Dictionary<string, AbilitySource> AbilitySourceTable, string AbilitySourceKey, AbilitySource ParsedAbilitySource, ref bool IsRawAbilitySourceParsed, ref bool IsConnected, IBackLinkable LinkBack)
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(ConnectedAbility as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObject(SkillTypeId as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddObject(ConnectedItem as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddObject(Npc as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddObject(ConnectedEffect as ISerializableJsonObject, data, ref offset, BaseOffset, 20, StoredObjectTable);
            AddObject(ConnectedRecipeEffect as ISerializableJsonObject, data, ref offset, BaseOffset, 24, StoredObjectTable);
            AddObject(ConnectedQuest as ISerializableJsonObject, data, ref offset, BaseOffset, 28, StoredObjectTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 32, StoredStringListTable);
            AddEnum(Type, data, ref offset, BaseOffset, 36);

            FinishSerializing(data, ref offset, BaseOffset, 38, null, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
