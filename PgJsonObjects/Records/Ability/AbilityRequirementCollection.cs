using System;
using System.Collections.Generic;

namespace PgJsonObjects
{
    public class AbilityRequirementCollection : List<IPgAbilityRequirement>, ISerializableJsonObjectCollection
    {
        /*public ISerializableJsonObject GetAt(int index)
        {
            return this[index] as ISerializableJsonObject;
        }*/

        public static IPgAbilityRequirement CreateItem(byte[] data, ref int offset)
        {
            int TypeValue = BitConverter.ToInt32(data, offset);
            //offset += 4;

            bool IsSingle = ((TypeValue & 0x8000) != 0);
            OtherRequirementType OtherRequirementType = (OtherRequirementType)(TypeValue & ~0x8000);

            switch (OtherRequirementType)
            {
/*                case OtherRequirementType.IsAdmin:
                    return new PgAbilityRequirementIsAdmin(data, ref offset);*/

                case OtherRequirementType.IsLycanthrope:
                    return new PgAbilityRequirementIsLycanthrope(data, ref offset);

/*                case OtherRequirementType.CurHealth:
                    return new PgAbilityRequirementCurHealth(data, ref offset);*/

/*                case OtherRequirementType.Race:
                    if (IsSingle)
                        return new PgAbilityRequirementSingleRace(data, ref offset);
                    else
                        return new PgAbilityRequirementRace(data, ref offset);*/

                case OtherRequirementType.HasEffectKeyword:
                    return new PgAbilityRequirementHasEffectKeyword(data, ref offset);

                case OtherRequirementType.FullMoon:
                    return new PgAbilityRequirementFullMoon(data, ref offset);

                case OtherRequirementType.IsHardcore:
                    return new PgAbilityRequirementIsHardcore(data, ref offset);

                case OtherRequirementType.DruidEventState:
                    return new PgAbilityRequirementDruidEventState(data, ref offset);

                case OtherRequirementType.PetCount:
                    return new PgAbilityRequirementPetCount(data, ref offset);

                case OtherRequirementType.RecipeKnown:
                    return new PgAbilityRequirementRecipeKnown(data, ref offset);

                case OtherRequirementType.IsNotInCombat:
                    return new PgAbilityRequirementIsNotInCombat(data, ref offset);

                case OtherRequirementType.IsLongtimeAnimal:
                    return new PgAbilityRequirementIsLongtimeAnimal(data, ref offset);

                case OtherRequirementType.InHotspot:
                    return new PgAbilityRequirementInHotspot(data, ref offset);

                case OtherRequirementType.HasInventorySpaceFor:
                    return new PgAbilityRequirementHasInventorySpaceFor(data, ref offset);

                case OtherRequirementType.IsVegetarian:
                    return new PgAbilityRequirementIsVegetarian(data, ref offset);

                case OtherRequirementType.InGraveyard:
                    return new PgAbilityRequirementInGraveyard(data, ref offset);

/*                case OtherRequirementType.Appearance:
                    if (IsSingle)
                        return new PgAbilityRequirementSingleAppearance(data, ref offset);
                    else
                        return new PgAbilityRequirementAppearance(data, ref offset);*/

                case OtherRequirementType.Or:
                    return new PgAbilityRequirementOr(data, ref offset);

                case OtherRequirementType.EquippedItemKeyword:
                    return new PgAbilityRequirementEquippedItemKeyword(data, ref offset);

/*                case OtherRequirementType.GardenPlantMax:
                    return new PgAbilityRequirementGardenPlantMax(data, ref offset);*/

                case OtherRequirementType.InteractionFlagSet:
                    return new PgAbilityRequirementInteractionFlagSet(data, ref offset);

                case OtherRequirementType.IsVolunteerGuide:
                    return new PgAbilityRequirementIsVolunteerGuide(data, ref offset);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
