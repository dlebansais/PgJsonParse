using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeItem : GenericJsonObject<RecipeItem>
    {
        #region Direct Properties
        public Item Item { get; private set; }
        public int ItemCode { get { return RawItemCode.HasValue ? RawItemCode.Value : 0; } }
        private int? RawItemCode;
        private bool IsItemCodeParsed;
        public int StackSize { get { return RawStackSize.HasValue ? (RawStackSize.Value > 0 ? RawStackSize.Value : 1) : 0; } }
        public int? RawStackSize { get; private set; }
        public double PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 0; } }
        public double? RawPercentChance { get; private set; }
        public List<RecipeItemKey> ItemKeyList { get; private set; } = new List<RecipeItemKey>();
        public List<Item> MatchingKeyItemList { get; } = new List<Item>();
        private bool IsItemKeyParsed;
        public string Desc { get; private set; }
        public double ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 0; } }
        public double? RawChanceToConsume { get; private set; }
        public double DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        public double? RawDurabilityConsumed { get; private set; }
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        public bool? RawAttuneToCrafter { get; private set; }
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        public Recipe ParentRecipe { get; private set; }
        #endregion

        #region Parsing
        public static readonly Dictionary<RecipeItemKey, string> RecipeItemKeyStringMap = new Dictionary<RecipeItemKey, string>()
        {
            { RecipeItemKey.EquipmentSlot_MainHand, "EquipmentSlot:MainHand" },
            { RecipeItemKey.EquipmentSlot_OffHand, "EquipmentSlot:OffHand" },
            { RecipeItemKey.EquipmentSlot_Hands, "EquipmentSlot:Hands" },
            { RecipeItemKey.EquipmentSlot_Chest, "EquipmentSlot:Chest" },
            { RecipeItemKey.EquipmentSlot_Legs, "EquipmentSlot:Legs" },
            { RecipeItemKey.EquipmentSlot_Head, "EquipmentSlot:Head" },
            { RecipeItemKey.EquipmentSlot_Feet, "EquipmentSlot:Feet" },
            { RecipeItemKey.EquipmentSlot_Ring, "EquipmentSlot:Ring" },
            { RecipeItemKey.EquipmentSlot_Necklace, "EquipmentSlot:Necklace" },
            { RecipeItemKey.Rarity_Common, "Rarity:Common" },
            { RecipeItemKey.Rarity_Uncommon, "Rarity:Uncommon" },
            { RecipeItemKey.Rarity_Rare, "Rarity:Rare" },
            { RecipeItemKey.MinRarity_Exceptional, "MinRarity:Exceptional" },
            { RecipeItemKey.MinRarity_Uncommon, "MinRarity:Uncommon" },
            { RecipeItemKey.Rarity_Exceptional, "Rarity:Exceptional" },
            { RecipeItemKey.MinRarity_Epic, "MinRarity:Epic" },
            { RecipeItemKey.MinTSysPrereq_0, "MinTSysPrereq:0" },
            { RecipeItemKey.MaxTSysPrereq_30, "MaxTSysPrereq:30" },
            { RecipeItemKey.MinTSysPrereq_31, "MinTSysPrereq:31" },
            { RecipeItemKey.MaxTSysPrereq_60, "MaxTSysPrereq:60" },
            { RecipeItemKey.MinTSysPrereq_61, "MinTSysPrereq:61" },
            { RecipeItemKey.MaxTSysPrereq_90, "MaxTSysPrereq:90" },
        };

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "ItemCode", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawItemCode = value,
                GetInteger = () => RawItemCode } },
            { "StackSize", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = ParseStackSize,
                GetInteger = () => RawStackSize } },
            { "PercentChance", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawPercentChance = value,
                GetFloat = () => RawPercentChance } },
            { "ItemKeys", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => StringToEnumConversion<RecipeItemKey>.ParseList(value, RecipeItemKeyStringMap, ItemKeyList, errorInfo),
                GetStringArray = () => StringToEnumConversion<RecipeItemKey>.ToStringList(ItemKeyList, RecipeItemKeyStringMap) } },
            { "Desc", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Desc = value,
                GetString = () => Desc } },
            { "ChanceToConsume", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawChanceToConsume = value,
                GetFloat = () => RawChanceToConsume } },
            { "DurabilityConsumed", new FieldParser() {
                Type = FieldType.Float,
                ParseFloat = (float value, ParseErrorInfo errorInfo) => RawDurabilityConsumed = value,
                GetFloat = () => RawDurabilityConsumed } },
            { "AttuneToCrafter", new FieldParser() {
                Type = FieldType.Bool,
                ParseBool = (bool value, ParseErrorInfo errorInfo) => RawAttuneToCrafter = value,
                GetBool = () => RawAttuneToCrafter } },
        }; } }

        private void ParseStackSize(int value, ParseErrorInfo ErrorInfo)
        {
            RawStackSize = value;

            if (value < 0)
                ErrorInfo.AddInvalidObjectFormat("RecipeItem StackSize");
        }
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(null);

            Generator.AddInteger("ItemCode", RawItemCode);
            Generator.AddInteger("StackSize", RawStackSize);
            Generator.AddString("Desc", Desc);
            Generator.AddDouble("ChanceToConsume", RawChanceToConsume);
            Generator.AddDouble("DurabilityConsumed", RawDurabilityConsumed);
            Generator.AddDouble("PercentChance", RawPercentChance);
            Generator.AddBoolean("AttuneToCrafter", RawAttuneToCrafter);

            Generator.CloseObject();
        }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Item != null)
                    AddWithFieldSeparator(ref Result, Item.Name);
                foreach (RecipeItemKey ItemKey in ItemKeyList)
                    AddWithFieldSeparator(ref Result, TextMaps.RecipeItemKeyTextMap[ItemKey]);
                foreach (Item Item in MatchingKeyItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);
                AddWithFieldSeparator(ref Result, Desc);
                if (AttuneToCrafter)
                    AddWithFieldSeparator(ref Result, "Is Attuned To Crafter");

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];

            ParentRecipe = Parent as Recipe;

            Item = Item.ConnectByCode(ErrorInfo, ItemTable, RawItemCode, Item, ref IsItemCodeParsed, ref IsConnected, this);
            if (Item == null && !IsItemKeyParsed)
            {
                if (ItemKeyList.Count == 0)
                {
                    IsItemKeyParsed = true;
                    ErrorInfo.AddInvalidObjectFormat("Recipe Item");
                }
                else
                {
                    foreach (RecipeItemKey ItemKey in ItemKeyList)
                    {
                        switch (ItemKey)
                        {
                            case RecipeItemKey.EquipmentSlot_MainHand:
                            case RecipeItemKey.EquipmentSlot_OffHand:
                            case RecipeItemKey.EquipmentSlot_Hands:
                            case RecipeItemKey.EquipmentSlot_Chest:
                            case RecipeItemKey.EquipmentSlot_Legs:
                            case RecipeItemKey.EquipmentSlot_Head:
                            case RecipeItemKey.EquipmentSlot_Feet:
                            case RecipeItemKey.EquipmentSlot_Ring:
                            case RecipeItemKey.EquipmentSlot_Necklace:
                            case RecipeItemKey.Rarity_Common:
                            case RecipeItemKey.Rarity_Uncommon:
                            case RecipeItemKey.Rarity_Rare:
                            case RecipeItemKey.MinRarity_Exceptional:
                            case RecipeItemKey.MinRarity_Uncommon:
                            case RecipeItemKey.MinTSysPrereq_0:
                            case RecipeItemKey.MaxTSysPrereq_30:
                            case RecipeItemKey.MinTSysPrereq_31:
                            case RecipeItemKey.MaxTSysPrereq_60:
                            case RecipeItemKey.MinTSysPrereq_61:
                            case RecipeItemKey.MaxTSysPrereq_90:
                                break;

                            default:
                                List<Item> ParsedList = new List<Item>();
                                ParsedList = Item.ConnectByItemKey(ErrorInfo, ItemTable, ItemKey, ParsedList, ref IsItemKeyParsed, ref IsConnected, this);

                                foreach (Item Item in ParsedList)
                                    if (!MatchingKeyItemList.Contains(Item))
                                        MatchingKeyItemList.Add(Item);
                                break;
                        }
                    }
                }
            }

            return IsConnected;
        }
        #endregion

        #region Recursive Components Sum
        public double PerfectCottonRatio
        {
            get
            {
                if (Item != null)
                    return Item.PerfectCottonRatio;
                else
                    return 0;
            }
        }

        public void SetPerfectCottonRatio(double RecipePerfectCottonRatio)
        {
            if (Item != null && StackSize > 0)
                Item.SetPerfectCottonRatio((RecipePerfectCottonRatio * PercentChance) / StackSize);
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "RecipeItem"; } }
        #endregion
    }
}
