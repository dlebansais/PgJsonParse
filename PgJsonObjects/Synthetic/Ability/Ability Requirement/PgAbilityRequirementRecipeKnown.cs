using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementRecipeKnown: PgAbilityRequirement<PgAbilityRequirementRecipeKnown>, IPgAbilityRequirementRecipeKnown
    {
        public PgAbilityRequirementRecipeKnown(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilityRequirementRecipeKnown CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilityRequirementRecipeKnown CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilityRequirementRecipeKnown(data, ref offset);
        }

        public override OtherRequirementType Type { get { return OtherRequirementType.RecipeKnown; } }
        public override string Key { get { return GetString(4); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(8, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public IPgRecipe RecipeKnown { get { return GetObject(12, ref _RecipeKnown, PgRecipe.CreateNew); } } private IPgRecipe _RecipeKnown;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(Type) } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RecipeKnown != null ? RecipeKnown.InternalName : null } },
        }; } }
    }
}
