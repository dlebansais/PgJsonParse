using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PgJsonObjects
{
    public interface IObjectDefinition
    {
        string JsonFileName { get; }
        int MinVersion { get; }
        IParser FileParser { get; }
        IMainJsonObjectCollection ObjectList { get; }
        Dictionary<string, IGenericJsonObject> ObjectTable { get; }
        bool LoadAsArray { get; }
        bool UseJavaFormat { get; }
    }

    public class ObjectDefinition<T> : IObjectDefinition
         where T : MainJsonObject<T>, new()
    {
        public ObjectDefinition(string jsonFileName, int minVersion, bool loadAsArray, bool useJavaFormat, bool VerifyParse)
        {
            JsonFileName = jsonFileName;
            MinVersion = minVersion;
            LoadAsArray = loadAsArray;
            UseJavaFormat = useJavaFormat;
            FileParser.VerifyParse = VerifyParse;
        }

        public string JsonFileName { get; private set; }
        public int MinVersion { get; private set; }
        public IMainJsonObjectCollection ObjectList { get; private set; } = new MainJsonObjectCollection<T>();
        public IParser FileParser { get; private set; } = new Parser<T>();
        public Dictionary<string, IGenericJsonObject> ObjectTable { get; private set; } = new Dictionary<string, IGenericJsonObject>();
        public bool LoadAsArray { get; private set; }
        public bool UseJavaFormat { get; private set; }
    }
}
