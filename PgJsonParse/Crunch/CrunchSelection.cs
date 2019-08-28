﻿using PgJsonObjects;
using System;
using System.Collections.Generic;

namespace PgJsonParse
{
    public class CrunchSelection
    {
        public const int MaxHitCount = 4;
        public static readonly TimeSpan GlobalCooldown = TimeSpan.FromSeconds(1.9);
        public static readonly Dictionary<PowerSkill, int> MaxAttainableLevel = new Dictionary<PowerSkill, int>()
        {
            { PowerSkill.AnimalHandling, 80 },
            { PowerSkill.Archery, 80 },
            { PowerSkill.BattleChemistry, 80 },
            { PowerSkill.Cow, 80 },
            { PowerSkill.Deer, 80 },
            { PowerSkill.Druid, 80 },
            { PowerSkill.FireMagic, 80 },
            { PowerSkill.GiantBat, 80 },
            { PowerSkill.Hammer, 80 },
            { PowerSkill.IceMagic, 80 },
            { PowerSkill.Knife, 80 },
            { PowerSkill.Mentalism, 80 },
            { PowerSkill.Necromancy, 80 },
            { PowerSkill.Pig, 80 },
            { PowerSkill.Psychology, 80 },
            { PowerSkill.Shield, 80 },
            { PowerSkill.Spider, 80 },
            { PowerSkill.Staff, 80 },
            { PowerSkill.Sword, 80 },
            { PowerSkill.Unarmed, 80 },
            { PowerSkill.Werewolf, 80 },
        };

        public CrunchSelection(Skill PrimarySkill, Skill SecondarySkill, List<Ability> AbilityList, Dictionary<ItemSlot, List<Power>> Gear, List<CrunchTarget> TargetList)
        {
            this.PrimarySkill = PrimarySkill;
            this.SecondarySkill = SecondarySkill;
            this.AbilityList = AbilityList;
            this.Gear = Gear;
            this.TargetList = TargetList;
        }

        public Skill PrimarySkill { get; private set; }
        public Skill SecondarySkill { get; private set; }
        public List<Ability> AbilityList { get; private set; }
        public List<Ability> BestSequenceAbilityList { get; private set; }
        public Dictionary<ItemSlot, List<Power>> Gear { get; private set; }
        public List<CrunchTarget> TargetList { get; private set; }
        public TimeSpan BestFightDuration { get; private set; }
        public double BestDPS { get; private set; }
        public double BestDPSRounded { get; private set; }

        public void Crunch()
        {
            Random rng = new Random(0);

            Sequence HitSequence = Sequence.Create(MaxHitCount, AbilityList.Count);

            BestFightDuration = TimeSpan.MaxValue;
            BestDPS = 0;
            BestSequenceAbilityList = new List<Ability>();

            while (!HitSequence.IsCompleted)
            {
                TimeSpan FightDuration;
                double DPS;
                int SequenceCount;
                Crunch(HitSequence.Array, BestFightDuration, rng, out FightDuration, out DPS, out SequenceCount);

                if (BestDPS < DPS)
                {
                    BestDPS = DPS;
                    BestFightDuration = FightDuration;
                    BestSequenceAbilityList.Clear();

                    for (int Index = 0; Index < HitSequence.Array.Length && Index < SequenceCount; Index++)
                        BestSequenceAbilityList.Add(AbilityList[HitSequence.Array[Index]]);
                }

                HitSequence.Next();
            }

            BestDPSRounded = Math.Round(BestDPS);
        }

        private void BuildNextSequence(int[] HitSequence, int SequenceIndex)
        {
            if (SequenceIndex == 0)
                return;

            for (int ArrayIndex = 0; ArrayIndex < HitSequence.Length; ArrayIndex++)
                if (HitSequence[ArrayIndex] + 1 < AbilityList.Count)
                {
                    HitSequence[ArrayIndex]++;
                    return;
                }
                else
                    HitSequence[ArrayIndex] = 0;
        }

        private void Crunch(int[] HitSequence, TimeSpan BestFightDuration, Random rng, out TimeSpan FightDuration, out double DPS, out int SequenceCount)
        {
            TimeSpan[] RefreshTable = new TimeSpan[AbilityList.Count];
            for (int i = 0; i < RefreshTable.Length; i++)
                RefreshTable[i] = TimeSpan.Zero;
            double TotalPowerConsumed = 0;
            double TotalMetabolismConsumed = 0;

            foreach (CrunchTarget Target in TargetList)
                Target.StartFight();

            FightDuration = TimeSpan.Zero;

            int HitCount;
            for (HitCount = 0; HitCount < HitSequence.Length; HitCount++)
            {
                if (FightDuration > BestFightDuration)
                    break;

                int SelectedIndex = HitSequence[HitCount];
                Ability SelectedAbility = AbilityList[SelectedIndex];

                TimeSpan RefreshTime = RefreshTable[SelectedIndex];
                FightDuration += RefreshTime;

                bool FirstTargetHit = false;
                foreach (CrunchTarget Target in TargetList)
                    if (!Target.IsDead)
                    {
                        double AttackAccuracy = rng.NextDouble();
                        Target.HitWithAbility(SelectedAbility, AttackAccuracy, FirstTargetHit);

                        if (!FirstTargetHit)
                            FirstTargetHit = true;
                    }

                TotalPowerConsumed += SelectedAbility.PvE.PowerCost;
                TotalMetabolismConsumed += SelectedAbility.PvE.MetabolismCost;

                foreach (CrunchTarget Target in TargetList)
                    if (!Target.IsDead)
                        Target.TriggerVulnerability(0.1, rng);

                for (int i = 0; i < RefreshTable.Length; i++)
                    if (RefreshTable[i] > RefreshTime)
                        RefreshTable[i] -= RefreshTime;
                    else
                        RefreshTable[i] = TimeSpan.Zero;

                RefreshTable[SelectedIndex] = TimeSpan.FromSeconds(SelectedAbility.ResetTime);

                if (SelectedAbility.SharesResetTimerWith != null)
                    if (AbilityList.Contains(SelectedAbility.SharesResetTimerWith as Ability))
                    {
                        int OtherAbilityIndex = AbilityList.IndexOf(SelectedAbility.SharesResetTimerWith as Ability);

                        if (RefreshTable[OtherAbilityIndex] < RefreshTable[SelectedIndex])
                            RefreshTable[OtherAbilityIndex] = RefreshTable[SelectedIndex];
                    }

                for (int i = 0; i < RefreshTable.Length; i++)
                    if (RefreshTable[i] < GlobalCooldown)
                        RefreshTable[i] = GlobalCooldown;
            }

            double TotalDamageReceived = 0;
            foreach (CrunchTarget Target in TargetList)
                TotalDamageReceived += Target.TotalDamageReceived;

            DPS = TotalDamageReceived / FightDuration.TotalSeconds;
            SequenceCount = HitCount;
        }
    }
}
