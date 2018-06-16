namespace PgJsonObjects
{
    public class PgMinFavorLevelQuestRequirement : GenericPgObject, IPgMinFavorLevelQuestRequirement
    {
        public PgMinFavorLevelQuestRequirement(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public GameNpc FavorNpc { get { return GetObject(0, ref _FavorNpc); } } private GameNpc _FavorNpc;
        public bool IsEmpty { get { return RawIsEmpty.HasValue && RawIsEmpty.Value; } }
        public bool? RawIsEmpty { get { return GetBool(4, 0); } }
        public Favor FavorLevel { get { return GetEnum<Favor>(6); } }
    }
}
