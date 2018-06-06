using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class HasInventorySpaceForAbilityRequirement : AbilityRequirement
    {
        public HasInventorySpaceForAbilityRequirement(string RawItem)
        {
            this.RawItem = RawItem;
        }

        public Item Item { get; private set; }
        private string RawItem;
        private bool IsRawItemParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.HasInventorySpaceFor) } },
            { "Item", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => RawItem } },
        }; } }

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.AddString("T", "HasInventorySpaceFor");
            //Generator.AddString("Item", Item != null ? Item.Name : null);
            Generator.AddString("Item", RawItem);
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, Item.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            Item = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, Item, ref IsRawItemParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
