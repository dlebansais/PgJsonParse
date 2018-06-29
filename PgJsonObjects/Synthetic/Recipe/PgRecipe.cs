using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipe : MainPgObject<PgRecipe>, IPgRecipe
    {
        public PgRecipe(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 116;
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

        public string Description { get { return GetString(0); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(4); } }
        public RecipeItemCollection IngredientList { get { return GetObjectList(8, ref _IngredientList, RecipeItemCollection.CreateItem, () => new RecipeItemCollection()); } } private RecipeItemCollection _IngredientList;
        public string InternalName { get { return GetString(12); } }
        public string Name { get { return GetString(16); } }
        public RecipeItemCollection ResultItemList { get { return GetObjectList(20, ref _ResultItemList, RecipeItemCollection.CreateItem, () => new RecipeItemCollection()); } } private RecipeItemCollection _ResultItemList;
        public IPgSkill Skill { get { return GetObject(24, ref _Skill, PgSkill.CreateNew); } } private IPgSkill _Skill;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get { return GetInt(28); } }
        //public RecipeResultEffectCollection ResultEffectList { get { return GetObjectList(32, ref _ResultEffectList, (byte[] data, ref int offset) => new PgRecipeResultEffect(data, offset), () => new RecipeResultEffectCollection()); } } private RecipeResultEffectCollection _ResultEffectList;
        public IPgSkill SortSkill { get { return GetObject(36, ref _SortSkill, PgSkill.CreateNew); } } private IPgSkill _SortSkill;
        public List<RecipeKeyword> KeywordList { get { return GetEnumList(40, ref _KeywordList); } } private List<RecipeKeyword> _KeywordList;
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get { return GetInt(44); } }
        public string UsageDelayMessage { get { return GetString(48); } }
        public RecipeAction ActionLabel { get { return GetEnum<RecipeAction>(52); } }
        public RecipeUsageAnimation UsageAnimation { get { return GetEnum<RecipeUsageAnimation>(54); } }
        public AbilityRequirementCollection OtherRequirementList { get { return GetObjectList(56, ref _OtherRequirementList, AbilityRequirementCollection.CreateItem, () => new AbilityRequirementCollection()); } } private AbilityRequirementCollection _OtherRequirementList;
        public RecipeCostCollection CostList { get { return GetObjectList(60, ref _CostList, RecipeCostCollection.CreateItem, () => new RecipeCostCollection()); } } private RecipeCostCollection _CostList;
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get { return GetInt(64); } }
        public string UsageAnimationEnd { get { return GetString(68); } }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get { return GetInt(72); } }
        public uint? DyeColor { get { return GetUInt(76); } }
        public IPgSkill RewardSkill { get { return GetObject(80, ref _RewardSkill, PgSkill.CreateNew); } } private IPgSkill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(84); } }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get { return GetInt(88); } }
        public IPgRecipe SharesResetTimerWith { get { return GetObject(92, ref _SharesResetTimerWith, CreateNew); } } private IPgRecipe _SharesResetTimerWith;
        public string ItemMenuLabel { get { return GetString(96); } }
        public string RawItemMenuCategory { get { return GetString(100); } }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get { return GetInt(104); } }
        public IPgRecipe PrereqRecipe { get { return GetObject(108, ref _PrereqRecipe, CreateNew); } } private IPgRecipe _PrereqRecipe;
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get { return GetBool(112, 0); } }
        public ItemKeyword RecipeItemKeyword { get { return GetEnum<ItemKeyword>(114); } }
    }
}
