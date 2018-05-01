using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class EquipmentBoostServerInfoEffect : ServerInfoEffect
    {
        public EquipmentBoostServerInfoEffect(ServerInfoEffectType ServerInfoEffect, int? RawLevel, ItemEffect RawAttribute, float? RawAttributeEffect)
            : base(ServerInfoEffect, RawLevel)
        {
            this.Boost = RawAttribute;
            this.RawAttributeEffect = RawAttributeEffect;
        }

        public ItemEffect Boost { get; private set; }
        public float AttributeEffect { get { return RawAttributeEffect.HasValue ? RawAttributeEffect.Value : 0; } }
        public float? RawAttributeEffect;

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
