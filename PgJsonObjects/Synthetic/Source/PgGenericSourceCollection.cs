using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgGenericSourceCollection : List<IPgGenericSource>, IPgGenericSourceCollection
    {
        public const int RecipeEffectTag = 0x100;

        public static IPgGenericSource CreateItem(IGenericPgObject Parent, byte[] data, ref int offset)
        {
            IPgGenericSource Result;

            int SourceTypeValue = BitConverter.ToInt32(data, offset);

            if (SourceTypeValue < 0)
                Result = new PgMiscSource(data, ref offset);
            else
            {
                bool IsRecipeEffect;

                if ((SourceTypeValue & RecipeEffectTag) != 0)
                {
                    SourceTypeValue &= ~RecipeEffectTag;
                    IsRecipeEffect = true;
                }
                else
                    IsRecipeEffect = false;

                switch ((SourceTypes)SourceTypeValue)
                {
                    case SourceTypes.AutomaticFromSkill:
                        Result = new PgSkillupSource(data, ref offset);
                        break;

                    case SourceTypes.Item:
                        Result = new PgItemSource(data, ref offset);
                        break;

                    case SourceTypes.Training:
                        Result = new PgTrainingSource(data, ref offset);
                        break;

                    case SourceTypes.Effect:
                        if (IsRecipeEffect)
                            Result = new PgRecipeEffectSource(data, ref offset);
                        else
                            Result = new PgEffectSource(data, ref offset);
                        break;

                    case SourceTypes.Quest:
                        Result = new PgQuestSource(data, ref offset);
                        break;

                    case SourceTypes.Gift:
                        Result = new PgGiftSource(data, ref offset);
                        break;

                    case SourceTypes.HangOut:
                        Result = new PgHangOutSource(data, ref offset);
                        break;

                    default:
                        return null;
                }
            }

            Result.Init(Parent);

            return Result;
        }
    }
}
