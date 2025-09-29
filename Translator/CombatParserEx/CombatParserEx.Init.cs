namespace TranslatorCombatParserEx;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using PgObjects;
using Translator;

internal partial class CombatParserEx
{
    public CombatParserEx(List<object> objectList)
    {
        foreach (object Item in objectList)
            switch (Item)
            {
                case PgAbility AsAbility:
                    AbilityObjectKeyList.Add(AsAbility.Key);
                    break;
                case PgEffect AsEffect:
                    EffectObjectKeyList.Add(AsEffect.Key);
                    break;
                case PgPower AsPower:
                    PowerObjectKeyList.Add(AsPower.Key);
                    break;
                case PgItem AsItem:
                    ItemObjectKeyList.Add(AsItem.Key);
                    break;
                case PgSkill AsSkill:
                    SkillTable.Add(AsSkill.Key, AsSkill);
                    break;
            }

        foreach (KeyValuePair<string, PgSkill> Entry in SkillTable)
            if (Generate.IsCombatSkill(Entry.Value, SkillTable))
                SkillList.Add(Entry.Value);

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

        InitValidAbilityList(SkillTable);
        InitMutationDuration();
    }

    private void InitValidAbilityList(Dictionary<string, PgSkill> skillTable)
    {
        foreach (string Key in AbilityObjectKeyList)
        {
            PgAbility Ability = AbilityFromKey(Key);
            string Skill_Key = FromSkillKey(Ability.Skill_Key);
            PgSkill AbilitySkill = AbilitySkillFromKey(Skill_Key);

            if (Generate.IsCombatSkill(AbilitySkill, skillTable) || AbilitySkill.Key == "Crossbow")
            {
                CombatAbilityList.Add(Ability);

                foreach (AbilityKeyword Keyword in Ability.KeywordList)
                {
                    if (!KeywordToAbilities.TryGetValue(Keyword, out List<PgAbility> Abilities))
                    {
                        Abilities = new List<PgAbility>();
                        KeywordToAbilities.Add(Keyword, Abilities);
                    }

                    Abilities.Add(Ability);
                }
            }
        }

        foreach (KeyValuePair<AbilityKeyword, List<PgAbility>> Entry in KeywordToAbilities)
        {
            bool AllHaveDamage = true;
            bool SomeHaveDamage = false;
            foreach (PgAbility Ability in Entry.Value)
            {
                if (Ability.PvE.RawDamage.HasValue)
                    SomeHaveDamage = true;
                else
                    AllHaveDamage = false;
            }

            if (AllHaveDamage && SomeHaveDamage)
                AbilitiesDealingDirectDamage.Add(Entry.Key);
        }
    }

    public static string FromSkillKey(string? key)
    {
        if (key == null || key == string.Empty)
            return string.Empty;
        else
            return key.Substring(1);
    }

    private static bool IsSkillInList(List<PgSkill> skillList, string skillKey)
    {
        foreach (PgSkill Item in skillList)
            if (Item.Key == skillKey)
                return true;

        return false;
    }

    private void InitMutationDuration()
    {
        MutationDuration = float.NaN;

        foreach (string Key in EffectObjectKeyList)
        {
            PgEffect Effect = EffectFromKey(Key);
            if (Effect.KeywordList.Contains(EffectKeyword.Mutation))
            {
                float NewDuration = Effect.Duration;
                if (float.IsNaN(MutationDuration))
                    MutationDuration = NewDuration;
                else if (MutationDuration != NewDuration)
                {
                    Debug.WriteLine("Incompatible mutation durations detected");
                }
            }
        }

        Debug.Assert(!float.IsNaN(MutationDuration));
    }

    private PgAbility AbilityFromKey(string key) => (PgAbility)ParsingContext.ObjectKeyTable[typeof(PgAbility)][key].Item;
    private PgSkill SkillFromKey(string key) => (PgSkill)ParsingContext.ObjectKeyTable[typeof(PgSkill)][key].Item;
    private PgPower PowerFromKey(string key) => (PgPower)ParsingContext.ObjectKeyTable[typeof(PgPower)][key].Item;
    private PgEffect EffectFromKey(string key) => (PgEffect)ParsingContext.ObjectKeyTable[typeof(PgEffect)][key].Item;
    private PgItem ItemFromKey(string key) => (PgItem)ParsingContext.ObjectKeyTable[typeof(PgItem)][key].Item;
    private PgSkill AbilitySkillFromKey(string key) => key.Length == 0 ? PgSkill.Unknown : (key == "AnySkill" ? PgSkill.AnySkill : SkillFromKey(key));

