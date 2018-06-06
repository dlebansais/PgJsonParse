using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class TemporaryServerInfoEffect : ServerInfoEffect
    {
        public TemporaryServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, ItemEffect RawAttribute, float? RawAttributeEffect, int? RawDuration)
            : base(ServerInfoEffect, RawLevel)
        {
            Boost = RawAttribute;
            this.RawAttributeEffect = RawAttributeEffect;
            this.RawDuration = RawDuration;
        }

        public ItemEffect Boost { get; private set; }
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect;
        public int Duration { get { return RawDuration.HasValue ? RawDuration.Value : 0; } }
        public int? RawDuration;

        public override string RawEffect
        {
            get
            {
                return null;
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
                    Attribute Link = Attribute.ConnectSingleProperty(ErrorInfo, AttributeTable, AsItemAttributeLink.AttributeName, AsItemAttributeLink.Link, ref IsParsed, ref IsConnected, null);
                    AsItemAttributeLink.SetLink(Link);
                }
            }

            return IsConnected;
        }
        #endregion
    }
}
