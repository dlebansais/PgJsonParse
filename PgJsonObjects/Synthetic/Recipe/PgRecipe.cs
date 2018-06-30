using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipe : MainPgObject<PgRecipe>, IPgRecipe
    {
        public PgRecipe(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 120;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgRecipe CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgRecipe CreateNew(byte[] data, ref int offset)
        {
            PgRecipe Result = new PgRecipe(data, ref offset);
            return Result;
        }

        public override string Key { get { return GetString(0); } }
        public string Description { get { return GetString(4); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(8); } }
        public RecipeItemCollection IngredientList { get { return GetObjectList(12, ref _IngredientList, RecipeItemCollection.CreateItem, () => new RecipeItemCollection()); } } private RecipeItemCollection _IngredientList;
        public string InternalName { get { return GetString(16); } }
        public string Name { get { return GetString(20); } }
        public RecipeItemCollection ResultItemList { get { return GetObjectList(24, ref _ResultItemList, RecipeItemCollection.CreateItem, () => new RecipeItemCollection()); } } private RecipeItemCollection _ResultItemList;
        public IPgSkill Skill { get { return GetObject(28, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get { return GetInt(32); } }
        //public RecipeResultEffectCollection ResultEffectList { get { return GetObjectList(32, ref _ResultEffectList, (byte[] data, ref int offset) => new PgRecipeResultEffect(data, offset), () => new RecipeResultEffectCollection()); } } private RecipeResultEffectCollection _ResultEffectList;
        public IPgSkill SortSkill { get { return GetObject(40, ref _SortSkill, PgSkill.CreateNew); } } private IPgSkill _SortSkill;
        public List<RecipeKeyword> KeywordList { get { return GetEnumList(44, ref _KeywordList); } } private List<RecipeKeyword> _KeywordList;
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get { return GetInt(48); } }
        public string UsageDelayMessage { get { return GetString(52); } }
        public RecipeAction ActionLabel { get { return GetEnum<RecipeAction>(56); } }
        public RecipeUsageAnimation UsageAnimation { get { return GetEnum<RecipeUsageAnimation>(58); } }
        public AbilityRequirementCollection OtherRequirementList { get { return GetObjectList(60, ref _OtherRequirementList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OtherRequirementList;
        public RecipeCostCollection CostList { get { return GetObjectList(64, ref _CostList, RecipeCostCollection.CreateItem, () => new RecipeCostCollection()); } } private RecipeCostCollection _CostList;
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get { return GetInt(68); } }
        public string UsageAnimationEnd { get { return GetString(72); } }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get { return GetInt(76); } }
        public uint? DyeColor { get { return GetUInt(80); } }
        public IPgSkill RewardSkill { get { return GetObject(84, ref _RewardSkill, PgSkill.CreateNew); } } private IPgSkill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(88); } }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get { return GetInt(92); } }
        public IPgRecipe SharesResetTimerWith { get { return GetObject(96, ref _SharesResetTimerWith, CreateNew); } } private IPgRecipe _SharesResetTimerWith;
        public string ItemMenuLabel { get { return GetString(100); } }
        public string RawItemMenuCategory { get { return GetString(104); } }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get { return GetInt(108); } }
        public IPgRecipe PrereqRecipe { get { return GetObject(112, ref _PrereqRecipe, CreateNew); } } private IPgRecipe _PrereqRecipe;
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get { return GetBool(116, 0); } }
        public ItemKeyword RecipeItemKeyword { get { return GetEnum<ItemKeyword>(118); } }

        protected override Dictionary<string, FieldParser> FieldTable { get { return FieldTable; } }
        protected override List<string> FieldTableOrder { get { return FieldTableOrder; } }
    }
}