    private List<string> AbilityObjectKeyList = new List<string>();
    private List<string> EffectObjectKeyList = new List<string>();
    private List<string> PowerObjectKeyList = new List<string>();
    private List<string> ItemObjectKeyList = new List<string>();
    private Dictionary<string, PgSkill> SkillTable = new();
    private List<PgSkill> SkillList = new List<PgSkill>();
    private List<ItemSlot> ValidSlotList = new()
    {
        ItemSlot.MainHand,
        ItemSlot.OffHand,
        ItemSlot.Head,
        ItemSlot.Chest,
        ItemSlot.Legs,
        ItemSlot.Feet,
        ItemSlot.Hands,
        ItemSlot.Necklace,
        ItemSlot.Ring,
        ItemSlot.Racial,
        ItemSlot.Waist,
    };
    private List<PgAbility> CombatAbilityList = new();
    private const AbilityKeyword Internal_NonBasic = (AbilityKeyword)0xFFFF;
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
        AbilityKeyword.FireMagicAttack,
        AbilityKeyword.Unarmed,
        AbilityKeyword.Kick,
        AbilityKeyword.CowFrontKick,
        AbilityKeyword.Bomb,
        AbilityKeyword.PsiWave,
        AbilityKeyword.PsiHealthWave,
        AbilityKeyword.PsiArmorWave,
        AbilityKeyword.PsiPowerWave,
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
        AbilityKeyword.SurvivalUtility,
        AbilityKeyword.MajorHeal,
        AbilityKeyword.Rabbit,
        AbilityKeyword.Mutation_ExtraSkin,
        AbilityKeyword.Mutation_ExtraHeart,
        AbilityKeyword.Mutation_ExtraToes,
        AbilityKeyword.StabledPet,
        AbilityKeyword.CombatRefresh,
        AbilityKeyword.SummonZombie,
        AbilityKeyword.Shield,
        AbilityKeyword.SummonDeer,
        AbilityKeyword.SummonedSpider,
        AbilityKeyword.MinorHealTargeted,
        AbilityKeyword.PsychicAttack,
        AbilityKeyword.PsychologyAttack,
        AbilityKeyword.KnifeSlashing,
        AbilityKeyword.StaffCrushing,
        AbilityKeyword.BardBlast,
        AbilityKeyword.Melee,
        AbilityKeyword.DruidNonBasic,
        AbilityKeyword.Projectile,
        AbilityKeyword.EclipseOfShadows,
        AbilityKeyword.EmbraceOfDespair,
        AbilityKeyword.Willbreaker,
        AbilityKeyword.TrickFox,
        AbilityKeyword.SummonedTornado,
        AbilityKeyword.ShieldBash,
        AbilityKeyword.Minigolem,
        Internal_NonBasic,
    };
    private Dictionary<AbilityPetType, AbilityKeyword> PetTypeToKeywordTable = new()
    {
        { AbilityPetType.SummonedSpider, AbilityKeyword.SummonedSpider },
        { AbilityPetType.StabledPet, AbilityKeyword.StabledPet },
        { AbilityPetType.SummonedColdSphere, AbilityKeyword.SummonedColdSphere },
        { AbilityPetType.StunTrap, AbilityKeyword.StunTrap },
        { AbilityPetType.PowerGlyph, AbilityKeyword.PowerGlyph },
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
        { "Rotflesh", "Rotskin" },
        { "Wall of Coldfire", "Wall of Fire" },
        { "Wall of Healing Flame", "Wall of Fire" },
        { "Wall of Smarmy Flame", "Wall of Fire" },
        { "Charring Fireball", "Super Fireball" },
        { "Summon Sandstorm", "Summon Tornado" },
        { "Summon Doomstorm", "Summon Tornado" },
        { "Raise Flapskull", "Raise Zombie" },
    };
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
    private List<AbilityKeyword> AbilitySpecificKeywordList = new List<AbilityKeyword>()
    {
        AbilityKeyword.StrikeANerve,
        AbilityKeyword.PigChomp,
        AbilityKeyword.CowStampede,
        AbilityKeyword.CowBash,
        AbilityKeyword.MycotoxinBomb,
        AbilityKeyword.StaffPin,
        AbilityKeyword.WaveOfDarkness,
        AbilityKeyword.SparkOfDeath,
        AbilityKeyword.MultiShot,
        AbilityKeyword.HeavyMultiShot,
        AbilityKeyword.SpiderFear,
        AbilityKeyword.SpiderAcid,
        AbilityKeyword.SpiderIncubate,
        AbilityKeyword.RingOfFire,
        AbilityKeyword.CrushingBall,
        AbilityKeyword.EmbraceOfDespair,
        AbilityKeyword.RippleOfAmnesia,
        AbilityKeyword.PulseOfLife,
        AbilityKeyword.IceSpear,
        AbilityKeyword.FanOfBlades,
        AbilityKeyword.BlastOfFury,
        AbilityKeyword.BlastOfDefiance,
        AbilityKeyword.BlastOfDespair,
        AbilityKeyword.BunFuBlast,
        AbilityKeyword.CorruptHate,
    };
    private Dictionary<string, List<AbilityKeyword>> WideAbilityTable = new Dictionary<string, List<AbilityKeyword>>()
    {
        { "Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack } },
        { "Core Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack } },
        { "Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.EpicAttack } },
        { "Basic Attack", new List<AbilityKeyword>() { AbilityKeyword.BasicAttack } },
        { "Nice and Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack, AbilityKeyword.EpicAttack } },
        { "Nice Attack and Epic Attack", new List<AbilityKeyword>() { AbilityKeyword.NiceAttack, AbilityKeyword.EpicAttack } },
        { "Core and Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.CoreAttack, AbilityKeyword.NiceAttack } },
        { "Basic, Core, and Nice Attack", new List<AbilityKeyword>() { AbilityKeyword.BasicAttack, AbilityKeyword.CoreAttack, AbilityKeyword.NiceAttack } },
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
        { "All Fire Magic attack", new List<AbilityKeyword>() { AbilityKeyword.FireMagicAttack } },
        { "Unarmed attack", new List<AbilityKeyword>() { AbilityKeyword.Unarmed } },
        { "Kick attack", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "Any Kick ability", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "All kicks", new List<AbilityKeyword>() { AbilityKeyword.Kick } },
        { "Cow's Front Kick", new List<AbilityKeyword>() { AbilityKeyword.CowFrontKick } },
        { "Bomb attack", new List<AbilityKeyword>() { AbilityKeyword.Bomb } },
        { "Psi Health Wave, Armor Wave, and Power Wave", new List<AbilityKeyword>() { AbilityKeyword.PsiHealthWave, AbilityKeyword.PsiArmorWave, AbilityKeyword.PsiPowerWave } },
        { "All Psi Wave Ability", new List<AbilityKeyword>() { AbilityKeyword.PsiWave } },
        { "All types of shield Bash", new List<AbilityKeyword>() { AbilityKeyword.ShieldBash } },
        { "All Shield Bash ability", new List<AbilityKeyword>() { AbilityKeyword.ShieldBash } },
        { "All Shield ability", new List<AbilityKeyword>() { AbilityKeyword.Shield } },
        { "Hammer attack", new List<AbilityKeyword>() { AbilityKeyword.HammerAttack } },
        { "All Druid ability", new List<AbilityKeyword>() { AbilityKeyword.Druid } },
        { "All Staff attack", new List<AbilityKeyword>() { AbilityKeyword.StaffAttack } },
        { "All Ice Magic ability that hit multiple", new List<AbilityKeyword>() { AbilityKeyword.IceMagicAoE } },
        { "All Ice Magic attack that hit a single", new List<AbilityKeyword>() { AbilityKeyword.IceMagicSingleTarget } },
        { "All Ice Magic ability", new List<AbilityKeyword>() { AbilityKeyword.IceMagic } },
        { "Knife ability with 'Cut'", new List<AbilityKeyword>() { AbilityKeyword.KnifeCut } },
        { "All Knife ability WITHOUT 'Cut'", new List<AbilityKeyword>() { AbilityKeyword.KnifeNonCut } },
        { "All Knife Fighting attack", new List<AbilityKeyword>() { AbilityKeyword.Knife } },
        { "All Knife ability", new List<AbilityKeyword>() { AbilityKeyword.Knife } },
        { "Bard Songs", new List<AbilityKeyword>() { AbilityKeyword.BardSong } },
        { "All Major Healing ability targeting you", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        { "All Bun-Fu moves", new List<AbilityKeyword>() { AbilityKeyword.Rabbit } },
        { "Survival Utility", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility } },
        { "Survival Utility and Major Heal", new List<AbilityKeyword>() { AbilityKeyword.SurvivalUtility, AbilityKeyword.MajorHeal } },
        { "Extra Skin mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraSkin } },
        { "Extra Heart mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart } },
        { "Extra Heart and Extra Toes mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart, AbilityKeyword.Mutation_ExtraToes } },
        { "Extra Heart and Extra Skin mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraHeart, AbilityKeyword.Mutation_ExtraSkin } },
        { "Extra Toes mutation", new List<AbilityKeyword>() { AbilityKeyword.Mutation_ExtraToes } },
        { "Animal Handling pets", new List<AbilityKeyword>() { AbilityKeyword.StabledPet } },
        { "Allies' Combat Refreshes", new List<AbilityKeyword>() { AbilityKeyword.CombatRefresh } },
        { "Raised Zombies", new List<AbilityKeyword>() { AbilityKeyword.SummonZombie } },
        { "Major Heal", new List<AbilityKeyword>() { AbilityKeyword.MajorHeal } },
        { "Summoned Deer", new List<AbilityKeyword>() { AbilityKeyword.SummonDeer } },
        { "Incubated Spiders", new List<AbilityKeyword>() { AbilityKeyword.SummonedSpider } },
        { "Minor Healing (Targeted)", new List<AbilityKeyword>() { AbilityKeyword.MinorHealTargeted } },
        { "All Mentalism and Psychology attack", new List<AbilityKeyword>() { AbilityKeyword.PsychicAttack, AbilityKeyword.PsychologyAttack } },
        { "All non-basic attack", new List<AbilityKeyword>() { Internal_NonBasic } },
        { "Knife ability that normally deal Slashing damage", new List<AbilityKeyword>() { AbilityKeyword.KnifeSlashing } },
        { "Staff ability that normally deal Crushing damage", new List<AbilityKeyword>() { AbilityKeyword.StaffCrushing } },
        { "Bardic Blast", new List<AbilityKeyword>() { AbilityKeyword.BardBlast } },
        { "Melee ability", new List<AbilityKeyword>() { AbilityKeyword.Melee } },
        { "All Druid ability except Shillelagh", new List<AbilityKeyword>() { AbilityKeyword.DruidNonBasic } },
        { "ability that fire a projectile, such as Toxinball, Fireball, or most Archery abilities", new List<AbilityKeyword>() { AbilityKeyword.Projectile } },
        { "@ , Willbreaker, and Embrace of Despair", new List<AbilityKeyword>() { AbilityKeyword.EclipseOfShadows, AbilityKeyword.Willbreaker, AbilityKeyword.EmbraceOfDespair } },
        { "Trick Foxes", new List<AbilityKeyword>() { AbilityKeyword.TrickFox } },
        { "Summoned Tornadoes", new List<AbilityKeyword>() { AbilityKeyword.SummonedTornado } },
        { "All targets' melee attack", new List<AbilityKeyword>() { AbilityKeyword.Melee } },
        { "Your golem minion has", new List<AbilityKeyword>() { AbilityKeyword.Minigolem } },
        { "Combat Refreshes", new List<AbilityKeyword>() { AbilityKeyword.CombatRefresh } },
    };
    private static readonly Dictionary<int, string> DamageTypeTextMap = new Dictionary<int, string>()
    {
        { (int)GameDamageType.Internal_None, string.Empty },
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
    private static readonly Dictionary<int, string> SkillTextMap = new Dictionary<int, string>()
    {
        { (int)GameCombatSkill.Internal_None, string.Empty },
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
        { (int)GameCombatSkill.Bard, "Bard" },
        { (int)GameCombatSkill.Rabbit, "Rabbit" },
        { (int)GameCombatSkill.Priest, "Priest" },
        { (int)GameCombatSkill.Warden, "Warden" },
        { (int)GameCombatSkill.Lycanthropy, "Lycanthropy" },
        { (int)GameCombatSkill.SpiritFox, "Spirit Fox" },
    };
    private static readonly Dictionary<int, string> SongMap = new Dictionary<int, string>()
    {
        { (int)AbilityKeyword.SongOfBravery, "Song Of Bravery" },
        { (int)AbilityKeyword.SongOfDiscord, "Song Of Discord" },
        { (int)AbilityKeyword.SongOfResurgence, "Song Of Resurgence" },
    };
    private Dictionary<AbilityKeyword, List<PgAbility>> KeywordToAbilities = new();
    private Dictionary<CombatKeywordEx, CombatCondition> KeywordToCondition = new()
    {
        { CombatKeywordEx.RequireTwoKnives, CombatCondition.WieldingTwoKnives },
        { CombatKeywordEx.RequireNoAggro, CombatCondition.WithoutFocus },
        { CombatKeywordEx.RequirePlayingSong, CombatCondition.WhilePlayingSong },
        { CombatKeywordEx.RequireBloodMistForm, CombatCondition.WhileInBloodMistForm },
        { CombatKeywordEx.RequireSameTarget, CombatCondition.SpecificTarget },
        { CombatKeywordEx.RequireTargetOfAbility, CombatCondition.TargetOfAbility },
        { CombatKeywordEx.RequireDirectDamageKillShot, CombatCondition.DirectDamageKillShot },
        { CombatKeywordEx.RequireLowRage, CombatCondition.TargetHasLowRage },
        { CombatKeywordEx.RequireKillTarget, CombatCondition.TargetIsKilled },
        { CombatKeywordEx.RequireVulnerableTarget, CombatCondition.TargetIsVulnerable },
        { CombatKeywordEx.RequireAttackAbility, CombatCondition.AttackAbility },
        { CombatKeywordEx.RequireEliteTarget, CombatCondition.TargetIsElite },
        { CombatKeywordEx.IfTargetDies, CombatCondition.TargetKilled },
        { CombatKeywordEx.RequireMinimumDistance, CombatCondition.MinimumDistance},
        { CombatKeywordEx.BeforeTrigger, CombatCondition.AbilityNotTriggered},
        { CombatKeywordEx.OnTrigger, CombatCondition.AbilityTriggered},
        { CombatKeywordEx.RequireBeingHit, CombatCondition.OnHit},
        { CombatKeywordEx.RequireDoingDamageOverTime, CombatCondition.DoingDamageOverTime},
        { CombatKeywordEx.RequireDamageType, CombatCondition.SpecificDirectDamageType},
        { CombatKeywordEx.RequireDirectDamageType, CombatCondition.SpecificDirectDamageType},
        { CombatKeywordEx.RequireAnatomyAberration, CombatCondition.TargetAnatomyIsAberration},
    };
    private Dictionary<CombatKeywordEx, CombatKeywordEx> OverTimeEffects = new()
    {
        { CombatKeywordEx.RestoreHealth, CombatKeywordEx.RestoreHealthOverTime },
        { CombatKeywordEx.RestorePower, CombatKeywordEx.RestorePowerOverTime },
        { CombatKeywordEx.RestoreArmor, CombatKeywordEx.RestoreArmorOverTime },
        { CombatKeywordEx.RestoreHealthOrArmor, CombatKeywordEx.RestoreHealthOrArmorOverTime },
        { CombatKeywordEx.RageAttackBoost, CombatKeywordEx.RageAttackBoostOverTime },
        { CombatKeywordEx.DamageBoost, CombatKeywordEx.DamageOverTimeBoost },
    };
    private float MutationDuration;
    private List<AbilityKeyword> AbilitiesDealingDirectDamage = new();
}
