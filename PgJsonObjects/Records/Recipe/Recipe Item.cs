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
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 0; } }
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
        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "ItemCode", ParseFieldItemCode },
            { "StackSize", ParseFieldStackSize },
            { "PercentChance", ParseFieldPercentChance },
            { "ItemKeys", ParseFieldItemKeys },
            { "Desc", ParseFieldDesc },
            { "ChanceToConsume", ParseFieldChanceToConsume },
            { "DurabilityConsumed", ParseFieldDurabilityConsumed },
            { "AttuneToCrafter", ParseFieldAttuneToCrafter },
        };

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
            { RecipeItemKey.Rarity_Uncommon, "Rarity:Uncommon" },
            { RecipeItemKey.Rarity_Rare, "Rarity:Rare" },
            { RecipeItemKey.MinRarity_Exceptional, "MinRarity:Exceptional" },
            { RecipeItemKey.MinRarity_Uncommon, "MinRarity:Uncommon" },
        };

        private static void ParseFieldItemCode(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseItemCode((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem ItemCode");
        }

        private void ParseItemCode(int RawItemCode, ParseErrorInfo ErrorInfo)
        {
            this.RawItemCode = RawItemCode;
        }

        private static void ParseFieldStackSize(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseStackSize((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem StackSize");
        }

        private void ParseStackSize(int RawStackSize, ParseErrorInfo ErrorInfo)
        {
            if (RawStackSize > 1)
                this.RawStackSize = RawStackSize;
            else
            {
                this.RawStackSize = 1;

                if (RawStackSize < 0)
                    ErrorInfo.AddInvalidObjectFormat("RecipeItem StackSize");
            }
        }

        private static void ParseFieldPercentChance(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParsePercentChance((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParsePercentChance(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem PercentChance");
        }

        private void ParsePercentChance(double RawPercentChance, ParseErrorInfo ErrorInfo)
        {
            this.RawPercentChance = RawPercentChance;
        }

        private static void ParseFieldItemKeys(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawItemKeys;
            if ((RawItemKeys = Value as ArrayList) != null)
                This.ParseItemKeys(RawItemKeys, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem ItemKeys");
        }

        private void ParseItemKeys(ArrayList RawItemKeys, ParseErrorInfo ErrorInfo)
        {
            List<RecipeItemKey> ParsedItemKeyList = new List<RecipeItemKey>();
            StringToEnumConversion<RecipeItemKey>.ParseList(RawItemKeys, RecipeItemKeyStringMap, ParsedItemKeyList, ErrorInfo);

            if (ParsedItemKeyList.Count > 1)
                ItemKeyList.AddRange(ParsedItemKeyList);
            else
                ItemKeyList.AddRange(ParsedItemKeyList);
        }

        private static void ParseFieldDesc(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDesc;
            if ((RawDesc = Value as string) != null)
                This.ParseDesc(RawDesc, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem Desc");
        }

        private void ParseDesc(string RawDesc, ParseErrorInfo ErrorInfo)
        {
            Desc = RawDesc;
        }

        private static void ParseFieldChanceToConsume(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseChanceToConsume((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseChanceToConsume(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem ChanceToConsume");
        }

        private void ParseChanceToConsume(double RawChanceToConsume, ParseErrorInfo ErrorInfo)
        {
            this.RawChanceToConsume = RawChanceToConsume;
        }

        private static void ParseFieldDurabilityConsumed(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseDurabilityConsumed((int)Value, ErrorInfo);
            else if (Value is decimal)
                This.ParseDurabilityConsumed(decimal.ToDouble((decimal)Value), ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem DurabilityConsumed");
        }

        private void ParseDurabilityConsumed(double RawDurabilityConsumed, ParseErrorInfo ErrorInfo)
        {
            this.RawDurabilityConsumed = RawDurabilityConsumed;
        }

        private static void ParseFieldAttuneToCrafter(RecipeItem This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is bool)
                This.ParseAttuneToCrafter((bool)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem AttuneToCrafter");
        }

        private void ParseAttuneToCrafter(bool RawAttuneToCrafter, ParseErrorInfo ErrorInfo)
        {
            this.RawAttuneToCrafter = RawAttuneToCrafter;
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
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

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
                            case RecipeItemKey.Rarity_Uncommon:
                            case RecipeItemKey.Rarity_Rare:
                            case RecipeItemKey.MinRarity_Exceptional:
                            case RecipeItemKey.MinRarity_Uncommon:
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
