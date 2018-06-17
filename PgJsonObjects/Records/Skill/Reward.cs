using PgJsonReader;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class Reward : GenericJsonObject<Reward>
    {
        #region Direct Properties
        public int RewardLevel { get { return RawRewardLevel.HasValue ? RawRewardLevel.Value : 0; } }
        public int? RawRewardLevel { get; private set; }
        public List<Race> RaceRestrictionList { get; private set; } = new List<Race>();
        public Ability Ability { get; private set; }
        public string Notes { get; private set; }
        public Recipe Recipe { get; private set; }
        public Skill BonusSkill { get; private set; }
        private bool IsBonusSkillParsed;
        private string RawAbility;
        private bool IsRawAbilityParsed;
        private string RawRecipe;
        private bool IsRawRecipeParsed;
        private PowerSkill RawBonusSkill;
        #endregion

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        public Skill ParentSkill { get; private set; }
        #endregion

        #region Parsing
        protected override void InitializeKey(string key, int index, IJsonValue value, ParseErrorInfo ErrorInfo)
        {
            base.InitializeKey(key, index, value, ErrorInfo);

            string[] SplitKey = Key.Split('_');

            if (SplitKey.Length >= 1)
            {
                int ParsedRewardLevel;
                if (int.TryParse(SplitKey[0], out ParsedRewardLevel))
                    RawRewardLevel = ParsedRewardLevel;
                else
                    ErrorInfo.AddInvalidObjectFormat("Reward Level");

                for (int i = 1; i < SplitKey.Length; i++)
                {
                    Race ParsedRace;
                    if (StringToEnumConversion<Race>.TryParse(SplitKey[i], out ParsedRace, ErrorInfo))
                        RaceRestrictionList.Add(ParsedRace);
                }
            }
            else
                ErrorInfo.AddInvalidObjectFormat("Reward");
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Ability", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawAbility = value,
                GetString = () => RawAbility } },
            { "BonusToSkill", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawBonusSkill = StringToEnumConversion<PowerSkill>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<PowerSkill>.ToString(RawBonusSkill, null, PowerSkill.Internal_None) } },
            { "Recipe", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RawRecipe = value,
                GetString = () => RawRecipe  } },
            { "Notes", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => Notes = value,
                GetString = () => Notes } },
        }; } }
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                if (Ability != null)
                    AddWithFieldSeparator(ref Result, Ability.Name);
                AddWithFieldSeparator(ref Result, Notes);
                if (Recipe != null)
                    AddWithFieldSeparator(ref Result, Recipe.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> RecipeTable = AllTables[typeof(Recipe)];
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            ParentSkill = Parent as Skill;

            Ability = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawAbility, Ability, ref IsRawAbilityParsed, ref IsConnected, this);
            Recipe = Recipe.ConnectSingleProperty(ErrorInfo, RecipeTable, RawRecipe, Recipe, ref IsRawRecipeParsed, ref IsConnected, this);

            if (RawBonusSkill != PowerSkill.Internal_None && RawBonusSkill != PowerSkill.AnySkill && RawBonusSkill != PowerSkill.Unknown)
                BonusSkill = PgJsonObjects.Skill.ConnectPowerSkill(ErrorInfo, SkillTable, RawBonusSkill, BonusSkill, ref IsBonusSkillParsed, ref IsConnected, this);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Reward"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, string> StoredStringtable = new Dictionary<int, string>();
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, IList> StoredEnumListTable = new Dictionary<int, IList>();

            AddInt(RawRewardLevel, data, ref offset, BaseOffset, 0);
            AddEnumList(RaceRestrictionList, data, ref offset, BaseOffset, 4, StoredEnumListTable);
            AddObject(Ability, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddString(Notes, data, ref offset, BaseOffset, 12, StoredStringtable);
            AddObject(Recipe, data, ref offset, BaseOffset, 16, StoredObjectTable);
            AddObject(BonusSkill, data, ref offset, BaseOffset, 20, StoredObjectTable);

            FinishSerializing(data, ref offset, BaseOffset, 24, StoredStringtable, StoredObjectTable, null, StoredEnumListTable, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
