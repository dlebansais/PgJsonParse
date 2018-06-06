using PgJsonReader;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class SkillRequirement : GenericJsonObject<SkillRequirement>
    {
        #region Indirect Properties
        protected override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        private static Dictionary<string, FieldParser> GlobalFieldTable = new Dictionary<string, FieldParser>();

        protected override Dictionary<string, FieldParser> FieldTable
        {
            get
            {
                if (GlobalFieldTable.Count == 0)
                {
                    for (int i = 0; i < (int)PowerSkill.Priest; i++)
                    {
                        string FieldKey = StringToEnumConversion<PowerSkill>.ToString((PowerSkill)(i + 1));
                        GlobalFieldTable.Add(FieldKey, new FieldParser() { Type = FieldType.Integer, GetInteger = () => { return FieldSetTable.ContainsKey(FieldKey) ? (int?)FieldSetTable[FieldKey] : null; } });
                    }
                }

                return GlobalFieldTable;
            }
        }

        private Dictionary<string, int> FieldSetTable = new Dictionary<string, int>();

        public void SetFieldValue(string FieldName, ItemSkillLink link)
        {
            if (Key == null)
                base.InitializeKey("SkillReqs", 0, null, null);

            foreach (KeyValuePair<string, FieldParser> Entry in FieldTable)
                if (Entry.Key == FieldName && link.SkillLevel.HasValue)
                {
                    FieldSetTable[FieldName] = link.SkillLevel.Value;
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
