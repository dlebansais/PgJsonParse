﻿using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgNpcPreference : GenericPgObject<PgNpcPreference>, IPgNpcPreference
    {
        public PgNpcPreference(byte[] data, ref int offset)
            : base(data, offset)
        {
        }

        protected override PgNpcPreference CreateItem(byte[] data, ref int offset)
        {
            return CreateNew(data, ref offset);
        }

        public static PgNpcPreference CreateNew(byte[] data, ref int offset)
        {
            return new PgNpcPreference(data, ref offset);
        }

        public List<ItemKeyword> ItemKeywordList { get { return GetEnumList(0, ref _ItemKeywordList); } } private List<ItemKeyword> _ItemKeywordList;
        public List<string> RawKeywordList { get { return GetStringList(4, ref _RawKeywordList); } } private List<string> _RawKeywordList;
        public double Preference { get { return RawPreference.HasValue ? RawPreference.Value : 0; } }
        public double? RawPreference { get { return GetDouble(8); } }
        public int MinValueRequirement { get { return RawMinValueRequirement.HasValue ? RawMinValueRequirement.Value : 0; } }
        public int? RawMinValueRequirement { get { return GetInt(12); } }
        public IPgSkill SkillRequirement { get { return GetObject(16, ref _SkillRequirement, PgSkill.CreateNew); } } private IPgSkill _SkillRequirement;
        public ItemSlot SlotRequirement { get { return GetEnum<ItemSlot>(20); } }
        public RecipeItemKey RarityRequirement { get { return GetEnum<RecipeItemKey>(22); } }
        public RecipeItemKey MinRarityRequirement { get { return GetEnum<RecipeItemKey>(24); } }
    }
}
