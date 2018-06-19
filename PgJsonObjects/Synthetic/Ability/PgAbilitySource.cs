﻿namespace PgJsonObjects
{
    public class PgAbilitySource : MainPgObject<PgAbilitySource>, IPgAbilitySource
    {
        public PgAbilitySource(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgAbilitySource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilitySource CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilitySource(data, ref offset);
        }

        public IPgAbility ConnectedAbility { get { return GetObject(0, ref _ConnectedAbility, PgAbility.CreateNew); } } private IPgAbility _ConnectedAbility;
        public IPgSkill SkillTypeId { get { return GetObject(4, ref _SkillTypeId, PgSkill.CreateNew); } } private IPgSkill _SkillTypeId;
        public IPgItem ConnectedItem { get { return GetObject(8, ref _ConnectedItem, PgItem.CreateNew); } } private IPgItem _ConnectedItem;
        public IPgGameNpc Npc { get { return GetObject(12, ref _Npc, PgGameNpc.CreateNew); } } private IPgGameNpc _Npc;
        public IPgEffect ConnectedEffect { get { return GetObject(16, ref _ConnectedEffect, PgEffect.CreateNew); } } private IPgEffect _ConnectedEffect;
        public IPgRecipe ConnectedRecipeEffect { get { return GetObject(20, ref _ConnectedRecipeEffect, PgRecipe.CreateNew); } } private IPgRecipe _ConnectedRecipeEffect;
        public IPgQuest ConnectedQuest { get { return GetObject(24, ref _ConnectedQuest, PgQuest.CreateNew); } } private IPgQuest _ConnectedQuest;
        public SourceTypes Type { get { return GetEnum<SourceTypes>(28); } }
    }
}
