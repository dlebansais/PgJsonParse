using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjective : GenericJsonObject<QuestObjective>
    {
        #region Constants
        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Type", ParseFieldType },
            { "Target", ParseFieldTarget },
            { "Description", ParseFieldDescription },
            { "Number", ParseFieldNumber },
            { "InteractionFlags", ParseFieldInteractionFlags },
            { "MapName", ParseFieldMapName },
            { "MapSpot", ParseFieldMapSpot },
            { "ItemName", ParseFieldItemName },
            { "MustCompleteEarlierObjectivesFirst", ParseFieldMustCompleteEarlierObjectivesFirst },
            { "InteractionFlag", ParseFieldInteractionFlag },
            { "MinAmount", ParseFieldMinAmount },
            { "MinFavorReceived", ParseFieldMinFavorReceived },
            { "MaxFavorReceived", ParseFieldMaxFavorReceived },
            { "Skill", ParseFieldSkill },
            { "StringParam", ParseFieldStringParam },
            { "ResultItemKeyword", ParseFieldResultItemKeyword },
            { "AbilityKeyword", ParseFieldAbilityKeyword },
            { "MaxAmount", ParseFieldMaxAmount },
            { "AnatomyType", ParseFieldAnatomyType },
            { "ItemKeyword", ParseFieldItemKeyword },
        };
        #endregion

        #region Properties
        public QuestObjectiveType Type { get; private set; }
        public string Target { get; private set; }
        public string Description { get; private set; }
        public int Number { get { return RawNumber.HasValue ? RawNumber.Value : 0; } }
        private int? RawNumber;
        public List<string> InteractionFlagList { get; private set; }
        public MapAreaName MapName { get; private set; }
        public MapSpotType MapSpotType { get; private set; }
        public string MapSpot { get; private set; }
        public Item QuestItem { get; private set; }
        private string RawItemName;
        private bool IsItemNameParsed;
        public bool MustCompleteEarlierObjectivesFirst { get { return RawMustCompleteEarlierObjectivesFirst.HasValue && RawMustCompleteEarlierObjectivesFirst.Value; } }
        private bool? RawMustCompleteEarlierObjectivesFirst;
        public string InteractionFlag { get; private set; }
        public string MinAmount { get; private set; }
        public string MinFavorReceived { get; private set; }
        public string MaxFavorReceived { get; private set; }
        public PowerSkill Skill { get; private set; }
        public string StringParam { get; private set; }
        public string ResultItemKeyword { get; private set; }
        public string AbilityKeyword { get; private set; }
        public string MaxAmount { get; private set; }
        public string AnatomyType { get; private set; }
        public ItemKeyword ItemKeyword { get; private set; }
        #endregion

        #region Client Interface
        private static void ParseFieldType(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawType;
            if ((RawType = Value as string) != null)
                This.ParseType(RawType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Type");
        }

        private void ParseType(string RawType, ParseErrorInfo ErrorInfo)
        {
            QuestObjectiveType ParsedType;
            StringToEnumConversion<QuestObjectiveType>.TryParse(RawType, out ParsedType, ErrorInfo);
            Type = ParsedType;
        }

        private static void ParseFieldTarget(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawTarget;
            if ((RawTarget = Value as string) != null)
                This.ParseTarget(RawTarget, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Target");
        }

        private void ParseTarget(string RawTarget, ParseErrorInfo ErrorInfo)
        {
            Target = RawTarget;
        }

        private static void ParseFieldDescription(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
        }

        private static void ParseFieldNumber(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumber((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective  Number");
        }

        private void ParseNumber(int RawNumber, ParseErrorInfo ErrorInfo)
        {
            this.RawNumber = RawNumber;
        }

        private static void ParseFieldInteractionFlags(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawInteractionFlags;
            if ((RawInteractionFlags = Value as ArrayList) != null)
                This.ParseInteractionFlags(RawInteractionFlags, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags");
        }

        private void ParseInteractionFlags(ArrayList RawInteractionFlags, ParseErrorInfo ErrorInfo)
        {
            foreach (object RawInteractionFlag in RawInteractionFlags)
            {
                string AsString;
                if ((AsString = RawInteractionFlag as string) != null)
                    InteractionFlagList.Add(AsString);
                else
                    ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlags");
            }
        }

        private static void ParseFieldMapName(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMapName;
            if ((RawMapName = Value as string) != null)
                This.ParseMapName(RawMapName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MapName");
        }

        private void ParseMapName(string RawMapName, ParseErrorInfo ErrorInfo)
        {
            if (RawMapName.StartsWith("Area"))
            {
                MapAreaName ParsedArea;
                StringToEnumConversion<MapAreaName>.TryParse(RawMapName.Substring(4), out ParsedArea, ErrorInfo);
                MapName = ParsedArea;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MapName");
        }

        private static void ParseFieldMapSpot(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMapSpot;
            if ((RawMapSpot = Value as string) != null)
                This.ParseMapSpot(RawMapSpot, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MapSpot");
        }

        private void ParseMapSpot(string RawMapSpot, ParseErrorInfo ErrorInfo)
        {
            if (RawMapSpot.StartsWith("NPC_"))
            {
                MapSpotType = MapSpotType.Npc;
                MapSpot = RawMapSpot.Substring(4);
            }

            else if (RawMapSpot.StartsWith("Spawn_"))
            {
                MapSpotType = MapSpotType.Spawn;
                MapSpot = RawMapSpot.Substring(6);
            }

            else if (RawMapSpot.StartsWith("GeneralArea_"))
            {
                MapSpotType = MapSpotType.GeneralArea;
                MapSpot = RawMapSpot.Substring(12);
            }

            else
            {
                MapSpotType = MapSpotType.Misc;
                MapSpot = RawMapSpot;
            }
        }

        private static void ParseFieldItemName(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemName;
            if ((RawItemName = Value as string) != null)
                This.ParseItemName(RawItemName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemName");
        }

        private void ParseItemName(string RawItemName, ParseErrorInfo ErrorInfo)
        {
            this.RawItemName = RawItemName;
        }

        private static void ParseFieldMustCompleteEarlierObjectivesFirst(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseMustCompleteEarlierObjectivesFirst((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MustCompleteEarlierObjectivesFirst");
        }

        private void ParseMustCompleteEarlierObjectivesFirst(bool RawMustCompleteEarlierObjectivesFirst, ParseErrorInfo ErrorInfo)
        {
            this.RawMustCompleteEarlierObjectivesFirst = RawMustCompleteEarlierObjectivesFirst;
        }

        private static void ParseFieldInteractionFlag(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInteractionFlag;
            if ((RawInteractionFlag = Value as string) != null)
                This.ParseInteractionFlag(RawInteractionFlag, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective InteractionFlag");
        }

        private void ParseInteractionFlag(string RawInteractionFlag, ParseErrorInfo ErrorInfo)
        {
            InteractionFlag = RawInteractionFlag;
        }

        private static void ParseFieldMinAmount(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMinAmount;
            if ((RawMinAmount = Value as string) != null)
                This.ParseMinAmount(RawMinAmount, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinAmount");
        }

        private void ParseMinAmount(string RawMinAmount, ParseErrorInfo ErrorInfo)
        {
            MinAmount = RawMinAmount;
        }

        private static void ParseFieldMinFavorReceived(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMinFavorReceived;
            if ((RawMinFavorReceived = Value as string) != null)
                This.ParseMinFavorReceived(RawMinFavorReceived, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MinFavorReceived");
        }

        private void ParseMinFavorReceived(string RawMinFavorReceived, ParseErrorInfo ErrorInfo)
        {
            MinFavorReceived = RawMinFavorReceived;
        }

        private static void ParseFieldMaxFavorReceived(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMaxFavorReceived;
            if ((RawMaxFavorReceived = Value as string) != null)
                This.ParseMaxFavorReceived(RawMaxFavorReceived, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxFavorReceived");
        }

        private void ParseMaxFavorReceived(string RawMaxFavorReceived, ParseErrorInfo ErrorInfo)
        {
            MaxFavorReceived = RawMaxFavorReceived;
        }

        private static void ParseFieldSkill(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
            Skill = ParsedSkill;
        }

        private static void ParseFieldStringParam(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawStringParam;
            if ((RawStringParam = Value as string) != null)
                This.ParseStringParam(RawStringParam, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective StringParam");
        }

        private void ParseStringParam(string RawStringParam, ParseErrorInfo ErrorInfo)
        {
            StringParam = RawStringParam;
        }

        private static void ParseFieldResultItemKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawResultItemKeyword;
            if ((RawResultItemKeyword = Value as string) != null)
                This.ParseResultItemKeyword(RawResultItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ResultItemKeyword");
        }

        private void ParseResultItemKeyword(string RawResultItemKeyword, ParseErrorInfo ErrorInfo)
        {
            ResultItemKeyword = RawResultItemKeyword;
        }

        private static void ParseFieldAbilityKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAbilityKeyword;
            if ((RawAbilityKeyword = Value as string) != null)
                This.ParseAbilityKeyword(RawAbilityKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AbilityKeyword");
        }

        private void ParseAbilityKeyword(string RawAbilityKeyword, ParseErrorInfo ErrorInfo)
        {
            AbilityKeyword = RawAbilityKeyword;
        }

        private static void ParseFieldMaxAmount(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawMaxAmount;
            if ((RawMaxAmount = Value as string) != null)
                This.ParseMaxAmount(RawMaxAmount, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective MaxAmount");
        }

        private void ParseMaxAmount(string RawMaxAmount, ParseErrorInfo ErrorInfo)
        {
            MaxAmount = RawMaxAmount;
        }

        private static void ParseFieldAnatomyType(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawAnatomyType;
            if ((RawAnatomyType = Value as string) != null)
                This.ParseAnatomyType(RawAnatomyType, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective AnatomyType");
        }

        private void ParseAnatomyType(string RawAnatomyType, ParseErrorInfo ErrorInfo)
        {
            AnatomyType = RawAnatomyType;
        }

        private static void ParseFieldItemKeyword(QuestObjective This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawItemKeyword;
            if ((RawItemKeyword = Value as string) != null)
                This.ParseItemKeyword(RawItemKeyword, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("QuestObjective ItemKeyword");
        }

        private void ParseItemKeyword(string RawItemKeyword, ParseErrorInfo ErrorInfo)
        {
            ItemKeyword ParsedItemKeyword;
            StringToEnumConversion<ItemKeyword>.TryParse(RawItemKeyword, out ParsedItemKeyword, ErrorInfo);
            ItemKeyword = ParsedItemKeyword;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "QuestObjective"; } }

        protected override void InitializeFields()
        {
            InteractionFlagList = new List<string>();
            QuestItem = null;
            RawItemName = null;
            IsItemNameParsed = false;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool IsConnected = false;

            QuestItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, QuestItem, ref IsItemNameParsed, ref IsConnected);

            return IsConnected;
        }
        #endregion
    }
}
