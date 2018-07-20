using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgStorageVault : MainPgObject<PgStorageVault>, IPgStorageVault
    {
        public PgStorageVault(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 40;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgStorageVault CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgStorageVault CreateNew(byte[] data, ref int offset)
        {
            return new PgStorageVault(data, ref offset);
        }

        public override void Init()
        {
            FromFavorListToTable(FavorLevelList, FavorLevelTable);

            AddLinkBack(MatchingNpc);
        }

        public override string Key { get { return GetString(0); } }
        public int Id { get { return RawId.HasValue ? RawId.Value : 0; } }
        public int? RawId { get { return GetInt(4); } }
        public IPgGameNpc MatchingNpc { get { return GetObject(8, ref _MatchingNpc, PgGameNpc.CreateNew); } } private IPgGameNpc _MatchingNpc;
        public int NumSlots { get { return RawNumSlots.HasValue ? RawNumSlots.Value : 0; } }
        public int? RawNumSlots { get { return GetInt(12); } }
        public string RequirementDescription { get { return GetString(16); } }
        public string InteractionFlagRequirement { get { return GetString(20); } }
        public string NpcFriendlyName { get { return GetString(24); } }
        public ItemKeyword RequiredItemKeyword { get { return GetEnum<ItemKeyword>(28); } }
        public MapAreaName Grouping { get { return GetEnum<MapAreaName>(30); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(32, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;
        public bool HasAssociatedNpc { get { return RawHasAssociatedNpc.HasValue && RawHasAssociatedNpc.Value; } }
        public bool? RawHasAssociatedNpc { get { return GetBool(36, 0); } }
        public MapAreaName Area { get { return GetEnum<MapAreaName>(38); } }
        public List<int> FavorLevelList { get { return GetIntList(40, ref _FavorLevelList); } } private List<int> _FavorLevelList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ID", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawId } },
            { "NpcFriendlyName", new FieldParser() {
                Type = FieldType.String,
                GetString = () => NpcFriendlyName } },
            { "Area", new FieldParser() {
                Type = FieldType.String,
                GetString = GetArea } },
            { "NumSlots", new FieldParser() {
                Type = FieldType.Integer,
                GetInteger = () => RawNumSlots } },
            { "HasAssociatedNpc", new FieldParser() {
                Type = FieldType.Bool,
                GetBool = () => RawHasAssociatedNpc } },
            { "Levels", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetLevels } },
            { "RequiredItemKeyword", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<ItemKeyword>.ToString(RequiredItemKeyword, null, ItemKeyword.Internal_None) } },
            { "RequirementDescription", new FieldParser() {
                Type = FieldType.String,
                GetString = () => RequirementDescription } },
            { "Grouping", new FieldParser() {
                Type = FieldType.String,
                GetString = GetGrouping } },
            { "Requirements", new FieldParser() {
                Type = FieldType.Object,
                GetObject = GetRequirements } },
        }; } }

        private string GetArea()
        {
            if (Area == MapAreaName.Several)
                return "*";
            else
                return "Area" + StringToEnumConversion<MapAreaName>.ToString(Area);
        }

        private IObjectContentGenerator GetLevels()
        {
            FavorLevelDesc Result = new FavorLevelDesc();

            for (int i = 0; i * 2 < FavorLevelList.Count; i++)
                Result.SetFavorLevel((Favor)FavorLevelList[(i * 2) + 0], FavorLevelList[(i * 2) + 1]);

            return Result;
        }

        private string GetGrouping()
        {
            if (Grouping == MapAreaName.Several)
                return "*";
            else
                return "Area" + StringToEnumConversion<MapAreaName>.ToString(Grouping, null);
        }

        private IObjectContentGenerator GetRequirements()
        {
            AbilityRequirementInteractionFlagSet Result;

            if (InteractionFlagRequirement == "Ivyn Gave Passcode")
                Result = new AbilityRequirementInteractionFlagSet("Ivyn_Gave_Passcode");
            else if (InteractionFlagRequirement == "Serbule Hills Tapestry Inn Chest")
                Result = new AbilityRequirementInteractionFlagSet("Serbule2_TapestryInnChest");
            else
                Result = null;

            if (Result != null)
            {
                List<string> FakeOrder = new List<string>() { "T", "InteractionFlag" };
                Result.CopyFieldTableOrder("Requirements", FakeOrder);
            }

            return Result;
        }

        public override string SortingName { get { return NpcFriendlyName; } }
        public string SearchResultIconFileName { get { return "icon_" + StorageVault.SearchResultIconId; } }
        public Dictionary<Favor, int> FavorLevelTable { get; private set; } = new Dictionary<Favor, int>();

        public static void FromFavorListToTable(List<int> favorLevelList, Dictionary<Favor, int> favorLevelTable)
        {
            favorLevelTable.Clear();

            for (int i = 0; i * 2 < favorLevelList.Count; i++)
            {
                Favor FavorLevel = (Favor)favorLevelList[i * 2 + 0];
                int SlotCount = favorLevelList[i * 2 + 1];
                favorLevelTable.Add(FavorLevel, SlotCount);
            }
        }
    }
}
