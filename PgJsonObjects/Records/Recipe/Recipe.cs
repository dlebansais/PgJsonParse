using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace PgJsonObjects
{
    public class Recipe : GenericJsonObject<Recipe>
    {
        #region Constants
        public static readonly Dictionary<RecipeEffect, string> RecipeEffectStringMap = new Dictionary<RecipeEffect, string>()
        {
            { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySerbule0, "CreateGeologySurveyRedwall(GeologySurveySerbule0)" },
            { RecipeEffect.CreateGeologySurveyBlue_GeologySurveySerbule1, "CreateGeologySurveyBlue(GeologySurveySerbule1)" },
            { RecipeEffect.CreateGeologySurveyGreen_GeologySurveySerbule2, "CreateGeologySurveyGreen(GeologySurveySerbule2)" },
            { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySerbule4, "CreateGeologySurveyWhite(GeologySurveySerbule4)" },
            { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySouthSerbule0, "CreateGeologySurveyRedwall(GeologySurveySouthSerbule0)" },
            { RecipeEffect.CreateGeologySurveyOrange_GeologySurveySouthSerbule3, "CreateGeologySurveyOrange(GeologySurveySouthSerbule3)" },
            { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySouthSerbule4, "CreateGeologySurveyWhite(GeologySurveySouthSerbule4)" },
            { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyEltibule1, "CreateGeologySurveyBlue(GeologySurveyEltibule1)" },
            { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyEltibule2, "CreateGeologySurveyGreen(GeologySurveyEltibule2)" },
            { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyEltibule3, "CreateGeologySurveyOrange(GeologySurveyEltibule3)" },
            { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyKurMountains1, "CreateGeologySurveyBlue(GeologySurveyKurMountains1)" },
            { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyKurMountains2, "CreateGeologySurveyGreen(GeologySurveyKurMountains2)" },
            { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyKurMountains3, "CreateGeologySurveyOrange(GeologySurveyKurMountains3)" },
            { RecipeEffect.CreateGeologySurveyWhite_GeologySurveyKurMountains4, "CreateGeologySurveyWhite(GeologySurveyKurMountains4)" },
            { RecipeEffect.CreateMiningSurvey1X_MiningSurveyKurMountains1X, "CreateMiningSurvey1X(MiningSurveyKurMountains1X)" },
            { RecipeEffect.CreateMiningSurvey2X_MiningSurveyKurMountains2X, "CreateMiningSurvey2X(MiningSurveyKurMountains2X)" },
            { RecipeEffect.CreateMiningSurvey3X_MiningSurveyKurMountains3X, "CreateMiningSurvey3X(MiningSurveyKurMountains3X)" },
            { RecipeEffect.CreateMiningSurvey1_MiningSurveySouthSerbule1, "CreateMiningSurvey1(MiningSurveySouthSerbule1)" },
            { RecipeEffect.CreateMiningSurvey2_MiningSurveySouthSerbule2, "CreateMiningSurvey2(MiningSurveySouthSerbule2)" },
            { RecipeEffect.CreateMiningSurvey3_MiningSurveyEltibule3, "CreateMiningSurvey3(MiningSurveyEltibule3)" },
            { RecipeEffect.CreateMiningSurvey4_MiningSurveyEltibule4, "CreateMiningSurvey4(MiningSurveyEltibule4)" },
            { RecipeEffect.CreateMiningSurvey5_MiningSurveyEltibule5, "CreateMiningSurvey5(MiningSurveyEltibule5)" },
            { RecipeEffect.CreateMiningSurvey6_MiningSurveyEltibule6, "CreateMiningSurvey6(MiningSurveyEltibule6)" },
            { RecipeEffect.CreateMiningSurvey4X_MiningSurveyIlmari4X, "CreateMiningSurvey4X(MiningSurveyIlmari4X)" },
            { RecipeEffect.CreateMiningSurvey5X_MiningSurveyIlmari5X, "CreateMiningSurvey5X(MiningSurveyIlmari5X)" },
            { RecipeEffect.CreateMiningSurvey6X_MiningSurveyIlmari6X, "CreateMiningSurvey6X(MiningSurveyIlmari6X)" },
            { RecipeEffect.CreateMiningSurvey7X_MiningSurveyIlmari7X, "CreateMiningSurvey7X(MiningSurveyIlmari7X)" },
        };
        public static readonly Dictionary<RecipeAction, string> RecipeActionStringMap = new Dictionary<RecipeAction, string>()
        {
            { RecipeAction.DecomposeItem, "Decompose Item" },
            { RecipeAction.RemoveAugment, "Remove Augment" },
            { RecipeAction.DistillItem, "Distill Item" },
            { RecipeAction.RepairItem, "Repair Item" },
            { RecipeAction.CreateMap, "Create Map" },
            { RecipeAction.CraftIceBlade, "Craft Ice Blade" },
            { RecipeAction.StudySkull, "Study Skull" },
            { RecipeAction.StudyEquipment, "Study Equipment" },
            { RecipeAction.SayTheSooth, "Say the Sooth" },
            { RecipeAction.MixDye, "Mix Dye" },
            { RecipeAction.InfuseSpirits, "Infuse Spirits" },
            { RecipeAction.CreateSpiritBelt, "Create Spirit Belt" },
            { RecipeAction.ApplyAugment, "Apply Augment" },
        };

        private Dictionary<string, FieldValueHandler> _FieldTable = new Dictionary<string, FieldValueHandler>()
        {
            { "Description", ParseFieldDescription },
            { "IconId", ParseFieldIconId },
            { "Ingredients", ParseFieldIngredients },
            { "InternalName", ParseFieldInternalName },
            { "Name", ParseFieldName },
            { "ResultItems", ParseFieldResultItems },
            { "ServerInfo", ParseFieldServerInfo },
            { "Skill", ParseFieldSkill },
            { "SkillLevelReq", ParseFieldSkillLevelReq },
            { "ResultEffects", ParseFieldResultEffects },
            { "SortSkill", ParseFieldSortSkill },
            { "Keywords", ParseFieldKeywords },
            { "ActionLabel", ParseFieldActionLabel },
            { "UsageDelay", ParseFieldUsageDelay },
            { "UsageDelayMessage", ParseFieldUsageDelayMessage },
            { "UsageAnimation", ParseFieldUsageAnimation },
            { "OtherRequirements", ParseFieldOtherRequirements },
            { "Costs", ParseFieldCosts },
            { "NumResultItems", ParseFieldNumResultItems },
            { "UsageAnimationEnd", ParseFieldUsageAnimationEnd },
            { "ResetTimeInSeconds", ParseFieldResetTimeInSeconds },
            { "DyeColor", ParseFieldDyeColor },
        };
        #endregion

        #region Properties
        public string Description { get; private set; }
        public int IconId { get { return RawIconId.HasValue ? RawIconId.Value : 0; } }
        private int? RawIconId;
        public List<RecipeItem> IngredientList { get; private set; }
        private bool EmptyIngredientList;
        public string InternalName { get; private set; }
        public string Name { get; private set; }
        public List<RecipeItem> ResultItemList { get; private set; }
        private bool EmptyResultItemList;
        public ServerInfo ServerInfo { get; private set; }
        public PowerSkill Skill { get; private set; }
        public int SkillLevelReq { get { return RawSkillLevelReq.HasValue ? RawSkillLevelReq.Value : 0; } }
        private int? RawSkillLevelReq;
        public List<RecipeResultEffect> ResultEffectList { get; private set; }
        public PowerSkill SortSkill { get; private set; }
        public List<RecipeKeyword> KeywordList { get; private set; }
        public RecipeAction ActionLabel { get; private set; }
        public int UsageDelay { get { return RawUsageDelay.HasValue ? RawUsageDelay.Value : 0; } }
        private int? RawUsageDelay;
        public string UsageDelayMessage { get; private set; }
        public RecipeUsageAnimation UsageAnimation { get; private set; }
        public List<OtherRequirementType> OtherRequirementList { get; private set; }
        public float CurHealth { get; private set; }
        public Race RequiredRace { get; private set; }
        public AnimalForm RequiredForm { get; private set; }
        public List<DisallowedState> DisallowedDruidStateList { get; private set; }
        public RecipeKeyword PetType { get; private set; }
        public int PetTypeMaxCount { get { return RawPetTypeMaxCount.HasValue ? RawPetTypeMaxCount.Value : 0; } }
        private int? RawPetTypeMaxCount;
        public List<RecipeCost> CostList { get; private set; }
        public int NumResultItems { get { return RawNumResultItems.HasValue ? RawNumResultItems.Value : 0; } }
        private int? RawNumResultItems;
        public string UsageAnimationEnd { get; private set; }
        public int ResetTimeInSeconds { get { return RawResetTimeInSeconds.HasValue ? RawResetTimeInSeconds.Value : 0; } }
        private int? RawResetTimeInSeconds;
        public uint? DyeColor;
        #endregion

        #region Client Interface
        private static void ParseFieldDescription(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDescription;
            if ((RawDescription = Value as string) != null)
                This.ParseDescription(RawDescription, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Description");
        }

        private void ParseDescription(string RawDescription, ParseErrorInfo ErrorInfo)
        {
            Description = RawDescription;
            ErrorInfo.ParseIconId(Description);
        }

        private static void ParseFieldIconId(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseIconId((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe IconId");
        }

        private void ParseIconId(int RawIconId, ParseErrorInfo ErrorInfo)
        {
            this.RawIconId = RawIconId;
        }

        private static void ParseFieldIngredients(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawIngredients;
            if ((RawIngredients = Value as ArrayList) != null)
                This.ParseIngredients(RawIngredients, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Ingredients");
        }

        private void ParseIngredients(ArrayList RawIngredients, ParseErrorInfo ErrorInfo)
        {
            List<RecipeItem> ParsedIngredientList;
            JsonObjectParser<RecipeItem>.InitAsSublist(RawIngredients, out ParsedIngredientList, ErrorInfo);
            IngredientList = ParsedIngredientList;
            EmptyIngredientList = (IngredientList.Count == 0);
        }

        private static void ParseFieldInternalName(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawInternalName;
            if ((RawInternalName = Value as string) != null)
                This.ParseInternalName(RawInternalName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe InternalName");
        }

        private void ParseInternalName(string RawInternalName, ParseErrorInfo ErrorInfo)
        {
            InternalName = RawInternalName;
        }

        private static void ParseFieldName(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawName;
            if ((RawName = Value as string) != null)
                This.ParseName(RawName, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Name");
        }

        private void ParseName(string RawName, ParseErrorInfo ErrorInfo)
        {
            Name = RawName;
        }

        private static void ParseFieldResultItems(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawResultItems;
            if ((RawResultItems = Value as ArrayList) != null)
                This.ParseResultItems(RawResultItems, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResultItems");
        }

        private void ParseResultItems(ArrayList RawResultItems, ParseErrorInfo ErrorInfo)
        {
            List<RecipeItem> ParsedResultItemList;
            JsonObjectParser<RecipeItem>.InitAsSublist(RawResultItems, out ParsedResultItemList, ErrorInfo);
            ResultItemList = ParsedResultItemList;
            EmptyResultItemList = (ResultItemList.Count == 0);
        }

        private static void ParseFieldServerInfo(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            Dictionary<string, object> RawServerInfo;
            if ((RawServerInfo = Value as Dictionary<string, object>) != null)
                This.ParseServerInfo(RawServerInfo, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ServerInfo");
        }

        private void ParseServerInfo(Dictionary<string, object> RawServerInfo, ParseErrorInfo ErrorInfo)
        {
            ServerInfo ParsedServerInfo;
            JsonObjectParser<ServerInfo>.InitAsSubitem("ServerInfo", RawServerInfo, out ParsedServerInfo, ErrorInfo);
            ServerInfo = ParsedServerInfo;
        }

        private static void ParseFieldSkill(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSkill;
            if ((RawSkill = Value as string) != null)
                This.ParseSkill(RawSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Skill");
        }

        private void ParseSkill(string RawSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSkill, out ParsedSkill, ErrorInfo);
            Skill = ParsedSkill;
        }

        private static void ParseFieldSkillLevelReq(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseSkillLevelReq((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe SkillLevelReq");
        }

        private void ParseSkillLevelReq(int RawSkillLevelReq, ParseErrorInfo ErrorInfo)
        {
            this.RawSkillLevelReq = RawSkillLevelReq;
        }

        private static void ParseFieldResultEffects(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawResultEffects;
            if ((RawResultEffects = Value as ArrayList) != null)
                This.ParseResultEffects(RawResultEffects, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResultEffects");
        }

        private void ParseResultEffects(ArrayList RawResultEffects, ParseErrorInfo ErrorInfo)
        {
            foreach (object Item in RawResultEffects)
            {
                string RawEffect;
                if ((RawEffect = Item as string) !=  null)
                {
                    RecipeResultEffect NewResultEffect;
                    if (ParseResultEffect(RawEffect, ErrorInfo, out NewResultEffect))
                        ResultEffectList.Add(NewResultEffect);
                }
                else
                    ErrorInfo.AddInvalidObjectFormat("Recipe ResultEffects");
            }
        }

        private static void ParseFieldSortSkill(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawSortSkill;
            if ((RawSortSkill = Value as string) != null)
                This.ParseSortSkill(RawSortSkill, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe SortSkill");
        }

        private void ParseSortSkill(string RawSortSkill, ParseErrorInfo ErrorInfo)
        {
            PowerSkill ParsedSortSkill;
            StringToEnumConversion<PowerSkill>.TryParse(RawSortSkill, out ParsedSortSkill, ErrorInfo);
            SortSkill = ParsedSortSkill;
        }

        private static void ParseFieldKeywords(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawKeywords;
            if ((RawKeywords = Value as ArrayList) != null)
                This.ParseKeywords(RawKeywords, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Keywords");
        }

        private void ParseKeywords(ArrayList RawKeywords, ParseErrorInfo ErrorInfo)
        {
            StringToEnumConversion<RecipeKeyword>.ParseList(RawKeywords, KeywordList, ErrorInfo);
        }

        private static void ParseFieldActionLabel(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawActionLabel;
            if ((RawActionLabel = Value as string) != null)
                This.ParseActionLabel(RawActionLabel, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ActionLabel");
        }

        private void ParseActionLabel(string RawActionLabel, ParseErrorInfo ErrorInfo)
        {
            RecipeAction ParsedActionLabel;
            StringToEnumConversion<RecipeAction>.TryParse(RawActionLabel, RecipeActionStringMap, out ParsedActionLabel, ErrorInfo);
            ActionLabel = ParsedActionLabel;
        }

        private static void ParseFieldUsageDelay(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseUsageDelay((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageDelay");
        }

        private void ParseUsageDelay(int RawUsageDelay, ParseErrorInfo ErrorInfo)
        {
            this.RawUsageDelay = RawUsageDelay;
        }

        private static void ParseFieldUsageDelayMessage(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageDelayMessage;
            if ((RawUsageDelayMessage = Value as string) != null)
                This.ParseUsageDelayMessage(RawUsageDelayMessage, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageDelayMessage");
        }

        private void ParseUsageDelayMessage(string RawUsageDelayMessage, ParseErrorInfo ErrorInfo)
        {
            UsageDelayMessage = RawUsageDelayMessage;
        }

        private static void ParseFieldUsageAnimation(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageAnimation;
            if ((RawUsageAnimation = Value as string) != null)
                This.ParseUsageAnimation(RawUsageAnimation, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageAnimation");
        }

        private void ParseUsageAnimation(string RawUsageAnimation, ParseErrorInfo ErrorInfo)
        {
            RecipeUsageAnimation ConvertedRecipeUsageAnimation;
            StringToEnumConversion<RecipeUsageAnimation>.TryParse(RawUsageAnimation, out ConvertedRecipeUsageAnimation, ErrorInfo);
            UsageAnimation = ConvertedRecipeUsageAnimation;
        }

        private static void ParseFieldOtherRequirements(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList AsArrayList;
            Dictionary<string, object> AsDictionary;

            if ((AsArrayList = Value as ArrayList) != null)
            {
                foreach (object Item in AsArrayList)
                {
                    if ((AsDictionary = Item as Dictionary<string, object>) != null)
                        This.ParseOtherRequirements(AsDictionary, ErrorInfo);
                    else
                        ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements");
                }
            }

            else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseOtherRequirements(AsDictionary, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements");
        }

        public void ParseOtherRequirements(Dictionary<string, object> Requirements, ParseErrorInfo ErrorInfo)
        {
            if (!Requirements.ContainsKey("T"))
            {
                ErrorInfo.AddMissingEnum("Recipe OtherRequirements", "T");
                return;
            }

            string RequirementString = Requirements["T"] as string;
            if (RequirementString == null)
            {
                ErrorInfo.AddInvalidObjectFormat("OtherRequirements Type");
                return;
            }

            OtherRequirementType RequirementTypeValue;
            if (!StringToEnumConversion<OtherRequirementType>.TryParse(RequirementString, out RequirementTypeValue, ErrorInfo))
                return;

            switch (RequirementTypeValue)
            {
                case OtherRequirementType.CurHealth:
                    if (Requirements.ContainsKey("Health"))
                    {
                        string HealthRequirement = Requirements["Health"] as string;
                        if (HealthRequirement == null)
                        {
                            ErrorInfo.AddInvalidObjectFormat("OtherRequirements Health");
                            return;
                        }

                        float NewCurHealth;
                        FloatFormat NewCurHealthFormat;
                        if (Tools.TryParseFloat(HealthRequirement, out NewCurHealth, out NewCurHealthFormat))
                        {
                            OtherRequirementList.Add(RequirementTypeValue);
                            CurHealth = NewCurHealth;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements Health");
                    break;

                case OtherRequirementType.Race:
                    if (Requirements.ContainsKey("AllowedRace"))
                    {
                        string RaceRequirement = Requirements["AllowedRace"] as string;
                        if (RaceRequirement == null)
                        {
                            ErrorInfo.AddInvalidObjectFormat("OtherRequirements AllowedRace");
                            return;
                        }

                        Race RaceValue;
                        if (StringToEnumConversion<Race>.TryParse(RaceRequirement, out RaceValue, ErrorInfo))
                        {
                            OtherRequirementList.Add(RequirementTypeValue);
                            RequiredRace = RaceValue;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements AllowedRace");
                    break;

                case OtherRequirementType.HasEffectKeyword:
                    if (Requirements.ContainsKey("Keyword"))
                    {
                        string KeywordRequirement = Requirements["Keyword"] as string;
                        if (KeywordRequirement == null)
                        {
                            ErrorInfo.AddInvalidObjectFormat("OtherRequirements Keyword");
                            return;
                        }

                        AnimalForm FormValue;
                        if (StringToEnumConversion<AnimalForm>.TryParse(KeywordRequirement, out FormValue, ErrorInfo))
                        {
                            OtherRequirementList.Add(RequirementTypeValue);
                            RequiredForm = FormValue;
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements Keyword");
                    break;

                case OtherRequirementType.DruidEventState:
                    if (Requirements.ContainsKey("DisallowedStates"))
                    {
                        ArrayList DisallowedStates = Requirements["DisallowedStates"] as ArrayList;
                        if (DisallowedStates == null || DisallowedStates.Count == 0)
                        {
                            ErrorInfo.AddInvalidObjectFormat("OtherRequirements DisallowedStates");
                            return;
                        }

                        OtherRequirementList.Add(RequirementTypeValue);

                        foreach (object StateObject in DisallowedStates)
                        {
                            string StateAsString = StateObject as string;
                            if (StateAsString == null)
                            {
                                ErrorInfo.AddInvalidObjectFormat("OtherRequirements DisallowedStates");
                                return;
                            }

                            DisallowedState DisallowedStateValue;
                            if (StringToEnumConversion<DisallowedState>.TryParse(StateAsString, out DisallowedStateValue, ErrorInfo))
                                DisallowedDruidStateList.Add(DisallowedStateValue);
                        }

                        break;
                    }

                    ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements Keyword");
                    break;

                case OtherRequirementType.PetCount:
                    if (Requirements.ContainsKey("PetTypeTag"))
                    {
                        string PetTypeTag = Requirements["PetTypeTag"] as string;
                        if (PetTypeTag == null)
                        {
                            ErrorInfo.AddInvalidObjectFormat("OtherRequirements PetTypeTag");
                            return;
                        }

                        RecipeKeyword KeywordValue;
                        if (StringToEnumConversion<RecipeKeyword>.TryParse(PetTypeTag, out KeywordValue, ErrorInfo))
                        {
                            OtherRequirementList.Add(RequirementTypeValue);
                            PetType = KeywordValue;

                            if (Requirements.ContainsKey("MaxCount"))
                            {
                                int? MaxCount = Requirements["MaxCount"] as int?;
                                if (!MaxCount.HasValue)
                                {
                                    ErrorInfo.AddInvalidObjectFormat("OtherRequirements PetType MaxCount");
                                    return;
                                }

                                RawPetTypeMaxCount = MaxCount.Value;
                            }
                            break;
                        }
                    }

                    ErrorInfo.AddInvalidObjectFormat("Recipe OtherRequirements Keyword");
                    break;

                case OtherRequirementType.IsAdmin:
                case OtherRequirementType.IsLycanthrope:
                case OtherRequirementType.FullMoon:
                case OtherRequirementType.IsHardcore:

                default:
                    OtherRequirementList.Add(RequirementTypeValue);
                    break;
            }
        }

        private static void ParseFieldCosts(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawCosts;
            if ((RawCosts = Value as ArrayList) != null)
                This.ParseCosts(RawCosts, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe Costs");
        }

        private void ParseCosts(ArrayList RawCosts, ParseErrorInfo ErrorInfo)
        {
            List<RecipeCost> ParsedCostList;
            JsonObjectParser<RecipeCost>.InitAsSublist(RawCosts, out ParsedCostList, ErrorInfo);
            CostList = ParsedCostList;
        }

        private static void ParseFieldNumResultItems(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumResultItems((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe NumResultItems");
        }

        private void ParseNumResultItems(int RawNumResultItems, ParseErrorInfo ErrorInfo)
        {
            this.RawNumResultItems = RawNumResultItems;
        }

        private static void ParseFieldUsageAnimationEnd(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawUsageAnimationEnd;
            if ((RawUsageAnimationEnd = Value as string) != null)
                This.ParseUsageAnimationEnd(RawUsageAnimationEnd, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe UsageAnimationEnd");
        }

        private void ParseUsageAnimationEnd(string RawUsageAnimationEnd, ParseErrorInfo ErrorInfo)
        {
            UsageAnimationEnd = RawUsageAnimationEnd;
        }

        private static void ParseFieldResetTimeInSeconds(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseResetTimeInSeconds((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe ResetTimeInSeconds");
        }

        private void ParseResetTimeInSeconds(int RawResetTimeInSeconds, ParseErrorInfo ErrorInfo)
        {
            this.RawResetTimeInSeconds = RawResetTimeInSeconds;
        }

        private static void ParseFieldDyeColor(Recipe This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawDyeColor;
            if ((RawDyeColor = Value as string) != null)
                This.ParseDyeColor(RawDyeColor, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("Recipe DyeColor");
        }

        private void ParseDyeColor(string RawDyeColor, ParseErrorInfo ErrorInfo)
        {
            uint NewColor;
            if (Tools.TryParseColor(RawDyeColor, out NewColor))
                DyeColor = NewColor;
            else
                ErrorInfo.AddInvalidString("Item DyeColor", RawDyeColor);
        }







        private bool ParseResultEffect(string RawEffect, ParseErrorInfo ErrorInfo, out RecipeResultEffect NewResultEffect)
        {
            NewResultEffect = new RecipeResultEffect();

            RecipeEffect ConvertedRecipeEffect;
            if (StringToEnumConversion<RecipeEffect>.TryParse(RawEffect, RecipeEffectStringMap, out ConvertedRecipeEffect, null))
            {
                NewResultEffect.Effect = ConvertedRecipeEffect;
                return true;
            }

            if (ParseDecomposeEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseExtractEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseRepairEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseCraftEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseEnhanceEffect(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            if (ParseAddItemPower(RawEffect, ErrorInfo, ref NewResultEffect))
                return true;

            ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
            return false;
        }

        private bool ParseDecomposeEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string DecomposePattern = "DecomposeItemByTSysLevels(";
            if (RawEffect.StartsWith(DecomposePattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.DecomposeItemByTSysLevels;
                string Decomposed = RawEffect.Substring(DecomposePattern.Length, RawEffect.Length - DecomposePattern.Length - 1);
                string[] DecomposedSplit = Decomposed.Split(',');

                if (DecomposedSplit.Length == 2)
                {
                    DecomposeMaterial ConvertedMaterial;
                    DecomposeSkill ConvertedSkill;
                    if (StringToEnumConversion<DecomposeMaterial>.TryParse(DecomposedSplit[0], out ConvertedMaterial, ErrorInfo) &&
                        StringToEnumConversion<DecomposeSkill>.TryParse(DecomposedSplit[1], out ConvertedSkill, ErrorInfo))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.Material = ConvertedMaterial;
                        NewResultEffect.Skill = ConvertedSkill;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ParseExtractEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string ExtractPattern = "ExtractTSysPower(";
            if (RawEffect.StartsWith(ExtractPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.ExtractTSysPower;
                string Extracted = RawEffect.Substring(ExtractPattern.Length, RawEffect.Length - ExtractPattern.Length - 1);
                string[] ExtractedSplit = Extracted.Split(',');

                if (ExtractedSplit.Length == 3)
                {
                    Augment ConvertedAugment;
                    DecomposeSkill ConvertedSkill;
                    DecomposeMaterial ConvertedMaterial;
                    if (StringToEnumConversion<Augment>.TryParse(ExtractedSplit[0], out ConvertedAugment, ErrorInfo) &&
                        StringToEnumConversion<DecomposeSkill>.TryParse(ExtractedSplit[1], out ConvertedSkill, ErrorInfo) &&
                        StringToEnumConversion<DecomposeMaterial>.TryParse(ExtractedSplit[2], out ConvertedMaterial, ErrorInfo))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.ExtractedAugment = ConvertedAugment;
                        NewResultEffect.Skill = ConvertedSkill;
                        NewResultEffect.Material = ConvertedMaterial;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ParseRepairEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string RepairPattern = "RepairItemDurability(";
            if (RawEffect.StartsWith(RepairPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.RepairItemDurability;
                string Repaired = RawEffect.Substring(RepairPattern.Length, RawEffect.Length - RepairPattern.Length - 1);
                string[] RepairedSplit = Repaired.Split(',');

                if (RepairedSplit.Length == 3)
                {
                    float RepairMinEfficiency;
                    FloatFormat RepairMinEfficiencyFormat;
                    float RepairMaxEfficiency;
                    FloatFormat RepairMaxEfficiencyFormat;
                    int RepairCooldown;
                    if (Tools.TryParseFloat(RepairedSplit[0], out RepairMinEfficiency, out RepairMinEfficiencyFormat) &&
                        Tools.TryParseFloat(RepairedSplit[1], out RepairMaxEfficiency, out RepairMaxEfficiencyFormat) &&
                        int.TryParse(RepairedSplit[2], out RepairCooldown))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.RepairMinEfficiency = RepairMinEfficiency;
                        NewResultEffect.RepairMinEfficiencyFormat = RepairMinEfficiencyFormat;
                        NewResultEffect.RepairMaxEfficiency = RepairMaxEfficiency;
                        NewResultEffect.RepairMaxEfficiencyFormat = RepairMaxEfficiencyFormat;
                        NewResultEffect.RepairCooldown = RepairCooldown;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool ParseCraftEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string CraftPattern = "TSysCraftedEquipment(";
            if (RawEffect.StartsWith(CraftPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.TSysCraftedEquipment;
                string Crafted = RawEffect.Substring(CraftPattern.Length, RawEffect.Length - CraftPattern.Length - 1);
                string[] CraftedSplit = Crafted.Split(',');

                if (CraftedSplit.Length == 0)
                {
                    ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
                    return false;
                }

                else
                {
                    CraftedBoost Boost;
                    int BoostLevel = 0;
                    bool IsCamouflaged = false;
                    int? AdditionalEnchantments = null;
                    string BoostedAnimal = null;

                    string CraftedItem = CraftedSplit[0];

                    if (CraftedItem.Length > 0 && CraftedItem[CraftedItem.Length - 1] == 'C')
                    {
                        IsCamouflaged = true;
                        CraftedItem = CraftedItem.Substring(0, CraftedItem.Length - 1);
                    }

                    if (CraftedItem.Length > 0)
                    {
                        int DigitIndex = CraftedItem.Length;
                        while (DigitIndex > 0 && char.IsDigit(CraftedItem[DigitIndex - 1]))
                            DigitIndex--;

                        if (DigitIndex < CraftedItem.Length)
                        {
                            string LevelString = CraftedItem.Substring(DigitIndex, CraftedItem.Length - DigitIndex);
                            int ParsedLevel;
                            if (int.TryParse(LevelString, out ParsedLevel))
                            {
                                CraftedItem = CraftedItem.Substring(0, DigitIndex);
                                BoostLevel = ParsedLevel;
                            }
                        }
                    }

                    CraftedBoost ConvertedBoost;
                    if (StringToEnumConversion<CraftedBoost>.TryParse(CraftedItem, out ConvertedBoost, ErrorInfo))
                        Boost = ConvertedBoost;
                    else
                        return false;

                    if (CraftedSplit.Length > 1)
                    {
                        int ParsedAdditionalEnchantments;
                        if (int.TryParse(CraftedSplit[1], out ParsedAdditionalEnchantments))
                            AdditionalEnchantments = ParsedAdditionalEnchantments;
                        else
                        {
                            ErrorInfo.AddMissingEnum("RecipeEffect", RawEffect);
                            return false;
                        }
                    }

                    if (CraftedSplit.Length > 2)
                        BoostedAnimal = CraftedSplit[2];

                    NewResultEffect.Effect = ConvertedRecipeEffect;
                    NewResultEffect.Boost = Boost;
                    NewResultEffect.BoostLevel = BoostLevel;
                    NewResultEffect.IsCamouflaged = IsCamouflaged;
                    NewResultEffect.AdditionalEnchantments = AdditionalEnchantments;
                    NewResultEffect.BoostedAnimal = BoostedAnimal;
                    return true;
                }
            }

            return false;
        }

        private bool ParseEnhanceEffect(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string EnhancePattern = "CraftingEnhanceItem";
            if (RawEffect.StartsWith(EnhancePattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.CraftingEnhanceItem;
                string Enhance = RawEffect.Substring(EnhancePattern.Length, RawEffect.Length - EnhancePattern.Length - 1);
                string[] EnhanceSplit = Enhance.Split('(');

                if (EnhanceSplit.Length == 2)
                {
                    EnhancementEffect ConvertedEnhancementEffect;
                    if (StringToEnumConversion<EnhancementEffect>.TryParse(EnhanceSplit[0], out ConvertedEnhancementEffect, ErrorInfo))
                    {
                        string[] EnhanceDataSplit = EnhanceSplit[1].Split(',');
                        if (EnhanceDataSplit.Length == 2)
                        {
                            float AddedQuantity;
                            int ConsumedEnhancementPoints;

                            if (float.TryParse(EnhanceDataSplit[0], System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out AddedQuantity) &&
                                int.TryParse(EnhanceDataSplit[1], out ConsumedEnhancementPoints))
                            {
                                NewResultEffect.Effect = ConvertedRecipeEffect;
                                NewResultEffect.Enhancement = ConvertedEnhancementEffect;
                                NewResultEffect.AddedQuantity = AddedQuantity;
                                NewResultEffect.ConsumedEnhancementPoints = ConsumedEnhancementPoints;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool ParseAddItemPower(string RawEffect, ParseErrorInfo ErrorInfo, ref RecipeResultEffect NewResultEffect)
        {
            string AddItemPowerPattern = "AddItemTSysPower(";
            if (RawEffect.StartsWith(AddItemPowerPattern) && RawEffect.EndsWith(")"))
            {
                RecipeEffect ConvertedRecipeEffect = RecipeEffect.AddItemTSysPower;
                string PowerAdded = RawEffect.Substring(AddItemPowerPattern.Length, RawEffect.Length - AddItemPowerPattern.Length - 1);
                string[] PowerAddedSplit = PowerAdded.Split(',');

                if (PowerAddedSplit.Length == 2)
                {
                    ShamanicSlotPower ConvertedSlot;
                    int PowerLevel;
                    if (StringToEnumConversion<ShamanicSlotPower>.TryParse(PowerAddedSplit[0], out ConvertedSlot, ErrorInfo) &&
                        int.TryParse(PowerAddedSplit[1], out PowerLevel))
                    {
                        NewResultEffect.Effect = ConvertedRecipeEffect;
                        NewResultEffect.SlotPower = ConvertedSlot;
                        NewResultEffect.SlotPowerLevel = PowerLevel;
                        return true;
                    }
                    else
                        ErrorInfo.AddMissingEnum("ShamanicSlotPower", PowerAddedSplit[0]);
                }
            }

            return false;
        }

        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.AddString("ActionLabel", StringToEnumConversion<RecipeAction>.ToString(ActionLabel, RecipeActionStringMap, RecipeAction.Internal_None));
            StringToEnumConversion<RecipeCost>.ListToString(Generator, "Costs", CostList);

            Generator.AddString("Description", Description);

            if (DyeColor.HasValue)
            {
                string AsString = Tools.ColorToString(DyeColor.Value);
                Generator.AddString("DyeColor", AsString);
            }

            Generator.AddInteger("IconId", RawIconId);

            if (IngredientList.Count > 0)
            {
                Generator.OpenArray("Ingredients");

                foreach (RecipeItem Ingredient in IngredientList)
                    Ingredient.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }
            else if (EmptyIngredientList)
                Generator.AddEmptyArray("Ingredients");

            Generator.AddString("InternalName", InternalName);

            if (KeywordList.Count > 0)
            {
                Generator.OpenArray("Keywords");

                foreach (RecipeKeyword Keyword in KeywordList)
                    Generator.AddString(null, Keyword.ToString());

                Generator.CloseArray();
            }

            Generator.AddString("Name", Name);

            if (OtherRequirementList.Count > 0)
            {
                Generator.OpenObject("OtherRequirements");
                Generator.AddString("T", OtherRequirementList[0].ToString());

                switch (OtherRequirementList[0])
                {
                    default:
                        break;

                    case OtherRequirementType.CurHealth:
                        Generator.AddString("Health", CurHealth.ToString());
                        break;

                    case OtherRequirementType.Race:
                        Generator.AddString("AllowedRace", RequiredRace.ToString());
                        break;

                    case OtherRequirementType.HasEffectKeyword:
                        Generator.AddString("HasEffectKeyword", RequiredForm.ToString());
                        break;

                    case OtherRequirementType.DruidEventState:
                        Generator.OpenArray("DisallowedStates");
                        foreach (DisallowedState State in DisallowedDruidStateList)
                            Generator.AddString(null, StringToEnumConversion<DisallowedState>.ToString(State, null, DisallowedState.Internal_None));
                        Generator.CloseArray();
                        break;

                    case OtherRequirementType.PetCount:
                        Generator.AddString("PetTypeTag", PetType.ToString());
                        Generator.AddInteger("MaxCount", RawPetTypeMaxCount);
                        break;

                    case OtherRequirementType.IsAdmin:
                    case OtherRequirementType.IsLycanthrope:
                    case OtherRequirementType.FullMoon:
                    case OtherRequirementType.IsHardcore:
                        Generator.AddString(null, OtherRequirementList[0].ToString());
                        break;
                }

                Generator.CloseObject();
            }

            if (ResultEffectList.Count > 0)
            {
                Generator.OpenArray("ResultEffects");

                foreach (RecipeResultEffect ResultEffect in ResultEffectList)
                    GenerateResultEffectContent(Generator, ResultEffect);

                Generator.CloseArray();
            }

            Generator.AddInteger("NumResultItems", RawNumResultItems);

            if (ResultItemList.Count > 0)
            {
                Generator.OpenArray("ResultItems");

                foreach (RecipeItem Item in ResultItemList)
                    Item.GenerateObjectContent(Generator);

                Generator.CloseArray();
            }
            else if (EmptyResultItemList)
                Generator.AddEmptyArray("ResultItems");

            if (ServerInfo != null)
                ServerInfo.GenerateObjectContent(Generator);

            if (Skill != PowerSkill.Internal_None)
                Generator.AddString("Skill", Skill.ToString());

            Generator.AddInteger("SkillLevelReq", RawSkillLevelReq);

            if (SortSkill != PowerSkill.Internal_None)
                Generator.AddString("SortSkill", SortSkill.ToString());

            if (UsageAnimation != RecipeUsageAnimation.Internal_None)
                Generator.AddString("UsageAnimation", UsageAnimation.ToString());

            Generator.AddInteger("UsageDelay", RawUsageDelay);
            Generator.AddString("UsageDelayMessage", UsageDelayMessage);

            Generator.CloseObject();
        }

        public void GenerateResultEffectContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            string Content;

            switch (ResultEffect.Effect)
            {
                default:
                    Content = StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap);
                    break;

                case RecipeEffect.DecomposeItemByTSysLevels:
                    Content = GenerateDecomposeContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.ExtractTSysPower:
                    Content = GenerateExtractContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.RepairItemDurability:
                    Content = GenerateRepairContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.TSysCraftedEquipment:
                    Content = GenerateCraftContent(Generator, ResultEffect);
                    break;

                case RecipeEffect.CraftingEnhanceItem:
                    Content = GenerateEnhanceContent(Generator, ResultEffect);
                    break;
            }

            Generator.AddString(null, Content);
        }

        public string GenerateDecomposeContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap) + "(" + ResultEffect.Material + "," + ResultEffect.Skill + ")";
        }

        public string GenerateExtractContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap) + "(" + ResultEffect.ExtractedAugment + "," + ResultEffect.Skill + "," + ResultEffect.Material + ")";
        }

        public string GenerateRepairContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap) + "(" + Tools.FloatToString(ResultEffect.RepairMinEfficiency, ResultEffect.RepairMinEfficiencyFormat) + "," + Tools.FloatToString(ResultEffect.RepairMaxEfficiency, ResultEffect.RepairMaxEfficiencyFormat) + "," + ResultEffect.RepairCooldown + ")";
        }

        public string GenerateCraftContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            string CraftedItem = ResultEffect.Boost.ToString();

            if (ResultEffect.BoostLevel != 0)
                CraftedItem += ResultEffect.BoostLevel.ToString();
            if (ResultEffect.IsCamouflaged)
                CraftedItem += "C";

            if (ResultEffect.AdditionalEnchantments != null)
                CraftedItem += "," + ResultEffect.AdditionalEnchantments.Value.ToString();

            if (ResultEffect.BoostedAnimal != null)
                CraftedItem += "," + ResultEffect.BoostedAnimal;

            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap) + "(" + CraftedItem + ")";
        }

        public string GenerateEnhanceContent(JsonGenerator Generator, RecipeResultEffect ResultEffect)
        {
            return StringToEnumConversion<RecipeEffect>.ToString(ResultEffect.Effect, RecipeEffectStringMap) + ResultEffect.Enhancement.ToString() + "(" + ResultEffect.AddedQuantity.ToString(CultureInfo.InvariantCulture.NumberFormat) + "," + ResultEffect.ConsumedEnhancementPoints + ")";
        }

        public static bool ConnectTableByInternamName(ParseErrorInfo ErrorInfo, Dictionary<string, Recipe> RecipeTable, List<string> ConnectedList, Dictionary<string, Recipe> ConnectedTable)
        {
            bool Connected = false;

            foreach (string s in ConnectedList)
            {
                bool Found = false;
                foreach (KeyValuePair<string, Recipe> Entry in RecipeTable)
                    if (Entry.Value.InternalName == s)
                    {
                        Found = true;
                        Connected = true;
                        if (ConnectedTable.ContainsKey(s))
                            ErrorInfo.AddDuplicateString("Recipe", s);
                        else
                            ConnectedTable.Add(Entry.Key, Entry.Value);
                        break;
                    }

                if (!Found)
                    ErrorInfo.AddMissingKey(s);
            }

            return Connected;
        }

        public static Recipe ConnectSingleProperty(ParseErrorInfo ErrorInfo, Dictionary<string, Recipe> RecipeTable, string RawRecipeName, Recipe ParsedRecipe, ref bool IsRawRecipeParsed, ref bool IsConnected)
        {
            if (IsRawRecipeParsed)
                return ParsedRecipe;

            IsRawRecipeParsed = true;

            if (RawRecipeName == null)
                return null;

            foreach (KeyValuePair<string, Recipe> Entry in RecipeTable)
                if (Entry.Value.InternalName == RawRecipeName)
                {
                    IsConnected = true;
                    return Entry.Value;
                }

            ErrorInfo.AddMissingKey(RawRecipeName);
            return null;
        }
        #endregion

        #region Ancestor Interface
        protected override Dictionary<string, FieldValueHandler> FieldTable { get { return _FieldTable; } }
        protected override string FieldTableName { get { return "Recipe"; } }

        protected override void InitializeFields()
        {
            CostList = new List<RecipeCost>();
            IngredientList = new List<RecipeItem>();
            KeywordList = new List<RecipeKeyword>();
            OtherRequirementList = new List<OtherRequirementType>();
            DisallowedDruidStateList = new List<DisallowedState>();
            ResultEffectList = new List<RecipeResultEffect>();
            ResultItemList = new List<RecipeItem>();
            CurHealth = 0;
            RequiredRace = Race.Internal_None;
            RequiredForm = AnimalForm.Internal_None;
            DisallowedDruidStateList = new List<DisallowedState>();
            PetType = RecipeKeyword.Internal_None;
            RawPetTypeMaxCount = null;
            RawPetTypeMaxCount = 0;
        }

        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, Dictionary<string, Ability> AbilityTable, Dictionary<string, Attribute> AttributeTable, Dictionary<string, Item> ItemTable, Dictionary<string, Recipe> RecipeTable, Dictionary<string, Skill> SkillTable, Dictionary<string, Quest> QuestTable, Dictionary<string, Effect> EffectTable)
        {
            bool Connected = false;

            foreach (RecipeCost Item in CostList)
                Connected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            foreach (RecipeItem Item in IngredientList)
                Connected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            foreach (RecipeItem Item in ResultItemList)
                Connected |= Item.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            if (ServerInfo != null)
                Connected |= ServerInfo.Connect(ErrorInfo, AbilityTable, AttributeTable, ItemTable, RecipeTable, SkillTable, QuestTable, EffectTable);

            return Connected;
        }
        #endregion
    }
}
