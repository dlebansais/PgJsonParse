namespace PgJsonObjects
{
    public class PgRecipeSource : GenericPgObject, IPgRecipeSource
    {
        public PgRecipeSource(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Recipe ConnectedRecipe { get { return GetObject(0, ref _ConnectedRecipe); } } private Recipe _ConnectedRecipe;
        public Skill SkillTypeId { get { return GetObject(4, ref _SkillTypeId); } } private Skill _SkillTypeId;
        public Item ConnectedItem { get { return GetObject(8, ref _ConnectedItem); } } private Item _ConnectedItem;
        public GameNpc Npc { get { return GetObject(12, ref _Npc); } } private GameNpc _Npc;
        public Effect ConnectedEffect { get { return GetObject(16, ref _ConnectedEffect); } } private Effect _ConnectedEffect;
        public Quest ConnectedQuest { get { return GetObject(20, ref _ConnectedQuest); } } private Quest _ConnectedQuest;
        public SourceTypes Type { get { return GetEnum<SourceTypes>(24); } }
    }
}
