using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PgJsonObjects
{
    public class PgAdvancementTable : MainPgObject<PgAdvancementTable>, IPgAdvancementTable, INotifyPropertyChanged
    {
        public PgAdvancementTable(byte[] data, ref int offset)
            : base(data, offset)
        {
            offset += 8;
            int Count = FieldTableOrder.Count;
            offset += Count * 4;

            SerializableJsonObject.AlignSerializedLength(ref offset);
        }

        protected override PgAdvancementTable CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgAdvancementTable CreateNew(byte[] data, ref int offset)
        {
            PgAdvancementTable Result = new PgAdvancementTable(data, ref offset);
            Result.InitLevels();

            return Result;
        }

        public void InitLevels()
        {
            Levels = new IPgAdvancement[FieldTableOrder.Count];

            for (int i = 0; i < FieldTableOrder.Count; i++)
            {
                string Field = FieldTableOrder[i];

                FieldParser NewFieldParser = new FieldParser();
                NewFieldParser.Type = FieldType.Object;
                NewFieldParser.GetObject = GetCreatorHandler(i);
                FieldTable.Add(Field, NewFieldParser);
            }
        }

        private Func<IObjectContentGenerator> GetCreatorHandler(int index)
        {
            return () => GetLevel(index);
        }

        private IObjectContentGenerator GetLevel(int index)
        {
            return GetObject(8 + index * 4, ref Levels[index], PgAdvancement.CreateNew) as IObjectContentGenerator;
        }

        public IPgAdvancement[] Levels { get; private set; }
        public override string Key { get { return GetString(0); } }
        protected override List<string> FieldTableOrder { get { return GetStringList(4, ref _FieldTableOrder); } } private List<string> _FieldTableOrder;

        protected override Dictionary<string, FieldParser> FieldTable
        {
            get { return _FieldTable; }
        }

        private Dictionary<string, FieldParser> _FieldTable = new Dictionary<string, FieldParser>();

        #region Indirect Properties
        public override string SortingName { get { return InternalName; } }
        public string SearchResultIconFileName { get { return "icon_" + AdvancementTable.SearchResultIconId; } }
        #endregion

        public int Id { get { return KeyToId(Key); } }
        public string InternalName { get { return KeyToInternalName(Key); } }
        public string FriendlyName { get { return KeyToFriendlyName(Key); } }
        public Dictionary<int, IPgAdvancement> LevelTable
        {
            get
            {
                if (_LevelTable == null)
                {
                    _LevelTable = new Dictionary<int, IPgAdvancement>();
                    foreach (KeyValuePair<string, FieldParser> Entry in FieldTable)
                    {
                        IPgAdvancement Item = Entry.Value.GetObject() as IPgAdvancement;
                        _LevelTable.Add(Item.Id, Item);
                    }
                }

                return _LevelTable;
            }
        }
        private Dictionary<int, IPgAdvancement> _LevelTable;

        public static int KeyToId(string key)
        {
            if (key != null)
            {
                string[] Splitted = key.Split('_');
                if (Splitted.Length > 0)
                {
                    if (int.TryParse(Splitted[0], out int Result))
                        return Result;
                }
            }

            return 0;
        }

        public static string KeyToInternalName(string key)
        {
            if (key != null)
            {
                string[] Splitted = key.Split('_');
                if (Splitted.Length > 1)
                    return Splitted[1];
            }

            return null;
        }

        public static string KeyToFriendlyName(string key)
        {
            if (key != null)
            {
                string[] Splitted = key.Split('_');
                if (Splitted.Length > 1)
                {
                    string Name = Splitted[1];
                    if (AdvancementFriendlyNames.FriendlyNameTable.ContainsKey(Name))
                        return AdvancementFriendlyNames.FriendlyNameTable[Name];
                }
            }

            //Debug.WriteLine(key);
            return null;
        }

        public bool HasManyLevels { get { return FieldTable.Count > 1; } }

        public int CurrentLevel
        {
            get
            {
                if (_CurrentLevelIndex >= Levels.Length)
                    _CurrentLevelIndex = Levels.Length - 1;

                if (_CurrentLevelIndex >= 0 && _CurrentLevelIndex < Levels.Length)
                {
                    IPgAdvancement Item = GetLevel(_CurrentLevelIndex) as IPgAdvancement;
                    return Item.Id;
                }
                else
                    return 0;
            }
        }
        private static int _CurrentLevelIndex;

        public IPgAdvancement CurrentAdvancement
        {
            get
            {
                if (_CurrentLevelIndex >= Levels.Length)
                    _CurrentLevelIndex = Levels.Length - 1;

                if (_CurrentLevelIndex >= 0 && _CurrentLevelIndex < Levels.Length)
                {
                    IPgAdvancement Item = GetLevel(_CurrentLevelIndex) as IPgAdvancement;
                    return Item;
                }
                else
                    return null;
            }
        }

        public void OnLevelChange(int change)
        {
            int NewIndex = _CurrentLevelIndex + change;

            if (NewIndex >= 0 && NewIndex < LevelTable.Count)
            {
                _CurrentLevelIndex = NewIndex;
                NotifyPropertyChanged(nameof(CurrentLevel));
                NotifyPropertyChanged(nameof(CurrentAdvancement));
            }
        }

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
