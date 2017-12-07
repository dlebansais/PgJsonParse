using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ServerInfoEffect
    {
        public ServerInfoEffect(ServerInfoEffectType Type, int? RawLevel)
        {
            this.Type = Type;
            this.RawLevel = RawLevel;
        }

        public void SetLinkBack(GenericJsonObject LinkBack)
        {
            this.LinkBack = LinkBack;
        }

        public ServerInfoEffectType Type { get; private set; }
        public int Level { get { return RawLevel.HasValue ? RawLevel.Value : 0; } }
        public int? RawLevel { get; private set; }
        public GenericJsonObject LinkBack { get; private set; }

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
