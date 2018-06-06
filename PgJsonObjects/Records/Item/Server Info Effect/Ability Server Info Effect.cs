using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityServerInfoEffect : ServerInfoEffect
    {
        public AbilityServerInfoEffect(ServerInfoEffectType type, int? RawLevel, string RawBestowAbility)
            : base(type, RawLevel)
        {
            this.RawBestowAbility = RawBestowAbility;
            IsRawBestowAbilityParsed = false;
        }

        private string RawBestowAbility;
        private bool IsRawBestowAbilityParsed;
        public Ability BestowAbility { get; private set; }

        public override string RawEffect
        {
            get
            {
                return base.RawEffect + "(" + RawBestowAbility + ")";
            }
        }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                if (BestowAbility != null)
                    Result += BestowAbility.Name;

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> AbilityTable = AllTables[typeof(Ability)];

            BestowAbility = Ability.ConnectSingleProperty(ErrorInfo, AbilityTable, RawBestowAbility, BestowAbility, ref IsRawBestowAbilityParsed, ref IsConnected, LinkBack);

            return IsConnected;
        }
        #endregion
    }
}
