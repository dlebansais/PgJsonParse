using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgQuestObjectiveUseRecipe : GenericPgObject, IPgQuestObjectiveUseRecipe
    {
        public PgQuestObjectiveUseRecipe(byte[] data, int offset)
            : base(data, offset)
        {
        }

        public Skill ConnectedSkill { get { return GetObject(0, ref _ConnectedSkill); } } private Skill _ConnectedSkill;
        public List<Recipe> RecipeTargetList { get { return GetObjectList(4, ref _RecipeTargetList); } } private List<Recipe> _RecipeTargetList;
        public List<Item> ResultItemList { get { return GetObjectList(8, ref _ResultItemList); } } private List<Item> _ResultItemList;
    }
}
