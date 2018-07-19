using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementRecipeKnown : AbilityRequirement, IPgAbilityRequirementRecipeKnown
    {
        public AbilityRequirementRecipeKnown(string rawRecipe)
        {
            RawRecipe = rawRecipe;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.RecipeKnown; } }
        public IPgRecipe Recipe { get; private set; }
        private string RawRecipe;
        private bool IsRawRecipeParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Recipe != null ? Recipe.InternalName : null } },
        }; } }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Recipe != null)
                    AddWithFieldSeparator(ref Result, Recipe.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IJsonKey> RecipeTable = AllTables[typeof(Recipe)];

            Recipe = PgJsonObjects.Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipe, Recipe, ref IsRawRecipeParsed, ref IsConnected, Parent);

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

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Recipe as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
