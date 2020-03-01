using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilitySource : MainPgObject<PgAbilitySource>, IPgAbilitySource
    {
        public PgAbilitySource(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 54;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAbilitySource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAbilitySource CreateNew(byte[] data, ref int offset)
        {
            return new PgAbilitySource(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public IPgAbility ConnectedAbility { get { return GetObject(4, ref _ConnectedAbility, PgAbility.CreateNew); } } private IPgAbility _ConnectedAbility;
        public IPgSkill SkillTypeId { get { return GetObject(8, ref _SkillTypeId, PgSkill.CreateNew); } } private IPgSkill _SkillTypeId;
        public IPgItem ConnectedItem { get { return GetObject(12, ref _ConnectedItem, PgItem.CreateNew); } } private IPgItem _ConnectedItem;
        public IPgGameNpc Npc { get { return GetObject(16, ref _Npc, PgGameNpc.CreateNew); } } private IPgGameNpc _Npc;
        public IPgEffect ConnectedEffect { get { return GetObject(20, ref _ConnectedEffect, PgEffect.CreateNew); } } private IPgEffect _ConnectedEffect;
        public IPgRecipe ConnectedRecipeEffect { get { return GetObject(24, ref _ConnectedRecipeEffect, PgRecipe.CreateNew); } } private IPgRecipe _ConnectedRecipeEffect;
        public IPgQuest ConnectedQuest { get { return GetObject(28, ref _ConnectedQuest, PgQuest.CreateNew); } } private IPgQuest _ConnectedQuest;
        protected override List<string> FieldTableOrder { get { return GetStringList(32, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public string EffectTypeId { get { return GetString(36); } }
        public string RawNpcId { get { return GetString(40); } }
        public string RawNpcName { get { return GetString(44); } }
        public string RawEffectName { get { return GetString(48); } }
        public SourceType Type { get { return GetEnum<SourceType>(52); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => StringToEnumConversion<SourceType>.ToString(Type) } },
            { "SkillTypeId", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => SkillTypeId != null ? StringToEnumConversion<PowerSkill>.ToString(SkillTypeId.CombatSkill) : null } },
            { "ItemTypeId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = GetItemTypeId } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => Quest.NpcToString(RawNpcId, RawNpcName) } },
            { "EffectName", new FieldParser() {
                Type = FieldType.String,
                GetString  = () => RawEffectName } },
            { "EffectTypeId", new FieldParser() {
                Type = FieldType.String,
                GetString = () => EffectTypeId } },
            { "QuestId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = GetQuestId } },
        }; } }

        private int? GetItemTypeId()
        {
            if (ConnectedItem == null)
                return null;

            string KeyId = ConnectedItem.Key.Substring(5);

            if (int.TryParse(KeyId, out int Result))
                return Result;

            return 0;
        }

        private int? GetQuestId()
        {
            if (ConnectedQuest == null)
                return null;

            string KeyId = ConnectedQuest.Key.Substring(6);

            if (int.TryParse(KeyId, out int Result))
                return Result;

            return 0;
        }

        public override string SortingName { get { return null; } }
    }
}
