using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class OrQuestRequirement : QuestRequirement, IPgOrQuestRequirement
    {
        public OrQuestRequirement(OtherRequirementType OtherRequirementType, List<QuestRequirement> OrList)
            : base(OtherRequirementType)
        {
            this.OrList = OrList;
        }

        public List<QuestRequirement> OrList { get; private set; }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (QuestRequirement Item in OrList)
                    AddWithFieldSeparator(ref Result, Item.TextContent);

                return Result;
            }
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IList> StoredObjectListTable = new Dictionary<int, IList>();

            AddObjectList(OrList, data, ref offset, BaseOffset, 0, StoredObjectListTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, null, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
