using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeKnownAbilityRequirement : AbilityRequirement
    {
        public RecipeKnownAbilityRequirement(string RawRecipeKnown)
        {
            this.RawRecipeKnown = RawRecipeKnown;
        }

        public Recipe RecipeKnown { get; private set; }
        private string RawRecipeKnown;
        private bool IsRawRecipeKnownParsed;

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("T", "RecipeKnown");

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                AddWithFieldSeparator(ref Result, RecipeKnown.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable, Dictionary<string, GameNpc> GameNpcTable, Dictionary<string, StorageVault> StorageVaultTable, Dictionary<string, AbilitySource> AbilitySourceTable)
        {
            bool IsConnected = false;

            RecipeKnown = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipeKnown, RecipeKnown, ref IsRawRecipeKnownParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
