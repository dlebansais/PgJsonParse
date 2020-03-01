using System.Collections;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class ObjectDefinition
    {
        public ObjectDefinition()
        {
            Verify = ConstVerify;
            UseJson = ConstUseJson;
        }

        public bool Verify { get; }
        public bool UseJson { get; }

        public const bool ConstVerify = false;
        public const bool ConstUseJson = false;
    }

    public interface IObjectDefinition
    {
        string JsonFileName { get; }
        int MinVersion { get; }
        PgObjectCreator CreateNewObject { get; }
        IParser FileParser { get; }
        IVerifyer FileVerifyer { get; }
        Dictionary<string, IJsonKey> ObjectTable { get; }
        IMainJsonObjectCollection JsonObjectList { get; }
        IMainPgObjectCollection PgObjectList { get; }
        bool LoadAsArray { get; }
        bool LoadAsObject { get; }
        bool UseJavaFormat { get; }
        IList VerifedObjectList { get; }
    }

    public class ObjectDefinition<TJson, TPg, TI> : ObjectDefinition, IObjectDefinition
         where TJson : MainJsonObject<TJson>, TI, new()
         where TPg : IMainPgObject, IJsonKey, IObjectContentGenerator, TI
        //where TI : IJsonKey, IObjectContentGenerator
    {
        public ObjectDefinition(string jsonFileName, int minVersion, PgObjectCreator createNewObject, bool loadAsArray, bool loadAsObject, bool useJavaFormat, bool VerifyParse)
        {
            JsonFileName = jsonFileName;
            MinVersion = minVersion;
            CreateNewObject = createNewObject;
            LoadAsArray = loadAsArray;
            LoadAsObject = loadAsObject;
            UseJavaFormat = useJavaFormat;
            FileVerifyer.VerifyParse = VerifyParse;
        }

        public string JsonFileName { get; private set; }
        public int MinVersion { get; private set; }
        public PgObjectCreator CreateNewObject { get; private set; }
        public IParser FileParser { get; private set; } = new Parser<TJson, TI>();
        public IVerifyer FileVerifyer { get; private set; } = new Verifyer<TPg, TI>();
        //public IVerifyer FileVerifyer { get; private set; } = new Verifyer<TJson, TI>();
        public Dictionary<string, IJsonKey> ObjectTable { get; private set; } = new Dictionary<string, IJsonKey>();
        public IMainJsonObjectCollection JsonObjectList { get; private set; } = new MainJsonObjectCollection<TJson, TPg, TI>();
        public IMainPgObjectCollection PgObjectList { get; private set; } = new MainPgObjectCollection<TPg, TI>();
        public bool LoadAsArray { get; private set; }
        public bool LoadAsObject { get; private set; }
        public bool UseJavaFormat { get; private set; }
        public IList VerifedObjectList { get { return UseJson ? JsonObjectList as IList : PgObjectList as IList; } }
    }
}
