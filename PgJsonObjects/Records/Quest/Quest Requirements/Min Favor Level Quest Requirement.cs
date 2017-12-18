using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class MinFavorLevelQuestRequirement : QuestRequirement
    {
        public MinFavorLevelQuestRequirement(MapAreaName RequirementFavorNpcArea, string RequirementFavorNpcId, string RequirementFavorNpcName, Favor RequirementFavorLevel)
        {
            FavorNpcArea = RequirementFavorNpcArea;
            FavorNpcId = RequirementFavorNpcId;
            FavorNpcName = RequirementFavorNpcName;
            FavorLevel = RequirementFavorLevel;
        }

        public GameNpc FavorNpc { get; private set; }
        private bool IsFavorNpcParsed;
        public MapAreaName FavorNpcArea { get; private set; }
        private string FavorNpcId;
        private string FavorNpcName;
        public Favor FavorLevel { get; private set; }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "MinFavorLevel");

            Generator.CloseObject();
        }
        #endregion

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
