using Presentation;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class TemporaryServerInfoEffect : ServerInfoEffect, IPgTemporaryServerInfoEffect
    {
        public TemporaryServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, IPgItemEffect RawAttribute, float? RawAttributeEffect, int? RawDuration)
            : base(ServerInfoEffect, RawLevel)
        {
            Boost = RawAttribute;
            this.RawAttributeEffect = RawAttributeEffect;
            this.RawDuration = RawDuration;
        }

        public IPgItemEffect Boost { get; private set; }
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get; private set; }
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration { get; private set; }

        public override string RawEffect
        {
            get
            {
                ItemEffect JsonBoost = Boost as ItemEffect;
                return base.RawEffect + "(" + JsonBoost.AsEffectString() + (RawAttributeEffect.HasValue ? "," + InvariantCulture.SingleToString(RawAttributeEffect.Value) : "") + (RawDuration.HasValue ? ",1," + RawDuration.Value : "") + ")";
            }
        }

        #region Indexing
        public override string TextContent
        {
            get
            {
                string Result = base.TextContent;

                ItemAttributeLink AsItemAttributeLink;
                if ((AsItemAttributeLink = Boost as ItemAttributeLink) != null)
                    Result += AsItemAttributeLink.AttributeName;

                return Result;
            }
        }
        #endregion

        #region Connecting Objects
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IJsonKey>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IJsonKey> AttributeTable = AllTables[typeof(Attribute)];

            ItemAttributeLink AsItemAttributeLink;
            if ((AsItemAttributeLink = Boost as ItemAttributeLink) != null)
            {
                if (!AsItemAttributeLink.IsParsed)
                {
                    bool IsParsed = false;
                    IPgAttribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected);
                    AsItemAttributeLink.SetLink(Link);
                }
            }

            return IsConnected;
        }
        #endregion

        #region Serializing
        protected override void SerializeJsonObjectInternal(byte[] data, ref int offset)
        {
            int BaseOffset = offset;
            Dictionary<int, ISerializableJsonObject> StoredObjectTable = new Dictionary<int, ISerializableJsonObject>();
            Dictionary<int, List<string>> StoredStringListTable = new Dictionary<int, List<string>>();

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddInt(((Boost is ItemAttributeLink) ? 1 : 0), data, ref offset, BaseOffset, 4);
            AddObject(Boost as ISerializableJsonObject, data, ref offset, BaseOffset, 8, StoredObjectTable);
            AddDouble(RawAttributeEffect, data, ref offset, BaseOffset, 12);
            AddInt(RawDuration, data, ref offset, BaseOffset, 16);
            AddStringList(new List<string>(), data, ref offset, BaseOffset, 20, StoredStringListTable);

            FinishSerializing(data, ref offset, BaseOffset, 24, null, StoredObjectTable, null, null, null, null, StoredStringListTable, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
