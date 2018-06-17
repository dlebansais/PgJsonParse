namespace PgJsonObjects
{
    public class PgMinFavorLevelQuestRequirement : GenericPgObject<PgMinFavorLevelQuestRequirement>, IPgMinFavorLevelQuestRequirement
    {
        public PgMinFavorLevelQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        protected override PgMinFavorLevelQuestRequirement CreateItem(byte[] data, int offset)
        {
            return new PgMinFavorLevelQuestRequirement(data, offset);
        }

        public GameNpc FavorNpc { get { return GetObject(4, ref _FavorNpc); } } private GameNpc _FavorNpc;
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get { return GetBool(8, 0); } }
        public Favor FavorLevel { get { return GetEnum<Favor>(10); } }
    }
}
