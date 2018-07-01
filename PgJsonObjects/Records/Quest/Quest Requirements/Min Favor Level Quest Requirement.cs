using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinFavorLevelQuestRequirement : QuestRequirement, IPgMinFavorLevelQuestRequirement
    {
        public MinFavorLevelQuestRequirement(OtherRequirementType OtherRequirementType, Favor RequirementFavorLevel)
            : base (OtherRequirementType)
        {
            RawIsEmpty = true;
            FavorLevel = RequirementFavorLevel;
        }

        public MinFavorLevelQuestRequirement(OtherRequirementType OtherRequirementType, MapAreaName RequirementFavorNpcArea, string RequirementFavorNpcId, string RequirementFavorNpcName, Favor RequirementFavorLevel)
            : base(OtherRequirementType)
        {
            RawIsEmpty = false;
            FavorNpcArea = RequirementFavorNpcArea;
            FavorNpcId = RequirementFavorNpcId;
            FavorNpcName = RequirementFavorNpcName;
            FavorLevel = RequirementFavorLevel;
        }

        public IPgGameNpc FavorNpc { get; private set; }
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get; private set; }
        public Favor FavorLevel { get; private set; }
        public string FavorNpcId { get; private set; }
        public string FavorNpcName { get; private set; }
        public MapAreaName FavorNpcArea { get; private set; }

        private bool IsFavorNpcParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Quest.NpcToString(FavorNpcArea, FavorNpcId, FavorNpcName, IsEmpty) } },
            { "Level", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<Favor>.ToString(FavorLevel, null, Favor.Internal_None) } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (FavorNpc != null)
                {
                    AddWithFieldSeparator(ref Result, TextMaps.FavorTextMap[FavorLevel]);
                    AddWithFieldSeparator(ref Result, FavorNpc.Name);
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[FavorNpcArea]);
                }
                else if (FavorNpcName != null)
                {
                    AddWithFieldSeparator(ref Result, TextMaps.FavorTextMap[FavorLevel]);
                    AddWithFieldSeparator(ref Result, FavorNpcName);
                    AddWithFieldSeparator(ref Result, TextMaps.MapAreaNameTextMap[FavorNpcArea]);
                }

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> GameNpcTable = AllTables[typeof(GameNpc)];

            FavorNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, FavorNpcId, FavorNpc, ref IsFavorNpcParsed, ref IsConnected, Parent as IBackLinkable);
            if (FavorNpcId != null && FavorNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(FavorNpcId, out ParsedSpecialNpc, ErrorInfo))
                    FavorNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            int BitOffset = 0;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(FavorNpc as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);
            AddBool(RawIsEmpty, data, ref offset, ref BitOffset, BaseOffset, 4, 0);
            CloseBool(ref offset, ref BitOffset);
            AddEnum(FavorLevel, data, ref offset, BaseOffset, 6);
            AddString(FavorNpcId, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddString(FavorNpcName, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddEnum(FavorNpcArea, data, ref offset, BaseOffset, 16);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
