﻿namespace PgBuilder
{
    using System.Collections.Generic;

    public class Sentence
    {
        public Sentence(string format, CombatKeyword associatedKeyword)
        {
            Format = format;
            AssociatedKeywordList = new List<CombatKeyword>() { associatedKeyword };
            SignInterpretation = SignInterpretation.Normal;
        }

        public Sentence(string format, CombatKeyword associatedKeyword, SignInterpretation signInterpretation)
        {
            Format = format;
            AssociatedKeywordList = new List<CombatKeyword>() { associatedKeyword };
            SignInterpretation = signInterpretation;
        }

        public Sentence(string format, List<CombatKeyword> combatKeywordList)
        {
            Format = format;
            AssociatedKeywordList = combatKeywordList;
            SignInterpretation = SignInterpretation.Normal;
        }

        public Sentence(string format, List<CombatKeyword> combatKeywordList, SignInterpretation signInterpretation)
        {
            Format = format;
            AssociatedKeywordList = combatKeywordList;
            SignInterpretation = signInterpretation;
        }

        public string Format { get; }
        public List<CombatKeyword> AssociatedKeywordList { get; }
        public SignInterpretation SignInterpretation { get; }
        public bool IsUsed { get; private set; }

        public void SetUsed()
        {
            IsUsed = true;
        }
    }
}
