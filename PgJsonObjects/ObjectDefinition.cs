using System.Collections.Generic;

namespace PgJsonObjects
{
    public interface IObjectDefinition
    {
        string JsonFileName { get; }
        int MinVersion { get; }
        IParser FileParser { get; }
        IMainJsonObjectCollection JsonObjectList { get; }
        Dictionary<string, IGenericJsonObject> ObjectTable { get; }
        bool LoadAsArray { get; }
        bool UseJavaFormat { get; }
    }

    public class ObjectDefinition<TJson, TPg> : IObjectDefinition
         where TJson : MainJsonObject<TJson>, new()
         where TPg : IMainPgObject
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
        public IMainJsonObjectCollection JsonObjectList { get; private set; } = new MainJsonObjectCollection<TJson, TPg>();
        public IParser FileParser { get; private set; } = new Parser<TJson>();
        public Dictionary<string, IGenericJsonObject> ObjectTable { get; private set; } = new Dictionary<string, IGenericJsonObject>();
        public bool LoadAsArray { get; private set; }
        public bool UseJavaFormat { get; private set; }
    }
}
