using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RunTimeBehaviorRuleSetQuestRequirement : QuestRequirement, IPgRunTimeBehaviorRuleSetQuestRequirement
    {
        public RunTimeBehaviorRuleSetQuestRequirement(OtherRequirementType OtherRequirementType, string RequirementRule)
            : base(OtherRequirementType)
        {
            this.RequirementRule = RequirementRule;

            if (RequirementRule == "ChristmasQuests")
                Rule = "During Christmas Quests";
            else
                Rule = RequirementRule;
        }

        public string RequirementRule { get; private set; }
        public string Rule { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "Rule", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RequirementRule } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Rule != null)
                    Result = Rule;

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddString(RequirementRule, data, ref offset, BaseOffset, 8, StoredStringtable);
            AddString(Rule, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 16, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 20, StoredStringtable, null, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
