using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestCompletedQuestRequirement : QuestRequirement
    {
        public QuestCompletedQuestRequirement(OtherRequirementType OtherRequirementType, List<string> RawRequirementQuestList)
            : base(OtherRequirementType)
        {
            this.RawRequirementQuestList = RawRequirementQuestList;
        }

        private List<string> RawRequirementQuestList;
        public List<Quest> QuestList { get; private set; } = new List<Quest>();
        private bool IsRawRequirementQuestParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawRequirementQuestList.Count > 0 ? RawRequirementQuestList[0] : null } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "QuestCompleted");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (Quest Item in QuestList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> QuestTable = AllTables[typeof(Quest)];

            if (!IsRawRequirementQuestParsed)
            {
                IsRawRequirementQuestParsed = true;

                int Index = 0;
                while (Index < RawRequirementQuestList.Count)
                {
                    Quest RequirementQuest = null;
                    bool IsParsed = false;
                    RequirementQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawRequirementQuestList[0], RequirementQuest, ref IsParsed, ref IsConnected, Parent as GenericJsonObject);

                    if (RequirementQuest != null)
                    {
                        RawRequirementQuestList.RemoveAt(0);
                        QuestList.Add(RequirementQuest);
                    }
                    else
                        Index++;
                }
            }

            return IsConnected;
        }
        #endregion
    }
}
