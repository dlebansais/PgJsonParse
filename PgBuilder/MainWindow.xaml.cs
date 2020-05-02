namespace PgBuilder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;
    using Microsoft.Win32;
    using System.Globalization;
    using PgJsonObjects;
    using PgJsonReader;

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Init
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            InitPowerKeyToCompleteEffectTable();
            LoadCachedData(out int Version);
            FillSkillList();
            FillGearSlotList();
            FillAbilitySlotList();
            AnalyzeCachedData(Version);

            LoadSettings();

            Closed += OnClosed;
        }

        private void InitPowerKeyToCompleteEffectTable()
        {
            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in PowerKeyToCompleteEffect.AbilityList)
            {
                string Key = Entry.Key;

                List<AbilityKeyword> AbilityList = PowerKeyToCompleteEffect.AbilityList[Key];
                List<CombatEffect> StaticCombatEffectList = PowerKeyToCompleteEffect.StaticCombatEffectList[Key];
                List<CombatEffect> DynamicCombatEffectList = PowerKeyToCompleteEffect.DynamicCombatEffectList[Key];
                List<AbilityKeyword> TargetAbilityList = PowerKeyToCompleteEffect.TargetAbilityList[Key];

                ModEffect ModEffect = new ModEffect(AbilityList, StaticCombatEffectList, DynamicCombatEffectList, TargetAbilityList);
                PowerKeyToCompleteEffectTable.Add(Key, ModEffect);
            }
        }

        private Dictionary<string, ModEffect> PowerKeyToCompleteEffectTable = new Dictionary<string, ModEffect>();
        #endregion

        #region Data Load
        public void LoadCachedData(out int version)
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            version = 0;
            string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
            foreach (string Item in VersionFolders)
                if (int.TryParse(Path.GetFileName(Item), out int VersionValue) && version < VersionValue)
                    version = VersionValue;

            string IconCacheFolder = Path.Combine(ApplicationFolder, "Shared Icons");
            string VersionFolder = Path.Combine(VersionCacheFolder, version.ToString());
            IconFolder = IconCacheFolder;

            LoadCachedData(VersionFolder, IconFolder);
        }

        public void LoadCachedData(string versionFolder, string iconFolder)
        {
            try
            {
                string CacheFileName = Path.Combine(versionFolder, "cache.pg");
                if (File.Exists(CacheFileName))
                {
                    byte[] Data = LoadBinaryFile(CacheFileName);
                    DeserializeAll(versionFolder, iconFolder, Data);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void DeserializeAll(string versionFolder, string iconFolder, byte[] data)
        {
            SerializableJsonObject.ResetSerializedObjectTable();
            GenericPgObject.ResetCreatedObjectTable();
            byte[] CurrentOffset = new byte[4];

            List<IObjectDefinition> DefinitionList = new List<IObjectDefinition>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                DefinitionList.Add(Entry.Value);

            for (int ProgressIndex = 0; ProgressIndex < DefinitionList.Count; ProgressIndex++)
            {
                DeserializeAll0(data, CurrentOffset, DefinitionList, ProgressIndex);
            }

            DeserializeAll2(versionFolder, iconFolder, data);
        }

        private bool DeserializeAll0(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            try
            {
                IObjectDefinition Definition = definitionList[progressIndex];
                int Offset = BitConverter.ToInt32(currentOffset, 0);

                Definition.JsonObjectList.Clear();

                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;
                PgObjectList.Clear();

                int Count = BitConverter.ToInt32(data, Offset);
                Offset += 4;

                int ObjectOffset = Offset;

                for (int i = 0; i < Count; i++)
                {
                    Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                    IMainPgObject Item = GenericPgObject.CreateMainObject(Definition.CreateNewObject, data, ref Offset);
                    PgObjectList.Add(Item);
                }

                Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
                Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void DeserializeAll2(string versionFolder, string iconFolder, byte[] data)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(PgJsonObjects.Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = (IPgPower)Entry.Value;
                Power.InitTierList(AttributeTable);
            }
        }

        private static byte[] LoadBinaryFile(string fileName)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BinaryReader br = new BinaryReader(fs);
            byte[] Result = br.ReadBytes((int)fs.Length);
            return Result;
        }

        public static string IconFolder { get; private set; }
        #endregion

        #region Data Analysis
        private void AnalyzeCachedData(int version)
        {
            FilterValidPowers(out _, out List<IPgPower> PowerSimpleEffectList);
            FilterValidEffects(out Dictionary<string, Dictionary<string, List<IPgEffect>>> AllEffectTable);
            FindPowersWithMatchingEffect(AllEffectTable, PowerSimpleEffectList, out Dictionary<IPgPower, List<IPgEffect>> PowerToEffectTable, out List<IPgPower> UnmatchedPowerList);
            GetAbilityNames(out List<string> AbilityNameList, out Dictionary<string, List<AbilityKeyword>> NameToKeyword);

            AnalyzeMatchingEffects(version, AbilityNameList, NameToKeyword, PowerToEffectTable);
            AnalyzeRemainingEffects(version, AbilityNameList, NameToKeyword, UnmatchedPowerList);

            CheckAllSentencesUsed();
        }

        private void FilterValidPowers(out List<IPgPower> powerAttributeList, out List<IPgPower> powerSimpleEffectList)
        {
            powerAttributeList = new List<IPgPower>();
            powerSimpleEffectList = new List<IPgPower>();

            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];

            List<ItemSlot> ValidSlotList = new List<ItemSlot>();
            foreach (GearSlot Item in GearSlotList)
                ValidSlotList.Add(Item.Slot);

            int TotalTierCount = 0;

            foreach (IPgPower Item in PowerDefinition.PgObjectList)
                FilterValidPowers(ValidSlotList, powerAttributeList, powerSimpleEffectList, Item, ref TotalTierCount);

            Debug.WriteLine($"{powerSimpleEffectList.Count + powerAttributeList.Count} powers, {powerAttributeList.Count} with attribute, {powerSimpleEffectList.Count} with description, Total: {TotalTierCount} mods");
        }

        private void FilterValidPowers(List<ItemSlot> validSlotList, List<IPgPower> powerAttributeList, List<IPgPower> powerSimpleEffectList, IPgPower power, ref int TotalTierCount)
        {
            IList<IPgPowerTier> TierEffectList = power.TierEffectList;

            Debug.Assert(TierEffectList.Count > 0);

            int AttributeCount = 0;
            int SimpleCount = 0;

            foreach (IPgPowerTier TierItem in TierEffectList)
                CheckAttributeOrSimple(TierItem, ref AttributeCount, ref SimpleCount);

            Debug.Assert(AttributeCount == 0 || SimpleCount == 0);

            if (!IsSlotCompatible(validSlotList, power))
                return;

            Debug.Assert(power.RawSkill == PowerSkill.Internal_None ||
                         power.RawSkill == PowerSkill.Unknown ||
                         SkillList.Contains(power.Skill) ||
                         power.RawSkill == PowerSkill.Gourmand ||
                         power.RawSkill == PowerSkill.AnySkill ||
                         power.RawSkill == PowerSkill.Endurance ||
                         power.RawSkill == PowerSkill.ArmorPatching ||
                         power.RawSkill == PowerSkill.ShamanicInfusion);

            if (power.RawSkill == PowerSkill.Internal_None || power.RawSkill == PowerSkill.Unknown || power.RawSkill == PowerSkill.Gourmand)
                return;
            if (power.IsUnavailable)
                return;

            if (AttributeCount > 0)
                powerAttributeList.Add(power);
            else
                powerSimpleEffectList.Add(power);

            TotalTierCount += TierEffectList.Count;
        }

        private void CheckAttributeOrSimple(IPgPowerTier powerTier, ref int attributeCount, ref int simpleCount)
        {
            IList<IPgPowerEffect> ItemEffectList = powerTier.EffectList;

            int EffectListCount = ItemEffectList.Count;
            for (int i = 0; i < EffectListCount; i++)
            {
                IPgPowerEffect ItemEffect = ItemEffectList[i];

                Debug.Assert((ItemEffect is IPgPowerAttributeLink) || (ItemEffect is IPgPowerSimpleEffect));

                if (ItemEffect is IPgPowerAttributeLink)
                    attributeCount++;

                if (ItemEffect is IPgPowerSimpleEffect)
                    simpleCount++;
            }
        }

        private bool IsSlotCompatible(List<ItemSlot> validSlotList, IPgPower power)
        {
            foreach (ItemSlot SlotItem in power.SlotList)
                if (validSlotList.Contains(SlotItem))
                    return true;

            return false;
        }

        private void FilterValidEffects(out Dictionary<string, Dictionary<string, List<IPgEffect>>> allEffectTable)
        {
            IObjectDefinition EffectDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Effect)];

            allEffectTable = new Dictionary<string, Dictionary<string, List<IPgEffect>>>();

            foreach (IPgEffect Item in EffectDefinition.PgObjectList)
            {
                if (Item.Key.Length <= 12 || Item.Key[Item.Key.Length - 3] != '0')
                    continue;

                Debug.Assert(Item.AbilityKeywordList.Count > 0);

                bool IsIntSuffix = int.TryParse(Item.Key.Substring(Item.Key.Length - 3), out int TierIndex);
                Debug.Assert(IsIntSuffix);
                Debug.Assert(TierIndex >= 1 && TierIndex < 30);

                string Subkey = Item.Key.Substring(0, Item.Key.Length - 3);
                string PowerKey = Subkey.Substring(Subkey.Length - 3);

                if (!allEffectTable.ContainsKey(PowerKey))
                    allEffectTable.Add(PowerKey, new Dictionary<string, List<IPgEffect>>());
                Dictionary<string, List<IPgEffect>> SameKeyTable = allEffectTable[PowerKey];

                if (!SameKeyTable.ContainsKey(Subkey))
                    SameKeyTable.Add(Subkey, new List<IPgEffect>());
                SameKeyTable[Subkey].Add(Item);
            }
        }

        private void FindPowersWithMatchingEffect(Dictionary<string, Dictionary<string, List<IPgEffect>>> allEffectTable, List<IPgPower> powerSimpleEffectList, out Dictionary<IPgPower, List<IPgEffect>> powerToEffectTable, out List<IPgPower> unmatchedPowerList)
        {
            powerToEffectTable = new Dictionary<IPgPower, List<IPgEffect>>();
            unmatchedPowerList = new List<IPgPower>();

            foreach (IPgPower Item in powerSimpleEffectList)
                if (FindPowersWithMatchingEffect(allEffectTable, Item, out List<IPgEffect> MatchingEffectList))
                    powerToEffectTable.Add(Item, MatchingEffectList);
                else
                    unmatchedPowerList.Add(Item);
        }

        private bool FindPowersWithMatchingEffect(Dictionary<string, Dictionary<string, List<IPgEffect>>> allEffectTable, IPgPower power, out List<IPgEffect> matchingEffectList)
        {
            matchingEffectList = null;

            string Key = power.Key;
            Debug.Assert(Key.Length >= 9);

            string PowerKey = Key.Substring(Key.Length - 3);
            if (!allEffectTable.ContainsKey(PowerKey))
                return false;

            Dictionary<string, List<IPgEffect>> SameKeyTable = allEffectTable[PowerKey];
            IList<IPgPowerTier> TierEffectList = power.TierEffectList;
            List<string> MatchingKeyList = new List<string>();
            List<string> OneToOneMatchingKeyList = new List<string>();

            foreach (KeyValuePair<string, List<IPgEffect>> Entry in SameKeyTable)
            {
                List<IPgEffect> TierList = Entry.Value;
                if (TierEffectList.Count != TierList.Count)
                    continue;

                if (HasCommonIcon(power, TierList, out bool IsOneToOne))
                {
                    MatchingKeyList.Add(Entry.Key);

                    if (IsOneToOne)
                        OneToOneMatchingKeyList.Add(Entry.Key);
                }
            }

            if (MatchingKeyList.Count == 0)
                return false;

            if (OneToOneMatchingKeyList.Count > 1)
            {
                Debug.WriteLine($"Possible mod bug: {power}");
                return false;
            }

            int MaxEffectSameTier = 0;
            foreach (IPgPowerTier Item in TierEffectList)
            {
                IList<IPgPowerEffect> EffectList = Item.EffectList;
                if (MaxEffectSameTier < EffectList.Count)
                    MaxEffectSameTier = EffectList.Count;
            }

            if (MaxEffectSameTier > 1)
                return false;

            if (OneToOneMatchingKeyList.Count == 0)
            {
                if (MatchingKeyList.Count > 1)
                {
                    Debug.WriteLine($"Possible mod bug ({OneToOneMatchingKeyList.Count}): {power}");
                    matchingEffectList = SameKeyTable[MatchingKeyList[1]];
                }
                else
                    matchingEffectList = SameKeyTable[MatchingKeyList[0]];
            }
            else
                matchingEffectList = SameKeyTable[OneToOneMatchingKeyList[0]];

            return true;
        }

        private bool HasCommonIcon(IPgPower power, List<IPgEffect> effectList, out bool isOneToOne)
        {
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = (IList<IPgAbility>)AbilityDefinition.VerifiedObjectList;

            List<int> PowerIconList = new List<int>();
            List<int> EffectIconList = new List<int>();

            foreach (IPgPowerTier Item in power.TierEffectList)
            {
                foreach (IPgPowerEffect PowerEffectItem in Item.EffectList)
                {
                    Debug.Assert(PowerEffectItem is IPgPowerSimpleEffect);
                    IPgPowerSimpleEffect SimpleEffect = (IPgPowerSimpleEffect)PowerEffectItem;

                    foreach (int Id in SimpleEffect.IconIdList)
                        if (!PowerIconList.Contains(Id))
                            PowerIconList.Add(Id);
                }
            }

            List<AbilityKeyword> AbilityKeywordList = new List<AbilityKeyword>();
            foreach (IPgEffect EffectItem in effectList)
                foreach (AbilityKeyword Keyword in EffectItem.AbilityKeywordList)
                    if (!AbilityKeywordList.Contains(Keyword))
                        AbilityKeywordList.Add(Keyword);

            if (AbilityKeywordList.Contains(AbilityKeyword.NiceAttack) ||
                AbilityKeywordList.Contains(AbilityKeyword.CoreAttack) ||
                AbilityKeywordList.Contains(AbilityKeyword.BasicAttack) ||
                AbilityKeywordList.Contains(AbilityKeyword.MajorHeal) ||
                AbilityKeywordList.Contains(AbilityKeyword.MinorHealTargeted) ||
                AbilityKeywordList.Contains(AbilityKeyword.SignatureDebuff) ||
                AbilityKeywordList.Contains(AbilityKeyword.SignatureSupport))
                EffectIconList.Add(108);

            if (AbilityKeywordList.Contains(AbilityKeyword.Sword) && power.RawSkill == PowerSkill.Sword)
                EffectIconList.Add(108);

            if (AbilityKeywordList.Contains(AbilityKeyword.FireSpell) && power.RawSkill == PowerSkill.FireMagic)
                EffectIconList.Add(108);

            if (AbilityKeywordList.Contains(AbilityKeyword.Unarmed) && power.RawSkill == PowerSkill.Unarmed)
                EffectIconList.Add(108);

            if (AbilityKeywordList.Contains(AbilityKeyword.Melee))
            {
                EffectIconList.Add(108);

                if (PowerIconList.Count == 0)
                    PowerIconList.Add(108);
            }

            foreach (IPgAbility AbilityItem in AbilityList)
            {
                bool KeywordMatch = false;

                foreach (AbilityKeyword Keyword in AbilityItem.KeywordList)
                    if (AbilityKeywordList.Contains(Keyword))
                    {
                        KeywordMatch = true;
                        break;
                    }

                if (KeywordMatch)
                {
                    if (!EffectIconList.Contains(AbilityItem.IconId))
                        EffectIconList.Add(AbilityItem.IconId);
                }
            }

            isOneToOne = PowerIconList.Count == 1 && EffectIconList.Count == 1;

            foreach (int IconId in PowerIconList)
                if (EffectIconList.Contains(IconId))
                    return true;

            return false;
        }

        private List<AbilityKeyword> KeywordIgnoreList = new List<AbilityKeyword>()
        {
            AbilityKeyword.Lint_NotLearnable,
            AbilityKeyword.Lint_HarmlessWithDamageBoosts,
            AbilityKeyword.Attack,
            AbilityKeyword.BasicAttack,
            AbilityKeyword.NiceAttack,
            AbilityKeyword.CoreAttack,
            AbilityKeyword.EpicAttack,
            AbilityKeyword.CombatRefresh,
            AbilityKeyword.SignatureDebuff,
            AbilityKeyword.SignatureSupport,
            AbilityKeyword.MajorHeal,
            AbilityKeyword.MinorHeal,
            AbilityKeyword.MinorHealTargeted,
            AbilityKeyword.Burst,
            AbilityKeyword.SurvivalUtility,
            AbilityKeyword.FistAttack,
            AbilityKeyword.FireMagicAttack,
            AbilityKeyword.Melee,
            AbilityKeyword.Ranged,
            AbilityKeyword.Kick,
            AbilityKeyword.BodyPartAttack,
            AbilityKeyword.BodypartAttack,
            AbilityKeyword.BarrageOnly,
            AbilityKeyword.SerpentStrike,
            AbilityKeyword.HipSlam,
            AbilityKeyword.FireSpell,
            AbilityKeyword.FireBurst,
            AbilityKeyword.SelfImmolation,
            AbilityKeyword.PsychologyAttack,
            AbilityKeyword.PsychologyHeal,
            AbilityKeyword.PhrenologyCriticals,
            AbilityKeyword.AnatomyCriticals,
            AbilityKeyword.WerewolfAttack,
            AbilityKeyword.DeerAttack,
            AbilityKeyword.CowAttack,
            AbilityKeyword.StaffAttack,
            AbilityKeyword.Mutation,
            AbilityKeyword.BattleChemistryAttack,
            AbilityKeyword.SummonSkeletonArcherOrMage,
            AbilityKeyword.SummonSkeletonArcherOrSwordsman,
            AbilityKeyword.SummonSkeletonSwordsmanOrMage,
            AbilityKeyword.SpiderAttack,
            AbilityKeyword.HammerNonBasic,
            AbilityKeyword.DruidHeal,
            AbilityKeyword.IceMagicSingleTarget,
            AbilityKeyword.SummonedColdSphere,
            AbilityKeyword.KnifeCut,
            AbilityKeyword.KnifeNonCut,
            AbilityKeyword.BardBlast,
            AbilityKeyword.SummonedFireWall,
            AbilityKeyword.PigAttack,
            AbilityKeyword.ChemistryBomb,
            AbilityKeyword.Bomb,
            AbilityKeyword.SummonSkeleton,
            AbilityKeyword.PriestAttack,
            AbilityKeyword.HeavyArchery,
            AbilityKeyword.Unarmed,
            AbilityKeyword.FireMagic,
            AbilityKeyword.Psychology,
            AbilityKeyword.Werewolf,
            AbilityKeyword.Deer,
            AbilityKeyword.Cow,
            AbilityKeyword.BattleChemistry,
            AbilityKeyword.Pig,
            AbilityKeyword.Staff,
            AbilityKeyword.Necromancy,
            AbilityKeyword.Spider,
            AbilityKeyword.Shield,
            AbilityKeyword.Hammer,
            AbilityKeyword.Druid,
            AbilityKeyword.IceMagic,
            AbilityKeyword.BardSong,
            AbilityKeyword.Bard,
            AbilityKeyword.Knife,
            AbilityKeyword.Rabbit,
            AbilityKeyword.Priest,
            AbilityKeyword.Archery,
        };

        private Dictionary<string, string> KnownBaseAbilityNameTable = new Dictionary<string, string>()
        {
            { "Boiling Veins", "Molten Veins" },
            { "Super Warmthball", "Super Fireball" },
            { "Heat Breath", "Fire Breath" },
            { "Chillball", "Frostball" },
            { "Warmthball", "Fireball" },
            { "Flare Fireball", "Fireball" },
            { "Call Living Stabled Pet", "Call Stabled Pet" },
            { "Raise Skeletal Ratkin Mage", "Raise Skeletal Battle Mage" },
            { "Slicing Ice", "Slice" },
            { "Pouncing Rend", "Pouncing Rake" },
            { "Pinning Slash", "Pin" },
            { "Free-Summon Skeletal Archer", "Raise Skeletal Archer" },
            { "Rotflesh", "Rotskin" },
        };

        private Dictionary<string, List<AbilityKeyword>> WideAbilityTable = new Dictionary<string, List<AbilityKeyword>>()
        {
            { "Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack } },
            { "Core Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack } },
            { "Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.EpicAttack } },
            { "Basic Attack", new List<AbilityKeyword>() { AbilityKeyword.BasicAttack } },
            { "Nice and Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack, AbilityKeyword.EpicAttack } },
            { "Core and Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack, AbilityKeyword.NiceAttack } },
            { "Signature Support", new List<AbilityKeyword>() { AbilityKeyword.SignatureSupport } },
            { "Signature Debuff", new List<AbilityKeyword>() { AbilityKeyword.SignatureDebuff } },
            { "Major Healing", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
            { "Minor Healing", new List<AbilityKeyword>() { AbilityKeyword.MinorHeal } },
            { "Minor Heal", new List<AbilityKeyword>() { AbilityKeyword.MinorHeal } },
            { "First Aid", new List<AbilityKeyword>() { AbilityKeyword.FirstAid } },
            { "Crossbow", new List<AbilityKeyword>() { AbilityKeyword.Crossbow } },
            { "Ranged Attack", new List<AbilityKeyword>() { AbilityKeyword.Ranged } },
            { "All Sword", new List<AbilityKeyword>() { AbilityKeyword.Sword } },
            { "All Fire spell", new List<AbilityKeyword>() { AbilityKeyword.FireSpell } },
            { "Unarmed attack", new List<AbilityKeyword>() { AbilityKeyword.Unarmed } },
            { "Kick attack", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
            { "Any Kick ability", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
            { "All kicks", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
            { "Bomb attack", new List<AbilityKeyword>() { AbilityKeyword.Bomb } },
            { "All Psi Wave Abilities", new List<AbilityKeyword>() { AbilityKeyword.PsiWave } },
            { "All types of shield Bash", new List<AbilityKeyword>() { AbilityKeyword.Bash } },
            { "All Shield Bash ability", new List<AbilityKeyword>() { AbilityKeyword.Bash } },
            { "Hammer attack", new List<AbilityKeyword>() { AbilityKeyword.HammerAttack } },
            { "All Druid ability", new List<AbilityKeyword>() { AbilityKeyword.Druid } },
            { "All Staff attack", new List<AbilityKeyword>() { AbilityKeyword.StaffAttack } },
            { "All Ice Magic ability that hit multiple", new List<AbilityKeyword>() { AbilityKeyword.IceMagicAoE } },
            { "All Ice Magic attack that hit a single", new List<AbilityKeyword>() { AbilityKeyword.IceMagicSingleTarget } },
            { "All Ice Magic ability", new List<AbilityKeyword>() { AbilityKeyword.IceMagic } },
            { "Knife ability with 'Cut' in their name", new List<AbilityKeyword>() { AbilityKeyword.KnifeCut } },
            { "All Knife ability WITHOUT 'Cut' in their name", new List<AbilityKeyword>() { AbilityKeyword.KnifeNonCut } },
            { "All Knife Fighting attack", new List<AbilityKeyword>() { AbilityKeyword.Knife } },
            { "Bard Songs", new List<AbilityKeyword>() { AbilityKeyword.BardSong } },
            //{ "Spider Skill", new List<AbilityKeyword>() { AbilityKeyword.Spider } },
            { "All Major Healing ability targeting you", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
            { "All Bun-Fu moves", new List<AbilityKeyword>() { AbilityKeyword.Rabbit } },
            { "Survival Utility", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility } },
            { "Survival Utility and Major Heal", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility, AbilityKeyword.MajorHeal } },
            { "Knee Spikes Mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_KneeSpikes} },
            { "Extra Skin mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraSkin} },
            { "Extra Heart mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart } },
            { "Extra Heart and Stretchy Spine mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart, AbilityKeyword.Mutation_StretchySpine } },
            { "Stretchy Spine mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_StretchySpine } },
            { "Animal Handling pets", new List<AbilityKeyword>() { AbilityKeyword.StabledPet } },
            { "Allies' Combat Refreshes", new List<AbilityKeyword>() { AbilityKeyword.CombatRefresh } },
            { "Raised Zombies", new List<AbilityKeyword>() { AbilityKeyword.SummonZombie } },
            { "Major Heal", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        };

        private List<AbilityKeyword> GenericAbilityList = new List<AbilityKeyword>()
        {
            AbilityKeyword.NiceAttack,
            AbilityKeyword.CoreAttack,
            AbilityKeyword.EpicAttack,
            AbilityKeyword.BasicAttack,
            AbilityKeyword.SignatureSupport,
            AbilityKeyword.SignatureDebuff,
            AbilityKeyword.MajorHeal,
            AbilityKeyword.MinorHeal,
            AbilityKeyword.FirstAid,
            AbilityKeyword.Crossbow,
            AbilityKeyword.Ranged,
            AbilityKeyword.Sword,
            AbilityKeyword.FireSpell,
            AbilityKeyword.Unarmed,
            AbilityKeyword.Kick,
            AbilityKeyword.Bomb,
            AbilityKeyword.PsiWave,
            AbilityKeyword.Bash,
            AbilityKeyword.HammerAttack,
            AbilityKeyword.Druid,
            AbilityKeyword.StaffAttack,
            AbilityKeyword.IceMagicAoE,
            AbilityKeyword.IceMagicSingleTarget,
            AbilityKeyword.IceMagic,
            AbilityKeyword.KnifeCut,
            AbilityKeyword.KnifeNonCut,
            AbilityKeyword.Knife,
            AbilityKeyword.BardSong,
            //AbilityKeyword.Spider,
            AbilityKeyword.SurvivalUtility,
            AbilityKeyword.MajorHeal,
            AbilityKeyword.Rabbit,
            AbilityKeyword.Mutation_KneeSpikes,
            AbilityKeyword.Mutation_ExtraSkin,
            AbilityKeyword.Mutation_ExtraHeart,
            AbilityKeyword.Mutation_StretchySpine,
            AbilityKeyword.StabledPet,
            AbilityKeyword.CombatRefresh,
            AbilityKeyword.SummonZombie,
        };

        private void GetAbilityNames(out List<string> abilityNameList, out Dictionary<string, List<AbilityKeyword>> nameToKeyword)
        {
            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = (IList<IPgAbility>)AbilityDefinition.VerifiedObjectList;

            Dictionary<AbilityKeyword, string> KeywordToName = new Dictionary<AbilityKeyword, string>();

            abilityNameList = new List<string>();

            foreach (IPgAbility Item in AbilityList)
            {
                if (!SkillList.Contains(Item.Skill))
                    continue;

                string Name = Item.BaseName;

                if (Name.Length > 0 && Name[0] == '(')
                    continue;
                if (Name.EndsWith(" (Energy Bow)"))
                    continue;
                if (Name.EndsWith(" 3B"))
                    continue;
                if (Name.EndsWith(" 3+"))
                    continue;
                if (Name.EndsWith(" #"))
                    continue;
                if (Name.EndsWith(" (Purple)"))
                    continue;
                if (Name == "Cold Protection")
                    continue;

                if (KnownBaseAbilityNameTable.ContainsKey(Name))
                    Name = KnownBaseAbilityNameTable[Name];

                if (!abilityNameList.Contains(Name))
                {
                    List<AbilityKeyword> KeywordList = new List<AbilityKeyword>(Item.KeywordList);

                    if (KeywordList.Count > 0)
                    {
                        foreach (AbilityKeyword Keyword in KeywordIgnoreList)
                            if (KeywordList.Contains(Keyword))
                                KeywordList.Remove(Keyword);

                        if (KeywordList.Count == 0)
                            Debug.WriteLine($"{Name} has no keyword.");
                        else
                        {
                            if (KeywordList.Count > 1)
                            {
                                AbilityKeyword MatchingKeyword = AbilityKeyword.Internal_None;
                                foreach (AbilityKeyword Keyword in KeywordList)
                                    if (TextMaps.AbilityKeywordTextMap[Keyword] == Name)
                                    {
                                        MatchingKeyword = Keyword;
                                        break;
                                    }

                                if (MatchingKeyword != AbilityKeyword.Internal_None)
                                    KeywordList = new List<AbilityKeyword>() { MatchingKeyword };
                            }

                            if (KeywordList.Count > 1)
                            {
                                string KeywordListString = string.Empty;

                                foreach (AbilityKeyword Keyword in KeywordList)
                                {
                                    if (KeywordListString.Length > 0)
                                        KeywordListString += ", ";

                                    KeywordListString += Keyword.ToString();
                                }

                                Debug.WriteLine($"{Name} has more than one keyword: {KeywordListString}.");
                            }
                            else
                            {
                                AbilityKeyword MatchingKeyword = KeywordList[0];

                                if (KeywordToName.ContainsKey(MatchingKeyword))
                                    Debug.WriteLine($"Keyword {MatchingKeyword} for {Name} is already used by {KeywordToName[MatchingKeyword]}.");
                                else
                                    KeywordToName.Add(MatchingKeyword, Name);
                            }
                        }
                    }

                    abilityNameList.Add(Name);
                }
            }

            AddAbilityToNameList(abilityNameList, "Werewolf Claw");
            AddAbilityToNameList(abilityNameList, "Armor Wave");
            AddAbilityToNameList(abilityNameList, "Health Wave");
            AddAbilityToNameList(abilityNameList, "Power Wave");
            AddAbilityToNameList(abilityNameList, "Fire Walls'");
            AddAbilityToNameList(abilityNameList, "Summoned Skeletal Swordsmen");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemTauntingPunch, "Taunting Punch");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.PoisonBombToss, "Poison Bomb");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemSelfDestruct, "Self Destruct");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemRageAcidToss, "Rage Acid Toss");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemDoomAdmixture, "Doom Admixture");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemRageMist, "Rage Mist");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemInvigoratingMist, "Invigorating Mist");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemSelfSacrifice, "Self Sacrifice");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.GolemFireBalm, "Fire Balm");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonedFireWall, "Fire Walls");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonSkeleton, "Summoned Skeletons");
            AddAbilityToNameListAndTable(abilityNameList, KeywordToName, AbilityKeyword.SummonSkeletonArcherOrMage, "Summoned Skeletal Archers and Mages");

            nameToKeyword = new Dictionary<string, List<AbilityKeyword>>();
            foreach (KeyValuePair<AbilityKeyword, string> Entry in KeywordToName)
            {
                Debug.Assert(abilityNameList.Contains(Entry.Value));
                nameToKeyword.Add(Entry.Value, new List<AbilityKeyword>() { Entry.Key });
            }

            nameToKeyword.Add("Fire Walls'", new List<AbilityKeyword>() { AbilityKeyword.SummonedFireWall });
            nameToKeyword.Add("Summoned Skeletal Swordsmen", new List<AbilityKeyword>() { AbilityKeyword.SummonSkeletonSwordsman });

            foreach (AbilityKeyword Keyword in GenericAbilityList)
            {
                bool IsInTable = false;
                foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
                    if (Entry.Value.Contains(Keyword))
                    {
                        IsInTable = true;
                        break;
                    }

                Debug.Assert(IsInTable);
            }

            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
                foreach (AbilityKeyword Keyword in Entry.Value)
                    Debug.Assert(GenericAbilityList.Contains(Keyword));

            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
            {
                string Pattern = Entry.Key;
                List<AbilityKeyword> KeywordList = Entry.Value;

                foreach (AbilityKeyword Keyword in KeywordList)
                    Debug.Assert(GenericAbilityList.Contains(Keyword));

                if (!Pattern.EndsWith(" ability"))
                {
                    string PatternWithAbility = Pattern + " ability";

                    Debug.Assert(!nameToKeyword.ContainsKey(PatternWithAbility));
                    nameToKeyword.Add(PatternWithAbility, KeywordList);
                    abilityNameList.Add(PatternWithAbility);
                }

                Debug.Assert(!nameToKeyword.ContainsKey(Pattern));
                nameToKeyword.Add(Pattern, KeywordList);
                abilityNameList.Add(Pattern);
            }

            abilityNameList.Sort();

            for (int i = 0; i < abilityNameList.Count; i++)
            {
                string SmallName = abilityNameList[i];

                for (int j = i + 1; j < abilityNameList.Count; j++)
                {
                    string LargeName = abilityNameList[j];

                    if (LargeName.Contains(SmallName))
                    {
                        abilityNameList.RemoveAt(j);
                        abilityNameList.RemoveAt(i);
                        abilityNameList.Insert(i, LargeName);
                        abilityNameList.Insert(i + 1, SmallName);
                        i--;
                        break;
                    }
                }
            }
        }

        private void AddAbilityToNameList(List<string> abilityNameList, string abilityName)
        {
            Debug.Assert(!abilityNameList.Contains(abilityName));

            abilityNameList.Add(abilityName);
        }

        private void AddAbilityToNameListAndTable(List<string> abilityNameList, Dictionary<AbilityKeyword, string> keywordToName, AbilityKeyword keyword, string abilityName)
        {
            Debug.Assert(!abilityNameList.Contains(abilityName));

            abilityNameList.Add(abilityName);
            keywordToName.Add(keyword, abilityName);
        }
        #endregion

        #region Data Analysis, Matching
        bool WriteFile = false;
        bool CompareTable = false;
        const int AnalyzedVersionMatching = 334;

        private void AnalyzeMatchingEffects(int version, List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, Dictionary<IPgPower, List<IPgEffect>> powerToEffectTable)
        {
            if (version <= AnalyzedVersionMatching)
                return;

            int DebugIndex = 0;
            int SkipIndex = 0;
            List<string[]> StringKeyTable = new List<string[]>();
            List<ModEffect[]> LocalPowerKeyToCompleteEffectTable = new List<ModEffect[]>();

            foreach (KeyValuePair<IPgPower, List<IPgEffect>> Entry in powerToEffectTable)
            {
                DebugIndex++;

                if (SkipIndex > 0)
                {
                    SkipIndex--;
                    continue;
                }

                //Debug.WriteLine("");
                //Debug.WriteLine($"Debug Index: {DebugIndex - 1} / {powerToEffectTable.Count} (Matching)");

                IPgPower ItemPower = Entry.Key;
                List<IPgEffect> ItemEffectList = Entry.Value;

                AnalyzeMatchingEffects(abilityNameList, nameToKeyword, ItemPower, ItemEffectList, out string[] stringKeyArray, out ModEffect[] ModEffectArray);

                StringKeyTable.Add(stringKeyArray);
                LocalPowerKeyToCompleteEffectTable.Add(ModEffectArray);
            }

            if (WriteFile)
                WritePowerKeyToCompleteEffectFile(StringKeyTable, LocalPowerKeyToCompleteEffectTable);
            if (CompareTable)
                CompareWithPowerKeyToCompleteEffectTable(StringKeyTable, LocalPowerKeyToCompleteEffectTable);

            DisplayParsingResult(powerToEffectTable);
        }

        private void WritePowerKeyToCompleteEffectFile(List<string[]> stringKeyTable, List<ModEffect[]> powerKeyToCompleteEffectTable)
        {
            using (FileStream fs = new FileStream("PowerKeyToCompleteEffect.cs", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    Debug.Assert(stringKeyTable.Count == powerKeyToCompleteEffectTable.Count);

                    sw.WriteLine("namespace PgBuilder");
                    sw.WriteLine("{");
                    sw.WriteLine("    using System.Collections.Generic;");
                    sw.WriteLine("    using PgJsonObjects;");
                    sw.WriteLine("");
                    sw.WriteLine("    public static class PowerKeyToCompleteEffect");
                    sw.WriteLine("    {");
                    sw.WriteLine("        public static Dictionary<string, List<AbilityKeyword>> AbilityList { get; } = new Dictionary<string, List<AbilityKeyword>>()");
                    sw.WriteLine("        {");

                    for (int i = 0; i < stringKeyTable.Count; i++)
                    {
                        string[] StringKeyArray = stringKeyTable[i];
                        ModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                        Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                        for (int j = 0; j < StringKeyArray.Length; j++)
                        {
                            string StringKey = StringKeyArray[j];
                            ModEffect ModEffect = ModEffectArray[j];

                            WritePowerKeyToCompleteEffectAbilityKeywordListLine(sw, StringKey, ModEffect.AbilityList);
                        }
                    }

                    sw.WriteLine("        };");
                    sw.WriteLine("");
                    sw.WriteLine("        public static Dictionary<string, List<CombatEffect>> StaticCombatEffectList { get; } = new Dictionary<string, List<CombatEffect>>()");
                    sw.WriteLine("        {");

                    for (int i = 0; i < stringKeyTable.Count; i++)
                    {
                        string[] StringKeyArray = stringKeyTable[i];
                        ModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                        Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                        for (int j = 0; j < StringKeyArray.Length; j++)
                        {
                            string StringKey = StringKeyArray[j];
                            ModEffect ModEffect = ModEffectArray[j];

                            WritePowerKeyToCompleteEffectCombatEffectListLine(sw, StringKey, ModEffect.StaticCombatEffectList);
                        }
                    }

                    sw.WriteLine("        };");
                    sw.WriteLine("");
                    sw.WriteLine("        public static Dictionary<string, List<CombatEffect>> DynamicCombatEffectList { get; } = new Dictionary<string, List<CombatEffect>>()");
                    sw.WriteLine("        {");

                    for (int i = 0; i < stringKeyTable.Count; i++)
                    {
                        string[] StringKeyArray = stringKeyTable[i];
                        ModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                        Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                        for (int j = 0; j < StringKeyArray.Length; j++)
                        {
                            string StringKey = StringKeyArray[j];
                            ModEffect ModEffect = ModEffectArray[j];

                            WritePowerKeyToCompleteEffectCombatEffectListLine(sw, StringKey, ModEffect.DynamicCombatEffectList);
                        }
                    }

                    sw.WriteLine("        };");
                    sw.WriteLine("");
                    sw.WriteLine("        public static Dictionary<string, List<AbilityKeyword>> TargetAbilityList { get; } = new Dictionary<string, List<AbilityKeyword>>()");
                    sw.WriteLine("        {");

                    for (int i = 0; i < stringKeyTable.Count; i++)
                    {
                        string[] StringKeyArray = stringKeyTable[i];
                        ModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                        Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                        for (int j = 0; j < StringKeyArray.Length; j++)
                        {
                            string StringKey = StringKeyArray[j];
                            ModEffect ModEffect = ModEffectArray[j];

                            WritePowerKeyToCompleteEffectAbilityKeywordListLine(sw, StringKey, ModEffect.TargetAbilityList);
                        }
                    }

                    sw.WriteLine("        };");
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }
        }

        private void WritePowerKeyToCompleteEffectAbilityKeywordListLine(StreamWriter sw, string stringKey, List<AbilityKeyword> abilityList)
        {
            string AbilityKeywordListString = AbilityKeywordListToLongString(abilityList);

            sw.WriteLine($"            {{ \"{stringKey}\", new List<AbilityKeyword>() {AbilityKeywordListString} }},");
        }

        private void WritePowerKeyToCompleteEffectCombatEffectListLine(StreamWriter sw, string stringKey, List<CombatEffect> combatEffectList)
        {
            string CombatEffectListString = CombatEffectListToString(combatEffectList);

            sw.WriteLine($"            {{ \"{stringKey}\", new List<CombatEffect>() {CombatEffectListString} }},");
        }

        private string CombatEffectListToString(List<CombatEffect> combatEffectList)
        {
            string CombatEffectListString = string.Empty;

            for (int j = 0; j < combatEffectList.Count; j++)
            {
                CombatEffect CombatEffect = combatEffectList[j];

                if (CombatEffectListString.Length > 0)
                    CombatEffectListString += ", ";

                CombatEffectListString += CombatEffectToString(CombatEffect);
            }

            if (CombatEffectListString.Length == 0)
                CombatEffectListString = "{ }";
            else
                CombatEffectListString = $"{{ {CombatEffectListString} }}";

            return CombatEffectListString;
        }

        private string CombatEffectToString(CombatEffect combatEffect)
        {
            string CombatEffectString;

            if (!combatEffect.Data1.IsValueSet && combatEffect.DamageType == GameDamageType.None && combatEffect.CombatSkill == GameCombatSkill.None)
                CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword})";
            else
            {
                if (combatEffect.Data1.IsValueSet)
                {
                    string CreateMethod = combatEffect.Data1.IsPercent ? "FromDoublePercent" : "FromDouble";
                    string NumericValueString = $"NumericValue.{CreateMethod}({combatEffect.Data1.Value.ToString(CultureInfo.InvariantCulture)})";

                    if (combatEffect.DamageType == GameDamageType.None && combatEffect.CombatSkill == GameCombatSkill.None)
                        CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, {NumericValueString})";
                    else
                        CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, {NumericValueString}, new NumericValue(), (GameDamageType){(int)combatEffect.DamageType}, GameCombatSkill.{combatEffect.CombatSkill})";
                }
                else
                    CombatEffectString = $"new CombatEffect(CombatKeyword.{combatEffect.Keyword}, new NumericValue(), new NumericValue(), (GameDamageType){(int)combatEffect.DamageType}, GameCombatSkill.{combatEffect.CombatSkill})";
            }

            return CombatEffectString;
        }

        private void CompareWithPowerKeyToCompleteEffectTable(List<string[]> stringKeyTable, List<ModEffect[]> powerKeyToCompleteEffectTable)
        {
            Debug.Assert(stringKeyTable.Count == powerKeyToCompleteEffectTable.Count);

            for (int i = 0; i < stringKeyTable.Count; i++)
            {
                string[] StringKeyArray = stringKeyTable[i];
                ModEffect[] ModEffectArray = powerKeyToCompleteEffectTable[i];

                Debug.Assert(StringKeyArray.Length == ModEffectArray.Length);

                for (int j = 0; j < StringKeyArray.Length; j++)
                {
                    string StringKey = StringKeyArray[j];
                    ModEffect ModEffect = ModEffectArray[j];

                    CompareWithPowerKeyToCompleteEffectLine(i, StringKey, ModEffect);
                }
            }
        }

        private void CompareWithPowerKeyToCompleteEffectLine(int index, string stringKey, ModEffect modEffect)
        {
            if (!PowerKeyToCompleteEffectTable.ContainsKey(stringKey))
            {
                Debug.WriteLine($"Index #{index}, Key missing: {stringKey}");
                return;
            }

            if (!ModEffect.IsEqualStrict(PowerKeyToCompleteEffectTable[stringKey], modEffect))
            {
                Debug.WriteLine($"Index #{index}, Line mistmatch for key {stringKey}");
            }
        }

        private void CheckAllSentencesUsed()
        {
            foreach (Sentence Item in SentenceList)
                if (!Item.IsUsed)
                    Debug.WriteLine($"Sentence '{Item.Format}' not used");
        }

        private string PowerEffectPairKey(IPgPower power, int tierIndex)
        {
            string PowerTierString = (tierIndex + 1).ToString("D03");
            string Key = $"{power.Key}_{PowerTierString}";

            return Key;
        }

        private void DisplayParsingResult(Dictionary<IPgPower, List<IPgEffect>> powerToEffectTable)
        {
            foreach (KeyValuePair<IPgPower, List<IPgEffect>> Entry in powerToEffectTable)
            {
                IList<IPgPowerTier> TierEffectList = Entry.Key.TierEffectList;
                IPgPowerTier LastTier = TierEffectList[TierEffectList.Count - 1];
                IPgPowerEffectCollection EffectList = LastTier.EffectList;

                foreach (IPgPowerEffect ItemEffect in EffectList)
                {
                    if (ItemEffect is IPgPowerSimpleEffect AsSimpleEffect)
                    {
                        List<IPgEffect> TierList = Entry.Value;
                        IPgEffect Effect = TierList[TierList.Count - 1];

                        Debug.WriteLine("");
                        Debug.WriteLine(AsSimpleEffect.Description);
                        Debug.WriteLine(Effect.Desc);
                        break;
                    }
                }
            }
        }

        private void AnalyzeMatchingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPower itemPower, List<IPgEffect> itemEffectList, out string[] stringKeyArray, out ModEffect[] ModEffectArray)
        {
            IList<IPgPowerTier> TierEffectList = itemPower.TierEffectList;

            Debug.Assert(TierEffectList.Count == itemEffectList.Count);
            Debug.Assert(TierEffectList.Count > 0);

            int ValidationIndex = 0;
            if (TierEffectList.Count >= 2)
                ValidationIndex = 1;
            if (TierEffectList.Count >= 4)
                ValidationIndex = 2;

            List<CombatKeyword>[] PowerTierKeywordListArray = new List<CombatKeyword>[TierEffectList.Count];
            List<CombatKeyword>[] EffectKeywordListArray = new List<CombatKeyword>[TierEffectList.Count];
            stringKeyArray = new string[TierEffectList.Count];
            ModEffectArray = new ModEffect[TierEffectList.Count];

            int LastTierIndex = TierEffectList.Count - 1;

            AnalyzeMatchingEffects(abilityNameList, nameToKeyword, TierEffectList[LastTierIndex], itemEffectList[LastTierIndex], out List<CombatKeyword> ExtractedPowerTierKeywordList, out List<CombatKeyword> ExtractedEffectKeywordList, out ModEffect ExtractedModEffect, true);
            PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
            EffectKeywordListArray[LastTierIndex] = ExtractedEffectKeywordList;
            ModEffectArray[LastTierIndex] = ExtractedModEffect;

            for (int i = 0; i + 1 < TierEffectList.Count; i++)
            {
                AnalyzeMatchingEffects(abilityNameList, nameToKeyword, TierEffectList[i], itemEffectList[i], out List<CombatKeyword> ComparedPowerTierKeywordList, out List<CombatKeyword> ComparedEffectKeywordList, out ModEffect ParsedModEffect, false);

                bool AllowIncomplete = i < ValidationIndex;

                if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
                {
                    Debug.WriteLine($"Mismatching power at tier #{i}");
                    return;
                }

                if (!IsSameCombatKeywordList(ExtractedEffectKeywordList, ComparedEffectKeywordList, AllowIncomplete))
                {
                    Debug.WriteLine($"Mismatching effect at tier #{i}");
                    return;
                }

                PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
                EffectKeywordListArray[i] = ComparedEffectKeywordList;
                ModEffectArray[i] = ParsedModEffect;
            }

            for (int i = 0; i < TierEffectList.Count; i++)
            {
                string Key = PowerEffectPairKey(itemPower, i);
                stringKeyArray[i] = Key;
            }
        }

        private bool IsSameCombatKeywordList(List<CombatKeyword> list1, List<CombatKeyword> list2, bool allowIncomplete)
        {
            if (list2.Count == list1.Count + 1 && list2[0] == CombatKeyword.ApplyWithChance)
                return true;

            if ((!allowIncomplete && list1.Count != list2.Count) || (allowIncomplete && list1.Count < list2.Count))
                return false;

            Debug.Assert(list1.Count >= list2.Count);

            List<CombatKeyword> MissingKeywordList = new List<CombatKeyword>();
            for (int i = 0; i < list2.Count; i++)
                if (!list1.Contains(list2[i]))
                    MissingKeywordList.Add(list2[i]);

            if (MissingKeywordList.Count == 0)
                return true;

            if (MissingKeywordList.Count == 1 && MissingKeywordList[0] == CombatKeyword.ApplyWithChance && list1.Contains(CombatKeyword.DamageBoost) && list1.Count == list2.Count)
                return true;

            return false;
        }

        private bool AnalyzeMatchingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPowerTier powerTier, IPgEffect effect, out List<CombatKeyword> extractedPowerTierKeywordList, out List<CombatKeyword> extractedEffectKeywordList, out ModEffect modEffect, bool displayAnalysisResult)
        {
            modEffect = null;

            IList<IPgPowerEffect> EffectList = powerTier.EffectList;
            Debug.Assert(EffectList.Count == 1);
            IPgPowerSimpleEffect powerSimpleEffect = (IPgPowerSimpleEffect)EffectList[0];

            string ModText = powerSimpleEffect.Description;
            string EffectText = effect.Desc;

            HackEffectText(ref ModText, ref EffectText);

            bool IsMod = false;
            if (EffectText == ModText)
                IsMod = true;

            AnalyzeText(abilityNameList, nameToKeyword, ModText, true, out List<AbilityKeyword> ModAbilityList, out List<CombatEffect> ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);
            AnalyzeText(abilityNameList, nameToKeyword, EffectText, IsMod, out List<AbilityKeyword> EffectAbilityList, out List<CombatEffect> EffectCombatList, out List<AbilityKeyword> EffectTargetAbilityList);

            string ParsedEffectString = CombatEffectListToString(EffectCombatList, out extractedEffectKeywordList);
            string ParsedEffectTargetAbilityList = AbilityKeywordListToShortString(EffectTargetAbilityList);
            string ParsedAbilityList = AbilityKeywordListToShortString(ModAbilityList);
            string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
            string ParsedModTargetAbilityList = AbilityKeywordListToShortString(ModTargetAbilityList);

            bool IsSameTarget = ModEffect.IsSameAbilityKeywordList(ModTargetAbilityList, EffectTargetAbilityList);
            if (!IsSameTarget)
            {
                Debug.WriteLine("");
                Debug.WriteLine("BAD TARGET!");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
                return false;
            }

            bool IsContained = CombatEffect.Contains(ModCombatList, EffectCombatList, out List<CombatEffect> StaticCombatEffectList, out List<CombatEffect> DynamicCombatEffectList);
            if (!IsContained)
            {
                Debug.WriteLine("");
                Debug.WriteLine("UNPARSED!");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
                return false;
            }

            if (displayAnalysisResult)
            {
                /*Debug.WriteLine("");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");*/
            }

            modEffect = new ModEffect(ModAbilityList, StaticCombatEffectList, DynamicCombatEffectList, ModTargetAbilityList);
            return true;
        }

        private void HackEffectText(ref string modText, ref string effectText)
        {
            if (modText == "Doe Eyes restores 3 power" && effectText == "Restores 3 Armor")
                effectText = "Restores 3 Power";
            else if (effectText.EndsWith("for 20 seconds or until you teleport"))
                effectText = effectText.Substring(0, effectText.Length - 22 - 15) + effectText.Substring(effectText.Length - 22);
            else if (effectText.EndsWith("for 20 seconds or until you Feint"))
                effectText = effectText.Substring(0, effectText.Length - 19 - 15) + effectText.Substring(effectText.Length - 19);
            else if (effectText.StartsWith("Increases target's Max Rage by") && effectText.EndsWith("for 60 seconds"))
                effectText = effectText.Substring(0, effectText.Length - 15);
            else if (effectText.StartsWith("Indirect Poison +") && effectText.EndsWith(" for 5 seconds"))
                effectText = effectText.Substring(0, effectText.Length - 14) + " per tick";
            else if (effectText.StartsWith("Buffs targets' direct") && effectText.EndsWith("for 30 seconds every 5 seconds"))
                effectText = effectText.Substring(0, effectText.Length - 15) + " (stacking up to 6 times)";
            else if (modText.EndsWith("% damage to health and armor") && effectText.EndsWith("% Armor damage"))
                effectText = effectText.Substring(0, effectText.Length - 14) + "% Health and Armor damage";
            else if (modText.StartsWith("Astral Strike's "))
                modText = "Astral Strike" + modText.Substring(15);
            else if (modText.StartsWith("Pixie Flare's "))
                modText = "Pixie Flare" + modText.Substring(13);
            else if (effectText.StartsWith("Kick damage ") && modText.Contains("all kicks"))
                effectText = "All kicks damage " + effectText.Substring(12);

            int IndexFound = 0;
            RemoveDecorativeText(ref modText, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
            RemoveDecorativeText(ref effectText, "have less than a third of their Armor", "have less than 33% of their Armor", out _, ref IndexFound);
            RemoveDecorativeText(ref modText, "you take half damage from", "you take 50% damage from", out _, ref IndexFound);

            int NegateIndex = modText.IndexOf(". (You can negate the latent psychic damage by using");
            if (NegateIndex >= 0)
                modText = modText.Substring(0, NegateIndex);

            if (effectText.StartsWith("DamageType:"))
            {
                int EndDamageNameIndex = effectText.IndexOf(";");
                if (EndDamageNameIndex > 11)
                {
                    string DamageTypeName = effectText.Substring(11, EndDamageNameIndex - 11);
                    effectText = $"Deals {DamageTypeName} damage and " + effectText.Substring(EndDamageNameIndex + 1).Trim();
                }
            }

            BasicTextReplace(ref modText, ref effectText, "Sic Em", "Sic 'Em");
            BasicTextReplace(ref modText, ref effectText, "physical (slashing, piercing, and crushing)", "Crushing, Slashing, or Piercing");
            BasicTextReplace(ref modText, ref effectText, "Animal Handling pets'", "Animal Handling pets uuuuuuuuuuuuuuuuuuuuunused");
            BasicTextReplace(ref modText, ref effectText, "damage-over-time effects (if any)", "Damage over Time");
            BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels Ice Armor", "Fire damage no longer dispels");
            BasicTextReplace(ref modText, ref effectText, "Fire damage no longer dispels your Ice Armor", "Fire damage no longer dispels");
        }

        private void BasicTextReplace(ref string modText, ref string effectText, string oldText, string newText)
        {
            modText = modText.Replace(oldText, newText);
            effectText = effectText.Replace(oldText, newText);
        }

        private string AbilityKeywordListToLongString(List<AbilityKeyword> list)
        {
            string Result = string.Empty;

            foreach (AbilityKeyword Keyword in list)
            {
                if (Result.Length > 0)
                    Result += ", ";

                Result += $"AbilityKeyword.{Keyword}";
            }

            if (Result.Length == 0)
                Result = "{ }";
            else
                Result = $"{{ {Result} }}";

            return Result;
        }

        private string AbilityKeywordListToShortString(List<AbilityKeyword> list)
        {
            string Result = string.Empty;

            foreach (AbilityKeyword Keyword in list)
            {
                if (Result.Length > 0)
                    Result += ", ";

                Result += Keyword.ToString();
            }

            return Result;
        }

        private string CombatEffectListToString(List<CombatEffect> list, out List<CombatKeyword> combatKeywordList)
        {
            string Result = string.Empty;
            combatKeywordList = new List<CombatKeyword>();

            foreach (CombatEffect Item in list)
            {
                if (Result.Length > 0)
                    Result += ", ";

                Result += Item.ToString();
                combatKeywordList.Add(Item.Keyword);
            }

            return Result;
        }

        private void AnalyzeText(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, string text, bool isMod, out List<AbilityKeyword> extractedAbilityList, out List<CombatEffect> extractedCombatEffectList, out List<AbilityKeyword> extractedTargetAbilityList)
        {
            RemoveDecorationText(ref text);
            SimplifyGrammar(ref text);
            SimplifyRandom(ref text);

            int RemoveCount;

            if (isMod)
                ExtractAbilityList(abilityNameList, nameToKeyword, true, ref text, out extractedAbilityList, out RemoveCount);
            else
            {
                extractedAbilityList = new List<AbilityKeyword>();
                RemoveCount = 0;
            }

            ExtractAbilityList(abilityNameList, nameToKeyword, false, ref text, out extractedTargetAbilityList, out int TargetRemoveCount);

            ExtractAttributesFull(text, extractedAbilityList, out extractedCombatEffectList);

            if (RemoveCount > extractedAbilityList.Count && extractedAbilityList.Count > 0 && extractedTargetAbilityList.Count == 0)
                if (extractedAbilityList[extractedAbilityList.Count - 1] != AbilityKeyword.Chill)
                    extractedTargetAbilityList.Add(extractedAbilityList[extractedAbilityList.Count - 1]);
        }

        private void RemoveDecorationText(ref string text)
        {
            int IndexFound = 0;
            RemoveDecorativeText(ref text, "(wax) ", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(such as spiders, mantises, and beetles)", out _, ref IndexFound);
            RemoveDecorativeText(ref text, ", requiring more Rage to use their Rage Abilities", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(if any)", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(Accuracy cancels out the Evasion that certain monsters have)", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(which cancels out the Evasion that certain monsters have)", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(which boosts XP earned and critical-hit chance)", out _, ref IndexFound);
            RemoveDecorativeText(ref text, "(ignores a fatal attack once; resets after 15 minutes)", out _, ref IndexFound);
            ReplaceCaseInsensitive(ref text, " (or armor if health is full)", "/Armor");
        }

        private void ExtractAbilityList(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, bool limitParsing, ref string text, out List<AbilityKeyword> extractedAbilityList, out int removeCount)
        {
            extractedAbilityList = new List<AbilityKeyword>();
            removeCount = 0;

            Dictionary<int, string> ExtractedTable = new Dictionary<int, string>();

            foreach (string AbilityName in abilityNameList)
                if (nameToKeyword.ContainsKey(AbilityName))
                {
                    string NewText = text;
                    int NewIndex = -1;

                    RemoveDecorativeText(ref NewText, AbilityName, out bool IsRemoved, ref NewIndex);

                    if (IsRemoved)
                    {
                        bool IsAlreadyExtracted = false;
                        foreach (KeyValuePair<int, string> ExtractedEntry in ExtractedTable)
                        {
                            if (ExtractedEntry.Key == NewIndex)
                                IsAlreadyExtracted = true;

                            int ExistingIndex = ExtractedEntry.Value.IndexOf(AbilityName);
                            if (ExistingIndex >= 0 && ExtractedEntry.Key + ExistingIndex == NewIndex)
                                IsAlreadyExtracted = true;
                        }

                        if (!IsAlreadyExtracted)
                            ExtractedTable.Add(NewIndex, AbilityName);
                    }
                }

            if (ExtractedTable.Count > 0)
            {
                int SeparatingIndex = text.IndexOf("boost your");
                if (SeparatingIndex < 0)
                    SeparatingIndex = text.Length;

                List<string> KeyList = new List<string>();
                int LastBestIndex = -1;
                bool IsFirstAbilityGeneric = false;

                while (ExtractedTable.Count > 0)
                {
                    int BestIndex = -1;
                    foreach (KeyValuePair<int, string> Entry in ExtractedTable)
                        if (BestIndex == -1 || BestIndex > Entry.Key)
                            BestIndex = Entry.Key;

                    string BestString = ExtractedTable[BestIndex];
                    Debug.Assert(nameToKeyword.ContainsKey(BestString));
                    List<AbilityKeyword> KeywordList = nameToKeyword[BestString];

                    bool IsAbilityGeneric = false;
                    foreach (AbilityKeyword Keyword in KeywordList)
                        IsAbilityGeneric |= GenericAbilityList.Contains(Keyword);

                    if (LastBestIndex == -1)
                        IsFirstAbilityGeneric = IsAbilityGeneric;
                    else
                    {
                        if (limitParsing && (BestIndex > LastBestIndex + 28 || BestIndex > SeparatingIndex))
                            break;
                        if (IsAbilityGeneric != IsFirstAbilityGeneric)
                            break;
                    }

                    KeyList.Add(BestString);

                    ExtractedTable.Remove(BestIndex);


                    LastBestIndex = BestIndex;
                }

                int UnusedIndex = -1;
                foreach (string Key in KeyList)
                {
                    bool IsRemoved;
                    do
                    {
                        RemoveDecorativeText(ref text, Key, "@", out IsRemoved, ref UnusedIndex);
                        if (IsRemoved)
                            removeCount++;
                    }
                    while(IsRemoved);

                    extractedAbilityList.AddRange(nameToKeyword[Key]);
                }
            }
        }

        private void SimplifyGrammar(ref string text)
        {
            ReplaceCaseInsensitive(ref text, "reduces ", "reduce ");
            ReplaceCaseInsensitive(ref text, "removes ", "remove ");
            ReplaceCaseInsensitive(ref text, "deals ", "deal ");
            ReplaceCaseInsensitive(ref text, "generates ", "generate ");
            ReplaceCaseInsensitive(ref text, "taunts ", "taunt ");
            ReplaceCaseInsensitive(ref text, "costs ", "cost ");
            ReplaceCaseInsensitive(ref text, "restores ", "restore ");
            ReplaceCaseInsensitive(ref text, "becomes ", "become ");
            ReplaceCaseInsensitive(ref text, "ignites ", "ignite ");
            ReplaceCaseInsensitive(ref text, "boosts ", "boost ");
            ReplaceCaseInsensitive(ref text, "heals ", "heal ");
            ReplaceCaseInsensitive(ref text, "lowers ", "lower ");
            ReplaceCaseInsensitive(ref text, "mitigates ", "mitigate ");
            ReplaceCaseInsensitive(ref text, "grants ", "grant ");
            ReplaceCaseInsensitive(ref text, "depletes ", "deplete ");
            ReplaceCaseInsensitive(ref text, "reaps ", "reap ");
            ReplaceCaseInsensitive(ref text, "steals ", "steal ");
            ReplaceCaseInsensitive(ref text, "hits ", "hit ");
            ReplaceCaseInsensitive(ref text, "stuns ", "stun ");
            ReplaceCaseInsensitive(ref text, "knocks back", "knock back");
            ReplaceCaseInsensitive(ref text, "knocked back", "knock back");
            ReplaceCaseInsensitive(ref text, "knocks ", "knock ");
            ReplaceCaseInsensitive(ref text, " backwards", " backward");
            ReplaceCaseInsensitive(ref text, "causes ", "cause ");
            ReplaceCaseInsensitive(ref text, "lowers ", "lower ");
            ReplaceCaseInsensitive(ref text, "shortens ", "shorten ");
            ReplaceCaseInsensitive(ref text, "hastens ", "hasten ");
            ReplaceCaseInsensitive(ref text, "raises ", "raise ");
            ReplaceCaseInsensitive(ref text, "increases ", "increase ");
            ReplaceCaseInsensitive(ref text, "gives ", "give ");
            ReplaceCaseInsensitive(ref text, "takes ", "take ");
            ReplaceCaseInsensitive(ref text, "bleeds ", "bleed ");
            ReplaceCaseInsensitive(ref text, "gains ", "gain ");
            ReplaceCaseInsensitive(ref text, "slows ", "slow ");
            ReplaceCaseInsensitive(ref text, "suffers ", "suffer ");
            ReplaceCaseInsensitive(ref text, "triggers ", "trigger ");
            ReplaceCaseInsensitive(ref text, "makes ", "make ");
            ReplaceCaseInsensitive(ref text, "debuffs ", "debuff ");
            ReplaceCaseInsensitive(ref text, "buffs ", "buff ");
            ReplaceCaseInsensitive(ref text, "repairs ", "repair ");
            ReplaceCaseInsensitive(ref text, "ignores ", "ignore ");
            ReplaceCaseInsensitive(ref text, "resets ", "reset ");
            ReplaceCaseInsensitive(ref text, " dispels", " dispel");
            ReplaceCaseInsensitive(ref text, "dispels ", "dispel ");
            ReplaceCaseInsensitive(ref text, " seconds", " second");
            ReplaceCaseInsensitive(ref text, "-second", " second");
            ReplaceCaseInsensitive(ref text, " minutes", " minute");
            ReplaceCaseInsensitive(ref text, " meters", " meter");
            ReplaceCaseInsensitive(ref text, " timer ", " time ");
            ReplaceCaseInsensitive(ref text, "haven't ", "have not ");
            ReplaceCaseInsensitive(ref text, " an additional ", " ");
            ReplaceCaseInsensitive(ref text, " additional ", " ");
            ReplaceCaseInsensitive(ref text, "attacks ", "attack ");
            ReplaceCaseInsensitive(ref text, "debuffs ", "debuff ");
            ReplaceCaseInsensitive(ref text, "spells ", "spell ");
            ReplaceCaseInsensitive(ref text, "mutations ", "mutation ");
            ReplaceCaseInsensitive(ref text, "out-of-combat", "out of combat");
            ReplaceCaseInsensitive(ref text, "vs. ", "vs ");
            ReplaceCaseInsensitive(ref text, " versus ", " vs ");
            ReplaceCaseInsensitive(ref text, " it's ", " it is ");
            ReplaceCaseInsensitive(ref text, "+Up ", "Add up ");
            ReplaceCaseInsensitive(ref text, " direct-damage ", " direct damage ");
            ReplaceCaseInsensitive(ref text, " abilities ", " ability ");
        }

        private void ReplaceCaseInsensitive(ref string text, string searchPattern, string replacementPattern)
        {
            string LowerText = text.ToLowerInvariant();
            int Index;

            while ((Index = LowerText.IndexOf(searchPattern)) >= 0)
            {
                text = text.Substring(0, Index) + replacementPattern + text.Substring(Index + searchPattern.Length);
                LowerText = text.ToLowerInvariant();
            }
        }

        private void SimplifyRandom(ref string text)
        {
            int Index = text.IndexOf(" between ");
            if (Index < 0)
                return;

            string Prolog = text.Substring(0, Index);

            string MinString = string.Empty;
            Index += 9;
            while (Index < text.Length && (char.IsDigit(text[Index]) || text[Index] == '+'))
                MinString += text[Index++];

            if (MinString.Length == 0)
                return;

            if (Index + 5 >= text.Length || text.Substring(Index, 5) != " and ")
                return;

            string MaxString = string.Empty;
            Index += 5;
            while (Index < text.Length && (char.IsDigit(text[Index]) || text[Index] == '+'))
                MaxString += text[Index++];

            if (MaxString.Length == 0)
                return;

            if (text[Index++] != ' ')
                return;

            string Epilog = text.Substring(Index);

            int Min = int.Parse(MinString);
            int Max = int.Parse(MaxString);
            int Interval = Min <= 1 ? Max : (Max - Min);

            string RandomlyDetermined = "(randomly determined)";
            text = $"{Prolog} Add up to {Interval} {Epilog}";

            if (!text.Contains(RandomlyDetermined))
                text += " " + RandomlyDetermined;
        }

        private bool RemoveWideAbilityReferences(ref string text, List<AbilityKeyword> modifiedAbilityKeywordList, string pattern, List<AbilityKeyword> abilityKeywordList, out int indexFound)
        {
            string PatternWithAbility = pattern + " ability";
            string InputText = text;
            indexFound = text.Length;

            RemoveDecorativeText(ref InputText, PatternWithAbility, "@", out bool IsRemovedAbility, ref indexFound);
            RemoveDecorativeText(ref InputText, pattern, "@", out bool IsRemoved, ref indexFound);

            if (IsRemovedAbility || IsRemoved)
            {
                if (modifiedAbilityKeywordList.Count > 0)
                {
                    string KeywordListString = string.Empty;

                    foreach (AbilityKeyword Keyword in modifiedAbilityKeywordList)
                    {
                        if (KeywordListString.Length > 0)
                            KeywordListString += ", ";

                        KeywordListString += Keyword.ToString();
                    }

                    Debug.WriteLine($"Ability already removed, for keywords {KeywordListString}");
                }
                else
                {
                    text = InputText;
                    modifiedAbilityKeywordList.AddRange(abilityKeywordList);
                    return true;
                }
            }

            return false;
        }

        private void RemoveDecorativeText(ref string text, string pattern, out bool isRemoved, ref int indexFound)
        {
            RemoveDecorativeText(ref text, pattern, string.Empty, out isRemoved, ref indexFound);
        }

        private void RemoveDecorativeText(ref string text, string pattern, string replacement, out bool isRemoved, ref int indexFound)
        {
            isRemoved = false;

            string LowerText = text.ToLowerInvariant();
            string LowerPattern = pattern.ToLowerInvariant();
            int Index = LowerText.IndexOf(LowerPattern);

            if (Index >= 0 && IsStartingSentenceIndex(LowerText, Index) && IsEndingSentenceIndex(LowerText, Index + LowerPattern.Length))
            {
                string Prolog = text.Substring(0, Index).TrimEnd();
                string Epilog = text.Substring(Index + pattern.Length).TrimStart();

                if (Prolog.Length > 0 && Epilog.Length > 0)
                    if (replacement.Length > 0)
                        text = Prolog + " " + replacement + " " + Epilog;
                    else
                        text = Prolog + " " + Epilog;
                else if (Prolog.Length > 0)
                    if (replacement.Length > 0)
                        text = Prolog + " " + replacement;
                    else
                        text = Prolog;
                else
                    if (replacement.Length > 0)
                    text = replacement + " " + Epilog;
                else
                    text = Epilog;

                indexFound = Index;
                isRemoved = true;
            }
        }

        private bool IsStartingSentenceIndex(string text, int index)
        {
            return index == 0 || text[index - 1] == ' ' || text[index - 1] == ',';
        }

        private bool IsEndingSentenceIndex(string text, int index)
        {
            return index + 1 >= text.Length || text[index] == ' ' || text[index] == ',' || text[index] == '.' || (index + 1 < text.Length && text[index] == '\'' && text[index + 1] == 's');
        }

        private void ExtractAttributesFull(string text, List<AbilityKeyword> extractedAbilityList, out List<CombatEffect> extractedCombatEffectList)
        {
            List<CombatKeyword> SkippedKeywordList = new List<CombatKeyword>();
            ExtractAttributes(text, SkippedKeywordList, extractedAbilityList, out extractedCombatEffectList);
        }

        private void ExtractAttributes(string text, List<CombatKeyword> skippedKeywordList, List<AbilityKeyword> extractedAbilityList, out List<CombatEffect> extractedCombatEffectList)
        {
            extractedCombatEffectList = new List<CombatEffect>();
            List<CombatKeyword> SkippedKeywordListCopy = new List<CombatKeyword>(skippedKeywordList);

            bool HasUntiltrigger = false;
            bool HasDuration = false;

            bool IsAttributeExtracted;
            do
            {
                IsAttributeExtracted = ExtractKnownAttribute(SkippedKeywordListCopy, ref text, out List<CombatEffect> CombatEffectList);

                if (IsAttributeExtracted)
                {
                    extractedCombatEffectList.AddRange(CombatEffectList);
                    foreach (CombatEffect Item in CombatEffectList)
                    {
                        SkippedKeywordListCopy.Add(Item.Keyword);

                        if (Item.Keyword == CombatKeyword.UntilTrigger)
                            HasUntiltrigger = true;
                        if (Item.Keyword == CombatKeyword.EffectDuration)
                            HasDuration = true;
                    }
                }
            }
            while (IsAttributeExtracted);

            // Hack for ShadowFeint.
            if (extractedAbilityList.Count == 1 && extractedAbilityList[0] == AbilityKeyword.ShadowFeint && HasUntiltrigger && !HasDuration)
                extractedCombatEffectList.Add(new CombatEffect(CombatKeyword.EffectDuration, NumericValue.FromDouble(20)));

            // Hack for Aimed Shot.
            if (extractedAbilityList.Count == 1 && extractedAbilityList[0] == AbilityKeyword.AimedShot && extractedCombatEffectList.Count > 0 && extractedCombatEffectList[0].Keyword == CombatKeyword.DealDirectHealthDamage)
                extractedCombatEffectList[0] = new CombatEffect(CombatKeyword.DamageBoost, extractedCombatEffectList[0].Data, new NumericValue(), GameDamageType.Trauma, GameCombatSkill.None);

            for (int i = 0; i < extractedCombatEffectList.Count; i++)
            {
                CombatEffect CombatEffect = extractedCombatEffectList[i];
                if (CombatEffect.Keyword == CombatKeyword.AddMitigationPhysical && CombatEffect.DamageType == GameDamageType.None)
                {
                    extractedCombatEffectList[i] = new CombatEffect(CombatKeyword.AddMitigation, CombatEffect.Data1, CombatEffect.Data2, GameDamageType.Crushing | GameDamageType.Slashing | GameDamageType.Piercing, CombatEffect.CombatSkill);
                }
            }
        }

        private bool ExtractKnownAttribute(List<CombatKeyword> skippedKeywordList, ref string text, out List<CombatEffect> extractedCombatEffectList)
        {
            List<CombatKeyword> ExtractedKeywordList = new List<CombatKeyword>();
            NumericValue Data1 = new NumericValue();
            NumericValue Data2 = new NumericValue();
            GameDamageType DamageType = GameDamageType.None;
            GameCombatSkill CombatSkill = GameCombatSkill.None;
            int ParsedIndex = -1;
            string ModifiedText = text;
            Sentence SelectedSentence = null;

            foreach (Sentence Item in SentenceList)
                ExtractSentence(Item, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex, ref SelectedSentence);

            extractedCombatEffectList = new List<CombatEffect>();

            if (ExtractedKeywordList.Count > 0)
            {
                text = ModifiedText;

                foreach (CombatKeyword Item in ExtractedKeywordList)
                {
                    if (Item == CombatKeyword.Ignore)
                        continue;

                    CombatEffect ExtractedCombatEffect;
                    if (extractedCombatEffectList.Count == 0)
                        ExtractedCombatEffect = new CombatEffect(Item, Data1, Data2, DamageType, CombatSkill);
                    else
                        ExtractedCombatEffect = new CombatEffect(Item);

                    extractedCombatEffectList.Add(ExtractedCombatEffect);
                }

                Debug.Assert(SelectedSentence != null);
                SelectedSentence.SetUsed();

                return true;
            }
            else
                return false;
        }

        private void ExtractSentence(Sentence sentence, List<CombatKeyword> skippedKeywordList, string text, ref string modifiedText, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex, ref Sentence selectedSentence)
        {
            string NewText = text;
            List<CombatKeyword> NewExtractedKeywordList = new List<CombatKeyword>();
            NumericValue NewData1 = new NumericValue();
            NumericValue NewData2 = new NumericValue();
            GameDamageType NewDamageType = GameDamageType.None;
            GameCombatSkill NewCombatSkill = GameCombatSkill.None;
            int NewParsedIndex = -1;

            bool IsExtracted = ExtractNewSentence(sentence, skippedKeywordList, ref NewText, NewExtractedKeywordList, ref NewData1, ref NewData2, ref NewDamageType, ref NewCombatSkill, ref NewParsedIndex);

            if (!IsExtracted)
                return;

            if (parsedIndex < 0 || NewParsedIndex + 1 < parsedIndex)
            {
                modifiedText = NewText;
                extractedKeywordList.Clear();
                extractedKeywordList.AddRange(NewExtractedKeywordList);
                data1 = NewData1;
                data2 = NewData2;
                damageType = NewDamageType;
                combatSkill = NewCombatSkill;
                parsedIndex = NewParsedIndex;
                selectedSentence = sentence;
            }
        }

        private bool ExtractNewSentence(Sentence sentence, List<CombatKeyword> skippedKeywordList, ref string text, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
        {
            string format = sentence.Format;
            List<CombatKeyword> associatedKeywordList = sentence.AssociatedKeywordList;
            SignInterpretation signInterpretation = sentence.SignInterpretation;

            string LowerText = text.ToLowerInvariant();
            string LowerFormat = format.ToLowerInvariant();

            int Index = LowerFormat.IndexOf('%');

            if (Index >= 0)
            {
                char FormatType = LowerFormat[Index + 1];

                if (FormatType != 'f')
                {
                    Debug.WriteLine($"Format \"{format}\" contains % but for unsupported type {FormatType}");
                    return false;
                }

                string BeforePattern = LowerFormat.Substring(0, Index);
                string AfterPattern = LowerFormat.Substring(Index + 2);

                int StartIndex = -1;
                bool ContinueParsing;
                do
                {
                    StartIndex++;
                    ContinueParsing = ParseFormat(StartIndex, BeforePattern, AfterPattern, LowerText, associatedKeywordList, signInterpretation, ref text, extractedKeywordList, ref data1, ref data2, ref damageType, ref combatSkill, out int ParsedLength);
                    StartIndex += ParsedLength;
                }
                while (ContinueParsing);

                if (extractedKeywordList.Count > 0)
                {
                    parsedIndex = StartIndex;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                int PatternIndex = FindPattern(LowerText, LowerFormat, 0, out GameDamageType ParsedDamageType, out GameCombatSkill ParsedCombatSkill, out int FormatLength);
                if (PatternIndex < 0)
                    return false;

                text = text.Substring(0, PatternIndex) + text.Substring(PatternIndex + FormatLength).Trim();
                extractedKeywordList.AddRange(associatedKeywordList);
                damageType = ParsedDamageType;
                combatSkill = ParsedCombatSkill;

                parsedIndex = PatternIndex;
                return true;
            }
        }

        private bool ParseFormat(int startIndex, string beforePattern, string afterPattern, string lowerText, List<CombatKeyword> associatedKeywordList, SignInterpretation signInterpretation, ref string text, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, out int parsedLength)
        {
            int PatternIndex;
            int AfterPatternIndex;
            GameDamageType DamageTypeBefore;
            GameCombatSkill CombatSkillBefore;
            int PatternLengthBefore;
            GameDamageType DamageTypeAfter;
            GameCombatSkill CombatSkillAfter;
            int PatternLengthAfter;

            if (beforePattern.Length > 0)
            {
                PatternIndex = FindPattern(lowerText, beforePattern, startIndex, out DamageTypeBefore, out CombatSkillBefore, out PatternLengthBefore);
                DamageTypeAfter = GameDamageType.None;
                CombatSkillAfter = GameCombatSkill.None;
                PatternLengthAfter = 0;
            }
            else
            {
                DamageTypeBefore = GameDamageType.None;
                CombatSkillBefore = GameCombatSkill.None;
                PatternLengthBefore = 0;

                AfterPatternIndex = FindPattern(lowerText, afterPattern, startIndex, out DamageTypeAfter, out CombatSkillAfter, out PatternLengthAfter);
                if (AfterPatternIndex > 0)
                {
                    PatternIndex = NumericValue.BackwardIndex(lowerText, AfterPatternIndex);
                    if (PatternIndex >= 0)
                        startIndex -= (AfterPatternIndex - PatternIndex);
                }
                else
                    PatternIndex = -1;
            }

            if (PatternIndex < 0)
            {
                parsedLength = 0;
                return false;
            }

            int StartDataIndex = PatternIndex + PatternLengthBefore;
            int EndDataIndex = StartDataIndex;

            if (EndDataIndex + 1 < lowerText.Length && (lowerText[EndDataIndex] == '+' || lowerText[EndDataIndex] == '-') && char.IsDigit(lowerText[EndDataIndex + 1]))
                EndDataIndex++;

            while (EndDataIndex < lowerText.Length && (char.IsDigit(lowerText[EndDataIndex]) || (lowerText[EndDataIndex] == '.' && EndDataIndex > StartDataIndex)))
                EndDataIndex++;

            if (EndDataIndex < lowerText.Length && lowerText[EndDataIndex] == '%')
                EndDataIndex++;

            parsedLength = PatternIndex - startIndex + 1;

            if (EndDataIndex <= StartDataIndex)
                return true;

            NumericValue Data = NumericValue.Parse(lowerText.Substring(StartDataIndex, EndDataIndex - StartDataIndex));

            AfterPatternIndex = FindPattern(lowerText, afterPattern, EndDataIndex, out DamageTypeAfter, out CombatSkillAfter, out PatternLengthAfter);
            if (AfterPatternIndex != EndDataIndex)
                return true;

            Debug.Assert(DamageTypeBefore == GameDamageType.None || DamageTypeAfter == GameDamageType.None);
            Debug.Assert(CombatSkillBefore == GameCombatSkill.None || CombatSkillAfter == GameCombatSkill.None);

            data1 = Data;

            switch (signInterpretation)
            {
                case SignInterpretation.Normal:
                    break;
                case SignInterpretation.Opposite:
                    data1.ChangeSign();
                    break;
                case SignInterpretation.AlwaysNegative:
                    if (data1.Value > 0)
                        data1.ChangeSign();
                    break;
            }

            text = text.Substring(0, PatternIndex) + text.Substring(EndDataIndex + PatternLengthAfter).Trim();
            extractedKeywordList.AddRange(associatedKeywordList);

            if (DamageTypeBefore != GameDamageType.None)
                damageType = DamageTypeBefore;
            else if (DamageTypeAfter != GameDamageType.None)
                damageType = DamageTypeAfter;

            if (CombatSkillBefore != GameCombatSkill.None)
                combatSkill = CombatSkillBefore;
            else if (CombatSkillAfter != GameCombatSkill.None)
                combatSkill = CombatSkillAfter;

            return false;
        }

        private int FindPattern(string text, string pattern, int startIndex, out GameDamageType damageType, out GameCombatSkill combatSkill, out int patternLength)
        {
            damageType = GameDamageType.None;
            combatSkill = GameCombatSkill.None;

            int Value;
            int Index;

            Debug.Assert(typeof(GameDamageType).GetEnumNames().Length == DamageTypeTextMap.Count);

            Index = FindPattern("#D", DamageTypeTextMap, text, pattern, startIndex, out Value, out patternLength);
            if (Index >= 0)
            {
                damageType = (GameDamageType)Value;
                return Index;
            }

            Debug.Assert(typeof(GameCombatSkill).GetEnumNames().Length == SkillTextMap.Count);

            Index = FindPattern("#S", SkillTextMap, text, pattern, startIndex, out Value, out patternLength);
            if (Index >= 0)
            {
                combatSkill = (GameCombatSkill)Value;
                return Index;
            }

            return -1;
        }

        private int FindPattern(string patternString, Dictionary<int, string> textMap, string text, string pattern, int startIndex, out int value, out int patternLength)
        {
            int DamageTypeIndex = pattern.IndexOf(patternString.ToLowerInvariant());

            if (DamageTypeIndex < 0)
            {
                value = 0;
                patternLength = pattern.Length;
                return text.IndexOf(pattern, startIndex);
            }
            else
            {
                string BeforeTypePattern = pattern.Substring(0, DamageTypeIndex);
                string AfterTypePattern = pattern.Substring(DamageTypeIndex + patternString.Length);

                int LowestIndex = -1;
                int LowestValue = 0;
                int LowestLength = -1;

                Dictionary<int, string> FoundTextMap = new Dictionary<int, string>();

                bool IsAddedToFoundTextMap;
                int BestFoundIndex = -1;

                do
                {
                    IsAddedToFoundTextMap = false;
                    int BestKey = -1;
                    int BestKeyIndex = text.Length;

                    foreach (KeyValuePair<int, string> Entry in textMap)
                    {
                        if (Entry.Key == 0)
                            continue;

                        if (FoundTextMap.ContainsKey(Entry.Key))
                            continue;

                        string ValueText = Entry.Value.ToLowerInvariant();
                        int FoundIndex = text.IndexOf(ValueText);

                        if (FoundIndex >= 0)
                            if (BestKeyIndex > FoundIndex)
                                if (BestFoundIndex == -1 || FoundIndex < BestFoundIndex + 17)
                                {
                                    BestKeyIndex = FoundIndex;
                                    BestKey = Entry.Key;
                                }
                    }

                    if (BestKey != -1)
                    {
                        FoundTextMap.Add(BestKey, textMap[BestKey].ToLowerInvariant());
                        IsAddedToFoundTextMap = true;
                        BestFoundIndex = BestKeyIndex;
                    }
                }
                while (IsAddedToFoundTextMap);

                List<int> FoundTextKey = new List<int>(FoundTextMap.Keys);
                Dictionary<string, int> PossibleTextMap = new Dictionary<string, int>();
                int Value;

                switch (FoundTextMap.Count)
                {
                    case 3:
                        Value = 0;
                        for (int i = 0; i < 3; i++)
                            Value |= FoundTextKey[i];

                        for (int i = 0; i < 3; i++)
                            for (int j = 0; j < 3; j++)
                                if (j != i)
                                    for (int k = 0; k < 3; k++)
                                        if (k != i && k != j)
                                        {
                                            string KeyAnd = $"{FoundTextMap[FoundTextKey[i]]}, {FoundTextMap[FoundTextKey[j]]}, and {FoundTextMap[FoundTextKey[k]]}";
                                            PossibleTextMap.Add(KeyAnd, Value);
                                            string KeyOr = $"{FoundTextMap[FoundTextKey[i]]}, {FoundTextMap[FoundTextKey[j]]}, or {FoundTextMap[FoundTextKey[k]]}";
                                            PossibleTextMap.Add(KeyOr, Value);
                                            string KeySlash = $"{FoundTextMap[FoundTextKey[i]]}/{FoundTextMap[FoundTextKey[j]]}/{FoundTextMap[FoundTextKey[k]]}";
                                            PossibleTextMap.Add(KeySlash, Value);
                                        }
                        break;
                    case 2:
                        Value = 0;
                        for (int i = 0; i < 2; i++)
                            Value |= FoundTextKey[i];

                        for (int i = 0; i < 2; i++)
                            for (int j = 0; j < 2; j++)
                                if (j != i)
                                    {
                                        string KeyAnd = $"{FoundTextMap[FoundTextKey[i]]} and {FoundTextMap[FoundTextKey[j]]}";
                                        PossibleTextMap.Add(KeyAnd, Value);
                                        string KeyOr = $"{FoundTextMap[FoundTextKey[i]]} or {FoundTextMap[FoundTextKey[j]]}";
                                        PossibleTextMap.Add(KeyOr, Value);
                                    }
                        break;

                    case 0:
                        break;

                    default:
                    case 1:
                        Value = FoundTextKey[0];
                        PossibleTextMap.Add(FoundTextMap[FoundTextKey[0]], Value);
                        break;
                }

                foreach (KeyValuePair<string, int> Entry in PossibleTextMap)
                {
                    string ExtendedPattern = $"{BeforeTypePattern}{Entry.Key}{AfterTypePattern}";
                    int Index = text.IndexOf(ExtendedPattern, startIndex);
                    if (Index == -1)
                        continue;

                    if (LowestIndex == -1 || LowestIndex > Index)
                    {
                        LowestIndex = Index;
                        LowestValue = Entry.Value;
                        LowestLength = ExtendedPattern.Length;
                    }
                }

                if (LowestIndex >= 0)
                {
                    Debug.Assert(LowestValue > 0);
                    Debug.Assert(LowestLength > 0);

                    value = LowestValue;
                    patternLength = LowestLength;

                    return LowestIndex;
                }
                else
                {
                    value = 0;
                    patternLength = pattern.Length;
                    return -1;
                }
            }
        }
        #endregion

        #region Data Analysis, Remaining
        const int AnalyzedVersionRemaining = 333;

        private void AnalyzeRemainingEffects(int version, List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, List<IPgPower> powerSimpleEffectList)
        {
            if (version <= AnalyzedVersionRemaining)
                return;

            int DebugIndex = 0;
            int SkipIndex = 101;
            List<string[]> StringKeyTable = new List<string[]>();
            List<ModEffect[]> LocalPowerKeyToCompleteEffectTable = new List<ModEffect[]>();

            foreach (IPgPower ItemPower in powerSimpleEffectList)
            {
                DebugIndex++;

                if (SkipIndex > 0)
                {
                    SkipIndex--;
                    continue;
                }

                Debug.WriteLine("");
                Debug.WriteLine($"Debug Index: {DebugIndex - 1} / {powerSimpleEffectList.Count} (Remaining)");

                AnalyzeRemainingEffects(abilityNameList, nameToKeyword, ItemPower, out string[] stringKeyArray, out ModEffect[] ModEffectArray);

                StringKeyTable.Add(stringKeyArray);
                LocalPowerKeyToCompleteEffectTable.Add(ModEffectArray);
            }
        }

        private void AnalyzeRemainingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPower itemPower, out string[] stringKeyArray, out ModEffect[] ModEffectArray)
        {
            IList<IPgPowerTier> TierEffectList = itemPower.TierEffectList;

            Debug.Assert(TierEffectList.Count > 0);

            int ValidationIndex = 0;
            if (TierEffectList.Count >= 2)
                ValidationIndex = 1;
            if (TierEffectList.Count >= 4)
                ValidationIndex = 2;

            List<CombatKeyword>[] PowerTierKeywordListArray = new List<CombatKeyword>[TierEffectList.Count];
            stringKeyArray = new string[TierEffectList.Count];
            ModEffectArray = new ModEffect[TierEffectList.Count];

            int LastTierIndex = TierEffectList.Count - 1;

            AnalyzeRemainingEffects(abilityNameList, nameToKeyword, TierEffectList[LastTierIndex], out List<CombatKeyword> ExtractedPowerTierKeywordList, out ModEffect ExtractedModEffect, true);
            PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
            ModEffectArray[LastTierIndex] = ExtractedModEffect;

            for (int i = 0; i + 1 < TierEffectList.Count; i++)
            {
                AnalyzeRemainingEffects(abilityNameList, nameToKeyword, TierEffectList[i], out List<CombatKeyword> ComparedPowerTierKeywordList, out ModEffect ParsedModEffect, false);

                bool AllowIncomplete = i < ValidationIndex;

                if (!IsSameCombatKeywordList(ExtractedPowerTierKeywordList, ComparedPowerTierKeywordList, AllowIncomplete))
                {
                    Debug.WriteLine($"Mismatching power at tier #{i}");
                    return;
                }

                PowerTierKeywordListArray[i] = ComparedPowerTierKeywordList;
                ModEffectArray[i] = ParsedModEffect;
            }

            for (int i = 0; i < TierEffectList.Count; i++)
            {
                string Key = PowerEffectPairKey(itemPower, i);
                stringKeyArray[i] = Key;
            }
        }

        private bool AnalyzeRemainingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPowerTier powerTier, out List<CombatKeyword> extractedPowerTierKeywordList, out ModEffect modEffect, bool displayAnalysisResult)
        {
            modEffect = null;

            IList<IPgPowerEffect> EffectList = powerTier.EffectList;
            Debug.Assert(EffectList.Count == 1);
            IPgPowerSimpleEffect powerSimpleEffect = (IPgPowerSimpleEffect)EffectList[0];

            string ModText = powerSimpleEffect.Description;

            //HackEffectText(ref ModText, ref EffectText);

            AnalyzeText(abilityNameList, nameToKeyword, ModText, true, out List<AbilityKeyword> ModAbilityList, out List<CombatEffect> ModCombatList, out List<AbilityKeyword> ModTargetAbilityList);

            string ParsedAbilityList = AbilityKeywordListToShortString(ModAbilityList);
            string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
            string ParsedModTargetAbilityList = AbilityKeywordListToShortString(ModTargetAbilityList);

            if (displayAnalysisResult)
            {
                Debug.WriteLine("");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            }

            modEffect = new ModEffect(ModAbilityList, new List<CombatEffect>(), ModCombatList, ModTargetAbilityList);
            return true;
        }
        #endregion

        #region Tables
        public static readonly Dictionary<int, string> DamageTypeTextMap = new Dictionary<int, string>()
        {
            { (int)GameDamageType.None, "None" },
            { (int)GameDamageType.Crushing, "Crushing" },
            { (int)GameDamageType.Slashing, "Slashing" },
            { (int)GameDamageType.Nature, "Nature" },
            { (int)GameDamageType.Fire, "Fire" },
            { (int)GameDamageType.Cold, "Cold" },
            { (int)GameDamageType.Piercing, "Piercing" },
            { (int)GameDamageType.Psychic, "Psychic" },
            { (int)GameDamageType.Trauma, "Trauma" },
            { (int)GameDamageType.Electricity, "Electricity" },
            { (int)GameDamageType.Poison, "Poison" },
            { (int)GameDamageType.Acid, "Acid" },
            { (int)GameDamageType.Darkness, "Darkness" },
        };

        public static readonly Dictionary<int, string> SkillTextMap = new Dictionary<int, string>()
        {
            { (int)GameCombatSkill.None, "None" },
            { (int)GameCombatSkill.Sword, "Sword" },
            { (int)GameCombatSkill.FireMagic, "Fire Magic" },
            { (int)GameCombatSkill.Unarmed, "Unarmed" },
            { (int)GameCombatSkill.Psychology, "Psychology" },
            { (int)GameCombatSkill.Staff, "Staff" },
            { (int)GameCombatSkill.Mentalism, "Mentalism" },
            { (int)GameCombatSkill.Archery, "Archery" },
            { (int)GameCombatSkill.Shield, "Shield" },
            { (int)GameCombatSkill.AnimalHandling, "Animal Handling" },
            { (int)GameCombatSkill.Knife, "Knife" },
            { (int)GameCombatSkill.Cow, "Cow" },
            { (int)GameCombatSkill.Deer, "Deer" },
            { (int)GameCombatSkill.Pig, "Pig" },
            { (int)GameCombatSkill.Spider, "Spider" },
            { (int)GameCombatSkill.Werewolf, "Werewolf" },
            { (int)GameCombatSkill.BattleChemistry, "Battle Chemistry" },
            { (int)GameCombatSkill.Necromancy, "Necromancy" },
            { (int)GameCombatSkill.Hammer, "Hammer" },
            { (int)GameCombatSkill.Druid, "Druid" },
            { (int)GameCombatSkill.IceMagic, "Ice Magic" },
            { (int)GameCombatSkill.GiantBat, "Giant Bat" },
            { (int)GameCombatSkill.Axe, "Axe" },
            { (int)GameCombatSkill.Bard, "Bard" },
            { (int)GameCombatSkill.Rabbit, "Rabbit" },
            { (int)GameCombatSkill.Priest, "Priest" },
            { (int)GameCombatSkill.Warden, "Warden" },
            { (int)GameCombatSkill.FairyMagic, "Fairy Magic" },
            { (int)GameCombatSkill.Lycanthropy, "Lycanthropy" },
        };

        private static List<Sentence> SentenceList = new List<Sentence>()
        {
            new Sentence("Place an extra trap", CombatKeyword.AnotherTrap),
            new Sentence("Target's Critical Hit Chance reduced by %f", CombatKeyword.ReduceCriticalChance),
            new Sentence("Chance to critically-hit is reduced by %f", CombatKeyword.ReduceCriticalChance),
            new Sentence("Mend a broken bone", CombatKeyword.MendBrokenBone),
            new Sentence("Turn while leaping", CombatKeyword.FreeMovementLeaping),
            new Sentence("Free-form movement while leaping", CombatKeyword.FreeMovementLeaping),
            new Sentence("Repeated castings on same target", CombatKeyword.RepeatedCasting),
            new Sentence("Exhilarates on the same target", CombatKeyword.RepeatedCasting),
            new Sentence("Boost Jump Height", CombatKeyword.BoostJumpHeight),
            new Sentence("Shuffling their hatred", CombatKeyword.ShuffleTaunt),
            new Sentence("Target ignore you", CombatKeyword.Ignored),
            new Sentence("Cause the target to ignore you", CombatKeyword.Ignored),
            new Sentence("Uniformly diminishes all targets' entire aggro lists by %f", CombatKeyword.ChangeTaunt),
            new Sentence("This absorbed damage is added to your next @ attack at a %f rate", CombatKeyword.ReturnDamage),
            new Sentence("This absorbed damage is added to your next @", CombatKeyword.ReturnDamage),
            new Sentence("Boost your next attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
            new Sentence("Future @ attack damage %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
            new Sentence("Boost the damage of future @ attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
            new Sentence("Until %f damage is mitigated", CombatKeyword.MitigationLimit),
            new Sentence("Up to a maximum of %f total mitigated damage", CombatKeyword.MitigationLimit),
            new Sentence("(Randomly determined for each attack)", CombatKeyword.RandomDamage),
            new Sentence("(Randomly determined)", CombatKeyword.RandomDamage),
            new Sentence("(Random)", CombatKeyword.RandomDamage),
            new Sentence("If it is a #D attack", CombatKeyword.IfDamageType),
            //new Sentence("If it's a #D attack", CombatKeyword.IfDamageType),
            new Sentence("Briefly terrifies the target", CombatKeyword.Fear),
            new Sentence("Cause all sentient targets to flee in terror", CombatKeyword.FearSentient),
            new Sentence("Trigger the target's Vulnerability", CombatKeyword.SetVulnerable),
            new Sentence("To Arthropods", CombatKeyword.TargetAnatomyArthropods),
            new Sentence("To Undead", CombatKeyword.TargetUndead),
            new Sentence("On an undead target", CombatKeyword.TargetUndead),
            new Sentence("Vs Undead", CombatKeyword.TargetUndead),
            new Sentence("To Aberration", CombatKeyword.TargetAnatomyAbberation),
            new Sentence("Vs Elite", CombatKeyword.TargetElite),
            new Sentence("To non-Elite target", CombatKeyword.TargetNotElite),
            new Sentence("To non-Elite enemies", CombatKeyword.TargetNotElite),
            new Sentence("To Vulnerable target", CombatKeyword.TargetVulnerable),
            new Sentence("To sentient creatures", CombatKeyword.TargetSentient),
            new Sentence("If the target is Vulnerable", CombatKeyword.TargetVulnerable),
            new Sentence("If target is Vulnerable", CombatKeyword.TargetVulnerable),
            new Sentence("Is used on a Vulnerable target", CombatKeyword.TargetVulnerable),
            new Sentence("Both you and your pet", CombatKeyword.ApplyToPetAndMaster),
            new Sentence("Your pet's", CombatKeyword.ApplyToPet),
            new Sentence("Your pet gain", CombatKeyword.ApplyToPet),
            new Sentence("Give your pet", CombatKeyword.ApplyToPet),
            new Sentence("Grant your pet", CombatKeyword.ApplyToPet),
            new Sentence("To your pet", CombatKeyword.ApplyToPet),
            new Sentence("To your undead", CombatKeyword.ApplyToPet),
            new Sentence("To the same target", CombatKeyword.SameTarget),
            new Sentence("To you and your allies", CombatKeyword.ApplyToAllies),
            new Sentence("To you and nearby allies", CombatKeyword.ApplyToAllies),
            new Sentence("To YOU", CombatKeyword.TargetSelf),
            new Sentence("If target is covered", CombatKeyword.TargetUnderEffect),
            new Sentence("To targets that are covered", CombatKeyword.TargetUnderEffect),
            new Sentence("To targets that are Knocked Down", CombatKeyword.TargetKnockedDown),
            new Sentence("Deal #D damage", CombatKeyword.ChangeDamageType),
            new Sentence("Instead of Crushing", CombatKeyword.Ignore),
            new Sentence("Next attack", CombatKeyword.NextAttack),
            new Sentence("Next ability", CombatKeyword.NextAttack),
            new Sentence("For one attack", CombatKeyword.NextAttack),
            new Sentence("Target's next Rage Attack", CombatKeyword.TargetNextRageAttack),
            new Sentence("The next time they use a Rage attack", CombatKeyword.TargetNextRageAttack),
            new Sentence("%f of target's attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
            new Sentence("%f of their attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
            new Sentence("Target's attack", CombatKeyword.TargetSubsequentAttacks),
            new Sentence("Ignore Knockback effect", CombatKeyword.IgnoreKnockback),
            new Sentence("Immunity to Knockback", CombatKeyword.IgnoreKnockback),
            new Sentence("Grant all targets immunity to Knockback", CombatKeyword.IgnoreKnockback),
            new Sentence("Grant Knockback Immunity", CombatKeyword.IgnoreKnockback),
            new Sentence("Immune to Knockback effects", CombatKeyword.IgnoreKnockback),
            new Sentence("Target take %f indirect #D damage", CombatKeyword.AddIndirectVulnerability),
            new Sentence("Cause the target to take %f damage from indirect #D", CombatKeyword.AddIndirectVulnerability),
            new Sentence("Target is %f more vulnerable to #D damage", CombatKeyword.AddVulnerability),
            new Sentence("Target %f more vulnerable to #D damage", CombatKeyword.AddVulnerability),
            new Sentence("Make the target %f more vulnerable to #D", CombatKeyword.AddVulnerability),
            new Sentence("Cause the target to become %f more vulnerable to #D attack", CombatKeyword.AddVulnerability),
            new Sentence("Increase the damage target take from #D by %f", CombatKeyword.AddVulnerability),
            new Sentence("Target take %f more damage from #D", CombatKeyword.AddVulnerability),
            new Sentence("Take %f direct #D damage", CombatKeyword.AddDirectVulnerability),
            new Sentence("Take %f damage from #D attack", CombatKeyword.AddVulnerability),
            new Sentence("#D Vulnerability %f", CombatKeyword.AddVulnerability),
            new Sentence("%f more damage from any #D attack", CombatKeyword.AddVulnerability),
            new Sentence("Take %f damage from both #D", CombatKeyword.AddVulnerability),
            //new Sentence("%f damage from #D", CombatKeyword.AddVulnerability),
            new Sentence("But take %f damage from any #D attack", CombatKeyword.AddVulnerability),
            new Sentence("#D Vulnerability +infinity", CombatKeyword.DestroyedByDamageType),
            new Sentence("Instantly destroyed by ANY #D Damage", CombatKeyword.DestroyedByDamageType),
            new Sentence("Targets suffer %f damage from other #D attack", CombatKeyword.AddVulnerability),
            new Sentence("Deal %f damage to Health and Armor", CombatKeyword.DamageBoostToHealthAndArmor),
            new Sentence("Increase the damage of all targets' attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }),
            new Sentence("%f damage from all attack", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }),
            new Sentence("Turning half of that into Trauma damage", CombatKeyword.ExtraTraumaDamage),
            //new Sentence("Pet's #D attack deal %f damage", CombatKeyword.AnimalPetAttackBoost),
            //new Sentence("Pet's #D attack %f damage", CombatKeyword.AnimalPetAttackBoost),
            new Sentence("#D attack Deal %f damage", CombatKeyword.DamageBoost),
            new Sentence("Rage attack deal %f damage", CombatKeyword.AnimalPetRageAttackBoost),
            new Sentence("Pet's Rage Attack Damage %f", CombatKeyword.AnimalPetRageAttackBoost),
            new Sentence("Deal up to %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Add up to %f damage", CombatKeyword.DamageBoost),
            new Sentence("Add up to %f extra damage", CombatKeyword.DamageBoost),
            new Sentence("Deal %f Armor damage", CombatKeyword.DealArmorDamage),
            new Sentence("Deal %f immediate #D damage", CombatKeyword.DamageBoost),
            new Sentence("Deal %f #D damage", CombatKeyword.DamageBoost),
            new Sentence("Deal %f damage", CombatKeyword.DamageBoost),
            new Sentence("Plus %f more damage", CombatKeyword.DamageBoost),
            //new Sentence("Deal %f fire damage to you", CombatKeyword.SelfImmolation),
            new Sentence("Pet take %f #D damage", CombatKeyword.PetImmolation),
            new Sentence("Pet bleed for %f #D damage", CombatKeyword.PetImmolation),
            new Sentence("Cause your pet to bleed for %f #D damage", CombatKeyword.PetImmolation),
            new Sentence("(Debuff cannot stack with itself)", CombatKeyword.NonStackingDebuff),
            new Sentence("(This effect does not stack with itself.)", CombatKeyword.NonStackingDebuff),
            new Sentence("(This buff does not stack with itself)", CombatKeyword.NonStackingDebuff),
            new Sentence("(Stacking up to %f times)", CombatKeyword.StackingDebuffLimit),
            new Sentence("(This effect does not stack with itself)", CombatKeyword.NonStackingDebuff),
            //new Sentence("Combo: Deer Bash+Any Melee+Any Melee+Deer Kick:", CombatKeyword.Combo1),
            //new Sentence("Combo: Gripjaw+Any Spider+Any Spider+Inject Venom:", CombatKeyword.Combo2),
            //new Sentence("Combo: Rip+Any Melee+Any Giant Bat Attack+Tear:", CombatKeyword.Combo3),
            //new Sentence("Combo: Screech+Any Giant Bat Attack+Any Melee+Virulent Bite:", CombatKeyword.Combo4),
            //new Sentence("Combo: Rip+Any Melee+Any Melee+Bat Stability:", CombatKeyword.Combo5),
            //new Sentence("Combo: Sonic Burst+Any Giant Bat Attack+Any Ranged Attack+Any Ranged Attack:", CombatKeyword.Combo6),
            new Sentence("Combo: Suppress+Any Melee+Any Melee+Headcracker:", CombatKeyword.Combo7),
            //new Sentence("Final step hit all enemies within %f meter", CombatKeyword.ComboFinalStepBurst),
            //new Sentence("Final step hit all targets within %f meter", CombatKeyword.ComboFinalStepBurst),
            //new Sentence("Final step deal %f damage", CombatKeyword.ComboFinalStepDamage),
            new Sentence("Final step stun the target while dealing %f damage", CombatKeyword.ComboFinalStepDamageAndStun),
            //new Sentence("Final step boost base damage %f for 10 second", CombatKeyword.ComboFinalStepBoostBaseDamage),
            //new Sentence("Whenever you take damage from an enemy", CombatKeyword.ReflectOnAnyAttack),
            //new Sentence("Each time they attack and damage you", CombatKeyword.ReflectOnAnyAttack),
            //new Sentence("If you are using the #S skill", CombatKeyword.ActiveSkill),
            //new Sentence("While #S skill active", CombatKeyword.ActiveSkill),
            new Sentence("While #S skill is active", CombatKeyword.ActiveSkill),
            new Sentence("(If #S skill is active)", CombatKeyword.ActiveSkill),
            new Sentence("Gain %f #S Skill Base Damage", CombatKeyword.BaseDamageBoost),
            //new Sentence("You have not been attacked in the past %f second", CombatKeyword.NotAttackedRecently),
            new Sentence("If you have less than half of your Health remaining", CombatKeyword.LessThanHalfMaxHealth),
            //new Sentence("Incubated Spiders %f chance to avoid being hit burst attack", CombatKeyword.SpiderPetAvoidBurst),
            //new Sentence("Combat Refresh restore %f health", CombatKeyword.CombatRefreshRestoreHeatlth),
            new Sentence("Healing from Combat Refreshes %f", CombatKeyword.CombatRefreshRestoreHeatlth),
            new Sentence("Boost the target's #D damage-over-time by %f per tick", CombatKeyword.DealIndirectDamage),
            //new Sentence("If , , or  deal damage, that damage is boosted %f per tick", CombatKeyword.DamageBoost),
            new Sentence("Take %f damage from #D", CombatKeyword.AddVulnerability),
            //new Sentence("Boost the damage of your Core and @ %f", CombatKeyword.DamageBoost),
            new Sentence("Boost your #S damage %f", CombatKeyword.DamageBoost),
            new Sentence("Boost your @ damage %f", CombatKeyword.DamageBoost),
            new Sentence("Boost your #D attack damage %f", CombatKeyword.DamageBoost),
            new Sentence("Boost #D attack damage %f", CombatKeyword.DamageBoost),
            new Sentence("Boost #D attack %f", CombatKeyword.DamageBoost),
            new Sentence("Boost the damage of your @ by %f", CombatKeyword.DamageBoost),
            new Sentence("Boost the damage of your @ %f", CombatKeyword.DamageBoost),
            new Sentence("Boost the damage from @ %f", CombatKeyword.DamageBoost),
            new Sentence("Boost the damage of all your attack %f", CombatKeyword.DamageBoost),
            new Sentence("Boost damage from @ %f", CombatKeyword.DamageBoost),
            //new Sentence("Boost the damage from all kicks %f", CombatKeyword.DamageBoost),
            //new Sentence("Boost your direct and indirect damage %f", CombatKeyword.DamageBoost),
            new Sentence("Boost #D damage %f", CombatKeyword.DamageBoost),
            //new Sentence("Increase the damage of your ranged attack by %f", CombatKeyword.DamageBoost),
            new Sentence("Increase the damage of your next attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }),
            new Sentence("Increase the damage of your @ by %f", CombatKeyword.DamageBoost),
            //new Sentence("Plus %f Damage", CombatKeyword.DamageBoost),
            new Sentence("#S Skill Base Damage %f", CombatKeyword.BaseDamageBoost),
            new Sentence("#S Base Damage by %f", CombatKeyword.BaseDamageBoost),
            new Sentence("#S Base Damage %f", CombatKeyword.BaseDamageBoost),
            new Sentence("Your #S Base Damage is %f", CombatKeyword.BaseDamageBoost),
            new Sentence("Your #S Base Damage increase %f", CombatKeyword.BaseDamageBoost),
            //new Sentence("Base Damage %f", CombatKeyword.BaseDamageBoost),
            //new Sentence("Base Damage by %f", CombatKeyword.BaseDamageBoost),
            //new Sentence("Base Damage increase %f", CombatKeyword.BaseDamageBoost),
            //new Sentence("Causing %f Damage", CombatKeyword.DamageBoost),
            //new Sentence("Boost the target's fire damage-over-time by %f", CombatKeyword.DamageBoost),
            new Sentence("Direct #D Damage %f", CombatKeyword.DamageBoost),
            new Sentence("Universal Indirect Damage %f", CombatKeyword.DealIndirectDamage),
            new Sentence("Boost targets' indirect damage %f", CombatKeyword.DealIndirectDamage),
            new Sentence("Indirect #D Damage %f", CombatKeyword.DealIndirectDamage),
            new Sentence("Indirect #D %f per tick", CombatKeyword.DealIndirectDamage),
            //new Sentence("Damage is %f per tick", CombatKeyword.DamageBoost),
            new Sentence("Damage over Time %f per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }),
            new Sentence("Damage over Time deal %f damage per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }),
            //new Sentence("Damage over Time deal %f per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }),
            //new Sentence("Base Damage is %f", CombatKeyword.BaseDamageBoost),
            //new Sentence("Damage is %f", CombatKeyword.DamageBoost),
            new Sentence("Damage is boosted %f", CombatKeyword.DamageBoost),
            //new Sentence("Suffer %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Reap %f of the damage to you as healing", CombatKeyword.DrainHealth),
            //new Sentence("Reap %f of the damage done", CombatKeyword.DrainHealth),
            new Sentence("Reap %f health", CombatKeyword.DrainHealth),
            //new Sentence("Melee Attackers suffer %f indirect damage", CombatKeyword.ReflectMeleeIndirectDamage),
            //new Sentence("Up to a max of %f", CombatKeyword.MaxOccurence),
            //new Sentence("The reap cap is %f", CombatKeyword.DrainMax),
            //new Sentence("Deal %f damage", CombatKeyword.DamageBoost),
            new Sentence("Deal %f direct damage", CombatKeyword.DamageBoost),
            //new Sentence("Deal %f damage to Health", CombatKeyword.DealDirectHealthDamage),
            new Sentence("%f direct health damage", CombatKeyword.DealDirectHealthDamage),
            new Sentence("+Up to %f extra damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }),
            new Sentence("%f random damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }),
            new Sentence("All attack deal %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Nice attack deal %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Core attack deal %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Deal %f indirect damage", CombatKeyword.DealIndirectDamage),
            new Sentence("Dealing %f damage", CombatKeyword.DamageBoost),
            new Sentence("Cause %f damage", CombatKeyword.DamageBoost),
            //new Sentence("Take %f damage", CombatKeyword.DamageBoost),
            new Sentence("Over %f second", CombatKeyword.EffectDuration),
            new Sentence("Lasts %f second", CombatKeyword.EffectDuration),
            new Sentence("For %f second after using ", CombatKeyword.EffectDuration),
            new Sentence("For %f second", CombatKeyword.EffectDuration),
            //new Sentence("Within %f second", CombatKeyword.EffectDuration),
            new Sentence("(%f second)", CombatKeyword.EffectDuration),
            new Sentence("For %f minute", CombatKeyword.EffectDurationMinute),
            new Sentence("After a %f second delay", CombatKeyword.EffectDelay),
            new Sentence("After an %f second delay", CombatKeyword.EffectDelay),
            new Sentence("After %f second", CombatKeyword.EffectDelay),
            new Sentence("Every %f second", CombatKeyword.EffectRecurrence),
            new Sentence("Every other second", CombatKeyword.EffectRecurrence),
            new Sentence("With each heal", CombatKeyword.EffectRecurrence),
            new Sentence("Remove (up to) %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            new Sentence("Reduce Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Reduce Rage %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Reduce %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            new Sentence("Reduce the target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
            new Sentence("Reduce target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
            new Sentence("reduce targets' Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
            new Sentence("Generate %f Rage", CombatKeyword.AddRage),
            //new Sentence("Generate %f less Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Lower Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Lower Rage %f", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Remove %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Lose %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            //new Sentence("Deplete %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite),
            new Sentence("Generate no Rage", CombatKeyword.ZeroRage),
            new Sentence("Raise the target's Max Rage by %f", CombatKeyword.IncreaseMaxRage),
            new Sentence("Raise target's Max Rage by %f", CombatKeyword.IncreaseMaxRage),
            new Sentence("Increase target's Max Rage by%f", CombatKeyword.IncreaseMaxRage),
            //new Sentence("Generate no Taunt", CombatKeyword.ZeroTaunt),
            new Sentence("Power Cost %f", CombatKeyword.AddPowerCost),
            new Sentence("Power Cost is %f", CombatKeyword.AddPowerCost),
            new Sentence("Reduce the Power cost of your @ %f", CombatKeyword.AddPowerCost),
            new Sentence("Reduce Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }),
            new Sentence("Reduce the Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }),
            new Sentence("Reduce the Power cost of @ %f", CombatKeyword.AddPowerCost),
            new Sentence("In-Combat Armor Regeneration %f", CombatKeyword.AddArmorRegen),
            new Sentence("Armor Regeneration (in-combat) %f", CombatKeyword.AddArmorRegen),
            new Sentence("%f Armor Regeneration", CombatKeyword.AddArmorRegen),
            new Sentence("Recover %f Armor every five second", CombatKeyword.AddArmorRegen),
            new Sentence("Recover %f Armor", CombatKeyword.AddArmor),
            //new Sentence("Power Regeneration is %f", CombatKeyword.AddPowerRegen),
            new Sentence("Cost %f Power", CombatKeyword.AddPowerCost),
            new Sentence("Regain %f Power", CombatKeyword.AddPowerRegen),
            //new Sentence("The maximum Power restored  increase %f", CombatKeyword.AddPowerCostMax),
            new Sentence("Max Armor %f", CombatKeyword.AddMaxArmor),
            new Sentence("Gain %f Armor", CombatKeyword.AddMaxArmor),
            new Sentence("Increase your Max Health by %f", CombatKeyword.AddMaxHealth),
            new Sentence("Increase your Max Armor by %f", CombatKeyword.AddMaxArmor),
            //new Sentence("Reuse Time %f second", CombatKeyword.AddReuseTimer),
            new Sentence("Reuse Time is %f second", CombatKeyword.AddReuseTimer),
            new Sentence("Reuse Time is %f sec", CombatKeyword.AddReuseTimer),
            //new Sentence("Reuse Time is %f", CombatKeyword.AddReuseTimer),
            new Sentence("Hasten current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Hasten the current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Hasten the current reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Hasten the remaining reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Hasten your current Combat Refresh delay by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Hasten your current Combat Refresh time by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Shorten the remaining reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Shorten the current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative),
            new Sentence("Reset time of @ is increased %f second", CombatKeyword.AddCombatRefreshTimer),
            new Sentence("Reduce the taunt of all your attack by %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
            new Sentence("Taunt %f", CombatKeyword.AddTaunt),
            new Sentence("Taunted %f", CombatKeyword.AddTaunt),
            new Sentence("Taunt as if they did %f more damage", CombatKeyword.AddTaunt),
            new Sentence("Taunt as if they did %f damage", CombatKeyword.AddTaunt),
            new Sentence("Taunt their opponents %f less", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
            new Sentence("Taunt of all your attack %f", CombatKeyword.AddTaunt),
            new Sentence("%f Taunt", CombatKeyword.AddTaunt),
            new Sentence("When you have %f or less of your Armor left", CombatKeyword.BelowArmor),
            new Sentence("Have less than %f of their Armor", CombatKeyword.BelowArmor),
            //new Sentence("Restore %f Health, Armor, and Power respectively", CombatKeyword.RestoreHealthArmorPower),
            //new Sentence("Restore %f Health, Armor, and Power", CombatKeyword.RestoreHealthArmorPower),
            new Sentence("%f Health/Armor healing", CombatKeyword.RestoreHealthArmor),
            new Sentence("Heal you for %f Health/Armor", new List<CombatKeyword>() { CombatKeyword.RestoreHealthArmor, CombatKeyword.TargetSelf }),
            new Sentence("Heal you for %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }),
            new Sentence("Heal your pet for %f Health/Armor", CombatKeyword.RestoreHealthArmorToPet),
            new Sentence("You regain %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }),
            new Sentence("Restore %f Health/Armor to your pet", CombatKeyword.RestoreHealthArmorToPet),
            new Sentence("Restore %f Health/Armor", CombatKeyword.RestoreHealthArmor),
            new Sentence("Restore %f of your Max Health", CombatKeyword.RestoreMaxHealth),
            new Sentence("Restore %f health", CombatKeyword.RestoreHealth),
            //new Sentence("Healing Abilities %f", CombatKeyword.RestoreHealth),
            new Sentence("Boost the healing of your @ %f", CombatKeyword.RestoreHealth),
            new Sentence("Heal you for %f of your Max Health", CombatKeyword.RestoreMaxHealth),
            new Sentence("Heal all targets for %f health", CombatKeyword.RestoreHealth),
            //new Sentence("You regain %f health", CombatKeyword.RestoreHealth),
            new Sentence("Heal %f armor", CombatKeyword.RestoreArmor),
            new Sentence("Restore %f armor", CombatKeyword.RestoreArmor),
            //new Sentence("You regain %f armor", CombatKeyword.RestoreArmor),
            //new Sentence("Basic attack restore %f Power", CombatKeyword.RestorePower),
            new Sentence("Restore %f Power", CombatKeyword.RestorePower),
            //new Sentence("Restore %f to you", CombatKeyword.RestoreAny),
            //new Sentence("Recover %f armor", CombatKeyword.RestoreArmor),
            new Sentence("Recover %f health", CombatKeyword.RestoreHealth),
            new Sentence("Recover %f power", CombatKeyword.RestorePower),
            new Sentence("Restoration %f", CombatKeyword.RestoreHealth),
            new Sentence("You regain %f power", CombatKeyword.RestorePower),
            //new Sentence("Cost no Power to cast", CombatKeyword.ZeroPowerCost),
            //new Sentence("Take %f second to channel", CombatKeyword.AddChannelTime),
            new Sentence("Boost the healing from your @ %f", CombatKeyword.TargetAbilityBoost),
            //new Sentence("Heal you for %f health", CombatKeyword.RestoreHealth),
            new Sentence("Heal you for %f armor", CombatKeyword.RestoreArmor),
            new Sentence("Heal %f health", CombatKeyword.RestoreHealth),
            new Sentence("Healing %f", CombatKeyword.RestoreHealth),
            new Sentence("Heal you %f", CombatKeyword.RestoreHealth),
            new Sentence("Heal %f", CombatKeyword.RestoreHealth),
            new Sentence("Restore %f Body Heat", CombatKeyword.RestoreBodyHeat),
            //new Sentence("Sprint Speed is %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Sprint Speed increase by %f", CombatKeyword.AddSprintSpeed),
            new Sentence("%f Sprint Speed", CombatKeyword.AddSprintSpeed),
            new Sentence("Max Health %f", CombatKeyword.AddMaxHealth),
            new Sentence("Max Health by %f", CombatKeyword.AddMaxHealth),
            new Sentence("%f Max Health", CombatKeyword.AddMaxHealth),
            //new Sentence("Max Armor %f", CombatKeyword.AddMaxArmor),
            new Sentence("%f Max Armor", CombatKeyword.AddMaxArmor),
            new Sentence("Have %f Armor", CombatKeyword.AddMaxArmor),
            //new Sentence("To your minions", CombatKeyword.ApplyToNecroPet),
            new Sentence("Attack Range is %f", CombatKeyword.AddRange),
            //new Sentence("Range is %f meter", CombatKeyword.AddRange),
            new Sentence("Stun you", CombatKeyword.SelfStun),
            new Sentence("Complete stun immunity", CombatKeyword.StunImmunity),
            new Sentence("Grant immunity to new stun", CombatKeyword.StunImmunity),
            new Sentence("Grant them immunity to stun", CombatKeyword.StunImmunity),
            new Sentence("Grant immunity to new slow and root", CombatKeyword.SlowRootImmunity),
            new Sentence("Grant them immunity to Slow and Root", CombatKeyword.SlowRootImmunity),
            new Sentence("Dispel stun", CombatKeyword.RemoveStun),
            new Sentence("Dispel any Stun", CombatKeyword.RemoveStun),
            new Sentence("Dispel slow and root", CombatKeyword.RemoveSlowRoot),
            new Sentence("Dispel any Slow or Root", CombatKeyword.RemoveSlowRoot),
            new Sentence("Target is prone to random self-stuns", CombatKeyword.Concussion),
            new Sentence("Stun targets", CombatKeyword.Stun),
            //new Sentence("Stun incorporeal enemies", CombatKeyword.StunIncorporeal),
            new Sentence("Stun", CombatKeyword.Stun),
            new Sentence("Targets are Knock back", CombatKeyword.Knockback),
            new Sentence("Knock back targets", CombatKeyword.Knockback),
            new Sentence("Knock all targets back", CombatKeyword.Knockback),
            new Sentence("Knock the target backward", CombatKeyword.Knockback),
            new Sentence("Knock the enemy backward", CombatKeyword.Knockback),
            new Sentence("Knock the target back", CombatKeyword.Knockback),
            new Sentence("Knock target backward", CombatKeyword.Knockback),
            new Sentence("Knock targets backward", CombatKeyword.Knockback),
            new Sentence("Knock them backward", CombatKeyword.Knockback),
            new Sentence("Reset the time on", CombatKeyword.ResetOtherAbilityTimer),
            //new Sentence("Deal %f total damage against Demons", CombatKeyword.DamageBoostAgainstSpecie),
            new Sentence("Boost targets' mitigation %f", CombatKeyword.AddMitigation),
            new Sentence("#D mitigation %f", CombatKeyword.AddMitigation),
            new Sentence("Grant %f Universal #D Mitigation", CombatKeyword.AddMitigation),
            new Sentence("Universal Damage Mitigation %f", CombatKeyword.AddMitigation),
            new Sentence("Reduce the damage you take from #D attack by %f", CombatKeyword.AddMitigation),
            new Sentence("Reduce the damage of the next attack that hit the target by %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.NextAttack }),
            new Sentence("Take %f less damage from all attack", CombatKeyword.AddMitigation),
            new Sentence("Target take %f less damage from attack", CombatKeyword.AddMitigation),
            new Sentence("Target take %f less damage from #D attack", CombatKeyword.AddMitigation),
            new Sentence("Target to take %f less damage from attack", CombatKeyword.AddMitigation),
            new Sentence("Target to take %f less damage from #D attack", CombatKeyword.AddMitigation),
            //new Sentence("Mitigate %f of all damage", CombatKeyword.AddMitigation),
            //new Sentence("Grant your pet %f mitigation versus direct attack", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.ApplyToPet }),
            //new Sentence("Your pet gain %f mitigation versus direct attack", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.ApplyToPet }),
            new Sentence("Up to %f direct damage mitigation", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }),
            new Sentence("#D Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
            new Sentence("Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
            new Sentence("Mitigation vs all attack by Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }),
            new Sentence("Mitigation vs physical damage %f", CombatKeyword.AddMitigationPhysical),
            new Sentence("Physical Damage Mitigation %f", CombatKeyword.AddMitigationPhysical),
            new Sentence("%f absorption of any physical damage", CombatKeyword.AddMitigationPhysical),
            new Sentence("Any internal (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal),
            new Sentence("Any physical (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal),
            new Sentence("Universal Indirect Mitigation %f", CombatKeyword.AddMitigationIndirect),
            new Sentence("Mitigate all damage over time by %f per tick", CombatKeyword.AddMitigationIndirect),
            new Sentence("when armor is empty, up to %f when armor is full", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }),
            new Sentence("Stacks up to %f times", CombatKeyword.MaxStack),
            //new Sentence("Stacks up to %fx", CombatKeyword.MaxStack),
            //new Sentence("All Shield ability", CombatKeyword.ApplyToAbilitiesShield),
            //new Sentence("Grant allies", CombatKeyword.ApplyToAllies),
            new Sentence("To all allies", CombatKeyword.ApplyToAllies),
            new Sentence("And your allies' attack", CombatKeyword.ApplyToAllies),
            //new Sentence("%f evasion of burst attack", CombatKeyword.AddEvasionBurst),
            new Sentence("Burst Evasion %f", CombatKeyword.AddEvasionBurst),
            new Sentence("%f Burst Evasion", CombatKeyword.AddEvasionBurst),
            new Sentence("Boost Burst Evasion by %f", CombatKeyword.AddEvasionBurst),
            new Sentence("Projectile Evasion %f", CombatKeyword.AddEvasionProjectile),
            new Sentence("%f Projectile Evasion", CombatKeyword.AddEvasionProjectile),
            new Sentence("Melee Evasion %f", CombatKeyword.AddEvasionMelee),
            new Sentence("%f Melee Evasion", CombatKeyword.AddEvasionMelee),
            //new Sentence("%f mitigation of all physical attack", CombatKeyword.AddMitigationPhysical),
            new Sentence("%f mitigation of any physical damage", CombatKeyword.AddMitigationPhysical),
            new Sentence("%f mitigation against physical attack", CombatKeyword.AddMitigationPhysical),
            new Sentence("%f mitigation from direct attack", CombatKeyword.AddMitigationDirect),
            new Sentence("%f mitigation vs direct attack", CombatKeyword.AddMitigationDirect),
            new Sentence("%f mitigation against all attack", CombatKeyword.AddMitigation),
            new Sentence("%f Mitigation from all attack", CombatKeyword.AddMitigation),
            new Sentence("%f Mitigation from attack", CombatKeyword.AddMitigation),
            new Sentence("%f mitigation vs #D", CombatKeyword.AddMitigation),
            new Sentence("Increase your Mitigation vs #D attack %f", CombatKeyword.AddMitigation),
            new Sentence("%f mitigation from #D attack", CombatKeyword.AddMitigation),
            new Sentence("Direct #D mitigation %f", CombatKeyword.AddMitigation),
            new Sentence("%f direct damage mitigation", CombatKeyword.AddMitigationDirect),
            new Sentence("Boost your direct damage mitigation %f", CombatKeyword.AddMitigationDirect),
            new Sentence("Cause all targets to suffer %f damage from direct #D attack", CombatKeyword.AddMitigation, SignInterpretation.Opposite),
            new Sentence("You take %f damage from #D attack", CombatKeyword.AddMitigation),
            new Sentence("%f #D mitigation", CombatKeyword.AddMitigation),
            new Sentence("Increase your #D Mitigation %f", CombatKeyword.AddMitigation),
            new Sentence("Debuff their mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.AlwaysNegative),
            new Sentence("%f Damage Mitigation", CombatKeyword.AddMitigation),
            new Sentence("%f Direct Mitigation", CombatKeyword.AddMitigationDirect),
            new Sentence("Mitigate %f of all Slashing/Crushing/Piercing damage", CombatKeyword.AddMitigationPhysical),
            new Sentence("Mitigate %f of all physical damage", CombatKeyword.AddMitigationPhysical),
            new Sentence("%f Cold Protection (Direct and Indirect)", CombatKeyword.AddProtectionCold),
            new Sentence("%f Direct and Indirect Cold Protection", CombatKeyword.AddProtectionCold),
            new Sentence("Remove ongoing #D effects (up to %f dmg/sec)", CombatKeyword.RemoveEffects),
            //new Sentence("Chance to Ignore Knockbacks %f", CombatKeyword.AddChanceToIgnoreKnockback),
            new Sentence("%f chance to ignore Stun", CombatKeyword.AddChanceToIgnoreStun),
            new Sentence("Chance to ignore Stun %f", CombatKeyword.AddChanceToIgnoreStun),
            //new Sentence("Targets whose Rage meter are at least %f full", CombatKeyword.AboveRage),
            //new Sentence("Targets whose Rage meter is at least %f full", CombatKeyword.AboveRage),
            new Sentence("If target's Rage is at least %f full", CombatKeyword.AboveRage),
            new Sentence("If target's Rage meter is at least %f full", CombatKeyword.AboveRage),
            //new Sentence("%f chance to Knock Down", CombatKeyword.AddChanceToKnockdown),
            new Sentence("There's a %f chance", CombatKeyword.ApplyWithChance),
            new Sentence("%f chance to", CombatKeyword.ApplyWithChance),
            //new Sentence("When wielding two knives", CombatKeyword.RequireTwoKnives),
            new Sentence("If the target is not focused on you", CombatKeyword.RequireNoAggro),
            new Sentence("If they are not focused on you", CombatKeyword.RequireNoAggro),
            //new Sentence("If target is not focused on you", CombatKeyword.RequireNoAggro),
            //new Sentence("To all melee attackers", CombatKeyword.ApplyToMeleeReflect),
            //new Sentence("The first melee attacker is knocked away", CombatKeyword.ReflectKnockbackOnFirstMelee),
            //new Sentence("When a melee attack deal damage to you", CombatKeyword.ReflectOnMelee),
            //new Sentence("Deal its damage when you are hit burst attack", CombatKeyword.ReflectOnBurst),
            //new Sentence("Deal its damage when you are hit ranged attack", CombatKeyword.ReflectOnRanged),
            //new Sentence("A melee attack deal damage to you", CombatKeyword.ReflectOnMelee),
            //new Sentence("In addition, you can use the ability  %f", CombatKeyword.EnableOtherAbility),
            //new Sentence("In addition, you can use the ability", CombatKeyword.EnableOtherAbility),
            //new Sentence("Chance to consume grass is %f", CombatKeyword.ChanceToConsume),
            //new Sentence("You regenerate %f Health per tick (every 5 second, in and out of combat)", CombatKeyword.AddHealthRegen),
            //new Sentence("Summoned Deer attack", CombatKeyword.ApplyToDeerPet),
            //new Sentence("Summoned Deer", CombatKeyword.ApplyToDeerPet),
            new Sentence("Have %f health", CombatKeyword.AddMaxHealth),
            //new Sentence("Have %f armor", CombatKeyword.AddMaxArmor),
            //new Sentence("Incubated Spiders", CombatKeyword.ApplyToSpiderPet),
            new Sentence("Per second", CombatKeyword.Recurring),
            new Sentence("Steal %f health", CombatKeyword.DrainHealth),
            //new Sentence("Steal %f more health", CombatKeyword.DrainHealth),
            //new Sentence("Ability's range is reduced to %fm", CombatKeyword.AddRange, SignInterpretation.Opposite),
            //new Sentence("Chance to consume carrot is %f", CombatKeyword.ChanceToConsume),
            //new Sentence("Lower aggro toward you %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite),
            //new Sentence("Attack range %f meter", CombatKeyword.AddRange),
            new Sentence("Until you trigger the teleport", CombatKeyword.UntilTrigger),
            new Sentence("Until you Feint", CombatKeyword.UntilTrigger),
            new Sentence("Boost your movement speed by %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Boost movement speed by %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Increase your movement speed by %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Out of Combat Sprint Speed %f", CombatKeyword.AddOutOfCombatSpeed),
            new Sentence("%f Out of Combat Sprint Speed", CombatKeyword.AddOutOfCombatSpeed),
            new Sentence("Your Out of Combat Sprint speed %f", CombatKeyword.AddOutOfCombatSpeed),
            new Sentence("Your Out of Combat Sprint speed by %f", CombatKeyword.AddOutOfCombatSpeed),
            new Sentence("Speed is %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Movement speed %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Sprint speed %f", CombatKeyword.AddSprintSpeed),
            new Sentence("Sprint speed by %f", CombatKeyword.AddSprintSpeed),
            new Sentence("%f Movement Speed", CombatKeyword.AddSprintSpeed),
            //new Sentence("%f Sprint Speed", CombatKeyword.AddSprintSpeed),
            new Sentence("Fly speed is boosted %f", CombatKeyword.AddFlySpeed),
            new Sentence("Fly Speed %f", CombatKeyword.AddFlySpeed),
            new Sentence("Swim Speed %f", CombatKeyword.AddSwimSpeed),
            new Sentence("Slow target's movement by %f", CombatKeyword.Slow),
            new Sentence("Slow target's movement speed by %f", CombatKeyword.Slow),
            new Sentence("Give you %f Accuracy", CombatKeyword.AddAccuracy),
            new Sentence("Melee Accuracy %f", CombatKeyword.AddMeleeAccuracy),
            new Sentence("Accuracy %f", CombatKeyword.AddAccuracy),
            new Sentence("%f Accuracy", CombatKeyword.AddAccuracy),
            //new Sentence("%f projectile evasion", CombatKeyword.AddProjectileEvasion),
            //new Sentence("%f melee evasion", CombatKeyword.AddMeleeEvasion),
            new Sentence("Boost melee evasion %f", CombatKeyword.AddMeleeEvasion),
            new Sentence("Lower targets' Evasion by %f", CombatKeyword.RemoveEvasion),
            new Sentence("%f more chance of missing", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
            new Sentence("%f Miss Chance", CombatKeyword.AddAccuracy, SignInterpretation.Opposite),
            new Sentence("%f Physical Damage Reflection", CombatKeyword.AddPhysicalReflection),
            new Sentence("%f resistance to #D damage", CombatKeyword.AddDamageResistance),
            new Sentence("#D Resistance %f", CombatKeyword.AddDamageResistance),
            new Sentence("You are %f resistant to #D damage", CombatKeyword.AddDamageResistance),
            new Sentence("Within %f meter", CombatKeyword.WithinDistance),
            new Sentence("%f resistance to Elemental damage (Fire, Cold, Electricity)", CombatKeyword.AddElementalDamageResistance),
            new Sentence("Worth %f more XP", CombatKeyword.IncreaseXPGain),
            new Sentence("%f Earned Combat XP", CombatKeyword.IncreaseXPGain),
            new Sentence("Slain within %f second", CombatKeyword.MaxKillTime),
            new Sentence("Buff targets' direct #D damage %f", CombatKeyword.DamageBoost),
            new Sentence("%f Direct Damage", CombatKeyword.DamageBoost),
            new Sentence("%f #D damage", CombatKeyword.DamageBoost),
            new Sentence("#D damage %f", CombatKeyword.DamageBoost),
            new Sentence("%f health damage", CombatKeyword.DealDirectHealthDamage),
            new Sentence("%f #D health damage", CombatKeyword.DealDirectHealthDamage),
            //new Sentence("%f Direct #D Damage", CombatKeyword.DamageBoost),
            new Sentence("%f Health and Armor damage", CombatKeyword.DamageBoostToHealthAndArmor),
            new Sentence("%f damage", CombatKeyword.DamageBoost),
            new Sentence("Damage %f", CombatKeyword.DamageBoost),
            new Sentence("%f armor damage", CombatKeyword.DealArmorDamage),
            new Sentence("%f @", CombatKeyword.TargetAbilityBoost),
            new Sentence("@ boost %f", CombatKeyword.TargetAbilityBoost),
            new Sentence("Boost your @ %f", CombatKeyword.TargetAbilityBoost),
            new Sentence("Until you are attacked", CombatKeyword.UntilAttacked),
            new Sentence("(for all targets)", CombatKeyword.ApplyToAllies),
            new Sentence("Target does not yell for help because of this attack", CombatKeyword.NoYellForHelp),
            new Sentence("Doesn't cause the target to yell for help", CombatKeyword.NoYellForHelp),
            new Sentence("This attack does not cause the target to shout for help", CombatKeyword.NoYellForHelp),
            new Sentence("Does not cause the target to shout for help", CombatKeyword.NoYellForHelp),
            new Sentence("If it is a Werewolf ability", CombatKeyword.IfWerewolf),
            new Sentence("To the kicker", CombatKeyword.ToKickerTarget),
            new Sentence("Cause kicks", CombatKeyword.ToKickerTarget),
            new Sentence("Randomly repair broken bones twice as often", CombatKeyword.RepairBrokenBone),
            new Sentence("And %f armor", CombatKeyword.RestoreArmor),
            new Sentence("And %f power", CombatKeyword.RestorePower),
            new Sentence("Restore %f", CombatKeyword.RestoreHealth),
            new Sentence("%f Enthusiasm", CombatKeyword.AddEnthusiasm),
            new Sentence("%f Death Avoidance", CombatKeyword.AddDeathAvoidance),
            new Sentence("Target suffer a second blast of #D damage", CombatKeyword.SecondBlast),
            new Sentence("Target take a second full blast of delayed #D damage", CombatKeyword.SecondBlast),
            new Sentence("#D damage no longer dispel", CombatKeyword.NoDispel),
            new Sentence("Ignore mitigation from armor", CombatKeyword.IgnoreArmor),
            new Sentence("%f Body Heat", CombatKeyword.RestoreBodyHeat),
            new Sentence("Damage is %f", CombatKeyword.DamageBoost),
            new Sentence("Deal double damage", CombatKeyword.DamageBoostDouble),
            new Sentence("Channeling time is %f second", CombatKeyword.AddChannelingTime),
            new Sentence("Summons figment", CombatKeyword.AnotherTrap),
            new Sentence("Reduce it by %f more", CombatKeyword.Again, SignInterpretation.AlwaysNegative),
            new Sentence("Any time you Evade a Melee attack", CombatKeyword.OnEvadeMelee),
            new Sentence("Any time you Evade an attack", CombatKeyword.OnEvade),
            new Sentence("%f of all #D damage you take is mitigated and added to the damage done by your next Kick", CombatKeyword.MitigateReflectKick),
            new Sentence("%f of all #D damage you take is mitigated and added to the damage done by your next", CombatKeyword.MitigateReflect),
            new Sentence("At a %f rate", CombatKeyword.ReflectRate),
            new Sentence("Damage become #D", CombatKeyword.ChangeDamageType),
            new Sentence("Cause targets to lose %f Rage", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative),
        };
        #endregion

        #region Properties
        public List<IPgSkill> SkillList { get; } = new List<IPgSkill>();
        public int SelectedSkill1 { get; private set; } = -1;
        public int SelectedSkill2 { get; private set; } = -1;
        public List<GearSlot> GearSlotList { get; } = new List<GearSlot>();
        public List<AbilitySlot> AbilitySlot1List { get; } = new List<AbilitySlot>();
        public List<AbilitySlot> AbilitySlot2List { get; } = new List<AbilitySlot>();
        public string LastBuildFile { get; private set; }

        public string TitleText
        {
            get
            {
                string Result = "Project: Gorgon - Builder";
                if (LastBuildFile != null)
                    Result += $" - {LastBuildFile}";

                return Result;
            }
        }
        #endregion

        #region Implementation
        private void FillSkillList()
        {
            IObjectDefinition PowerDefinition = ObjectList.Definitions[typeof(PgJsonObjects.Power)];
            IList<IPgPower> PowerList = (IList<IPgPower>)PowerDefinition.VerifiedObjectList;
            
            SkillList.Clear();

            foreach (IPgPower PowerItem in PowerList)
            {
                if (PowerItem.IsUnavailable)
                    continue;

                IList<IPgPowerTier> TierEffectList = PowerItem.TierEffectList;
                if (TierEffectList.Count == 0)
                    continue;

                int CombatGearSlotCount = 0;
                foreach (ItemSlot GearSlot in PowerItem.SlotList)
                    switch (GearSlot)
                    {
                        case ItemSlot.Internal_None:
                        case ItemSlot.None:
                            break;

                        default:
                            CombatGearSlotCount++;
                            break;
                    }

                if (CombatGearSlotCount == 0)
                    continue;

                if (PowerItem.RawSkill == PowerSkill.Internal_None ||
                    PowerItem.RawSkill == PowerSkill.AnySkill ||
                    PowerItem.RawSkill == PowerSkill.ArmorPatching ||
                    PowerItem.RawSkill == PowerSkill.Endurance ||
                    PowerItem.RawSkill == PowerSkill.ShamanicInfusion)
                    continue;

                if (!SkillList.Contains(PowerItem.Skill))
                    SkillList.Add(PowerItem.Skill);
            }

            SkillList.Sort(SortByName);
        }

        private static int SortByName(IPgSkill skill1, IPgSkill skill2)
        {
            return string.Compare(skill1.Name, skill2.Name, StringComparison.InvariantCulture);
        }

        private void FillGearSlotList()
        {
            GearSlotList.Add(new GearSlot("Main Hand", ItemSlot.MainHand));
            GearSlotList.Add(new GearSlot("Off Hand", ItemSlot.OffHand));
            GearSlotList.Add(new GearSlot("Head", ItemSlot.Head));
            GearSlotList.Add(new GearSlot("Chest", ItemSlot.Chest));
            GearSlotList.Add(new GearSlot("Legs", ItemSlot.Legs));
            GearSlotList.Add(new GearSlot("Hands", ItemSlot.Hands));
            GearSlotList.Add(new GearSlot("Feet", ItemSlot.Feet));
            GearSlotList.Add(new GearSlot("Neck", ItemSlot.Necklace));
            GearSlotList.Add(new GearSlot("Ring", ItemSlot.Ring));
            GearSlotList.Add(new GearSlot("Racial", ItemSlot.Racial));
            GearSlotList.Add(new GearSlot("Waist", ItemSlot.Waist));
        }

        private void ResetGearSlots()
        {
            IPgSkill Skill1 = SelectedSkill1 >= 0 ? SkillList[SelectedSkill1] : null;
            IPgSkill Skill2 = SelectedSkill2 >= 0 ? SkillList[SelectedSkill2] : null;

            foreach (GearSlot Item in GearSlotList)
                Item.Reset(Skill1, Skill2);
        }

        private void FillAbilitySlotList()
        {
            for (int Index = 0; Index < 6; Index++)
            {
                AbilitySlot1List.Add(new AbilitySlot());
                AbilitySlot2List.Add(new AbilitySlot());
            }
        }

        private void ResetAbilitySlots(List<AbilitySlot> abilitySlotList)
        {
            foreach (AbilitySlot Item in abilitySlotList)
                Item.Reset();
        }

        private void FillEmptyAbilitySlots(IPgSkill skill, List<AbilitySlot> abilitySlotList, List<AbilityTierList> compatibleAbilityList)
        {
            compatibleAbilityList.Clear();

            IObjectDefinition AbilityDefinition = ObjectList.Definitions[typeof(Ability)];
            IList<IPgAbility> AbilityList = (IList<IPgAbility>)AbilityDefinition.VerifiedObjectList;

            foreach (IPgAbility Item in AbilityList)
                if (AbilityBelongToSkill(skill, Item))
                    AddAbilityTier(compatibleAbilityList, Item);

            if (skill.ParentSkill != null)
            {
                foreach (IPgAbility Item in AbilityList)
                    if (AbilityBelongToSkill(skill.ParentSkill, Item))
                        AddAbilityTier(compatibleAbilityList, Item);
            }

            List<string> FilledSlotList = new List<string>();
            foreach (AbilitySlot Item in abilitySlotList)
                if (!Item.IsEmpty)
                    FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));

            foreach (AbilitySlot Item in abilitySlotList)
                if (Item.IsEmpty)
                {
                    if (FillEmptyAbilitySlot(compatibleAbilityList, skill, Item, FilledSlotList))
                        FilledSlotList.Add(AbilitySlot.CuteDigitStrippedName(Item.Ability));
                }
        }

        private static bool AbilityBelongToSkill(IPgSkill skill, IPgAbility item)
        {
            if (item.Skill != skill)
                return false;

            if (item.KeywordList.Contains(AbilityKeyword.Lint_NotLearnable) && item.Name != "Sword Slash")
                return false;

            return true;
        }

        private static void AddAbilityTier(List<AbilityTierList> compatibleAbilityList, IPgAbility ability)
        {
            foreach (AbilityTierList Item in compatibleAbilityList)
                if (AbilitySlot.CuteDigitStrippedName(Item.Source) == AbilitySlot.CuteDigitStrippedName(ability))
                {
                    Item.Add(ability);
                    return;
                }

            compatibleAbilityList.Add(new AbilityTierList(ability));
        }

        private bool FillEmptyAbilitySlot(List<AbilityTierList> compatibleAbilityList, IPgSkill skill, AbilitySlot abilitySlot, List<string> filledSlotList)
        {
            foreach (AbilityTierList Item in compatibleAbilityList)
                if (!filledSlotList.Contains(AbilitySlot.CuteDigitStrippedName(Item.Source)))
                {
                    abilitySlot.SetAbility(Item, IconFolder);
                    return true;
                }

            return false;
        }

        private void DisplayAbilityChoiceMenu(FrameworkElement control, AbilitySlot slot, List<AbilityTierList> compatibleAbilityList)
        {
            if (control.ContextMenu == null)
                control.ContextMenu = new ContextMenu();
            ContextMenu Menu = control.ContextMenu;

            List<string> AbilityNameList = new List<string>();
            Menu.Items.Clear();

            foreach (AbilityTierList TierListItem in compatibleAbilityList)
            {
                if (AbilityNameList.Contains(TierListItem.Name))
                    continue;
                AbilityNameList.Add(TierListItem.Name);

                string Name = TierListItem.Name;
                MenuItem NewMenuItem = new MenuItem();
                NewMenuItem.Header = Name;
                NewMenuItem.IsChecked = TierListItem == slot.AbilityTierList;

                foreach (IPgAbility AbilityItem in TierListItem)
                {
                    MenuItem NewSubmenuItem = new MenuItem();
                    NewSubmenuItem.Header = AbilityItem.Name;
                    NewSubmenuItem.IsChecked = AbilityItem == slot.Ability;
                    NewSubmenuItem.Click += OnAbilityMenuClick;

                    NewMenuItem.Items.Add(NewSubmenuItem);
                }

                Menu.Items.Add(NewMenuItem);
            }

            Menu.Items.Add(new Separator());

            MenuItem MenuItemClear = new MenuItem();
            MenuItemClear.Header = "Clear";
            MenuItemClear.Click += OnClearAbility;
            MenuItemClear.DataContext = slot;

            Menu.Items.Add(MenuItemClear);
        }

        private void SelectAbilityByName(AbilitySlot slot, string selectedName, List<AbilityTierList> compatibleAbilityList)
        {
            foreach (AbilityTierList TierListItem in compatibleAbilityList)
                foreach (IPgAbility AbilityItem in TierListItem)
                    if (AbilityItem.Name == selectedName)
                    {
                        slot.SetAbility(TierListItem, AbilityItem, IconFolder);
                        return;
                    }

            slot.Reset();
        }

        private void SelectAbilityByKey(AbilitySlot slot, string selectedKey, List<AbilityTierList> compatibleAbilityList)
        {
            foreach (AbilityTierList TierListItem in compatibleAbilityList)
                foreach (IPgAbility AbilityItem in TierListItem)
                    if (AbilityItem.Key == selectedKey)
                    {
                        slot.SetAbility(TierListItem, AbilityItem, IconFolder);
                        return;
                    }

            slot.Reset();
        }

        private void CloseAbilityChoiceMenu(FrameworkElement control)
        {
            if (control.ContextMenu == null)
                return;
            ContextMenu Menu = control.ContextMenu;

            foreach (MenuItem MenuItem in Menu.Items)
                foreach (MenuItem SubmenuItem in MenuItem.Items)
                    SubmenuItem.Click -= OnAbilityMenuClick;
        }

        private readonly List<AbilityTierList> CompatibleAbility1List = new List<AbilityTierList>();
        private readonly List<AbilityTierList> CompatibleAbility2List = new List<AbilityTierList>();
        #endregion

        #region Events
        private void OnClosed(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void OnSkillSelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox) sender;
            OnSkillSelectionChanged1(Control.SelectedIndex);
        }

        private void OnSkillSelectionChanged1(int index)
        {
            if (SelectedSkill1 == index)
                return;

            SelectedSkill1 = index;
            NotifyPropertyChanged(nameof(SelectedSkill1));

            ResetAbilitySlots(AbilitySlot1List);
            FillEmptyAbilitySlots(SkillList[SelectedSkill1], AbilitySlot1List, CompatibleAbility1List);
            ResetGearSlots();

            CommandManager.InvalidateRequerySuggested();
            RecalculateMods();
        }

        private void OnSkillSelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            OnSkillSelectionChanged2(Control.SelectedIndex);
        }

        private void OnSkillSelectionChanged2(int index)
        {
            if (SelectedSkill2 == index)
                return;

            SelectedSkill2 = index;
            NotifyPropertyChanged(nameof(SelectedSkill2));

            ResetAbilitySlots(AbilitySlot2List);
            FillEmptyAbilitySlots(SkillList[SelectedSkill2], AbilitySlot2List, CompatibleAbility2List);
            ResetGearSlots();

            CommandManager.InvalidateRequerySuggested();
            RecalculateMods();
        }

        private void OnAbilityContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            FrameworkElement Control = (FrameworkElement)sender;
            AbilitySlot Slot = (AbilitySlot)Control.DataContext;

            if (AbilitySlot1List.Contains(Slot))
                DisplayAbilityChoiceMenu(Control, Slot, CompatibleAbility1List);
            else if (AbilitySlot2List.Contains(Slot))
                DisplayAbilityChoiceMenu(Control, Slot, CompatibleAbility2List);
        }

        private void OnAbilityMenuClick(object sender, RoutedEventArgs e)
        {
            MenuItem SubmenuItem = (MenuItem)sender;
            MenuItem MenuItem = (MenuItem)SubmenuItem.Parent;
            ContextMenu Menu = (ContextMenu)MenuItem.Parent;
            AbilitySlot Slot = (AbilitySlot)Menu.DataContext;
            string SelectedName = (string)SubmenuItem.Header;

            if (AbilitySlot1List.Contains(Slot))
                SelectAbilityByName(Slot, SelectedName, CompatibleAbility1List);
            else if (AbilitySlot2List.Contains(Slot))
                SelectAbilityByName(Slot, SelectedName, CompatibleAbility2List);

            RecalculateMods();
        }

        private void OnClearAbility(object sender, RoutedEventArgs e)
        {
            MenuItem Menu = (MenuItem)e.OriginalSource;
            AbilitySlot Slot = (AbilitySlot)Menu.DataContext;

            OnClearAbility(Slot);
        }

        private void OnClearAbility(AbilitySlot slot)
        {
            slot.Reset();
            RecalculateMods();
        }

        private void OnAbilityContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            FrameworkElement Control = (FrameworkElement)sender;
            CloseAbilityChoiceMenu(Control);
        }

        private void OnSelectAbilities1(List<string> abilityTable)
        {
            int Index;

            for (Index = 0; Index < 6 && Index < abilityTable.Count; Index++)
            {
                AbilitySlot Slot = AbilitySlot1List[Index];
                string SelectedKey = abilityTable[Index];
                SelectAbilityByKey(Slot, SelectedKey, CompatibleAbility1List);
            }

            for (; Index < 6; Index++)
            {
                AbilitySlot Slot = AbilitySlot1List[Index];
                OnClearAbility(Slot);
            }

            RecalculateMods();
        }

        private void OnSelectAbilities2(List<string> abilityTable)
        {
            int Index;

            for (Index = 0; Index < 6 && Index < abilityTable.Count; Index++)
            {
                AbilitySlot Slot = AbilitySlot2List[Index];
                string SelectedKey = abilityTable[Index];
                SelectAbilityByKey(Slot, SelectedKey, CompatibleAbility2List);
            }

            for (; Index < 6; Index++)
            {
                AbilitySlot Slot = AbilitySlot2List[Index];
                OnClearAbility(Slot);
            }

            RecalculateMods();
        }

        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            GearSlot Slot = (GearSlot) Control.DataContext;

            if (Slot.SelectedItemIndex != Control.SelectedIndex)
            {
                Slot.SetSelectedItem(Control.SelectedIndex);
                RecalculateMods();
            }
        }

        private void OnSelectGear(string gearSlotName, string itemKey)
        {
            foreach (GearSlot Item in GearSlotList)
                if (Item.Name == gearSlotName)
                {
                    Item.SetSelectedItem(itemKey);
                    RecalculateMods();
                    break;
                }
        }

        private void OnPowerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox Control = (ComboBox)sender;
            if (Control.Items.Count > 0)
            {
                Mod Mod = (Mod)Control.DataContext;

                if (Mod.SelectedPowerIndex != Control.SelectedIndex)
                {
                    Mod.SetSelectedPower(Control.SelectedIndex);
                    RecalculateMods();
                }
            }
        }

        private void CanAddMod(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = SelectedSkill1 >= 0 || SelectedSkill2 >= 0;
        }

        private void OnAddMod(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            GearSlot Slot = (GearSlot)Control.DataContext;
            Slot.AddMod();
        }

        private void OnIncrementTier(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.IncrementTier();
            RecalculateMods();
        }

        private void OnDecrementTier(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.DecrementTier();
            RecalculateMods();
        }

        private void OnMoveDown(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.MoveDown();
        }

        private void OnMoveUp(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            Mod.MoveUp();
        }

        private void OnSelectGearMods(string gearSlotName, List<KeyValuePair<string, int>> modList)
        {
            foreach (GearSlot Item in GearSlotList)
                if (Item.Name == gearSlotName)
                {
                    OnSelectGearMods(Item, modList);
                    break;
                }

            RecalculateMods();
        }

        private void OnSelectGearMods(GearSlot slot, List<KeyValuePair<string, int>> modList)
        {
            slot.ResetMods();

            foreach (KeyValuePair<string, int> Item in modList)
                slot.AddMod(Item.Key, Item.Value);
        }

        private void OnLoad(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog Dlg = new OpenFileDialog();
            Dlg.FileName = LastBuildFile;
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                OnLoad(Dlg.FileName);
        }

        private void OnLoad(string filePath)
        {
            if (!LoadBuild(filePath, out IPgSkill Skill1, out IPgSkill Skill2, out List<string>[] AbilityTable, out Dictionary<string, string> GearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> GearModTable))
            { 
                MessageBox.Show("Invalid file format", "Error");
                return;
            }

            Debug.Assert(AbilityTable.Length == 2);
            Debug.Assert(SkillList.Contains(Skill1));
            Debug.Assert(SkillList.Contains(Skill2));

            LastBuildFile = filePath;
            NotifyPropertyChanged(nameof(LastBuildFile));
            NotifyPropertyChanged(nameof(TitleText));

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<int>(OnSkillSelectionChanged1), SkillList.IndexOf(Skill1));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<int>(OnSkillSelectionChanged2), SkillList.IndexOf(Skill2));
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<string>>(OnSelectAbilities1), AbilityTable[0]);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<List<string>>(OnSelectAbilities2), AbilityTable[1]);
            foreach (KeyValuePair<string, string> Entry in GearItemTable)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, string>(OnSelectGear), Entry.Key, Entry.Value);
            foreach (KeyValuePair<string, List<KeyValuePair<string, int>>> Entry in GearModTable)
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string, List<KeyValuePair<string, int>>>(OnSelectGearMods), Entry.Key, Entry.Value);

            RecalculateMods();
        }

        private void OnSave(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog Dlg = new SaveFileDialog();
            Dlg.FileName = LastBuildFile;
            Dlg.Filter = "Build File (*.txt)|*.txt";

            bool? Result = Dlg.ShowDialog(this);
            if (Result.Value && !string.IsNullOrEmpty(Dlg.FileName))
                SaveBuild(Dlg.FileName);
        }

        private void OnClearItem(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button) e.OriginalSource;
            GearSlot Slot = (GearSlot) Control.DataContext;

            Slot.ResetItem();
        }

        private void OnClearMod(object sender, ExecutedRoutedEventArgs e)
        {
            Button Control = (Button)e.OriginalSource;
            Mod Mod = (Mod)Control.DataContext;
            GearSlot Slot = Mod.ParentSlot;

            Slot.RemoveMod(Mod);
        }
        #endregion

        #region Load
        private bool LoadBuild(string fileName, out IPgSkill skill1, out IPgSkill skill2, out List<string>[] abilityTable, out Dictionary<string, string> gearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> gearModTable)
        {
            using FileStream Stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return LoadBuild(Stream, out skill1, out skill2, out abilityTable, out gearItemTable, out gearModTable);
        }

        private bool LoadBuild(FileStream stream, out IPgSkill skill1, out IPgSkill skill2, out List<string>[] abilityTable, out Dictionary<string, string> gearItemTable, out Dictionary<string, List<KeyValuePair<string, int>>> gearModTable)
        {
            skill1 = null;
            skill2 = null;
            abilityTable = new List<string>[2]
            {
                new List<string>(),
                new List<string>(),
            };
            gearItemTable = new Dictionary<string, string>();
            gearModTable = new Dictionary<string, List<KeyValuePair<string, int>>>();

            JsonTextReader Reader = new JsonTextReader(stream);
            
            Reader.Read();
            if (!(Reader.CurrentValue is string BuildKey && BuildKey  == "Build"))
                return false;

            Reader.Read();
            if (Reader.CurrentToken != Json.Token.ObjectStart)
                return false;

            if (!LoadBuildSkills(Reader, out skill1, out skill2))
                return false;

            Reader.Read();

            int SkillIndex = 0;
            int AbilityIndex = 0;
            while (LoadBuildAbility(Reader, ref SkillIndex, ref AbilityIndex, out string AbilityName))
            {
                if (SkillIndex < 0 || SkillIndex >= abilityTable.Length)
                    return false;

                abilityTable[SkillIndex].Add(AbilityName);
                Reader.Read();
            }

            while (LoadBuildGear(Reader, out string SlotName, out string ItemName, out List<KeyValuePair<string, int>> ModTable))
            {
                if (gearItemTable.ContainsKey(SlotName) || gearModTable.ContainsKey(SlotName))
                    return false;

                gearItemTable.Add(SlotName, ItemName);
                gearModTable.Add(SlotName, ModTable);

                Reader.Read();
            }

            if (Reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildSkills(JsonTextReader reader, out IPgSkill skill1, out IPgSkill skill2)
        {
            skill1 = null;
            skill2 = null;

            reader.Read();
            if (!(reader.CurrentValue is string SkillKey1 && SkillKey1 == "Skill1"))
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string SkillValue1))
                return false;

            reader.Read();
            if (!(reader.CurrentValue is string SkillKey2 && SkillKey2 == "Skill2"))
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string SkillValue2))
                return false;

            if (!TryParseSkill(SkillValue1, out skill1) || !TryParseSkill(SkillValue2, out skill2))
                return false;

            return true;
        }

        private bool TryParseSkill(string skillName, out IPgSkill skill)
        {
            skill = null;

            foreach (IPgSkill Item in SkillList)
                if (Item.Name == skillName)
                {
                    skill = Item;
                    return true;
                }

            return false;
        }

        private bool LoadBuildAbility(JsonTextReader reader, ref int skillIndex, ref int abilityIndex, out string abilityName)
        {
            abilityName = string.Empty;

            if (!(reader.CurrentValue is string Key))
                return false;

            string[] Split = Key.Split('_');
            if (Split.Length != 3)
                return false;

            string KeyName = Split[0];
            if (KeyName != "Ability")
                return false;

            if (!int.TryParse(Split[1], out int SkillIndexValue))
                return false;

            if (SkillIndexValue != skillIndex)
            {
                if (SkillIndexValue != skillIndex + 1)
                    return false;

                skillIndex++;
                abilityIndex = 0;
            }

            if (!int.TryParse(Split[2], out int AbilityIndexValue))
                return false;

            if (abilityIndex != AbilityIndexValue)
                return false;

            abilityIndex++;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string AbilityValue))
                return false;

            abilityName = AbilityValue;

            return true;
        }

        private bool LoadBuildGear(JsonTextReader reader, out string slotName, out string itemName, out List<KeyValuePair<string, int>> modTable)
        {
            slotName = string.Empty;
            itemName = string.Empty;
            modTable = new List<KeyValuePair<string, int>>();

            if (!(reader.CurrentValue is string SlotKey))
                return false;

            slotName = SlotKey;

            reader.Read();
            if (reader.CurrentToken != Json.Token.ObjectStart)
                return false;

            reader.Read();
            if (reader.CurrentValue is string GearKey1 && GearKey1 == "Item")
            {
                reader.Read();
                if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string ItemValue))
                    return false;

                itemName = ItemValue;

                reader.Read();
            }

            int PowerIndex = 0;
            int TierIndex = 0;
            while (LoadBuildPower(reader, ref PowerIndex, ref TierIndex, out string PowerKey, out int TierLevel))
            {
                modTable.Add(new KeyValuePair<string, int>(PowerKey, TierLevel));
                reader.Read();
            }

            if (reader.CurrentToken != Json.Token.ObjectEnd)
                return false;

            return true;
        }

        private bool LoadBuildPower(JsonTextReader reader, ref int powerIndex, ref int tierIndex, out string powerKey, out int tierLevel)
        {
            powerKey = string.Empty;
            tierLevel = -1;

            if (!(reader.CurrentValue is string Key1))
                return false;

            string[] Split1 = Key1.Split('_');
            if (Split1.Length != 2)
                return false;

            string KeyName1 = Split1[0];

            if (KeyName1 != "Power")
                return false;

            if (!int.TryParse(Split1[1], out int PowerIndexValue))
                return false;

            if (powerIndex != PowerIndexValue)
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.String || !(reader.CurrentValue is string PowerValue))
                return false;

            powerKey = PowerValue;
            powerIndex++;

            reader.Read();

            if (!(reader.CurrentValue is string Key2))
                return false;

            string[] Split2 = Key2.Split('_');
            if (Split2.Length != 2)
                return false;

            string KeyName2 = Split2[0];

            if (KeyName2 != "PowerTier")
                return false;

            if (!int.TryParse(Split2[1], out int TierIndexValue))
                return false;

            if (tierIndex != TierIndexValue)
                return false;

            if (tierIndex >= powerIndex)
                return false;

            reader.Read();
            if (reader.CurrentToken != Json.Token.Integer)
                return false;

            tierLevel = (int)reader.CurrentValue;
            tierIndex++;

            return true;
        }
        #endregion

        #region Save
        private void SaveBuild(string filePath)
        {
            using FileStream Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            SaveBuild(Stream);

            LastBuildFile = filePath;
            NotifyPropertyChanged(nameof(LastBuildFile));
            NotifyPropertyChanged(nameof(TitleText));
        }

        private void SaveBuild(FileStream stream)
        {
            JsonTextWriter Writer = new JsonTextWriter(true);
            Writer.ObjectKey("Build");
            Writer.ObjectStart();

            SaveBuildSkills(Writer);

            for (int i = 0; i < AbilitySlot1List.Count; i++)
            {
                AbilitySlot Item = AbilitySlot1List[i];
                SaveBuildAbilitySlot(Writer, 0, i, Item);
            }

            for (int i = 0; i < AbilitySlot2List.Count; i++)
            {
                AbilitySlot Item = AbilitySlot2List[i];
                SaveBuildAbilitySlot(Writer, 1, i, Item);
            }

            foreach (GearSlot Item in GearSlotList)
                SaveBuildGearSlot(Writer, Item);

            Writer.ObjectEnd();
            Writer.Flush(stream);
        }

        private void SaveBuildSkills(JsonTextWriter writer)
        {
            string SkillName1 = SelectedSkill1 >= 0 ? SkillList[SelectedSkill1].Name : string.Empty;
            string SkillName2 = SelectedSkill2 >= 0 ? SkillList[SelectedSkill2].Name : string.Empty;

            writer.ObjectKey("Skill1");
            writer.Value(SkillName1);

            writer.ObjectKey("Skill2");
            writer.Value(SkillName2);
        }

        private void SaveBuildAbilitySlot(JsonTextWriter writer, int skillIndex, int abilityIndex, AbilitySlot slot)
        {
            string Key = $"Ability_{skillIndex}_{abilityIndex}";
            string Value = slot.IsEmpty ? string.Empty : slot.Ability.Key;

            writer.ObjectKey(Key);
            writer.Value(Value);
        }

        private void SaveBuildGearSlot(JsonTextWriter writer, GearSlot slot)
        {
            writer.ObjectKey(slot.Name);
            writer.ObjectStart();

            if (slot.SelectedItemIndex >= 0 && slot.SelectedItemIndex < slot.ItemList.Count)
            {
                writer.ObjectKey("Item");
                writer.Value(slot.ItemList[slot.SelectedItemIndex].ItemKey);
            }

            int Index, PowerIndex;
            for (Index = 0, PowerIndex = 0; Index < slot.ModList.Count; Index++)
            {
                Mod Mod = slot.ModList[Index];
                SaveBuildMod(writer, ref PowerIndex, Mod);
            }

            writer.ObjectEnd();
        }

        private void SaveBuildMod(JsonTextWriter writer, ref int powerIndex, Mod mod)
        {
            if (mod.SelectedPowerIndex < 0 || mod.SelectedPowerIndex >= mod.AvailablePowerList.Count)
                return;

            Power Power = mod.AvailablePowerList[mod.SelectedPowerIndex];
            SaveBuilPower(writer, ref powerIndex, Power);
        }

        private void SaveBuilPower(JsonTextWriter writer, ref int powerIndex, Power power)
        {
            if (power.SelectedTier < 0 || power.SelectedTier >= power.Source.CombinedTierList.Count)
                return;

            string KeyPower = $"Power_{powerIndex}";
            string ValuePower = power.Source.Key;
            string KeyTier = $"PowerTier_{powerIndex}";
            int ValueTier = power.SelectedTier;

            writer.ObjectKey(KeyPower);
            writer.Value(ValuePower);
            writer.ObjectKey(KeyTier);
            writer.Value(ValueTier);

            powerIndex++;
        }
        #endregion

        #region Settings
        private const string LastBuildFileValueName = "LastBuildFile";

        private void LoadSettings()
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                Key = Key.CreateSubKey("Project Gorgon Tools");
                RegistryKey SettingKey = Key.CreateSubKey("PgBuilder");

                string Value;

                Value = SettingKey?.GetValue(LastBuildFileValueName) as string;
                if (Value != null)
                    if (File.Exists(Value))
                        OnLoad(Value);
            }
            catch
            {
            }
        }

        private void SaveSettings()
        {
            try
            {
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"Software", true);
                Key = Key.CreateSubKey("Project Gorgon Tools");
                RegistryKey SettingKey = Key.CreateSubKey("PgBuilder");

                SettingKey?.SetValue(LastBuildFileValueName, LastBuildFile, RegistryValueKind.String);
            }
            catch
            {
            }
        }
        #endregion

        #region Recalculate Mods
        private void RecalculateMods()
        {
            if (RecalculateOperation == null || RecalculateOperation.Status == DispatcherOperationStatus.Completed)
            {
                Debug.WriteLine("Scheduling recalculate");
                RecalculateOperation = Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(RecalculateModsNow));
            }
            else
                Debug.WriteLine("Skipping recalculate");
        }

        private void RecalculateModsNow()
        {
            Debug.WriteLine("Starting recalculate");
            ResetAllMods();
            RecalculateAllMods();
            Debug.WriteLine("Done with recalculate");
        }

        private void ResetAllMods()
        {
            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.ResetMods();

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.ResetMods();
        }

        private void RecalculateAllMods()
        {
            foreach (GearSlot Item in GearSlotList)
                RecalculateSlotMods(Item);
        }

        private void RecalculateSlotMods(GearSlot slot)
        {
            if (slot.SelectedItemIndex >= 0)
            {
                ItemInfo Item = slot.ItemList[slot.SelectedItemIndex];
                RecalculateItemMods(Item);
            }

            foreach (Mod Item in slot.ModList)
                RecalculateSelectedMods(Item);
        }

        private void RecalculateItemMods(ItemInfo gearItem)
        {
            foreach (IPgItemEffect Item in gearItem.Item.EffectDescriptionList)
                RecalculateEffectMod(Item);
        }

        private void RecalculateSelectedMods(Mod mod)
        {
            IPgPowerTier Tier = mod.SelectedTier;
            if (Tier == null)
                return;

            foreach (IPgPowerEffect Item in Tier.EffectList)
                RecalculateEffectMod(mod.SelectedPower, mod.SelectedPower.SelectedTier, Item);
        }

        private void RecalculateEffectMod(IPgItemEffect effect)
        {
            switch (effect)
            {
                case IPgItemAttributeLink AsItemAttributeLink:
                    RecalculateAttributeMods(AsItemAttributeLink.Link, AsItemAttributeLink.AttributeEffect);
                    break;

                case IPgItemSimpleEffect AsItemSimpleEffect:
                    RecalculateSimpleEffectMods(AsItemSimpleEffect);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void RecalculateEffectMod(Power power, int tier, IPgPowerEffect effect)
        {
            switch (effect)
            {
                case IPgPowerAttributeLink AsPowerAttributeLink:
                    RecalculateAttributeMods(AsPowerAttributeLink.AttributeLink, AsPowerAttributeLink.AttributeEffect);
                    break;

                case IPgPowerSimpleEffect AsPowerSimpleEffect:
                    RecalculateSimpleEffectMods(power, tier, AsPowerSimpleEffect);
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        private void RecalculateAttributeMods(IPgAttribute attribute, float attributeEffect)
        {
            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.RecalculateMods(attribute.Key, attributeEffect);

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.RecalculateMods(attribute.Key, attributeEffect);
        }

        private void RecalculateSimpleEffectMods(IPgItemSimpleEffect itemSimpleEffect)
        {
            Debug.WriteLine($"Ignoring item effect: {itemSimpleEffect.Description}");
        }

        private void RecalculateSimpleEffectMods(Power power, int tier, IPgPowerSimpleEffect powerSimpleEffect)
        {
            if (powerSimpleEffect.Description == "All Ice Magic attacks that hit a single target have a 33% chance to deal +48% damage")
            {
            }

            foreach (KeyValuePair<string, string> Entry in PowerToEffectTable2)
                if (powerSimpleEffect.Description.StartsWith(Entry.Key))
                {
                    RecalculateKnownEffectMods(power, Entry.Value, tier);
                    break;
                }
        }

        private void RecalculateKnownEffectMods(Power power, string effectKey, int tier)
        {
            foreach (AbilitySlot Item in AbilitySlot1List)
                Item.AddEffect(power, effectKey, tier);

            foreach (AbilitySlot Item in AbilitySlot2List)
                Item.AddEffect(power, effectKey, tier);
        }

        private static Dictionary<string, string> PowerToEffectTable2 { get; } = new Dictionary<string, string>()
        {
            { "All Ice Magic attacks that hit a single target have a 33% chance to deal", "50032" },
        };

        private DispatcherOperation RecalculateOperation = null;
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        /// Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
