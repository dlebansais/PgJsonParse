namespace PgJsonObjects
{
    public class PgRecipeSource : MainPgObject<PgRecipeSource>, IPgRecipeSource
    {
        public PgRecipeSource(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 26;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgRecipeSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeSource CreateNew(byte[] data, ref int offset)
        {
            return new PgRecipeSource(data, ref offset);
        }

        public IPgRecipe ConnectedRecipe { get { return GetObject(0, ref _ConnectedRecipe, PgRecipe.CreateNew); } } private IPgRecipe _ConnectedRecipe;
        public IPgSkill SkillTypeId { get { return GetObject(4, ref _SkillTypeId, PgSkill.CreateNew); } } private IPgSkill _SkillTypeId;
        public IPgItem ConnectedItem { get { return GetObject(8, ref _ConnectedItem, PgItem.CreateNew); } } private IPgItem _ConnectedItem;
        public IPgGameNpc Npc { get { return GetObject(12, ref _Npc, PgGameNpc.CreateNew); } } private IPgGameNpc _Npc;
        public IPgEffect ConnectedEffect { get { return GetObject(16, ref _ConnectedEffect, PgEffect.CreateNew); } } private IPgEffect _ConnectedEffect;
        public IPgQuest ConnectedQuest { get { return GetObject(20, ref _ConnectedQuest, PgQuest.CreateNew); } } private IPgQuest _ConnectedQuest;
        public SourceTypes Type { get { return GetEnum<SourceTypes>(24); } }
    }
}
