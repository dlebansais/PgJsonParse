using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeKnownAbilityRequirement : AbilityRequirement, IPgAbilityRequirementRecipeKnown
    {
        public RecipeKnownAbilityRequirement(string RawRecipeKnown)
        {
            this.RawRecipeKnown = RawRecipeKnown;
        }

        public IPgRecipe RecipeKnown { get; private set; }
        private string RawRecipeKnown;
        private bool IsRawRecipeKnownParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType.RecipeKnown) } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RecipeKnown != null ? RecipeKnown.InternalName : null } },
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

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)OtherRequirementType.RecipeKnown, data, ref offset, BaseOffset, 0);
            AddString(Key, data, ref offset, BaseOffset, 4, StoredStringtable);
            AddObject(RecipeKnown as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddStringList(FieldTableOrder, data, ref offset, BaseOffset, 12, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 16, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
