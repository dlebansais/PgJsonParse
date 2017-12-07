using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class QuestObjectiveUseAbility : QuestObjective
    {
        #region Init
        public QuestObjectiveUseAbility(string Description, int? RawNumber, bool? RawMustCompleteEarlierObjectivesFirst, int? MinHour, int? MaxHour, AbilityKeyword AbilityTarget)
            : base(Description, RawNumber, RawMustCompleteEarlierObjectivesFirst, MinHour, MaxHour)
        {
            this.AbilityTarget = AbilityTarget;
        }
        #endregion

        #region Properties
        public AbilityKeyword AbilityTarget { get; private set; }
        public List<Ability> AbilityTargetList { get; private set; } = new List<Ability>();
        private bool IsAbilityTargetParsed;
        #endregion

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (AbilityTarget != AbilityKeyword.Internal_None)
                    AddWithFieldSeparator(ref Result, TextMaps.AbilityKeywordTextMap[AbilityTarget]);
                foreach (Ability Ability in AbilityTargetList)
                    AddWithFieldSeparator(ref Result, Ability.Name);

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];

            AbilityTargetList = PgJsonObjects.Ability.ConnectByKeyword(ErrorInfo, AbilityTable, AbilityTarget, AbilityTargetList, ref IsAbilityTargetParsed, ref IsConnected, ParentQuest);

            return IsConnected;
        }
        #endregion
    }
}
