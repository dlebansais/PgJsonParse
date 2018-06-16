using PgJsonReader;
using Presentation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfo : GenericJsonObject<ServerInfo>
    {
        #region Direct Properties
        public List<ServerInfoEffect> ServerInfoEffectList { get; private set; } = new List<PgJsonObjects.ServerInfoEffect>();
        public List<Item> GiveItemList { get; private set; } = new List<Item>();
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get; private set; }
        public List<AbilityRequirement> OtherRequirementList { get; private set; } = new List<AbilityRequirement>();
        public ItemRequiredHotspot RequiredHotspot { get; private set; }

        private List<string> RawItemNameList = new List<string>();
        private bool IsServerInfoEffectListEmpty;
        private bool IsOtherRequirementListEmpty;
        #endregion

        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        public void SetLinkBack(GenericJsonObject LinkBack)
        {
            this.LinkBack = LinkBack;
            foreach (ServerInfoEffect Item in ServerInfoEffectList)
                Item.SetLinkBack(LinkBack);
        }

        public ServerInfoEffect ToSpecificServerInfoEffect(ParseErrorInfo ErrorInfo)
        {
            switch (ServerInfoEffect)
            {
                case ServerInfoEffectType.RaiseSkillToLevel:
                case ServerInfoEffectType.GiveXP:
                case ServerInfoEffectType.GiveXpVerbose:
                    if (Skill != PowerSkill.Internal_None && SkillLevel >= 0)
                        return new SkillAndLevelServerInfoEffect(ServerInfoEffect, EffectLevel, Skill, SkillLevel);
                    else
                        return null;

                case ServerInfoEffectType.BoostHydration:
                case ServerInfoEffectType.BoostBodyHeat:
                case ServerInfoEffectType.IocainePoison:
                case ServerInfoEffectType.UseGeologySurvey_Serbule:
                case ServerInfoEffectType.UseGeologySurvey_SouthSerbule:
                case ServerInfoEffectType.UseGeologySurvey_Eltibule:
                case ServerInfoEffectType.UseGeologySurvey_Kur:
                case ServerInfoEffectType.UseMiningSurveyX_KurMountains:
                case ServerInfoEffectType.UseMiningSurvey_SouthSerbule:
                case ServerInfoEffectType.UseMiningSurvey_Eltibule:
                case ServerInfoEffectType.UseMiningSurveyX_Ilmari:
                case ServerInfoEffectType.UseEltibuleTreasureMap:
                case ServerInfoEffectType.UseIlmariTreasureMap:
                case ServerInfoEffectType.Armor:
                    if (EffectValue.HasValue)
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value, false);
                    else
                        return null;

                case ServerInfoEffectType.HouseholdPoison:
                    if (EffectValue.HasValue)
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value, false);
                    else
                        return new SimpleServerInfoEffect(ServerInfoEffect, EffectLevel, null);

                case ServerInfoEffectType.LearnAbility:
                    if (RawBestowAbility != null)
                        return new AbilityServerInfoEffect(ServerInfoEffect, EffectLevel, RawBestowAbility);
                    else
                        return null;

                case ServerInfoEffectType.GiveCouncilCoins:
                    if (LowValue.HasValue && HighValue.HasValue && LowValue.Value < HighValue.Value)
                        return new IntervalServerInfoEffect(ServerInfoEffect, EffectLevel, LowValue.Value, HighValue.Value);
                    else if (EffectValue.HasValue)
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value, LowValue.HasValue && HighValue.HasValue);
                    else
                        return null;

                case ServerInfoEffectType.GivePoetryAppreciationXp:
                    if (PoetryXpValue.HasValue && RecitalXpValue.HasValue)
                        return new PoetryServerInfoEffect(ServerInfoEffect, EffectLevel, PoetryXpValue.Value, RecitalXpValue.Value);
                    else
                        return null;

                case ServerInfoEffectType.Drinking_Beer:
                case ServerInfoEffectType.Drinking_HardLiquor:
                case ServerInfoEffectType.Drinking_Wine:
                    if (DrinkATValue.HasValue && AlcoholPowerValue.HasValue)
                        return new DrinkEffectServerInfoEffect(ServerInfoEffect, EffectLevel, DrinkATValue.Value, AlcoholPowerValue.Value);
                    else
                        return null;

                case ServerInfoEffectType.ArmorPotion:
                case ServerInfoEffectType.HealthPotion:
                case ServerInfoEffectType.PowerPotion:
                    return new PotionServerInfoEffect(ServerInfoEffect, EffectLevel, HealthGainInstant, ArmorGainInstant, PowerGainInstant);

                case ServerInfoEffectType.HealthHOTPotion:
                case ServerInfoEffectType.PowerHOTPotion:
                    return new PotionServerInfoEffect(ServerInfoEffect, EffectLevel, HealthGainPerSecond, ArmorGainPerSecond, PowerGainPerSecond, TotalGainDuration);

                case ServerInfoEffectType.SummonFlower:
                case ServerInfoEffectType.SummonFlowerDisplay:
                case ServerInfoEffectType.SpawnMushroomFarmBox:
                case ServerInfoEffectType.SpawnItemDispenser:
                case ServerInfoEffectType.BestowGolemConditional:
                case ServerInfoEffectType.BestowGolemAbility:
                case ServerInfoEffectType.PulseEvent:
                case ServerInfoEffectType.SetPrimaryCombatSkill:
                case ServerInfoEffectType.SetInteractionFlag:
                case ServerInfoEffectType.SummonGruesomeSpookyPunch:
                    return new SimpleServerInfoEffect(ServerInfoEffect, EffectLevel, EffectParameter);

                case ServerInfoEffectType.EquipmentBoost:
                    if (RawAttribute != null && RawAttributeEffect.HasValue && RawDuration.HasValue)
                        return new TemporaryServerInfoEffect(ServerInfoEffect, EffectLevel, RawAttribute, RawAttributeEffect, RawDuration);
                    else if (RawAttribute != null && RawAttributeEffect.HasValue)
                        return new EquipmentBoostServerInfoEffect(ServerInfoEffect, EffectLevel, RawAttribute, RawAttributeEffect);
                    else
                        return null;

                default:
                    return new SimpleServerInfoEffect(ServerInfoEffect, EffectLevel, null);
            }
        }

        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Effects", new FieldParser() {
                Type = FieldType.StringArray,
                ParseStringArray = ParseEffects,
                SetArrayIsEmpty = () => IsServerInfoEffectListEmpty = true,
                GetStringArray = GetEffects,
                GetArrayIsEmpty = () => IsServerInfoEffectListEmpty } },
            { "GiveItems", new FieldParser() {
                Type = FieldType.SimpleStringArray,
                ParseSimpleStringArray = (string value, ParseErrorInfo errorInfo) => RawItemNameList.Add(value),
                GetStringArray = () => RawItemNameList } },
            { "RequiredHotspot", new FieldParser() {
                Type = FieldType.String,
                ParseString = (string value, ParseErrorInfo errorInfo) => RequiredHotspot = StringToEnumConversion<ItemRequiredHotspot>.Parse(value, errorInfo),
                GetString = () => StringToEnumConversion<ItemRequiredHotspot>.ToString(RequiredHotspot, null, ItemRequiredHotspot.Internal_None) } },
            { "NumItemsToGive", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNumItemsToGive = value,
                GetInteger = () => RawNumItemsToGive } },
            { "OtherRequirements", new FieldParser() {
                Type = FieldType.ObjectArray,
                ParseObjectArray = (JsonObject value, ParseErrorInfo errorInfo) => JsonObjectParser<AbilityRequirement>.ParseList("OtherRequirements", value, OtherRequirementList, errorInfo),
                SetArrayIsEmpty = () => IsOtherRequirementListEmpty = true,
                GetObjectArray = () => OtherRequirementList,
                GetArrayIsEmpty = () => IsOtherRequirementListEmpty,
                SimplifyArray = true } },
        }; } }

        private bool ParseEffects(string RawEffect, ParseErrorInfo ErrorInfo)
        {
            ServerInfoEffect NewEffect = ParseEffectString(RawEffect, ErrorInfo);
            if (NewEffect != null)
            {
                ServerInfoEffectList.Add(NewEffect);
                return true;
            }
            else
                return false;
        }

        private List<string> GetEffects()
        {
            List<string> Result = new List<string>();

            foreach (ServerInfoEffect Item in ServerInfoEffectList)
            {
                string RawEffect = Item.RawEffect;
                Result.Add(RawEffect);
            }

            return Result;
        }

        private ServerInfoEffect ParseEffectString(string RawEffect, ParseErrorInfo ErrorInfo)
        {
            string RawEffectString = null;
            string Details = null;

            int StartDetailIndex = RawEffect.IndexOf('(');
            if (StartDetailIndex > 0)
            {
                int EndDetailIndex = RawEffect.IndexOf(')', StartDetailIndex);
                if (EndDetailIndex < 0)
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return null;
                }

                RawEffectString = RawEffect.Substring(0, StartDetailIndex);
                Details = RawEffect.Substring(StartDetailIndex + 1, EndDetailIndex - StartDetailIndex - 1);
            }
            else
                RawEffectString = RawEffect;

            int Level = 0;
            int Scale = 1;
            while (RawEffectString.Length > 0 && char.IsDigit(RawEffectString[RawEffectString.Length - 1]))
            {
                string Digit = RawEffectString[RawEffectString.Length - 1].ToString();
                int DigitValue;
                if (!int.TryParse(Digit, out DigitValue))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return null;
                }

                Level += Scale * DigitValue;
                Scale *= 10;

                RawEffectString = RawEffectString.Substring(0, RawEffectString.Length - 1);
            }

            ServerInfoEffect = StringToEnumConversion<ServerInfoEffectType>.Parse(RawEffectString, ErrorInfo);

            if (Scale > 1)
                EffectLevel = Level;
            else
                EffectLevel = null;

            switch (ServerInfoEffect)
            {
                case ServerInfoEffectType.RaiseSkillToLevel:
                case ServerInfoEffectType.GiveXP:
                case ServerInfoEffectType.GiveXpVerbose:
                    ParseSkillAndLevel(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.BoostHydration:
                case ServerInfoEffectType.BoostBodyHeat:
                case ServerInfoEffectType.IocainePoison:
                case ServerInfoEffectType.UseGeologySurvey_Serbule:
                case ServerInfoEffectType.UseGeologySurvey_SouthSerbule:
                case ServerInfoEffectType.UseGeologySurvey_Eltibule:
                case ServerInfoEffectType.UseGeologySurvey_Kur:
                case ServerInfoEffectType.UseMiningSurveyX_KurMountains:
                case ServerInfoEffectType.UseMiningSurvey_SouthSerbule:
                case ServerInfoEffectType.UseMiningSurvey_Eltibule:
                case ServerInfoEffectType.UseMiningSurveyX_Ilmari:
                case ServerInfoEffectType.UseEltibuleTreasureMap:
                case ServerInfoEffectType.UseIlmariTreasureMap:
                case ServerInfoEffectType.Armor:
                    ParseEffectValue(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.HouseholdPoison:
                    ParseOptionalEffectValue(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.LearnAbility:
                    ParseAbility(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.GiveCouncilCoins:
                    ParseInterval(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.GivePoetryAppreciationXp:
                    ParsePoetryXp(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.Drinking_Beer:
                case ServerInfoEffectType.Drinking_HardLiquor:
                case ServerInfoEffectType.Drinking_Wine:
                    ParseDrinkEffect(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.ArmorPotion:
                case ServerInfoEffectType.HealthPotion:
                case ServerInfoEffectType.PowerPotion:
                case ServerInfoEffectType.HealthHOTPotion:
                case ServerInfoEffectType.PowerHOTPotion:
                    ParsePotion(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.SummonFlower:
                case ServerInfoEffectType.SummonFlowerDisplay:
                case ServerInfoEffectType.PulseEvent:
                case ServerInfoEffectType.BestowGolemConditional:
                case ServerInfoEffectType.BestowGolemAbility:
                case ServerInfoEffectType.SpawnItemDispenser:
                case ServerInfoEffectType.SetPrimaryCombatSkill:
                case ServerInfoEffectType.SetInteractionFlag:
                    ParseParameter(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.SpawnMushroomFarmBox:
                    ParseParameter(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.EquipmentBoost:
                    ParseEquipmentBoost(Details, ErrorInfo);
                    break;

                case ServerInfoEffectType.SummonGruesomeSpookyPunch:
                    if (Details != null)
                        ParseParameter(Details, ErrorInfo);
                    break;

                default:
                    if (Details != null)
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return null;
                    }
                    break;
            }

            ServerInfoEffect NewEffect = ToSpecificServerInfoEffect(ErrorInfo);
            return NewEffect;
        }

        private void ParseSkillAndLevel(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');
            if (Splitted.Length != 2)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string SkillLevelString = Splitted[1];

            PowerSkill ParsedSkill;
            int SkillLevelValue = 0;

            if (!StringToEnumConversion<PowerSkill>.TryParse(Splitted[0], out ParsedSkill, ErrorInfo) ||
                !int.TryParse(Splitted[1], out SkillLevelValue))
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            if (ParsedSkill == PowerSkill.Internal_None || ParsedSkill == PowerSkill.AnySkill || ParsedSkill == PowerSkill.Unknown)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            Skill = ParsedSkill;
            SkillLevel = SkillLevelValue;
        }

        private void ParseAbility(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            RawBestowAbility = Details;
        }

        private void ParseEffectValue(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            int EffectValue = 0;
            if (!int.TryParse(Details, out EffectValue))
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            this.EffectValue = EffectValue;
        }

        private void ParseOptionalEffectValue(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
                return;

            int EffectValue = 0;
            if (!int.TryParse(Details, out EffectValue))
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            this.EffectValue = EffectValue;
        }

        private void ParseInterval(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');
            if (Splitted.Length != 2)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            int LowValue = 0;
            int HighValue = 0;
            if (!int.TryParse(Splitted[0], out LowValue) || !int.TryParse(Splitted[1], out HighValue) || LowValue > HighValue)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            this.LowValue = LowValue;
            this.HighValue = HighValue;
            EffectValue = LowValue;
        }

        private void ParsePoetryXp(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');
            if (Splitted.Length != 2)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            int PoetryXpValue = 0;
            int RecitalXpValue = 0;
            if (!int.TryParse(Splitted[0], out PoetryXpValue) || !int.TryParse(Splitted[1], out RecitalXpValue))
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            this.PoetryXpValue = PoetryXpValue;
            this.RecitalXpValue = RecitalXpValue;
        }

        private void ParseDrinkEffect(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');
            if (Splitted.Length != 2)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            int DrinkATValue = 0;
            int AlcoholPowerValue = 0;
            if (!int.TryParse(Splitted[0], out DrinkATValue) || !int.TryParse(Splitted[1], out AlcoholPowerValue))
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            this.DrinkATValue = DrinkATValue;
            this.AlcoholPowerValue = AlcoholPowerValue;
        }

        private void ParsePotion(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');

            int HealthGainInstant = 0;
            int HealthGainPerSecond = 0;
            int ArmorGainInstant = 0;
            int ArmorGainPerSecond = 0;
            int PowerGainInstant = 0;
            int PowerGainPerSecond = 0;
            int TotalGainDuration = 0;

            switch (Splitted.Length)
            {
                case 1:
                    if (!int.TryParse(Splitted[0], out HealthGainInstant))
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return;
                    }

                    this.HealthGainInstant = HealthGainInstant;
                    break;

                case 2:
                    if (!int.TryParse(Splitted[0], out HealthGainInstant) ||
                        !int.TryParse(Splitted[1], out ArmorGainInstant))
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return;
                    }

                    this.HealthGainInstant = HealthGainInstant;
                    this.ArmorGainInstant = ArmorGainInstant;
                    break;

                case 3:
                    if (!int.TryParse(Splitted[0], out HealthGainInstant) ||
                        !int.TryParse(Splitted[1], out ArmorGainInstant) ||
                        !int.TryParse(Splitted[2], out PowerGainInstant))
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return;
                    }

                    this.HealthGainInstant = HealthGainInstant;
                    this.ArmorGainInstant = ArmorGainInstant;
                    this.PowerGainInstant = PowerGainInstant;
                    break;

                case 5:
                    if (!int.TryParse(Splitted[0], out HealthGainPerSecond) ||
                        !int.TryParse(Splitted[1], out ArmorGainPerSecond) ||
                        !int.TryParse(Splitted[2], out PowerGainPerSecond) ||
                        !int.TryParse(Splitted[4], out TotalGainDuration))
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return;
                    }

                    this.HealthGainPerSecond = HealthGainPerSecond;
                    this.ArmorGainPerSecond = ArmorGainPerSecond;
                    this.PowerGainPerSecond = PowerGainPerSecond;
                    this.TotalGainDuration = TotalGainDuration;
                    break;

                default:
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return;
            }
        }

        private void ParseParameter(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            EffectParameter = Details;
        }

        private void ParseEquipmentBoost(string Details, ParseErrorInfo ErrorInfo)
        {
            if (Details == null)
            {
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                return;
            }

            string[] Splitted = Details.Split(',');

            if (Splitted.Length == 2)
            {
                string RawEffectDesc = Splitted[0].Trim();
                string RawEffect = Splitted[1].Trim();

                float AttributeEffect = 0;
                if (!InvariantCulture.TryParseSingle(RawEffect, out AttributeEffect))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return;
                }

                ItemEffect RawAttribute;
                if (!ItemEffect.TryParse(RawEffectDesc, out RawAttribute))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return;
                }

                this.RawAttribute = RawAttribute;
                RawAttributeEffect = AttributeEffect;
            }

            else if (Splitted.Length == 4)
            {
                string RawEffectDesc = Splitted[0].Trim();
                string RawEffect = Splitted[1].Trim();
                string RawDuration = Splitted[3].Trim();

                float AttributeEffect = 0;
                int Duration = 0;
                if (!InvariantCulture.TryParseSingle(RawEffect, out AttributeEffect) || !int.TryParse(RawDuration, out Duration))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return;
                }

                ItemEffect RawAttribute;
                if (!ItemEffect.TryParse(RawEffectDesc, out RawAttribute))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return;
                }

                this.RawAttribute = RawAttribute;
                RawAttributeEffect = AttributeEffect;
                this.RawDuration = Duration;
            }

            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
        }

        private GenericJsonObject LinkBack;
        private ServerInfoEffectType ServerInfoEffect;
        private int? EffectLevel;
        private PowerSkill Skill;
        private int SkillLevel;
        private int? LowValue;
        private int? HighValue;
        private int? EffectValue;
        private int? PoetryXpValue;
        private int? RecitalXpValue;
        private int? DrinkATValue;
        private int? AlcoholPowerValue;
        private string RawBestowAbility;
        private ItemEffect RawAttribute;
        private float? RawAttributeEffect;
        private int? RawDuration;
        private int? HealthGainInstant;
        private int? HealthGainPerSecond;
        private int? ArmorGainInstant;
        private int? ArmorGainPerSecond;
        private int? PowerGainInstant;
        private int? PowerGainPerSecond;
        private int? TotalGainDuration;
        private string EffectParameter;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = "";

                foreach (ServerInfoEffect Item in ServerInfoEffectList)
                    AddWithFieldSeparator(ref Result, Item.TextContent);

                foreach (Item Item in GiveItemList)
                    AddWithFieldSeparator(ref Result, Item.Name);

                if (RequiredHotspot != ItemRequiredHotspot.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.ItemRequiredHotspotTextMap[RequiredHotspot]);

                foreach (AbilityRequirement Item in OtherRequirementList)
                    Result += Item.TextContent;

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;
            Dictionary<string, IGenericJsonObject> ItemTable = AllTables[typeof(Item)];
            Dictionary<string, IGenericJsonObject> SkillTable = AllTables[typeof(Skill)];

            foreach (ServerInfoEffect Item in ServerInfoEffectList)
                if (Item.ConnectFields(ErrorInfo, Parent, AllTables))
                    IsConnected = true;

            foreach (string RawItemName in RawItemNameList)
            {
                Item ParsedItem = null;
                bool IsRawItemParsed = false;
                ParsedItem = Item.ConnectSingleProperty(ErrorInfo, ItemTable, RawItemName, ParsedItem, ref IsRawItemParsed, ref IsConnected, LinkBack);
                if (ParsedItem != null)
                {
                    if (!GiveItemList.Contains(ParsedItem))
                        GiveItemList.Add(ParsedItem);
                }
            }

            foreach (AbilityRequirement Item in OtherRequirementList)
                IsConnected |= Item.Connect(ErrorInfo, LinkBack, AllTables);

            return IsConnected;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "ServerInfo"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, IList> StoredObjectListTable = new Dictionary<int, IList>();

            AddObjectList(ServerInfoEffectList, data, ref offset, BaseOffset, 0, StoredObjectListTable);
            AddObjectList(GiveItemList, data, ref offset, BaseOffset, 4, StoredObjectListTable);
            AddInt(RawNumItemsToGive, data, ref offset, BaseOffset, 8);
            AddObjectList(OtherRequirementList, data, ref offset, BaseOffset, 12, StoredObjectListTable);
            AddEnum(RequiredHotspot, data, ref offset, BaseOffset, 16);

            FinishSerializing(data, ref offset, BaseOffset, 18, null, null, null, null, null, null, null, StoredObjectListTable);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
