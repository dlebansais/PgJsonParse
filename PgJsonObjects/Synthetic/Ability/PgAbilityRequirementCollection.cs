using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class PgAbilityRequirementCollection : List<IPgAbilityRequirement>, IPgAbilityRequirementCollection
    {
        public static IPgAbilityRequirement CreateItem(byte[] data, ref int offset)
        {
            int TypeValue = BitConverter.ToInt32(data, offset);

            bool IsSingle = ((TypeValue & 0x8000) != 0);
            OtherRequirementType OtherRequirementType = (OtherRequirementType)(TypeValue & ~0x8000);
            IPgAbilityRequirement Result;

            switch (OtherRequirementType)
            {
                case OtherRequirementType.IsLycanthrope:
                    Result = PgAbilityRequirementIsLycanthrope.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.HasEffectKeyword:
                    Result = PgAbilityRequirementHasEffectKeyword.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.FullMoon:
                    Result = PgAbilityRequirementIsFullMoon.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsHardcore:
                    Result = PgAbilityRequirementIsHardcore.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.DruidEventState:
                    Result = PgAbilityRequirementDruidEventState.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.PetCount:
                    Result = PgAbilityRequirementPetCount.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.RecipeKnown:
                    Result = PgAbilityRequirementRecipeKnown.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsNotInCombat:
                    Result = PgAbilityRequirementIsNotInCombat.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsLongtimeAnimal:
                    Result = PgAbilityRequirementIsLongtimeAnimal.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.InHotspot:
                    Result = PgAbilityRequirementInHotspot.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.HasInventorySpaceFor:
                    Result = PgAbilityRequirementHasInventorySpaceFor.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsVegetarian:
                    Result = PgAbilityRequirementIsVegetarian.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.InGraveyard:
                    Result = PgAbilityRequirementIsInGraveyard.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.Or:
                    Result = PgAbilityRequirementOr.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.EquippedItemKeyword:
                    Result = PgAbilityRequirementEquippedItemKeyword.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.InteractionFlagSet:
                    Result = PgAbilityRequirementInteractionFlagSet.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsVolunteerGuide:
                    Result = PgAbilityRequirementIsVolunteerGuide.CreateNew(data, ref offset);
                    break;

                case OtherRequirementType.IsNotGuest:
                    Result = PgAbilityRequirementIsNotGuest.CreateNew(data, ref offset);
                    break;

                default:
                    throw new InvalidOperationException();
            }

            return Result;
        }
    }
}
