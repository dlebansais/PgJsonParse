using System;
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
            Generator.AddString("T", "RecipeKnown");
            //Generator.AddString("Recipe", RecipeKnown != null ? RecipeKnown.Name : null);
            Generator.AddString("Recipe", RawRecipeKnown);
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];

            RecipeKnown = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipeKnown, RecipeKnown, ref IsRawRecipeKnownParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion
    }
}
