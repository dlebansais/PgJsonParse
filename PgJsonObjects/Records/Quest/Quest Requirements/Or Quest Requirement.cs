using System.Collections.Generic;

namespace PgJsonObjects
{
    public class OrQuestRequirement : QuestRequirement
    {
        public OrQuestRequirement(List<QuestRequirement> OrList)
        {
            this.OrList = OrList;
        }

        public List<QuestRequirement> OrList { get; private set; }

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
