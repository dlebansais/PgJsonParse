﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgPlayerTitle : MainPgObject<PgPlayerTitle>, IPgPlayerTitle
    {
        public PgPlayerTitle(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 16;
            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgPlayerTitle CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgPlayerTitle CreateNew(byte[] data, ref int offset)
        {
            return new PgPlayerTitle(data, ref offset);
        }

        public override string Key { get { return GetString(0); } }
        public string Title { get { return GetString(4); } }
        public string RawTitle { get { return GetString(8); } }
        public string Tooltip { get { return GetString(12); } }
        public List<TitleKeyword> KeywordList { get { return GetEnumList(16, ref _KeywordList); } } private List<TitleKeyword> _KeywordList;
    }
}
