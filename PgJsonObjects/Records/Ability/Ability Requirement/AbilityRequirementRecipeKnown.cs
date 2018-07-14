using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementRecipeKnown : AbilityRequirement, IPgAbilityRequirementRecipeKnown
    {
        public AbilityRequirementRecipeKnown(string rawRecipeKnown)
        {
            RawRecipeKnown = rawRecipeKnown;
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.RecipeKnown; } }
        public IPgRecipe RecipeKnown { get; private set; }
        private string RawRecipeKnown;
        private bool IsRawRecipeKnownParsed;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
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

                if (RecipeKnown != null)
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
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            SerializeJsonObjectInternalProlog(data, ref offset, StoredStringtable, StoredStringListTable);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(RecipeKnown as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, StoredStringtable, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
