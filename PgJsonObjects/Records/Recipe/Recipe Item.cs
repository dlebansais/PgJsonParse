using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class RecipeItem : GenericJsonObject<RecipeItem>
    {
        #region Constant
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

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
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
        #endregion

        #region Properties
        public Item Item { get; private set; }
        public int ItemCode { get { return RawItemCode.HasValue ? RawItemCode.Value : 0; } }
        private int? RawItemCode;
        private bool IsItemCodeParsed;
        public int StackSize { get { return RawStackSize.HasValue ? RawStackSize.Value : 0; } }
        private int? RawStackSize;
        public double PercentChance { get { return RawPercentChance.HasValue ? RawPercentChance.Value : 1.0; } }
        private double? RawPercentChance;
        public RecipeItemKey ItemKey { get; private set; }
        public List<Item> MatchingKeyItemList { get; private set; }
        private bool IsItemKeyParsed;
        public string Desc { get; private set; }
        public double ChanceToConsume { get { return RawChanceToConsume.HasValue ? RawChanceToConsume.Value : 1.0; } }
        private double? RawChanceToConsume;
        public double DurabilityConsumed { get { return RawDurabilityConsumed.HasValue ? RawDurabilityConsumed.Value : 0; } }
        private double? RawDurabilityConsumed;
        public bool AttuneToCrafter { get { return RawAttuneToCrafter.HasValue && RawAttuneToCrafter.Value; } }
        private bool? RawAttuneToCrafter;

        protected override string SortingName { get { return null; } }
        public Recipe ParentRecipe { get; private set; }

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

        public bool IsLinkedDescription
        {
            get
            {
                if (IsSingleLinkLinkDescription)
                    return true;

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
                        return false;

                    default:
                        return (MatchingKeyItemList.Count < 10);
                }
            }
        }

        public bool IsSingleLinkLinkDescription { get { return Item != null; } }

        public string CombinedDescription
        {
            get
            {
                if (Item != null)
                    return Item.Name;

                string Result;

                switch (ItemKey)
                {
                    case RecipeItemKey.EquipmentSlot_MainHand:
                        return "Any Main Hand";
                    case RecipeItemKey.EquipmentSlot_OffHand:
                        return "Any Off Hand";
                    case RecipeItemKey.EquipmentSlot_Hands:
                        return "Any Hands";
                    case RecipeItemKey.EquipmentSlot_Chest:
                        return "Any Chest";
                    case RecipeItemKey.EquipmentSlot_Legs:
                        return "Any Legs";
                    case RecipeItemKey.EquipmentSlot_Head:
                        return "Any Head";
                    case RecipeItemKey.EquipmentSlot_Feet:
                        return "Any Feet";
                    case RecipeItemKey.EquipmentSlot_Ring:
                        return "Any Ring";
                    case RecipeItemKey.EquipmentSlot_Necklace:
                        return "Any Necklace";
                    case RecipeItemKey.Rarity_Uncommon:
                        return "Any uncommon item";
                    case RecipeItemKey.Rarity_Rare:
                        return "Any rare item";
                    case RecipeItemKey.MinRarity_Exceptional:
                        return "Any exceptional item or better";
                    case RecipeItemKey.MinRarity_Uncommon:
                        return "Any magical item";

                    default:
                        if (MatchingKeyItemList.Count >= 10)
                        {
                            return "Any " + TextMaps.RecipeItemKeyTextMap[ItemKey];
                        }
                        else
                        {
                            Result = "";

                            foreach (Item Item in MatchingKeyItemList)
                            {
                                if (Result.Length > 0)
                                    Result += ", ";

                                Result += Item.Name;
                            }

                            if (Result.Length > 0)
                                Result = "One of: " + Result;
                        }

                        return Result;
                }
            }
        }

        public string ExtraInfo
        {
            get
            {
                if (StackSize > 1)
                    return "x" + StackSize;
                else if (RawChanceToConsume.HasValue && RawChanceToConsume.Value > 0 && RawChanceToConsume.Value < 1)
                    return " (" + (RawChanceToConsume.Value* 100).ToString() + "% chance to consume)";
                else if (RawDurabilityConsumed.HasValue && RawDurabilityConsumed.Value > 0 && RawDurabilityConsumed.Value < 1)
                    return " (" + (RawDurabilityConsumed.Value * 100).ToString() + "% durability consumed)";
                else
                    return null;
            }
        }
        #endregion

        #region Client Interface
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
            this.RawStackSize = RawStackSize;
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

            if (ParsedItemKeyList.Count == 1)
            {
                ItemKey = ParsedItemKeyList[0];
                IsItemKeyParsed = false;
            }
            else
                ErrorInfo.AddInvalidObjectFormat("RecipeItem ItemKeys");
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

        public override string TextContent
        {
            get
            {
                string Result = "";

                Result += Desc + JsonGenerator.FieldSeparator;

                return Result;
            }
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "RecipeItem"; } }

        protected override void InitializeFields()
        {
            MatchingKeyItemList = new List<Item>();
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable, Dictionary<string, XpTable> XpTableTable, Dictionary<string, AdvancementTable> AdvancementTableTable)
        {
            bool IsConnected = false;

            ParentRecipe = Parent as Recipe;

            Item = Item.ConnectByCode(ErrorInfo, ItemTable, RawItemCode, Item, ref IsItemCodeParsed, ref IsConnected, this);
            if (Item == null)
            {
                switch (ItemKey)
                {
                    case RecipeItemKey.Internal_None:
                        if (!IsItemKeyParsed)
                        {
                            IsItemKeyParsed = true;
                            ErrorInfo.AddInvalidObjectFormat("Recipe Item");
                        }
                        break;

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
                        MatchingKeyItemList = Item.ConnectByKey(ErrorInfo, ItemTable, ItemKey, MatchingKeyItemList, ref IsItemKeyParsed, ref IsConnected, this);
                        break;
                }
            }

            return IsConnected;
        }
        #endregion
    }
}
