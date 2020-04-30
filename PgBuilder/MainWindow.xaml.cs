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

            LoadCachedData();
            FillSkillList();
            FillGearSlotList();
            FillAbilitySlotList();
            AnalyzeCachedData();

            LoadSettings();

            Closed += OnClosed;
        }
        #endregion

        #region Data Load
        public void LoadCachedData()
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            int Version = 0;
            string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
            foreach (string Item in VersionFolders)
                if (int.TryParse(Path.GetFileName(Item), out int VersionValue) && Version < VersionValue)
                    Version = VersionValue;

            string IconCacheFolder = Path.Combine(ApplicationFolder, "Shared Icons");
            string VersionFolder = Path.Combine(VersionCacheFolder, Version.ToString());
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
        private void AnalyzeCachedData()
        {
            FilterValidPowers(out _, out List<IPgPower> PowerSimpleEffectList);
            FilterValidEffects(out Dictionary<string, Dictionary<string, List<IPgEffect>>> AllEffectTable);
            FindPowersWithMatchingEffect(AllEffectTable, PowerSimpleEffectList, out Dictionary<IPgPower, List<IPgEffect>> PowerToEffectTable, out List<IPgPower> UnmatchedPowerList);
            GetAbilityNames(out List<string> AbilityNameList, out Dictionary<string, List<AbilityKeyword>> NameToKeyword);

            AnalyzeMatchingEffects(AbilityNameList, NameToKeyword, PowerToEffectTable);
            //AnalyzeRemainingEffects(AbilityNameList, UnmatchedPowerList);
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
            { "Hammer attack", new List<AbilityKeyword>() { AbilityKeyword.HammerAttack } },
            { "All Druid abilities", new List<AbilityKeyword>() { AbilityKeyword.Druid } },
            { "All Staff attack", new List<AbilityKeyword>() { AbilityKeyword.StaffAttack } },
            { "All Ice Magic abilities", new List<AbilityKeyword>() { AbilityKeyword.IceMagic } },
            { "Knife abilities with 'Cut' in their name", new List<AbilityKeyword>() { AbilityKeyword.KnifeCut } },
            { "All Knife abilities WITHOUT 'Cut' in their name", new List<AbilityKeyword>() { AbilityKeyword.KnifeNonCut } },
            { "All Knife Fighting attack", new List<AbilityKeyword>() { AbilityKeyword.Knife } },
            { "Bard Songs", new List<AbilityKeyword>() { AbilityKeyword.BardSong } },
            { "Spider Skill", new List<AbilityKeyword>() { AbilityKeyword.Spider } },
            { "All Major Healing abilities targeting you", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
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
            AbilityKeyword.IceMagic,
            AbilityKeyword.KnifeCut,
            AbilityKeyword.KnifeNonCut,
            AbilityKeyword.Knife,
            AbilityKeyword.BardSong,
            AbilityKeyword.Spider,
            AbilityKeyword.SurvivalUtility,
            AbilityKeyword.MajorHeal,
            AbilityKeyword.Rabbit,
            AbilityKeyword.Mutation_KneeSpikes,
            AbilityKeyword.Mutation_ExtraSkin,
            AbilityKeyword.Mutation_ExtraHeart,
            AbilityKeyword.Mutation_StretchySpine,
            AbilityKeyword.StabledPet,
            AbilityKeyword.CombatRefresh,
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
                string PatternWithAbilities = Pattern + " abilities";
                string PatternWithAbility = Pattern + " ability";
                List<AbilityKeyword> KeywordList = Entry.Value;

                foreach (AbilityKeyword Keyword in KeywordList)
                    Debug.Assert(GenericAbilityList.Contains(Keyword));

                Debug.Assert(!nameToKeyword.ContainsKey(PatternWithAbilities));
                nameToKeyword.Add(PatternWithAbilities, KeywordList);
                abilityNameList.Add(PatternWithAbilities);

                Debug.Assert(!nameToKeyword.ContainsKey(PatternWithAbility));
                nameToKeyword.Add(PatternWithAbility, KeywordList);
                abilityNameList.Add(PatternWithAbility);

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
                        abilityNameList.Insert(i, SmallName);
                        abilityNameList.Insert(i, LargeName);
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
        private void AnalyzeMatchingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, Dictionary<IPgPower, List<IPgEffect>> powerToEffectTable)
        {
            int DebugIndex = 0;
            int SkipIndex = 0;
            List<string[]> StringKeyTable = new List<string[]>();
            List<List<CombatEffect>[]> PowerKeyToCompleteEffectTable = new List<List<CombatEffect>[]>();

            foreach (KeyValuePair<IPgPower, List<IPgEffect>> Entry in powerToEffectTable)
            {
                DebugIndex++;

                if (SkipIndex > 0)
                {
                    SkipIndex--;
                    continue;
                }

                //Debug.WriteLine($"Debug Index: {DebugIndex - 1} / {powerToEffectTable.Count}");
                //Debug.WriteLine("");

                IPgPower ItemPower = Entry.Key;
                List<IPgEffect> ItemEffectList = Entry.Value;

                AnalyzeMatchingEffects(abilityNameList, nameToKeyword, ItemPower, ItemEffectList, out string[] stringKeyArray, out List<CombatEffect>[] modCombatListArray);

                StringKeyTable.Add(stringKeyArray);
                PowerKeyToCompleteEffectTable.Add(modCombatListArray);
            }

            //WritePowerKeyToCompleteEffectFile(StringKeyTable, PowerKeyToCompleteEffectTable);
            CompareWithPowerKeyToCompleteEffectTable(StringKeyTable, PowerKeyToCompleteEffectTable);

            DisplayParsingResult(powerToEffectTable);
        }

        private void WritePowerKeyToCompleteEffectFile(List<string[]> stringKeyTable, List<List<CombatEffect>[]> powerKeyToCompleteEffectTable)
        {
            using (FileStream fs = new FileStream("PowerKeyToCompleteEffect.cs", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("namespace PgBuilder");
                    sw.WriteLine("{");
                    sw.WriteLine("    using System.Collections.Generic;");
                    sw.WriteLine("");
                    sw.WriteLine("    public static class PowerKeyToCompleteEffect");
                    sw.WriteLine("    {");
                    sw.WriteLine("        public static Dictionary<string, List<CombatEffect>> Table { get; } = new Dictionary<string, List<CombatEffect>>()");
                    sw.WriteLine("        {");

                    Debug.Assert(stringKeyTable.Count == powerKeyToCompleteEffectTable.Count);

                    for (int i = 0; i < stringKeyTable.Count; i++)
                    {
                        string[] StringKeyArray = stringKeyTable[i];
                        List<CombatEffect>[] ModCombatListArray = powerKeyToCompleteEffectTable[i];

                        Debug.Assert(StringKeyArray.Length == ModCombatListArray.Length);

                        for (int j = 0; j < StringKeyArray.Length; j++)
                        {
                            string StringKey = StringKeyArray[j];
                            List<CombatEffect> ModCombatList = ModCombatListArray[j];

                            WritePowerKeyToCompleteEffectLine(sw, StringKey, ModCombatList);
                        }
                    }

                    sw.WriteLine("        };");
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }
        }

        private void WritePowerKeyToCompleteEffectLine(StreamWriter sw, string stringKey, List<CombatEffect> modCombatList)
        {
            string CombatEffectListString = string.Empty;

            for (int j = 0; j < modCombatList.Count; j++)
            {
                CombatEffect CombatEffect = modCombatList[j];

                if (CombatEffectListString.Length > 0)
                    CombatEffectListString += ", ";

                string CombatEffectString;

                if (!CombatEffect.Data1.IsValueSet && CombatEffect.DamageType == GameDamageType.None && CombatEffect.CombatSkill == GameCombatSkill.None)
                    CombatEffectString = $"new CombatEffect(CombatKeyword.{CombatEffect.Keyword})";
                else
                {
                    if (CombatEffect.Data1.IsValueSet)
                    {
                        string CreateMethod = CombatEffect.Data1.IsPercent ? "FromDoublePercent" : "FromDouble";
                        string NumericValueString = $"NumericValue.{CreateMethod}({CombatEffect.Data1.Value.ToString(CultureInfo.InvariantCulture)})";

                        if (CombatEffect.DamageType == GameDamageType.None && CombatEffect.CombatSkill == GameCombatSkill.None)
                            CombatEffectString = $"new CombatEffect(CombatKeyword.{CombatEffect.Keyword}, {NumericValueString})";
                        else
                            CombatEffectString = $"new CombatEffect(CombatKeyword.{CombatEffect.Keyword}, {NumericValueString}, new NumericValue(), (GameDamageType){(int)CombatEffect.DamageType}, GameCombatSkill.{CombatEffect.CombatSkill})";
                    }
                    else
                        CombatEffectString = $"new CombatEffect(CombatKeyword.{CombatEffect.Keyword}, new NumericValue(), new NumericValue(), (GameDamageType){(int)CombatEffect.DamageType}, GameCombatSkill.{CombatEffect.CombatSkill})";
                }

                CombatEffectListString += CombatEffectString;
            }

            if (CombatEffectListString.Length == 0)
                CombatEffectListString = " ";
            else
                CombatEffectListString = $" {CombatEffectListString} ";

            string Line = $"            {{ \"{stringKey}\", new List<CombatEffect>() {{{CombatEffectListString}}} }},";

            sw.WriteLine(Line);
        }

        private void CompareWithPowerKeyToCompleteEffectTable(List<string[]> stringKeyTable, List<List<CombatEffect>[]> powerKeyToCompleteEffectTable)
        {
            Debug.Assert(stringKeyTable.Count == powerKeyToCompleteEffectTable.Count);

            for (int i = 0; i < stringKeyTable.Count; i++)
            {
                string[] StringKeyArray = stringKeyTable[i];
                List<CombatEffect>[] ModCombatListArray = powerKeyToCompleteEffectTable[i];

                Debug.Assert(StringKeyArray.Length == ModCombatListArray.Length);

                for (int j = 0; j < StringKeyArray.Length; j++)
                {
                    string StringKey = StringKeyArray[j];
                    List<CombatEffect> ModCombatList = ModCombatListArray[j];

                    CompareWithPowerKeyToCompleteEffectLine(StringKey, ModCombatList);
                }
            }
        }

        private void CompareWithPowerKeyToCompleteEffectLine(string stringKey, List<CombatEffect> modCombatList)
        {
            if (!PowerKeyToCompleteEffect.Table.ContainsKey(stringKey))
            {
                Debug.WriteLine($"Key missing: {stringKey}");
                return;
            }

            if (!CombatEffect.IsEqualStrict(PowerKeyToCompleteEffect.Table[stringKey], modCombatList))
            {
                Debug.WriteLine($"Line mistmatch for key {stringKey}");
            }
        }

        private string PowerEffectPairKey(IPgPower power, List<IPgEffect> effectList, int tierIndex)
        {
            string PowerTierString = (tierIndex + 1).ToString("D03");
            string Key = $"{power.Key}_{PowerTierString}|{effectList[tierIndex].Key}";

            return Key;
        }

        private void AnalyzeMatchingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPower itemPower, List<IPgEffect> itemEffectList, out string[] stringKeyArray, out List<CombatEffect>[] modCombatListArray)
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
            modCombatListArray = new List<CombatEffect>[TierEffectList.Count];
            stringKeyArray = new string[TierEffectList.Count];

            int LastTierIndex = TierEffectList.Count - 1;

            AnalyzeMatchingEffects(abilityNameList, nameToKeyword, TierEffectList[LastTierIndex], itemEffectList[LastTierIndex], out List<CombatKeyword> ExtractedPowerTierKeywordList, out List<CombatKeyword> ExtractedEffectKeywordList, out List<CombatEffect> ExtracteddModCombatList, true);
            PowerTierKeywordListArray[LastTierIndex] = ExtractedPowerTierKeywordList;
            EffectKeywordListArray[LastTierIndex] = ExtractedEffectKeywordList;
            modCombatListArray[LastTierIndex] = ExtracteddModCombatList;

            for (int i = 0; i + 1 < TierEffectList.Count; i++)
            {
                AnalyzeMatchingEffects(abilityNameList, nameToKeyword, TierEffectList[i], itemEffectList[i], out List<CombatKeyword> ComparedPowerTierKeywordList, out List<CombatKeyword> ComparedEffectKeywordList, out List<CombatEffect> ParsedModCombatList, false);

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
                modCombatListArray[i] = ParsedModCombatList;
            }

            for (int i = 0; i < TierEffectList.Count; i++)
            {
                string Key = PowerEffectPairKey(itemPower, itemEffectList, i);
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

            //if (MissingKeywordList.Count == 1 && MissingKeywordList[0] == CombatKeyword.DamageBoost && list1.Contains(CombatKeyword.DamagePercentageBoost) && list1.Count == list2.Count)
            //    return true;

            if (MissingKeywordList.Count == 1 && MissingKeywordList[0] == CombatKeyword.ApplyWithChance && list1.Contains(CombatKeyword.DamageBoost) && list1.Count == list2.Count)
                return true;

            return false;
        }

        private void AnalyzeMatchingEffects(List<string> abilityNameList, Dictionary<string, List<AbilityKeyword>> nameToKeyword, IPgPowerTier powerTier, IPgEffect effect, out List<CombatKeyword> extractedPowerTierKeywordList, out List<CombatKeyword> extractedEffectKeywordList, out List<CombatEffect> parsedModCombatList, bool displayAnalysisResult)
        {
            extractedPowerTierKeywordList = new List<CombatKeyword>();
            parsedModCombatList = new List<CombatEffect>();

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
            string ParsedEffectTargetAbilityList = AbilityKeywordListToString(EffectTargetAbilityList);
            string ParsedAbilityList = AbilityKeywordListToString(ModAbilityList);
            string ParsedPowerString = CombatEffectListToString(ModCombatList, out extractedPowerTierKeywordList);
            string ParsedModTargetAbilityList = AbilityKeywordListToString(ModTargetAbilityList);

            bool IsSameTarget = IsSameAbilityKeywordList(ModTargetAbilityList, EffectTargetAbilityList);
            if (!IsSameTarget)
            {
                Debug.WriteLine("");
                Debug.WriteLine("BAD TARGET!");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
                return;
            }

            bool IsContained = CombatEffect.Contains(ModCombatList, EffectCombatList);
            if (!IsContained)
            {
                Debug.WriteLine("");
                Debug.WriteLine("UNPARSED!");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
                return;
            }

            parsedModCombatList = ModCombatList;

/*
            if (displayAnalysisResult)
            {
                Debug.WriteLine("");
                Debug.WriteLine($"   Effect: {effect.Desc}");
                Debug.WriteLine($"Parsed as: {ParsedEffectString}, Target: {ParsedEffectTargetAbilityList}");
                Debug.WriteLine($"    Power: {powerSimpleEffect.Description}");
                Debug.WriteLine($"Parsed as: {{{ParsedAbilityList}}} {ParsedPowerString}, Target: {ParsedModTargetAbilityList}");
            }
*/
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

        private bool IsSameAbilityKeywordList(List<AbilityKeyword> list1, List<AbilityKeyword> list2)
        {
            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (list1[i] != list2[i])
                    return false;

            return true;
        }

        private string AbilityKeywordListToString(List<AbilityKeyword> list)
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
                        if (limitParsing && BestIndex > LastBestIndex + 28)
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

        private bool RemoveWideAbilityReferences(ref string text, List<AbilityKeyword> modifiedAbilityKeyword)
        {
            int LowestIndex = text.Length;
            string EntryKey = string.Empty;
            List<AbilityKeyword> EntryValue = new List<AbilityKeyword>();

            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
            {
                string SearchText = text;
                List<AbilityKeyword> SearchAbilityKeyword = new List<AbilityKeyword>(modifiedAbilityKeyword);

                if (RemoveWideAbilityReferences(ref SearchText, SearchAbilityKeyword, Entry.Key, Entry.Value, out int indexFound))
                {
                    if (LowestIndex > indexFound)
                    {
                        LowestIndex = indexFound;
                        EntryKey = Entry.Key;
                        EntryValue = Entry.Value;
                    }
                }
            }

            if (EntryValue.Count > 0)
            {
                RemoveWideAbilityReferences(ref text, modifiedAbilityKeyword, EntryKey, EntryValue, out _);
                return true;
            }
            else
                return false;
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
            int Interval = Max - Min + 1;

            string RandomlyDetermined = "(randomly determined)";
            text = $"{Prolog} Add up to {Interval} {Epilog}";

            if (!text.Contains(RandomlyDetermined))
                text += " " + RandomlyDetermined;
        }

        private bool RemoveWideAbilityReferences(ref string text, List<AbilityKeyword> modifiedAbilityKeywordList, string pattern, List<AbilityKeyword> abilityKeywordList, out int indexFound)
        {
            string PatternWithAbilities = pattern + " abilities";
            string PatternWithAbility = pattern + " ability";
            string InputText = text;
            indexFound = text.Length;

            RemoveDecorativeText(ref InputText, PatternWithAbilities, "@", out bool IsRemovedAbilities, ref indexFound);
            RemoveDecorativeText(ref InputText, PatternWithAbility, "@", out bool IsRemovedAbility, ref indexFound);
            RemoveDecorativeText(ref InputText, pattern, "@", out bool IsRemoved, ref indexFound);

            if ((IsRemovedAbilities || IsRemovedAbility) && IsRemoved)
                Debug.WriteLine($"Double remove: {pattern} and {PatternWithAbilities} (or {PatternWithAbility})");
            else if (IsRemovedAbilities && IsRemovedAbility)
                Debug.WriteLine($"Double remove: {PatternWithAbilities} and {PatternWithAbility}");
            else if (IsRemovedAbilities || IsRemovedAbility || IsRemoved)
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

        private void ExtractAbilityNames(Dictionary<string, AbilityKeyword> nameToKeyword, ref string text, out List<AbilityKeyword> extractedAbilityList)
        {
            extractedAbilityList = new List<AbilityKeyword>();

            List<AbilityKeyword> NewExtractedAbilityList = new List<AbilityKeyword>();
            Dictionary<int, string> ExtractedTable = new Dictionary<int, string>();

            foreach (KeyValuePair<string, AbilityKeyword> Entry in nameToKeyword)
            {
                string NewText = text;
                int NewIndex = -1;

                if (ExtractAbilityName(Entry.Key, Entry.Value, ref NewText, NewExtractedAbilityList, ref NewIndex) && !ExtractedTable.ContainsKey(NewIndex))
                    ExtractedTable.Add(NewIndex, Entry.Key);
            }

            if (ExtractedTable.Count > 0)
            {
                List<string> KeyList = new List<string>();
                int LastBestIndex = -1;

                while (ExtractedTable.Count > 0)
                {
                    int BestIndex = -1;
                    foreach (KeyValuePair<int, string> Entry in ExtractedTable)
                        if (BestIndex == -1 || BestIndex > Entry.Key)
                            BestIndex = Entry.Key;

                    if (LastBestIndex != -1 && BestIndex > LastBestIndex + 44)
                        break;

                    string BestString = ExtractedTable[BestIndex];
                    Debug.Assert(nameToKeyword.ContainsKey(BestString));
                    KeyList.Add(BestString);

                    ExtractedTable.Remove(BestIndex);
                    LastBestIndex = BestIndex;
                }

                int UnusedIndex = -1;
                foreach (string Key in KeyList)
                {
                    AbilityKeyword Value = nameToKeyword[Key];
                    ExtractAbilityName(Key, Value, ref text, extractedAbilityList, ref UnusedIndex);
                }
            }
        }

        private bool ExtractAbilityName(string abilityName, AbilityKeyword keyword, ref string text, List<AbilityKeyword> extractedAbilityList, ref int startIndex)
        {
            bool IsExtracted = false;

            if (text.StartsWith($"{abilityName}'s damage "))
            {
                text = text.Substring(abilityName.Length + 3);
                startIndex = 0;
                IsExtracted = true;
            }
            else if (text.StartsWith($"{abilityName}'s "))
            {
                text = text.Substring(abilityName.Length + 3);
                startIndex = 0;
                IsExtracted = true;
            }
            else if (text.StartsWith($"{abilityName} ") || text.StartsWith($"{abilityName},"))
            {
                text = text.Substring(abilityName.Length);
                startIndex = 0;
                IsExtracted = true;
            }
            else
            {
                int Index = text.IndexOf($" {abilityName} ", StringComparison.InvariantCulture);
                if (Index < 0)
                    Index = text.IndexOf($" {abilityName},", StringComparison.InvariantCulture);
                if (Index < 0)
                    Index = text.IndexOf($" {abilityName}.", StringComparison.InvariantCulture);
                if (Index >= 0)
                {
                    startIndex = Index;
                    text = text.Substring(0, Index + 1) + text.Substring(Index + abilityName.Length + 1);
                    IsExtracted = true;
                }
            }

            if (IsExtracted)
            {
                Debug.Assert(!extractedAbilityList.Contains(keyword));
                extractedAbilityList.Add(keyword);

                text = text.Trim();

                return true;
            }
            else
                return false;
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

            ExtractSentence("Place an extra trap", CombatKeyword.AnotherTrap, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target's Critical Hit Chance reduced by %f", CombatKeyword.ReduceCriticalChance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Chance to critically-hit is reduced by %f", CombatKeyword.ReduceCriticalChance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mend a broken bone", CombatKeyword.MendBrokenBone, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Turn while leaping", CombatKeyword.FreeMovementLeaping, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Free-form movement while leaping", CombatKeyword.FreeMovementLeaping, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Repeated castings on same target", CombatKeyword.RepeatedCasting, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Exhilarates on the same target", CombatKeyword.RepeatedCasting, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost Jump Height", CombatKeyword.BoostJumpHeight, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Shuffling their hatred", CombatKeyword.ShuffleTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target ignore you", CombatKeyword.Ignored, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause the target to ignore you", CombatKeyword.Ignored, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Uniformly diminishes all targets' entire aggro lists by %f", CombatKeyword.ChangeTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("This absorbed damage is added to your next @ attack at a %f rate", CombatKeyword.ReturnDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("This absorbed damage is added to your next @", CombatKeyword.ReturnDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your next attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Future @ attack damage %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage of future @ attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Until %f damage is mitigated", CombatKeyword.MitigationLimit, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Up to a maximum of %f total mitigated damage", CombatKeyword.MitigationLimit, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(Randomly determined for each attack)", CombatKeyword.RandomDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(Randomly determined)", CombatKeyword.RandomDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(Random)", CombatKeyword.RandomDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If it is a #D attack", CombatKeyword.IfDamageType, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Briefly terrifies the target", CombatKeyword.Fear, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause all sentient targets to flee in terror", CombatKeyword.FearSentient, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Trigger the target's Vulnerability", CombatKeyword.SetVulnerable, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To Arthropods", CombatKeyword.TargetAnatomyArthropods, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To Undead", CombatKeyword.TargetUndead, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("On an undead target", CombatKeyword.TargetUndead, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Vs Undead", CombatKeyword.TargetUndead, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To Aberration", CombatKeyword.TargetAnatomyAbberation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Vs Elite", CombatKeyword.TargetElite, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To non-Elite target", CombatKeyword.TargetNotElite, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To non-Elite enemies", CombatKeyword.TargetNotElite, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To Vulnerable target", CombatKeyword.TargetVulnerable, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To sentient creatures", CombatKeyword.TargetSentient, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If the target is Vulnerable", CombatKeyword.TargetVulnerable, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If target is Vulnerable", CombatKeyword.TargetVulnerable, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Is used on a Vulnerable target", CombatKeyword.TargetVulnerable, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Both you and your pet", CombatKeyword.ApplyToPetAndMaster, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your pet's", CombatKeyword.ApplyToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your pet gain", CombatKeyword.ApplyToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Give your pet", CombatKeyword.ApplyToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant your pet", CombatKeyword.ApplyToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To your pet", CombatKeyword.ApplyToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To the same target", CombatKeyword.SameTarget, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To YOU", CombatKeyword.TargetSelf, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If target is covered", CombatKeyword.TargetUnderEffect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To targets that are covered", CombatKeyword.TargetUnderEffect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To targets that are Knocked Down", CombatKeyword.TargetKnockedDown, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal #D damage", CombatKeyword.ChangeDamageType, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Instead of Crushing", CombatKeyword.Ignore, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Next attack", CombatKeyword.NextAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Next ability", CombatKeyword.NextAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("For one attack", CombatKeyword.NextAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target's next Rage Attack", CombatKeyword.TargetNextRageAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("The next time they use a Rage attack", CombatKeyword.TargetNextRageAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f of target's attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f of their attack miss and have no effect", CombatKeyword.AddAccuracy, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target's attack", CombatKeyword.TargetSubsequentAttacks, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Ignore Knockback effect", CombatKeyword.IgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Immunity to Knockback", CombatKeyword.IgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant all targets immunity to Knockback", CombatKeyword.IgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant Knockback Immunity", CombatKeyword.IgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Immune to Knockback effects", CombatKeyword.IgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target take %f indirect #D damage", CombatKeyword.AddIndirectVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause the target to take %f damage from indirect #D", CombatKeyword.AddIndirectVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target is %f more vulnerable to #D damage", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target %f more vulnerable to #D damage", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Make the target %f more vulnerable to #D", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause the target to become %f more vulnerable to #D attack", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage target take from #D by %f", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target take %f more damage from #D", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f direct #D damage", CombatKeyword.AddDirectVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f damage from #D attack", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D Vulnerability %f", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f more damage from any #D attack", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f damage from both #D", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f damage from #D", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("But take %f damage from any #D attack", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D Vulnerability +infinity", CombatKeyword.DestroyedByDamageType, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Instantly destroyed by ANY #D Damage", CombatKeyword.DestroyedByDamageType, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Targets suffer %f damage from other #D attack", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f damage to Health and Armor", CombatKeyword.DamageBoostToHealthAndArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage of all targets' attack %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f damage from all attack", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.ApplyToAllies }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Turning half of that into Trauma damage", CombatKeyword.ExtraTraumaDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Pet's #D attack deal %f damage", CombatKeyword.AnimalPetAttackBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Pet's #D attack %f damage", CombatKeyword.AnimalPetAttackBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Rage attack deal %f damage", CombatKeyword.AnimalPetRageAttackBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Pet's Rage Attack Damage %f", CombatKeyword.AnimalPetRageAttackBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal up to %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Add up to %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Add up to %f extra damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f Armor damage", CombatKeyword.DealArmorDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f immediate #D damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f #D damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f fire damage to you", CombatKeyword.SelfImmolation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Pet take %f #D damage", CombatKeyword.PetImmolation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Pet bleed for %f #D damage", CombatKeyword.PetImmolation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause your pet to bleed for %f #D damage", CombatKeyword.PetImmolation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(Debuff cannot stack with itself)", CombatKeyword.NonStackingDebuff, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(This effect does not stack with itself.)", CombatKeyword.NonStackingDebuff, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(This buff does not stack with itself)", CombatKeyword.NonStackingDebuff, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(Stacking up to %f times)", CombatKeyword.StackingDebuffLimit, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(This effect does not stack with itself)", CombatKeyword.NonStackingDebuff, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Deer Bash+Any Melee+Any Melee+Deer Kick:", CombatKeyword.Combo1, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Gripjaw+Any Spider+Any Spider+Inject Venom:", CombatKeyword.Combo2, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Rip+Any Melee+Any Giant Bat Attack+Tear:", CombatKeyword.Combo3, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Screech+Any Giant Bat Attack+Any Melee+Virulent Bite:", CombatKeyword.Combo4, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Rip+Any Melee+Any Melee+Bat Stability:", CombatKeyword.Combo5, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combo: Sonic Burst+Any Giant Bat Attack+Any Ranged Attack+Any Ranged Attack:", CombatKeyword.Combo5, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Final step hit all enemies within %f meter", CombatKeyword.ComboFinalStepBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Final step hit all targets within %f meter", CombatKeyword.ComboFinalStepBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Final step deal %f damage", CombatKeyword.ComboFinalStepDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Final step stun the target and deal %f damage", CombatKeyword.ComboFinalStepDamageAndStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Final step boost base damage %f for 10 second", CombatKeyword.ComboFinalStepBoostBaseDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Whenever you take damage from an enemy", CombatKeyword.ReflectOnAnyAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Each time they attack and damage you", CombatKeyword.ReflectOnAnyAttack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If you are using the #S skill", CombatKeyword.ActiveSkill, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("While #S skill active", CombatKeyword.ActiveSkill, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("While #S skill is active", CombatKeyword.ActiveSkill, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(If #S skill is active)", CombatKeyword.ActiveSkill, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Gain %f #S Skill Base Damage", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You have not been attacked in the past %f second", CombatKeyword.NotAttackedRecently, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If you have less than half of your Health remaining", CombatKeyword.LessThanHalfMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Incubated Spiders %f chance to avoid being hit burst attack", CombatKeyword.SpiderPetAvoidBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Combat Refresh restore %f health", CombatKeyword.CombatRefreshRestoreHeatlth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Healing from Combat Refreshes %f", CombatKeyword.CombatRefreshRestoreHeatlth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the target's #D damage-over-time by %f per tick", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If , , or  deal damage, that damage is boosted %f per tick", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f damage from #D", CombatKeyword.AddVulnerability, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage of your Core and @ %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your @ damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your #D attack damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage of your @ by %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage of your @ %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage from @ %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage of all your attack %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost damage from @ %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the damage from all kicks %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your direct and indirect damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost #D damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage of your ranged attack by %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage of your next attack by %f", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.NextAttack }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage of your @ by %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase the damage of your @ by %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Plus %f Damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#S Base Damage %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your #S Base Damage is %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your #S Base Damage increase %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Base Damage %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Base Damage by %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Base Damage increase %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Causing %f Damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the target's fire damage-over-time by %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Direct #D Damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Universal Indirect Damage %f", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost targets' indirect damage %f", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Indirect #D Damage %f", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Indirect #D %f per tick", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage is %f per tick", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage over Time %f per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage over Time deal %f damage per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage over Time deal %f per tick", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.DamageOverTime }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Base Damage is %f", CombatKeyword.BaseDamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage is %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage is %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage is boosted %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Suffer %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reap %f of the damage to you as healing", CombatKeyword.DrainHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reap %f of the damage done", CombatKeyword.DrainHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reap %f health", CombatKeyword.DrainHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Melee Attackers suffer %f indirect damage", CombatKeyword.ReflectMeleeIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Up to a max of %f", CombatKeyword.MaxOccurence, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("The reap cap is %f", CombatKeyword.DrainMax, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f direct damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f damage to Health", CombatKeyword.DealDirectHealthDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f direct health damage", CombatKeyword.DealDirectHealthDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("+Up to %f extra damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f random damage", new List<CombatKeyword>() { CombatKeyword.DamageBoost, CombatKeyword.RandomDamage }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("All attack deal %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Nice attack deal %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Core attack deal %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f indirect damage", CombatKeyword.DealIndirectDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);//again
            ExtractSentence("Dealing %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Over %f second", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lasts %f second", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("For %f second after using ", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("For %f second", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Within %f second", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(%f second)", CombatKeyword.EffectDuration, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("For %f minute", CombatKeyword.EffectDurationMinute, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("After a %f second delay", CombatKeyword.EffectDelay, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("After an %f second delay", CombatKeyword.EffectDelay, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("After %f second", CombatKeyword.EffectDelay, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Every %f second", CombatKeyword.EffectRecurrence, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("With each heal", CombatKeyword.EffectRecurrence, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Remove (up to) %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce Rage %f", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce %f more Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce target's Rage by %f", CombatKeyword.AddRage, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Generate %f Rage", CombatKeyword.AddRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Generate %f less Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lower Rage by %f", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lower Rage %f", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Remove %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lose %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deplete %f Rage", CombatKeyword.AddRage, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Generate no Rage", CombatKeyword.ZeroRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Raise the target's Max Rage by %f", CombatKeyword.IncreaseMaxRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Raise target's Max Rage by %f", CombatKeyword.IncreaseMaxRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase target's Max Rage by%f", CombatKeyword.IncreaseMaxRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Generate no Taunt", CombatKeyword.ZeroTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Power Cost %f", CombatKeyword.AddPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Power Cost is %f", CombatKeyword.AddPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the Power cost of your @ %f", CombatKeyword.AddPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the Power cost of your next @ by %f", new List<CombatKeyword>() { CombatKeyword.AddPowerCost, CombatKeyword.NextUse }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the Power cost of @ %f", CombatKeyword.AddPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("In-Combat Armor Regeneration %f", CombatKeyword.AddArmorRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Armor Regeneration (in-combat) %f", CombatKeyword.AddArmorRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Armor Regeneration", CombatKeyword.AddArmorRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Recover %f Armor every five second", CombatKeyword.AddArmorRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Power Regeneration is %f", CombatKeyword.AddPowerRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cost %f Power", CombatKeyword.AddPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Regain %f Power", CombatKeyword.AddPowerRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("The maximum Power restored  increase %f", CombatKeyword.AddPowerCostMax, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Max Armor %f", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Gain %f Armor", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase your Max Health by %f", CombatKeyword.AddMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase your Max Armor by %f", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reuse Time %f second", CombatKeyword.AddReuseTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reuse Time is %f second", CombatKeyword.AddReuseTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reuse Time is %f sec", CombatKeyword.AddReuseTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reuse Time is %f", CombatKeyword.AddReuseTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reuse Time %f second", CombatKeyword.AddReuseTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten the current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten the current reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten the remaining reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten your current Combat Refresh delay by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Hasten your current Combat Refresh time by %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Shorten the remaining reset time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Shorten the current reuse time of @ by %f second", CombatKeyword.AddReuseTimer, SignInterpretation.AlwaysNegative, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reset time of @ is increased %f second", CombatKeyword.AddCombatRefreshTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the taunt of all your attack by %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunt %f", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunted %f", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunt as if they did %f more damage", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunt as if they did %f damage", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunt their opponents %f less", CombatKeyword.AddTaunt, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Taunt of all your attack %f", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Taunt", CombatKeyword.AddTaunt, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("When you have %f or less of your Armor left", CombatKeyword.BelowArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Have less than %f of their Armor", CombatKeyword.BelowArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Health, Armor, and Power respectively", CombatKeyword.RestoreHealthArmorPower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Health, Armor, and Power", CombatKeyword.RestoreHealthArmorPower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Health/Armor healing", CombatKeyword.RestoreHealthArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you for %f Health/Armor", new List<CombatKeyword>() { CombatKeyword.RestoreHealthArmor, CombatKeyword.TargetSelf }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you for %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal your pet for %f Health/Armor", CombatKeyword.RestoreHealthArmorToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You regain %f Health", new List<CombatKeyword>() { CombatKeyword.RestoreHealth, CombatKeyword.TargetSelf }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Health/Armor to your pet", CombatKeyword.RestoreHealthArmorToPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Health/Armor", CombatKeyword.RestoreHealthArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f of your Max Health", CombatKeyword.RestoreMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Healing Abilities %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the healing of your @ %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you for %f of your Max Health", CombatKeyword.RestoreMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal all targets for %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You regain %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You regain %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Basic attack restore %f Power", CombatKeyword.RestorePower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Power", CombatKeyword.RestorePower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f to you", CombatKeyword.RestoreAny, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Recover %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Recover %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Recover %f power", CombatKeyword.RestorePower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restoration %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You regain %f power", CombatKeyword.RestorePower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cost no Power to cast", CombatKeyword.ZeroPowerCost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f second to channel", CombatKeyword.AddChannelTime, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost the healing from your @ %f", CombatKeyword.TargetAbilityBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you for %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you for %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal %f health", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Healing %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal you %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Heal %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f Body Heat", CombatKeyword.RestoreBodyHeat, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Sprint Speed is %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Sprint Speed increase by %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Sprint Speed", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Max Health %f", CombatKeyword.AddMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Max Health by %f", CombatKeyword.AddMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Max Health", CombatKeyword.AddMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Max Armor %f", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Max Armor", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Have %f Armor", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To your minions", CombatKeyword.ApplyToNecroPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Attack Range is %f", CombatKeyword.AddRange, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Range is %f meter", CombatKeyword.AddRange, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stun you", CombatKeyword.SelfStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Complete stun immunity", CombatKeyword.StunImmunity, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant immunity to new stun", CombatKeyword.StunImmunity, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant them immunity to stun", CombatKeyword.StunImmunity, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant immunity to new slow and root", CombatKeyword.SlowRootImmunity, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant them immunity to Slow and Root", CombatKeyword.SlowRootImmunity, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Dispel stun", CombatKeyword.RemoveStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Dispel any Stun", CombatKeyword.RemoveStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Dispel slow and root", CombatKeyword.RemoveSlowRoot, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Dispel any Slow or Root", CombatKeyword.RemoveSlowRoot, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target is prone to random self-stuns", CombatKeyword.Concussion, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stun targets", CombatKeyword.Stun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stun incorporeal enemies", CombatKeyword.StunIncorporeal, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stun", CombatKeyword.Stun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Targets are Knock back", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock back targets", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock all targets back", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock the target backward", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock the enemy backward", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock the target back", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock target backward", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock targets backward", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Knock them backward", CombatKeyword.Knockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reset the time on", CombatKeyword.ResetOtherAbilityTimer, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal %f total damage against Demons", CombatKeyword.DamageBoostAgainstSpecie, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost targets' mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant %f Universal #D Mitigation", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Universal Damage Mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Reduce the damage you take from #D attack by %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Take %f less damage from all attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target take %f less damage from attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target take %f less damage from #D attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target to take %f less damage from attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target to take %f less damage from #D attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigate %f of all damage", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant your pet %f mitigation versus direct attack", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.ApplyToPet }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your pet gain %f mitigation versus direct attack", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.ApplyToPet }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Up to %f direct damage mitigation", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigation vs Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigation vs all attack by Elites %f", new List<CombatKeyword>() { CombatKeyword.AddMitigation, CombatKeyword.TargetElite }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigation vs physical damage %f", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Physical Damage Mitigation %f", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f absorption of any physical damage", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Any internal (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Any physical (#D) attack that hit you are reduced by %f", CombatKeyword.AddMitigationInternal, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Universal Indirect Mitigation %f", CombatKeyword.AddMitigationIndirect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigate all damage over time by %f per tick", CombatKeyword.AddMitigationIndirect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("when armor is empty, up to %f when armor is full", new List<CombatKeyword>() { CombatKeyword.VariableMitigation, CombatKeyword.ApplyToPet }, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stacks up to %f times", CombatKeyword.MaxStack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Stacks up to %fx", CombatKeyword.MaxStack, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("All Shield abilities", CombatKeyword.ApplyToAbilitiesShield, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Grant allies", CombatKeyword.ApplyToAllies, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To all allies", CombatKeyword.ApplyToAllies, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("And your allies' attack", CombatKeyword.ApplyToAllies, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f evasion of burst attack", CombatKeyword.AddEvasionBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Burst Evasion %f", CombatKeyword.AddEvasionBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Burst Evasion", CombatKeyword.AddEvasionBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost Burst Evasion by %f", CombatKeyword.AddEvasionBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Projectile Evasion %f", CombatKeyword.AddEvasionProjectile, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Projectile Evasion", CombatKeyword.AddEvasionProjectile, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Melee Evasion %f", CombatKeyword.AddEvasionMelee, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Melee Evasion", CombatKeyword.AddEvasionMelee, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation of all physical attack", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation of any physical damage", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation against physical attack", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation from direct attack", CombatKeyword.AddMitigationDirect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation against all attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Mitigation from all attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Mitigation from attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation vs #D", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase your Mitigation vs #D attack %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f mitigation from #D attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Direct #D mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f direct damage mitigation", CombatKeyword.AddMitigationDirect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your direct damage mitigation %f", CombatKeyword.AddMitigationDirect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause all targets to suffer %f damage from direct #D attack", CombatKeyword.AddMitigation, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You take %f damage from #D attack", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f #D mitigation", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase your #D Mitigation %f", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Damage Mitigation", CombatKeyword.AddMitigation, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigate %f of all Slashing/Crushing/Piercing damage", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Mitigate %f of all physical damage", CombatKeyword.AddMitigationPhysical, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Cold Protection (Direct and Indirect)", CombatKeyword.AddProtectionCold, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Direct and Indirect Cold Protection", CombatKeyword.AddProtectionCold, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Remove ongoing #D effects (up to %f dmg/sec)", CombatKeyword.RemoveEffects, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Chance to Ignore Knockbacks %f", CombatKeyword.AddChanceToIgnoreKnockback, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f chance to ignore Stun", CombatKeyword.AddChanceToIgnoreStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Chance to ignore Stun %f", CombatKeyword.AddChanceToIgnoreStun, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Targets whose Rage meter are at least %f full", CombatKeyword.AboveRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Targets whose Rage meter is at least %f full", CombatKeyword.AboveRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If target's Rage is at least %f full", CombatKeyword.AboveRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If target's Rage meter is at least %f full", CombatKeyword.AboveRage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f chance to Knock Down", CombatKeyword.AddChanceToKnockdown, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("There's a %f chance", CombatKeyword.ApplyWithChance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f chance to", CombatKeyword.ApplyWithChance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("When wielding two knives", CombatKeyword.RequireTwoKnives, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If the target is not focused on you", CombatKeyword.RequireNoAggro, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If they are not focused on you", CombatKeyword.RequireNoAggro, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If target is not focused on you", CombatKeyword.RequireNoAggro, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To all melee attackers", CombatKeyword.ApplyToMeleeReflect, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("The first melee attacker is knocked away", CombatKeyword.ReflectKnockbackOnFirstMelee, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("When a melee attack deal damage to you", CombatKeyword.ReflectOnMelee, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal its damage when you are hit burst attack", CombatKeyword.ReflectOnBurst, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Deal its damage when you are hit ranged attack", CombatKeyword.ReflectOnRanged, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("A melee attack deal damage to you", CombatKeyword.ReflectOnMelee, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("In addition, you can use the ability  %f", CombatKeyword.EnableOtherAbility, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("In addition, you can use the ability", CombatKeyword.EnableOtherAbility, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Chance to consume grass is %f", CombatKeyword.ChanceToConsume, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You regenerate %f Health per tick (every 5 second, in and out of combat)", CombatKeyword.AddHealthRegen, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Summoned Deer attack", CombatKeyword.ApplyToDeerPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Summoned Deer", CombatKeyword.ApplyToDeerPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Have %f health", CombatKeyword.AddMaxHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Have %f armor", CombatKeyword.AddMaxArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Incubated Spiders", CombatKeyword.ApplyToSpiderPet, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Per second", CombatKeyword.Recurring, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Steal %f health", CombatKeyword.DrainHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Steal %f more health", CombatKeyword.DrainHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Ability's range is reduced to %fm", CombatKeyword.AddRange, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Chance to consume carrot is %f", CombatKeyword.ChanceToConsume, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lower aggro toward you %f", CombatKeyword.AddTaunt, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Attack range %f meter", CombatKeyword.AddRange, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Until you trigger the teleport", CombatKeyword.UntilTrigger, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Until you Feint", CombatKeyword.UntilTrigger, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your movement speed by %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost movement speed by %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Increase your movement speed by %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Out of Combat Sprint Speed %f", CombatKeyword.AddOutOfCombatSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Out of Combat Sprint Speed", CombatKeyword.AddOutOfCombatSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your Out of Combat Sprint speed %f", CombatKeyword.AddOutOfCombatSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Your Out of Combat Sprint speed by %f", CombatKeyword.AddOutOfCombatSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Speed is %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Movement speed %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Sprint speed %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Sprint speed by %f", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Movement Speed", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Sprint Speed", CombatKeyword.AddSprintSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Fly speed is boosted %f", CombatKeyword.AddFlySpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Fly Speed %f", CombatKeyword.AddFlySpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Swim Speed %f", CombatKeyword.AddSwimSpeed, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Slow target's movement by %f", CombatKeyword.Slow, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Slow target's movement speed by %f", CombatKeyword.Slow, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Give you %f Accuracy", CombatKeyword.AddAccuracy, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Melee Accuracy %f", CombatKeyword.AddMeleeAccuracy, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Accuracy %f", CombatKeyword.AddAccuracy, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Accuracy", CombatKeyword.AddAccuracy, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f projectile evasion", CombatKeyword.AddProjectileEvasion, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f melee evasion", CombatKeyword.AddMeleeEvasion, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost melee evasion %f", CombatKeyword.AddMeleeEvasion, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Lower targets' Evasion by %f", CombatKeyword.RemoveEvasion, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f more chance of missing", CombatKeyword.AddAccuracy, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Miss Chance", CombatKeyword.AddAccuracy, SignInterpretation.Opposite, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Physical Damage Reflection", CombatKeyword.AddPhysicalReflection, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f resistance to #D damage", CombatKeyword.AddDamageResistance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D Resistance %f", CombatKeyword.AddDamageResistance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("You are %f resistant to #D damage", CombatKeyword.AddDamageResistance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Within %f meter", CombatKeyword.WithinDistance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f resistance to Elemental damage (Fire, Cold, Electricity)", CombatKeyword.AddElementalDamageResistance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Worth %f more XP", CombatKeyword.IncreaseXPGain, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Earned Combat XP", CombatKeyword.IncreaseXPGain, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Slain within %f second", CombatKeyword.MaxKillTime, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Buff targets' direct #D damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Direct Damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f #D damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f health damage", CombatKeyword.DealDirectHealthDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f #D health damage", CombatKeyword.DealDirectHealthDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Direct #D Damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Health and Armor damage", CombatKeyword.DamageBoostToHealthAndArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Direct Damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f damage", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Damage %f", CombatKeyword.DamageBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f armor damage", CombatKeyword.DealArmorDamage, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f @", CombatKeyword.TargetAbilityBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("@ boost %f", CombatKeyword.TargetAbilityBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Boost your @ %f", CombatKeyword.TargetAbilityBoost, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Until you are attacked", CombatKeyword.UntilAttacked, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("(for all targets)", CombatKeyword.ApplyToAllies, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target does not yell for help because of this attack", CombatKeyword.NoYellForHelp, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Doesn't cause the target to yell for help", CombatKeyword.NoYellForHelp, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("This attack does not cause the target to shout for help", CombatKeyword.NoYellForHelp, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Does not cause the target to shout for help", CombatKeyword.NoYellForHelp, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("If it is a Werewolf ability", CombatKeyword.IfWerewolf, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("To the kicker", CombatKeyword.ToKickerTarget, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Cause kicks", CombatKeyword.ToKickerTarget, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Randomly repair broken bones twice as often", CombatKeyword.RepairBrokenBone, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("And %f armor", CombatKeyword.RestoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("And %f power", CombatKeyword.RestorePower, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Restore %f", CombatKeyword.RestoreHealth, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Enthusiasm", CombatKeyword.AddEnthusiasm, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Death Avoidance", CombatKeyword.AddDeathAvoidance, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target suffer a second blast of #D damage", CombatKeyword.SecondBlast, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Target take a second full blast of delayed #D damage", CombatKeyword.SecondBlast, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("#D damage no longer dispel", CombatKeyword.NoDispel, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("Ignores mitigation from armor", CombatKeyword.IgnoreArmor, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);
            ExtractSentence("%f Body Heat", CombatKeyword.RestoreBodyHeat, SignInterpretation.Normal, skippedKeywordList, text, ref ModifiedText, ExtractedKeywordList, ref Data1, ref Data2, ref DamageType, ref CombatSkill, ref ParsedIndex);

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

                return true;
            }
            else
                return false;
        }

        private void ExtractSentence(string format, CombatKeyword associatedKeyword, SignInterpretation signInterpretation, List<CombatKeyword> skippedKeywordList, string text, ref string modifiedText, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
        {
            ExtractSentence(format, new List<CombatKeyword>() { associatedKeyword }, signInterpretation, skippedKeywordList, text, ref modifiedText, extractedKeywordList, ref data1, ref data2, ref damageType, ref combatSkill, ref parsedIndex);
        }

        private void ExtractSentence(string format, List<CombatKeyword> associatedKeywordList, SignInterpretation signInterpretation, List<CombatKeyword> skippedKeywordList, string text, ref string modifiedText, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
        {
            string NewText = text;
            List<CombatKeyword> NewExtractedKeywordList = new List<CombatKeyword>();
            NumericValue NewData1 = new NumericValue();
            NumericValue NewData2 = new NumericValue();
            GameDamageType NewDamageType = GameDamageType.None;
            GameCombatSkill NewCombatSkill = GameCombatSkill.None;
            int NewParsedIndex = -1;

            bool IsExtracted = ExtractNewSentence(format, associatedKeywordList, signInterpretation, skippedKeywordList, ref NewText, NewExtractedKeywordList, ref NewData1, ref NewData2, ref NewDamageType, ref NewCombatSkill, ref NewParsedIndex);

            if (!IsExtracted)
                return;

            if (parsedIndex < 0 || NewParsedIndex < parsedIndex)
            {
                modifiedText = NewText;
                extractedKeywordList.Clear();
                extractedKeywordList.AddRange(NewExtractedKeywordList);
                data1 = NewData1;
                data2 = NewData2;
                damageType = NewDamageType;
                combatSkill = NewCombatSkill;
                parsedIndex = NewParsedIndex;
            }
        }

        private bool ExtractNewSentence(string format, List<CombatKeyword> associatedKeywordList, SignInterpretation signInterpretation, List<CombatKeyword> skippedKeywordList, ref string text, List<CombatKeyword> extractedKeywordList, ref NumericValue data1, ref NumericValue data2, ref GameDamageType damageType, ref GameCombatSkill combatSkill, ref int parsedIndex)
        {
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
                                if (BestFoundIndex == -1 || FoundIndex < BestFoundIndex + 15)
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

        public static readonly Dictionary<int, string> DamageTypeTextMap = new Dictionary<int, string>()
        {
            { (int)GameDamageType.None, "None" },
            /*{ (int)GameDamageType.PsychicNature, "Psychic and Nature" },
            { (int)GameDamageType.ElectricityAcidNature, "Electricity, Acid, and Nature" },
            { (int)GameDamageType.PsychicElectricityFire, "Psychic, Electricity, or Fire" },
            { (int)GameDamageType.ColdFireElectricity, "Cold, Fire, and Electricity" },
            { (int)GameDamageType.CrushingSlashingPiercing, "Crushing, Slashing, or Piercing" },
            { (int)GameDamageType.SlashingPiercing, "Slashing and Piercing" },*/
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

        private string CleanedUpText(string text)
        {
            bool IsCleanedUp;
            do
            {
                IsCleanedUp = false;
                text = CleanedUpText(text, "and", ref IsCleanedUp);
                text = CleanedUpText(text, "cause", ref IsCleanedUp);
                text = CleanedUpText(text, "the", ref IsCleanedUp);
                text = CleanedUpText(text, "also", ref IsCleanedUp);
                text = CleanedUpText(text, "have a", ref IsCleanedUp);
            }
            while (IsCleanedUp);

            text = GrammarChanged(text);

            return text;
        }

        private string CleanedUpText(string text, string pattern, ref bool isCleanedUp)
        {
            if (text.ToLowerInvariant().StartsWith(pattern.ToLowerInvariant() + " "))
            {
                isCleanedUp = true;
                return text.Substring(pattern.Length).Trim();
            }
            else
                return text;
        }

        private string GrammarChanged(string text)
        {
            foreach (KeyValuePair<string, List<AbilityKeyword>> Entry in WideAbilityTable)
                text = GrammarChangedWithPlural(text, Entry.Key);

            return text;
        }

        private string GrammarChangedWithPlural(string text, string pattern)
        {
            Debug.Assert(pattern.Length > 1);

            if (pattern.EndsWith("s"))
                text = GrammarChanged(text, pattern.Substring(0, pattern.Length - 1));

            text = GrammarChanged(text, pattern);

            return text;
        }

        private string GrammarChanged(string text, string pattern)
        {
            text = GrammarChanged(text, pattern, "boost your ", " damage", "", " deal");

            return text;
        }

        private string GrammarChanged(string text, string pattern, string patternProlog, string patternEpilog, string replacementProlog, string replacementEpilog)
        {
            string LowerText = text.ToLowerInvariant();
            string PatternString = $"{patternProlog}{pattern.ToLowerInvariant()}{patternEpilog}";
            int Index = LowerText.IndexOf(PatternString);

            if (Index >= 0)
                return text.Substring(0, Index) + $"{replacementProlog}{pattern}{replacementEpilog}" + text.Substring(Index + PatternString.Length);
            else
                return text;
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
        #endregion

        #region Data Analysis, Remaining
        /*
        private void AnalyzeRemainingEffects(List<string> abilityNameList, List<IPgPower> powerSimpleEffectList)
        {
            List<IPgPower> ParsedSimpleEffectList = new List<IPgPower>();

            foreach (IPgPower Item in powerSimpleEffectList)
            {
                if (PerfectMatch.ContainsKey(Item.ToString()))
                    continue;

                if (Item.ToString().EndsWith("and reduce the damage of the next attack that hits the target by 20%"))
                    continue; // effect_14323
                if (Item.ToString() == "Molten Veins causes any nearby Fire Walls to recover 117 health")
                    continue; // Increase Fire Wall heal displayed
                if (Item.ToString() == "While Unarmed skill is active: you gain +4% Melee Evasion and any time you Evade a Melee attack you recover 64 Armor")
                    continue; // Increase evasion and armor regen
                if (Item.ToString() == "While Unarmed skill is active: any time you Evade an attack, your next attack deals +133 damage")
                    continue; // special effect
                if (Item.ToString() == "While Unarmed skill is active: 18% of all Slashing, Piercing, and Crushing damage you take is mitigated and added to the damage done by your next Punch, Jab, or Infuriating Fist at a 260% rate")
                    continue; // special effect
                if (Item.ToString() == "While Unarmed skill is active: 25% of all Darkness and Psychic damage you take is mitigated and added to the damage done by your next Punch, Jab, or Infuriating Fist at a 300% rate")
                    continue; // special effect
                if (Item.ToString() == "While Unarmed skill active: 21% of all Acid, Poison, and Nature damage you take is mitigated and added to the damage done by your next Kick at a 280% rate")
                    continue; // special effect
                if (Item.ToString() == "Combo: Suppress+Any Melee+Any Melee+Headcracker: final step stuns the target while dealing +200 damage.")
                    continue; // special effect
                if (Item.ToString() == "You heal 22 health every other second while under the effect of Haste Concoction")
                    continue; // special effect
                if (Item.ToString() == "You heal 16 health and 16 armor every other second while under the effect of Haste Concoction")
                    continue; // special effect
                if (Item.ToString() == "You regain 8 Power every other second while under the effect of Haste Concoction")
                    continue; // special effect
                if (Item.ToString() == "While the Shield skill is active, you mitigate 1 point of attack damage for every 20 Armor you have remaining. (Normally, you would mitigate 1 for every 25 Armor remaining.)")
                    continue; // special effect
                if (Item.ToString() == "When you are hit by a monster's Rage Attack, the current reuse timer of Stunning Bash is hastened by 1 second and your next Stunning Bash deals +80 damage")
                    continue; // special effect

                if (IsExtractable(abilityNameList, Item))
                    continue;

                ParsedSimpleEffectList.Add(Item);
            }
        }

        private bool IsExtractable(List<string> abilityNameList, IPgPower power)
        {
            foreach (IPgPowerTier Item in power.TierEffectList)
                if (!IsExtractable(abilityNameList, Item))
                    return false;

            return true;
        }

        private bool IsExtractable(List<string> abilityNameList, IPgPowerTier powerTier)
        {
            foreach (IPgPowerEffect Item in powerTier.EffectList)
                if (Item is IPgPowerSimpleEffect AsSimpleEffect)
                    if (!IsExtractable(abilityNameList, AsSimpleEffect))
                        return false;

            return true;
        }

        private bool IsExtractable(List<string> abilityNameList, IPgPowerSimpleEffect simpleEffect)
        {
            string Text = simpleEffect.Description;

            RemoveDecorationText(ref Text);
            ExtractAbilityName(abilityNameList, ref Text, out bool IsNameExtracted);
            ExtractAllKnownAttributes(ref Text, out bool IsAttributeExtracted);
            RemoveUnusedText(ref Text);

            if (IsNameExtracted)
                IsNameExtracted = true;

            if (Text.Length == 0)
                return true;

            return false;
        }

        private void ExtractAbilityName(List<string> abilityNameList, ref string text, out bool isExtracted)
        {
            isExtracted = false;

            foreach (string Name in abilityNameList)
                ExtractAbilityName(Name, ref text, ref isExtracted);

            ExtractAbilityName("Unarmed attacks", ref text, ref isExtracted);
            ExtractAbilityName("All bomb attacks", ref text, ref isExtracted);
            ExtractAbilityName("All Psi Wave Abilities", ref text, ref isExtracted);
            ExtractAbilityName("Hammer attacks", ref text, ref isExtracted);
            ExtractAbilityName("All Druid abilities", ref text, ref isExtracted);
            ExtractAbilityName("Knife abilities with 'Cut' in their name", ref text, ref isExtracted);
            ExtractAbilityName("all Knife abilities WITHOUT 'Cut' in their name", ref text, ref isExtracted);
            ExtractAbilityName("all Knife Fighting attacks", ref text, ref isExtracted);
            ExtractAbilityName("Bard Songs", ref text, ref isExtracted);
            ExtractAbilityName("All Major Healing abilities targeting you", ref text, ref isExtracted);
            ExtractAbilityName("All Bun-Fu moves", ref text, ref isExtracted);
        }

        private void ExtractAbilityName(string abilityName, ref string text, ref bool isExtracted)
        {
            if (text.StartsWith($"{abilityName}'s damage "))
            {
                text = text.Substring(abilityName.Length + 3);
                isExtracted = true;
            }
            else if (text.StartsWith($"{abilityName}'s "))
            {
                text = text.Substring(abilityName.Length + 3);
                isExtracted = true;
            }
            else if (text.StartsWith($"{abilityName} ") || text.StartsWith($"{abilityName},"))
            {
                text = text.Substring(abilityName.Length);
                isExtracted = true;
            }
            else
            {
                int Index = text.IndexOf($" {abilityName} ", StringComparison.InvariantCulture);
                if (Index < 0)
                    Index = text.IndexOf($" {abilityName},", StringComparison.InvariantCulture);
                if (Index < 0)
                    Index = text.IndexOf($" {abilityName}.", StringComparison.InvariantCulture);
                if (Index >= 0)
                {
                    text = text.Substring(0, Index + 1) + text.Substring(Index + abilityName.Length + 1);
                    isExtracted = true;
                }
            }
        }

        private void ExtractAllKnownAttributes(ref string text, out bool isExtracted)
        {
            ExtractKnownAttribute(ref text, false, out isExtracted);
        }

        private void ExtractKnownAttribute(ref string text, bool stopIfExtracted, out bool isExtracted)
        {
            isExtracted = false;

            RemoveDecorativeText(ref text, out bool IsDecorativeTextRemoved);
            SimplifyBonus(ref text);
            SimplifySemantic(ref text);
            SimplifyGrammar(ref text);
            ExtractDamageChange(ref text);
            SimplifyDamageType(ref text);
            SimplifyMitigrationType(ref text, ref isExtracted);
            ExtractSentence(ref text, "Combo: Deer Bash+Any Melee+Any Melee+Deer Kick:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combo: Gripjaw+Any Spider+Any Spider+Inject Venom:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combo: Rip+Any Melee+Any Giant Bat Attack+Tear:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combo: Screech+Any Giant Bat Attack+Any Melee+Virulent Bite:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combo: Rip+Any Melee+Any Melee+Bat Stability:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combo: Sonic Burst+Any Giant Bat Attack+Any Ranged Attack+Any Ranged Attack:", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Final step hit all enemies within %f meter", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Final step deal %f% damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Final step hit all targets within %f meter", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Final step stun the target and deal %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Final step boost base damage %f% for 10 second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Whenever you take damage from an enemy", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "If you are using the Priest skill", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "You have not been attacked in the past %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Each time they attack and damage you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Incubated Spiders %f% chance to avoid being hit burst attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Combat Refresh restore %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "If , , or  deal damage, that damage is boosted %f% per tick", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Boost your Nice Attack damage %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Boost your direct and indirect damage %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Plus %f Damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Base Damage %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Causing %f Damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage is %f% per tick", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage is %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Damage is boosted %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Suffer %f% damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reap %f% of the damage to you as healing", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reap %d% of the damage done", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Melee Attackers suffer %d indirect damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Up to a max of %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "The reap cap is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal %f% damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal %f damage to health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "All attacks deal %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Nice attacks deal %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Core attacks deal %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal %f damage", stopIfExtracted, ref isExtracted, out _);//again
            ExtractSentence(ref text, "Deal %f indirect damage", stopIfExtracted, ref isExtracted, out _);//again
            ExtractSentence(ref text, "Dealing %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Cause %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Take %f damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Over %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "For %f second after using ", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "For %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Within %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "For %f minute", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "After a %f second delay", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reduce Rage %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reduce %f more Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Generate %f Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Generate %f% Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Generate %f less Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Lower Rage by %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Lower Rage %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Remove %f Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Lose %f Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deplete %f Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Generate no Rage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Generate no Taunt", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Power Cost %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Power Cost is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Power Regeneration is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Cost %f% Power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Cost %f Power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "The maximum Power restored  increase %d", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Max Armor %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Timer %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Timer is %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Timer is %f sec", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Timer is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Time is %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Reuse Time %f second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Taunt %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Taunt %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "When you have %f% or less of your Armor left", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f Health, Armor, and Power respectively", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f Health, Armor, and Power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f Health/Armor", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f armor", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f armor", stopIfExtracted, ref isExtracted, out _);//again
            ExtractSentence(ref text, "Basic attacks restore %f Power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f Power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Restore %f to you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Recover %f armor", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Recover %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Recover %f power", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Cost no Power to cast", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Take %f second to channel", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Heal %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Healing %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Healing %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Heal %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Sprint Speed is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Max Health %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "To your minions", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Attack Range is %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Range is %f meter", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Stun incorporeal enemies", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Resets the timer on", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal %f% total damage against Demons", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "While Shield skill active", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "While Warden skill active", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "While Cow skill active", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Mitigate %f% of all damage", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Stacks up to %f times", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Stacks up to %fx", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "All Shield abilities", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Grant allies", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "%f% evasion of burst attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "%f% mitigation of all physical attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "%f mitigation of all physical attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Chance to Ignore Knockbacks %d%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Chance to Ignore Stun %d%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "To targets whose Rage meter are at least %d% full", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Targets whose Rage meter is at least %f% full", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "%f% chance to Knock Down", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "%f% chance to", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "When wielding two knives", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "If the target is not focused on you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "If target is not focused on you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "To all melee attackers", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "The first melee attacker is knocked away", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "When a melee attack deal damage to you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal its damage when you are hit burst attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Deal its damage when you are hit ranged attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "A melee attack deal damage to you", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "In addition, you can use the ability  %d", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "In addition, you can use the ability", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Chance to consume grass is %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "You regenerate %f Health per tick (every 5 second, in and out of combat)", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Summoned Deer have %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Summoned Deer have %f armor", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Summoned Deer attacks", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Summoned Deer", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Incubated Spiders have %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Incubated Spiders have %f armor", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Incubated Spiders", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Per second", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "While Spider skill is active", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Steals %f health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Steals %f more health", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Ability's range is reduced to %fm", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Chance to consume carrot is %f%", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Lower aggro toward you %f", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Attack range %f meter", stopIfExtracted, ref isExtracted, out _);
            ExtractSentence(ref text, "Debuff cannot stack with itself", stopIfExtracted, ref isExtracted, out _);
        }

        private void RemoveDecorativeText(ref string text, out bool isRemoved)
        {
            RemoveDecorativeText(ref text, "(Equipping this item will teach you the ability if needed.)", out isRemoved);
        }

        private void SimplifyBonus(ref string text)
        {
            ReplaceCaseInsensitive(ref text, " +", " ");
            ReplaceCaseInsensitive(ref text, " -", " ");
        }

        private void SimplifySemantic(ref string text)
        {
            ReplaceCaseInsensitive(ref text, " target's ", " ");
            ReplaceCaseInsensitive(ref text, " is increased ", " ");
            ReplaceCaseInsensitive(ref text, " increased ", " ");
            ReplaceCaseInsensitive(ref text, " an additional ", " ");
            ReplaceCaseInsensitive(ref text, " additional ", " ");
            ReplaceCaseInsensitive(ref text, " sooner", " ");
            ReplaceCaseInsensitive(ref text, " faster", " ");
            ReplaceCaseInsensitive(ref text, " by ", " ");
            ReplaceCaseInsensitive(ref text, " causes your minions to ", " ");
            ReplaceCaseInsensitive(ref text, " causes the target to ", " ");
            ReplaceCaseInsensitive(ref text, " raises ", " ");
            ReplaceCaseInsensitive(ref text, " all allies ", " allies ");
            ReplaceCaseInsensitive(ref text, " to all targets", " ");
            ReplaceCaseInsensitive(ref text, " have a ", " ");
            ReplaceCaseInsensitive(ref text, "after an ", "after a ");
            ReplaceCaseInsensitive(ref text, "-second", " second");
        }

        string[] DamageNames = new string[]
        {
            "Crushing",
            "Slashing",
            "Nature",
            "Fire",
            "Cold",
            "Piercing",
            "Psychic",
            "Trauma",
            "Electricity",
            "Poison",
            "Acid",
            "Darkness",
            "Werewolf",
            "Bard",
            "Giant Bat"
        };

        private void ExtractDamageChange(ref string text)
        {
            foreach (string Item in DamageNames)
                ExtractDamageChange(ref text, Item);
        }

        private void ExtractDamageChange(ref string text, string damageType)
        {
            ReplaceCaseInsensitive(ref text, $"damage become {damageType.ToLowerInvariant()} instead of crushing", " ");
            ReplaceCaseInsensitive(ref text, $"damage become {damageType.ToLowerInvariant()} instead of electricity", " ");
            ReplaceCaseInsensitive(ref text, $"damage type become {damageType.ToLowerInvariant()}", " ");
            ReplaceCaseInsensitive(ref text, $"damage type is {damageType.ToLowerInvariant()} instead of trauma", " ");
            ReplaceCaseInsensitive(ref text, $"deal {damageType.ToLowerInvariant()} damage instead of psychic", " ");
            ReplaceCaseInsensitive(ref text, $"deal {damageType.ToLowerInvariant()} damage (instead of nature)", " ");
            ReplaceCaseInsensitive(ref text, $"deal {damageType.ToLowerInvariant()} damage (instead of crushing)", " ");
            ReplaceCaseInsensitive(ref text, $"deal {damageType.ToLowerInvariant()} damage (instead of psychic)", " ");
            ReplaceCaseInsensitive(ref text, $"damage type is changed to {damageType.ToLowerInvariant()}", " ");
        }

        private void SimplifyDamageType(ref string text)
        {
            SimplifyDamageType(ref text, "indirect poison and indirect trauma");
            SimplifyDamageType(ref text, "indirect nature and indirect electricity");
            SimplifyDamageType(ref text, "health");
            SimplifyDamageType(ref text, "direct");
            SimplifyDamageType(ref text, "direct health");
            SimplifyDamageType(ref text, "armor");

            foreach (string Item in DamageNames)
                SimplifyDamageType(ref text, Item);
        }

        private void SimplifyDamageType(ref string text, string damageType)
        {
            ReplaceCaseInsensitive(ref text, $"{damageType.ToLowerInvariant()} damage", "damage");
            ReplaceCaseInsensitive(ref text, $"{damageType.ToLowerInvariant()} base damage", "base damage");
        }

        private void SimplifyMitigrationType(ref string text, ref bool isExtracted)
        {
            SimplifyMitigrationType(ref text, "Direct Poison and Acid", ref isExtracted);
            SimplifyMitigrationType(ref text, "Indirect Poison and Acid", ref isExtracted);

            foreach (string Item in DamageNames)
                SimplifyMitigrationType(ref text, Item, ref isExtracted);
        }

        private void SimplifyMitigrationType(ref string text, string damageType, ref bool isExtracted)
        {
            ExtractSentence(ref text, $"Direct {damageType} Mitigation %f", ref isExtracted, out _);
            ExtractSentence(ref text, $"Indirect {damageType} Mitigation %f", ref isExtracted, out _);
            ExtractSentence(ref text, $"{damageType} Mitigation %f", ref isExtracted, out _);
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
            ReplaceCaseInsensitive(ref text, "hits ", "hit ");
            ReplaceCaseInsensitive(ref text, "stuns ", "stun ");
            ReplaceCaseInsensitive(ref text, "causes ", "cause ");
            ReplaceCaseInsensitive(ref text, "lowers ", "lower ");
            ReplaceCaseInsensitive(ref text, "takes ", "take ");
            ReplaceCaseInsensitive(ref text, "increases ", "increase ");
            ReplaceCaseInsensitive(ref text, " seconds", " second");
            ReplaceCaseInsensitive(ref text, " minutes", " minute");
            ReplaceCaseInsensitive(ref text, " meters", " meter");
            ReplaceCaseInsensitive(ref text, "haven't ", "have not ");
        }

        private void ExtractSentence(ref string text, string format, bool stopIfExtracted, ref bool isExtracted, out double data)
        {
            data = 0;

            if (isExtracted && stopIfExtracted)
                return;

            ExtractSentence(ref text, format, ref isExtracted, out data);
        }

        private void ExtractSentence(ref string text, string format, ref bool isExtracted, out double data)
        {
            data = 0;

            string LowerText = text.ToLowerInvariant();
            string LowerFormat = format.ToLowerInvariant();

            int Index = LowerFormat.IndexOf('%');

            if (Index >= 0)
            {
                char FormatType = LowerFormat[Index + 1];
                string Pattern = LowerFormat.Substring(0, Index);
                string AfterPattern = LowerFormat.Substring(Index + 2);
                int PatternIndex;

                if (Pattern.Length > 0)
                    PatternIndex = LowerText.IndexOf(Pattern);
                else
                {
                    PatternIndex = 0;
                    while (PatternIndex < LowerText.Length && !char.IsDigit(LowerText[PatternIndex]))
                        PatternIndex++;

                    if (PatternIndex >= LowerText.Length)
                        PatternIndex = -1;
                }

                if (PatternIndex < 0)
                    return;

                if (FormatType == 'd')
                {
                    int StartDataIndex = PatternIndex + Pattern.Length;
                    int EndDataIndex = StartDataIndex;
                    while (EndDataIndex < LowerText.Length && char.IsDigit(LowerText[EndDataIndex]))
                        EndDataIndex++;

                    if (EndDataIndex <= StartDataIndex)
                        return;
                    data = int.Parse(LowerText.Substring(StartDataIndex, EndDataIndex - StartDataIndex));
                    if (!LowerText.Substring(EndDataIndex).StartsWith(AfterPattern))
                        return;

                    text = text.Substring(0, PatternIndex) + text.Substring(EndDataIndex + AfterPattern.Length);
                    isExtracted = true;
                }
                else if (FormatType == 'f')
                {
                    int StartDataIndex = PatternIndex + Pattern.Length;
                    int EndDataIndex = StartDataIndex;
                    while (EndDataIndex < LowerText.Length && (char.IsDigit(LowerText[EndDataIndex]) || LowerText[EndDataIndex] == '.'))
                        EndDataIndex++;

                    if (EndDataIndex <= StartDataIndex)
                        return;
                    data = double.Parse(LowerText.Substring(StartDataIndex, EndDataIndex - StartDataIndex), NumberStyles.Float, CultureInfo.InvariantCulture);
                    if (!LowerText.Substring(EndDataIndex).StartsWith(AfterPattern))
                        return;

                    text = text.Substring(0, PatternIndex) + text.Substring(EndDataIndex + AfterPattern.Length);
                    isExtracted = true;
                }
            }
            else
            {
                int PatternIndex = LowerText.IndexOf(LowerFormat);
                if (PatternIndex < 0)
                    return;

                text = text.Substring(0, PatternIndex) + text.Substring(PatternIndex + LowerFormat.Length);
                isExtracted = true;
            }
        }

        private void RemoveUnusedText(ref string text)
        {
            ReplaceCaseInsensitive(ref text, ",", " ");
            ReplaceCaseInsensitive(ref text, ".", " ");
            ReplaceCaseInsensitive(ref text, "(", " ");
            ReplaceCaseInsensitive(ref text, ")", " ");
            ReplaceCaseInsensitive(ref text, "meaning you recover this power every 5 second  in and out of combat", " ");
            ReplaceCaseInsensitive(ref text, "when  deal damage", " ");
            ReplaceCaseInsensitive(ref text, "it also ignite the suspect", " ");
            ReplaceCaseInsensitive(ref text, "ignite all targets", " ");
            ReplaceCaseInsensitive(ref text, " and ability ", " ");
            ReplaceCaseInsensitive(ref text, " and it ", " ");
            ReplaceCaseInsensitive(ref text, " and also ", " ");
            ReplaceCaseInsensitive(ref text, " and ", " ");
            ReplaceCaseInsensitive(ref text, " but also ", " ");
            ReplaceCaseInsensitive(ref text, " but ", " ");
            ReplaceCaseInsensitive(ref text, " further ", " ");
            ReplaceCaseInsensitive(ref text, " its ", " ");
            ReplaceCaseInsensitive(ref text, " instead ", " ");
            ReplaceCaseInsensitive(ref text, "returning it to you as armor", " ");
            ReplaceCaseInsensitive(ref text, " in response to melee damage", " ");
            ReplaceCaseInsensitive(ref text, " abilities", " ");
            ReplaceCaseInsensitive(ref text, "crushing  slashing  or piercing", " ");
            ReplaceCaseInsensitive(ref text, "your golem minion's ", " ");
            ReplaceCaseInsensitive(ref text, "when you are near your fire wall", " ");
            ReplaceCaseInsensitive(ref text, "ignite the target", " ");
            ReplaceCaseInsensitive(ref text, "causing them to", " ");
            ReplaceCaseInsensitive(ref text, " to melee attackers", " ");
            ReplaceCaseInsensitive(ref text, "cause target to bleed", " ");
            ReplaceCaseInsensitive(ref text, "cause targets to", " ");
            ReplaceCaseInsensitive(ref text, "cause you to", " ");
            ReplaceCaseInsensitive(ref text, "increase ", " ");
            ReplaceCaseInsensitive(ref text, "suddenly ", " ");
            ReplaceCaseInsensitive(ref text, "when you teleport via ", " ");
            ReplaceCaseInsensitive(ref text, "when you are hit a monster's rage attack", " ");
            ReplaceCaseInsensitive(ref text, "when you are hit", " ");
            ReplaceCaseInsensitive(ref text, "while you are near ", " ");
            ReplaceCaseInsensitive(ref text, "you gain ", " ");
            ReplaceCaseInsensitive(ref text, " to you", " ");
            ReplaceCaseInsensitive(ref text, " you ", " ");
            ReplaceCaseInsensitive(ref text, "your ", " ");
            ReplaceCaseInsensitive(ref text, "using ", " ");
            ReplaceCaseInsensitive(ref text, " on an existing zombie", " ");
            ReplaceCaseInsensitive(ref text, " is ", " ");
            ReplaceCaseInsensitive(ref text, "this effect stacks with itself", " ");
            ReplaceCaseInsensitive(ref text, "who then ", " ");
            ReplaceCaseInsensitive(ref text, "plus it ", " ");
            ReplaceCaseInsensitive(ref text, "every few second", " ");
            ReplaceCaseInsensitive(ref text, "so it can be used again immediately", " ");
            ReplaceCaseInsensitive(ref text, "cause melee attackers to ignite", " ");
            ReplaceCaseInsensitive(ref text, "coats the target in stinging insects that", " ");
            ReplaceCaseInsensitive(ref text, "covers the target in insects that", " ");
            ReplaceCaseInsensitive(ref text, "cause  to", " ");
            ReplaceCaseInsensitive(ref text, "infects the target", " ");
            ReplaceCaseInsensitive(ref text, " the ", " ");
            ReplaceCaseInsensitive(ref text, "cow's ", " ");
            ReplaceCaseInsensitive(ref text, "inflicts bugs on target", " ");
            ReplaceCaseInsensitive(ref text, "to target", " ");
            ReplaceCaseInsensitive(ref text, "with each attack", " ");
            ReplaceCaseInsensitive(ref text, " gain ", " ");
            ReplaceCaseInsensitive(ref text, "if ", " ");
            ReplaceCaseInsensitive(ref text, "also ", " ");
            ReplaceCaseInsensitive(ref text, "have a", " ");
            text = text.Trim();
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

        private void CleanCase(ref string s)
        {
            if (s.Contains(" health ") || s.EndsWith(" health"))
                s = s.Replace(" health", " Health");
            if (s.Contains(" armor ") || s.EndsWith(" armor"))
                s = s.Replace(" armor", " Armor");
            if (s.Contains(" power ") || s.EndsWith(" power"))
                s = s.Replace(" power", " Power");
        }
        */
        private Dictionary<string, string> PerfectMatchWithAddition = new Dictionary<string, string>()
        {
            { "Nice Attacks deal +40 damage and cause the target's next Rage Attack to deal -25% damage (debuff cannot stack with itself)",
                "Target's next Rage Attack deals -25% damage" },

        };

        private Dictionary<string, string> PerfectMatch = new Dictionary<string, string>()
        {
            { "Nice Attacks deal +64 damage and hasten your current Combat Refresh delay by 1 second",
                "Hastens your current Combat Refresh timer by 1 second" },

            { "Core Attacks deal +32 damage and hasten your current Combat Refresh delay by 1 second",
                "Hastens your current Combat Refresh timer by 1 second" },

            { "Core Attacks deal +41 damage and reduce the Power cost of your next Minor Heal ability by -22",
                "Reduce Power cost of your next Minor Heal by -22" },

            { "Signature Support abilities restore 32 Power to all allies within 20 meters",
                "Restores 32 Power to all allies within 20 meters" },

            { "Basic Attacks restore 12 Power",
                "Restores 12 Power" },

            { "Signature Debuffs deal +25% damage and also deal 108 Armor damage",
                "Deals 108 Armor damage" },

            { "Major Healing abilities also restore 112 Armor",
                "Restores 112 Armor" },

            { "Major Healing abilities have a 65% chance to restore an additional 152 Health",
                "65% chance to restore 152 Health" },

            { "Signature Debuffs deal +80 damage and restore 96 Armor to you after a 10-second delay",
                "Restores 96 Armor to you after a 10-second delay" },

            { "Crossbow abilities boost your Epic Attack Damage +40% for 15 seconds",
              "Epic Attacks deal +40% damage for 15 seconds" },

            { "Signature Support abilities restore 80 Armor to all allies within 20 meters",
                "Restores 80 Armor to all allies within 20 meters" },

            { "Minor Heals restore 71 Armor",
                "Restores 71 Armor" },

            { "Melee attacks deal +50 damage to Pigs",
                "Melee attacks deal +50 damage to Pigs" },

            { "All sword abilities deal +17% damage when you have 33% or less of your Armor left",
                "+17% damage when you have 33% or less of your Armor left" },

            { "All Sword abilities have a 22% chance to restore 30 Health to you",
                "22% chance to restore 30 Health" },

            { "All fire spells deal up to +115 damage (randomly determined)",
                "+up to 115 damage (random)" },

            { "Unarmed attacks deal +9 damage and have +16 Accuracy (which cancels out the Evasion that certain monsters have)",
                "+16 Accuracy" },

            { "Crossbow abilities restore 240 health after a 15 second delay",
              "Restores 240 health after 15 seconds" },

            { "Crossbow abilities restore 272 armor after a 12 second delay",
              "Restores 272 armor after 12 seconds" },

            { "Sword Slash and Thrusting Blade restore 18 armor",
              "Restores 18 Armor" },

            { "Many Cuts deals +132 armor damage",
              "+132 armor damage" },

            { "Many Cuts deals +21% damage and stuns targets that have less than a third of their Armor remaining. However, Power cost is +33%",
              "Stuns targets that have less than a third of their Armor remaining" },

            { "Many Cuts knocks back targets that have less than a third of their Armor, also dealing +40 damage",
              "Knocks back targets that have less than a third of their Armor, also dealing +40 damage" },

            { "Many Cuts and Debilitating Blow deal +134 damage to Arthropods (such as spiders, mantises, and beetles)",
              "+134 damage to Arthropods" },

            { "Wind Strike hastens the current reuse timer of Finishing Blow by 6 seconds",
              "Hastens current reuse timer of Finishing Blow by 6 seconds" },

            { "Debilitating Blow hastens the current reuse timer of Decapitate by 8 seconds",
              "Hastens current reuse timer of Decapitate by 8 seconds" },

            { "Many Cuts hits all enemies within 5 meters, dealing +30 damage",
              "Hits all enemies within 5 meters" },

            { "Parry restores 26 health",
              "Restores 26 Health" },

            { "Parry hits all enemies within 5 meters, dealing an additional +30 damage",
              "Hits all enemies within 5 meters" },

            { "Riposte restores 53 armor",
              "Restores 53 Armor" },

            { "Wind Strike causes your next attack to deal +114 damage",
              "Your next attack deals +114 damage" },

            { "Wind Strike and Heart Piercer deal 124 armor damage",
              "+124 armor damage" },

            { "Wind Strike deals +38% damage and gives you +16 Accuracy for 10 seconds (Accuracy cancels out the Evasion that certain monsters have)",
              "Accuracy +16 for 10 seconds" },

            { "Finishing Blow gives you 25% resistance to Elemental damage (Fire, Cold, Electricity) for 10 seconds",
              "+25% resistance to Elemental damage (Fire, Cold, Electricity) for 10 seconds" },

            { "Wind Strike gives you +50% projectile evasion for 5 seconds",
              "+50% projectile evasion for 5 seconds" },

            { "Decapitate deals +80 damage and briefly terrifies the target",
              "Briefly terrifies the target" },

            { "Flashing Strike deals +25% damage and gives you 50% resistance to Darkness damage for 4 seconds",
              "+50% resistance to Darkness damage for 4 seconds" },

            { "Finishing Blow restores 42 Power to you",
              "Restores 42 Power" },

            { "Finishing Blow restores 104 armor to you",
              "Restores 104 Armor" },

            { "Decapitate restores 165 armor to you",
              "Restores 165 Armor" },

            { "Decapitate deals +425 damage to non-Elite targets",
              "+425 damage to non-Elite targets" },

            { "Flashing Strike heals you for 55 health",
              "Restores 55 Health" },

            { "Flashing Strike deals +217 damage to undead",
              "+217 damage to undead" },

            { "Heart Piercer heals you for 40 health",
              "Restores 40 Health" },

            { "Heart Piercer removes (up to) 212 more Rage, turning half of that into Trauma damage",
              "Removes (up to) 212 more Rage, turning half of that into Trauma damage" },

            { "Heart Piercer deals +18% piercing damage and heals you for 23 health",
              "Restores 23 Health" },

            { "Precision Pierce and Heart Piercer restore 18 Health to you",
              "Restores 18 Health" },

            { "For 6 seconds after using Precision Pierce, your Nice Attacks deal +64 damage",
              "Nice Attack Damage +64 for 6 seconds" },

            { "Debilitating Blow deals +40 damage and causes your Core Attacks to deal +75 damage for 7 seconds",
              "Core Attack Damage +75 for 7 seconds" },

            { "Fire Breath and Super Fireball deal +165 damage over 10 seconds",
              "+165 damage over 10 seconds" },

            { "Fire Breath deals +72 damage and grants you +24 mitigation vs Fire for 10 seconds",
              "+24 Fire Mitigation for 10 seconds" },

            { "Fire Breath hits all targets within 8 meters and deals +24.5% damage, but reuse timer is +3 seconds and Power cost is +33%",
              "Hits all targets within 8 meters" },

            { "Frostball, Scintillating Frost, and Defensive Chill boost your Nice Attack Damage +61 for 7 seconds",
              "Nice Attack Damage +61 for 7 seconds" },

            { "Calefaction causes target to take +45% damage from Cold for 12 seconds",
              "Target takes +45% damage from Cold for 12 seconds" },

            { "Room-Temperature Ball and Defensive Burst cause the target's attacks to deal -16 damage for 10 seconds",
              "Target's attacks deal -16 damage for 10 seconds" },

            { "Super Fireball causes the target to take +60% damage from indirect Fire (this effect does not stack with itself)",
              "Target takes +60% indirect Fire damage (this effect does not stack with itself)" },

            { "Flesh to Fuel boosts your Core Attack Damage +118 for 7 seconds",
              "Core Attack Damage +118 for 7 seconds" },

            { "Flesh to Fuel restores +62 Armor",
              "Restores +62 Armor" },

            { "Flesh to Fuel restores +34 Power but has a 5% chance to stun you",
              "5% chance to stun YOU" },

            { "Flesh to Fuel increases your Out of Combat Sprint speed +8 for 15 seconds",
              "+8 Out of Combat Sprint Speed for 15 seconds" },

            { "Room-Temperature Ball deals Darkness damage and causes +126 damage over 12 seconds",
              "DamageType:Darkness; +126 Darkness damage over 12 seconds" },

            { "You regain 20 Power when using Ring of Fire, Defensive Burst, or Defensive Chill",
              "Restores 20 Power" },

            { "You regain 31 Health when using Ring of Fire, Defensive Burst, or Defensive Chill",
              "Restores 31 Health" },

            { "Scintillating Flame restores 33 Health",
              "Restores 33 Health" },

            { "Scintillating Flame and Molten Veins boost your Core Attack Damage and Epic Attack Damage +39 for 15 seconds",
              "Core Attack Damage and Epic Attack Damage +39 for 15 seconds" },

            { "Scintillating Flame and Scintillating Frost stun and deal +100% damage to Vulnerable targets",
              "Stun and +100% damage to Vulnerable targets" },

            { "Ring of Fire deals +49% damage but has a 5% chance to deal 140 fire damage to YOU",
              "5% chance to deal 140 fire damage to YOU" },

            { "Scintillating Frost and Defensive Chill restore 31 Armor",
              "Restores 31 Armor" },

            { "Molten Veins restores 55 Armor",
              "Restores 55 Armor" },

            { "Calefaction restores 45 Health",
              "Restores 45 Health" },

            { "Fireball and Frostball Damage +36%",
              "Fireball and Frostball Damage +36%" },

            { "Frostball targets all enemies within 10 meters and deals +52 damage, but reuse timer is +3 seconds",
              "Hits all enemies within 10 meters" },

            { "Frostball slows target's movement by 25% and deals +16 damage",
              "Slows target's movement by 25%" },

            { "Defensive Burst deals +19% damage and raises Basic Attack Damage +28% for 10 seconds",
              "Basic Attack Damage +28% for 10 seconds" },

            { "Frostball, Scintillating Frost, and Defensive Chill grant +10 Direct and Indirect Cold Protection for 10 seconds",
              "+10 Cold Protection (Direct and Indirect) for 10 seconds" },

            { "Defensive Burst and Defensive Chill restore 42 Armor to you",
              "Restores 42 Armor" },

            { "Defensive Chill deals +46 damage and grants you 70% chance to ignore Knockback effects for 7 seconds",
              "70% chance to ignore Knockback effects for 7 seconds" },

            { "Fire Walls deal +53 damage per hit",
              "+53 Direct Damage" },

            { "Fire Walls have +176 Max Health",
              "+176 Max Health" },

            { "Fire Walls' attacks taunt +250%",
              "Taunt +250%" },

            { "Punch, Jab, and Infuriating Fist restore 18 Health to you",
              "Restores 18 Health" },

            { "Kick attacks restore 42 Armor",
              "Restores 42 Armor" },

            { "Kick attacks deal +13% damage and grant you 8% Physical Damage Reflection for 15 seconds",
              "+8% Physical Damage Reflection for 15 seconds" },

            { "Kick attacks deal +18% damage and slow target's movement speed by 45%",
              "Slows target's movement by 45%" },

            { "Kick attacks deal +34% damage when you have 33% or less of your Armor left",
              "+34% damage when you have 33% or less of your Armor left" },

            { "Barrage hits all enemies within 5 meters and deals +19% damage, but reuse timer is +3 seconds",
              "Hits all enemies within 5 meters" },

            { "Barrage and Headbutt make the target 19% more vulnerable to Psychic damage for 20 seconds (this effect does not stack with itself)",
              "Target is 19% more vulnerable to Psychic damage for 20 seconds (this effect does not stack with itself)" },

            { "Headbutt deals +20% damage and conjures a magical field that mitigates 15% of all physical damage you take for 10 seconds (or until 400 damage is mitigated)",
              "Mitigates 15% of all Slashing/Crushing/Piercing damage for 10 seconds (or until 400 damage is mitigated)" },

            { "Barrage costs -14 Power and restores 23 Armor to you",
              "Restores 23 Armor" },

            { "Hip Throw deals +226 armor damage",
              "+226 armor damage" },

            { "Hip Throw hits all enemies within 8 meters and deals +16% damage, but Power cost is +20",
              "Hits all enemies within 8 meters" },

            { "Bodyslam deals +37% damage and slows target's movement speed by 45%",
              "Slows target's movement speed by 45%" },

            { "Bruising Blow causes the target to take +24% damage from Poison for 20 seconds",
              "Target's Poison Vulnerability +24% for 20 seconds" },

            { "Bodyslam deals +435 damage to non-Elite enemies",
              "+435 damage to non-Elite enemies" },

            { "Bodyslam heals you for 130 health",
              "Restores 130 Health" },

            { "Cobra Strike and Mamba Strike boost your Nice Attack and Signature Debuff ability damage +55 for 7 seconds",
              "Nice Attack and Signature Debuff Damage +55 for 7 seconds" },

            { "Cobra Strike and Mamba Strike restore 40 Armor to you",
              "Restores 40 Armor" },

            { "Bruising Blow deals +28% damage and hastens the current reuse timer of Bodyslam by 5 seconds",
              "Hastens the current reuse timer of Bodyslam by 5 second" },

            { "Bruising Blow and Headbutt restore 28 Health",
              "Restores 28 Health" },

            { "Bruising Blow deals Trauma damage instead of Crushing, and targets suffer +12.5% damage from other Trauma attacks for 20 seconds",
              "DamageType:Trauma; Target's Trauma Vulnerability +12.5% for 20 seconds" },

            { "Slashing Strike and Claw Barrage boost damage from Epic attacks +74 for 10 seconds",
              "Epic Attack Damage +74 for 10 seconds" },

            { "Slashing Strike deals +15% damage and hastens the current reuse timer of Hip Throw by 2.5 seconds",
              "Hastens the current reuse timer of Hip Throw by 2.5 seconds" },

            { "Psychoanalyze deals between 160 and 360 extra damage",
              "+Up to 200 extra damage (randomly determined)" },

            { "Psychoanalyze restores 115 Health to you after a 15 second delay",
              "Restores 115 Health after a 15 second delay" },

            { "Psychoanalyze restores 172 Armor to you",
              "Restores 172 Armor" },

            { "Psychoanalyze causes the target to be worth 16% more XP if slain within 60 seconds",
              "Target is worth 16% more XP if slain within 60 seconds" },

            { "Psychoanalyze causes the target to take +20 damage from Psychic attacks for 60 seconds",
              "Target takes +20 direct Psychic damage for 60 seconds" },

            { "Tell Me About Your Mother restores 141 Armor to you",
              "Restores 141 Armor" },

            { "Tell Me About Your Mother causes target's attacks to deal -20 damage for 60 seconds",
              "Target's attacks deal -20 damage for 60 seconds" },

            { "Tell Me About Your Mother boosts your Epic Attack Damage +110 and reduces the Power cost of your Epic Attacks -19 for 15 seconds",
              "Epic Attack Damage +110 and Epic Attack Power Cost -19 for 15 seconds" },

            { "Strike a Nerve deals between 48 and 160 extra damage",
              "+Up to 112 extra damage (randomly determined)" },

            { "Strike a Nerve deals +132 armor damage",
              "+132 armor damage" },

            { "Pep Talk restores 47 Power",
              "Restores 47 Power" },

            { "Pep Talk removes ongoing Poison effects (up to 44 dmg/sec)",
              "Removes ongoing Poison effects (up to 44 dmg/sec)" },

            { "Pep Talk removes ongoing Fire effects (up to 48 dmg/sec)",
              "Removes ongoing Fire effects (up to 48 dmg/sec)" },

            { "Pep Talk restores 112 Armor",
              "Restores 112 Armor" },

            { "Fast Talk heals you for 37 health",
              "Restores 37 Health" },

            { "Fast Talk heals you for 58 armor",
              "Restores 58 Armor" },

            { "Positive Attitude boosts your Out-of-Combat Sprint Speed by 4 for 60 seconds",
              "+4 Out-of-Combat Sprint Speed for 60 seconds" },

            { "Positive Attitude increases your Poison Mitigation +16 for 30 seconds",
              "+16 Poison Mitigation for 30 seconds" },

            { "Positive Attitude increases your Core Attack Damage +98 for 15 seconds",
              "Core Attack Damage +98 for 15 seconds" },

            { "Inspire Confidence increases all targets' Accuracy +16 for 10 seconds",
              "+16 Accuracy for 10 seconds" },

            { "Inspire Confidence increases the damage of all targets' attacks +12 for 30 seconds",
              "+12 damage from all attacks for 30 seconds" },

            { "Inspire Confidence restores +119 Health after a 15 second delay",
              "Restores 119 Health after a 15 second delay" },

            { "Inspire Confidence restores +165 Health after a 25 second delay",
              "Restores 165 Health after a 25 second delay" },

            { "But I Love You deals +20% damage and stuns the target",
              "Stuns the target" },

            { "But I Love You boosts your Nice and Epic Attack Damage +75 for 8 seconds",
              "Nice and Epic Attack Damage +75 for 8 seconds" },

            { "You Were Adopted deals +33% damage and triggers the target's Vulnerability",
              "Triggers the target's Vulnerability" },

            { "Soothe boosts the healing from your Major Healing abilities +44 for 10 seconds",
              "+44 Major Healing for 10 seconds" },

            { "Ridicule boosts movement speed by 4.5 for 6 seconds",
              "+4.5 Movement Speed for 6 seconds" },

            { "All Staff attacks have a 5.5% chance to trigger the target's Vulnerability",
              "5.5% chance to trigger the target's Vulnerability" },

            { "Phoenix Strike costs -16 Power and boosts your Direct Fire Damage +20% for 30 seconds",
              "Direct Fire Damage +20% for 30 seconds" },

            { "Double Hit causes your next attack to deal +100 damage if it is a Crushing attack",
              "Your next attack deals +100 damage if it is a Crushing attack" },

            { "Pin heals you for 45 health",
              "Restores 45 Health" },

            { "Pin boosts Core Attack and Nice Attack Damage +83 for 7 seconds",
              "Core Attack and Nice Attack Damage +83 for 7 seconds" },

            { "Suppress and Heed the Stick have +35 Accuracy",
              "+35 Accuracy" },

            { "Double Hit costs -16 Power and makes the target 10% more vulnerable to Slashing for 15 seconds",
              "Target is 10% more vulnerable to Slashing damage for 15 seconds" },

            { "Blocking Stance boosts your Direct Cold Damage +15.5% for 30 seconds",
              "Direct Cold Damage +15.5% for 30 seconds" },

            { "Double Hit deals +36% damage and hastens the current reuse timer of Headcracker by 2 seconds",
              "Hastens the current reuse timer of Headcracker by 2 second" },

            { "Lunge deals +39% damage to health and armor",
              "+39% Armor damage" },

            { "Lunge hits all enemies within 5 meters, but deals -8% damage and reuse timer is +2 seconds",
              "Hits all enemies within 5 meters" },

            { "Lunge deals +55 damage and knocks the target backwards",
              "Knocks the target backwards" },

            { "Lunge deals +121 armor damage",
              "+121 armor damage" },

            { "Deflective Spin heals 108 Health over 60 seconds",
              "Restores 108 Health over 60 seconds" },

            { "Deflective Spin restores 42 Power after a 20 second delay",
              "Restores 42 Power after a 20 second delay" },

            { "If you have less than half of your Health remaining, Deflective Spin heals you for 31% of your Max Health",
              "Restores 31% of your Max Health if you have less than half of your Health remaining" },

            { "Pin deals +120 damage and has +25 Accuracy (which cancels out the Evasion that certain monsters have)",
              "+25 Accuracy" },

            { "Blocking Stance restores 38 Power to you",
              "Restores 38 Power" },

            { "For 30 seconds after using Blocking Stance, your Mentalism Base Damage is +12.5%",
              "Your Mentalism Base Damage is +12.5% for 30 seconds" },

            { "For 60 seconds after using Blocking Stance, First Aid heals you +68",
              "First Aid Healing +68 for 60 seconds" },

            { "For 60 seconds after using Redirect, First Aid heals you +75",
              "First Aid Healing +75 for 60 seconds" },

            { "Redirect deals +60 damage and stuns the target",
              "Stuns the target" },

            { "Strategic Thrust deals +48% damage, plus 40% more damage if the target is Vulnerable",
              "+40% damage if the target is Vulnerable" },

            { "If Strategic Thrust is used on a Vulnerable target, it deals +113 damage and restores 80 Health to you",
              "+113 damage and restores 80 Health if target is Vulnerable" },

            { "Heed The Stick heals you for 28 health (or armor if health is full)",
              "Restores 28 health (or armor if health is full)" },

            { "Heed The Stick gives you +17 mitigation from direct attacks for 10 seconds",
              "+17 mitigation from direct attacks for 10 seconds" },

            { "After using Headcracker, you take half damage from Psychic attacks for 15 seconds",
              "Direct Psychic Mitigation +50% for 15 seconds" },

            { "Pin causes target's attacks to deal -50% damage for 5 seconds",
              "Target's attacks deal -50% damage for 5 seconds" },

            { "For 30 seconds after using Phoenix Strike, your Survival Utility and Major Heal abilities restore 83 Health to you",
              "Survival Utility and Major Heal abilities restore 83 Health (30 seconds)" },

            { "Phoenix Strike deals +10% damage and triggers the target's Vulnerability",
              "Triggers the target's Vulnerability" },

            { "Suppress heals you for 45 health",
              "Restores 45 Health" },

            { "Werewolf Bite deals +19% damage and boosts your Nice Attack Damage +50 for 10 seconds",
              "Nice Attack Damage +50 for 10 seconds" },

            { "Claw and Double Claw restore 18 Health",
              "Restores 18 Health" },

            { "Bite restores 30 Health to you",
              "Restores 30 Health" },

            { "Werewolf Bite hits all enemies within 5 meters and deals +16% damage, but reuse timer is +2 seconds",
              "Hits all enemies within 5 meters" },

            { "Skulk boosts the damage of your Core and Nice Attacks +50 for 30 seconds",
              "Core Attack Damage and Nice Attack Damage +50 for 30 seconds" },

            { "Pouncing Rake deals +172 Armor damage",
              "+172 armor damage" },

            { "Blood of the Pack restores 90 Health over 10 seconds to you and your allies",
              "Restores 90 Health over 10 seconds to all targets" },

            { "Future Pack Attacks to the same target deal +80 damage",
              "Future Pack Attacks to the same target deal +80 damage" },

            { "After using Pack Attack, your Lycanthropy Base Damage increases +85% for 7 seconds or until you are attacked",
              "Lycanthropy Base Damage +85% for 7 seconds or until you are attacked" },

            { "Blood of the Pack causes you and your allies' attacks to deal +35 damage for 30 seconds",
              "+35 Direct Damage (for all targets) for 30 seconds" },

            { "Sanguine Fangs causes the target to take +14% damage from Slashing attacks for 15 seconds",
              "Target takes +14% damage from Slashing attacks for 15 seconds" },

            { "Sanguine Fangs deals +29% Crushing damage and doesn't cause the target to yell for help",
              "Target does not yell for help because of this attack" },

            { "See Red heals you for 38 health",
              "Restores 38 Health" },

            { "For 8 seconds after using See Red, all other Lycanthropy attacks deal 116 Trauma damage over 8 seconds",
              "For 8 seconds, all Lycanthropy attacks deal 116 Trauma damage over 8 seconds" },

            { "See Red increases the damage of your next attack by +95",
              "Your next attack deals +95 damage" },

            { "See Red grants you 14% melee evasion for 8 seconds",
              "Gain 14% melee evasion for 8 seconds" },

            { "Skulk grants you +24 Mitigation against all attacks",
              "+24 Damage Mitigation" },

            { "Shadow Feint raises your Lycanthropy Base Damage +52% until you trigger the teleport",
              "Lycanthropy Base Damage +52% for 20 seconds or until you teleport" },

            { "Shadow Feint causes your next attack to deal +192 damage if it is a Werewolf ability",
              "Your next attack deals +192 damage if it is a Werewolf ability" },

            { "Shadow Feint reduces the taunt of all your attacks by 46% until you trigger the teleport",
              "All attacks taunt -46% for 20 seconds or until you Feint" },

            { "When Skulk is used, you recover 52 Health and all enemies within 10 meters are taunted -700",
              "Recover 52 Health; All enemies within 10 meters are taunted -700" },

            { "Skulk causes your next attack to deal +43% damage if it is a Crushing attack",
              "Your next attack deals +43% damage if it is a Crushing attack" },

            { "Skulk grants you +70% Projectile Evasion",
              "+70% Projectile Evasion" },

            { "Freezing Mist restores 133 Armor to you",
              "Restores 133 Armor" },

            { "Healing Mist heals +82 Health",
              "Restores 82 Health" },

            { "Healing Mist heals +118 Armor",
              "Restores 118 Armor" },

            { "Healing Mist restores 40 power",
              "Restores 40 Power" },

            { "Healing Injection heals 90 Health after a 20 second delay",
              "Restores 90 Health after a 20 second delay" },

            { "Bomb attacks deal +55 damage and hasten the current reuse timer of Healing Mist by 2.5 seconds",
              "Shortens the remaining reset time of Healing Mist by 2.5 seconds" },

            { "Healing Mist hastens the remaining reset timer of Pep Talk by 10 seconds (if Pep Talk is not already ready to use)",
              "Shortens the remaining reset time of Pep Talk by 10 seconds" },

            { "Healing Mist hastens the remaining reset timer of Reconstruct by 10 seconds (if Reconstruct is not already ready to use)",
              "Shortens the remaining reset time of Reconstruct by 10 seconds" },

            { "Healing Mist hastens the remaining reset timer of Regrowth by 10 seconds (if Regrowth is not already ready to use)",
              "Shortens the remaining reset time of Regrowth by 10 seconds" },

            { "Your Knee Spikes mutation also causes kicks to restore 18 Health to the kicker",
              "Causes kicks to restore 18 Health to the kicker" },

            { "Your Knee Spikes mutation causes kicks to deal an additional +20% damage",
              "Causes kicks to deal an additional +20% damage" },

            { "Your Extra Skin mutation causes the target to heal 55 Health every 20 seconds",
              "Causes the target to heal 55 Health every 20 seconds" },

            { "Your Extra Skin mutation provides +18 mitigation from Slashing attacks",
              "Provides +18 mitigation from Slashing attacks" },

            { "Your Extra Skin mutation provides +18 mitigation from Piercing attacks",
              "Provides +18 mitigation from Piercing attacks" },

            { "Your Extra Heart mutation causes the target to regain +35 Power every 20 seconds",
              "Causes the target to regain +35 Power every 20 seconds" },

            { "Your Extra Heart and Stretchy Spine mutations grant the target +55 Max Health",
              "Grant the target +55 Max Health" },

            { "Your Stretchy Spine mutation randomly repairs broken bones twice as often",
              "Randomly repairs broken bones twice as often" },

            { "Spark of Death deals +49 damage and renders target 10% more vulnerable to Electricity damage for 30 seconds",
              "Target takes 10% more damage from Electricity for 30 seconds" },

            { "Summoned Skeletons deal +17% direct damage",
              "+17% Direct Damage" },

            { "Life Steal restores 34 Health",
              "Restores 34 Health" },

            { "Life Steal targets all enemies within 10 meters and steals +32 health, but reuse timer is +3 seconds and Power cost is +23",
              "Reap 32 additional health, hits all enemies within 10 meters" },

            { "Life Steal reaps 35 additional health",
              "Reap 35 additional health" },

            { "Deathgaze deals +95% damage and has +20 Accuracy (which cancels out the Evasion that certain monsters have)",
              "+20 Accuracy" },

            { "Deathgaze deals +84 damage and restores 83 armor to you",
              "Restores 83 Armor" },

            { "Deathgaze deals +118 damage and increases your sprint speed +2.5 for 15 seconds",
              "+2.5 Sprint Speed for 15 seconds" },

            { "Death's Hold causes target to take +14% damage from Slashing for 15 seconds",
              "Target takes +14% damage from Slashing for 15 seconds" },

            { "Death's Hold causes target to take +14% damage from Electricity for 15 seconds",
              "Target takes +14% damage from Electricity for 15 seconds" },

            { "Rebuild Undead restores 55 Health to you",
              "Restores 55 Health to YOU" },

            { "Rebuild Undead restores 110 health/armor to your undead after a 10 second delay",
              "Restores 110 health/armor to your undead after a 10 second delay" },

            { "Wave of Darkness deals +160 damage to sentient creatures",
              "+160 damage to sentient creatures" },

            { "Summoned Skeletons have +50 health",
              "+50 Max Health" },

            { "Summoned Skeletons have +76 armor",
              "+76 Max Armor" },

            { "Summoned Skeletal Archers and Mages deal +28 direct damage",
              "+28 Direct Damage" },

            { "Summoned Skeletal Swordsmen have +108 armor",
              "+108 Max Armor" },

            { "Summoned Skeletal Swordsmen taunt as if they did 850% more damage",
              "Pets' Taunt +850%" },

            { "Summoned Skeletal Swordsmen have -45% Max Rage, allowing them to use their stun attack more often",
              " -45% Max Rage" },

            { "Provoke Undead restores 52 Health to you and causes your attacks to taunt +20% for 10 seconds",
              "Restores 52 Health to you; your Taunt is +20% for 10 seconds" },

            { "Heal Undead restores +18 health/armor and grants target undead +13 Mitigation from all attacks for 8 seconds",
              "Target undead gain +13 Mitigation from all attacks for 8 seconds" },

            { "Heal Undead restores +24 Health/Armor and boosts your next attack +43 if it is a Darkness attack",
              "Your next attack deals +43 damage if it is a Darkness attack" },

            { "Heal Undead restores +20 and has a 25% chance to boost targets' mitigation +35 for 8 seconds",
              "25% chance to boost targets' mitigation +35 for 8 seconds" },

            { "Summoned Skeletons deal +18% direct damage, but take +150% more damage from any cold attacks",
              "+18% Direct Damage, but Cold Vulnerability +150%" },

            { "Summoned Skeletal Archers and Mages deal +20% direct damage, but take +50% damage from any slashing, piercing, or crushing attacks",
              "+20% Direct Damage, but Slashing/Piercing/Crushing Vulnerability +50%" },

            { "Summoned Skeletal Archers and Mages deal +13% direct damage, but are instantly destroyed by ANY Nature Damage",
              "+13% Direct Damage, but Nature Vulnerability +Infinity" },

            { "Raised Zombies deal +21% damage",
              "+21% Direct Damage" },

            { "Raised Zombies deal +39 damage and taunt as if they did +200% more damage",
              "+39 Direct Damage and Taunt +200%" },

            { "Raised Zombies deal +20 damage and speed is +10",
              "+20 Direct Damage and Movement Speed +10" },

            { "Electrify stuns the target and deals +32 damage",
              "Stuns target" },

            { "Electrify, System Shock, and Panic Charge restore 30 Health after a 15 second delay",
              "Restores 30 Health after a 15 second delay" },

            { "For 15 seconds after using Mindreave, your Major Healing abilities restore +48 Health (this effect does not stack with itself)",
              "Major Healing +48 for 15 seconds (this effect does not stack with itself)" },

            { "System Shock boosts the damage of your Signature Debuffs by +92 for 6 seconds",
              "Signature Debuff Damage +92 for 6 seconds" },

            { "System Shock restores 44 Armor to you",
              "Restores 44 Armor" },

            { "Reconstruct restores +23 Health and causes the target to take 24 less damage from attacks for 10 seconds",
              "Target takes 24 less damage from attacks for 10 seconds" },

            { "Revitalize restores +22 Health and causes the target to take 30 less damage from Psychic and Nature attacks for 10 seconds",
              "Target takes 30 less damage from Psychic and Nature attacks for 10 seconds" },

            { "Reconstruct restores 51 Power to the target",
              "Restores 51 Power to the target" },

            { "Revitalize restores +20 Health and removes ongoing Trauma effects (up to 26 dmg/sec)",
              "Removes ongoing Trauma effects (up to 26 dmg/sec)" },

            { "Reconstruct restores 35 power and boosts target's sprint speed by 4.5 for 10 seconds",
              "Restores 35 Power and boosts target's sprint speed by 4.5 for 10 seconds" },

            { "Revitalize restores 67 armor to YOU (regardless of the target of the ability)",
              "Restores 67 Armor to YOU" },

            { "Psi Health Wave and Psi Adrenaline Wave instantly heal all targets for 32 health",
              "Instantly restores 32 Health to all targets" },

            { "Psi Health Wave heals all targets for 74 health after a 25 second delay",
              "Restores 74 Health to all targets after a 25 second delay" },

            { "Psi Health Wave and Psi Armor Wave instantly heal you for 47 health",
              "Instantly restores 47 Health to YOU" },

            { "Psi Health Wave grants all targets +28 Mitigation vs. Electricity, Acid, and Nature attacks for 20 seconds",
              "All targets gain +28 Mitigation vs. Electricity, Acid, and Nature attacks for 20 seconds" },

            { "Psi Armor Wave instantly restores 52 armor to all targets",
              "Instantly restores 52 Armor to all targets" },

            { "Psi Armor Wave and Psi Adrenaline Wave restore 158 armor to all targets after a 25 second delay",
              "Restores 158 Armor to all targets after a 25 second delay" },

            { "Psi Health Wave and Psi Armor Wave instantly restore 84 armor to you",
              "Instantly restores 84 Armor to YOU" },

            { "Agonize deals +22% damage and conjures a magical field that mitigates 20% of all physical damage you take for 1 minute (or until 200 damage is mitigated).",
              "Conjures a magical field that mitigates 20% of all physical damage you take for 1 minute (or until 200 damage is mitigated)." },

            { "Psi Power Wave and Psi Adrenaline Wave restore 54 power to all targets after a 25 second delay",
              "Restores 54 Power to all targets after a 25 second delay" },

            { "Psi Power Wave instantly restores 31 power to all targets",
              "Instantly restores 31 Power to all targets" },

            { "Psi Power Wave and Psi Adrenaline Wave instantly restore 32 power to you",
              "Instantly restores 32 power to YOU" },

            { "Psi Power Wave and Psi Armor Wave cause all targets' melee attacks to cost -8 Power for 20 seconds",
              "All targets' melee attacks cost -8 Power for 20 seconds" },

            { "Psi Health Wave, Armor Wave, and Power Wave grant all targets +42 Psychic Damage for 60 seconds",
              "All targets gain +42 Psychic Damage for 60 seconds" },

            { "Psi Adrenaline Wave increases all targets' Slashing damage +9% for 20 seconds",
              "All targets gain +9% Slashing Damage for 20 seconds" },

            { "Psi Adrenaline Wave increases all targets' Electricity damage +9% for 20 seconds",
              "All targets gain +9% Electricity Damage for 20 seconds" },

            { "Psi Adrenaline Wave increases all targets' Crushing damage +9% for 20 seconds",
              "All targets gain +9% Crushing Damage for 20 seconds" },

            { "Agonize deals +70% damage and reuse timer is -11 seconds, but the ability deals 120 health damage to YOU",
              "Deals 120 psychic health damage to YOU" },

            { "Panic Charge boosts the damage of all your attacks +23 for 20 seconds",
              "+23 Direct Damage for 20 seconds" },

            { "Panic Charge knocks all targets back and restores 78 armor to you",
              "Targets are knocked back; Restores 78 Armor" },

            { "Pain Bubble deals +80 damage and restores 60 armor to you",
              "Restores 60 Armor" },

            { "Electrify restores 64 Health to you",
              "Restores 64 Health" },

            { "Electrify restores 40 power to you",
              "Restores 40 Power" },

            { "Pain Bubble increases the damage of your ranged attacks by 13% for 10 seconds",
              "Ranged Attack Damage Damage +13% for 10 seconds" },

            { "Fairy Fire causes your next attack to deal +98 damage if it's a Psychic, Electricity, or Fire attack",
              "Fairy Fire causes your next attack to deal +98 damage if it's a Psychic, Electricity, or Fire attack" },

            { "Astral Strike's reuse timer is -4 secs, and damage is boosted +80% vs Elite enemies",
              "+80% damage vs Elites" },

            { "Astral Strike's damage is +160 and all targets are Stunned",
              "Targets are Stunned" },

            { "Astral Strike causes all targets to suffer +102 damage from direct Cold attacks for 10 seconds",
              "Targets' Direct Cold Mitigation -102 for 10 seconds" },

            { "Pixie Flare's attack range is +5, and it deals +210 damage to targets that are covered in Fairy Fire",
              "+210 damage if target is covered in Fairy Fire" },

            { "Fae Conduit restores +8 Power every 5 seconds",
              "Restores +8 Power every 5 seconds" },

            { "Fae Conduit also heals 45 Health every 5 seconds",
              "Restores +45 Health every 5 seconds" },

            { "Fae Conduit also buffs targets' direct Cold, Fire, and Electricity damage +40 for 30 seconds (stacking up to 6 times)",
              "Buffs targets' direct Cold, Fire, and Electricity damage +40 for 30 seconds every 5 seconds" },

            { "Basic Shot and Aimed Shot heal you for 28 health",
              "Restores 28 health" },

            { "Aimed Shot deals 132 additional health damage over 12 seconds",
              "+132 Trauma damage over 12 seconds" },

            { "Aimed Shot boosts your Nice Attack Damage +108 for 10 seconds",
              "Nice Attack Damage +108 for 10 seconds" },

            { "Aimed Shot deals +30% damage and boosts your Accuracy +20 for 10 seconds",
              "Accuracy +20 for 10 seconds" },

            { "Multishot restores 70 Health to you after a 15 second delay",
              "Restores 70 Health after a 15 second delay" },

            { "Long Shot boosts your Epic Attack Damage +16% for 15 seconds",
              "Epic Attack Damage +16% for 15 seconds" },

            { "Long Shot boosts your Armor Regeneration (in-combat) +16 for 15 seconds",
              "Armor Regeneration (in-combat) +16 for 15 seconds" },

            { "Long Shot restores 49 health to you after a 15 second delay",
              "Restores 49 Health after a 15 second delay" },

            { "Blitz Shot and Basic Shot boost your healing from Combat Refreshes +8 for 30 seconds",
              "Healing from Combat Refreshes +8 for 30 seconds" },

            { "Poison Arrow increases the damage target takes from Poison by 19% for 10 seconds",
              "Increases target's Poison Vulnerability +19% for 10 seconds" },

            { "Poison Arrow makes target's attacks deal -10 damage for 20 seconds",
              "Target's attacks deal -10 damage for 20 seconds" },

            { "Snare Arrow boosts the healing of your Major Healing abilities +70 for 15 seconds",
              "Major Healing Restoration +70 for 15 seconds" },

            { "Snare Arrow raises target's Max Rage by 1200, requiring more Rage to use their Rage Abilities",
              "Increases target's Max Rage by1200 for 60 seconds" },

            { "Snare Arrow restores 43 Health and 43 Armor to you",
              "Restores 43 Health and 43 Armor" },

            { "Bow Bash gives you +8 mitigation of any physical damage for 20 seconds. (This effect does not stack with itself.)",
              "+8 mitigation of any physical damage for 20 seconds. (This effect does not stack with itself.)" },

            { "Bow Bash heals you for 16 health",
              "Restores 16 Health" },

            { "Bow Bash deals +180 damage and knocks the target backwards, but ability's reuse timer is +3 seconds",
              "Knocks target backwards" },

            { "Mangling Shot deals +43% damage and slows target's movement by 25%",
              "Slows target's movement by 25%" },

            { "Mangling Shot causes target to take +11.5% damage from Piercing for 10 seconds",
              "Target takes +11.5% damage from Piercing for 10 seconds" },

            { "Mangling Shot deals +16% damage and causes target's attacks to deal -16 damage for 20 seconds",
              "Target's attacks deal -16 damage for 20 seconds" },

            { "Restorative Arrow heals YOU for 90 Health",
              "Restores 90 Health to YOU" },

            { "Restorative Arrow restores an additional 126 Health over 30 seconds",
              "Restores 126 Health over 30 seconds" },

            { "Restorative Arrow boosts target's Nice Attack and Epic Attack Damage +144 for 10 seconds",
              "Boosts target's Nice Attack and Epic Attack Damage +144 for 10 seconds" },

            { "All types of shield Bash attacks restore 16 Armor",
              "Restores 16 Armor" },

            { "Infuriating Bash deals +48 damage and boosts your Indirect Acid Damage +54 for 7 seconds",
              "Indirect Acid Damage +54 for 7 seconds" },

            { "Strategic Preparation boosts your Indirect Acid Damage +25% for 20 seconds",
              "Indirect Acid Damage +25% for 20 seconds" },

            { "Strategic Preparation boosts your in-combat Armor regeneration +28 for 20 seconds",
              "In-Combat Armor Regeneration +28 for 20 seconds" },

            { "Strategic Preparation causes your next attack to deal +130 damage if it is a Crushing, Slashing, or Piercing attack",
              "Next attack deals +130 damage if it is a Crushing, Slashing, or Piercing attack" },

            { "Reinforce causes your Major Healing abilities to restore +52 for 10 seconds",
              "Major Healing abilities restore +52 for 10 seconds" },

            { "Disrupting Bash causes the target to take +10% damage from Crushing attacks for 8 seconds",
              "Target's Crushing Vulnerability +10% for 8 seconds" },

            { "Vigorous Defense boosts your Sprint Speed +7 for 15 seconds",
              "Sprint Speed +7 for 15 seconds" },

            { "Shield Team causes all targets' Survival Utility abilities to restore 100 Armor to them. Lasts 20 seconds",
              "All targets' Survival Utility abilities restore 100 Armor for 20 seconds" },

            { "Reinforce boosts your Nice Attack Damage +160 for 9 seconds",
              "Nice Attack Damage +160 for 9 seconds" },

            { "Take the Lead heals you for 96 Health after a 15 second delay",
              "Restores 96 Health after a 15 second delay" },

            { "Take the Lead boosts your sprint speed by an additional +5 and you recover 36 Power after a 15 second delay",
              "+5 sprint speed and you recover 36 Power after a 15 second delay" },

            { "Take the Lead boosts the taunt of all your attacks +160%",
              "+160% Taunt for all attacks" },

            { "Finish It Restores 62 Health",
              "Restores 62 Health" },

            { "All Shield Bash Abilities deal +50 damage and hasten the current reuse timer of Finish It by 2 seconds",
              "Hastens current reuse timer of Finish It by 2 seconds" },

            { "Fight Me You Fools boosts Core Attack Damage +200 for 6 seconds",
              "Core Attack Damage +200 for 6 seconds" },

            { "All Shield Bash Abilities deal +50 damage and hasten the current reuse timer of Fight Me You Fools by 2 seconds",
              "Hastens current reuse timer of Fight Me You Fools by 2 seconds" },

            { "Fire Shield boosts your direct and indirect Cold mitigation +16 for 20 seconds",
              "Direct and Indirect Cold Mitigation +16" },

            { "Fire Shield boosts your direct and indirect Fire mitigation +16 for 20 seconds",
              "Direct and Indirect Fire Mitigation +16" },

            { "Shrill Command deals +36% damage and hastens the current reuse timer of Clever Trick by 2 seconds",
              "Hastens the current reuse timer of Clever Trick by 2 seconds" },

            { "Shrill Command deals +34% damage and shortens the current reuse time of Sic 'Em by 1 second",
              "Shortens the current reuse timer of Sic 'Em by 1 second" },

            { "Shrill Command deals +25% damage and reduces the target's Rage by -350",
              "Shrill Command deals +25% damage and reduces the target's Rage by -350" },

            { "Monstrous Rage boosts your Slashing attack damage +18% for 8 seconds",
              "Your Slashing attacks deal +18% damage for 8 seconds" },

            { "Monstrous Rage boosts your Crushing attack damage +19% for 8 seconds",
              "Your Crushing attacks deal +19% damage for 8 seconds" },

            { "Monstrous Rage and Unnatural Wrath boost your pet's next attack damage +113",
              "Your pet's next attack deals +113 damage" },

            { "When you use Sic Em, your sprint speed increases by +9 for 10 seconds",
              "+9 sprint speed for 10 seconds" },

            { "Sic Em boosts your pet's Slashing attacks (if any) +85 damage for 10 seconds",
              "Your pet's Slashing attacks (if any) deal +85 damage for 10 seconds" },

            { "Sic Em boosts your pet's Crushing attacks (if any) +85 damage for 10 seconds",
              "Your pet's Crushing attacks (if any) deal +85 damage for 10 seconds" },

            { "Sic Em gives both you and your pet +32 Accuracy for 10 seconds",
              "Both you and your pet gain +32 Accuracy for 10 seconds" },

            { "Sic Em causes your pet's attacks to generate -221 Rage for 10 seconds",
              "Your pet's attacks generate -221 Rage for 10 seconds" },

            { "Sic 'Em restores 46 Health to both you and your pet",
              "Restores 46 Health to both you and your pet" },

            { "Get It Off Me increases your pet's Taunt an additional +280%",
              "Your pet's attacks taunt +280%" },

            { "Get It Off Me restores 114 Armor to you",
              "Restores 114 Armor to you" },

            { "Get It Off Me heals you for 160 Health after a 15 second delay",
              "Restores 160 Health to you after a 15 second delay" },

            { "Feed Pet restores 140 Health (or Armor if Health is full) to your pet after a 20 second delay",
              "Restores 140 Health/Armor to your pet after a 20 second delay" },

            { "Feed Pet restores 80 Armor to your pet and hastens the current reuse timer of Clever Trick by -4.5 second",
              "Restores 80 Armor to your pet and hastens the current reuse timer of Clever Trick by -4.5 seconds" },

            { "Wild Endurance heals your pet for 120 Health (or Armor if Health is full)",
              "Restores 120 Health/Armor to your pet" },

            { "Nimble Limbs heals your pet for 101 Health (or Armor if Health is full)",
              "Restores 101 Health/Armor to your pet" },

            { "Using Unnatural Wrath on your pet heals you for 38 Health (or Armor if Health is full)",
              "Restores 38 Health/Armor to you" },

            { "Unnatural Wrath causes your pet to bleed for 160 trauma damage over 10 seconds, but also deal +144 damage per attack during that time",
              "Pet bleeds for 160 trauma damage over 10 seconds, but also deal +144 damage per attack during that time" },

            { "Unnatural Wrath grants your pet +51% mitigation versus direct attacks for 14 seconds. After 15 seconds, the pet takes 160 psychic damage. (You can negate the latent psychic damage by using First Aid 4+ on your pet.)",
              "Your pet gains +51% mitigation versus direct attacks for 14 seconds. After 15 seconds, the pet takes 160 psychic damage" },

            { "Wild Endurance gives your pet complete stun immunity and +8 Health/Armor healing per second for 15 seconds",
              "Your pet gains complete stun immunity and restores 8 Health/Armor per second for 15 seconds" },

            { "After using Wild Endurance, your next use of Feed Pet restores +120 Health/Armor",
              "Your next use of Feed Pet restores +120 Health/Armor" },

            { "Nimble Limbs grants your pet +16 mitigation vs. physical (slashing, piercing, and crushing) attacks for 15 seconds",
              "Your pet gains +16 mitigation vs. physical (slashing, piercing, and crushing) attacks for 15 seconds" },

            { "That'll Do restores 72 Health to your pet and 32 Power to you",
              "Restores 72 Health to your pet and 32 Power to you" },

            { "Animal Handling pets have +118 Max Health",
              "+118 Max Health" },

            { "Animal Handling pets have +151 Max Armor",
              "+151 Max Armor" },

            { "Animal Handling pets' Sic 'Em and Clever Trick attacks deal +100 damage",
              "Pet's Sic 'Em and Clever Trick direct damage +100" },

            { "Animal Handling pets' Sic 'Em attacks deal +16% damage",
              "Sic 'Em direct damage +16%" },

            { "Animal Handling pets' Sic 'Em abilities taunt +1200",
              "Pet's Sic 'Em taunts +1200" },

            { "Animal Handling pets have +64 Enthusiasm (which boosts XP earned and critical-hit chance)",
              "+64 Enthusiasm" },

            { "Animal Handling pets taunt as if they did +160% additional damage",
              "Taunt +160%" },

            { "Animal Handling pets taunt their opponents 32% less",
              "Taunt -32%" },

            { "Animal Handling pets' damage-over-time effects (if any) deal +130% damage per tick",
              "Damage over Time +130% per tick" },

            { "Animal Handling pets absorb some direct damage based on their remaining Armor (absorbing 0% when armor is empty, up to 20% when armor is full)",
              "Up to +20% direct damage mitigation based on remaining Armor" },

            { "Animal Handling pets have +48% Death Avoidance (ignores a fatal attack once; resets after 15 minutes)",
              "+48% Death Avoidance" },

            { "Animal Handling pets recover +17 Armor every five seconds (whether in combat or not)",
              "+17 Armor Regeneration" },

            { "Animal Handling pets' healing abilities, if any, restore +45% health",
              "Healing Abilities +45%" },

            { "Nimble Limbs gives pet +19% melee evasion for 30 seconds",
              "+19% Melee Evasion" },

            { "Animal Handling pets' Clever Trick abilities deal +245 damage",
              "Clever Trick direct damage +245" },

            { "Animal Handling pets' Clever Trick abilities deal +20% damage",
              "Clever Trick direct damage +20%" },

            { "For 17 seconds after using Clever Trick, pets' basic attacks have a 15% chance to deal double damage",
              "For 17 seconds, pets' basic attacks have a 15% chance to deal double damage" },

            { "Animal Handling pets' basic attacks deal +13% damage",
              "Pet basic attack direct damage +13%" },

            { "Way of the Hammer boosts Slashing and Piercing Damage +34% for 10 seconds",
              "Also boosts Slashing and Piercing Damage +34% for 10 seconds" },

            { "Seismic Impact hits all targets within 8 meters and deals +17% damage",
              "Hits all enemies within 8 meters" },

            { "Seismic Impact restores 80 Armor to you",
              "Restores 80 Armor" },

            { "Seismic Impact deals +58% damage to targets that are Knocked Down",
              "+58% damage to targets that are Knocked Down" },

            { "Way of the Hammer restores 80 Armor to all targets",
              "Restores 80 Armor to all targets" },

            { "Pound To Slag restores 120 health to you",
              "Restores 120 Health" },

            { "Pound To Slag deals +32% damage and hits all enemies within 5 meters, but reuse time is +10 seconds and Power cost is +35%",
              "Hits all enemies within 5 meters" },

            { "Pound To Slag deals +110 damage and hastens the current reuse timer of Look at My Hammer by 5 seconds",
              "Hastens the current reuse timer of Look at My Hammer by 5 seconds" },

            { "Way of the Hammer boosts all targets' Electricity Damage +35% for 10 seconds",
              "Also boosts Electricity Damage +35% for 10 seconds" },

            { "Pound To Slag deals +512 damage if target's Rage is at least 66% full",
              "+512 damage if target's Rage is at least 66% full" },

            { "Look At My Hammer reduces the damage you take from Slashing, Piercing, and Crushing attacks by 42 for 5 seconds",
              "Boosts Slashing, Piercing, and Crushing Mitigation +42 for 5 seconds" },

            { "After using Look At My Hammer, all other Hammer attacks cost -10 Power for 8 seconds",
              "After using Look At My Hammer, all other Hammer attacks cost -10 Power for 8 seconds" },

            { "After using Look At My Hammer, all other Hammer attacks cost -9 Power for 10 seconds",
              "After using Look At My Hammer, all other Hammer attacks cost -9 Power for 10 seconds" },

            { "After using Look At My Hammer, all other Hammer attacks cost -8 Power for 12 seconds",
              "After using Look At My Hammer, all other Hammer attacks cost -8 Power for 12 seconds" },

            { "Leaping Smash and Latent Charge boost your Core Attack damage +91 for 6 seconds",
              "Core Attack damage +91 for 6 seconds" },

            { "Leaping Smash restores 52 Armor to you",
              "Restores 52 Armor" },

            { "Rib Shatter deals +170 damage to targets that are knocked down",
              "+170 damage to targets that are knocked down" },

            { "Way of the Hammer grants all targets +14 Direct Mitigation for 10 seconds",
              "+14 Direct Mitigation for 10 seconds" },

            { "Rib Shatter and Leaping Smash Damage +44% if target's Rage is at least 66% full",
              "+44% damage if target's Rage is at least 66% full" },

            { "Thunderstrike heals you for 39 health",
              "Restores 39 Health" },

            { "Thunderstrike deals +18% damage and knocks all targets back",
              "Knocks all targets backward" },

            { "Discharging Strike deals +8.5% damage plus 53% more damage if target's Rage meter is at least 66% full",
              "+53% damage if target's Rage meter is at least 66% full" },

            { "Discharging Strike and Latent Charge boost your Epic Attack damage +90 for 15 seconds",
              "Epic Attack damage +90 for 15 seconds" },

            { "Hurl Lightning deals +20% damage and applies Moderate Concussion status: target is prone to random self-stuns",
              "Applies Moderate Concussion status: target is prone to random self-stuns" },

            { "Reckless Slam boosts your direct damage mitigation +16 for 5 seconds",
              "+16 direct damage mitigation for 5 seconds" },

            { "Reckless Slam and Reverberating Strike boost your Nice Attack Damage +66 for 9 seconds",
              "Nice Attack Damage +66 for 9 seconds" },

            { "Latent Charge deals +80 direct damage. In addition, the target takes a second full blast of delayed Electricity damage after an 8-second delay",
              "Target suffers a second blast of Electricity damage after 8 seconds" },

            { "Pulse of Life restores 80 Health over 15 seconds",
              "Restores 80 Health over 15 seconds" },

            { "Heart Thorn restores 74 armor to you",
              "Restores 74 Armor" },

            { "Pulse of Life gives +18 Fire, Cold, and Electricity Mitigation (direct and indirect) for 15 seconds",
              "Grants +18 Universal Fire, Cold, and Electricity Mitigation for 15 seconds" },

            { "Rotskin hits all targets within 10 meters and further debuffs their mitigation -48",
              "Hits all targets within 10 meters" },

            { "Rotskin hastens the current reuse timer of Regrowth by 5 seconds",
              "Hastens the current reuse timer of Regrowth by 5 seconds" },

            { "Rotskin deals +20% damage and boosts your Nice Attack Damage +83 for 10 seconds",
              "Nice Attack Damage +83 for 10 seconds" },

            { "Brambleskin increases your Max Health by +69 for 30 seconds and heals 69 Health",
              "Max Health +69 for 30 seconds (and restores 69 Health)" },

            { "Brambleskin increases your Max Armor by +95 for 30 seconds and restores 95 Armor",
              "Max Armor +95 for 30 seconds (and restores 95 Armor)" },

            { "Brambleskin increases your Max Power by +40 for 30 seconds and restores 40 Power",
              "Max Power +40 for 30 seconds (and restores 40 Power)" },

            { "Cloud Sight causes target's attacks to have +5% more chance of missing, but Power cost is +15%",
              "+5% Miss Chance" },

            { "Cosmic Strike deals +15% damage and boosts your Major Healing +80 for 10 seconds",
              "Major Healing Boost +80 for 10 seconds" },

            { "Fill With Bile heals 76 health and 76 armor",
              "Restores 76 Health and 76 Armor" },

            { "Fill With Bile increases target's Max Health by +76 for 3 minutes and heals 76 health",
              "Max Health by +76 for 3 minutes (and restores 76 Health)" },

            { "Regrowth restores 48 Power",
              "Restores 48 Power" },

            { "Regrowth restores +44 Health and conjures a magical field on the target that mitigates 10% of all physical damage they take for 1 minute (or until 100 damage is mitigated)",
              "Conjures a magical field on the target that mitigates 10% of all physical damage they take for 1 minute (or until 100 damage is mitigated)." },

            { "Regrowth restores +35 Health and causes your Minor Heals to restore +46 Health for 10 seconds",
              "Minor Healing Restores +46 Health for 10 seconds" },

            { "Energize restores 97 armor to each target",
              "Restores 97 Armor to each target" },

            { "Energize restores +20 Health and conjures a magical field that mitigates 10% of all physical damage they take for 1 minute (or until 100 damage is mitigated).",
              "Conjures a magical field that mitigates 10% of all physical damage they take for 1 minute (or until 100 damage is mitigated)." },

            { "Your Healing Sanctuary restores +27 health with each heal",
              "Sanctuary restores +27 Health with each heal" },

            { "Your Healing Sanctuary restores +40 Armor with each heal",
              "Sanctuary restores +40 Armor with each heal" },

            { "Your Healing Sanctuary restores +16 Power with each heal",
              "Sanctuary restores +16 Power with each heal" },

            { "Fill With Bile increases target's direct Poison damage +51",
              "Direct Poison damage +51" },

            { "Your Healing Sanctuary heals +19 health and buffs Melee Accuracy +12",
              "Sanctuary restores +19 health; Buffs Melee Accuracy +12" },

            { "Ice Spear deals between +1 and +245 extra damage (randomly determined)",
              "+1 to 245 damage (randomly determined)" },

            { "Ice Spear heals you for 46 health after a 15 second delay",
              "Restores 46 Health after a 15 second delay" },

            { "All Ice Magic abilities that hit multiple targets have a 20% chance to deal +50% damage",
              "20% chance to deal +50% damage" },

            { "All Ice Magic attacks that hit a single target have a 33% chance to deal +48% damage",
              "33% chance to deal +48% damage" },

            { "Chill causes target to take +16% damage from Crushing attacks for 6 seconds, but reset time of Chill is increased +4 seconds",
              "Target takes +16% damage from Crushing attacks for 6 seconds" },

            { "You regain 23 Power after using Ice Nova or Shardblast",
              "Restores 23 Power" },

            { "You regain 36 Health when using Ice Nova or Shardblast",
              "Restores 36 Health" },

            { "Ice Nova restores 55 Armor to you",
              "Restores 55 Armor" },

            { "Ice Armor restores 170 Armor over 30 seconds",
              "Restores 170 Armor over 30 seconds" },

            { "Ice Armor restores 80 Power over 30 seconds",
              "Restores 80 Power over 30 seconds" },

            { "Ice Armor instantly restores 79 Armor, and Fire damage no longer dispels your Ice Armor",
              "Restores 79 Armor; Fire damage no longer dispels Ice Armor" },

            { "Ice Armor boosts direct and indirect Trauma Mitigation +42 and all attacks taunt +20%",
              "Trauma Mitigation +42;  all attacks taunt +20%" },

            { "Ice Armor boosts Cold attack damage +26",
              "Cold attack damage +26" },

            { "Freeze Solid restores 133 armor to you after a 15 second delay",
              "Restores 133 Armor to you after a 15 second delay" },

            { "Freeze Solid reduces the Power cost of all Ice Magic abilities -11 for 7 seconds",
              "Reduces the Power cost of all Ice Magic abilities -11 for 7 seconds" },

            { "Freeze Solid resets the timer on Ice Spear (so it can be used again immediately)",
              "Resets the timer on Ice Spear (so it can be used again immediately)" },

            { "Frostbite causes target's attacks to deal -18 damage",
              "Target's attacks deal -18 damage" },

            { "Frostbite deals +118 damage and raises the target's Max Rage by 67%, preventing them from using their Rage attacks as often",
              "Raises the target's Max Rage by 67%, preventing them from using their Rage attacks as often" },

            { "Frostbite debuffs target so that 11% of their attacks miss and have no effect",
              "11% of target's attacks miss and have no effect" },

            { "Tundra Spikes deals 220 armor damage and taunts +600",
              "+220 armor damage" },

            { "Tundra Spikes stuns all targets after a 10 second delay",
              "Stuns all targets after a 10 second delay" },

            { "Tundra Spikes deals +19% damage, gains +8 Accuracy, and lowers targets' Evasion by -16 for 20 seconds",
              "+8 Accuracy; lowers targets' Evasion by -16 for 20 seconds" },

            { "Blizzard has a 75% chance to cause all sentient targets to flee in terror",
              "75% chance to cause all sentient targets to flee in terror" },

            { "You regain 60 Health when using Blizzard",
              "Restores 60 Health to you" },

            { "Blizzard deals 248 armor damage and generates -120 Rage",
              "+248 armor damage" },

            { "Ice Lightning boosts your Core Attack Damage +85 for 7 seconds",
              "Core Attack Damage +85 for 7 seconds" },

            { "Ice Lightning causes the target to become 17% more vulnerable to Fire attacks for 7 seconds",
              "Target's Direct Fire Vulnerability +17% for 7 seconds" },

            { "Cryogenic Freeze restores 135 Health",
              "Restores 135 Health" },

            { "Cryogenic Freeze restores 201 Armor",
              "Restores 201 Armor" },

            { "Cryogenic Freeze restores 85 Power",
              "Restores 85 Power" },

            { "While in Cryogenic Freeze, you are 100% resistant to Fire damage",
              "Fire Resistance +100%" },

            { "While in Cryogenic Freeze, you are 100% resistant to Poison damage",
              "Poison Resistance +100%" },

            { "Ice Veins heals 120 Health over 10 seconds",
              "Restores 120 Health over 10 seconds" },

            { "Your Cold Sphere's attacks deal +26 damage and taunt -45%",
              "Pet Damage +26 and Taunt -45%" },

            { "Your Cold Sphere gains 128 Armor",
              "+128 Max Armor" },

            { "Your Cold Sphere's Rage attack deals +260 damage",
              "Pet's Rage Attack Damage +260" },

            { "Shardblast resets the timer on Ice Armor (so it can be used again immediately)",
              "Resets the timer on Ice Armor (so it can be used again immediately)" },

            { "Opening Thrust heals you for 14 health",
              "Restores 14 Health" },

            { "For 5 seconds after using Opening Thrust, all knife abilities with 'Cut' in their name deal +24 damage",
              "For 5 seconds, all knife abilities with 'Cut' in their name deal +24 damage" },

            { "Opening Thrust has a 25% chance to cause all Knife abilities WITHOUT 'Cut' in their name to have a 32.5% chance to deal +35% damage for 10 seconds",
              "25% chance to cause all Knife abilities WITHOUT 'Cut' in their name to have a 32.5% chance to deal +35% damage for 10 seconds" },

            { "Marking Cut causes target to take +24% damage from Trauma attacks for 10 seconds",
              "Target takes +24% damage from Trauma attacks for 10 seconds" },

            { "Marking Cut deals +52 armor damage and does not cause the target to shout for help",
              "+52 armor damage; this attack does not cause the target to shout for help" },

            { "Blur Cut restores 40 Health after a 15 second delay",
              "Restores 40 Health after a 15 second delay" },

            { "Blur Cut boosts Burst Evasion by 24% for 8 seconds",
              "Burst Evasion +24% for 8 seconds" },

            { "Blur Cut grants a 37% chance to ignore stuns for 8 seconds",
              "Chance to Ignore Stuns +37% for 8 seconds" },

            { "Poisoner's Cut has a 50% chance to deal +115% damage",
              "50% chance to deal +115% damage" },

            { "Poisoner's Cut boosts Indirect Poison Damage an additional +16 per tick",
              "Indirect Poison +16 for 5 seconds" },

            { "Fending Blade restores 24 Health to you immediately and reduces the target's Rage by 320 after a 5 second delay",
              "Restores 24 Health; Reduces Rage by 320 after a 5 second delay" },

            { "Fending Blade restores 22 Power",
              "Restores 22 Power" },

            { "Slice has a 40% chance to deal +45% damage and restore 85 armor",
              "40% chance to deal +45% damage and restore 85 Armor" },

            { "Slice ignores mitigation from armor and deals +76 damage",
              "Ignores mitigation from armor" },

            { "Venomstrike has a 46% chance to stun the target and deal +48 damage",
              "46% chance to stun the target and deal +48 damage" },

            { "Backstab steals 97 health from the target and gives it to you",
              "Steals 97 Health from the target and gives it to you" },

            { "Surge Cut restores +75 Health to you",
              "Restores +75 Health" },

            { "Surge Cut restores 96 Armor to you",
              "Restores 96 Armor" },

            { "Hamstring Throw deals +117 direct health damage",
              "+117 direct health damage" },

            { "Hamstring Throw deals +85 direct health damage and causes the target to take +15% damage from Trauma for 20 seconds",
              "+85 direct health damage; target takes +15% damage from Trauma for 20 seconds" },

            { "Surprise Throw deals +35% damage and stuns the target if they are not focused on you",
              "Stuns the target if they are not focused on you" },

            { "Surprise Throw restores 70 Power if the target is not focused on you",
              "Restores 70 Power if the target is not focused on you" },

            { "Fan of Blades deals +15% damage to all targets and knocks them backwards",
              "Knocks targets backwards" },

            { "Fan of Blades deals +76 damage and causes targets to take +20% damage from Poison for 30 seconds",
              "Target's Poison Vulnerability +20% for 30 seconds" },

            { "All bard songs restore 22 Health to YOU every 4 seconds",
              "Restores +22 Health to YOU every 4 seconds" },

            { "All bard songs restore 35 Armor to YOU every 4 seconds",
              "Restores +35 Armor to YOU every 4 seconds" },

            { "Song of Discord deals +21 damage and has a 5% chance to stun each target every 2 seconds",
              "5% chance to stun each target every 2 seconds" },

            { "Song of Discord reduces targets' Rage by -130 every 2 seconds",
              "Reduces targets' Rage by -130 every 2 seconds" },

            { "Song of Discord has a 45% chance to deal +25% damage to each target every 2 seconds",
              "45% chance to deal +25% damage to each target every 2 seconds" },

            { "Song of Resurgence also restores 8 Power every 4 seconds to each target in range",
              "Restores 8 Power to each target in range every 4 seconds" },

            { "While playing Song of Resurgence, your Major Healing abilities restore +50 Health",
              "Major Healing abilities restore +50 Health" },

            { "Song of Bravery has a 15% chance every 4 seconds to grant listeners a Moment of Bravery: all attacks deal +25% damage for 5 seconds",
              "15% chance every 4 seconds to grant listeners a Moment of Bravery: all attacks deal +25% damage for 5 seconds" },

            { "Song of Bravery boosts allies' Basic Attack and Core Attack damage +55",
              "Allies' Basic Attack and Core Attack Damage +55" },

            { "Song of Bravery causes allies' Combat Refreshes to restore +76 Armor",
              "Allies' Combat Refreshes restore +76 Armor" },

            { "Blast of Fury deals +42% damage and knocks the target back, but the ability's reuse timer is +2 seconds",
              "Knocks targets backwards" },

            { "Blast of Fury deals 160 Armor damage and restores 35 Armor to you",
              "Deals 160 Armor damage, Restores 35 Armor to you" },

            { "Blast of Despair causes your Nice Attacks to deal +115 damage for 10 seconds",
              "Nice Attacks Damage +115 for 10 seconds" },

            { "Blast of Despair restores 34 Armor to you",
              "Restores 34 Armor" },

            { "Thunderous Note causes the target to take +13% damage from Nature attacks for 15 seconds",
              "Target takes +13% damage from Nature for 15 seconds" },

            { "Rally restores 47 Power",
              "Restores 47 Power" },

            { "Rally restores 170 Armor after a 20 second delay",
              "Restores 170 Armor after a 20 second delay" },

            { "Anthem of Avoidance gives all targets +23% Burst Evasion for 8 seconds",
              "Grants +23% Burst Evasion for 8 seconds" },

            { "Anthem of Avoidance gives all targets +18% Melee Evasion for 8 seconds",
              "Grants +18% Melee Evasion for 8 seconds" },

            { "Anthem of Avoidance grants all targets immunity to Knockbacks for 8 seconds",
              "Grants immunity to Knockbacks for 8 seconds" },

            { "Anthem of Avoidance hastens the current reuse timer of Rally by 5 seconds",
              "Hastens the current reuse timer of Rally by 5 seconds" },

            { "Entrancing Lullaby deals 450 Trauma damage after a 20 second delay",
              "Deals 450 Trauma damage after a 20 second delay" },

            { "Virtuoso's Ballad restores 61 Power",
              "Restores 61 Power" },

            { "Virtuoso's Ballad restores 160 Armor",
              "Restores 160 Armor" },

            { "Moment of Resolve dispels any Stun effects on allies and grants them immunity to Stuns for 8 seconds",
              "Dispels stuns and grants immunity to new stun effects for 8 seconds" },

            { "Moment of Resolve dispels any Slow or Root effects on allies and grants them immunity to Slow and Root effects for 8 seconds",
              "Dispels slow and root effects and grants immunity to new slow and root effects for 8 seconds" },

            { "Moment of Resolve boosts targets' Movement Speed +3 for 8 seconds",
              "Movement Speed +3 for 8 seconds" },

            { "Disharmony causes target to deal -8 damage with their next attack",
              "Target's next attack deals -8 damage" },

            { "Cow's Front Kick has a 66% chance to deal +132 damage",
              "66% chance to deal +132 damage" },

            { "Stampede boosts the damage of future Stampede attacks by +38 for 60 seconds (stacks up to 15 times)",
              "Future Stampede attack damage +38 for 60 seconds" },

            { "Cow's Bash restores 33 Power to you",
              "Restores 33 Power" },

            { "Cow's Bash heals you for 50 health",
              "Restores 50 Health" },

            { "Cow's Front Kick has a 50% chance to hit all enemies within 5 meters and deal +68 damage",
              "50% chance to hit all enemies within 5 meters and deal +68 damage" },

            { "Cow's Front Kick causes the next attack that hits you to deal -39% damage",
              "The next attack that hits you deals -39% damage" },

            { "Cow's Bash boosts your Nice Attack damage +160 for 9 seconds",
              "Nice Attack damage +160 for 9 seconds" },

            { "Stampede boosts your Slashing/Crushing/Piercing Mitigation vs. Elites +6 for 30 seconds (stacks up to 5 times)",
              "Slashing/Crushing/Piercing Mitigation vs. Elites +6 for 30 seconds (stacks up to 5 times)" },

            { "Moo of Calm heals +80 health",
              "Restores +80 Health" },

            { "Moo of Calm restores +50 power",
              "Restores +50 Power" },

            { "Moo of Calm restores +100 armor",
              "Restores +100 Armor" },

            { "For 30 seconds after you use Moo of Calm, any internal (Poison/Trauma/Psychic) attacks that hit you are reduced by 32. This absorbed damage is added to your next Stampede attack at a 200% rate.",
              "For 30 seconds, any internal (Poison/Trauma/Psychic) attacks that hit you are reduced by 32. This absorbed damage is added to your next Stampede attack at a 200% rate." },

            { "Graze boosts your out-of-combat sprint speed by 8.5 for 30 seconds",
              "Out-of-Combat Sprint Speed +8.5 for 30 seconds" },

            { "Chew Cud increases your mitigation versus Crushing, Slashing, and Piercing attacks +14 for 10 seconds",
              "Crushing/Slashing/Piercing Mitigation +14 for 10 seconds" },

            { "Chew Cud increases your mitigation versus all attacks by Elites +14 for 10 seconds",
              "Mitigation vs. Elites +14 for 10 seconds" },

            { "Clobbering Hoof attacks have a 50% chance to deal +102% damage",
              "50% chance to deal +102% damage" },

            { "Moo of Determination restores +110 armor",
              "Restores +110 Armor" },

            { "Moo of Determination restores 144 Health over 9 seconds",
              "Restores 144 Health over 9 seconds" },

            { "For 30 seconds after you use Moo of Determination, any physical (Slashing/Piercing/Crushing) attacks that hit you are reduced by 24. This absorbed damage is added to your next Front Kick.",
              "Any physical (Slashing/Piercing/Crushing) attacks that hit you are reduced by 24. This absorbed damage is added to your next Front Kick." },

            { "Tough Hoof immediately restores 73 armor",
              "Restores 73 Armor" },

            { "Tough Hoof has a 66% chance to deal +78% damage and taunt +700",
              "66% chance to deal +78% damage and taunt +700" },

            { "Deadly Emission Damage +46 and Targets are Knocked Backwards",
              "Knocks target backwards" },

            { "Deer Kick implants insect eggs in the target. Future Deer Kicks by any deer cause target to take 310 Nature damage over 5 seconds",
              "Subsequent Deer Kicks to this target deal 310 Nature damage over 5 seconds" },

            { "Deer Bash deals +120 damage and knocks the enemy backwards",
              "Knocks target backwards" },

            { "Deer Bash has a 60% chance to deal +110% damage",
              "60% chance to deal +110% damage" },

            { "Deer Bash heals 38 health",
              "Restores 38 Health" },

            { "After using Doe Eyes, your next attack deals +169 damage",
              "Your next attack deals +169 damage" },

            { "Doe Eyes restores 95 armor",
              "Restores 95 Armor" },

            { "Doe Eyes restores 42 power",
              "Restores 42 Power" },

            { "Doe Eyes heals 64 health",
              "Restores 64 Health" },

            { "Cuteness Overload heals you for 84 health",
              "Restores 84 Health" },

            { "Cuteness Overload heals you for 72 health and increases your movement speed by +6 for 8 seconds",
              "Restores 72 Health; Increases movement speed +6 for 8 seconds" },

            { "Cuteness Overload restores 135 armor to you",
              "Restores 135 Armor" },

            { "King of the Forest has a 90% chance to deal +160 damage",
              "90% chance to deal +160 damage" },

            { "King of the Forest gives you +15 mitigation of any physical damage for 20 seconds",
              "Slashing/Piercing/Crushing Mitigation +15 for 20 seconds" },

            { "Pummeling Hooves has a 60% chance to deal +66% damage and taunt +400",
              "60% chance to deal +66% damage and taunt +400" },

            { "Bounding Escape heals you for 72 health",
              "Restores 72 Health" },

            { "Bounding Escape restores 117 armor to you",
              "Restores 117 Armor" },

            { "Bounding Escape restores 44 power to you",
              "Restores 44 Power" },

            { "Bounding Escape grants you +42% Projectile Evasion for 10 seconds",
              "Projectile Evasion +42% for 10 seconds" },

            { "Antler Slash restores 8 power to you",
              "Restores 8 Power" },

            { "Antler Slash heals you for 16 health",
              "Restores 16 Health" },

            { "Antler Slash has a 50% chance to restore 40 armor",
              "50% chance to restore 40 Armor" },

            { "Forest Challenge raises Max Health +52 for 60 seconds (and heals +52)",
              "Max Health +52 for 60 seconds" },

            { "Pig Bite has a 44% chance to deal +40 damage and hit all targets within 5 meters",
              "44% chance to deal +40 damage and hit all targets within 5 meters" },

            { "Pig Bite restores 16 Health",
              "Restores 16 Health" },

            { "Grunt of Abeyance restores 28 Power to all targets",
              "Restores 28 Power to all targets" },

            { "Grunt of Abeyance restores 61 Armor to all targets",
              "Restores 61 Armor to all targets" },

            { "Grunt of Abeyance grants all targets 20% mitigation from attacks, up to a maximum of 200 total mitigated damage.",
              "All targets gain 20% mitigation from attacks, up to a maximum of 200 total mitigated damage." },

            { "Strategic Chomp boosts your mitigation versus physical damage +8 for 20 seconds",
              "Slashing/Piercing/Crushing Mitigation +8 for 20 seconds" },

            { "Strategic Chomp restores 21 Power",
              "Restores 21 Power" },

            { "Pig Rend has a 60% chance to deal +84% damage",
              "60% chance to deal +84% damage" },

            { "Squeal boosts sprint speed by 10 for 10 seconds",
              "Sprint Speed +10 for 10 seconds" },

            { "Squeal uniformly diminishes all targets' entire aggro lists by 36%, making them less locked in to their aggro choices and more easily susceptible to additional taunts and detaunts",
              "Uniformly diminishes all targets' entire aggro lists by 36%" },

            { "Mudbath restores 121 armor to the target",
              "Restores 121 Armor" },

            { "Mudbath gives the target +12 absorption of any physical damage for 20 seconds",
              "Physical Damage Mitigation +12 for 20 seconds" },

            { "Mudbath causes the target to take 19% less damage from all attacks for 10 seconds",
              "Universal Damage Mitigation +19% for 10 seconds" },

            { "Harmlessness heals you for 81 health",
              "Restores 81 Health" },

            { "Harmlessness restores 126 armor to you",
              "Restores 126 Armor" },

            { "Harmlessness restores 52 power to you",
              "Restores 52 Power" },

            { "Harmlessness confuses the target about which enemy is which, permanently shuffling their hatred levels toward all enemies they know about",
              "Confuses the target about which enemy is which, permanently shuffling their hatred levels toward all enemies they know about" },

            { "Pig Punt causes the target to ignore you for 10 seconds, or until you attack it again",
              "Target ignores you for 10 seconds, or until you attack it again" },

            { "Pig Punt has a 35% chance to confuse the target about which enemy is which, permanently shuffling their hatred levels toward all enemies they know about",
              "35% chance to confuse the target about which enemy is which, permanently shuffling their hatred levels toward all enemies they know about" },

            { "Pig Punt deals +20 damage and slows target's movement by 45%",
              "Slows target's movement by 45%" },

            { "For 15 seconds, Frenzy boosts targets' receptivity to Major Heals so that they restore +51 Health",
              "Healing from Major Heals +51 for 15 seconds" },

            { "Frenzy restores 30 power to all targets",
              "Restores 30 Power" },

            { "Frenzy gives all targets +11 absorption of any physical damage for 20 seconds",
              "Physical Damage Mitigation +11 for 20 seconds" },

            { "For 10 seconds, Frenzy boosts targets' indirect damage +8",
              "Universal Indirect Damage +8 for 10 seconds" },

            { "Porcine Alertness gives all targets +39 Accuracy for 20 seconds",
              "Accuracy +39 for 20 seconds" },

            { "Porcine Alertness restores 55 armor to all targets",
              "Restores 55 Armor" },

            { "Porcine Alertness gives all targets +30% chance to ignore Stun effects for 20 seconds",
              "Chance To Ignore Stuns +30% for 20 seconds" },

            { "Porcine Alertness heals all targets for 52 health after a 15 second delay",
              "Restores 52 Health after a 15 second delay" },

            { "Premeditated Doom channeling time is -1 second and boosts your Indirect Poison damage +9 (per tick) for 20 seconds",
              "Indirect Poison damage +9 (per tick) for 20 seconds" },

            { "Spider Bite and Infinite Legs have a 50% chance to deal +40% damage",
              "50% chance to deal +40% damage" },

            { "Infinite Legs has a 20% chance to boost Spider Skill Base Damage +10% for 30 seconds",
              "20% chance to boost Spider Skill Base Damage +10% for 30 seconds" },

            { "Inject Venom has a 50% chance to deal +85% damage",
              "50% chance to deal +85% damage" },

            { "Inject Venom heals you for 40 health",
              "Restores 40 Health" },

            { "Web Trap boosts your movement speed by 8 for 10 seconds",
              "Movement speed +8 for 10 seconds" },

            { "Gripjaw restores 62 Armor to you",
              "Restores 62 Armor" },

            { "Gripjaw has a 70% chance to deal +87% damage",
              "70% chance to deal +87% damage" },

            { "Gripjaw deals +32% damage and hastens the current reset timer of Grappling Web by 5 seconds",
              "Hastens the current reset timer of Grappling Web by 5 seconds" },

            { "Spider Bite and Infinite Legs restore 16 Health",
              "Restores 16 Health" },

            { "For 12 seconds after using Infinite Legs, additional Infinite Legs attacks deal +28 damage",
              "Infinite Legs Damage +28 For 12 seconds" },

            { "Spit Acid causes your Signature Debuff abilities to deal +144 damage for 8 seconds",
              "Signature Debuff Damage +144 for 8 seconds" },

            { "Spit Acid deals +192 armor damage",
              "+192 armor damage" },

            { "Spit Acid raises your Poison Damage +27% for 30 seconds (this buff does not stack with itself)",
              "Poison Damage +27% for 30 seconds (this buff does not stack with itself)" },

            { "Terrifying Bite boosts sprint speed +8 for 10 seconds",
              "Sprint speed +8 for 10 seconds" },

            { "Terrifying Bite causes the target to take +16% damage from Poison attacks",
              "Target takes +16% damage from Poison attacks" },

            { "If you use Premeditated Doom while standing near your Web Trap, you gain +50% Spider Skill Base Damage for 20 seconds",
              "If used while standing near your Web Trap, you gain +50% Spider Skill Base Damage for 20 seconds" },

            { "Premeditated Doom boosts sprint speed +4.5 for 20 seconds",
              "Sprint speed +4.5 for 20 seconds" },

            { "After using Grappling Web, you are immune to Knockback effects for 12 seconds",
              "Grants Knockback Immunity for 12 seconds" },

            { "Grappling Web causes the target to take +16% damage from both Poison (both direct and indirect)",
              "Target takes +16% damage from Poison" },

            { "Rip deals +34 damage and restores 11 Power",
              "Restores 11 Power" },

            { "Rip restores 20 Armor",
              "Restores 20 Armor" },

            { "Tear has a 50% chance to deal +100% damage",
              "50% chance to deal +100% damage" },

            { "Tear has a 33% chance to deal +100% damage and reset the timer on Screech (so Screech can be used again immediately)",
              "33% chance to deal +100% damage and reset the timer on Screech" },

            { "Rip and Tear deal +33 damage and hasten the current reuse timer of Drink Blood by 1 second",
              "Hastens the current reuse timer of Drink Blood by 1 second" },

            { "Wing Vortex has a 70% chance to deal +25% damage and restore 55 Health to you",
              "70% chance to deal +25% damage and restore 55 Health" },

            { "Wing Vortex has a 30% chance to deal +38% damage and stun all targets",
              "30% chance to deal +38% damage and stun all targets" },

            { "Wing Vortex causes targets' next attack to deal -74 damage",
              "Targets' next attack deals -74 damage" },

            { "For 30 seconds after using Drink Blood, all Nature attacks deal +45 damage",
              "Direct Nature Damage +45 for 30 seconds" },

            { "For 30 seconds after using Drink Blood, you gain +26 mitigation vs. Psychic and Trauma damage",
              "Psychic and Trauma Mitigation +26 for 30 seconds" },

            { "Virulent Bite deals 192 Trauma damage over 12 seconds and also has a 25% chance to deal +76% immediate Piercing damage",
              "25% chance to deal +76% Piercing damage" },

            { "Bat Stability heals 78 health",
              "Restores 78 Health" },

            { "Bat Stability provides +45% Projectile Evasion for 10 seconds",
              "Restores 12 Power" },

            { "Screech has a 60% chance to deal +90% damage",
              "60% chance to deal +90% damage" },

            { "Sonic Burst has a 60% chance to deal +100% damage to all targets",
              "60% chance to deal +100% damage" },

            { "Confusing Double heals you for 123 health",
              "Restores 123 Health" },

            { "Confusing Double boosts your movement speed by 3 and your Giant Bat Base Damage by 40% for 15 seconds",
              "Movement speed +3 and Giant Bat Base Damage +40% for 15 seconds" },

            { "Confusing Double restores 112 Power after a 10 second delay",
              "Restores 112 Power after a 10 second delay" },

            { "Your Confusing Double deals +55% damage with each attack",
              "Pet Damage +55%" },

            { "Confusing Double summons an additional figment. Each figment deals +61 damage with each attack",
              "Pet Damage +61" },

            { "Deathscream has a 60% chance to deal +100% damage",
              "60% chance to deal +100% damage" },

            { "While Rabbit skill is active, any Kick ability boosts Melee Evasion +6.5% for 10 seconds",
              "Boosts Melee Evasion +6.5% for 10 seconds (if Rabbit skill is active)" },

            { "Rabbit Scratch deals Trauma damage (instead of Slashing), and deals up to +128 damage (randomly determined)",
              "DamageType:Trauma; +128 random damage" },

            { "Rabbit Scratch restores 16 Armor to you",
              "Restores 16 Armor" },

            { "Thump deals +31 damage and knocks the enemy backwards",
              "Restores 42 Armor" },

            { "Thump causes the target to take +16% damage from Cold attacks for 10 seconds",
              "+8% Physical Damage Reflection for 15 seconds" },

            { "Bun-Fu Blitz causes the target to take +16% damage from Trauma attacks for 20 seconds",
              "Target takes +16% damage from Trauma attacks for 20 seconds" },

            { "Bun-Fu Blitz deals +36 damage and hastens the current reset timer of Thump by 3 seconds",
              "Hastens the current reset timer of Thump by 3 seconds" },

            { "Rabbit's Foot grants you and nearby allies +19% Burst Evasion for 10 seconds",
              "+19% Burst Evasion for 10 seconds" },

            { "Rabbit's Foot grants you and nearby allies +9% Earned Combat XP for 20 seconds",
              "+9% Earned Combat XP for 20 seconds" },

            { "Rabbit's Foot restores 28 Power to you and nearby allies",
              "Restores 28 Power" },

            { "Rabbit's Foot restores 89 Health to you and nearby allies after a 15 second delay",
              "Restores 89 Health after a 15 second delay" },

            { "Hare Dash restores 102 Armor to you",
              "Restores 102 Armor" },

            { "Hare Dash causes your next attack to deal +235 damage if it is a Crushing attack",
              "+235 Crushing damage for next attack" },

            { "Hare Dash grants +12% Melee Evasion for 8 seconds and boosts jump height for 15 seconds",
              "+12% Melee Evasion for 8 seconds, Boosts Jump Height for 15 seconds" },

            { "Hare Dash restores 80 Power over 15 seconds",
              "Restores 80 Power over 15 seconds" },

            { "Play Dead restores 90 Health",
              "Restores 90 Health" },

            { "Play Dead boosts your Psychic attack damage +80 for 20 seconds",
              "+80 Psychic Damage for 20 seconds" },

            { "Play Dead causes all affected enemies to take 330 Psychic damage after a 10-second delay",
              "Targets take 330 Psychic damage after a 10-second delay" },

            { "Play Dead boosts your Nice Attack Damage +288 for 15 seconds",
              "Nice Attack Damage +288 for 15 seconds" },

            { "Long Ear grants you +18% Projectile Evasion for 15 seconds",
              "+18% Projectile Evasion for 15 seconds" },

            { "Carrot Power restores 144 Health after a 12 second delay",
              "Restores 144 Health after a 12 second delay" },

            { "Carrot Power restores 155 Armor",
              "Restores 155 Armor" },

            { "Carrot Power boosts your Cold Damage +32% for 10 seconds",
              "Cold Damage +32% for 10 seconds" },

            { "Carrot Power boosts the damage from all kicks +124 for 10 seconds",
              "Kick damage +124 for 10 seconds" },

            { "Carrot Power boosts your Crushing Damage +32% for 10 seconds",
              "Crushing Damage +32% for 10 seconds" },

            { "Bun-Fu Strike reduces target's rage by 480, then reduces it by 690 more after a 5 second delay",
              "Reduces target's Rage by 690 after a 5 second delay" },

            { "Bun-Fu Strike deals +20% damage and restores 48 Health to you after an 8 second delay",
              "Restores 48 Health after an 8 second delay" },

            { "Bun-Fu Strike deals +49 damage and hastens the current reset timer of Bun-Fu Blitz by 2.5 seconds",
              "Hastens the current reset timer of Bun-Fu Blitz by 2.5 seconds" },

            { "Bun-Fu Blast deals +96 damage and hastens the current reuse timer of Bun-Fu Strike by 3.5 seconds",
              "Hastens the current reuse timer of Bun-Fu Strike by 3.5 seconds" },

            { "Love Tap hastens the current reuse timer of Carrot Power by 3.5 seconds",
              "Hastens the current reuse timer of Carrot Power by 3.5 seconds" },

            { "Admonish boosts your Priest Damage +16 for 10 seconds (this effect does not stack with itself)",
              "Boosts Priest Damage +16 for 10 seconds" },

            { "Admonish makes the target 8% more vulnerable to Psychic damage for 10 seconds (this effect does not stack with itself)",
              "Target is 8% more vulnerable to Psychic damage for 10 seconds (this effect does not stack with itself)" },

            { "When Castigate is used on an undead target, it has a 25% chance to deal +300 damage and stun the target",
              "Vs. Undead, 25% chance to deal +300 damage and stun the target" },

            { "Castigate deals Fire damage instead of Psychic, and deals +72% damage to Aberrations",
              "+72% damage to Aberrations" },

            { "Castigate boosts your Nice Attack Damage +96 for 8 seconds",
              "Nice Attack Damage +96 for 8 seconds" },

            { "For 30 seconds after casting Exhilarate on a target, additional Exhilarates on the same target restore +35 Health",
              "Repeated castings on same target restore +35 Health" },

            { "Exhilarate restores 64 Armor over 8 seconds",
              "Restores 64 Armor over 8 seconds" },

            { "Mend Flesh gives the target +11 mitigation against physical attacks for 12 seconds",
              "+11 slashing/crushing/piercing mitigation for 12 seconds" },

            { "Unfetter grants immunity to Knockback effects for 13 seconds",
              "Immunity to Knockbacks for 13 seconds" },

            { "Unfetter allows free-form movement while leaping, and if the target can fly, fly speed is boosted +2.4 m/s for 20 seconds",
              "Turn while leaping and Fly Speed +2.4 m/s for 20 seconds" },

            { "Unfetter boosts swim speed +3.2 m/s for 20 seconds",
              "Swim Speed +3.2 m/s for 20 seconds" },

            { "Unfetter restores 51 Power over 9 seconds",
              "Restores 51 Power over 9 seconds" },

            { "Corrupt Hate causes the target to deal 288 Psychic damage to themselves the next time they use a Rage attack",
              "Target deals 288 Psychic damage to themselves the next time they use a Rage attack" },

            { "Triage gives the target +26.5% Melee Evasion for 10 seconds",
              "Melee Evasion +26.5% for 10 seconds" },

            { "Triage gives the target +33% Burst Evasion for 10 seconds",
              "Burst Evasion +33% for 10 seconds" },

            { "Triage restores 80 Health over 15 seconds",
              "Restores 80 Health over 15 seconds" },

            { "Remedy removes ongoing Fire effects (up to 33 dmg/sec)",
              "Removes ongoing Fire effects (up to 33 dmg/sec)" },

            { "Remedy restores 20 Armor and mitigates all damage over time by 8 per tick for 10 seconds",
              "Universal Indirect Mitigation +8" },

            { "Remedy costs -16 Power to cast, its reuse timer is -1 second, and it has a 25% chance to mend a broken bone in the target",
              "25% chance to mend a broken bone in the target" },

            { "Give Warmth restores 63 Health and +17 Body Heat",
              "Restores +17 Body Heat" },

            { "Give Warmth causes the target's next attack to deal +208 damage if it is a Fire attack",
              "Boosts target's Fire Damage +208 for one attack" },

            { "Give Warmth boosts the target's fire damage-over-time by +10 per tick for 60 seconds",
              "Indirect Fire Damage +10" },

            { "Warning Jolt restores 16 Armor and boosts the damage of your Core Attacks +49 for 8 seconds",
              "Core Attack Damage +49 for 8 seconds" },

            { "Conditioning Shock causes target's next ability to deal -48 damage",
              "Target's next ability deals -48 direct damage" },

            { "Conditioning Shock deals +68 damage and, if target is a monster, its chance to critically-hit is reduced by 25% for 10 seconds",
              "Target's Critical Hit Chance reduced by 25% for 10 seconds" },

            { "Apprehend causes your Nice Attacks to deal +112 damage for 8 seconds",
              "Nice Attack Damage +112 for 8 seconds" },

            { "Apprehend deals +40 damage and hastens the current reuse timer of Controlled Burn by 2 seconds (so it can be used again more quickly)",
              "Hastens the current reuse timer of Controlled Burn by 2 seconds" },

            { "Stun Trap deals +272 damage to all nearby targets (when it activates)",
              "Trap Damage +272" },

            { "Stun Trap deals +45% damage to all nearby targets (when it activates)",
              "Trap Damage +45%" },

            { "Stun Trap deals +122 damage, and there's a 50% chance you'll place an extra trap",
              "Trap Damage +122" },

            { "Coordinated Assault causes all allies' Melee attacks to cost -10 Power for 30 seconds",
              "All allies' Melee attacks cost -10 Power for 30 seconds" },

            { "Coordinated Assault increases all allies' Max Health +45 for 30 seconds",
              "All allies' Max Health +45 for 30 seconds" },

            { "Coordinated Assault increases all allies' Max Armor +67 for 30 seconds",
              "All allies' Max Armor +67 for 30 seconds" },

            { "Coordinated Assault causes all allies' melee attacks to deal up to +90 damage (randomly determined for each attack) for 30 seconds",
              "Allies' melee attacks deal up to +90 damage (randomly determined) for 30 seconds" },

            { "Coordinated Assault grants all allies +8 direct-damage mitigation and +1.5 out-of-combat sprint speed for 30 seconds",
              "All allies gain +8 direct-damage mitigation and +1.5 out-of-combat sprint speed for 30 seconds" },
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
