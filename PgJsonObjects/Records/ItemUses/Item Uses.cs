using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ItemUses : GenericJsonObject<ItemUses>
    {
        #region Direct Properties
        public List<int> RecipesThatUseItemList { get; private set; } = new List<int>();
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return ""; } }
        public const int SearchResultIconId = 2118;
        public string SearchResultIconFileName { get { return "icon_" + SearchResultIconId; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "RecipesThatUseItem", new FieldParser() {
                Type = FieldType.SimpleIntegerArray,
                ParseSimpleIntegerArray = (int value, ParseErrorInfo errorInfo) => RecipesThatUseItemList.Add(value),
                GetIntegerArray = () => RecipesThatUseItemList } },
        }; } }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];

            Item ConnectedItem = null;
            bool IsItemNameParsed = false;
            ConnectedItem = Item.ConnectById(ErrorInfo, ItemTable, Key, ConnectedItem, ref IsItemNameParsed, ref IsConnected, null);

            if (ConnectedItem != null)
            {
                foreach (int RecipeId in RecipesThatUseItemList)
                {
                    Recipe ConnectedRecipe = null;
                    bool IsRecipeIdParsed = false;
                    ConnectedRecipe = PgJsonObjects.Recipe.ConnectByKey(ErrorInfo, RecipeTable, RecipeId, ConnectedRecipe, ref IsRecipeIdParsed, ref IsConnected, null);
                    if (ConnectedRecipe != null)
                    {
                        bool IsListed = false;
                        foreach (RecipeItem Item in ConnectedRecipe.IngredientList)
                        {
                            if ("item_" + Item.ItemCode == Key)
                            {
                                IsListed = true;
                                break;
                            }
                        }

                        if (!IsListed)
                            IsListed = true;
                    }
                    else
                        ConnectedRecipe = null;
                }
            }
            else
                ConnectedItem = null;

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "ItemUses"; } }
        #endregion
    }
}
