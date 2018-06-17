using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementCollection : List<AbilityRequirement>, ISerializableJsonObjectCollection
    {
        public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }

        public static IGenericPgObject CreateItem(byte[] data, int offset)
        {
            OtherRequirementType OtherRequirementType = (OtherRequirementType)BitConverter.ToInt32(data, offset);
            offset += 4;

            bool IsSingle = (((int)OtherRequirementType) & 0x8000) != 0;

            switch (OtherRequirementType)
            {
                case OtherRequirementType.IsAdmin:
                    return new PgAbilityRequirementIsAdmin(data, offset);

                case OtherRequirementType.IsLycanthrope:
                    return new PgAbilityRequirementIsLycanthrope(data, offset);

                case OtherRequirementType.CurHealth:
                    return new PgAbilityRequirementCurHealth(data, offset);

                case OtherRequirementType.Race:
                    if (IsSingle)
                        return new PgAbilityRequirementSingleRace(data, offset);
                    else
                        return new PgAbilityRequirementRace(data, offset);

                case OtherRequirementType.HasEffectKeyword:
                    return new PgAbilityRequirementHasEffectKeyword(data, offset);

                case OtherRequirementType.FullMoon:
                    return new PgAbilityRequirementFullMoon(data, offset);

                case OtherRequirementType.IsHardcore:
                    return new PgAbilityRequirementIsHardcore(data, offset);

                case OtherRequirementType.DruidEventState:
                    return new PgAbilityRequirementDruidEventState(data, offset);

                case OtherRequirementType.PetCount:
                    return new PgAbilityRequirementPetCount(data, offset);

                case OtherRequirementType.RecipeKnown:
                    return new PgAbilityRequirementRecipeKnown(data, offset);

                case OtherRequirementType.IsNotInCombat:
                    return new PgAbilityRequirementIsNotInCombat(data, offset);

                case OtherRequirementType.IsLongtimeAnimal:
                    return new PgAbilityRequirementIsLongtimeAnimal(data, offset);

                case OtherRequirementType.InHotspot:
                    return new PgAbilityRequirementInHotspot(data, offset);

                case OtherRequirementType.HasInventorySpaceFor:
                    return new PgAbilityRequirementHasInventorySpaceFor(data, offset);

                case OtherRequirementType.IsVegetarian:
                    return new PgAbilityRequirementIsVegetarian(data, offset);

                case OtherRequirementType.InGraveyard:
                    return new PgAbilityRequirementInGraveyard(data, offset);

                case OtherRequirementType.Appearance:
                    if (IsSingle)
                        return new PgAbilityRequirementSingleAppearance(data, offset);
                    else
                        return new PgAbilityRequirementAppearance(data, offset);

                case OtherRequirementType.Or:
                    return new PgAbilityRequirementOr(data, offset);

                case OtherRequirementType.EquippedItemKeyword:
                    return new PgAbilityRequirementEquippedItemKeyword(data, offset);

                case OtherRequirementType.GardenPlantMax:
                    return new PgAbilityRequirementGardenPlantMax(data, offset);

                case OtherRequirementType.InteractionFlagSet:
                    return new PgAbilityRequirementInteractionFlagSet(data, offset);

                case OtherRequirementType.IsVolunteerGuide:
                    return new PgAbilityRequirementIsVolunteerGuide(data, offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
