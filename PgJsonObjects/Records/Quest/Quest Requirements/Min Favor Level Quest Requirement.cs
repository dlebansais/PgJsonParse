using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinFavorLevelQuestRequirement : QuestRequirement
    {
        public MinFavorLevelQuestRequirement(OtherRequirementType OtherRequirementType, Favor RequirementFavorLevel)
            : base (OtherRequirementType)
        {
            IsEmpty = true;
            FavorLevel = RequirementFavorLevel;
        }

        public MinFavorLevelQuestRequirement(OtherRequirementType OtherRequirementType, MapAreaName RequirementFavorNpcArea, string RequirementFavorNpcId, string RequirementFavorNpcName, Favor RequirementFavorLevel)
            : base(OtherRequirementType)
        {
            FavorNpcArea = RequirementFavorNpcArea;
            FavorNpcId = RequirementFavorNpcId;
            FavorNpcName = RequirementFavorNpcName;
            FavorLevel = RequirementFavorLevel;
        }

        public bool IsEmpty { get; private set; }
        public GameNpc FavorNpc { get; private set; }
        private bool IsFavorNpcParsed;
        public MapAreaName FavorNpcArea { get; private set; }
        private string FavorNpcId;
        private string FavorNpcName;
        public Favor FavorLevel { get; private set; }

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

            FavorNpc = GameNpc.ConnectByKey(ErrorInfo, GameNpcTable, FavorNpcId, FavorNpc, ref IsFavorNpcParsed, ref IsConnected, Parent as GenericJsonObject);
            if (FavorNpcId != null && FavorNpc == null)
            {
                SpecialNpc ParsedSpecialNpc;
                if (StringToEnumConversion<SpecialNpc>.TryParse(FavorNpcId, out ParsedSpecialNpc, ErrorInfo))
                    FavorNpcName = TextMaps.SpecialNpcTextMap[ParsedSpecialNpc];
            }

            return IsConnected;
        }
        #endregion
    }
}
