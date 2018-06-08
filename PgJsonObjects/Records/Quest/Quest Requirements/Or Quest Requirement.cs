using System.Collections.Generic;

namespace PgJsonObjects
{
    public class OrQuestRequirement : QuestRequirement
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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "Or");

            Generator.CloseObject();
        }
        #endregion

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
    }
}
