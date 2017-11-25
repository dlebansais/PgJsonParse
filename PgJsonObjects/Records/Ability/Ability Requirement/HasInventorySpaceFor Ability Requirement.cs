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

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "HasInventorySpaceFor");

            Generator.CloseObject();
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = false;

            Item = PgJsonObjects.Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItem, Item, ref IsRawItemParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
