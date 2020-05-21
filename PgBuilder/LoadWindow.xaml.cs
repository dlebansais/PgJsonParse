namespace PgBuilder
{
    using PgJsonObjects;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    public partial class LoadWindow : Window, INotifyPropertyChanged
    {
        #region Constants
        public static int BUILDER_VERSION = 334;
        #endregion

        #region Init
        public LoadWindow()
        {
            InitializeComponent();
            DataContext = this;

            Progress1 = 0;
            Progress2 = 0;

            Thread LoadCachedData = new Thread(new ThreadStart(ExecuteLoadCachedData));
            LoadCachedData.Start();
        }

        private void ExecuteLoadCachedData()
        {
            if (LoadCachedData(BUILDER_VERSION))
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(OnStartLoadWindow));
            else
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(OnError));
        }

        private void OnStartLoadWindow()
        {
            MainWindow Dlg = new MainWindow();
            this.Close();

            if (Dlg.PowerKeyToCompleteEffectTable.Count > 0)
            {
                App.Current.MainWindow = Dlg;
                Dlg.Show();
            }
        }

        private void OnError()
        {
            MessageBox.Show($"This application can only use the data cache for version {BUILDER_VERSION}.");
            Close();
        }
        #endregion

        #region Properties
        public double Progress1 { get; private set; }
        public double Progress2 { get; private set; }
        #endregion

        #region Data Load
        public bool LoadCachedData(int version)
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(UserRootFolder))
                return false;

            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            if (!Directory.Exists(ApplicationFolder))
                return false;

            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");
            if (!Directory.Exists(VersionCacheFolder))
                return false;

            string[] VersionFolders = Directory.GetDirectories(VersionCacheFolder);
            bool IsFound = false;
            foreach (string Item in VersionFolders)
                if (int.TryParse(Path.GetFileName(Item), out int VersionValue) && VersionValue == version)
                {
                    IsFound = true;
                    break;
                }

            if (IsFound)
            {
                string VersionFolder = Path.Combine(VersionCacheFolder, version.ToString());
                string IconCacheFolder = Path.Combine(ApplicationFolder, "Shared Icons");
                App.IconFolder = IconCacheFolder;

                return LoadCachedData(VersionFolder, IconCacheFolder);
            }
            else
                return false;
        }

        public bool LoadCachedData(string versionFolder, string iconFolder)
        {
            try
            {
                string CacheFileName = Path.Combine(versionFolder, "cache.pg");
                if (File.Exists(CacheFileName))
                {
                    byte[] Data = LoadBinaryFile(CacheFileName);
                    DeserializeAll(versionFolder, iconFolder, Data);

                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return false;
        }

        private void DeserializeAll(string versionFolder, string iconFolder, byte[] data)
        {
            SerializableJsonObject.ResetSerializedObjectTable();
            GenericPgObject.ResetCreatedObjectTable();
            byte[] CurrentOffset = new byte[4];

            List<IObjectDefinition> DefinitionList = new List<IObjectDefinition>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                DefinitionList.Add(Entry.Value);

            for (int ProgressIndex = 0; ProgressIndex < DefinitionList.Count; ProgressIndex++)
            {
                DeserializeAll0(data, CurrentOffset, DefinitionList, ProgressIndex);

                Progress1 = ((ProgressIndex + 1) * 100.0) / DefinitionList.Count;
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(NotifyPropertyChanged), nameof(Progress1));
            }

            DeserializeAll2(versionFolder, iconFolder, data);
        }

        private bool DeserializeAll0(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            try
            {
                IObjectDefinition Definition = definitionList[progressIndex];
                int Offset = BitConverter.ToInt32(currentOffset, 0);

                Definition.JsonObjectList.Clear();

                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;
                PgObjectList.Clear();

                int Count = BitConverter.ToInt32(data, Offset);
                Offset += 4;

                int ObjectOffset = Offset;

                for (int i = 0; i < Count; i++)
                {
                    Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                    IMainPgObject Item = GenericPgObject.CreateMainObject(Definition.CreateNewObject, data, ref Offset);
                    PgObjectList.Add(Item);
                }

                Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
                Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        private void DeserializeAll2(string versionFolder, string iconFolder, byte[] data)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(PgJsonObjects.Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            int EntryIndex = 0;
            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                EntryIndex++;

                IPgPower Power = (IPgPower)Entry.Value;
                Power.InitTierList(AttributeTable);

                Progress2 = (EntryIndex * 100.0) / PowerTable.Count;
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(NotifyPropertyChanged), nameof(Progress2));
            }
        }

        private static byte[] LoadBinaryFile(string fileName)
        {
            using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BinaryReader br = new BinaryReader(fs);
            byte[] Result = br.ReadBytes((int)fs.Length);
            return Result;
        }
        #endregion

        #region Implementation of INotifyPropertyChanged
        /// <summary>
        /// Implements the PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Default parameter is mandatory with [CallerMemberName]")]
        internal void NotifyThisPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
