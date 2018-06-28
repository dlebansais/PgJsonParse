using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public abstract class ServerInfoEffect : SerializableJsonObject, IPgServerInfoEffect
    {
        public ServerInfoEffect(ServerInfoEffectType Type, int? RawLevel)
        {
            this.Type = Type;
            this.RawLevel = RawLevel;
        }

        public void SetLinkBack(IBackLinkable LinkBack)
        {
            this.LinkBack = LinkBack;
        }

        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public ServerInfoEffectType Type { get; private set; }
        public IBackLinkable LinkBack { get; private set; }
        public virtual string RawEffect
        {
            get
            {
                return StringToEnumConversion<ServerInfoEffectType>.ToString(Type) + (RawLevel.HasValue ? RawLevel.Value.ToString() : "");
            }
        }

        #region Indexing
        public virtual string TextContent
        {
            get
            {
                return TextMaps.ServerInfoEffectTypeTextMap[Type];
            }
        }
        #endregion

        #region Connecting Objects
        public virtual bool ConnectFields(ParseErrorInfo ErrorInfo, object Parent, Dictionary<Type, Dictionary<string, IGenericJsonObject>> AllTables)
        {
            bool IsConnected = false;

            return IsConnected;
        }
        #endregion
    }
}
