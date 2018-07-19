using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipeEffectSource : PgGenericSource<PgRecipeEffectSource>, IPgRecipeEffectSource
    {
        public PgRecipeEffectSource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }
        
        protected override PgRecipeEffectSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeEffectSource CreateNew(byte[] data, ref int offset)
        {
            return new PgRecipeEffectSource(data, ref offset);
        }

        public override void Init(IGenericPgObject Parent)
        {
            Parent.AddLinkBack(Recipe);
        }

        public IPgRecipe Recipe { get { return GetObject(PropertiesOffset + 0, ref _Recipe, PgRecipe.CreateNew); } } private IPgRecipe _Recipe;

        public override string Key { get { return null; } }
        protected override List<string> FieldTableOrder { get { return null; } }
        protected override Dictionary<string, FieldParser> FieldTable { get { return null; } }
        public override string SortingName { get { return null; } }
    }
}
