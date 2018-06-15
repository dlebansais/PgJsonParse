namespace PgJsonObjects
{
    public class PgAbilitySource : GenericPgObject, IPgAbilitySource
    {
        public PgAbilitySource(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Ability ConnectedAbility { get { return GetObject(0, ref _ConnectedAbility); } } private Ability _ConnectedAbility;
        public Skill SkillTypeId { get { return GetObject(4, ref _SkillTypeId); } } private Skill _SkillTypeId;
        public Item ConnectedItem { get { return GetObject(8, ref _ConnectedItem); } } private Item _ConnectedItem;
        public GameNpc Npc { get { return GetObject(12, ref _Npc); } } private GameNpc _Npc;
        public Effect ConnectedEffect { get { return GetObject(16, ref _ConnectedEffect); } } private Effect _ConnectedEffect;
        public Recipe ConnectedRecipeEffect { get { return GetObject(20, ref _ConnectedRecipeEffect); } } private Recipe _ConnectedRecipeEffect;
        public Quest ConnectedQuest { get { return GetObject(24, ref _ConnectedQuest); } } private Quest _ConnectedQuest;
        public SourceTypes Type { get { return GetEnum<SourceTypes>(28); } }
    }
}
