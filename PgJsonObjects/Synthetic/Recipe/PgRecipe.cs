using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgRecipe : GenericPgObject, IPgRecipe
    {
        public PgRecipe(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public string Description { get { return GetString(0); } }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        public int? RawIconId { get { return GetInt(4); } }
        public List<RecipeItem> IngredientList { get { return GetObjectList(8, ref _IngredientList); } } private List<RecipeItem> _IngredientList;
        public string InternalName { get { return GetString(12); } }
        public string Name { get { return GetString(16); } }
        public List<RecipeItem> ResultItemList { get { return GetObjectList(20, ref _ResultItemList); } } private List<RecipeItem> _ResultItemList;
        public Skill Skill { get { return GetObject(24, ref _Skill); } } private Skill _Skill;
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        public int? RawSkillLevelReq { get { return GetInt(28); } }
        public List<RecipeResultEffect> ResultEffectList { get { return GetObjectList(32, ref _ResultEffectList); } } private List<RecipeResultEffect> _ResultEffectList;
        public Skill SortSkill { get { return GetObject(36, ref _SortSkill); } } private Skill _SortSkill;
        public List<RecipeKeyword> KeywordList { get { return GetEnumList(40, ref _KeywordList); } } private List<RecipeKeyword> _KeywordList;
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        public int? RawUsageDelay { get { return GetInt(44); } }
        public string UsageDelayMessage { get { return GetString(48); } }
        public RecipeAction ActionLabel { get { return GetEnum<RecipeAction>(52); } }
        public RecipeUsageAnimation UsageAnimation { get { return GetEnum<RecipeUsageAnimation>(54); } }
        public List<AbilityRequirement> OtherRequirementList { get { return GetObjectList(56, ref _OtherRequirementList); } } private List<AbilityRequirement> _OtherRequirementList;
        public List<RecipeCost> CostList { get { return GetObjectList(60, ref _CostList); } } private List<RecipeCost> _CostList;
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        public int? RawNumResultItems { get { return GetInt(64); } }
        public string UsageAnimationEnd { get { return GetString(68); } }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        public int? RawResetTimeInSeconds { get { return GetInt(72); } }
        public uint? DyeColor { get { return GetUInt(76); } }
        public Skill RewardSkill { get { return GetObject(80, ref _RewardSkill); } } private Skill _RewardSkill;
        public int RewardSkillXp { get { return RawRewardSkillXp.HasValue ? RawRewardSkillXp.Value : 0; } }
        public int? RawRewardSkillXp { get { return GetInt(84); } }
        public int RewardSkillXpFirstTime { get { return RawRewardSkillXpFirstTime.HasValue ? RawRewardSkillXpFirstTime.Value : 0; } }
        public int? RawRewardSkillXpFirstTime { get { return GetInt(88); } }
        public Recipe SharesResetTimerWith { get { return GetObject(92, ref _SharesResetTimerWith); } } private Recipe _SharesResetTimerWith;
        public string ItemMenuLabel { get { return GetString(96); } }
        public string RawItemMenuCategory { get { return GetString(100); } }
        public int ItemMenuCategoryLevel { get { return RawItemMenuCategoryLevel.HasValue ? RawItemMenuCategoryLevel.Value : 0; } }
        public int? RawItemMenuCategoryLevel { get { return GetInt(104); } }
        public Recipe PrereqRecipe { get { return GetObject(108, ref _PrereqRecipe); } } private Recipe _PrereqRecipe;
        public bool IsItemMenuKeywordReqSufficient { get { return RawIsItemMenuKeywordReqSufficient.HasValue && RawIsItemMenuKeywordReqSufficient.Value; } }
        public bool? RawIsItemMenuKeywordReqSufficient { get { return GetBool(112, 0); } }
        public ItemKeyword RecipeItemKeyword { get { return GetEnum<ItemKeyword>(114); } }
    }
}
