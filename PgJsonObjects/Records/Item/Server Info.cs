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
        private List<string> RawItemNameList = new List<string>();
        public int NumItemsToGive { get { return RawNumItemsToGive.HasValue ? RawNumItemsToGive.Value : 0; } }
        public int? RawNumItemsToGive { get; private set; }
        public ItemRequiredHotspot RequiredHotspot { get; private set; }
        public List<AbilityRequirement> OtherRequirementList { get; private set; } = new List<AbilityRequirement>();
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
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value);
                    else
                        return null;

                case ServerInfoEffectType.HouseholdPoison:
                    if (EffectValue.HasValue)
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value);
                    else
                        return new SimpleServerInfoEffect(ServerInfoEffect, EffectLevel);

                case ServerInfoEffectType.LearnAbility:
                    if (RawBestowAbility != null)
                        return new AbilityServerInfoEffect(ServerInfoEffect, EffectLevel, RawBestowAbility);
                    else
                        return null;

                case ServerInfoEffectType.GiveCouncilCoins:
                    if (LowValue.HasValue && HighValue.HasValue)
                        return new IntervalServerInfoEffect(ServerInfoEffect, EffectLevel, LowValue.Value, HighValue.Value);
                    else if (EffectValue.HasValue)
                        return new ValueServerInfoEffect(ServerInfoEffect, EffectLevel, EffectValue.Value);
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
                case ServerInfoEffectType.HealthHOTPotion:
                case ServerInfoEffectType.PowerHOTPotion:
                case ServerInfoEffectType.SummonFlower:
                case ServerInfoEffectType.SummonFlowerDisplay:
                case ServerInfoEffectType.SpawnMushroomFarmBox:
                case ServerInfoEffectType.SpawnItemDispenser:
                case ServerInfoEffectType.BestowGolemConditional:
                case ServerInfoEffectType.BestowGolemAbility:
                case ServerInfoEffectType.EquipmentBoost:
                case ServerInfoEffectType.PulseEvent:
                case ServerInfoEffectType.SetPrimaryCombatSkill:
                case ServerInfoEffectType.SetInteractionFlag:

                case ServerInfoEffectType.SummonGruesomeSpookyPunch:

                default:
                    return new SimpleServerInfoEffect(ServerInfoEffect, EffectLevel);
            }
        }

        protected override Dictionary<string, FieldValueHandler> FieldTable { get; } = new Dictionary<string, FieldValueHandler>()
        {
            { "Effects", ParseFieldEffects},
            { "GiveItems", ParseFieldGiveItems},
            { "RequiredHotspot", ParseFieldRequiredHotspot},
            { "OtherRequirements", ParseFieldOtherRequirements},
            { "NumItemsToGive", ParseFieldNumItemsToGive},
        };

        private static void ParseFieldEffects(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawEffects;
            if ((RawEffects = Value as ArrayList) != null)
                This.ParseEffects(RawEffects, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
        }

        private void ParseEffects(ArrayList RawEffects, ParseErrorInfo ErrorInfo)
        {
            foreach (object Effect in RawEffects)
            {
                string AsString;
                if ((AsString = Effect as string) != null)
                {
                    ServerInfoEffect NewEffect = ParseEffectString(AsString, ErrorInfo);
                    if (NewEffect != null)
                        ServerInfoEffectList.Add(NewEffect);
                }
                    
                else
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
            }
        }

        private static void ParseFieldGiveItems(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            ArrayList RawGiveItems;
            if ((RawGiveItems = Value as ArrayList) != null)
                This.ParseGiveItems(RawGiveItems, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo GiveItems");
        }

        private void ParseGiveItems(ArrayList RawGiveItems, ParseErrorInfo ErrorInfo)
        {
            foreach (object Effect in RawGiveItems)
            {
                string ItemName;
                if ((ItemName = Effect as string) != null)
                    RawItemNameList.Add(ItemName);
                else
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo GiveItems");
            }
        }

        private static void ParseFieldRequiredHotspot(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            string RawRequiredHotspot;
            if ((RawRequiredHotspot = Value as string) != null)
                This.ParseRequiredHotspot(RawRequiredHotspot, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo RequiredHotspot");
        }

        private void ParseRequiredHotspot(string RawRequiredHotspot, ParseErrorInfo ErrorInfo)
        {
            ItemRequiredHotspot ParsedRequiredHotspot;
            StringToEnumConversion<ItemRequiredHotspot>.TryParse(RawRequiredHotspot, out ParsedRequiredHotspot, ErrorInfo);
            RequiredHotspot = ParsedRequiredHotspot;
        }

        private static void ParseFieldNumItemsToGive(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
        {
            if (Value is int)
                This.ParseNumItemsToGive((int)Value, ErrorInfo);
            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo NumItemsToGive");
        }

        private void ParseNumItemsToGive(int RawNumItemsToGive, ParseErrorInfo ErrorInfo)
        {
            this.RawNumItemsToGive = RawNumItemsToGive;
        }

        private static void ParseFieldOtherRequirements(ServerInfo This, object Value, ParseErrorInfo ErrorInfo)
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
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo OtherRequirements");
                }
            }

            else if ((AsDictionary = Value as Dictionary<string, object>) != null)
                This.ParseOtherRequirements(AsDictionary, ErrorInfo);

            else
                ErrorInfo.AddInvalidObjectFormat("ServerInfo OtherRequirements");
        }

        public void ParseOtherRequirements(Dictionary<string, object> RawOtherRequirements, ParseErrorInfo ErrorInfo)
        {
            AbilityRequirement ParsedOtherRequirement;
            JsonObjectParser<AbilityRequirement>.InitAsSubitem("OtherRequirements", RawOtherRequirements, out ParsedOtherRequirement, ErrorInfo);

            AbilityRequirement ConvertedAbilityRequirement = ParsedOtherRequirement.ToSpecificAbilityRequirement(ErrorInfo);
            if (ConvertedAbilityRequirement != null)
                OtherRequirementList.Add(ConvertedAbilityRequirement);
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
            while (RawEffectString.Length > 0 && char.IsDigit(RawEffectString[RawEffectString.Length - 1]))
            {
                string Digit = RawEffectString[RawEffectString.Length - 1].ToString();
                int DigitValue;
                if (!int.TryParse(Digit, out DigitValue))
                {
                    ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                    return null;
                }

                Level *= 10;
                Level += DigitValue;

                do
                    RawEffectString = RawEffectString.Substring(0, RawEffectString.Length - 1);
                while (RawEffectString.Length > 0 && RawEffectString[RawEffectString.Length - 1] == '_');
            }

            ServerInfoEffectType ParsedServerInfoEffect;
            StringToEnumConversion<ServerInfoEffectType>.TryParse(RawEffectString, out ParsedServerInfoEffect, ErrorInfo);
            ServerInfoEffect = ParsedServerInfoEffect;
            if (Level > 0)
                EffectLevel = Level;

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
                case ServerInfoEffectType.SummonFlower:
                case ServerInfoEffectType.SummonFlowerDisplay:
                case ServerInfoEffectType.SpawnMushroomFarmBox:
                case ServerInfoEffectType.SpawnItemDispenser:
                case ServerInfoEffectType.BestowGolemConditional:
                case ServerInfoEffectType.BestowGolemAbility:
                case ServerInfoEffectType.EquipmentBoost:
                case ServerInfoEffectType.PulseEvent:
                case ServerInfoEffectType.SetPrimaryCombatSkill:
                case ServerInfoEffectType.SetInteractionFlag:
                    if (Details == null)
                    {
                        ErrorInfo.AddInvalidObjectFormat("ServerInfo Effects");
                        return null;
                    }
                    break;

                case ServerInfoEffectType.SummonGruesomeSpookyPunch:
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

            if (LowValue < HighValue)
            {
                this.LowValue = LowValue;
                this.HighValue = HighValue;
            }
            else
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
        #endregion

        #region Json Reconstruction
        public override void GenerateObjectContent(JsonGenerator Generator)
        {
            Generator.OpenObject(Key);

            Generator.CloseObject();
        }
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
    }
}
