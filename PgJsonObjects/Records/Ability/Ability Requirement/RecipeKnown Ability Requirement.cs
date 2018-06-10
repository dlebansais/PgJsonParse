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

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.RecipeKnown) } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawRecipeKnown } },
        }; } }

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
