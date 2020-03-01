using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PgJsonObjects
{
    public class PgRecipeSource : MainPgObject<PgRecipeSource>, IPgRecipeSource
    {
        public PgRecipeSource(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 46;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgRecipeSource CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipeSource CreateNew(byte[] data, ref int offset)
        {
            PgRecipeSource Result = new PgRecipeSource(data, ref offset);
            IPgRecipe ConnectedRecipe = Result.ConnectedRecipe;
            IPgGameNpc Npc = Result.Npc;
            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public IPgRecipe ConnectedRecipe { get { return GetObject(4, ref _ConnectedRecipe, PgRecipe.CreateNew); } } private IPgRecipe _ConnectedRecipe;
        public IPgSkill SkillTypeId { get { return GetObject(8, ref _SkillTypeId, PgSkill.CreateNew); } } private IPgSkill _SkillTypeId;
        public IPgItem ConnectedItem { get { return GetObject(12, ref _ConnectedItem, PgItem.CreateNew); } } private IPgItem _ConnectedItem;
        public IPgGameNpc Npc { get { return GetObject(16, ref _Npc, PgGameNpc.CreateNew); } } private IPgGameNpc _Npc;
        public IPgEffect ConnectedEffect { get { return GetObject(20, ref _ConnectedEffect, PgEffect.CreateNew); } } private IPgEffect _ConnectedEffect;
        public IPgQuest ConnectedQuest { get { return GetObject(24, ref _ConnectedQuest, PgQuest.CreateNew); } } private IPgQuest _ConnectedQuest;
        protected override List<string> FieldTableOrder { get { return GetStringList(28, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public string RawNpcId { get { return GetString(32); } }
        public string RawNpcName { get { return GetString(36); } }
        public string RawEffectTypeId { get { return GetString(40); } }
        public SourceType Type { get { return GetEnum<SourceType>(44); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Type", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<SourceType>.ToString(Type, null, SourceType.Internal_None) } },
            { "SkillTypeId", new FieldParser() {
                Type = FieldType.String,
                GetString = () => SkillTypeId != null ? StringToEnumConversion<PowerSkill>.ToString(SkillTypeId.CombatSkill, null, PowerSkill.Internal_None) : null } },
            { "ItemTypeId", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = GetItemTypeId } },
            { "Npc", new FieldParser() {
                Type = FieldType.String,
                GetString = () => Quest.NpcToString(RawNpcId, RawNpcName) } },
            { "EffectName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => ConnectedEffect != null ? ConnectedEffect.Name : null } },
            { "EffectTypeId", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RawEffectTypeId } },
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
