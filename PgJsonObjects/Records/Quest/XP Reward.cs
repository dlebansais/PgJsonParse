using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class XPReward : GenericJsonObject<SkillRequirement>
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable
        {
            get
            {
                Dictionary<string, FieldParser> Result = new Dictionary<string, FieldParser>();

                for (int i = 0; i < (int)PowerSkill.Priest; i++)
                {
                    string FieldKey = StringToEnumConversion<PowerSkill>.ToString((PowerSkill)(i + 1));
                    Result.Add(FieldKey, new FieldParser() { Type = FieldType.Integer, GetInteger = () => { return FieldSetTable.ContainsKey(FieldKey) ? (int?)FieldSetTable[FieldKey] : null; } });
                }

                return Result;
            }
        }

        private Dictionary<string, int> FieldSetTable = new Dictionary<string, int>();

        public void SetFieldValue(PowerSkill skill, int value)
        {
            if (Key == null)
                base.InitializeKey("Rewards_XP", 0, null, null);

            string FieldName = StringToEnumConversion<PowerSkill>.ToString(skill);

            foreach (KeyValuePair<string, FieldParser> Entry in FieldTable)
                if (Entry.Key == FieldName)
                {
                    FieldSetTable[FieldName] = value;
                    FieldTableOrder.Add(FieldName);
                }
        }
        #endregion

        #region Indexing
        public override string TextContent { get { return ""; } }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "Item"; } }
        #endregion
    }
}
