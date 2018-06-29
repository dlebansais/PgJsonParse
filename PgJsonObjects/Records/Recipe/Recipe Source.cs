using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeSource : MainJsonObject<RecipeSource>, IPgRecipeSource
    {
        #region Direct Properties
        public IPgRecipe ConnectedRecipe { get; private set; }
        public IPgSkill SkillTypeId { get; private set; }
        public IPgItem ConnectedItem { get; private set; }
        public IPgGameNpc Npc { get; private set; }
        public IPgEffect ConnectedEffect { get; private set; }
        public IPgQuest ConnectedQuest { get; private set; }
        public SourceTypes Type { get; private set; }

        private int? RawRecipeId;
        private bool IsRecipeIdParsed;
        private PowerSkill RawSkillTypeId;
        private bool IsSkillTypeIdParsed;
        private int? RawItemTypeId;
        private bool IsItemTypeIdParsed;
        private string RawNpcId;
        private string RawNpcName;
        private bool IsNpcParsed;
        private string RawEffectName;
        private bool IsEffectParsed;
        private int? RawQuestId;
        private bool IsQuestIdParsed;
        private string RawEffectTypeId;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }

        public override void SetIndirectProperties(Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables, ParseErrorInfo ErrorInfo)
        {
            if (ConnectedRecipe != null)
            {
                GenericSource ConvertedSource = ToSpecificSource(ErrorInfo);
                if (ConvertedSource != null)
                    (ConnectedRecipe as Recipe).SetSource(ConvertedSource, ErrorInfo);
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
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (AutomaticFromSkill)");
                        return null;
                    }

                case SourceTypes.Item:
                    if (ConnectedItem != null)
                        return new ItemSource(ConnectedItem);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Item)");
                        return null;
                    }

                case SourceTypes.Training:
                    if (RawNpcName != null)
                        return new TrainingSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Training)");
                        return null;
                    }

                case SourceTypes.Effect:
                    if (ConnectedEffect != null)
                        return new EffectSource(ConnectedEffect);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Effect)");
                        return null;
                    }

                case SourceTypes.Quest:
                    if (ConnectedQuest != null)
                        return new QuestSource(ConnectedQuest);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Quest)");
                        return null;
                    }

                case SourceTypes.Gift:
                    if (RawNpcName != null)
                        return new GiftSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (Gift)");
                        return null;
                    }

                case SourceTypes.HangOut:
                    if (RawNpcName != null)
                        return new HangOutSource(RawNpcName, Npc);
                    else
                    {
                        ErrorInfo.AddInvalidObjectFormat("Recipe Source (HangOut)");
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
                int ParsedRecipeId;
                if (int.TryParse(key.Substring(IdIndex + 1), out ParsedRecipeId))
                    RawRecipeId = ParsedRecipeId;
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Type = StringToEnumConversion<SourceTypes>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<SourceTypes>.ToString(Type, null, SourceTypes.Internal_None) } },
            { "SkillTypeId", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseSkillTypeId,
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawSkillTypeId, null, PowerSkill.Internal_None) } },
            { "ItemTypeId", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseItemTypeId,
                GetInteger = () => RawItemTypeId } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseNpc,
                GetString = () => Quest.NpcToString(RawNpcId, RawNpcName) } },
            { "EffectName", new FieldParser() {
                Type = FieldType.String,
                ParseString = ParseEffectName,
                GetString = () => RawEffectName } },
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
                ErrorInfo.AddInvalidObjectFormat("RecipeSource RawSkillTypeId (type)");
        }

        private void ParseItemTypeId(int value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Item)
                RawItemTypeId = value;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource ItemTypeId (type)");
        }

        private void ParseNpc(string value, ParseErrorInfo ErrorInfo)
        {
            string ParsedNpcId;
            string ParsedNpcName;
            if (Quest.TryParseNPC(value, out ParsedNpcId, out ParsedNpcName, ErrorInfo))
            {
                RawNpcId = ParsedNpcId;
                RawNpcName = ParsedNpcName;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource Npc");
        }

        private void ParseEffectName(string value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Effect)
                RawEffectName = value;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource EffectName (type)");
        }

        private void ParseEffectTypeId(string value, ParseErrorInfo ErrorInfo)
        {
        }

        private void ParseQuestId(int value, ParseErrorInfo ErrorInfo)
        {
            if (Type == SourceTypes.Quest)
                RawQuestId = value;
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeSource QuestId (type)");
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
                if (ConnectedRecipe != null)
                    AddWithFieldSeparator(ref Result, ConnectedRecipe.Name);
                if (SkillTypeId != null)
                    AddWithFieldSeparator(ref Result, SkillTypeId.Name);
                if (ConnectedItem != null)
                    AddWithFieldSeparator(ref Result, ConnectedItem.Name);
                if (ConnectedEffect != null)
                    AddWithFieldSeparator(ref Result, ConnectedEffect.Name);
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
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];
            Dictionary<string, IGenericJsonObject> EffectTable = AllTables[typeof(Effect)];
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

            if (RawRecipeId.HasValue)
                ConnectedRecipe = PgJsonObjects.Recipe.ConnectByKey(ErrorInfo, RecipeTable, RawRecipeId.Value, ConnectedRecipe, ref IsRecipeIdParsed, ref IsConnected, null);

            if (RawSkillTypeId != PowerSkill.Internal_None && RawSkillTypeId != PowerSkill.AnySkill && RawSkillTypeId != PowerSkill.Unknown)
                SkillTypeId = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawSkillTypeId, SkillTypeId, ref IsSkillTypeIdParsed, ref IsConnected, ConnectedRecipe as IBackLinkable);

            if (RawItemTypeId.HasValue)
                ConnectedItem = PgJsonObjects.Item.ConnectByKey(ErrorInfo, ItemTable, RawItemTypeId.Value, ConnectedItem, ref IsItemTypeIdParsed, ref IsConnected, ConnectedRecipe as IBackLinkable);

            if (RawEffectName != null)
                ConnectedEffect = PgJsonObjects.Effect.ConnectSingleProperty(ErrorInfo, EffectTable, RawEffectName, ConnectedEffect, ref IsEffectParsed, ref IsConnected, ConnectedRecipe as IBackLinkable);

            if (RawQuestId.HasValue)
                ConnectedQuest = PgJsonObjects.Quest.ConnectByKey(ErrorInfo, QuestTable, RawQuestId.Value, ConnectedQuest, ref IsQuestIdParsed, ref IsConnected, ConnectedRecipe as IBackLinkable);

            Npc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, RawNpcId, Npc, ref IsNpcParsed, ref IsConnected, ConnectedRecipe as IBackLinkable);
            if (RawNpcId != null && Npc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(RawNpcId, out ParsedSpecialNpc, ErrorInfo))
                    RawNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeSource"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddString(Key, data, ref offset, BaseOffset, 0, StoredStringtable);
            AddObject(ConnectedRecipe as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddObject(SkillTypeId as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddObject(ConnectedItem as ISerializableJsonObject, data, ref offset, BaseOffset, 12, StoredObjectTable);
            AddObject(Npc as ISerializableJsonObject, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddObject(ConnectedEffect as ISerializableJsonObject, data, ref offset, BaseOffset, 20, StoredObjectTable);
            AddObject(ConnectedQuest as ISerializableJsonObject, data, ref offset, BaseOffset, 24, StoredObjectTable);
            AddEnum(Type, data, ref offset, BaseOffset, 28);

            FinishSerializing(data, ref offset, BaseOffset, 30, StoredStringtable, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
