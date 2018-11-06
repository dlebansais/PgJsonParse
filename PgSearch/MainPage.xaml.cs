using PgJsonObjects;
using Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace PgSearch
{
    public partial class MainPage : RootControl, INotifyPropertyChanged
    {
        #region Init
        public MainPage()
            : base(RootControlMode.ResizedWithCaption)
        {
            this.InitializeComponent();
            DataContext = this;

            DeserializeAll(310, null, null, null, ClientFiles.all.data["cache.pg"],
                           (bool success, byte[] data) => { });

            InitSearch();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SubscribeToCommand("BackwardCommand", OnBackward);
            SubscribeToCommand("ForwardCommand", OnForward);
            SubscribeToCommand("GoToCommand", OnGoTo);
        }
        #endregion

        #region Properties
        public string CurrentVersionCacheFolder { get; set; }
        #endregion

        #region Deserializing

        private void DeserializeAll(int versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback)
        {
            SerializableJsonObject.ResetSerializedObjectTable();
            GenericPgObject.ResetCreatedObjectTable();
            byte[] CurrentOffset = new byte[4];
            int progressIndex = 0;

            List<IObjectDefinition> DefinitionList = new List<IObjectDefinition>();
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
                DefinitionList.Add(Entry.Value);

            StartTask(() => DeserializeAll0(data, CurrentOffset, DefinitionList, progressIndex),
                      (bool nextStepSuccess) => DeserializeAll1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, data, callback, CurrentOffset, DefinitionList, progressIndex));
        }

        private bool DeserializeAll0(byte[] data, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            try
            {
                IObjectDefinition definition = definitionList[progressIndex];
                int Offset = BitConverter.ToInt32(currentOffset, 0);

                definition.JsonObjectList.Clear();

                IMainPgObjectCollection PgObjectList = definition.PgObjectList;
                PgObjectList.Clear();

                int Count = BitConverter.ToInt32(data, Offset);
                Offset += 4;

                int ObjectOffset = Offset;
                IMainPgObject Item;

                for (int i = 0; i < Count; i++)
                {
                    Offset = BitConverter.ToInt32(data, ObjectOffset + i * 4);

                    Item = GenericPgObject.CreateMainObject(definition.CreateNewObject, data, ref Offset);
                    PgObjectList.Add(Item);
                }

                Offset = BitConverter.ToInt32(data, ObjectOffset + Count * 4);
                Array.Copy(BitConverter.GetBytes(Offset), 0, currentOffset, 0, 4);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void DeserializeAll1(bool success, int versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback, byte[] currentOffset, List<IObjectDefinition> definitionList, int progressIndex)
        {
            if (success)
            {
                if (++progressIndex < definitionList.Count)
                    StartTask(() => DeserializeAll0(data, currentOffset, definitionList, progressIndex),
                                (bool nextStepSuccess) => DeserializeAll1(nextStepSuccess, versionInfo, errorInfo, versionFolder, iconFolder, data, callback, currentOffset, definitionList, progressIndex));
                else
                    DeserializeAll2(versionInfo, errorInfo, versionFolder, iconFolder, data, callback);
            }
            else
                callback(false, data);
        }

        private void DeserializeAll2(int versionInfo, ParseErrorInfo errorInfo, string versionFolder, string iconFolder, byte[] data, Action<bool, byte[]> callback)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition definition = Entry.Value;
                IMainPgObjectCollection PgObjectList = definition.PgObjectList;

                foreach (IGenericPgObject Item in PgObjectList)
                    if (Item is IBackLinkable AsLinkBack)
                        AsLinkBack.SortLinkBack();
            }

            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                IObjectDefinition Definition = Entry.Value;
                Dictionary<string, IJsonKey> ObjectTable = Definition.ObjectTable;
                IMainPgObjectCollection PgObjectList = Definition.PgObjectList;

                if (ObjectTable.Count == 0)
                    foreach (IJsonKey Item in PgObjectList)
                        ObjectTable.Add(Item.Key, Item);
            }

            Dictionary<string, IJsonKey> NpcTable = ObjectList.Definitions[typeof(GameNpc)].ObjectTable;
            Dictionary<string, IJsonKey> ItemTable = ObjectList.Definitions[typeof(Item)].ObjectTable;
            Dictionary<string, IJsonKey> PowerTable = ObjectList.Definitions[typeof(Power)].ObjectTable;
            Dictionary<string, IJsonKey> AttributeTable = ObjectList.Definitions[typeof(PgJsonObjects.Attribute)].ObjectTable;

            foreach (KeyValuePair<string, IJsonKey> Entry in NpcTable)
            {
                IPgGameNpc Npc = Entry.Value as IPgGameNpc;
                foreach (IPgNpcPreference NpcPreference in Npc.PreferenceList)
                    NpcPreference.InitFavorList(ItemTable);
            }

            foreach (KeyValuePair<string, IJsonKey> Entry in PowerTable)
            {
                IPgPower Power = Entry.Value as IPgPower;
                Power.InitTierList(AttributeTable);
            }

            callback(true, data);
        }
        #endregion

        #region Events
        private void OnBackward(object sender, EventArgs e)
        {
            OnBackward();
        }

        private void OnForward(object sender, EventArgs e)
        {
            OnForward();
        }

        private void OnGoTo(object sender, EventArgs e)
        {
            OnSearchTermsEntered();
        }

        private void OnSearchTermsEntered(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                OnSearchTermsEntered();
        }

        private void OnSearchCheckChanged(object sender, RoutedEventArgs e)
        {
            OnSearchCheckChanged();
        }
        #endregion

        #region Search
        public string SearchTerms { get; set; }
        public bool MustMatchCase { get; set; }
        public bool MustMatchWholeWord { get; set; }
        public bool IncludeAbility { get; set; }
        public bool IncludeDirectedGoal { get; set; }
        public bool IncludeGameNpc { get; set; }
        public bool IncludeStorageVault { get; set; }
        public bool IncludeEffect { get; set; }
        public bool IncludeItem { get; set; }
        public bool IncludeQuest { get; set; }
        public bool IncludeRecipe { get; set; }
        public bool IncludeSkill { get; set; }
        public bool IncludePower { get; set; }
        public bool IncludePlayerTitle { get; set; }
        public bool IncludeLoreBook { get; set; }
        public bool IncludeAdvancement { get; set; }

        public ObservableCollection<object> SearchResult { get; private set; }

        public object SearchSelectedItem
        {
            get { return _SearchSelectedItem; }
            set
            {
                if (_SearchSelectedItem != value)
                {
                    _SearchSelectedItem = value;
                    NotifyThisPropertyChanged();

                    int Index = CurrentSearchItem != null ? SearchHistory.IndexOf(CurrentSearchItem) : -1;
                    if (Index >= 0 && Index + 1 < SearchHistory.Count)
                        SearchHistory.RemoveRange(Index + 1, SearchHistory.Count - Index - 1);

                    SearchHistory.Add(value);
                    CurrentSearchItem = value;
                    IsBackwardEnabled = true;
                    IsForwardEnabled = false;
                }
            }
        }
        private object _SearchSelectedItem;

        public object CurrentSearchItem
        {
            get { return _CurrentSearchItem; }
            set
            {
                if (_CurrentSearchItem != value)
                {
                    _CurrentSearchItem = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        private object _CurrentSearchItem;

        public bool IsBackwardEnabled
        {
            get { return _IsBackwardEnabled; }
            private set
            {
                if (_IsBackwardEnabled != value)
                {
                    _IsBackwardEnabled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsBackwardEnabled;

        public bool IsForwardEnabled
        {
            get { return _IsForwardEnabled; }
            private set
            {
                if (_IsForwardEnabled != value)
                {
                    _IsForwardEnabled = value;
                    NotifyThisPropertyChanged();
                }
            }
        }
        public bool _IsForwardEnabled;

        private void InitSearch()
        {
            SearchTerms = "";
            MustMatchCase = false;
            MustMatchWholeWord = false;
            IncludeAbility = true;
            IncludeDirectedGoal = true;
            IncludeGameNpc = true;
            IncludeStorageVault = true;
            IncludeEffect = true;
            IncludeItem = true;
            IncludeQuest = true;
            IncludeRecipe = true;
            IncludeSkill = true;
            IncludePower = true;
            IncludePlayerTitle = true;
            IncludeLoreBook = true;
            IncludeAdvancement = true;
            SearchResult = new ObservableCollection<object>();
            SearchHistory = new List<object>();
            _CurrentSearchItem = null;
            _IsBackwardEnabled = false;
            _IsForwardEnabled = false;
        }

        private void OnSearchTermsEntered()
        {
            PerformSearch();
        }

        private void OnSearchCheckChanged()
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            if (SearchTerms == null || SearchTerms.Trim().Length < 3)
                return;

            List<string> ValidTermList = new List<string>();
            string[] AndTerms = SearchTerms.Split(new string[] { " AND " }, StringSplitOptions.None);
            string[] OrTerms = SearchTerms.Split(new string[] { " OR " }, StringSplitOptions.None);
            SearchModes SearchMode;

            if (AndTerms.Length > 1 && OrTerms.Length == 1)
            {
                SearchMode = SearchModes.And;
                foreach (string Term in AndTerms)
                    if (Term.Trim().Length > 0)
                        ValidTermList.Add(Term.Trim());
            }

            else if (AndTerms.Length == 1 && OrTerms.Length > 1)
            {
                SearchMode = SearchModes.Or;
                foreach (string Term in OrTerms)
                    if (Term.Trim().Length > 0)
                        ValidTermList.Add(Term.Trim());
            }
            else
            {
                SearchMode = SearchModes.Neither;
                ValidTermList.Add(SearchTerms.Trim());
            }

            if (ValidTermList.Count > 0)
            {
                SearchResult.Clear();
                SearchHistory.Clear();
                CurrentSearchItem = null;
                IsBackwardEnabled = false;
                IsForwardEnabled = false;

                PerformSearch(ValidTermList, SearchMode);
            }
        }

        private void PerformSearch(List<string> termList, SearchModes searchMode)
        {
            foreach (KeyValuePair<Type, IObjectDefinition> Entry in ObjectList.Definitions)
            {
                Type EntryType = Entry.Key;

                if ((EntryType == typeof(Ability) && IncludeAbility) ||
                    (EntryType == typeof(DirectedGoal) && IncludeDirectedGoal) ||
                    (EntryType == typeof(GameNpc) && IncludeGameNpc) ||
                    (EntryType == typeof(StorageVault) && IncludeStorageVault) ||
                    (EntryType == typeof(Effect) && IncludeEffect) ||
                    (EntryType == typeof(Item) && IncludeItem) ||
                    (EntryType == typeof(Quest) && IncludeQuest) ||
                    (EntryType == typeof(Recipe) && IncludeRecipe) ||
                    (EntryType == typeof(Skill) && IncludeSkill) ||
                    (EntryType == typeof(Power) && IncludePower) ||
                    (EntryType == typeof(PlayerTitle) && IncludePlayerTitle) ||
                    (EntryType == typeof(AdvancementTable) && IncludeAdvancement) ||
                    (EntryType == typeof(LoreBook) && IncludeLoreBook))
                    PerformSearch(termList, Entry.Value, searchMode);
            }
        }

        private void PerformSearch(List<string> termList, IObjectDefinition definition, SearchModes searchMode)
        {
            string IndexFilePath = $"{definition.JsonFileName}_index_txt";
            byte[] data = ClientFiles.all.data.ContainsKey(IndexFilePath) ? ClientFiles.all.data[IndexFilePath] : null;
            if (data == null)
                return;

            string TextContent = Encoding.UTF8.GetString(data);
            if (!string.IsNullOrEmpty(TextContent))
            {
                Dictionary<string, IJsonKey> ObjectTable = definition.ObjectTable;
                PerformSearch(termList, ObjectTable, TextContent, searchMode);
            }
        }

        private void PerformSearch(List<string> termList, Dictionary<string, IJsonKey> objectTable, string textContent, SearchModes searchMode)
        {
            List<IBackLinkable> Result = new List<IBackLinkable>();

            switch (searchMode)
            {
                case SearchModes.And:
                    for (int i = 0; i < termList.Count; i++)
                    {
                        List<IBackLinkable> SingleTermResult = new List<IBackLinkable>();
                        PerformSearch(termList[i], objectTable, textContent, SingleTermResult);

                        if (i == 0)
                        {
                            foreach (IBackLinkable o in SingleTermResult)
                                Result.Add(o);
                        }
                        else
                        {
                            List<IBackLinkable> ToRemove = new List<IBackLinkable>();
                            foreach (IBackLinkable o in Result)
                                if (!SingleTermResult.Contains(o))
                                    ToRemove.Add(o);

                            foreach (IBackLinkable o in ToRemove)
                                Result.Remove(o);
                        }
                    }
                    break;

                case SearchModes.Or:
                    foreach (string Term in termList)
                    {
                        List<IBackLinkable> SingleTermResult = new List<IBackLinkable>();
                        PerformSearch(Term, objectTable, textContent, SingleTermResult);

                        foreach (IBackLinkable o in SingleTermResult)
                            if (!Result.Contains(o))
                                Result.Add(o);
                    }
                    break;

                case SearchModes.Neither:
                    PerformSearch(termList[0], objectTable, textContent, Result);
                    break;
            }

            Result.Sort(GenericJsonObject.SortByName);

            foreach (object o in Result)
                SearchResult.Add(o);
        }

        private void PerformSearch(string term, Dictionary<string, IJsonKey> objectTable, string textContent, List<IBackLinkable> SingleTermResult)
        {
            int MatchIndex = -1;
            StringComparison Comparison = MustMatchCase ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;

            while ((MatchIndex = textContent.IndexOf(term, MatchIndex + 1, Comparison)) >= 0)
            {
                int KeyIndex = textContent.IndexOf(JsonGenerator.ObjectSeparator, MatchIndex + term.Length);
                if (KeyIndex < 0)
                    break;

                KeyIndex++;

                string Key = "";

                for (; ; )
                {
                    if (KeyIndex >= textContent.Length)
                        break;

                    char c = textContent[KeyIndex++];
                    if (c == '\r' || c == '\n')
                        break;

                    Key += c;
                }

                for (; ; )
                {
                    if (KeyIndex >= textContent.Length)
                        break;

                    char c = textContent[KeyIndex];
                    if (c != '\r' && c != '\n')
                        break;

                    KeyIndex++;
                }

                if (objectTable.ContainsKey(Key))
                {
                    IBackLinkable AsBackLinkable = objectTable[Key] as IBackLinkable;
                    if (AsBackLinkable != null)
                        SingleTermResult.Add(AsBackLinkable);
                    else
                        AsBackLinkable = null;
                }

                MatchIndex = KeyIndex;

                if (MatchIndex + term.Length >= textContent.Length)
                    break;

                MatchIndex--;
            }
        }

        private void OnBackward()
        {
            int Index = SearchHistory.IndexOf(CurrentSearchItem);
            if (Index > 0)
            {
                CurrentSearchItem = SearchHistory[Index - 1];

                if (Index == 1)
                    IsBackwardEnabled = false;
                IsForwardEnabled = true;
            }
        }

        private void OnForward()
        {
            int Index = SearchHistory.IndexOf(CurrentSearchItem);
            if (Index >= 0 && Index + 1 < SearchHistory.Count)
            {
                CurrentSearchItem = SearchHistory[Index + 1];

                if (Index + 1 == SearchHistory.Count - 1)
                    IsForwardEnabled = false;
                IsBackwardEnabled = true;
            }
        }

        private void OnRequestNavigate(object fromObject, string propertyName)
        {
            object ToObject = null;

            Type ObjectType = fromObject.GetType();

            if (propertyName == "hyperlink")
                ToObject = fromObject;
            else
            {
                PropertyInfo pi = ObjectType.GetProperty(propertyName);
                if (pi != null)
                    ToObject = pi.GetValue(fromObject);
            }

            if (ToObject != null)
            {
                int Index = CurrentSearchItem != null ? SearchHistory.IndexOf(CurrentSearchItem) : -1;
                if (Index >= 0 && Index + 1 < SearchHistory.Count)
                    SearchHistory.RemoveRange(Index + 1, SearchHistory.Count - Index - 1);

                SearchHistory.Add(ToObject);
                CurrentSearchItem = ToObject;
                IsBackwardEnabled = true;
                IsForwardEnabled = false;
            }
        }

        private List<object> SearchHistory;
        #endregion



        #region Implementation of INotifyPropertyChanged
        /// <summary>
        ///     Implements the PropertyChanged event.
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
