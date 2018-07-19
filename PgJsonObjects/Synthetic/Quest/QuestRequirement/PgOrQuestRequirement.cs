using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgOrQuestRequirement : PgQuestRequirement<PgOrQuestRequirement>, IPgOrQuestRequirement
    {
        public PgOrQuestRequirement(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgOrQuestRequirement CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgOrQuestRequirement CreateNew(byte[] data, ref int offset)
        {
            return new PgOrQuestRequirement(data, ref offset);
        }

        public IPgQuestRequirementCollection OrList { get { return GetObjectList(PropertiesOffset + 0, ref _OrList, PgQuestRequirementCollection.CreateItem, () => new PgQuestRequirementCollection()); } } private IPgQuestRequirementCollection _OrList;

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "T", new FieldParser() {
                Type = FieldType.String,
                GetString = () => StringToEnumConversion<OtherRequirementType>.ToString(OtherRequirementType, null, OtherRequirementType.Internal_None) } },
            { "List", new FieldParser() {
                Type = FieldType.ObjectArray,
                GetObjectArray = () => OrList } },
        }; } }

        public override IList<IBackLinkable> GetLinkBack()
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();

            if (OrList != null)
                foreach (IPgQuestRequirement Item in OrList)
                {
                    IList<IBackLinkable> ItemResult = Item.GetLinkBack();
                    if (ItemResult != null)
                        Result.AddRange(ItemResult);
                }

            return Result;
        }
    }
}
