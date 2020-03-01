using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeEffectSource : GenericSource<RecipeEffectSource>, IPgRecipeEffectSource
    {
        #region Init
        public RecipeEffectSource(IPgRecipe Recipe)
        {
            this.Recipe = Recipe;
        }

        protected override int Type { get { return ((int)SourceType.Effect) | PgGenericSourceCollection.RecipeEffectTag; } }
        #endregion

        #region Properties
        public IPgRecipe Recipe { get; private set; }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            SerializeJsonObjectInternalProlog(data, ref offset);
            int BaseOffset = offset;

            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();

            AddObject(Recipe as ISerializableJsonObject, data, ref offset, BaseOffset, 0, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 4, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
