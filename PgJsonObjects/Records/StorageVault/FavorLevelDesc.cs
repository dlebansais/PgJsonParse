using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class FavorLevelDesc : GenericJsonObject<FavorLevelDesc>
    {
        public FavorLevelDesc()
        {
            base.InitializeKey("Levels", 0, null, null);
        }

        #region Indirect Properties
        public override string SortingName { get { return null; } }
        #endregion

        #region Parsing
        protected override Dictionary<string, FieldParser> FieldTable { get { return new Dictionary<string, FieldParser> {
            { "Despised", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawDespised = value,
                GetInteger = () => RawDespised } },
            { "Neutral", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawNeutral = value,
                GetInteger = () => RawNeutral } },
            { "Comfortable", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawComfortable = value,
                GetInteger = () => RawComfortable } },
            { "Friends", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawFriends = value,
                GetInteger = () => RawFriends } },
            { "CloseFriends", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawCloseFriends = value,
                GetInteger = () => RawCloseFriends } },
            { "BestFriends", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawBestFriends = value,
                GetInteger = () => RawBestFriends } },
            { "LikeFamily", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawLikeFamily = value,
                GetInteger = () => RawLikeFamily } },
            { "SoulMates", new FieldParser() {
                Type = FieldType.Integer,
                ParseInteger = (int value, ParseErrorInfo errorInfo) => RawSoulMates = value,
                GetInteger = () => RawSoulMates } },
        }; } }

        public void SetFavorLevel(Favor favor, int level)
        {
            string FavorName = StringToEnumConversion<Favor>.ToString(favor);

            try
            {
                FieldParser Parser = FieldTable[FavorName];

                FieldTableOrder.Add(FavorName);
                Parser.ParseInteger(level, null);
            }
            catch
            {
            }
        }

        private int? RawDespised;
        private int? RawNeutral;
        private int? RawComfortable;
        private int? RawFriends;
        private int? RawCloseFriends;
        private int? RawBestFriends;
        private int? RawLikeFamily;
        private int? RawSoulMates;
        #endregion

        #region Indexing
        public override string TextContent { get { return null; } }
        #endregion

        #region Connecting Objects
        protected override bool ConnectFields(ParseErrorInfo ErrorInfo, IBackLinkable Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            return false;
        }
        #endregion

        #region Debugging
        protected override string FieldTableName { get { return "FavorLevelDesc"; } }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
        }
        #endregion
    }
}
