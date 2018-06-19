using Presentation;
using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class EquipmentBoostServerInfoEffect : ServerInfoEffect, IPgEquipmentBoostServerInfoEffect
    {
        public EquipmentBoostServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, IPgItemEffect RawAttribute, float? RawAttributeEffect)
            : base(ServerInfoEffect, RawLevel)
        {
            this.Boost = RawAttribute;
            this.RawAttributeEffect = RawAttributeEffect;
        }

        public IPgItemEffect Boost { get; private set; }
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect { get; private set; }

        public override string RawEffect
        {
            get
            {
                ItemEffect JsonBoost = Boost as ItemEffect;
                return base.RawEffect + "(" + JsonBoost.AsEffectString() + (RawAttributeEffect.HasValue ? ", " + InvariantCulture.SingleToString(RawAttributeEffect.Value) : "") + ")";
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
        public override bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = base.ConnectFields(ErrorInfo, Parent, AllTables);
            Dictionary<string, IGenericJsonObject> AttributeTable = AllTables[typeof(Attribute)];

            ItemAttributeLink AsItemAttributeLink;
            if ((AsItemAttributeLink = Boost as ItemAttributeLink) != null)
            {
                if (!AsItemAttributeLink.IsParsed)
                {
                    bool IsParsed = false;
                    IPgAttribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected, null);
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

            AddInt((int?)Type, data, ref offset, BaseOffset, 0);
            AddObject(Boost as ISerializableJsonObject, data, ref offset, BaseOffset, 4, StoredObjectTable);
            AddDouble(RawAttributeEffect, data, ref offset, BaseOffset, 8);

            FinishSerializing(data, ref offset, BaseOffset, 12, null, StoredObjectTable, null, null, null, null, null, null);
            AlignSerializedLength(ref offset);
        }
        #endregion
    }
}
