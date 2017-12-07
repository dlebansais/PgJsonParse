﻿using PgJsonObjects;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PgJsonParse
{
    public interface IObjectDefinition
    {
        string JsonFileName { get; }
        IParser FileParser { get; }
        IList ObjectList { get; }
        Dictionary<string, IGenericJsonObject> ObjectTable { get; }
    }

    public class ObjectDefinition<T> : IObjectDefinition
         where T : GenericJsonObject<T>, new()
    {
        public ObjectDefinition(string JsonFileName)
        {
            this.JsonFileName = JsonFileName;
        }

        public string JsonFileName { get; private set; }
        public IList ObjectList { get; private set; } = new ObservableCollection<T>();
        public IParser FileParser { get; private set; } = new Parser<T>();
        public Dictionary<string, IGenericJsonObject> ObjectTable { get; private set; } = new Dictionary<string, IGenericJsonObject>();
    }
}
