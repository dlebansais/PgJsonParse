﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class GuildQuestCompletedQuestRequirement : QuestRequirement, IPgGuildQuestCompletedQuestRequirement
    {
        public GuildQuestCompletedQuestRequirement(OtherRequirementType OtherRequirementType, List<string> RawRequirementQuestList)
            : base(OtherRequirementType)
        {
            this.RawRequirementQuestList = RawRequirementQuestList;
        }

        public IPgQuestCollection QuestList { get; private set; } = new QuestCollection();
        private List<string> RawRequirementQuestList;
        private bool IsRawRequirementQuestParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Quest", new FieldParser() {
                Type = FieldType.String,
                GetString = () => QuestList.Count > 0 ? QuestList[0].InternalName : null } },
        }; } }

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
                    IPgQuest RequirementQuest = null;
                    bool IsParsed = false;
                    RequirementQuest = Quest.ConnectSingleProperty(ErrorInfo, QuestTable, RawRequirementQuestList[0], RequirementQuest, ref IsParsed, ref IsConnected, Parent as IBackLinkable);

                    if (RequirementQuest != null)
                    {
                        RawRequirementQuestList.RemoveAt(0);
                        QuestList.Add(RequirementQuest as Quest);
                    }
                    else
                        Index++;
                }
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

            Dictionary<int, IPgCollection> StoredObjectListTable = new Dictionary<int, IPgCollection>();

            AddObjectList(QuestList, data, ref offset, BaseOffset, 0, StoredObjectListTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, null, null, null, null, null, StoredStringListTable, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
